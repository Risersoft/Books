Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmRejectNoteModel
    Inherits clsFormDataModel
    Dim myViewHist As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        myViewHist = Me.GetViewModel("PurItemHist")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, CompanyID, WODate, CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "SELECT  VendorID, VendorName, VendorClass FROM  purListVendor() where VendorType = 'MS' Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim ObjRejectNoteProc As New clsRejectNoteProc(myContext)
        Dim Sql, str1 As String
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Sql = "Select RejectNoteID,NoteNum,NoteDate,VendorID,CampusID,DocSysNum,FinYearID,Remarks,InvoiceID,InvoiceNum,InvoiceDate from purListRNote() Where RejectNoteID = " & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("NoteDate") = Now.Date
        End If

        BindDataTable(myUtils.cValTN(prepIDX))
        ObjRejectNoteProc.LoadVouch(myUtils.cValTN(myRow("RejectNoteID")))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-2-1-1-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""RejectNoteItem"" IDFIELD=""RejectNoteItemID""></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        myViewHist.MainGrid.BindGridData(Me.dsForm, 2)
        myViewHist.MainGrid.QuickConf("", True, "1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PurItemHist"" IDFIELD=""PurItemHistID""><COL KEY =""RejectNoteItemID""/></BAND>"
        myViewHist.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)
        Me.FormPrepared = True

        Return Me.FormPrepared
    End Function

    Public Function BindDataTable(ByVal RejectNoteId As Integer) As DataSet
        Dim ds As DataSet, Sql, str1, str2 As String

        str1 = "Select RejectNoteItemID, RejectNoteID, PurItemID, ItemCode, ItemName, QtyRej, QtyDevi, Reasons, ActionTaken from purListRNoteItems()  Where RejectNoteID = " & myUtils.cValTN(RejectNoteId) & ""
        str2 = "Select PurItemHistID, PurItemID, RejectNoteItemID, VouchNum, VouchDate, QtyRej, QtyDevi from purListItemHist()  Where RejectNoteItemID in (Select RejectNoteItemID from RejectNoteItem where RejectNoteId = " & myUtils.cValTN(RejectNoteId) & ")"

        Sql = "" & str1 & "; " & str2 & ""
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(dsForm, ds, "RejectNoteItem", 0)
        myUtils.AddTable(dsForm, ds, "PurItemHist", 1)

        myContext.Tables.SetAuto(dsForm.Tables("RejectNoteItem"), dsForm.Tables("PurItemHist"), "RejectNoteItemID", "_PurItemHist")
        Return dsForm
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            Dim ObjVouch As New clsVoucherNum(myContext)
            ObjVouch.GetNewSerialNo("RejectNoteID", "", myRow.Row)
            Dim RateNoteDescrip As String = " Num: " & myRow("NoteNum").ToString & " Dt: " & Format(myRow("NoteDate"), "dd-MMM-yyyy")

            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "RejectNoteID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select RejectNoteID,NoteNum,NoteDate,VendorID,CampusID,DocSysNum,FinYearID,Remarks,InvoiceID from RejectNote Where RejectNoteID = " & frmIDX)
                frmIDX = myRow("RejectNoteID")

                myUtils.ChangeAll(dsForm.Tables("RejectNoteItem").Select, "RejectNoteID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("RejectNoteItem"), "Select RejectNoteItemID, RejectNoteID, PurItemID, QtyRej, QtyDevi, Reasons, ActionTaken from RejectNoteItem")

                Dim ObjRejectNoteProc As New clsRejectNoteProc(myContext)
                ObjRejectNoteProc.LoadVouch(myUtils.cValTN(myRow("RejectNoteID")))
                ObjRejectNoteProc.SavePurItemHist(dsForm.Tables("PurItemHist"))

                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(RateNoteDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(RateNoteDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "invoice"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("VendorID = @vendorid and CampusID = @campusid and InvoiceID <> (@invoiceid) and InvoiceID in (Select InvoiceID from purListItemHist() where (isnull(QtyDevi, 0) > 0 or isnull(QtyRej,0) > 0))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""InvoiceID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "rejectnoteitem"
                    Dim Str As String = myContext.SQL.PopulateSQLParams("and InvoiceID = (@invoiceid) and (RejectNoteItemID in (Select RejectNoteItemID from RejectNoteItem where RejectNoteId = (@rejectnoteid)) or RejectNoteItemID is Null) ", Params)
                    Dim Sql1 As String = "Select RejectNoteItemID, RejectNoteID, PurItemID, ItemCode, ItemName, Sum(QtyRej) as QtyRej, sum(QtyDevi) as QtyDevi from purListItemHist() where (isnull(QtyDevi, 0) > 0 or isnull(QtyRej,0) > 0) " & Str & " Group BY RejectNoteItemID, RejectNoteID, PurItemID, ItemCode, ItemName"
                    Dim Sql2 As String = "Select PurItemHistID, PurItemID, RejectNoteItemID, VouchNum, VouchDate, QtyRej, QtyDevi from purListItemHist() where (isnull(QtyDevi, 0) > 0 or isnull(QtyRej,0) > 0) " & Str & ""
                    oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, "" & Sql1 & "; " & Sql2 & "")
            End Select
        End If
        Return oRet
    End Function
End Class
