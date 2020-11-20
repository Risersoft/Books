Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmInvoiceRecastModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False, myViewCostLot, myViewCostWBS, myViewCostCenter As clsViewModel

    Protected Overrides Sub InitViews()
        myViewCostLot = Me.GetViewModel("CostLot")
        myViewCostWBS = Me.GetViewModel("CostWBS")
        myViewCostCenter = Me.GetViewModel("CostCenter")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String
        sql = "Select CampusID, DispName, CompanyID,CampusType,TaxAreaCode, DivisionCodeList, WODate, CompletedOn, TaxAreaID, GstRegID, CampusCode from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "SELECT  VendorID, VendorName, VendorClass,TaxAreaCode, GSTIN, ImportAllow  FROM  purListVendor() Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "InvoiceType", "")
        Me.AddLookupField("InvoiceTypeCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "InvoiceTypeCode").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objIVProcFico As New clsIVProcFICO("IP", myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "InvoiceTypeCode,BillOf", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objIVProcFico.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myViewCostLot.MainGrid.MainConf("FORMATXML") = "<COL KEY=""LotNum"" CAPTION=""Lot No.""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostLot.MainGrid.BindGridData(Me.dsForm, 1)
        myViewCostLot.MainGrid.QuickConf("", True, "2-2-1", True)
        Dim str1 As String = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDFIELD, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue""/></BAND>"
        myViewCostLot.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostWBS.MainGrid.MainConf("FORMATXML") = "<COL KEY=""SerialNum"" CAPTION=""Serial No""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""WBSElemType"" CAPTION=""Element Type""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostWBS.MainGrid.BindGridData(Me.dsForm, 2)
        myViewCostWBS.MainGrid.QuickConf("", True, "2-2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDFIELD, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue""/></BAND>"
        myViewCostWBS.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostCenter.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostCenterName"" CAPTION=""Cost Center Name""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostCenter.MainGrid.BindGridData(Me.dsForm, 3)
        myViewCostCenter.MainGrid.QuickConf("", True, "2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDFIELD, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue""/></BAND>"
        myViewCostCenter.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))

        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            Me.AddDataSet("CDNInv", "Select * from Invoice where InvoiceID = " & myUtils.cValTN(myRow("CDNInvoiceID")))
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal InvoiceID As Integer)
        Dim ds As DataSet, Sql, Sql1, Sql2, Sql3 As String

        Sql1 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, PIDValue as InvoiceID, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql2 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, PIDValue as InvoiceID, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql3 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, PIDValue as InvoiceID, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""

        Sql = Sql1 & ";" & Sql2 & ";" & Sql3
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "CostLot", 0)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 1)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 2)
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")

        If Not Me.SelectedRow("CampusID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")


        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")

        If CheckPerValue() = False Then Me.AddError("", "Please Enter Correct % Value in Cost Assignment.")
        Return Me.CanSave
    End Function

    Private Function CheckPerValue() As Boolean
        Dim PerValue As Decimal
        For Each View As clsViewModel In New clsViewModel() {myViewCostLot, myViewCostWBS, myViewCostCenter}
            PerValue = PerValue + View.MainGrid.GetColSum("PerValue", "")
        Next

        If PerValue > 100 Then Return False Else Return True
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("InvoiceNum", "Accounting entry passed. Invoice can't be update.")
            End If

            If myFuncs.CheckPayment(myContext, myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("", "Payment done. Not allow to edit")
            End If

            Dim objIVProcRecast As New clsIVProcFICO("IP", myContext)
            myRow("DocType") = objIVProcRecast.DocType
            If objIVProcRecast.GetInvoiceTypeID(myRow.Row) = False Then Me.AddError("InvoiceNum", "Combination Not Available")

            If Me.SelectedRow("CampusID") Is Nothing OrElse objIVProcRecast.IsVouchDateAfterFinStart(myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), myRow("PostingDate")) = False Then
                Me.AddError("PostingDate", "Posting Date can not be less then Company Start Date.")
            End If

            myRow("AmountWV") = myUtils.cValTN(myRow("AmountTot"))

            If Me.CanSave Then
                Dim DocNumSysType As String = myFuncs.GetDocNumSysType(myContext, myUtils.cValTN(myRow("InvoiceTypeID")))
                Dim ObjVouch As New clsVoucherNum(myContext)
                ObjVouch.GetNewSerialNo("InvoiceID", DocNumSysType, myRow.Row)

                Dim oProc As New clsGSTInvoicePurch(myContext)
                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))

                Dim InvoiceRecastDescrip As String = myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Invoice Where InvoiceID = " & frmIDX)
                    frmIDX = myRow("InvoiceID")

                    ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)

                    Dim oRet = objIVProcRecast.HandleWorkflowState(myRow.Row)
                    If oRet.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoiceRecastDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoiceRecastDescrip, oRet.Message)
                        Me.AddError("", oRet.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceRecastDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Private Sub ChangeColRowwise(dt As DataTable, InvoiceID As Integer)
        myUtils.ChangeAll(dt.Select, "PIDField=InvoiceID")
        myUtils.ChangeAll(dt.Select, "PIDValue=" & InvoiceID)
    End Sub

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "invoice"
                    Dim InvoiceDate As Date = myContext.SQL.ParamValue("@InvoiceDate", Params)

                    Model = New clsViewModel(vwState, myContext)
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("DocType = 'IP' and InvoiceTypeCode in ('PF', 'PM','OF') and VendorID = (@VendorID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "' and InvoiceDate >= '" & Format(DateAdd("m", -18, InvoiceDate), "dd-MMM-yyyy") & "'", Params)
                    Dim Sql As String = "SELECT InvoiceID, InvoiceNum, InvoiceDate, AmountTot  From Invoice where " & sql1
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "1-1-1")
                Case "costlot"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myContext.CommonData.GetCompanyIDFromCampus(myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params)))
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CompanyID = " & CompanyID & " and WODate <= '" & Format(Dated, "dd-MMM-yyyy") & "' and ((EDate is Null) or (EDate >= '" & Format(Dated, "dd-MMM-yyyy") & "'))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdLotID""/>", True,, "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "costwbs"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myContext.CommonData.GetCompanyIDFromCampus(myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params)))
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CompanyID = " & CompanyID & " and StartTime <= '" & Format(Dated, "dd-MMM-yyyy") & "' and ((FinishTime is Null) or (FinishTime >= '" & Format(Dated, "dd-MMM-yyyy") & "'))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""WBSElementID""/>", True,, "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2><SQLWHERE></SQLWHERE></MODROW>")
                Case "costcenter"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myContext.CommonData.GetCompanyIDFromCampus(myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params)))
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CompanyID = " & CompanyID & " and Sdate <= '" & Format(Dated, "dd-MMM-yyyy") & "' and ((EDate is Null) or (EDate >= '" & Format(Dated, "dd-MMM-yyyy") & "'))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""CostCenterID""/>", True,, "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function
End Class
