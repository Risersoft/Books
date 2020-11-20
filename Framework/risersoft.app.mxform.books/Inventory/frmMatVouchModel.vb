Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmMatVouchModel
    Inherits clsFormDataModel
    Dim myViewItem, myViewOrderData, myViewRefDoc, myViewOB, myViewResRecItem, myViewResIssueItem, myViewResRecBom, myViewResIssueBom, myViewCostLot, myViewCostWBS, myViewCostCenter, myViewSerialSO As clsViewModel
    Dim PPFinal As Boolean = False

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        myViewItem = Me.GetViewModel("BOM")
        myViewOrderData = Me.GetViewModel("Order")
        myViewRefDoc = Me.GetViewModel("RefDoc")
        myViewOB = Me.GetViewModel("Opening")
        myViewResRecItem = Me.GetViewModel("ResRecItem")
        myViewResIssueItem = Me.GetViewModel("ResIssueItem")
        myViewResRecBom = Me.GetViewModel("ResRecBom")
        myViewResIssueBom = Me.GetViewModel("ResIssueBom")
        myViewCostLot = Me.GetViewModel("CostLot")
        myViewCostWBS = Me.GetViewModel("CostWBS")
        myViewCostCenter = Me.GetViewModel("CostCenter")
        myViewSerialSO = Me.GetViewModel("SerialSO")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String
        sql = myFuncsBase.CodeWordSQL("Material", "MvtType", "")
        Me.AddLookupField("VoucherType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "VoucherType").TableName)

        sql = myFuncsBase.CodeWordSQL("Stock", "taxtype", "")
        Me.AddLookupField("TaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxType").TableName)

        sql = "Select MatDepID, DepName, CampusID, TaxAreaCode,DivisionCodeList, WODate,CompletedOn,CompanyID from mmListDepsMat() where IsStore = 1 Order by DepName"
        Me.AddLookupField("MatDepId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DepsMat").TableName)

        sql = "Select MatDepID, DepName, CampusID, IsStore, IsShop, WODate,CompletedOn from mmListDepsMat() where (IsStore = 1) or (IsShop = 1) Order by DepName"
        Me.AddLookupField("VouchItem", "MatDepID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DepsMatItem").TableName)

        sql = "Select MatDepID, DepName, CampusID, IsStore, IsShop, WODate,CompletedOn from mmListDepsMat() where IsShop = 1 Order by DepName"
        Me.AddLookupField("VouchItem", "ForMatDepID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ForDepsMat").TableName)

        sql = "SELECT  VendorID, VendorName, TaxAreaCode,VendorType, MainPartyID, ImportAllow,GSTIN FROM  purListVendor() where (VendorType in ('MS','EM') or (PartyType = 'A')) Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)
        Me.AddLookupField("VouchItem", "VendorID", "Vendor ")

        sql = "SELECT  CustomerID, CustDescrip, TaxAreaCode, MainPartyID FROM slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)
        Me.AddLookupField("VouchItem", "CustomerID", "Customer")

        sql = "Select MatVouchTypeID, RefDocTypeName, DefMvtCode, VouchTypeName, RefDocTypeCode, VouchTypeCode from MatVouchType Order by VouchTypeName, SortIndex"
        Me.AddLookupField("MatVouchTypeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "MatVouchType").TableName)

        sql = "SELECT  VendorID, VendorName FROM  PurListVendor() where VendorType = 'TR' Order By VendorName"
        Me.AddLookupField("TransporterID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Transporter").TableName)

        Dim vlist As New clsValueList
        vlist.Add(True, "Received")
        vlist.Add(False, "Pending")
        Me.ValueLists.Add("ChallanPending", vlist)
        Me.AddLookupField("ChallanPending", "ChallanPending")

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        '------------------------MatVouch Item----------
        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("VouchItem", "ItemUnitIDEntry", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)
        Me.AddLookupField("VouchItem", "ItemUnitID", "Units")
        Me.AddLookupField("VouchItem", "ItemUnitID2", "Units")
        Me.AddLookupField("VouchItem", "OrderRateUnitID", "Units")

        sql = "Select MatMvtReasonID, Reason, ReasonCode, MatMvtCode from MatMvtReason  Order By Reason"
        Me.AddLookupField("VouchItem", "MatMvtReasonID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Reason").TableName)

        sql = "Select CampusID, DispName, DivisionCodeList, WODate,CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("VouchItem", "CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("InvoiceCampusID", "Campus")

        sql = "Select MatMvtCode.MatMvtCode, Description, MvtType, DocRefType, ReasonField, EntRefType, PricingType, DOCType,OrderUpdateCode,AllowOther,TaxTypeSrc from MatMvtCode Left Join MatMvtDocType on MatMvtCode.MatMvtCode = MatMvtDocType.MatMvtCode"
        Me.AddLookupField("VouchItem", "MvtCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "MatMvtCode").TableName)

        sql = "Select FixedAssetID, AssetName from FixedAsset Order By AssetName"
        Me.AddLookupField("VouchItem", "FixedAssetID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "FixedAsset").TableName)


        sql = "Select Class,Class,ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass where ClassType in ('A','M') or (ClassType = 'S' and ClassSubType in ('P','B'))"
        Me.AddLookupField("VouchItem", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Asset','Service') and CodeWord in ('APN', 'APU', 'ARO', 'ARW','EXP')  Order by CodeClass"
        Me.AddLookupField("VouchItem", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransType").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Material') and CodeWord in ('RM', 'CG', 'CT', 'ST','WIP','FG')  Order by CodeClass"
        Me.AddLookupField("VouchItem", "Stockstage", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Stockstage").TableName)

        sql = myFuncsBase.CodeWordSQL("Material", "ReturnFY", "")
        Me.AddLookupField("VouchItem", "ReturnFY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ReturnFY").TableName)

        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "CodeWord in ('A','M','S')")
        Me.AddLookupField("VouchItem", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = myFuncsBase.CodeWordSQL("Stock", "qtytype1", "", "Tag")
        Me.AddLookupField("VouchItem", "QtyTypeDes", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "QtyTypeDes").TableName)

        sql = myFuncsBase.CodeWordSQL("Stock", "qtytype1", "", "Tag")
        Me.AddLookupField("VouchItem", "QtyTypeSrc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "QtyTypeSrc").TableName)

        sql = myFuncsBase.CodeWordSQL("Material", "", "(CodeType = 'tpo' or CodeType = 'tmo')")
        Me.AddLookupField("VouchItem", "StockStage2", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "StockStage2").TableName)

        sql = myFuncsBase.CodeWordSQL("Stock", "taxtype", "")
        Me.AddLookupField("VouchItem", "TaxType2", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxType2").TableName)

        sql = myFuncsBase.CodeWordSQL("Material", "SpStock", "")
        Me.AddLookupField("VouchItem", "SpStock", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SpStock").TableName)

        sql = "Select Code, Code + '-' + Description as Description, Ty from HsnSac Order by Code"
        Me.AddLookupField("Hsn_Sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        '--------------Reservation-------------------------------

        sql = myFuncsBase.CodeWordSQL("MRP", "GRBehave", "")
        Me.AddLookupField("Res", "ReserveGRBehave", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Reserve").TableName)
        Me.AddLookupField("Res", "ReserveGIBehave", "Reserve")


        '----------------frmItemSelect-------------------------------------------
        sql = "Select ItemId, ItemCode, ItemName, UnitName, ItemUnitId, IsEto, SubCatId, ItemUnitId2, OrderQtyUnitId, OrderRateUnitId, IssueUnitId, OrderQtyNumReq, OrderQtyNumText,ValuationClass, HSN_SC from InvListItems()  Order by ItemName"
        Me.AddLookupField("VouchItem", "ItemId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Items").TableName)

        sql = "Select VarNum as VarNumId, VarNum, VarName, ItemId, PidUnitId from PIDUnitItemVar where PidUnitItemVarId = 0 Order by VarName"
        Me.AddLookupField("VouchItem", "VarNum", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "VarNum").TableName)

        sql = "Select VarNum as VarNumId, VarNum, VarName, PidUnitId from PIDUnitItemVar where PidUnitItemVarId = 0 Order by VarName"
        Me.AddLookupField("VouchItem", "NewVarNum", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "NewVarNum").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxCredit", "")
        Me.AddLookupField("TaxCredit", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxCredit").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql, Str1 As String
        Dim ObjMatVouchProc As New clsMatVouchProc(myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Sql = "Select * from MatVouch Where MatVouchID = " & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("VouchDate") = Now.Date
            myRow("MatVouchID") = 1000000
        Else
            Dim rPostPeriod As DataRow = ObjMatVouchProc.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        BindDataTable(myUtils.cValTN(prepIDX), Me.dsForm)
        objPricingCalcBase.InitData(myRow.Row, "MatVouchId", myUtils.cValTN(frmIDX), "VouchDate", "MatVouchItemId", "QtyRate", Me.dsForm.Tables("VouchItem"))

        ObjMatVouchProc.LoadMultiVouch(myUtils.cValTN(myRow("MatVouchId")))
        ObjMatVouchProc.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("VouchItem"))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-2-1-1-1-1-1-1-1")
        myView.MainGrid.PrepEdit("<BAND IDFIELD=""MatVouchItemId"" TABLE=""MatVouchItem"" INDEX=""0""><COL KEY=""MatVouchItemID, MatVouchID, pMatVouchItemID, ItemID,HasTransitTransfer, PriceSlabID, AmountTrWv, PIDUnitID, VarNum, MvtCode, SpStock, TransType, StockStage, StockStage2, TaxType, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, Remark, NewPIDUnitID, NewVarNum, AmountWV, FixedAssetID, SrcDesType, MfgCharges, ReserveGRBehave, ReserveGIBehave, ClassType, ValuationClass, OdNoteItemID, PPSubType,ReturnFY,TaxCredit,Hsn_Sc""/><COL KEY=""ItemCode"" CAPTION=""Item Code""/><COL KEY=""ItemName"" CAPTION=""Item Name""/><COL KEY=""QtyEntry"" CAPTION=""Qty""/><COL KEY=""UnitName"" CAPTION=""Unit Name""/><COL KEY=""AmountBasic"" CAPTION=""Basic Amount""/><COL KEY=""AmountTot"" CAPTION=""Total Amount""/><COL KEY=""BasicRate"" CAPTION=""Basic Rate""/></BAND>", EnumEditType.acCommandOnly)

        myViewItem.MainGrid.MainConf("HIDECOLS") = "BasicRate, AmountTot, AmountBasic,AmountWV"
        myViewItem.MainGrid.BindGridData(Me.dsForm, 1)
        myViewItem.MainGrid.QuickConf("", True, "1-2-1-1-1-1-1-1-1")
        Str1 = "<BAND IDFIELD=""MatVouchItemId"" TABLE=""MatVouchItem"" INDEX=""0""><COL KEY=""MatVouchItemID, MatVouchID, pMatVouchItemID, ItemID,HasTransitTransfer, PriceSlabID, AmountTrWv, PIDUnitID, VarNum, MvtCode, SpStock, TransType, StockStage,StockStage2, TaxType, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, Remark, NewPIDUnitID, NewVarNum, BasicRate, AmountTot, AmountBasic, AmountWV, FixedAssetID, SrcDesType, MfgCharges, ReserveGRBehave, ReserveGIBehave, ClassType, ValuationClass, OdNoteItemID, PPSubType,ReturnFY""/><COL KEY=""ItemCode"" CAPTION=""Item Code""/><COL KEY=""ItemName"" CAPTION=""Item Name""/><COL KEY=""QtyEntry"" CAPTION=""Qty""/><COL KEY=""UnitName"" CAPTION=""Unit Name""/></BAND>"
        myViewItem.MainGrid.PrepEdit(Str1, EnumEditType.acCommandOnly)

        myViewOrderData.MainGrid.BindGridData(Me.dsForm, 2)
        myViewOrderData.MainGrid.MainConf("FORMATXML") = "<COL KEY=""OrderNum"" CAPTION=""Order Num""/><COL KEY=""OrderDate"" CAPTION=""Order Date""/><COL KEY=""BasicRate"" CAPTION=""Basic Rate""/><COL KEY=""RateUnitName"" CAPTION=""Unit Name""/>"
        myViewOrderData.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1-1")

        myViewResRecItem.MainGrid.BindGridData(Me.dsForm, 3)
        myViewResRecItem.MainGrid.QuickConf("", True, "1-2-2-1")
        Str1 = "<BAND IDFIELD=""PlnReserveItemID"" TABLE=""PlnReserveItem"" INDEX=""0""><COL KEY=""PlnReserveItemID, PlnReserveID, IsTransfer,ResItemType,MatVouchItemID,PostPeriodID, Dated,Qty""/><COL SKIP=""True"" KEY=""CampusID"" LOOKUPSQL=""Select CampusID, DispName from Campus"" CAPTION=""Campus Name""/></BAND>"
        myViewResRecItem.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        myViewResIssueItem.MainGrid.BindGridData(Me.dsForm, 3)
        myViewResIssueItem.MainGrid.QuickConf("", True, "1-2-2-1")
        Str1 = "<BAND IDFIELD=""PlnReserveItemID"" TABLE=""PlnReserveItem"" INDEX=""0""><COL KEY=""PlnReserveItemID, PlnReserveID, IsTransfer,ResItemType,MatVouchItemID,PostPeriodID, Dated,Qty""/><COL SKIP=""True"" KEY=""CampusID"" LOOKUPSQL=""Select CampusID, DispName from Campus"" CAPTION=""Campus Name""/></BAND>"
        myViewResIssueItem.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        myViewResRecBom.MainGrid.BindGridData(Me.dsForm, 3)
        myViewResRecBom.MainGrid.QuickConf("", True, "1-2-2-1")
        Str1 = "<BAND IDFIELD=""PlnReserveItemID"" TABLE=""PlnReserveItem"" INDEX=""0""><COL KEY=""PlnReserveItemID, PlnReserveID,IsTransfer,ResItemType,MatVouchItemID,PostPeriodID, Dated,Qty""/><COL SKIP=""True"" KEY=""CampusID"" LOOKUPSQL=""Select CampusID, DispName from Campus"" CAPTION=""Campus Name""/></BAND>"
        myViewResRecBom.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        myViewResIssueBom.MainGrid.BindGridData(Me.dsForm, 3)
        myViewResIssueBom.MainGrid.QuickConf("", True, "1-2-2-1")
        Str1 = "<BAND IDFIELD=""PlnReserveItemID"" TABLE=""PlnReserveItem"" INDEX=""0""><COL KEY=""PlnReserveItemID, PlnReserveID,IsTransfer,ResItemType,MatVouchItemID,PostPeriodID, Dated,Qty""/><COL SKIP=""True"" KEY=""CampusID"" LOOKUPSQL=""Select CampusID, DispName from Campus"" CAPTION=""Campus Name""/></BAND>"
        myViewResIssueBom.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        myViewOB.MainGrid.BindGridData(Me.dsForm, 4)
        myViewOB.MainGrid.QuickConf("", True, "1-1-1-1-1-1")
        Str1 = "<BAND IDFIELD=""ItemStockValueBalID"" TABLE=""ItemStockValueBal"" INDEX=""0""><COL KEY=""ItemStockValueBalID, ItemStockValueID, MatVouchItemID, OBDescrip, Dated, Qty, Qty2, AmountTot, AmountWV""/></BAND>"
        myViewOB.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        myViewRefDoc.MainGrid.BindGridData(Me.dsForm, 5)
        myViewRefDoc.MainGrid.MainConf("FORMATXML") = "<COL KEY=""DocType"" CAPTION=""Document Type""/><COL KEY=""VouchNum"" CAPTION=""Voucher Num""/><COL KEY=""VouchDate"" CAPTION=""Voucher Date""/>"
        myViewRefDoc.MainGrid.QuickConf("", True, "1-1-1")
        myViewRefDoc.MainGrid.PrepEdit(Str1, EnumEditType.acCommandOnly)

        myViewCostLot.MainGrid.MainConf("FORMATXML") = "<COL KEY=""LotNum"" CAPTION=""Lot No.""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostLot.MainGrid.BindGridData(Me.dsForm, 6)
        myViewCostLot.MainGrid.QuickConf("", True, "2-2-1", True)
        Str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue""/></BAND>"
        myViewCostLot.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        myViewCostWBS.MainGrid.MainConf("FORMATXML") = "<COL KEY=""SerialNum"" CAPTION=""Serial No""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""WBSElemType"" CAPTION=""Element Type""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostWBS.MainGrid.BindGridData(Me.dsForm, 7)
        myViewCostWBS.MainGrid.QuickConf("", True, "2-2-2-1", True)
        Str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue""/></BAND>"
        myViewCostWBS.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        myViewCostCenter.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostCenterName"" CAPTION=""Cost Center Name""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostCenter.MainGrid.BindGridData(Me.dsForm, 8)
        myViewCostCenter.MainGrid.QuickConf("", True, "2-1", True)
        Str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue""/></BAND>"
        myViewCostCenter.MainGrid.PrepEdit(Str1, EnumEditType.acCommandAndEdit)

        Dim dtPS As DataTable = dsForm.Tables("ProdSerialItem")
        myViewSerialSO.MainGrid.BindGridData(dsForm, dtPS.DataSet.Tables.IndexOf(dtPS))
        myViewSerialSO.MainGrid.QuickConf("", True, "1-1-1")
        Str1 = "<BAND INDEX = ""0"" TABLE = ""ProdSerialItem"" IDFIELD=""ProdSerialItemID""><COL KEY ="" ProdSerialID, MatVouchItemID, ODNoteItemID ""/></BAND>"
        myView.MainGrid.PrepEdit(Str1, EnumEditType.acCommandOnly)


        objPricingCalcBase.CheckPriceSlabElement()
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function
    Private Function GenerateMVISQL(IDField As String, IDValue As Integer) As String
        Dim sql As String = "Select MatVouchItemID, 0 as OldMatVouchItemID, MatVouchID, {0}, ItemID, PriceSlabID,AdjustAmountTot, AdjustAmountWV, AmountTrWv, PIDUnitID, SOSpareID, QtySoSpare, VarNum, MvtCode, SpStock, Classtype, TransType, TaxCredit, StockStage, StockStage2, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, NewPIDUnitID, NewVarNum, {4}, {5}, FixedAssetID, pMatVouchItemID, MfgCharges, ReserveGRBehave, ReserveGIBehave, ValuationClass, PPSubType, OdNoteItemID, ReturnFY,Hsn_Sc, {1}, {2}, QtyEntry, {3}, BasicRate, AmountBasic, AmountTot, AmountWV,  Remark FROM matvouchitem where " & IDField & " = " & IDValue
        Dim str1 As String = "(select ODNoteID from odnoteitem where odnoteitem.odnoteitemid = matvouchitem.odnoteitemid) as ODNoteID"
        Dim str2 As String = "(select ItemCode from items where items.itemid = matvouchitem.itemid) as ItemCode"
        Dim str3 As String = "(select ItemName from items where items.itemid = matvouchitem.itemid) as ItemName"
        Dim str4 As String = "(select UnitName from invlistitems() where invlistitems.itemid = matvouchitem.itemid) as UnitName"
        Dim str5 As String = "(select WOInfo from PidUnit where Pidunit.pidunitid = matvouchitem.pidunitid) as WOInfo"
        Dim str6 As String = "(select WOInfo from PidUnit where Pidunit.pidunitid = matvouchitem.newpidunitid) as NewWOInfo"
        sql = String.Format(sql, str1, str2, str3, str4, str5, str6)
        Return sql
    End Function
    Public Function BindDataTable(ByVal MatVouchID As Integer, ByVal dsForm As DataSet) As DataSet
        Dim ds As DataSet, Sql, strMVI, strPIH, strISDQ, strISDV, strRefDoc, Sql7, Sql8, Sql9, Sql10 As String

        strMVI = Me.GenerateMVISQL("matvouchid", MatVouchID)
        strPIH = "Select PurItemHistID, PIDUnitID, PurItemID, RecvMatVouchID, MatVouchItemID, RecdTCInQly, Dated, OrderNum, OrderDate, BasicRate,RateUnitName, QtyRecd, RecdTCInSt, QtyRej, QtyOK, QtyDevi, QtyIssue from PurListItemHist() where MatVouchID = " & MatVouchID
        strISDQ = "Select PlnReserveItemID, PlnReserveID, MatVouchItemID,PostPeriodID, ResItemType, ProdLotID, ResItemSort, ItemID, PIDUnitID, IsTransfer, VarNum, ItemName,  Dated, LotNum, LotWOInfo as Woinfo, CampusID, Abs(Qty) as Qty from plnListReserveItem() where MatVouchID = " & MatVouchID
        Dim strWhere As String = myUtils.CombineWhere(False, "itemstockvalueid is null", "matvouchitemid in (select matvouchitemid from matvouchitem where matvouchid = " & MatVouchID & ")")
        strISDV = "Select ItemStockValueBalID, ItemStockValueID, MatVouchItemID, OBDescrip, Dated, Qty, Qty2, AmountTot, AmountWV from ItemStockValueBal Where" & strWhere
        strRefDoc = "Select IDField, IDValue, IDnoteID, ODNoteID, MatVouchID, IndentID, PInvoiceID, pMatVouchID, DocType, VouchNum, VouchDate from invListMatVouchRefDoc() where PMatVouchID = " & MatVouchID
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as MatVouchItemId, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'MatVouchID' and PIDValue = " & MatVouchID & ""
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as MatVouchItemId, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'MatVouchID' and PIDValue = " & MatVouchID & ""
        Sql9 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as MatVouchItemId, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'MatVouchID' and PIDValue = " & MatVouchID & ""
        Sql10 = "Select ProdSerialItemID, ProdSerialItem.ProdSerialID, MatVouchItemID,  ProdSerialNum, WOInfo, LotNum from ProdSerialItem Inner join plnListProdSerial() on ProdSerialItem.ProdSerialID = plnListProdSerial.ProdSerialID  Where MatVouchItemID in (select MatVouchItemID from MatVouchItem where MatVouchID = " & MatVouchID & ")"
        Sql = " " & strMVI & "; " & strPIH & "; " & strISDQ & "; " & strISDV & "; " & strRefDoc & ";" & Sql7 & ";" & Sql8 & ";" & Sql9 & ";" & Sql10
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(dsForm, ds, "VouchItem", 0)
        myUtils.AddTable(dsForm, ds, "Pur", 1)
        myUtils.AddTable(dsForm, ds, "Res", 2)
        myUtils.AddTable(dsForm, ds, "OB", 3)
        myUtils.AddTable(dsForm, ds, "RefDocAdd", 4)
        myUtils.AddTable(Me.dsForm, ds, "CostLot", 5)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 6)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 7)
        myUtils.AddTable(Me.dsForm, ds, "ProdSerialItem", 8)

        myContext.Tables.SetAuto(dsForm.Tables("VouchItem"), dsForm.Tables("Pur"), "MatVouchItemId", "_PurItemHist")
        myContext.Tables.SetAuto(dsForm.Tables("VouchItem"), dsForm.Tables("Res"), "MatVouchItemId", "_plnReserveItem")
        myContext.Tables.SetAuto(dsForm.Tables("VouchItem"), dsForm.Tables("OB"), "MatVouchItemId", "_ItemStockValueBal")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), dsForm.Tables("CostLot"), "MatVouchItemId", "_CostLot")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), dsForm.Tables("CostWBS"), "MatVouchItemId", "_CostWBS")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), dsForm.Tables("CostCenter"), "MatVouchItemId", "_CostCenter")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), Me.dsForm.Tables("ProdSerialItem"), "MatVouchItemID", "_ProdSerialItem")
        Return dsForm
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("MatDepId") Is Nothing Then Me.AddError("MatDepId", "Please select Storage Department")
        If Me.SelectedRow("MatVouchTypeID") Is Nothing Then Me.AddError("MatVouchTypeID", "Please select Voucher Type")
        If Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")
        If Me.SelectedRow("TaxType") Is Nothing Then Me.AddError("TaxType", "Please select Tax Type")
        If Not Me.SelectedRow("MatDepId") Is Nothing Then
            Dim CompanyId As Integer = myContext.CommonData.GetCompanyID(myRow.Row)
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(CompanyId), Me.myRow("VouchDate"), PPFinal)
        End If

        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("VouchDate", "Please Select Valid Post Period")
        If myRow("VouchDate") > Now.Date Then Me.AddError("VouchDate", "Please Select Valid Date.")

        If myUtils.cValTN(myRow("PInvoiceID")) > 0 Then Me.AddError("", "Invoice already created. Not allow to edit")

        Dim rr1() As DataRow = myContext.Data.SelectDistinct(Me.dsForm.Tables("RefDocAdd"), "PInvoiceID", False,, "PInvoiceID is Not NULL").Select
        If rr1.Length > 0 Then
            Me.AddError("", "Invoice already created. Not allow to edit")
        End If

        Dim str3 As String = myUtils.MakeCSV(Me.dsForm.Tables("Pur").Select, "RecvMatVouchID")
        Dim Sql As String = "Select Distinct PInvoiceID from MatVouch where MatVouchID in (" & str3 & ")"
        Dim rr2() As DataRow = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0).Select
        If rr2.Length > 1 Then
            Me.AddError("", "Invoice of Selected Material Voucher should be same")
        End If

        If myUtils.cValTN(myRow("PriceSlabID")) > 0 AndAlso myUtils.cValTN(myRow("DivisionID")) = 0 Then Me.AddError("DivisionID", "Please Select Division")


        Dim ObjMatVouchProc As New clsMatVouchProc(myContext)
        For Each r1 As DataRow In dsForm.Tables("VouchItem").Select("PMatVouchItemID Is NULL")
            Dim r4 As DataRow = ObjMatVouchProc.oMasterMM.GetMvtCodeSPDataRow(myUtils.cValTN(r1("MvtCode")), myUtils.cStrTN(r1("SpStock")))
            If Not myUtils.IsInList(myUtils.cStrTN(r4("BomMvtCode")), "") Then
                Dim rr3() As DataRow = dsForm.Tables("VouchItem").Select("PMatVouchItemID = " & myUtils.cValTN(r1("MatVouchItemID")))
                If rr3.Length = 0 Then
                    Me.AddError("", "Please Select BOM Item For Item Code - " & myUtils.cStrTN(r1("ItemCode")))
                End If
            End If
        Next

        If myUtils.cValTN(myRow("PriceSlabID")) > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("VouchItem"), "MatVouchItemId", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please Enter Correct % Value in Cost Assignment.")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        Dim Skip As Boolean = False
        VSave = False

        If Me.Validate Then
            Dim ObjMatVouchProc As New clsMatVouchProc(myContext)
            SetBRAmount(dsForm.Tables("VouchItem"))
            Me.PopulateBOMMatDepID(ObjMatVouchProc.oMasterMM, myRow("MatDepID"))
            ObjMatVouchProc.LoadMultiVouch(myUtils.cValTN(myRow("MatVouchId")))
            ObjMatVouchProc.LoadVouch(myRow.Row.Table, dsForm.Tables("VouchItem"))
            ObjMatVouchProc.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("VouchItem"))
            Dim oRet As clsProcOutput = ObjMatVouchProc.CheckBalance(dsForm.Tables("VouchItem"))

            If oRet.Success Then
                Dim ObjVouch As New clsVoucherNum(myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)
                ObjVouch.GetNewSerialNo("MatVouchID", Me.SelectedRow("MatVouchTypeID")("VouchTypeCode"), myRow.Row)

                objPricingCalcBase.InitData(myRow.Row, "MatVouchId", myUtils.cValTN(frmIDX), "PostingDate", "MatVouchItemId", "QtyRate", Me.dsForm.Tables("VouchItem"))
                objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "AmountWV", "")
                For Each r2 As DataRow In dsForm.Tables("VouchItem").Select()
                    objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "AmountBasic")
                Next
                objPricingCalcBase.PopulateAccountingKeys(myRow("vouchdate"))


                myRow("AmountTotVal") = myUtils.cValTN(myView.MainGrid.GetColSum("AmountTot"))
                myRow("AmountWVVal") = myUtils.cValTN(myView.MainGrid.GetColSum("AmountWV"))

                Dim TaxTypeDescrip As String = Me.SelectedRow("TaxType")("Descrip")
                Dim VouchDescrip As String = " No: " & myUtils.cStrTN(myRow("VouchNum")) & ", Dt: " & Format(myRow("VouchDate"), "dd-MMM-yyyy") & ", TaxType: " & TaxTypeDescrip & ""

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "MatVouchID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from MatVouch Where MatVouchID = " & frmIDX)
                    frmIDX = myRow("MatVouchID")

                    myUtils.ChangeAll(dsForm.Tables("VouchItem").Select, "MatVouchID=" & frmIDX)

                    myUtils.CopyCol(dsForm.Tables("VouchItem"), "MatVouchItemID", "OldMatVouchItemID")
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("VouchItem"), "Select MatVouchItemID, MatVouchID,SOSpareID,QtySOSpare, ItemID,PriceSlabID,ReturnFY, AmountTrWv, PIDUnitID, VarNum, MvtCode, SpStock, ClassType, TransType, StockStage, StockStage2, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, NewPIDUnitID, NewVarNum, AmountWV, FixedAssetID, pMatVouchItemID, MfgCharges, ReserveGRBehave, ReserveGIBehave, ValuationClass, OdNoteItemID, PPSubType, QtyEntry, BasicRate, AmountBasic, AmountTot, Remark, Hsn_Sc, TaxCredit from MatVouchItem")

                    For Each r1 As DataRow In dsForm.Tables("VouchItem").Select("PMatVouchItemID Is NULL")
                        For Each r2 As DataRow In dsForm.Tables("VouchItem").Select("PMatVouchItemID = " & r1("OldMatVouchItemID"))
                            r2("PMatVouchItemID") = r1("MatVouchItemID")
                            Skip = True
                        Next
                    Next

                    If Skip = True Then
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("VouchItem"), "Select MatVouchItemID, MatVouchID, ItemID,PriceSlabID,ReturnFY, PIDUnitID, SOSpareID, QtySOSpare, VarNum, MvtCode, SpStock, ClassType, TransType, StockStage, StockStage2, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, NewPIDUnitID, NewVarNum, AmountWV, FixedAssetID, pMatVouchItemID, MfgCharges, ReserveGRBehave, ReserveGIBehave, ValuationClass, OdNoteItemID, PPSubType, QtyEntry, BasicRate, AmountBasic, AmountTot, Remark, Hsn_Sc, TaxCredit from MatVouchItem")
                    End If
                    objPricingCalcBase.VSave()

                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("Pur"), "Select PurItemHistID,PurItemID,MatVouchItemID,RecvMatVouchID,Dated,QtyRecd,QtyOK,QtyRej,QtyDevi,QtyIssue,RecdTCInSt,RecdTCInQly from PurItemHist", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("OB"), "Select ItemStockValueBalID, ItemStockValueID, MatVouchItemID, OBDescrip, Dated, Qty, Qty2, AmountTot, AmountWV from ItemStockValueBal", True)

                    ObjMatVouchProc.UpdateBalance()
                    ObjMatVouchProc.SaveRefDoc(dsForm.Tables("RefDocAdd"))

                    For Each r2 As DataRow In dsForm.Tables("VouchItem").Select("PriceSlabID > 0")
                        Dim rr1() As DataRow = dsCombo.Tables("Items").Select("ItemID = " & myUtils.cValTN(r2("ItemID")) & " and HSN_SC is Null")
                        If rr1.Length > 0 Then
                            myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, "Update ItemSubCats set HSN_SC = '" & myUtils.cStrTN(r2("HSN_SC")) & "' where SubCatID = " & myUtils.cValTN(rr1(0)("SubCatID")) & " and HSN_SC is Null")
                        End If
                    Next

                    ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)

                    Dim oRet2 As clsProcOutput = ObjMatVouchProc.HandleWorkflowState(myRow.Row)
                    If oRet2.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(VouchDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(VouchDescrip, oRet.Message)
                        Me.AddError("", oRet2.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(VouchDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            Else
                Me.AddError("", oRet.Message)
            End If
        End If
    End Function

    Private Sub SetBRAmount(dt As DataTable)
        Dim ObjMatVouchProc As New clsMatVouchProc(myContext)
        For Each r1 As DataRow In dt.Select()
            Dim r4 As DataRow = ObjMatVouchProc.oMasterMM.GetMvtCodeSPDataRow(myUtils.cValTN(r1("MvtCode")), myUtils.cStrTN(r1("SpStock")))
            If myUtils.IsInList(myUtils.cStrTN(r4("PricingType")), "BR") Then
                If myUtils.IsInList(myUtils.cStrTN(r4("BomMvtCode")), "") Then
                    r1("AmountTot") = myUtils.cValTN(r1("BasicRate"))
                    r1("AmountWV") = myUtils.cValTN(r1("BasicRate"))
                Else
                    r1("MfgCharges") = myUtils.cValTN(r1("BasicRate"))
                End If
            End If
        Next
    End Sub

    Private Sub ChangeColRowwise(dt As DataTable, MatVouchID As Integer)
        myUtils.ChangeAll(dt.Select, "PIDField=MatVouchID")
        myUtils.ChangeAll(dt.Select, "PIDValue=" & MatVouchID)
        myUtils.ChangeAll(dt.Select, "pIDFieldItem=MatVouchItemID")

        For Each r1 In dt.Select
            r1("pIDValueItem") = r1("MatVouchItemID")
        Next
    End Sub

    Private Sub PopulateBOMMatDepID(oMaster As clsMasterDataMM, MatDepID As Integer)

        For Each r1 As DataRow In dsForm.Tables("VouchItem").Select("PMatVouchItemID Is Not NULL")
            Dim r2 As DataRow = oMaster.GetMvtCodeFldDataRow(myUtils.cValTN(r1("MvtCode")), myUtils.cStrTN(r1("SpStock")), "I", "MatDepID")
            If myUtils.IsInList(myUtils.cStrTN(r2("FieldType")), "R") Then
                Dim r4 As DataRow = oMaster.GetMvtCodeSPDataRow(r1("MvtCode"), r1("SpStock"))
                If Not IsNothing(r4) Then
                    If myUtils.IsInList(myUtils.cStrTN(r4("SrcDesTypeItem")), "H") Then
                        Dim rr1() As DataRow = dsForm.Tables("VouchItem").Select("MatVouchItemID = " & myUtils.cValTN(r1("PMatVouchItemID")))
                        If rr1.Length > 0 Then r1("MatDepID") = myUtils.cValTN(rr1(0)("MatDepID"))
                    Else
                        r1("MatDepID") = myUtils.cValTN(MatDepID)
                    End If
                End If
            End If
        Next
    End Sub

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, str1 As String = "", str2 As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Dim VouchDate As Date
            Dim VouchTypeCode As String = myUtils.cStrTN(myContext.SQL.ParamValue("@vouchtypecode", Params))
            Dim VendorID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@vendorid", Params))
            Dim CustomerID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@customerid", Params))
            If myContext.SQL.Exists(Params, "@VouchDate") Then VouchDate = myContext.SQL.ParamValue("@VouchDate", Params)

            If VendorID > 0 Then
                str1 = "VendorID = " & VendorID
            ElseIf CustomerID > 0 Then
                str1 = "CustomerID = " & CustomerID
            End If

            Dim Str4 As String = ""
            Dim Str6 As String = ""
            If myContext.SQL.Exists(Params, "@InvoiceCampusID") Then
                Dim InvoiceCampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@InvoiceCampusID", Params))
                Dim DivisionID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@DivisionID", Params))
                Str4 = "IsNull(DivisionID, 0) = " & DivisionID & ""
                Str6 = "IsNull(InvoiceCampusID, 0) = " & InvoiceCampusID & ""
            End If

            Select Case SelectionKey.Trim.ToLower
                Case "puritem"
                    Dim PurOrderID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@purorderid", Params))
                    Dim PurItemIDCSV As String = myUtils.cStrTN(myContext.SQL.ParamValue("@puritemidcsv", Params))
                    Dim Sql1 As String = "SELECT PurOrderID, PurItemID, ItemCode, ItemName, UnitName, TotalQty, QtyRecd, Convert(decimal(18,4), NULL) as Qty, Remark From PurListOItems() Where PurOrderID = " & PurOrderID & " and PurItemID Not in (" & PurItemIDCSV & ")"
                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = True
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""ItemCode"" CAPTION=""Item Code""/><COL KEY=""ItemName"" CAPTION=""Item Name""/><COL KEY=""UnitName"" CAPTION=""Unit Name""/>"
                    Model.MainGrid.QuickConf(Sql1, True, "1-3-1-1-1-1-2")
                    str1 = "<BAND IDFIELD=""PurItemID"" TABLE=""PurItems"" INDEX=""0""><COL KEY=""Qty""/></BAND>"
                    Model.MainGrid.PrepEdit(str1, EnumEditType.acEditOnly)
                Case "idn"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("IDNoteID Not in (Select IDNoteID from IDNoteItem where PurItemID in (@puritemidcsv)) and CampusID = (@campusid) and (MatVouchID is NULL or MatVouchID = (@matvouchid)) and isnull(iscompleted,0)=0", Params)
                    Dim sql1 As String = myUtils.CombineWhere(False, sql, str1, "NoteDate <= '" & Format(VouchDate, "dd-MMM-yyyy") & "'")
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""IDNoteID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql1) & "</SQLWHERE2></MODROW>")
                Case "po"
                    str1 = ""
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim CompanyId As Integer = myContext.CommonData.GetCompanyIDFromCampus(CampusID)

                    If myContext.SQL.Exists(Params, "@MainPartyID") Then
                        str1 = "MainPartyID = " & myContext.SQL.ParamValue("@MainPartyID", Params)
                    End If

                    Dim sql As String = myContext.SQL.PopulateSQLParams("OrderType = (@ordertype) And CompanyId = " & CompanyId & " And isnull(isReleased,0)= 1 And isnull(iscompleted, 0) = 0 ", Params)
                    Dim sql1 As String = myUtils.CombineWhere(False, sql, str1, Str4, "OrderDate <= '" & Format(VouchDate, "dd-MMM-yyyy") & "'")

                    Dim OrderType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@ordertype", Params))
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""" & OrderType & """/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql1) & "</SQLWHERE2></MODROW>")
                Case "jwo"

                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim CompanyId As Integer = myContext.CommonData.GetCompanyIDFromCampus(CampusID)

                    Dim sql As String = myContext.SQL.PopulateSQLParams("OrderType = (@ordertype) And CompanyId = " & CompanyId & " And isnull(isReleased,0)= 1 And isnull(iscompleted, 0) = 0 ", Params)
                    Dim sql1 As String = myUtils.CombineWhere(False, sql, str1, Str4, "OrderDate <= '" & Format(VouchDate, "dd-MMM-yyyy") & "'")

                    Dim OrderType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@ordertype", Params))
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""" & OrderType & """/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql1) & "</SQLWHERE2></MODROW>")
                Case "mo"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("OrderType = (@ordertype) And CampusID = @CampusID And isnull(isReleased,0)= 1 And isnull(iscompleted, 0) = 0 ", Params)
                    Dim sql1 As String = myUtils.CombineWhere(False, sql, str1, Str4, "OrderDate <= '" & Format(VouchDate, "dd-MMM-yyyy") & "'")

                    Dim OrderType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@ordertype", Params))
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""" & OrderType & """/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql1) & "</SQLWHERE2></MODROW>")
                Case "lpo"
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim CompanyId As Integer = myContext.CommonData.GetCompanyIDFromCampus(CampusID)
                    Dim VendorType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@VendorType", Params))
                    If VendorType <> "EM" Then str1 = ""
                    Dim sql As String = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, "OrderType = @ordertype", "CompanyId = " & CompanyId, "isnull(isReleased,0)= 1", "isnull(iscompleted, 0) = 0 "), Params)
                    Dim sql1 As String = myUtils.CombineWhere(False, sql, str1, Str4, "OrderDate <= '" & Format(VouchDate, "dd-MMM-yyyy") & "'")
                    Dim OrderType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@ordertype", Params))
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""" & OrderType & """/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql1) & "</SQLWHERE2></MODROW>")
                Case "odn"
                    Dim StrTaxType As String = ""
                    If myContext.SQL.Exists(Params, "@TaxType") Then StrTaxType = "TaxType = '" & myUtils.cStrTN(myContext.SQL.ParamValue("@TaxType", Params)) & "'"
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("Select ODNoteID from trpListODNoteItem() where MvtType = '" & VouchTypeCode & "' and (MatVouchID is NULL or MatVouchID = (@matvouchid))", Params)
                    Dim dtOdNote As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql1).Tables(0)
                    Dim str3 As String = myUtils.MakeCSV(dtOdNote.Select, "ODNoteID")

                    Dim sql As String = myContext.SQL.PopulateSQLParams("ODNoteID in (" & str3 & ") and OdNoteID Not in (Select OdNoteID from OdNoteItem where OdNoteItemID in (@odnoteitemidcsv)) and CampusID = (@campusid)", Params)
                    Dim sql2 As String = myUtils.CombineWhere(False, sql, str1, Str4, Str6, StrTaxType, "ChallanDate < '" & Format(VouchDate.AddDays(1), "dd-MMM-yyyy") & "'")
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ODNoteID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql2) & "</SQLWHERE2></MODROW>")
                Case "mv"
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params))
                    Dim MatDepID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@matdepid", Params))
                    Dim str3 As String = "isnull(HasTransitTransfer,0) = 1 and MatVouchID in (Select MatVouchID from MatVouchItem where (CampusID = " & CampusID & " or MatDepID = " & MatDepID & "))"
                    Dim str5 As String = "isnull(HasTransitTransfer,0) = 2 and MatVouchID in (Select MatVouchID from invListMatVouch() where (CampusID = " & CampusID & "))"
                    Dim str7 As String = myUtils.CombineWhereOR(False, str3, str5)

                    Dim sql As String = myContext.SQL.PopulateSQLParams("MatVouchID Not in (Select MatVouchID from MatVouchItem where MatVouchItemID in (@matvouchitemidcsv)) and (RecvMatVouchID is NULL or RecvMatVouchID = (@matvouchid)) ", Params)
                    Dim sql2 As String = myUtils.CombineWhere(False, sql, str1, str7, "VouchDate <= '" & Format(VouchDate, "dd-MMM-yyyy") & "'")
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""MatVouchID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql2) & "</SQLWHERE2></MODROW>")
                Case "prodlot"
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdLotID""/>", False)
                Case "orderdatau"
                    Dim Str3 As String
                    Dim MatDepId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@matdepid", Params))
                    If VendorID = 0 AndAlso MatDepId > 0 Then
                        str1 = "MatDepId = " & MatDepId
                    End If
                    Dim OrderType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@ordertype", Params))
                    If myUtils.IsInList(myUtils.cStrTN(OrderType), "MO") Then
                        Str3 = ""
                    Else
                        Str3 = myContext.SQL.PopulateSQLParams("isnull(InvoiceCampusID,0)=@InvoiceCampusID and isnull(DivisionID,0)=@DivisionID", Params)
                    End If

                    If myUtils.cStrTN(OrderType).Trim.Length > 0 Then str2 = "OrderType in ('" & myUtils.cStrTN(OrderType) & "')"
                    Dim sql As String = myUtils.CombineWhere(False, str1, str2, Str3, myContext.SQL.GenerateSQLWhere(Params, "ItemID,PIDUnitID,VarNum,ClassType,TransType,StockStage"))
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PurItemID""/>", False, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "orderdataq"
                    Dim MatDepId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@matdepid", Params))
                    If VendorID = 0 And MatDepId > 0 Then
                        str1 = "MatDepId = " & MatDepId
                    End If

                    str2 = "OrderDate <= '" & Format(Now.Date, "dd-MMM-yyyy") & "' and OrderType in ('PO','LPO','JWO')"
                    Dim sql As String = myUtils.CombineWhere(False, str1, str2, "QtyRecd > 0", myContext.SQL.GenerateSQLWhere(Params, "ItemID,PIDUnitID,VarNum,ClassType,TransType,StockStage"))
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PurItemHistID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "mi"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("IndentDate <= '" & Format(VouchDate, "dd-MMM-yyyy") & "' and IndentID Not in (Select IndentID from IndentItem where ItemID in (@itemidcsv)) and MvtType = '" & myUtils.cStrTN(VouchTypeCode) & "' and ForMatDepID = (@matdepid) and (MatVouchID is NULL or MatVouchID = (@matvouchid))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""IndentID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "indentitem"
                    Dim IndentID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@indentid", Params))
                    Dim Sql1 As String = "SELECT IndentID, IndentItemID, ItemCode, ItemName, UnitName, QtyEntry as Qty From proListIndentItem() Where IndentID = " & IndentID
                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = True
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""ItemCode"" CAPTION=""Item Code""/><COL KEY=""ItemName"" CAPTION=""Item Name""/><COL KEY=""UnitName"" CAPTION=""Unit Name""/><COL KEY=""QtyEntry"" CAPTION=""Qty""/>"
                    Model.MainGrid.QuickConf(Sql1, True, "1-3-1-1")
                    Dim str5 As String = "<BAND IDFIELD=""IndentItemID"" TABLE=""IndentItem"" INDEX=""0"">/></BAND>"
                    Model.MainGrid.PrepEdit(str5, EnumEditType.acEditOnly)
                Case "pidunit"
                    Dim sql As String = ""
                    If myContext.SQL.Exists(Params, "@CustomerIDCSV") Then sql = "<MODROW><SQLWHERE2>" & myContext.SQL.PopulateSQLParams("CustomerID in (@CustomerIDCSV)", Params) & "</SQLWHERE2></MODROW>"
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PIDUnitID""/>", False,, sql)
                Case "stockbaldep"
                    Dim ItemID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ItemID", Params))
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim CampusID2 As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID2", Params))
                    Dim Dated As Date = myContext.SQL.ParamValue("@Date", Params)
                    Dim CompanyId As Integer = myContext.CommonData.GetCompanyIDFromCampus(CampusID)
                    Dim PIDUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@PIDUnitID", Params))
                    Dim str As String = ""
                    If PIDUnitID > 0 Then
                        str = "(CampusID = " & CampusID & " or CampusID = " & CampusID2 & ") and ItemID = " & ItemID & " and PIDUnitID = " & PIDUnitID
                    Else
                        str = "(CampusID = " & CampusID & " or CampusID = " & CampusID2 & ") and ItemID = " & ItemID
                    End If

                    Model = New clsViewModel(vwState, myContext)
                    Dim objProc As New clsMVProcQtyDep(myContext)
                    Dim dt As DataTable = objProc.ItemStockBalance(CompanyId, Dated, str, "IT", True)
                    dt = myContext.Tables.SelectTable(dt, {"VendorID", "CustomerID", "DepName", "SpStock", "StockStage", "TaxType", "WoInfo", "VarNum", "QtyUR", "QtyR", "QtyQI", "QtyTransit", "QtyTot", "UnitName"})
                    Model.MainGrid.BindGridData(dt.DataSet, 0)
                    Model.MainGrid.QuickConf("", True, "2-1-1.2-1-1-1-.8-.8-.8-1.2-.8-1.2")
                Case "stockbalcamp"
                    Dim ItemID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ItemID", Params))
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim CampusID2 As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID2", Params))
                    Dim Dated As Date = myContext.SQL.ParamValue("@Date", Params)
                    Dim CompanyId As Integer = myContext.CommonData.GetCompanyIDFromCampus(CampusID)
                    Dim PIDUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@PIDUnitID", Params))
                    Dim str As String = ""
                    If PIDUnitID > 0 Then
                        str = "(CampusID = " & CampusID & " or CampusID = " & CampusID2 & ") and ItemID = " & ItemID & " and PIDUnitID = " & PIDUnitID
                    Else
                        str = "(CampusID = " & CampusID & " or CampusID = " & CampusID2 & ") and ItemID = " & ItemID
                    End If

                    Model = New clsViewModel(vwState, myContext)
                    Dim objProc As New clsMVProcQtyCampus(myContext)
                    Dim dt As DataTable = objProc.ItemStockBalance(CompanyId, Dated, str, "IT", True)
                    dt = myContext.Tables.SelectTable(dt, {"DispName", "SpStock", "StockStage", "TaxType", "WoInfo", "VarNum", "Qty", "QtyTransit", "QtyTot", "UnitName"})

                    Model.MainGrid.BindGridData(dt.DataSet, 0)
                    Model.MainGrid.QuickConf("", True, "2-1-1-1-1.2-1-.8-1-1-1")
                Case "covringfactor"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("Select AttribName, AttribValue from genlistattribvalue() where ItemID =  @ItemID", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""AttribName"" CAPTION=""Attribute Name""/><COL KEY=""AttribValue"" CAPTION=""Attribute Value""/>"
                    Model.MainGrid.QuickConf(sql, True, "2-1")
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
                Case "salesorder"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("MainPartyID = @MainPartyID and CompanyID = @CompanyId and SalesOrderID Not in (@SalesOrderID) and (isnull(OrderDate,'3099-Jan-01') <= '@VouchDate' or isnull(LOIDate,'3099-Jan-01') <= '@VouchDate')", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SalesOrderID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "serials"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdSerialID""/>", True, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
                Case "spares"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid and TransType = @transtype", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOSpareID""/>", False, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim Sql As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToUpper
                Case "PO", "LPO", "JWO", "MO"
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("PurOrderID = (@purorderid) and OrderType = (@ordertype) and isnull(iscompleted,0)=0 and PurItemID in (@puritemidcsv)", Params)
                    Sql = "SELECT ItemID, MatDepID, ISEto, BasicRate as BasicRatePO, RateUnitName, ItemCode, ItemName, ItemUnitID [ItemUnitIDEntry], UnitName, 0.0 as QtyEntry, qtyrate/totalqty as QtyRateFac,0.0 as QtyRate, Remark, PriceSlabID, VarNum, TaxCredit, OrderQtyUnitID, ItemUnitID, ItemUnitID2, OrderRateUnitID, PIDUnitID, ClassType, ValuationClass, StockStage, TransType, PurItemID, OrderNum, OrderDate From PurListOItems() where " & sql1
                Case "IDN"
                    Dim IDNoteID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@idnoteid", Params))
                    Sql = " SELECT *, ItemUnitID [ItemUnitIDEntry], Qty [QtyEntry], (Qty/totalqty) * QtyRate as QtyRate From trpListIDNoteItem() Where  IDNoteID = " & IDNoteID
                Case "ODN"
                    Dim ODNoteID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@odnoteid", Params))
                    Sql = " SELECT *, ItemUnitID [ItemUnitIDEntry] From trpListODNoteItem()  Where   ODNoteID = " & ODNoteID
                Case "MV"
                    Dim MatVouchID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@matvouchid", Params))
                    Sql = Me.GenerateMVISQL("matvouchid", MatVouchID)
                Case "MI"
                    Dim IndentID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@indentid", Params))
                    Sql = " SELECT *, 'M' as ClassType, 'RM' as StockStage, 'N' as SpStock From proListIndentItem() Where  IndentID = " & IndentID
                Case "VENDORID"
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("SpStock = 'V' and VendorID in (@idcsv)", Params)
                    Sql = "Select distinct ItemID, VendorID, 0 as MatDepID from ItemStockQtyCampus where " & sql1
                Case "MATDEPID"
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("SpStock = 'N' and MatDepID in (@idcsv)", Params)
                    Sql = "Select distinct ItemID, MatDepID, 0 as VendorID from ItemStockQtyDep where " & sql1
            End Select
            oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
        End If
        Return oRet
    End Function

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.ToLower
            Case "pidunititemvar"
                Dim Sql As String = "select PIDUnitItemVarId,Pidunitid,itemid,varnum,itemvmsid, varnum as varnumid, (case when isnull(varname,'') = ''  then 'Same as Item Name' else VarName end) as VarName from pidunititemvar  where ItemId = " & myUtils.cValTN(frmIDX) & " order by ItemId, PidUnitId, VarNum"
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
            Case "pidunit"
                Dim sql As String = "select * from plnlistpidunit() where pidunitid = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Case "itemid"
                Dim sql As String = "Select ItemId, ItemCode, ItemName, UnitName, ItemUnitId, IsEto, SubCatId, ItemUnitId2, OrderQtyUnitId, OrderRateUnitId, IssueUnitId, OrderQtyNumReq, OrderQtyNumText,ValuationClass from InvListItems()  Where ItemID = " & myUtils.cValTN(frmIDX) & " Order by ItemName"
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Case "salesorderdescrip"
                Dim sql As String = "Select OrderNum, OrderDate  from SalesOrder  where SalesOrderID = " & myUtils.cValTN(frmIDX)
                Dim dt2 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt2.Rows.Count > 0 Then oRet.Description = "Sales Order :- " & myUtils.cStrTN(dt2.Rows(0)("OrderNum")) & " Date - " & Format(dt2.Rows(0)("OrderDate"), "dd-MMM-yyyy")
            Case "sparesoitem"
                Dim sql As String = "Select SOSpareID, SpareName, PIDInfo from slsListSOSpares() where SalesOrderID = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
        End Select
        Return oRet
    End Function

    Public Overrides Sub OperateProcess(processKey As String)
        Dim oProc As New clsMatVouchProc(myContext)
        Select Case processKey.Trim.ToLower
            Case "generate"
                Dim rMatVouchType As DataRow = Me.SelectedRow("matvouchtypeid")

                Dim ds = Me.DatasetCollection("generatedata")
                Dim rr1() As DataRow = oProc.GenerateMatVouchItems(rMatVouchType, myRow.Row, ds.Tables("rSelVouch").Rows(0), ds.Tables("dtSql"), dsForm)
                myUtils.AddTable(ds, rr1(0).Table, "MatVouchItem")
        End Select
    End Sub
End Class