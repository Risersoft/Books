Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmAccVouchModel
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

        sql = myFuncsBase.CodeWordSQL("AccVouch", "AccVouchType", "")
        Me.AddLookupField("AccVouchType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AccVouchType").TableName)

        sql = myFuncsBase.CodeWordSQL("AccVouch", "AdjustType", "")
        Me.AddLookupField("AdjustType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AdjustType").TableName)

        sql = "Select GLAccountID, AccCode, AccName, SubLedgerType, StartDate, FinishDate from GLAccount Order by AccName"
        Me.AddLookupField("AccVouchItem", "GLAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GLAccount").TableName)

        sql = "SELECT  VendorID, VendorName FROM  purListVendor() Order By VendorName"
        Me.AddLookupField("AccVouchItem", "VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "SELECT  CustomerID, PartyName FROM slsListCustomer() Order By PartyName"
        Me.AddLookupField("AccVouchItem", "CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = "Select FixedAssetID, AssetName from FixedAsset Order By AssetName"
        Me.AddLookupField("AccVouchItem", "FixedAssetID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "FixedAsset").TableName)

        sql = "Select CampusID, DispName, WODate,CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("AccVouchItem", "CampusId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select EmployeeID, (EmpCode +' | '+ FullName) as FullName, JoinDate, LeaveDate, FullName as SubLedgerValue from hrpListAllEmp() Order By FullName"
        Me.AddLookupField("AccVouchItem", "EmployeeId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Employee").TableName)

        sql = myFuncsBase.CodeWordSQL("AccVouch", "AmountDC", "")
        Me.AddLookupField("AccVouchItem", "AmountDC", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AmountDC").TableName)

        sql = "select TaxAreaID, Descrip from TaxArea Order by Descrip"
        Me.AddLookupField("TaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxAreaCode").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from AccVouch Where AccVouchID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML, "AccVouchID,VoucherNum")

        If frmMode = EnumfrmMode.acAddM OrElse frmMode = EnumfrmMode.acCopyM Then
            myRow("VouchDate") = Now.Date
            myRow("AccVouchType") = "AV"
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
        myView.MainGrid.QuickConf("", True, "1-1-1-2-1-1-2")
        str1 = "<BAND IDFIELD=""AccVouchItemID"" TABLE=""AccVouchItem"" INDEX=""0""><COL KEY=""AccVouchItemID, AccVouchID, GLAccountID, Descrip, AmountDC, Amount, VendorID,TaxAreaID, CustomerID, FixedAssetID, EmployeeID, CampusID, ClearingAccVouchItemID, SLIDValue, InvoiceID""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)


        If CopyIDX > 0 Then
            sql = "Select AccVouchItemID, AccVouchID, GLAccountID, VendorID, InvoiceID, CustomerID, FixedAssetID, EmployeeID,TaxAreaID, CampusID, ClearingAccVouchItemID, SLIDValue, SubLedgerTypeDescrip, SubLedgerTypeValue, AccCode, ACCName, AmountDC, Amount, Descrip from accListVouchItem()  Where AccVouchID = " & CopyIDX
            Dim dtAccVouchItem As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            myUtils.CopyRows(dtAccVouchItem.Select, myView.MainGrid.myDV.Table)
        End If

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "AccVouchID", myUtils.cValTN(myRow("AccVouchID"))), GetType(Boolean), False))

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal AccVouchID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = "Select AccVouchItemID, AccVouchID, GLAccountID, VendorID, InvoiceID, CustomerID, FixedAssetID, EmployeeID,TaxAreaID, CampusID, ClearingAccVouchItemID, SLIDValue, SubLedgerTypeDescrip, SubLedgerTypeValue, AccCode, ACCName, AmountDC, Amount, Descrip from accListVouchItem()  Where AccVouchID = " & AccVouchID
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "AccVouchItem", 0)
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("AccVouchType") Is Nothing Then Me.AddError("AccVouchType", "Please select Voucher Type")
        If Me.SelectedRow("CompanyID") Is Nothing Then Me.AddError("CompanyID", "Please select Company")
        If Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Vouchers")

        If Not Me.SelectedRow("CompanyID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.myRow("CompanyID")), Me.myRow("VouchDate"), PPFinal)
        End If

        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("VouchDate", "Please Select Valid Post Period")

        If myView.MainGrid.GetColSum("Amount", "AmountDC = 'D'") <> myView.MainGrid.GetColSum("Amount", "AmountDC = 'C'") Then Me.AddError("", "Please Enter Correct Amount")

        If myRow("VouchDate") > Now.Date Then Me.AddError("VouchDate", "Please Select Valid Date.")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            myRow("PayTypeID") = 1
            Dim objAccVouch As New clsAVProc(myContext)
            objAccVouch.LoadVouch(myUtils.cValTN(frmIDX))
            objAccVouch.LoadVouch(myRow.Row.Table, myView.MainGrid.myDV.Table)
            If Me.CanSave Then
                Dim VouchDescrip As String = myRow("vouchernum").ToString & " Dt. " & Format(myRow("vouchdate"), "dd-MMM-yyyy")
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "accvouchid", frmIDX)
                    Dim Oret As clsProcOutput = objAccVouch.VSave()
                    If Oret.Success Then Oret = objAccVouch.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmIDX = myRow("accvouchid")
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

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim objBS As New clsAccBS(myContext)
        Dim Model As clsViewModel = Nothing


        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToUpper
                Case "LEDGER"
                    Model = New clsViewModel(vwState, myContext)
                    Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
                    Dim GLAccountID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@glaccountid", Params))
                    Dim OpenBalDate As String = myUtils.cStrTN(myContext.SQL.ParamValue("@openbaldate", Params))

                    oRet.Data = objBS.AccountLedger(CompanyID, GLAccountID, Now.Date, OpenBalDate, "").DataSet
                    Model.MainGrid.BindGridData(oRet.Data, 1)
                    Model.MainGrid.QuickConf("", True, "1-2-1-1-2-1-1-0-0-0-0", , "Ledger")
                Case "EMPLOYEE"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str4 As String = myFuncs.FilterTimeDependent(Dated, "JoinDate", "LeaveDate", 12)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""EmployeeID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(str4) & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "generate"
                    Dim CompanyId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim AdjustType As String = myContext.SQL.ParamValue("@AdjustType", Params)
                    Dim oBS As New clsAccBS(myContext)
                    oRet.Data = oBS.GenerateClosingItems(CompanyId, Dated, AdjustType)
                Case "missingdocsysnum"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CompanyID", Params))
                    Dim FormatType As String = myContext.SQL.ParamValue("@FormatType", Params)
                    oRet.ID = myFuncs.MissingDocSysNum(myContext, Dated, CompanyID, FormatType)
            End Select
        End If
        Return oRet
    End Function
End Class
