Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmCostVouchModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CompanyID, CompName  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = myFuncsBase.CodeWordSQL("CostVouch", "CostVouchType", "")
        Me.AddLookupField("CostVouchType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "CostVouchType").TableName)

        sql = "Select CostElementID, CostElemCode, CostElemName, CostElemType from CostElement  Order by CostElemCode"
        Me.AddLookupField("CostVouchItem", "CostElementID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "CostElement").TableName)

        sql = "Select ProdLotID, WoInfo + ' Lot - ' + LotNum, CompanyID from plnListProdLots()"
        Me.AddLookupField("CostVouchItem", "ProdLotID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ProdLot").TableName)

        sql = "Select CostCenterID,CostCenterName, CompanyID from accListCostcenter()"
        Me.AddLookupField("CostVouchItem", "CostCenterID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "CostCenter").TableName)

        sql = "Select WBSElementID, DescripWo, CompanyID from prjListWBSElement()"
        Me.AddLookupField("CostVouchItem", "WBSElementID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "WBSElement").TableName)

        sql = myFuncsBase.CodeWordSQL("AccVouch", "AmountDC", "")
        Me.AddLookupField("CostVouchItem", "AmountDC", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AmountDC").TableName)

        sql = "Select CodeWord, DescripShort from CodeWords where CodeClass = 'CostVouch' and CodeType = 'CostObject'"
        Me.AddLookupField("CostVouchItem", "IDField", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "IDField").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from CostVouch Where CostVouchID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML, "CostVouchID,VoucherNum")

        If frmMode = EnumfrmMode.acAddM OrElse frmMode = EnumfrmMode.acCopyM Then
            myRow("VouchDate") = Now.Date
            myRow("AccVouchType") = "CV"
            myRow("DocSysNum") = DBNull.Value
        Else
            Dim oMasterData As New clsMasterDataFICO(myContext)
            Dim rPostPeriod As DataRow = oMasterData.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Me.BindDataTable(myUtils.cValTN(frmIDX))
        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-2-1-1-2")
        str1 = "<BAND IDFIELD=""CostVouchItemID"" TABLE=""CostVouchItem"" INDEX=""0""><COL KEY=""CostVouchItemID, CostVouchID, CostElementID, AmountDC, Amount, VendorID,TaxAreaID, CustomerID, FixedAssetID, EmployeeID, CampusID, ClearingCostVouchItemID, SLIDValue, InvoiceID""/><COL KEY=""Descrip"" CAPTION=""Description""/><COL KEY=""CostElemCode"" CAPTION=""Element Code""/><COL KEY=""CostElemName"" CAPTION=""Element Name""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)


        If CopyIDX > 0 Then
            sql = "Select CostVouchItemID, CostVouchID, CostElementID, CostCenterID, ProdLotID,WBSElementID,ClearingCostVouchItemID,IDField,IDValue,Descrip,CostElemCode,CostElemName,AmountDC,Amount from accListCostVouchItem()  Where CostVouchID = " & CopyIDX
            Dim dtAccVouchItem As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            myUtils.CopyRows(dtAccVouchItem.Select, myView.MainGrid.myDV.Table)
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal CostVouchID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = "Select CostVouchItemID, CostVouchID, CostElementID, CostCenterID, ProdLotID, WBSElementID, ClearingCostVouchItemID,Descrip,IDField,IDValue,CostElemCode,CostElemName,AmountDC,Amount from accListCostVouchItem()  Where CostVouchID = " & CostVouchID
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "CostVouchItem", 0)
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Vouchers")
        myFuncs.ValidPostPeriod(myContext, Me, Nothing, Me.myRow("VouchDate"), PPFinal)
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("VouchDate", "Please Select Valid Post Period")
        If myRow("VouchDate") > Now.Date Then Me.AddError("VouchDate", "Please Select Valid Date.")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            myRow("PayTypeID") = 1
            Dim objCostVouch As New clsCostVouchProc(myContext)
            objCostVouch.LoadVouch(myUtils.cValTN(frmIDX))
            objCostVouch.LoadVouch(myRow.Row.Table, myView.MainGrid.myDV.Table)
            If Me.CanSave Then
                Dim VouchDescrip As String = myRow("vouchernum").ToString & " Dt. " & Format(myRow("vouchdate"), "dd-MMM-yyyy")
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CostVouchId", frmIDX)
                    Dim Oret As clsProcOutput = objCostVouch.VSave()
                    If Oret.Success Then Oret = objCostVouch.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmIDX = myRow("CostVouchId")
                        myContext.Provider.dbConn.CommitTransaction(VouchDescrip, frmIDX)
                        frmMode = EnumfrmMode.acEditM
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(VouchDescrip, Oret.Message)
                        Me.AddError("", Oret.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(VouchDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function
End Class