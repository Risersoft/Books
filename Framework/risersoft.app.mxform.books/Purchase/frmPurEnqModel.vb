Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmPurEnqModel
    Inherits clsFormDataModel
    Dim myViewItem As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
        myViewItem = Me.GetViewModel("Params")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select VendorID, VendorName, VendorType  from PurListVendor() where VendorType = 'MS' Order BY VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select CampusId,DispName, WODate, CompletedOn from mmlistCampus() Order by DispName"
        Me.AddLookupField("CampusId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select PersonId,isnull(FirstName,'') + ' ' + isnull(MidName ,'') + ' ' + isnull(LastName ,'') as PersonName from Persons"
        Me.AddLookupField("AttentionId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Attention").TableName)

        sql = "Select ItemId, ItemCode,ItemName, UnitName, ItemUnitId, OrderRateUnitId from InvListItems()"
        Me.AddLookupField("PurEnqItem", "ItemId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Items").TableName)

        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("PurEnqItem", "RateUnitId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)

        sql = "Select UnitName from ItemUnits"
        Me.AddLookupField("PurEnqItem", "UnitName", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ItemUnits").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from PurEnq where PurEnqId=" & myUtils.cValTN(prepIDX)
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML, "PurEnqID", "EnqNum")

        If frmMode = EnumfrmMode.acAddM Then
            myRow("EnqDate") = Now.Date
        End If

        Me.BindDataTable(myUtils.cValTN(frmIDX))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-3-1-1-1", True)
        str1 = "<BAND TABLE=""PurEnqItem"" IDFIELD=""PurEnqItemID""  INDEX=""0""><COL KEY=""PurEnqItemID, PurEnqID, ItemID, Qty, Rate, RateUnitID, ItemSuffix,SNum, UnitName ""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)


        myViewItem.MainGrid.BindGridData(Me.dsForm, 2)
        myViewItem.MainGrid.QuickConf("", True, "1-1", True, "Enquiry Parameter Detail")
        str1 = "<BAND TABLE=""PurEnqItemParams"" IDFIELD=""PurEnqItemParamId"" INDEX=""0""><COL KEY=""PurEnqItemParamId,PurEnqItemId,ParamName,ParamText""/></BAND>"
        myViewItem.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        If CopyIDX > 0 Then
            sql = "  Select PurEnqItemID, PurEnqID, ItemID, RateUnitID, ItemSuffix, SNum, ItemCode, Itemname, Qty, Rate, UnitName from PurListEnqItems() where PurEnqID = " & CopyIDX
            Dim dtPurEnqItem As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            sql = "Select PurEnqItemID,PurEnqItemParamID,ParamName,ParamText from PurEnqItemParams where PurEnqItemID in (Select PurEnqItemID from PurEnqItems where PurEnqID = " & CopyIDX & ")"
            Dim dtPurEnqParams As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            For Each r1 As DataRow In dtPurEnqItem.Select
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.MainGrid.myDV.Table)
                Dim rr1() As DataRow = dtPurEnqParams.Select("PurEnqItemID = " & myUtils.cValTN(r1("PurEnqItemID")) & "")

                myUtils.ChangeAll(rr1, "PurEnqItemID=" & myUtils.cValTN(r2("PurEnqItemID")))
                myUtils.CopyRows(rr1, myViewItem.MainGrid.myDV.Table)
            Next
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal PurEnqId As Integer)
        Dim Sql, Str1, Str2 As String, ds As DataSet

        Str1 = "Select PurEnqItemID, PurEnqID, ItemID, RateUnitID, ItemSuffix, SNum, ItemCode, Itemname, Qty, Rate, UnitName from PurListEnqItems() where PurEnqID = " & PurEnqId
        Str2 = "Select PurEnqItemID,PurEnqItemParamID,ParamName,ParamText from PurEnqItemParams where PurEnqItemID in (Select PurEnqItemID from PurEnqItems where PurEnqID = " & PurEnqId & ")"
        Sql = Str1 & "; " & Str2
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "PurEnqItem", 0)
        myUtils.AddTable(Me.dsForm, ds, "PurEnqItemParams", 1)

        myContext.Tables.SetAuto(Me.dsForm.Tables("PurEnqItem"), Me.dsForm.Tables("PurEnqItemParams"), "PurEnqItemID", "_PurEnqItemID")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If myRow("EnqNum").ToString.Trim.Length = 0 Then Me.AddError("EnqNum", "Please Enter Enq Num")
        If myRow("EnqDate") Is Nothing Then Me.AddError("EnqDate", "Please Enter Enq Date")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If Me.SelectedRow("campusid") Is Nothing Then Me.AddError("campusid", "Please select Campus")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim EnqDescrip As String = " Purchase Enquiry No: " & myUtils.cStrTN(myRow("EnqNum")) & ", Dt. " & Format(myRow("EnqDate"), "dd-MMM-yyyy")
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PurEnqID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, sqlForm)
                frmIDX = myRow("PurEnqID")

                myUtils.ChangeAll(dsForm.Tables("PurEnqItem").Select, "PurEnqID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PurEnqItem"), "Select PurEnqItemID, PurEnqID, ItemID, RateUnitID, ItemSuffix, SNum, Qty, Rate, UnitName from PurEnqItems")
                myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PurEnqItemParams"), "Select PurEnqItemID,PurEnqItemParamID,ParamName,ParamText from PurEnqItemParams", True)

                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(EnqDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(EnqDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
