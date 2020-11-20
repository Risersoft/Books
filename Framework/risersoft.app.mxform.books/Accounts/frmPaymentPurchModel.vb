Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmPaymentPurchModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim myViewFASO, myViewAdv, myViewCostLot, myViewCostWBS, myViewCostCenter As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
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

        sql = "Select CampusID, DispName, CompanyID,CampusType,TaxAreaCode, DivisionCodeList, WODate, CompletedOn, TaxAreaID, GstRegID, CampusCode from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("CashCampusID", "Campus")
        Me.AddLookupField("DeliveryCampusID", "Campus")

        sql = "SELECT  VendorID, VendorName, VendorClass,TaxAreaCode, GSTIN, ImportAllow FROM  purListVendor() where isnull(GSTIN,'') = ''  Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select BankAccountID, AccountName, GlAccountId, companyid, ShortName from BankAccount"
        Me.AddLookupField("BankAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BankAccount").TableName)

        Dim str1 As String = myUtils.CombineWhereOR(False, "isnull(imprestenabled,0)=1", "employeeid in (select imprestemployeeid from payment)")
        sql = "Select employeeid, empcode, fullname, onrolls, companyid,JoinDate,LeaveDate,IgnoreExpenseVoucher from hrpListAllEmp() where " & str1 & " order by fullname"
        Me.AddLookupField("ImprestEmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "emp").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "Mode", "")
        Me.AddLookupField("PaymentMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentMode").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Material', 'Asset', 'Service') and CodeType <> 'Type' and CodeWord in ('APN', 'APU', 'ARO', 'ARW', 'RM','CG','CT','ST','WIP','FG','CC','NC','CA','NA','Exp')  Order by CodeClass "
        Me.AddLookupField("PaymentItemTrans", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransType").TableName)

        sql = "Select Class as ClassCode, Class, ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass Where ClassType = 'M' or (ClassType = 'S' and ClassSubType in ('P','B')) or ClassType = 'A' Order by Class"
        Me.AddLookupField("PaymentItemTrans", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "(CodeWord = 'A' or CodeWord = 'M' or CodeWord = 'S')")
        Me.AddLookupField("PaymentItemTrans", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        sql = "select TaxAreaID, Descrip,TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", "Tag2 = 'IP'")
        Me.AddLookupField("GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

        sql = "Select Code, Code + '-' + Description as Description, Ty from HsnSac Order by Code"
        Me.AddLookupField("Hsn_Sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "GSTTypecode", "Tag2= 'IP'")
        Me.AddLookupField("GSTInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxCredit", "")
        Me.AddLookupField("TaxCredit", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxCredit").TableName)

        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("PaymentItemTrans", "ItemUnitID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

        sql = "Select * from systemoptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim objTourVouchProc As New clsTourVouchProc(myContext), objPaymentPurch As New clsPaymentFICO("PPU", myContext)
        Dim objPricingCalcBase As New clsPricingCalcBase(myContext)
        Dim sql As String
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Payment Where PaymentID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
            myRow("PostingDate") = Now.Date
            myRow("GSTInvoiceType") = "B2BUR"
        Else
            Dim rPostPeriod As DataRow = objPaymentPurch.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Me.BindDataTable(myUtils.cValTN(prepIDX))
        objPricingCalcBase.InitData(myRow.Row, "PaymentID", myUtils.cValTN(frmIDX), "PostingDate", "PaymentItemTransId", "QtyRate", Me.dsForm.Tables("PaymentItemTrans"))

        objPaymentPurch.LoadVouch(myUtils.cValTN(myRow("PaymentID")))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "2-1-1-1", True)
        Dim str1 As String = "<BAND INDEX = ""0"" TABLE = ""PaymentItemTrans"" IDFIELD=""PaymentItemTransID""><COL KEY =""PaymentID, ClassType, TransType, ValuationClass, PriceSlabID, QtyRate, Description, UnitName,BasicRate, AmountBasic,AmountTot, AmountWV, PPSubType,HSN_SC,TaxCredit,ItemUnitID""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        Dim dtFA As DataTable = dsForm.Tables("FixedAssetItem")
        myViewFASO.MainGrid.BindGridData(dsForm, dtFA.DataSet.Tables.IndexOf(dtFA))
        myViewFASO.MainGrid.QuickConf("", True, "2-1-1")
        str1 = "<BAND INDEX = ""0"" TABLE = ""FixedAssetItem"" IDFIELD=""FixedAssetItemID""><COL KEY ="" FixedAssetID, InvoiceItemID, PaymentItemTransID, Qty ""/><COL KEY=""AMOUNT"" NOEDIT=""True""/></BAND>"
        myViewFASO.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        sql = "Select TourVouchItemID, PaymentID, AdvanceVouchID,OpenAmountAdj,AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & frmIDX & " and AdvanceVouchID is Not NULL"
        myViewAdv.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount""/></BAND>"
        myViewAdv.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        objTourVouchProc.PopulatePreBalanceAdv(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID", frmIDX, "AdvanceVouchID")


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

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))

        objPricingCalcBase.CheckPriceSlabElement()
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal PaymentID As Integer)
        Dim ds As DataSet, Sql, Sql1, Sql2, Sql3, Sql5, Sql6, Sql7, Sql8 As String

        Sql1 = "Select PaymentItemTransID, PaymentID, ClassType, ItemUnitID, TransType,HSN_SC, ValuationClass, PriceSlabID, UnitName,BasicRate, AmountBasic,TaxCredit, PPSubType, Description, QtyRate,AmountTot, AmountWV from PaymentItemTrans" &
               " Where PaymentID = " & PaymentID & " "
        Sql2 = "Select FixedAssetItemID, FixedAssetID, PaymentItemTransID, EntryType, AssetName, AssetNumber, Qty, Amount from accListFixedAssetItem() Where PaymentItemTransID in (Select PaymentItemTransID from PaymentItemTrans where PaymentID = " & PaymentID & ") "
        Sql3 = "Select * from InvoiceItemGST Where PaymentItemTransID in (Select PaymentItemTransID from PaymentItemTrans where PaymentID = " & PaymentID & ") "
        Sql5 = "Select * from InvoiceGstCalc where PaymentID = " & PaymentID
        Sql6 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as PaymentItemTransID, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'PaymentID' and PIDValue = " & PaymentID & ""
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as PaymentItemTransID, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'PaymentID' and PIDValue = " & PaymentID & ""
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as PaymentItemTransID, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'PaymentID' and PIDValue = " & PaymentID & ""

        Sql = Sql1 & ";  " & Sql2 & ";  " & Sql3 & ";  " & Sql5 & ";" & Sql6 & ";" & Sql7 & ";" & Sql8
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "PaymentItemTrans", 0)
        myUtils.AddTable(Me.dsForm, ds, "FixedAssetItem", 1)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGST", 2)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceGstCalc", 3)
        myUtils.AddTable(Me.dsForm, ds, "CostLot", 4)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 5)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 6)
        myContext.Tables.SetAuto(Me.dsForm.Tables("PaymentItemTrans"), Me.dsForm.Tables("FixedAssetItem"), "PaymentItemTransID", "_FixedAssetItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("PaymentItemTrans"), Me.dsForm.Tables("InvoiceItemGST"), "PaymentItemTransID", "_InvoiceItemGST")
        myContext.Tables.SetAuto(Me.dsForm.Tables("PaymentItemTrans"), Me.dsForm.Tables("CostLot"), "PaymentItemTransID", "_CostLot")
        myContext.Tables.SetAuto(Me.dsForm.Tables("PaymentItemTrans"), Me.dsForm.Tables("CostWBS"), "PaymentItemTransID", "_CostWBS")
        myContext.Tables.SetAuto(Me.dsForm.Tables("PaymentItemTrans"), Me.dsForm.Tables("CostCenter"), "PaymentItemTransID", "_CostCenter")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If Not Me.SelectedRow("CampusID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("Dated"), PPFinal)
        End If

        If Not Me.SelectedRow("VendorID") Is Nothing Then
            If myRow("VouchNum").ToString.Trim.Length = 0 Then Me.AddError("VouchNum", "Enter Invoice No.")
        End If

        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")

        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")
        myFuncs.ValidatePaymentMode(Me)

        If myUtils.cValTN(myRow("PriceSlabID")) = 0 Then Me.AddError("", "Please Select Pricing")
        Dim rr2() As DataRow = dsForm.Tables("PaymentItemTrans").Select("isnull(PriceSlabID,0) = 0")
        If rr2.Length > 0 Then
            Me.AddError("VouchNum", "Please Select Pricing for Item")
        End If


        Dim oRet = myFuncs.CheckZeroTaxType(myUtils.cStrTN(myRow("GSTInvoiceSubType")), dsForm.Tables("InvoiceItemGST"))
        If myRow("Dated") >= myFuncs.GSTLounchDate AndAlso (Not oRet.Success) Then Me.AddError("", oRet.Message)

        Dim rr() As DataRow = dsForm.Tables("PaymentItemTrans").Select("TaxCredit = 'Y'")
        If rr.Length > 0 Then
            If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor Name")
        End If

        If myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("PaymentItemTrans"), "PaymentItemTransID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct %.")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        Dim Oret As clsProcOutput
        VSave = False

        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))) Then
                Me.AddError("VouchNum", "Accounting entry passed. Voucher can't be update.")
            End If
            Dim objPaymentPurch As New clsPaymentFICO("PPU", myContext)
            objPaymentPurch.LoadVouch(myUtils.cValTN(myRow("PaymentID")))
            myRow("DocType") = objPaymentPurch.DocType
            myRow("CompanyID") = myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID"))
            If Not Me.SelectedRow("DeliveryCampusID") Is Nothing Then
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("DeliveryCampusID")("TaxAreaID"))
            Else
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CampusID")("TaxAreaID"))
            End If
            If myRow("Dated") < myFuncs.GSTLounchDate Then
                myRow("P_gst") = "Y"
                myRow("GSTInvoiceType") = DBNull.Value
            Else
                myRow("P_gst") = "N"
            End If
            objPaymentPurch.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("PaymentItemTrans"))
            Oret = objPaymentPurch.CheckBalance()
            If Not Oret.Success Then Me.AddError("", Oret.Message)

            If Me.CanSave Then
                If Me.SelectedRow("VendorID") Is Nothing Then
                    myRow("VouchNum") = DBNull.Value
                    Dim ObjVouch As New clsVoucherNum(myContext)
                    ObjVouch.GetNewSerialNo("PaymentID", "PPU", myRow.Row)
                End If

                Dim oProc As New clsGSTPaymentPurch(myContext)
                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("VouchNum"), myUtils.cValTN(myRow("isamendment")))

                Dim objPricingCalcBase As New clsPricingCalcBase(myContext)
                objPricingCalcBase.InitData(myRow.Row, "PaymentID", myUtils.cValTN(frmIDX), "PostingDate", "PaymentItemTransId", "QtyRate", Me.dsForm.Tables("PaymentItemTrans"))
                objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTotPay", "", "")
                For Each r2 As DataRow In dsForm.Tables("PaymentItemTrans").Select()
                    objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "AmountBasic", "")
                Next
                objPricingCalcBase.PopulateAccountingKeys(myRow("postingdate"))

                If myUtils.cValTN(myRow("TDSAmount")) > 0 AndAlso myUtils.cValTN(myRow("TDSAmount")) >= myUtils.cValTN(myRow("AmountTotPay")) Then Me.AddError("", "TDSAmount should be less than Total Payment Amount")

                If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "IM") AndAlso myFuncs.IgnoreExpenseVoucher(myContext, myUtils.cValTN(myRow("ImprestEmployeeID"))) = False Then
                    If myUtils.cValTN(myViewAdv.MainGrid.GetColSum("Amount")) <> myUtils.cValTN(myRow("AmountTotPay")) Then Me.AddError("", "Advance Amount should be equal to Payment Amount.")
                End If

                If Me.CanSave Then
                    Dim PaymentDescrip As String = myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy")

                    Try
                        myContext.CommonData.GetDatasetFYComp(False)
                        myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Payment Where PaymentID = " & frmIDX)
                        frmIDX = myRow("PaymentID")

                        myUtils.ChangeAll(dsForm.Tables("PaymentItemTrans").Select, "PaymentID=" & frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PaymentItemTrans"), "Select PaymentItemTransID, PaymentID, ClassType, TransType, ValuationClass, PriceSlabID, QtyRate, Description, UnitName,BasicRate, AmountBasic,AmountTot, AmountWV, PPSubType,HSN_SC,TaxCredit,ItemUnitID from PaymentItemTrans")
                        myUtils.ChangeAll(dsForm.Tables("FixedAssetItem").Select, "EntryType=P")
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("FixedAssetItem"), "Select FixedAssetItemID, FixedAssetID, PaymentItemTransID, EntryType, Qty, Amount from FixedAssetItem", True)


                        If Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("P_gst")), "Y") Then
                            myUtils.ChangeAll(dsForm.Tables("InvoiceItemGST").Select, "PaymentID=" & frmIDX)
                            myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "PaymentID=" & frmIDX)

                            oProc.PopulateCalc(frmIDX, myRow.Row, Me.SelectedRow("CampusID"), dsForm.Tables("InvoiceItemGST"), dsForm.Tables("InvoiceGstCalc"), Nothing, Nothing, Me.dsCombo)

                            myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGST"), "Select * from InvoiceItemGST", True)
                            myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")
                        End If

                        myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID=" & frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(myViewAdv.MainGrid.myDS.Tables(0), "Select TourVouchItemID, PaymentID, AdvanceVouchID, Amount from TourVouchItem")

                        ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                        ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                        ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)

                        objPricingCalcBase.VSave()

                        Oret = objPaymentPurch.HandleWorkflowState(myRow.Row)
                        If Oret.Success Then
                            frmMode = EnumfrmMode.acEditM
                            myContext.Provider.dbConn.CommitTransaction(PaymentDescrip, frmIDX)
                            VSave = True
                        Else
                            myContext.Provider.dbConn.RollBackTransaction(PaymentDescrip, Oret.Message)
                            Me.AddError("", Oret.Message)
                        End If
                    Catch e As Exception
                        myContext.Provider.dbConn.RollBackTransaction(PaymentDescrip, e.Message)
                        Me.AddException("", e)
                    End Try
                End If
            End If
        End If
    End Function

    Private Sub ChangeColRowwise(dt As DataTable, PaymentID As Integer)
        myUtils.ChangeAll(dt.Select, "PIDField=PaymentID")
        myUtils.ChangeAll(dt.Select, "PIDValue=" & PaymentID)
        myUtils.ChangeAll(dt.Select, "pIDFieldItem=PaymentItemTransID")

        For Each r1 In dt.Select
            r1("pIDValueItem") = r1("PaymentItemTransID")
        Next
    End Sub

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
                Case "tourvouch"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("CompanyID  = @companyid and EmployeeID = @employeeid and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TA""/>", True, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
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
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ID", Params))
        Select Case DataKey.Trim.ToLower
            Case "generateprebal"
                Dim objTourVouchProc As New clsTourVouchProc(myContext)
                objTourVouchProc.PopulatePreBalanceAdv(dt.Select, "PaymentID", ID, "TourVouchID")
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
