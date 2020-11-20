Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.app.mxform.gst

<DataContract>
Public Class frmInvoicePurchModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim myViewFASO, myViewCostLot, myViewCostWBS, myViewCostCenter As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        myViewFASO = Me.GetViewModel("FASO")
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
        Me.AddLookupField("DeliveryCampusID", "Campus")

        sql = "SELECT  VendorID, VendorName, VendorClass,TaxAreaCode, GSTIN, ImportAllow  FROM  purListVendor() Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Material', 'Asset', 'Service') and CodeWord in ('APN', 'APU', 'ARO', 'ARW', 'RM','CG','CT','ST','CC','NC','CA','NA','Exp')  Order by CodeClass"
        Me.AddLookupField("InvoiceItem", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransType").TableName)

        sql = "Select Class as ClassCode, Class, ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass Where ClassType = 'M' or (ClassType = 'S' and ClassSubType in ('P','B')) or ClassType = 'A' Order by Class"
        Me.AddLookupField("InvoiceItem", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "CodeWord in ('A','M','S')")
        Me.AddLookupField("InvoiceItem", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "InvoiceType", "(CodeWord = 'PF' or CodeWord = 'FD' or CodeWord = 'FC')")
        Me.AddLookupField("InvoiceTypeCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "InvoiceTypeCode").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "BillOf", "")
        Me.AddLookupField("BillOf", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BillOf").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", "Tag2 = 'IP'")
        Me.AddLookupField("GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "GSTTypecode", "Tag2= 'IP'")
        Me.AddLookupField("GSTInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = "Select Code, Code + '-' + Description as Description, Ty from HsnSac Order by Code"
        Me.AddLookupField("Hsn_Sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass, Tag from CodeWords  where CodeClass = 'Invoice' and CodeType in ('B2B','CDN')  Order by CodeClass"
        Me.AddLookupField("GSTInvoiceSubType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceSubType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxCredit", "")
        Me.AddLookupField("TaxCredit", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxCredit").TableName)

        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("InvoiceItem", "ItemUnitID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

        sql = "Select * from systemoptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objIVProcFico As New clsIVProcFICO("IP", myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "SalesOrderID,InvoiceTypeCode,BillOf", oView, prepMode, prepIDX, strXML, "InvoiceID,InvoiceNum")

        If frmMode = EnumfrmMode.acAddM OrElse frmMode = EnumfrmMode.acCopyM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objIVProcFico.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Me.BindDataTable(myUtils.cValTN(frmIDX), Me.dsForm)
        objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("InvoiceItem"))

        objIVProcFico.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "2-1-1-1", True)
        Dim str1 As String = "<BAND INDEX = ""0"" TABLE = ""InvoiceItem"" IDFIELD=""InvoiceItemID""><COL KEY ="" InvoiceItemID, InvoiceID, ClassType, TransType,ItemUnitID, ValuationClass, PriceSlabID, BasicRate, AmountBasic, Description, AmountTot, AmountWV,HSN_SC, TaxCredit""/><COL  NOEDIT=""TRUE"" KEY=""QtyRate"" CAPTION=""Qty""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
            myRow("InvoiceTypeCode") = "PF"
            myRow("BillOf") = "P"

            sql = "Select SOSpareID, SpareName, PIDInfo from slsListSOSpares() where SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID"))
            Me.AddLookupField("ODNoteSpare", "SoSpareID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SOSpare").TableName)
            Me.AddLookupField("ODNoteSpare", "PIDInfoSp", "SOSpare")

            sql = "Select SOServiceID, ServiceName, PIDInfo from slsListSOService() Where SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID"))
            Me.AddLookupField("ODNoteSpare", "SoServiceID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SOService").TableName)
            Me.AddLookupField("ODNoteSpare", "PIDInfoSvc", "SOService")
        End If

        Dim dtFA As DataTable = dsForm.Tables("FixedAssetItem")
        myViewFASO.MainGrid.BindGridData(dsForm, dtFA.DataSet.Tables.IndexOf(dtFA))
        myViewFASO.MainGrid.QuickConf("", True, "2-1-1")
        str1 = "<BAND INDEX = ""0"" TABLE = ""FixedAssetItem"" IDFIELD=""FixedAssetItemID""><COL KEY ="" FixedAssetID, InvoiceItemID, PaymentItemTransID, Qty ""/><COL KEY=""AMOUNT"" NOEDIT=""True""/></BAND>"
        myViewFASO.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostLot.MainGrid.MainConf("FORMATXML") = "<COL KEY=""LotNum"" CAPTION=""Lot No.""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostLot.MainGrid.BindGridData(Me.dsForm, 5)
        myViewCostLot.MainGrid.QuickConf("", True, "2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue""/></BAND>"
        myViewCostLot.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostWBS.MainGrid.MainConf("FORMATXML") = "<COL KEY=""SerialNum"" CAPTION=""Serial No""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""WBSElemType"" CAPTION=""Element Type""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostWBS.MainGrid.BindGridData(Me.dsForm, 6)
        myViewCostWBS.MainGrid.QuickConf("", True, "2-2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue""/></BAND>"
        myViewCostWBS.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostCenter.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostCenterName"" CAPTION=""Cost Center Name""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostCenter.MainGrid.BindGridData(Me.dsForm, 7)
        myViewCostCenter.MainGrid.QuickConf("", True, "2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue""/></BAND>"
        myViewCostCenter.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)


        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))
        objPricingCalcBase.CheckPriceSlabElement()

        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            Me.AddDataSet("CDNInv", "Select * from Invoice where InvoiceID = " & myUtils.cValTN(myRow("CDNInvoiceID")))
        End If


        If CopyIDX > 0 Then
            sql = "Select * from invoice  Where InvoiceID = " & CopyIDX
            Dim ds2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Me.BindDataTable(Me.CopyIDX, ds2)

            For Each r1 As DataRow In ds2.Tables("InvoiceItem").Select
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, Me.dsForm.Tables("InvoiceItem"))

                For Each r3 As DataRow In ds2.Tables("InvoiceItemGST").Select("InvoiceItemID = " & r1("InvoiceItemID") & "")
                    Dim r4 As DataRow = myUtils.CopyOneRow(r3, Me.dsForm.Tables("InvoiceItemGST"),,, "InvoiceItemID", r2("InvoiceItemID"))
                Next

                For Each r3 As DataRow In ds2.Tables("FixedAssetItem").Select("InvoiceItemID = " & r1("InvoiceItemID") & "")
                    Dim r4 As DataRow = myUtils.CopyOneRow(r3, Me.dsForm.Tables("FixedAssetItem"),,, "InvoiceItemID", r2("InvoiceItemID"))
                Next

                For Each r3 As DataRow In ds2.Tables("CostLot").Select("InvoiceItemID = " & r1("InvoiceItemID") & "")
                    Dim r4 As DataRow = myUtils.CopyOneRow(r3, Me.dsForm.Tables("CostLot"),,, "InvoiceItemID", r2("InvoiceItemID"))
                Next

                For Each r3 As DataRow In ds2.Tables("CostWBS").Select("InvoiceItemID = " & r1("InvoiceItemID") & "")
                    Dim r4 As DataRow = myUtils.CopyOneRow(r3, Me.dsForm.Tables("CostWBS"),,, "InvoiceItemID", r2("InvoiceItemID"))
                Next

                For Each r3 As DataRow In ds2.Tables("CostCenter").Select("InvoiceItemID = " & r1("InvoiceItemID") & "")
                    Dim r4 As DataRow = myUtils.CopyOneRow(r3, Me.dsForm.Tables("CostCenter"),,, "InvoiceItemID", r2("InvoiceItemID"))
                Next
            Next
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal InvoiceID As Integer, ds As DataSet)
        Dim dsDB As DataSet, Sql, Sql1, Sql3, Sql4, Sql5, Sql6, Sql7, Sql8 As String

        Sql1 = "Select InvoiceItemID, InvoiceID, PriceSlabID,TransType2,HSN_SC, PPSubType, BasicRate,ItemUnitID, AmountBasic, TaxCredit, ClassType, TransType, ValuationClass, Description, QtyRate, AmountTot, AmountWV from InvoiceItem  Where InvoiceID = " & InvoiceID & " "
        Sql3 = "Select FixedAssetItemID, FixedAssetID, InvoiceItemID, EntryType, AssetName, AssetNumber, Qty, Amount from accListFixedAssetItem() Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql4 = "Select * from InvoiceItemGST Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql5 = "Select * from InvoiceGstCalc where invoiceid = " & InvoiceID

        Sql6 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""

        Sql = " " & Sql1 & ";" & Sql3 & ";" & Sql4 & ";" & Sql5 & ";" & Sql6 & ";" & Sql7 & ";" & Sql8
        dsDB = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(ds, dsDB, "InvoiceItem", 0)
        myUtils.AddTable(ds, dsDB, "FixedAssetItem", 1)
        myUtils.AddTable(ds, dsDB, "InvoiceItemGST", 2)
        myUtils.AddTable(ds, dsDB, "InvoiceGstCalc", 3)
        myUtils.AddTable(ds, dsDB, "CostLot", 4)
        myUtils.AddTable(ds, dsDB, "CostWBS", 5)
        myUtils.AddTable(ds, dsDB, "CostCenter", 6)
        myContext.Tables.SetAuto(ds.Tables("InvoiceItem"), ds.Tables("FixedAssetItem"), "InvoiceItemID", "_FixedAssetItem")
        myContext.Tables.SetAuto(ds.Tables("InvoiceItem"), ds.Tables("InvoiceItemGST"), "InvoiceItemID", "_InvoiceItemGST")
        myContext.Tables.SetAuto(ds.Tables("InvoiceItem"), ds.Tables("CostLot"), "InvoiceItemID", "_CostLot")
        myContext.Tables.SetAuto(ds.Tables("InvoiceItem"), ds.Tables("CostWBS"), "InvoiceItemID", "_CostWBS")
        myContext.Tables.SetAuto(ds.Tables("InvoiceItem"), ds.Tables("CostCenter"), "InvoiceItemID", "_CostCenter")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If Me.SelectedRow("BillOf") Is Nothing Then Me.AddError("BillOf", "Please select BillOf")
        If Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso Me.SelectedRow("GSTInvoiceType") Is Nothing Then Me.AddError("GSTInvoiceType", "Please select GST Invoice Type")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2B", "CDN", "CDNUR") AndAlso Me.SelectedRow("GSTInvoiceSubType") Is Nothing Then Me.AddError("GSTInvoiceSubType", "Please select GST Invoice Sub Type")

        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) < myUtils.cValTN(Me.myRow("TDSAmount")) Then Me.AddError("TDSAmount", "'TDS Base Amount' should be greater than 'TDS Amount'")
        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) > 0 AndAlso myUtils.cValTN(Me.myRow("TDSAmount")) = 0 Then
            Me.AddError("TDSAmount", "Please enter 'TDS Amount' or Remove 'TDS Base Amount'")
        End If

        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("BillOf")), "P") Then
            If myRow("InvoiceNum").ToString.Trim.Length = 0 Then Me.AddError("InvoiceNum", "Enter Invoice No.")
        End If

        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "FD", "FC") Then
            If myUtils.cValTN(myRow("CDNInvoiceID")) = 0 Then Me.AddError("", "Please Select Original Invoice")
        End If

        If Not Me.SelectedRow("CampusID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")


        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")

        If myUtils.cValTN(myRow("PriceSlabID")) = 0 Then Me.AddError("", "Please Select Pricing")
        Dim rr2() As DataRow = dsForm.Tables("InvoiceItem").Select("isnull(PriceSlabID,0) = 0")
        If rr2.Length > 0 Then
            Me.AddError("", "Please Select Pricing for Item")
        End If


        Dim oRet = myFuncs.CheckZeroTaxType(myUtils.cStrTN(myRow("GSTInvoiceSubType")), dsForm.Tables("InvoiceItemGST"))
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not oRet.Success) Then Me.AddError("", oRet.Message)

        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "PM") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "IMPG") Then
            If myUtils.NullNot(myRow("boe_num")) Then Me.AddError("boe_num", "Please Enter BOE No.")
            If myUtils.NullNot(myRow("boe_dt")) Then Me.AddError("boe_dt", "Please Select BOE Date")
            If myUtils.NullNot(myRow("boe_val")) Then Me.AddError("boe_val", "Please Enter BOE Value")
            If myUtils.NullNot(myRow("port_code")) Then Me.AddError("port_code", "Please Enter Port Code")
        End If

        If myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("InvoiceItem"), "InvoiceItemID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct %.")
        Return Me.CanSave
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

            myFuncs.SetPreGST(myContext, myRow.Row)
            If myUtils.IsInList(myRow("P_gst"), "y") Then
                myRow("GSTInvoiceType") = DBNull.Value
                myRow("GSTInvoiceSubType") = DBNull.Value
            End If

            If Not Me.SelectedRow("DeliveryCampusID") Is Nothing Then
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("DeliveryCampusID")("TaxAreaID"))
            Else
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CampusID")("TaxAreaID"))
            End If

            Dim objIVProcFico As New clsIVProcFICO("IP", myContext)
            objIVProcFico.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))
            myRow("DocType") = objIVProcFico.DocType
            If objIVProcFico.GetInvoiceTypeID(myRow.Row) = False Then Me.AddError("InvoiceNum", "Combination Not Available")

            If Me.SelectedRow("CampusID") Is Nothing OrElse objIVProcFico.IsVouchDateAfterFinStart(myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), myRow("PostingDate")) = False Then
                Me.AddError("PostingDate", "Posting Date can not be less then Company Start Date.")
            End If

            objIVProcFico.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("InvoiceItem"))
            Dim oRet As clsProcOutput = objIVProcFico.CheckBalance()
            If Not oRet.Success Then Me.AddError("", oRet.Message)

            Dim oProc As New clsGSTInvoicePurch(myContext)
            oProc.CalcVouchActionRP(Me.SelectedRow("CampusID")("gstregid"), myRow("postperiodid"), myRow.Row)

            myRow("TY") = myFuncs.SetTY(dsForm.Tables("InvoiceItem"))

            Dim rr1() As DataRow = dsForm.Tables("InvoiceItem").Select("ValuationClass = 'OutStanding'")
            If rr1.Length > 0 Then
                myRow("HasOutStanding") = 1
            Else
                myRow("HasOutStanding") = DBNull.Value
            End If

            If Me.CanSave Then
                If myRow("BillOf") = "C" Then
                    Dim DocNumSysType As String = myFuncs.GetDocNumSysType(myContext, myUtils.cValTN(myRow("InvoiceTypeID")))
                    Dim ObjVouch As New clsVoucherNum(myContext)
                    ObjVouch.GetNewSerialNo("InvoiceID", DocNumSysType, myRow.Row)
                End If

                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))

                Dim objPricingCalcBase As New clsPricingCalcBase(myContext)
                objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("InvoiceItem"))
                objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "AmountWV", "")
                For Each r2 As DataRow In dsForm.Tables("InvoiceItem").Select()
                    objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "AmountBasic", "")
                Next
                objPricingCalcBase.PopulateAccountingKeys(myRow("postingdate"))

                Dim InvoicePurchDescrip As String = myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Invoice Where InvoiceID = " & frmIDX)
                    frmIDX = myRow("InvoiceID")

                    myUtils.ChangeAll(dsForm.Tables("InvoiceItem").Select, "InvoiceID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItem"), "Select InvoiceItemID, InvoiceID, PPSubType,TransType2, PriceSlabID, BasicRate, AmountBasic, ClassType, TransType, ValuationClass, Description, QtyRate, AmountTot, AmountWV,HSN_SC,TaxCredit,ItemUnitID from InvoiceItem")

                    myUtils.ChangeAll(dsForm.Tables("FixedAssetItem").Select, "EntryType=I")
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("FixedAssetItem"), "Select FixedAssetItemID, FixedAssetID, InvoiceItemID, Qty, Amount, EntryType from FixedAssetItem", True)

                    If Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("P_gst")), "Y") Then
                        myUtils.ChangeAll(dsForm.Tables("InvoiceItemGST").Select, "InvoiceID=" & frmIDX)
                        myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "InvoiceID=" & frmIDX)

                        Dim rCDN = oProc.GetFirstRow(Me.DatasetCollection, "cdninv")
                        oProc.PopulateCalc(frmIDX, myRow.Row, Me.SelectedRow("CampusID"), dsForm.Tables("InvoiceItemGST"), dsForm.Tables("InvoiceGstCalc"), rCDN, Nothing, Me.dsCombo)

                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGST"), "Select * from InvoiceItemGST", True)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")
                    End If


                    ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)


                    objPricingCalcBase.VSave()

                    oRet = objIVProcFico.HandleWorkflowState(myRow.Row)
                    If oRet.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoicePurchDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, oRet.Message)
                        Me.AddError("", oRet.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Private Sub ChangeColRowwise(dt As DataTable, InvoiceID As Integer)
        myUtils.ChangeAll(dt.Select, "PIDField=InvoiceID")
        myUtils.ChangeAll(dt.Select, "PIDValue=" & InvoiceID)
        myUtils.ChangeAll(dt.Select, "pIDFieldItem=InvoiceItemID")

        For Each r1 In dt.Select
            r1("pIDValueItem") = r1("InvoiceItemID")
        Next
    End Sub

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim str As String = ""
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "order"
                Dim sql As String = "Select Ordernum, Customer from slsListOrder() where SalesOrderId = " & myUtils.cValTN(frmIDX) & ""
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Case "purorder"
                Dim sql As String = "select Distinct PurOrderID, OrderNum, OrderDate from purListItemHist() where InvoiceID = " & myUtils.cValTN(frmIDX)
                Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt.Rows.Count > 0 Then

                    For Each r2 In dt.Select
                        If str.Trim.Length > 0 Then
                            If myUtils.cStrTN(r2("OrderNum")).Trim.Length > 0 Then str = str & ", PO No. " & myUtils.cStrTN(r2("OrderNum")) & " Dt. " & Format(r2("OrderDate"), "dd-MMM-yyyy")
                        Else
                            If myUtils.cStrTN(r2("OrderNum")).Trim.Length > 0 Then str = "PO No. " & myUtils.cStrTN(r2("OrderNum")) & " Dt. " & Format(r2("OrderDate"), "dd-MMM-yyyy")
                        End If
                    Next
                    oRet.Description = myUtils.cStrTN(str)
                End If
        End Select
        Return oRet
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "serials"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdSerialID""/>", True, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
                Case "spares"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid and TransType = @transtype", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOSpareID""/>", False, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
                Case "services"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOServiceID""/>", False, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
                Case "sofixedasset"
                    Dim TransType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@transtype", Params))
                    Dim ValuationClass As String = myUtils.cStrTN(myContext.SQL.ParamValue("@valuationclass", Params))
                    Dim NewFixedAssetIDcsv As String = myUtils.cStrTN(myContext.SQL.ParamValue("@newfixedassetcsv", Params))
                    Dim OldFixedAssetIDcsv As String = myUtils.cStrTN(myContext.SQL.ParamValue("@oldfixedassetcsv", Params))

                    Dim oAssetProc As New clsFixedAssetProc(myContext)
                    Dim strXML As String = oAssetProc.ModRowXML(TransType, ValuationClass, NewFixedAssetIDcsv, OldFixedAssetIDcsv)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""FIXEDASSETID""/>", True, , strXML)
                Case "invoice"
                    Dim InvoiceDate As Date = myContext.SQL.ParamValue("@InvoiceDate", Params)

                    Model = New clsViewModel(vwState, myContext)
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("isNull(SalesOrderID, 0) = (@SalesOrderID) and DocType = 'IP' and InvoiceTypeCode in ('PF', 'PM','OF', 'TP') and VendorID = (@VendorID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "' and InvoiceDate >= '" & Format(DateAdd("m", -18, InvoiceDate), "dd-MMM-yyyy") & "'", Params)
                    Dim Sql As String = "SELECT InvoiceID, InvoiceTypeCode, InvoiceNum, InvoiceDate, AmountTot  From Invoice where " & sql1
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

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim Sql As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
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
