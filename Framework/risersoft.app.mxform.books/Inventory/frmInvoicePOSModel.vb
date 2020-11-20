Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.app.mxform.gst

<DataContract>
Public Class frmInvoicePOSModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False, dicSQL As New clsCollecString(Of String)
    Dim myViewFASO, myViewCostLot, myViewCostWBS, myViewCostCenter, myViewResRecItem, myViewResIssueItem As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        myViewFASO = Me.GetViewModel("FASO")
        myViewCostLot = Me.GetViewModel("CostLot")
        myViewCostWBS = Me.GetViewModel("CostWBS")
        myViewCostCenter = Me.GetViewModel("CostCenter")
        myViewResRecItem = Me.GetViewModel("ResRecItem")
        myViewResIssueItem = Me.GetViewModel("ResIssueItem")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select MatDepID, DepName, CampusID, TaxAreaCode,DivisionCodeList, WODate,CompletedOn, CompanyID from mmListDepsMat() where IsStore = 1 Order by DepName"
        Me.AddLookupField("MatDepId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DepsMat").TableName)

        sql = "Select CampusID, DispName, Camp.CompanyID, TaxAreaCode, DivisionCodeList, WODate, Camp.CompletedOn, TaxAreaID, GstRegID, Camp.SalesOrderID, PidUnitID,CampusCode,ProjectID from mmlistCampus() as Camp Left join SalesOrder on Camp.SalesOrderID = SalesOrder.SalesOrderID  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("DeliveryCampusID", "Campus")
        Me.AddLookupField("ProjectCampusID", "Campus")

        sql = "SELECT  CustomerID, CustDescrip, CustomerClass, GSTIN,TaxAreaCode, TaxAreaID, MainPartyID FROM  slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = "Select PartyID, PartyName, TaxAreaID from Party order by PartyName"
        Me.AddLookupField("ConsigneeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Party").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxType", "")
        Me.AddLookupField("TaxInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxInvoiceType").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)


        sql = "select TaxAreaID, Descrip,TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

        '----------------Item---------------
        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)


        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

        sql = "Select * from systemoptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")




        sql = myFuncsBase.CodeWordSQL("Material", "MvtType", "")
        Me.AddLookupField("VoucherType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "VoucherType").TableName)

        sql = myFuncsBase.CodeWordSQL("Stock", "taxtype", "")
        Me.AddLookupField("TaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxType").TableName)

        sql = "Select MatDepID, DepName, CampusID, IsStore, IsShop, WODate,CompletedOn from mmListDepsMat() where (IsStore = 1) or (IsShop = 1) Order by DepName"
        Me.AddLookupField("VouchItem", "MatDepID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "DepsMatItem").TableName)

        sql = "Select MatVouchTypeID, RefDocTypeName, DefMvtCode, VouchTypeName, RefDocTypeCode, VouchTypeCode from MatVouchType Order by VouchTypeName, SortIndex"
        Me.AddLookupField("MatVouchTypeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "MatVouchType").TableName)

        '------------------------MatVouch Item----------
        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("VouchItem", "ItemUnitIDEntry", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)
        Me.AddLookupField("VouchItem", "ItemUnitID", "Units")
        Me.AddLookupField("VouchItem", "ItemUnitID2", "Units")
        Me.AddLookupField("VouchItem", "OrderRateUnitID", "Units")


        sql = "Select Distinct MatMvtCode.MatMvtCode, Description, MvtType, DocRefType, ReasonField, EntRefType, PricingType, DOCType,OrderUpdateCode,AllowOther,TaxTypeSrc from MatMvtCode Left Join MatMvtDocType on MatMvtCode.MatMvtCode = MatMvtDocType.MatMvtCode where MvtType = 'GI' and (DocType = 'ODN') and DocSubType = 'SC' and PricingType = 'NA' and EntRefType is NULL"
        Me.AddLookupField("VouchItem", "MvtCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "MatMvtCode").TableName)


        sql = "Select Class,Class,ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass where ClassType in ('A','M') or (ClassType = 'S' and ClassSubType in ('P','B'))"
        Me.AddLookupField("VouchItem", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Material') and CodeWord in ('RM', 'CG', 'CT', 'ST','WIP','FG')  Order by CodeClass"
        Me.AddLookupField("VouchItem", "Stockstage", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Stockstage").TableName)


        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "CodeWord in ('A','M','S')")
        Me.AddLookupField("VouchItem", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = myFuncsBase.CodeWordSQL("Stock", "qtytype1", "", "Tag")
        Me.AddLookupField("VouchItem", "QtyTypeSrc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "QtyTypeSrc").TableName)


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


        Me.AddLookupField("CashCampusID", "Campus")

        sql = "Select BankAccountID, AccountName, GlAccountId, companyid, ShortName from BankAccount"
        Me.AddLookupField("BankAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BankAccount").TableName)

        Dim str1 As String = myUtils.CombineWhereOR(False, "isnull(imprestenabled,0)=1", "employeeid in (select imprestemployeeid from payment)")
        sql = "Select employeeid, empcode, fullname, onrolls, companyid,JoinDate,LeaveDate from hrpListAllEmp() where " & str1 & " order by fullname"
        Me.AddLookupField("ImprestEmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "emp").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "Mode", "Codeword <> 'WO'")
        Me.AddLookupField("PaymentMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentMode").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objIVProcFico As New clsIVProcFICO("IS", myContext), objPricingCalcBase As New clsPricingCalcBase(myContext), ObjMatVouchProc As New clsMatVouchProc(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0

        dicSQL.Add("Invoice", "Select *  from Invoice Where InvoiceID  = %frmIDX%")
        dicSQL.Add("MatVouch", "Select * from MatVouch Where PInvoiceID  = %frmIDX%")
        Me.InitData(dicSQL, "SalesOrderID,InvoiceTypeCode", oView, prepMode, prepIDX, strXML)



        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
            myRow("VouchDate") = Now.Date
            myRow("InvoiceTypeCode") = "PM"
            myRow("Billof") = "C"
            myRow("TaxType") = "NEX"
        Else
            Dim rPostPeriod As DataRow = objIVProcFico.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If


        Dim Tag As String = "Tag = 'IS'"
        Dim Where As String = "where CodeClass = 'Invoice' and CodeType in ('B2B', 'B2C', 'CDN', 'Exp')"


        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", Tag)
        Me.AddLookupField("GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "GSTTypecode", Tag)
        Me.AddLookupField("GSTInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceType").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass, Tag from CodeWords " & Where & " Order by CodeClass"
        Me.AddLookupField("GSTInvoiceSubType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceSubType").TableName)



        Me.BindDataTable(myUtils.cValTN(prepIDX))
        objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("VouchItem"))

        objIVProcFico.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))

        ObjMatVouchProc.LoadMultiVouch(myUtils.cValTN(myRow("MatVouchId")))
        ObjMatVouchProc.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("VouchItem"))


        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-2-1-1-1-1-1-1-1")
        myView.MainGrid.PrepEdit("<BAND IDFIELD=""MatVouchItemId"" TABLE=""MatVouchItem"" INDEX=""0""><COL KEY=""MatVouchItemID, MatVouchID, pMatVouchItemID, ItemID,HasTransitTransfer, PriceSlabID, AmountTrWv, PIDUnitID, VarNum, MvtCode, SpStock, TransType, StockStage, StockStage2, TaxType, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, Remark, NewPIDUnitID, NewVarNum, AmountWV, FixedAssetID, SrcDesType, MfgCharges, ReserveGRBehave, ReserveGIBehave, ClassType, ValuationClass, OdNoteItemID, PPSubType,ReturnFY,TaxCredit,Hsn_Sc""/><COL KEY=""ItemCode"" CAPTION=""Item Code""/><COL KEY=""ItemName"" CAPTION=""Item Name""/><COL KEY=""QtyEntry"" CAPTION=""Qty""/><COL KEY=""UnitName"" CAPTION=""Unit Name""/><COL KEY=""AmountBasic"" CAPTION=""Basic Amount""/><COL KEY=""AmountTot"" CAPTION=""Total Amount""/><COL KEY=""BasicRate"" CAPTION=""Basic Rate""/></BAND>", EnumEditType.acCommandOnly)

        myViewResRecItem.MainGrid.BindGridData(Me.dsForm, 2)
        myViewResRecItem.MainGrid.QuickConf("", True, "1-2-2-1")
        Dim str1 As String = "<BAND IDFIELD=""PlnReserveItemID"" TABLE=""PlnReserveItem"" INDEX=""0""><COL KEY=""PlnReserveItemID, PlnReserveID, IsTransfer,ResItemType,MatVouchItemID,PostPeriodID, Dated,Qty""/><COL SKIP=""True"" KEY=""CampusID"" LOOKUPSQL=""Select CampusID, DispName from Campus"" CAPTION=""Campus Name""/></BAND>"
        myViewResRecItem.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewResIssueItem.MainGrid.BindGridData(Me.dsForm, 2)
        myViewResIssueItem.MainGrid.QuickConf("", True, "1-2-2-1")
        str1 = "<BAND IDFIELD=""PlnReserveItemID"" TABLE=""PlnReserveItem"" INDEX=""0""><COL KEY=""PlnReserveItemID, PlnReserveID, IsTransfer,ResItemType,MatVouchItemID,PostPeriodID, Dated,Qty""/><COL SKIP=""True"" KEY=""CampusID"" LOOKUPSQL=""Select CampusID, DispName from Campus"" CAPTION=""Campus Name""/></BAND>"
        myViewResIssueItem.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Dim dtFA As DataTable = dsForm.Tables("FixedAssetItem")
        myViewFASO.MainGrid.BindGridData(dsForm, dtFA.DataSet.Tables.IndexOf(dtFA))
        myViewFASO.MainGrid.QuickConf("", True, "2-1-1")
        str1 = "<BAND INDEX = ""0"" TABLE = ""FixedAssetItem"" IDFIELD=""FixedAssetItemID""><COL KEY ="" FixedAssetID, InvoiceItemID, PaymentItemTransID, Qty ""/><COL KEY=""AMOUNT"" NOEDIT=""True""/></BAND>"
        myViewFASO.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)


        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))

        sql = "Select ProjectID from SalesOrder where SalesOrderID  = " & myUtils.cValTN(myRow("SalesOrderID")) & ""
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then
            Me.ModelParams.Add(New clsSQLParam("@ProjectID", myUtils.cValTN(dt1.Rows(0)("ProjectID")), GetType(Integer), False))
        End If

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

    Private Function GenerateMVISQL(ByVal InvoiceID As Integer) As String
        Dim sql As String = "Select matvouchitem.MatVouchItemID,InvoiceItem.InvoiceItemID, InvoiceItem.InvoiceID, Description,InvoiceItem.ItemUnitID,InvoiceItem.InvoiceItemType,InvoiceItem.PPSubType, 0 as OldMatVouchItemID, MatVouchID, {0}, matvouchitem.ItemID, InvoiceItem.PriceSlabID,AdjustAmountTot, AdjustAmountWV, AmountTrWv, matvouchitem.PIDUnitID, matvouchitem.VarNum, MvtCode, SpStock, matvouchitem.Classtype, InvoiceItem.TransType, matvouchitem.TaxCredit, StockStage, StockStage2, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, matvouchitem.QtyRate, NewPIDUnitID, NewVarNum, {4}, {5}, FixedAssetID, pMatVouchItemID, MfgCharges, ReserveGRBehave, ReserveGIBehave, matvouchitem.ValuationClass, matvouchitem.PPSubType, OdNoteItemID, matvouchitem.ReturnFY,matvouchitem.Hsn_Sc, {1}, {2}, QtyEntry, {3}, matvouchitem.BasicRate, matvouchitem.AmountBasic, matvouchitem.AmountTot, matvouchitem.AmountWV,  Remark FROM matvouchitem Inner Join InvoiceItem on MatVouchItem.MatVouchItemID = InvoiceItem.MatVouchItemID and MatVouchItem.TenantID = InvoiceItem.TenantID  where MatVouchID in (Select MatVouchID from MatVouch where PInvoiceID = " & InvoiceID & ")"
        Dim str1 As String = "(select ODNoteID from odnoteitem where odnoteitem.odnoteitemid = matvouchitem.odnoteitemid) as ODNoteID"
        Dim str2 As String = "(select ItemCode from items where items.itemid = matvouchitem.itemid) as ItemCode"
        Dim str3 As String = "(select ItemName from items where items.itemid = matvouchitem.itemid) as ItemName"
        Dim str4 As String = "(select UnitName from invlistitems() where invlistitems.itemid = matvouchitem.itemid) as UnitName"
        Dim str5 As String = "(select WOInfo from PidUnit where Pidunit.pidunitid = matvouchitem.pidunitid) as WOInfo"
        Dim str6 As String = "(select WOInfo from PidUnit where Pidunit.pidunitid = matvouchitem.newpidunitid) as NewWOInfo"
        sql = String.Format(sql, str1, str2, str3, str4, str5, str6)
        Return sql
    End Function

    Private Sub BindDataTable(ByVal InvoiceID As Integer)
        Dim ds As DataSet, Sql1, Sql2, Sql3, Sql4, Sql5, Sql, Sql6, Sql7, Sql8, Sql9, Sql10, Sql11, Sql12, Sql13 As String
        Sql1 = Me.GenerateMVISQL(InvoiceID)
        Sql2 = "Select PlnReserveItemID, PlnReserveID, MatVouchItemID,PostPeriodID, ResItemType, ProdLotID, ResItemSort, ItemID, PIDUnitID, IsTransfer, VarNum, ItemName,  Dated, LotNum, LotWOInfo as Woinfo, CampusID, Abs(Qty) as Qty from plnListReserveItem() where MatVouchID in (Select MatVouchID from MatVouch where PInvoiceID = " & InvoiceID & ")"
        Sql3 = "Select FixedAssetItemID, FixedAssetID, InvoiceItemID, EntryType, AssetName, AssetNumber, Qty, Amount from accListFixedAssetItem() Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql4 = "Select * from InvoiceItemGST Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql5 = "Select * from InvoiceGstCalc where invoiceid = " & InvoiceID
        Sql6 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql9 = "Select PurItemHistID, PIDUnitID, PurItemID, RecvMatVouchID, MatVouchItemID, RecdTCInQly, Dated, OrderNum, OrderDate, BasicRate,RateUnitName, QtyRecd, RecdTCInSt, QtyRej, QtyOK, QtyDevi, QtyIssue from PurListItemHist() where MatVouchID in (Select MatVouchID from MatVouch where PInvoiceID = " & InvoiceID & ")"
        Sql10 = "Select ProdSerialItemID, ProdSerialItem.ProdSerialID, InvoiceItemID, ProdSerialNum, WOInfo, LotNum from ProdSerialItem Inner join ProdSerial on ProdSerialItem.ProdSerialID = ProdSerial.ProdSerialID Inner join ProdLots on ProdSerial.ProdLotID = ProdLots.ProdLotID Inner join PIDUnit on ProdLots.PIDUnitID = PIDUnit.PIDUnitID  Where InvoiceItemID in (select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ")"
        Sql11 = "Select IDField, IDValue, IDnoteID, ODNoteID, MatVouchID, IndentID, PInvoiceID, pMatVouchID, DocType, VouchNum, VouchDate from invListMatVouchRefDoc() where PMatVouchID in (Select MatVouchID from MatVouch where PInvoiceID = " & InvoiceID & ")"
        Dim strWhere As String = myUtils.CombineWhere(False, "itemstockvalueid is null", "matvouchitemid in (select matvouchitemid from matvouchitem where matvouchid in (Select MatVouchID from MatVouch where PInvoiceID = " & InvoiceID & "))")
        Sql12 = "Select ItemStockValueBalID, ItemStockValueID, MatVouchItemID, OBDescrip, Dated, Qty, Qty2, AmountTot, AmountWV from ItemStockValueBal Where" & strWhere
        Sql13 = "Select * from PaymentItem Inner Join Payment on PaymentItem.PaymentID = Payment.PaymentID where InvoiceID = " & InvoiceID

        Sql = Sql1 & ";" & Sql2 & ";" & Sql3 & ";" & Sql4 & ";" & Sql5 & ";" & Sql6 & ";" & Sql7 & ";" & Sql8 & ";" & Sql9 & ";" & Sql10 & ";" & Sql11 & ";" & Sql12 & ";" & Sql13
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
        myUtils.AddTable(Me.dsForm, ds, "VouchItem", 0)
        myUtils.AddTable(Me.dsForm, ds, "Res", 1)
        myUtils.AddTable(Me.dsForm, ds, "FixedAssetItem", 2)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGST", 3)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceGstCalc", 4)
        myUtils.AddTable(Me.dsForm, ds, "CostLot", 5)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 6)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 7)
        myUtils.AddTable(Me.dsForm, ds, "Pur", 8)
        myUtils.AddTable(Me.dsForm, ds, "ProdSerialItem", 9)
        myUtils.AddTable(Me.dsForm, ds, "RefDocAdd", 10)
        myUtils.AddTable(Me.dsForm, ds, "OB", 11)
        myUtils.AddTable(Me.dsForm, ds, "Payment", 12)

        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), dsForm.Tables("Res"), "MatVouchItemId", "_plnReserveItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), Me.dsForm.Tables("FixedAssetItem"), "InvoiceItemID", "_FixedAssetItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), Me.dsForm.Tables("InvoiceItemGST"), "InvoiceItemID", "_InvoiceItemGST")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), Me.dsForm.Tables("CostLot"), "InvoiceItemID", "_CostLot")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), Me.dsForm.Tables("CostWBS"), "InvoiceItemID", "_CostWBS")
        myContext.Tables.SetAuto(Me.dsForm.Tables("VouchItem"), Me.dsForm.Tables("CostCenter"), "InvoiceItemID", "_CostCenter")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("MatDepID") Is Nothing Then Me.AddError("MatDepID", "Please select Department")
        If Me.SelectedRow("CustomerID") Is Nothing Then Me.AddError("CustomerID", "Please select Customer")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso Me.SelectedRow("GSTInvoiceType") Is Nothing Then Me.AddError("GSTInvoiceType", "Please select GST Invoice Type")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2C", "B2BUR", "IMPG", "IMPS")) AndAlso Me.SelectedRow("GSTInvoiceSubType") Is Nothing Then Me.AddError("GSTInvoiceSubType", "Please select GST Invoice Sub Type")

        If Not Me.SelectedRow("MatDepID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("MatDepID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")

        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")

        If Me.myView.MainGrid.myDV.Table.Select.Length > 0 And myUtils.cValTN(myRow("PriceSlabID")) = 0 Then Me.AddError("", "Please Select Pricing")
        Dim rr2() As DataRow = dsForm.Tables("VouchItem").Select("isnull(PriceSlabID,0) = 0 and InvoiceItemType Not in ('PIC', 'PIS', 'IGT','IGS')")
        If rr2.Length > 0 Then
            Me.AddError("", "Please Select Pricing for Item")
        End If

        If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then
            myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("ConsigneeID")("TaxAreaID"))
        Else
            If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 AndAlso Not Me.SelectedRow("CustomerID") Is Nothing Then myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CustomerID")("TaxAreaID"))
        End If


        If (Not Me.SelectedRow("DeliveryCampusID") Is Nothing) AndAlso (myUtils.cValTN(SelectedRow("DeliveryCampusID")("SalesOrderID"))) > 0 Then
            If myUtils.cValTN(SelectedRow("DeliveryCampusID")("SalesOrderID")) <> myUtils.cValTN(myRow("SalesOrderID")) Then Me.AddError("", "Please Select Correct Sales Order")
        End If

        If Me.SelectedRow("POSTaxAreaID") Is Nothing Then Me.AddError("POSTaxAreaID", "Please select Place of Supply.")

        Dim oRet = myFuncs.CheckZeroTaxType(myUtils.cStrTN(myRow("GSTInvoiceSubType")), dsForm.Tables("InvoiceItemGST"))
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not oRet.Success) Then Me.AddError("", oRet.Message)


        If myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("VouchItem"), "InvoiceItemID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct %.")
        Return Me.CanSave
    End Function

    Private Sub SetPaymentData(r1 As DataRow)
        r1("DocType") = "PC"
        r1("PaymentType") = "T"
        r1("CompanyID") = Me.SelectedRow("MatDepID")("CompanyID")
        r1("PostPeriodID") = myRow("PostPeriodID")
        r1("NewAmount") = r1("AmountTotPay")
        r1("Amount") = r1("AmountTotPay")
        r1("NewTDSAmount") = r1("TDSAmount")
        r1("NewWCTAmount") = r1("WCTAmount")
        r1("Dated") = myRow("PostingDate")
        r1("PostingDate") = myRow("PostingDate")
        r1("DivisionID") = myRow("DivisionID")
        r1("CustomerID") = myRow("CustomerID")
    End Sub

    Public Overrides Function VSave() As Boolean
        Dim Oret, Oret1 As clsProcOutput
        VSave = False

        If Me.Validate Then
            SetPaymentData(dsForm.Tables("Payment").Rows(0))

            If myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("InvoiceNum", "Accounting entry passed. Invoice can't be update.")
            End If

            If myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(dsForm.Tables("Payment").Rows(0)("PaymentID"))) Then
                Me.AddError("InvoiceNum", "Payment done. Invoice can't be update.")
            End If


            myFuncs.SetPreGST(myContext, myRow.Row)
            If myUtils.IsInList(myUtils.cStrTN(myRow("P_gst")), "y") Then
                myRow("GSTInvoiceType") = DBNull.Value
                myRow("GSTInvoiceSubType") = DBNull.Value
            End If

            Dim objIVProcFico As New clsIVProcFICO("IS", myContext)
            objIVProcFico.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))

            myRow("CampusID") = Me.SelectedRow("MatDepID")("CampusID")

            If Me.SelectedRow("CampusID") Is Nothing OrElse objIVProcFico.IsVouchDateAfterFinStart(myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), myRow("PostingDate")) = False Then
                Me.AddError("PostingDate", "Posting Date can not be less then Company Start Date.")
            End If

            myRow("DocType") = objIVProcFico.DocType
            myRow("TY") = myFuncs.SetTY(dsForm.Tables("VouchItem"))
            myRow("VouchDate") = myRow("PostingDate")

            If objIVProcFico.GetInvoiceTypeID(myRow.Row) = False Then Me.AddError("", "Combination Not Available")

            objIVProcFico.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("VouchItem"))
            Oret = objIVProcFico.CheckBalance()
            If Not Oret.Success Then Me.AddError("", Oret.Message)


            Oret = objIVProcFico.CheckChildren(Me.dsForm.Tables("VouchItem"))
            If Oret.Success = False Then Me.AddError("", Oret.Message)

            Dim oProc As New clsGSTInvoiceSale(myContext)
            oProc.CalcVouchActionRP(Me.SelectedRow("CampusID")("gstregid"), myRow("postperiodid"), myRow.Row)

            Dim ObjPaymentCust As New clsPaymentCustomer(myContext)
            ObjPaymentCust.LoadVouch(myUtils.cValTN(dsForm.Tables("Payment").Rows(0)("PaymentID")))

            Dim ObjMatVouchProc As New clsMatVouchProc(myContext)
            SetBRAmount(dsForm.Tables("VouchItem"))
            Me.PopulateBOMMatDepID(ObjMatVouchProc.oMasterMM, myRow("MatDepID"))
            ObjMatVouchProc.LoadMultiVouch(myUtils.cValTN(myRow("MatVouchId")))
            ObjMatVouchProc.LoadVouch(myRow.Row.Table, dsForm.Tables("VouchItem"))
            ObjMatVouchProc.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("VouchItem"))
            Oret = ObjMatVouchProc.CheckBalance(dsForm.Tables("VouchItem"))
            If Oret.Success = False Then Me.AddError("", Oret.Message)

            If Me.CanSave Then
                Dim DocNumSysType As String = myFuncs.GetDocNumSysType(myContext, myUtils.cValTN(myRow("InvoiceTypeID")))
                Dim ObjVouch As New clsVoucherNum(myContext)
                ObjVouch.GetNewSerialNo("InvoiceID", DocNumSysType, myRow.Row)
                ObjVouch.GetNewSerialNo("MatVouchID", Me.SelectedRow("MatVouchTypeID")("VouchTypeCode"), myRow.Row)
                ObjVouch.GetNewSerialNo("PaymentID", "PC", dsForm.Tables("Payment").Rows(0))


                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))

                Dim objPricingCalcBase As New clsPricingCalcBase(myContext)
                objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("VouchItem"))
                objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "AmountWV", "")
                For Each r2 As DataRow In dsForm.Tables("MatVouchItem").Select()
                    objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "AmountBasic", "")
                Next
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
                    myContext.Data.SaveMulti(dicSQL, myRow.Row, frmIDX)
                    frmIDX = myRow("InvoiceID")

                    myRow("PInvoiceID") = frmIDX
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "select matvouchid, pinvoiceid from matvouch where 0=1")

                    Dim dicSQL2 As New clsCollecString(Of String)
                    dicSQL2.Add("MatVouchitemid", "Select MatVouchItemID, MatVouchID, ItemID,PriceSlabID, AmountTrWv, PIDUnitID, VarNum, MvtCode, SpStock, ClassType, StockStage, StockStage2, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, ForMatDepID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, NewPIDUnitID, NewVarNum, AmountWV, FixedAssetID, pMatVouchItemID, MfgCharges, ReserveGRBehave, ReserveGIBehave, ValuationClass, OdNoteItemID, PPSubType, QtyEntry, BasicRate, AmountBasic, AmountTot, Remark, Hsn_Sc, TaxCredit from MatVouchItem Where matvouchID  = " & myRow("matvouchid"))
                    dicSQL2.Add("Invoiceitemid", "Select InvoiceItemID,MatVouchItemID, InvoiceID, VarNum, PIDUnitID, ItemID, ClassType,TransType, ValuationClass, PriceSlabID, PPSubType, AmountBasic, InvoiceItemType, Description, QtyRate,  BasicRate, AmountTot, AmountWV,ReturnFY,HSN_SC,ItemUnitID,TaxCredit from InvoiceItem Where InvoiceID  = " & myRow("invoiceid"))
                    myContext.Data.SaveMulti(dicSQL2, dsForm.Tables("VouchItem"), "InvoiceID=" & frmIDX & ", matvouchID  = " & myRow("matvouchid"))


                    myUtils.ChangeAll(dsForm.Tables("FixedAssetItem").Select, "EntryType=I")
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("FixedAssetItem"), "Select FixedAssetItemID, FixedAssetID, InvoiceItemID, EntryType, Qty, Amount from FixedAssetItem", True)

                    If Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("P_gst")), "Y") Then
                        Dim InvoiceItemID As String = myUtils.MakeCSV(dsForm.Tables("VouchItem").Select("InvoiceItemType Not in ('PIC','PIS','IGT')"), "InvoiceItemID")
                        myUtils.ChangeAll(dsForm.Tables("InvoiceItemGST").Select, "InvoiceID=" & frmIDX)
                        Dim rCDN = oProc.GetFirstRow(Me.DatasetCollection, "cdninv")
                        oProc.PopulateCalc(frmIDX, myRow.Row, Me.SelectedRow("CampusID"), dsForm.Tables("InvoiceItemGST"), dsForm.Tables("InvoiceGstCalc"), rCDN, Nothing, Me.dsCombo)
                        myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "InvoiceID=" & frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGST"), "Select * from InvoiceItemGST", True, "InvoiceItemID in (" & InvoiceItemID & ")")
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")
                    End If

                    Dim dicSQL3 As New clsCollecString(Of String)
                    dicSQL3.Add("PaymentID", "Select * from Payment where paymentid in (select paymentid from paymentitem where InvoiceID = " & myRow("invoiceid") & ")")
                    dicSQL3.Add("PaymentItemID", "Select PaymentID, InvoiceID, PaymentItemType, Amount, TDSAmount, WCTAmount from PaymentItem Where InvoiceID = " & myRow("invoiceid"))
                    myContext.Data.SaveMulti(dicSQL3, dsForm.Tables("Payment"), "InvoiceID=" & frmIDX & "")


                    Dim str As String = "Update PaymentItem set PaymentID = " & myUtils.cValTN(dsForm.Tables("Payment").Rows(0)("PaymentID")) & " where InvoiceID =  " & myRow("InvoiceID")
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, str)


                    ObjMatVouchProc.UpdateBalance()

                    ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)

                    objPricingCalcBase.VSave()

                    Oret1 = ObjPaymentCust.HandleWorkflowState(dsForm.Tables("Payment").Rows(0))
                    Oret = objIVProcFico.HandleWorkflowState(myRow.Row)
                    If Oret.Success AndAlso Oret1.Success Then
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
            Case "pidunit"
                Dim sql As String = "select * from plnlistpidunit() where pidunitid = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
        End Select
        Return oRet
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "sofixedasset"
                    Dim TransType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@transtype", Params))
                    Dim ValuationClass As String = myUtils.cStrTN(myContext.SQL.ParamValue("@valuationclass", Params))
                    Dim NewFixedAssetIDcsv As String = myUtils.cStrTN(myContext.SQL.ParamValue("@newfixedassetcsv", Params))
                    Dim OldFixedAssetIDcsv As String = myUtils.cStrTN(myContext.SQL.ParamValue("@oldfixedassetcsv", Params))
                    Dim oAssetProc As New clsFixedAssetProc(myContext)
                    Dim strXML As String = oAssetProc.ModRowXML(TransType, ValuationClass, NewFixedAssetIDcsv, OldFixedAssetIDcsv)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""FIXEDASSETID""/>", True, , strXML)
                Case "salesorder"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("MainPartyID = @MainPartyID and CompanyID = @CompanyId and SalesOrderID Not in (@SalesOrderID) and (isnull(OrderDate,'3099-Jan-01') <= '@InvoiceDate' or isnull(LOIDate,'3099-Jan-01') <= '@InvoiceDate')", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SalesOrderID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
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
