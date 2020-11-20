Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.app.mxform.gst

<DataContract>
Public Class frmInvoiceSaleModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim myViewSerialSO, myViewFASO, myViewAdv, myViewCostLot, myViewCostWBS, myViewCostCenter As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        myViewSerialSO = Me.GetViewModel("SerialSO")
        myViewFASO = Me.GetViewModel("FASO")
        myViewAdv = Me.GetViewModel("Adv")
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

        sql = "Select CampusID, DispName, Camp.CompanyID, TaxAreaCode, DivisionCodeList, WODate, Camp.CompletedOn, TaxAreaID, GstRegID, Camp.SalesOrderID, PidUnitID,CampusCode,ProjectID from mmlistCampus() as Camp Left join SalesOrder on Camp.SalesOrderID = SalesOrder.SalesOrderID  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("deliverycampusid", "Campus")
        Me.AddLookupField("ProjectCampusID", "Campus")

        sql = "SELECT  CustomerID, CustomerCode, PartyName, CustomerClass, GSTIN,TaxAreaCode, TaxAreaID, MainPartyID FROM  slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = myFuncsBase.CodeWordSQL("Document", "SerialType", "")
        Me.AddLookupField("SubSerialType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SubSerialType").TableName)

        sql = "Select PartyID, PartyName, TaxAreaID from Party order by PartyName"
        Me.AddLookupField("ConsigneeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Party").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxType", "")
        Me.AddLookupField("TaxInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxInvoiceType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "InvoiceType", "CodeWord in ('PF','FD','FC','IR')")
        Me.AddLookupField("InvoiceTypeCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "InvoiceTypeCode").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "BillOf", "")
        Me.AddLookupField("BillOf", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BillOf").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)


        sql = "select TaxAreaID, Descrip,TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

        '----------------Item---------------
        sql = "Select Class as ClassCode, Class, ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass Where ClassType = 'M' or (ClassType = 'S' and ClassSubType in ('S','B')) or ClassType = 'A' Order by Class"
        Me.AddLookupField("InvoiceItem", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = "Select CodeWord, DescripShort from CodeWords where CodeClass = 'Invoice' and CodeType = 'ItemType'  Order By DescripShort"
        Me.AddLookupField("InvoiceItem", "InvoiceItemType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "InvoiceItemType").TableName)

        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "CodeWord in ('A','M','S')")
        Me.AddLookupField("InvoiceItem", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Material', 'Asset', 'Service') and CodeType <> 'Type' and CodeWord in ('ASN', 'RM','CG','CT','ST','WIP','FG','SUN')  Order by CodeClass "
        Me.AddLookupField("InvoiceItem", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = "Select Code, Code + '-' + Description as Description, Ty from HsnSac Order by Code"
        Me.AddLookupField("Hsn_Sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("Material", "ReturnFY", "")
        Me.AddLookupField("InvoiceItem", "ReturnFY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ReturnFY").TableName)

        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("InvoiceItem", "ItemUnitID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

        sql = "Select * from systemoptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxCredit", "")
        Me.AddLookupField("TaxCredit", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxCredit").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objIVProcFico As New clsIVProcFICO("IS", myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select *,  0.00 as PostBalance  from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "SalesOrderID,InvoiceTypeCode,HasBOQRef", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objIVProcFico.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Dim Tag, Where As String
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "IR") Then
            Tag = "Tag2 = 'IP'"
            Where = "where CodeClass = 'Invoice' and CodeType in ('B2B','CDN')"
        Else
            Tag = "Tag = 'IS'"
            Where = "where CodeClass = 'Invoice' and CodeType in ('B2B', 'B2C', 'CDN', 'Exp')"
        End If


        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", Tag)
        Me.AddLookupField("GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "GSTTypecode", Tag)
        Me.AddLookupField("GSTInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceType").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass, Tag from CodeWords " & Where & " Order by CodeClass"
        Me.AddLookupField("GSTInvoiceSubType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceSubType").TableName)


        Me.BindDataTable(myUtils.cValTN(prepIDX))
        objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("InvoiceItem"))

        objIVProcFico.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))

        If myUtils.cBoolTN(myRow("HasBOQRef")) = False Then myView.MainGrid.MainConf("HIDECOLS") = "BOQNum, FullBOQNum"
        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-1-2-1-1-1")
        Dim str1 As String = "<BAND INDEX = ""0"" TABLE = ""InvoiceItem"" IDFIELD=""InvoiceItemID""><COL KEY ="" InvoiceItemID, InvoiceID, SOSpareID, SOServiceID, QtySOService, QtySOSpare, VarNum, PIDUnitID, ItemID, SerialNum, ClassType, TransType, ValuationClass, PriceSlabID, POSerialNum, RNPrevRate, QtyPrev, QtyPO, PPSubType, AmountBasic, InvoiceItemType, SortIndex, SubSortIndex, BasicRate, Description, AmountTot, AmountWV,ReturnFY,HSN_SC,ItemUnitID""/><COL  NOEDIT=""TRUE"" KEY=""QtyRate"" CAPTION=""Qty""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
            If frmMode = EnumfrmMode.acAddM Then myRow("InvoiceTypeCode") = "PF"

            Dim dtPS As DataTable = dsForm.Tables("ProdSerialItem")

            myViewSerialSO.MainGrid.BindGridData(dsForm, dtPS.DataSet.Tables.IndexOf(dtPS))
            myViewSerialSO.MainGrid.QuickConf("", True, "1-1-1")
            str1 = "<BAND INDEX = ""0"" TABLE = ""ProdSerialItem"" IDFIELD=""ProdSerialItemID""><COL KEY ="" ProdSerialID, InvoiceItemID, ODNoteItemID ""/></BAND>"
            myViewSerialSO.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

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


        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            Me.AddDataSet("CDNInv", "Select * from Invoice where InvoiceID = " & myUtils.cValTN(myRow("CDNInvoiceID")))
        ElseIf myUtils.cValTN(myRow("ReverseInvoiceID")) > 0 Then
            Me.AddDataSet("CDNInv", "Select *, 0.00 as PostBalance from Invoice where InvoiceID = " & myUtils.cValTN(myRow("ReverseInvoiceID")))
            objIVProcFico.PopulatePreBalanceDue(Me.DatasetCollection("CDNInv").Tables(0).Rows(0))
        End If

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))

        sql = "Select ProjectID from SalesOrder where SalesOrderID  = " & myUtils.cValTN(myRow("SalesOrderID")) & ""
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then
            Me.ModelParams.Add(New clsSQLParam("@ProjectID", myUtils.cValTN(dt1.Rows(0)("ProjectID")), GetType(Integer), False))
        End If

        sql = " Select PaymentItemID, PaymentID, InvoiceID, OpenAmountAdj, NewAmount, PaymentItemType, VouchNum, Dated, PaymentInfo, Amount from accListPaymentItem() Where InvoiceID = " & prepIDX & " and PaymentItemType = 'IP'"
        myViewAdv.MainGrid.QuickConf(sql, True, "1-1-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY ="" PaymentID, InvoiceID, PaymentItemType, Amount""/></BAND>"
        myViewAdv.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostLot.MainGrid.MainConf("FORMATXML") = "<COL KEY=""LotNum"" CAPTION=""Lot No.""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostLot.MainGrid.BindGridData(Me.dsForm, 6)
        myViewCostLot.MainGrid.QuickConf("", True, "2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue""/></BAND>"
        myViewCostLot.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostWBS.MainGrid.MainConf("FORMATXML") = "<COL KEY=""SerialNum"" CAPTION=""Serial No""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""WBSElemType"" CAPTION=""Element Type""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostWBS.MainGrid.BindGridData(Me.dsForm, 7)
        myViewCostWBS.MainGrid.QuickConf("", True, "2-2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue""/></BAND>"
        myViewCostWBS.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostCenter.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostCenterName"" CAPTION=""Cost Center Name""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostCenter.MainGrid.BindGridData(Me.dsForm, 8)
        myViewCostCenter.MainGrid.QuickConf("", True, "2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue""/></BAND>"
        myViewCostCenter.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)


        objPricingCalcBase.CheckPriceSlabElement()
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal InvoiceID As Integer)
        Dim ds As DataSet, Sql1, Sql2, Sql3, Sql4, Sql5, Sql, Sql6, Sql7, Sql8 As String

        Sql1 = " Select InvoiceItemID, InvoiceID, SOSpareID,PSospareID, SoServiceID,PSoServiceID, VarNum, ItemUnitID, PIDUnitID, ItemID, PriceSlabID, TaxCredit,ReturnFY, POSerialNum, RNPrevRate, QtyPrev, QtyPO, PPSubType, AmountBasic, QtySOService, QtySOSpare, BasicRate,InvoiceItemType,HSN_SC, ClassType, TransType, ValuationClass, SortIndex, SubSortIndex, BOQNum, FullBOQNum, SerialNum, Description, QtyRate, AmountTot, AmountWV  from AccListInvoiceItem()  Where InvoiceId = " & InvoiceID & ""
        Sql2 = " Select ProdSerialItemID, ProdSerialItem.ProdSerialID, InvoiceItemID, ProdSerialNum, WOInfo, LotNum from ProdSerialItem Inner join ProdSerial on ProdSerialItem.ProdSerialID = ProdSerial.ProdSerialID Inner join ProdLots on ProdSerial.ProdLotID = ProdLots.ProdLotID Inner join PIDUnit on ProdLots.PIDUnitID = PIDUnit .PIDUnitID  Where InvoiceItemID in (select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ")"
        Sql3 = "Select FixedAssetItemID, FixedAssetID, InvoiceItemID, EntryType, AssetName, AssetNumber, Qty, Amount from accListFixedAssetItem() Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql4 = "Select * from InvoiceItemGST Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql5 = "Select * from InvoiceGstCalc where invoiceid = " & InvoiceID
        Sql6 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""

        Sql = Sql1 & ";" & Sql2 & ";" & Sql3 & ";" & Sql4 & ";" & Sql5 & ";" & Sql6 & ";" & Sql7 & ";" & Sql8
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "InvoiceItem", 0)
        myUtils.AddTable(Me.dsForm, ds, "ProdSerialItem", 1)
        myUtils.AddTable(Me.dsForm, ds, "FixedAssetItem", 2)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGST", 3)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceGstCalc", 4)
        myUtils.AddTable(Me.dsForm, ds, "CostLot", 5)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 6)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 7)

        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("ProdSerialItem"), "InvoiceItemID", "_ProdSerialItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("FixedAssetItem"), "InvoiceItemID", "_FixedAssetItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("InvoiceItemGST"), "InvoiceItemID", "_InvoiceItemGST")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("CostLot"), "InvoiceItemID", "_CostLot")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("CostWBS"), "InvoiceItemID", "_CostWBS")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("CostCenter"), "InvoiceItemID", "_CostCenter")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("CustomerID") Is Nothing Then Me.AddError("CustomerID", "Please select Customer")
        If Me.SelectedRow("BillOf") Is Nothing Then Me.AddError("BillOf", "Please select BillOf")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso Me.SelectedRow("GSTInvoiceType") Is Nothing Then Me.AddError("GSTInvoiceType", "Please select GST Invoice Type")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2C", "B2BUR", "IMPG", "IMPS")) AndAlso Me.SelectedRow("GSTInvoiceSubType") Is Nothing Then Me.AddError("GSTInvoiceSubType", "Please select GST Invoice Sub Type")

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

        If Me.myView.MainGrid.myDV.Table.Select.Length > 0 And myUtils.cValTN(myRow("PriceSlabID")) = 0 Then Me.AddError("", "Please Select Pricing")
        Dim rr2() As DataRow = dsForm.Tables("InvoiceItem").Select("isnull(PriceSlabID,0) = 0 and InvoiceItemType Not in ('PIC', 'PIS', 'IGT','IGS')")
        If rr2.Length > 0 Then
            Me.AddError("", "Please Select Pricing for Item")
        End If


        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "IR") Then
            If (Not Me.SelectedRow("DeliveryCampusID") Is Nothing) Then
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("DeliveryCampusID")("TaxAreaID"))
            Else
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CampusID")("TaxAreaID"))
            End If
        Else
            If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("ConsigneeID")("TaxAreaID"))
            Else
                If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 AndAlso (Not Me.SelectedRow("CustomerID") Is Nothing) Then myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CustomerID")("TaxAreaID"))
            End If
        End If


        If (Not Me.SelectedRow("DeliveryCampusID") Is Nothing) AndAlso (myUtils.cValTN(SelectedRow("DeliveryCampusID")("SalesOrderID"))) > 0 Then
            If myUtils.cValTN(SelectedRow("DeliveryCampusID")("SalesOrderID")) <> myUtils.cValTN(myRow("SalesOrderID")) Then Me.AddError("", "Please Select Correct Sales Order")
        End If

        If Me.SelectedRow("POSTaxAreaID") Is Nothing Then Me.AddError("POSTaxAreaID", "Please select Place of Supply.")

        Dim oRet = myFuncs.CheckZeroTaxType(myUtils.cStrTN(myRow("GSTInvoiceSubType")), dsForm.Tables("InvoiceItemGST"))
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not oRet.Success) Then Me.AddError("", oRet.Message)

        If myUtils.cBoolTN(myRow("HasBOQRef")) Then
            For Each r1 As DataRow In myView.MainGrid.myDV.Table.Select("Isnull(QtyRate,0) = 0")
                Dim rr1() As DataRow = myView.MainGrid.myDV.Table.Select("PSoSpareID = " & myUtils.cValTN(r1("SoSpareID")) & "")
                If Not IsNothing(rr1) AndAlso rr1.Length = 0 Then
                    Me.AddError(myView.Key, r1, 0, "", "", "Atleast one child is required.")
                End If
            Next
        End If


        If myUtils.cValTN(myRow("AmountRO")) < 0 Then Me.AddError("AmountRO", "Difference Amount can not be less then Zero.")
        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "IR") AndAlso myUtils.cValTN(myRow("PostBalance")) < 0 Then Me.AddError("PostBalance", "Post Balance can not be less then Zero.")
        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "IR") AndAlso myViewAdv.MainGrid.myDS.Tables(0).Rows.Count > 0 AndAlso (myUtils.cValTN(myViewAdv.MainGrid.GetColSum("Amount", "")) + myUtils.cValTN(myRow("AmountRO"))) <> myUtils.cValTN(myView.MainGrid.GetColSum("AmountTot", "")) Then Me.AddError("", "Amount of Advance should be equal to Amount of Invoice.")

        If (Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "IR")) AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("InvoiceItem"), "InvoiceItemID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please Enter Correct % Value in Cost Assignment.")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        Dim Oret As clsProcOutput
        VSave = False

        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("InvoiceNum", "Accounting entry passed. Invoice can't be update.")
            End If

            If (Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "IR")) AndAlso myFuncs.CheckPayment(myContext, myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("", "Payment done. Not allow to edit")
            End If

            myFuncs.SetPreGST(myContext, myRow.Row)
            If myUtils.IsInList(myUtils.cStrTN(myRow("P_gst")), "y") Then
                myRow("GSTInvoiceType") = DBNull.Value
                myRow("GSTInvoiceSubType") = DBNull.Value
            Else
                If myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "RD", "RC", "FD", "FC") Then
                    If Not myFuncs.CheckOrigInvDate(myContext, myRow.Row) Then
                        Me.AddError("", "Orignal Invoice can not be 18 months old.")
                    End If
                End If
            End If

            Dim objIVProcFico As New clsIVProcFICO("IS", myContext)
            objIVProcFico.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))

            If Me.SelectedRow("CampusID") Is Nothing OrElse objIVProcFico.IsVouchDateAfterFinStart(myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), myRow("PostingDate")) = False Then
                Me.AddError("PostingDate", "Posting Date can not be less then Company Start Date.")
            End If

            myRow("DocType") = objIVProcFico.DocType
            myRow("TY") = myFuncs.SetTY(dsForm.Tables("InvoiceItem"))
            If objIVProcFico.GetInvoiceTypeID(myRow.Row) = False Then Me.AddError("", "Combination Not Available")

            objIVProcFico.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("InvoiceItem"))
            Oret = objIVProcFico.CheckBalance()
            If Not Oret.Success Then Me.AddError("", Oret.Message)


            Oret = objIVProcFico.CheckSOBalance(myRow.Row, dsForm.Tables("InvoiceItem"), dsForm.Tables("ProdSerialItem"))
            If Not Oret.Success Then Me.AddError("", Oret.Message)

            Oret = objIVProcFico.CheckChildren(Me.dsForm.Tables("InvoiceItem"))
            If Oret.Success = False Then Me.AddError("", Oret.Message)

            Dim oProc As New clsGSTInvoiceSale(myContext)
            oProc.CalcVouchActionRP(Me.SelectedRow("CampusID")("gstregid"), myRow("postperiodid"), myRow.Row)

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

                If myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2C") Then
                    If myUtils.cValTN(myRow("AmountTot")) > 250000 AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("sply_ty")), "INTER") Then
                        myRow("GSTInvoiceSubType") = "L"
                    Else
                        myRow("GSTInvoiceSubType") = "S"
                    End If
                End If

                Dim InvoiceSaleDescrip As String = myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Invoice Where InvoiceID = " & frmIDX)
                    frmIDX = myRow("InvoiceID")

                    myUtils.ChangeAll(dsForm.Tables("InvoiceItem").Select, "InvoiceID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItem"), "Select InvoiceItemID, InvoiceID, SOSpareID, VarNum, PIDUnitID, ItemID, SerialNum, ClassType, TransType, ValuationClass, PriceSlabID, POSerialNum, RNPrevRate, QtyPrev, QtyPO, QtySOService, QtySOSpare, PPSubType, AmountBasic, InvoiceItemType, SortIndex, SubSortIndex, Description, QtyRate,  BasicRate, AmountTot, AmountWV,ReturnFY,HSN_SC,ItemUnitID,TaxCredit from InvoiceItem")

                    myUtils.ChangeAll(dsForm.Tables("FixedAssetItem").Select, "EntryType=I")
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("FixedAssetItem"), "Select FixedAssetItemID, FixedAssetID, InvoiceItemID, EntryType, Qty, Amount from FixedAssetItem", True)

                    If Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("P_gst")), "Y") Then
                        Dim InvoiceItemID As String = myUtils.MakeCSV(dsForm.Tables("InvoiceItem").Select("InvoiceItemType Not in ('PIC','PIS','IGT')"), "InvoiceItemID")
                        myUtils.ChangeAll(dsForm.Tables("InvoiceItemGST").Select, "InvoiceID=" & frmIDX)
                        Dim rCDN = oProc.GetFirstRow(Me.DatasetCollection, "cdninv")
                        oProc.PopulateCalc(frmIDX, myRow.Row, Me.SelectedRow("CampusID"), dsForm.Tables("InvoiceItemGST"), dsForm.Tables("InvoiceGstCalc"), rCDN, Nothing, Me.dsCombo)
                        myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "InvoiceID=" & frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGST"), "Select * from InvoiceItemGST", True, "InvoiceItemID in (" & InvoiceItemID & ")")
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")
                    End If

                    myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentItemType=IP")
                    myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "InvoiceID= " & frmIDX)
                    Me.myContext.Provider.objSQLHelper.SaveResults(myViewAdv.MainGrid.myDS.Tables(0), "Select PaymentID, InvoiceID, PaymentItemType, Amount from PaymentItem")

                    ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)

                    objPricingCalcBase.VSave()

                    Oret = objIVProcFico.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoiceSaleDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoiceSaleDescrip, Oret.Message)
                        Me.AddError("", Oret.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceSaleDescrip, e.Message)
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
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "salesorderdescrip"
                Dim sql As String = "Select OrderNum, OrderDate  from SalesOrder  where SalesOrderID = " & myUtils.cValTN(frmIDX)
                Dim dt2 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt2.Rows.Count > 0 Then oRet.Description = "Sales Order :- " & myUtils.cStrTN(dt2.Rows(0)("OrderNum")) & " Date - " & Format(dt2.Rows(0)("OrderDate"), "dd-MMM-yyyy")
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
                    Dim SalesOrderID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@SalesOrderID", Params))
                    Dim sql1 As String = ""
                    If SalesOrderID > 0 Then
                        sql1 = myContext.SQL.PopulateSQLParams("isNull(SalesOrderID, 0) = (@SalesOrderID) and DocType = 'IS' and InvoiceTypeCode in ('PF', 'PM') and CustomerID = (@CustomerID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "'", Params)
                    Else
                        sql1 = myContext.SQL.PopulateSQLParams("DocType = 'IS' and InvoiceTypeCode in ('PF', 'PM','OF') and CustomerID = (@CustomerID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "'", Params)
                    End If

                    Dim Sql As String = "SELECT InvoiceID, SalesOrderID, InvoiceNum, InvoiceDate, AmountTot  From Invoice where " & sql1
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "1-1-1")
                Case "addspares"
                    Dim Str1, Str2, str3 As String
                    If myContext.SQL.Exists(Params, "@PidUnitID") Then
                        Str1 = "IsNull(PidUnitID, 0) = " & myUtils.cValTN(myContext.SQL.ParamValue("@PidUnitID", Params)) & ""
                    Else
                        Str1 = ""
                    End If

                    Str2 = myContext.SQL.PopulateSQLParams("SOSpareID Not In (@sospareid) And isnull(BillingLot,0) = 0", Params)
                    str3 = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Dim sql As String = myUtils.CombineWhere(False, Str1, Str2, str3)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOSpareID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "addservices"
                    Dim Str1, Str2, str3 As String
                    If myContext.SQL.Exists(Params, "@PidUnitID") Then
                        Str1 = "IsNull(PidUnitID, 0) = " & myUtils.cValTN(myContext.SQL.ParamValue("@PidUnitID", Params)) & ""
                    Else
                        Str1 = ""
                    End If

                    Str2 = myContext.SQL.PopulateSQLParams("SOServiceID Not In (@soserviceid)", Params)
                    str3 = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Dim sql As String = myUtils.CombineWhere(False, Str1, Str2, str3)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOServiceID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "salesorder"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("MainPartyID = @MainPartyID and CompanyID = @CompanyId and SalesOrderID Not in (@SalesOrderID) and (isnull(OrderDate,'3099-Jan-01') <= '@InvoiceDate' or isnull(LOIDate,'3099-Jan-01') <= '@InvoiceDate')", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SalesOrderID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "orginvir"
                    Dim InvoiceDate As Date = myContext.SQL.ParamValue("@InvoiceDate", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Dim SalesOrderID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@SalesOrderID", Params))
                    Dim sql1 As String = ""
                    If SalesOrderID > 0 Then
                        sql1 = myContext.SQL.PopulateSQLParams("isNull(SalesOrderID, 0) = (@SalesOrderID) and DocType = 'IS' and InvoiceTypeCode in ('PF', 'PM') and CustomerID = (@CustomerID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "'", Params)
                    Else
                        sql1 = myContext.SQL.PopulateSQLParams("DocType = 'IS' and InvoiceTypeCode in ('PF', 'PM','OF') and CustomerID = (@CustomerID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "'", Params)
                    End If

                    Dim Sql As String = "SELECT InvoiceID, SalesOrderID,openamountretained,openamountwo,openamountpaid,wctamount,TDSAmount, 0.00 as PostBalance, InvoiceNum, InvoiceDate, AmountTot  From Invoice where " & sql1
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "1-1-1")
                Case "adv"
                    Model = New clsViewModel(vwState, myContext)
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("CustomerID = @customerid and CompanyID = @companyid and Dated <= '@dated' and PaymentID Not in (@paymentidcsv) and IsProcessed = 1 and PaymentType = 'I' and ((Select Sum(isnull(AmountPen,0)) + Sum(isnull(AmountWO,0)) +  Sum(isnull(AmountCess,0)) from Paymentitem where PaymentItemType in ('DR', 'RW', 'SW','CW', 'OW') and paymentitem.paymentid = acclistpayment.paymentid) > isnull((Select Sum(Amount) from paymentitem where PaymentItemType = 'IP' and InvoiceID <> @InvoiceID and paymentitem.paymentid = acclistpayment.paymentid),0))", Params)

                    Dim Sql As String = "SELECT PaymentID, OpenAmountAdj, PaymentInfo, PaymentType, PartyName, VouchNum, Dated, AmountTotPay, NewAmount, BalanceAmount, Remark From AccListPayment() where " & sql1
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1")

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

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim objIVProcFico As New clsIVProcFICO("IS", myContext)
        Select Case DataKey.Trim.ToLower
            Case "generateprebal"
                objIVProcFico.PopulatePreBalanceDue(dt.Rows(0))
        End Select
    End Sub
End Class