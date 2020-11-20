Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmIDNoteModel
    Inherits clsFormDataModel
    Dim myViewItem As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
        myViewItem = Me.GetViewModel("NoteHistory")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select VendorID, VendorName, VendorType  from PurListVendor() where VendorID in (Select VendorID from PurOrder ) Order BY VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select MatDepID, DepName, CampusID, WODate, CompletedOn from mmListDepsMat() where IsShop = 1 Order by DepName"
        Me.AddLookupField("MOMatDepID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "MatDep").TableName)

        sql = "Select CampusID, DispName, WODate, CompletedOn from mmlistCampus() Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = myFuncsBase.CodeWordSQL("Purch", "OrderType", "")
        Me.AddLookupField("OrderType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "OrderType").TableName)

        sql = "Select ItemId, ItemCode, ItemName, UnitName, ItemUnitId, OrderQtyUnitId, OrderRateUnitId from InvListItems()  Order by ItemName"
        Me.AddLookupField("IDNoteItem", "ItemId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Items").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String
        Dim ObjIDNoteProc As New clsIDNoteProc(myContext)

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from IDNote Where IDNoteID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("NoteDate") = Now.Date
            myRow("ExpDate") = Now.Date
        End If

        ObjIDNoteProc.LoadVouch(myUtils.cValTN(myRow("IDNoteID")))

        sql = " Select IDNoteItemID, IDNoteID, PurItemID, ItemVMSID, ItemID, SpecWrite, OrderNum, OrderDate, ItemCode, ItemName, TotalQty, 0.00 as PreBalance, Qty, 0.00 as Balance, Remark from trpListIDNoteItem() where IDNoteID = " & prepIDX
        myView.MainGrid.QuickConf(sql, True, "1-1-1-2-1-1-1-1-1")
        str1 = "<BAND IDFIELD=""IDNoteItemID"" TABLE=""IDNoteItem"" INDEX=""0""><COL KEY="" IDNoteItemID, IDNoteID, PurItemID, Qty, Remark""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        Dim str2 As String = myUtils.MakeCSV(myView.MainGrid.myDS.Tables(0).Select, "PurItemID")
        Dim ds As DataSet = GetNoteHistory(myUtils.cValTN(myRow("IDNoteID")), str2)
        myViewItem.MainGrid.BindGridData(ds, 0)
        myViewItem.MainGrid.QuickConf("", True, "1-1-1")

        If myUtils.cValTN(myRow("MatVouchID")) > 0 Then
            Me.AddError("NoteNum", "MatVouch is created against this IDNote. Can't be Edited.")
            Me.FormPrepared = False
        Else
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("OrderType") Is Nothing Then Me.AddError("OrderType", "Please Select Order Type")
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If myUtils.cStrTN(myRow("OrderType")) <> "MO" AndAlso Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If myUtils.cStrTN(myRow("OrderType")) = "MO" AndAlso Me.SelectedRow("MOMatDepID") Is Nothing Then Me.AddError("MOMatDepID", "Please select Department")
        If Me.myView.MainGrid.myDS.Tables(0).Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")
        If CheckBalance() = False Then Me.AddError("", "Balance can't be less then Zero")

        If myUtils.cValTN(myRow("MatVouchID")) > 0 Then Me.AddError("", "Material Voucher already created. Not allow to edit")
        Return Me.CanSave
    End Function

    Private Function CheckBalance() As Boolean
        For Each r1 As DataRow In myView.MainGrid.myDS.Tables(0).Select
            If myUtils.cValTN(r1("Balance")) < 0 Then
                CheckBalance = False
                Exit For
            Else
                CheckBalance = True
            End If
        Next
        Return CheckBalance
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim ObjIDNoteProc As New clsIDNoteProc(myContext)
            ObjIDNoteProc.LoadVouch(myUtils.cValTN(myRow("IDNoteID")))
            ObjIDNoteProc.GenerateVoucherDelta(myRow.Row.Table, myView.MainGrid.myDS.Tables(0))
            Dim ObjVouch As New clsVoucherNum(myContext)
            ObjVouch.GetNewSerialNo("IDNoteId", "", myRow.Row)

            Dim r1 As DataRow = myContext.CommonData.FinRow(myRow("NoteDate"))
            If Not myUtils.NullNot(r1) Then
                myRow("FinYearID") = r1("FinYearID")
            End If

            Dim IDNoteDescrip As String = " Incoming Delivery Note Num: " & myUtils.cStrTN(myRow("NoteNum")) & " Dt: " & Format(myRow("NoteDate"), "dd-MMM-yyyy")

            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "IDNoteId", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                frmIDX = myRow("IDNoteId")

                myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, "IDNoteId=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDS.Tables(0), "Select IDNoteItemID, IDNoteID, PurItemID,  Qty, Remark from IDNoteItem")

                ObjIDNoteProc.UpdateBalance()
                frmMode = EnumfrmMode.acEditM

                myContext.Provider.dbConn.CommitTransaction(IDNoteDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(IDNoteDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "puritems"
                Dim sql As String = "Select PurItemID, PurOrderID,OrderNum, OrderDate, ItemvmsID, ItemID, ItemCode, ItemName, SpecWrite, TotalQty from PurListOItems() where PurOrderID  = " & myUtils.cValTN(frmIDX) & " "
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
        End Select
        Return oRet
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "notehistory"
                    Dim IdNoteID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@idnoteid", Params))
                    Dim PurItemIDCSV As String = myUtils.cStrTN(myContext.SQL.ParamValue("@puritemidcsv", Params))
                    oRet.Data = GetNoteHistory(IdNoteID, PurItemIDCSV)
            End Select
        End If
        Return oRet
    End Function

    Private Function GetNoteHistory(IdNoteID As Integer, PurItemIDCSV As String) As DataSet
        Dim Sql As String = " Select IDNoteID, IDNoteItemID, PurItemID, NoteNum, NoteDate, Qty from trpListIDNoteItem() where IdNoteId <> " & IdNoteID & " and PurItemID in (" & PurItemIDCSV & ")"
        Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
        Return ds
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, Sql As String = "", Str1 As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "purorder"
                    Dim MoMatDepID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@momatdepid", Params))
                    Dim VendorID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@vendorid", Params))

                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("Select PriceSlabID from PriceSlab where PriceProcID in (Select PriceProcID from PriceSlab where PriceSlabID = @priceslabid)", Params)
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim PriceSlabIDCSV As String = myUtils.MakeCSV(ds.Tables(0).Select, "PriceSlabID")
                        Str1 = " and PriceSlabID in (" & PriceSlabIDCSV & ")"
                    End If

                    If MoMatDepID > 0 Then
                        Sql = myContext.SQL.PopulateSQLParams("CampusID = @campusid and OrderType = @ordertype and MatDepID = " & MoMatDepID & " and PurItemID not in (@puritemidcsv) and IsCompleted = 0", Params)
                    ElseIf VendorID > 0 Then
                        Sql = myContext.SQL.PopulateSQLParams("CampusID = @campusid and OrderType = @ordertype and VendorID = " & VendorID & " and PurItemID not in (@puritemidcsv) and IsCompleted = 0 " & Str1 & "", Params)
                    End If
                    If Sql.Trim.Length > 0 Then Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PurOrderID""/>", False, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function
End Class
