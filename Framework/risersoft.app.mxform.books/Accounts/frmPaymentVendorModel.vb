Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmPaymentVendorModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim myViewWR, myViewRR, myViewDR, myViewRW, myViewAdv, myViewDetPI, myViewDetDR, myViewPP As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("PI")
        myViewWR = Me.GetViewModel("WR")
        myViewRR = Me.GetViewModel("RR")
        myViewDR = Me.GetViewModel("DR")
        myViewRW = Me.GetViewModel("RW")
        myViewAdv = Me.GetViewModel("Adv")
        myViewDetPI = Me.GetViewModel("DetPI")
        myViewDetDR = Me.GetViewModel("DetDR")
        myViewPP = Me.GetViewModel("PP")
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

        sql = "Select CompanyID, CompName,FinStartDate  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = "SELECT  VendorID, VendorName, VendorClass FROM  purListVendor() Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "InvType", "")
        Me.AddLookupField("PaymentType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentType").TableName)

        sql = "Select CampusID, DispName, CompanyID, WODate,CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CashCampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select BankAccountID, AccountName, GlAccountId, companyid, ShortName from BankAccount"
        Me.AddLookupField("BankAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BankAccount").TableName)

        Dim str1 As String = myUtils.CombineWhereOR(False, "isnull(imprestenabled,0)=1", "employeeid in (select imprestemployeeid from payment)")
        sql = "Select employeeid, empcode, fullname, onrolls, companyid,JoinDate,LeaveDate, IgnoreExpenseVoucher from hrpListAllEmp() where " & str1 & " order by fullname"
        Me.AddLookupField("ImprestEmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "emp").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "Mode", "Codeword <> 'WO'")
        Me.AddLookupField("PaymentMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentMode").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String, ObjPaymentVendor As New clsPaymentVendor(myContext)
        Dim objTourVouchProc As New clsTourVouchProc(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Payment Where PaymentID = " & prepIDX
        Me.InitData(sql, "InvoiceID,PaymentType", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
        Else
            Dim rPostPeriod As DataRow = ObjPaymentVendor.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        ObjPaymentVendor.LoadVouch(myUtils.cValTN(myRow("PaymentID")))

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myView.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountCess"" CAPTION=""CESS Deduct"" NOEDIT=""True""/><COL KEY=""AmountEXV"" CAPTION=""Exchange Rate""/><COL KEY=""InvoiceTypeCode"" CAPTION=""Invoice Type"" NOEDIT=""True""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/>"
        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1-1", True)

        myViewWR.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID, PaymentItemType, InvoiceTypeCode, BillOf, DocType,AmountDiscount, Amount, Fac, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWO, AmountPen, TDSAmount,WCTAmount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'WR'"
        myViewWR.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewWR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewRR.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID, PaymentItemType, InvoiceTypeCode, BillOf, DocType, Fac, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, Amount, TDSAmount,WCTAmount, AmountDiscount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'RR'"
        myViewRR.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewRR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDR.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewDR.MainGrid.BindGridData(Me.dsForm, 2)
        myViewDR.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountCess"" CAPTION=""CESS Deduct"" NOEDIT=""True""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountRet"" CAPTION=""Retention""/><COL KEY=""InvoiceTypeCode"" CAPTION=""Invoice Type"" NOEDIT=""True""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewDR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewRW.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID, PaymentItemType, InvoiceTypeCode, BillOf, DocType, TDSAmount,WCTAmount, Amount, Fac, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWO, AmountPen, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'RW'"
        myViewRW.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/></BAND>"
        myViewRW.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewAdv.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = "Select TourVouchItemID, PaymentID, AdvanceVouchID,OpenAmountAdj, AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & frmIDX & " and AdvanceVouchID is Not NULL"
        myViewAdv.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount""/></BAND>"
        myViewAdv.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDetPI.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewDetPI.MainGrid.BindGridData(Me.dsForm, 3)
        myViewDetPI.MainGrid.QuickConf("", True, "1-1", True)
        sql = "Select Class from AccountClass where InDeduction = 1 Order By Class"
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemDet"" IDFIELD=""PaymentItemDetID""><COL KEY =""PaymentItemDetID, PaymentItemID, Amount""/><COL KEY=""ValuationClass""  CAPTION=""Valuation Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewDetPI.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDetDR.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewDetDR.MainGrid.BindGridData(Me.dsForm, 4)
        myViewDetDR.MainGrid.QuickConf("", True, "1-1", True)
        sql = "Select Class from AccountClass where InDeduction = 1 Order By Class"
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemDet"" IDFIELD=""PaymentItemDetID""><COL KEY =""PaymentItemDetID, PaymentItemID, Amount""/><COL KEY=""ValuationClass""  CAPTION=""Valuation Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewDetDR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewPP.MainGrid.MainConf("formatxml") = "<COL KEY=""AdvVouchNum"" CAPTION=""Voucher No.""/><COL KEY=""AdvDated"" CAPTION=""Dated""/><COL KEY=""AdvPaymentInfo"" CAPTION=""PaymentInfo""/><COL KEY=""AdvPaymentType"" CAPTION=""Payment Type""/><COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewPP.MainGrid.BindGridData(Me.dsForm, 5)
        myViewPP.MainGrid.QuickConf("", True, "1-1-1-2-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, AdvancePaymentID, InvoiceID, Fac, PaymentItemType, Amount""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewPP.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        If myUtils.cValTN(Me.vBag("InvoiceID")) > 0 AndAlso frmMode = EnumfrmMode.acAddM Then
            GenerateInvoice(myUtils.cValTN(Me.vBag("InvoiceID")))
        End If

        ObjPaymentVendor.PopulatePreBalanceDue(myView.MainGrid.myDV.Table.Select, frmIDX, myRow("Dated"))
        ObjPaymentVendor.PopulatePreBalanceWR(myViewWR.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("Dated"))
        ObjPaymentVendor.PopulatePreBalanceRetained(myViewRR.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("Dated"))
        ObjPaymentVendor.PopulatePreBalanceDue(myViewDR.MainGrid.myDV.Table.Select, frmIDX, myRow("Dated"))
        ObjPaymentVendor.PopulatePreBalanceRetained(myViewRW.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("Dated"))
        objTourVouchProc.PopulatePreBalanceAdv(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID", frmIDX, "AdvanceVouchID")
        ObjPaymentVendor.PopulatePreBalancePP(myViewPP.MainGrid.myDV.Table.Select, frmIDX, myRow("Dated"))

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal PaymentID As Integer)
        Dim ds As DataSet, Sql1, Sql2, Sql3, Sql4, Sql, Sql5 As String

        Sql1 = " Select PaymentItemID, PaymentID, InvoiceID, PaymentItemType, BillOf, DocType, Fac, PostingDateIV as PostingDate, InvoiceTypeCode, InvoiceNum, InvoiceDate, 0.0 as PreBalance, Amount, AmountEXV, TDSAmount,WCTAmount, AmountCess, AmountDiscount, 0.0 as Balance from AccListPaymentItem() Where PaymentID = " & PaymentID & " and PaymentItemType = 'PI' "
        Sql2 = " Select PaymentItemID, PaymentID, InvoiceID, PaymentItemType, BillOf, DocType, WCTAmount, Amount, Fac, PostingDateIV as PostingDate, InvoiceTypeCode, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWo, AmountPen, AmountRet, TDSAmount, AmountCess, AmountDiscount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & PaymentID & " and PaymentItemType = 'DR'"
        Sql3 = " Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet where PaymentItemID in (Select PaymentItemID from PaymentItem where PaymentID = " & PaymentID & " and paymentitemtype='PI')"
        Sql4 = " Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet where PaymentItemID in (Select PaymentItemID from PaymentItem where PaymentID = " & PaymentID & " and paymentitemtype='DR')"
        Sql5 = " Select PaymentItemID, PaymentID, AdvancePaymentID, InvoiceID, OpenAmountAdj, NewAmount, PaymentItemType, Fac, AdvPaymentType, AdvVouchNum, AdvDated, AdvPaymentInfo, 0.00 as PreBalance, AmountWo, AmountPen, AmountDiscount, Amount, 0.00 as Balance from AccListPaymentItem() Where PaymentID = " & PaymentID & " and PaymentItemType = 'PP' "

        Sql = Sql1 & "; " & Sql2 & "; " & Sql3 & "; " & Sql4 & "; " & Sql5
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "PI", 0)
        myUtils.AddTable(Me.dsForm, ds, "DR", 1)
        myUtils.AddTable(Me.dsForm, ds, "DetPI", 2)
        myUtils.AddTable(Me.dsForm, ds, "DetDR", 3)
        myUtils.AddTable(Me.dsForm, ds, "PP", 4)

        myContext.Tables.SetAuto(Me.dsForm.Tables("PI"), Me.dsForm.Tables("DetPI"), "PaymentItemID", "_DetPI")
        myContext.Tables.SetAuto(Me.dsForm.Tables("DR"), Me.dsForm.Tables("DetDR"), "PaymentItemID", "_DetDR")
    End Sub

    Private Sub GenerateInvoice(InvoiceID As Integer)
        Dim Sql As String = "Select InvoiceID, DivisionID, VendorID, CompanyID, BillOf, DocType, PostingDate, InvoiceTypeCode, InvoiceNum, InvoiceDate  from AccListInvoice() Where InvoiceID = " & myUtils.cValTN(InvoiceID)
        Dim rr1() As DataRow = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0).Select
        If rr1.Length > 0 Then
            myRow("DivisionID") = myUtils.cValTN(rr1(0)("DivisionID"))
            myRow("VendorID") = myUtils.cValTN(rr1(0)("VendorID"))
            myRow("CompanyID") = myUtils.cValTN(rr1(0)("CompanyID"))
            Dim r2 As DataRow = myUtils.CopyOneRow(rr1(0), myView.MainGrid.myDV.Table)
        End If
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("CompanyID") Is Nothing Then Me.AddError("CompanyID", "Please select Company")
        If Me.SelectedRow("Divisionid") Is Nothing Then Me.AddError("Divisionid", "Please Select Division")
        If Me.SelectedRow("PaymentType") Is Nothing Then Me.AddError("PaymentType", "Please select Payment Type")
        If Me.SelectedRow("VendorId") Is Nothing Then Me.AddError("VendorId", "Please select Vendor")
        If myUtils.cValTN(Me.myRow("NewAmount")) < 0 Then Me.AddError("NewAmount", "Advance Amount can not be less than zero")
        If myUtils.cValTN(Me.myRow("NewTDSAmount")) < 0 Then Me.AddError("NewTDSAmount", "Advance TDS Amount can not be less than zero")
        If myUtils.cValTN(Me.myRow("NewWCTAmount")) < 0 Then Me.AddError("NewWCTAmount", "Advance GST-TDS can not be less than zero")
        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) < myUtils.cValTN(Me.myRow("TDSAmount")) Then Me.AddError("TDSAmount", "'TDS Base Amount' should be greater than 'TDS Amount'")
        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) > 0 AndAlso myUtils.cValTN(Me.myRow("TDSAmount")) = 0 Then
            Me.AddError("TDSAmount", "Please enter 'TDS Amount' or Remove 'TDS Base Amount'")
        End If

        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("PaymentType")), "S") AndAlso myUtils.cValTN(myRow("AmountTotPay")) <> 0 Then Me.AddError("AmountTotPay", "Debit Amount Should be Equal to Credit Amount.")

        If Not Me.SelectedRow("CompanyID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.myRow("CompanyID")), Me.myRow("Dated"), PPFinal)

            If myRow("Dated") >= Me.SelectedRow("CompanyID")("FinStartDate") Then
                If myUtils.IsInList(myUtils.cStrTN(Me.myRow("PaymentType")), "T") Then myFuncs.ValidatePaymentMode(Me)
            End If
        End If

        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")
        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")

        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "IM") AndAlso myFuncs.IgnoreExpenseVoucher(myContext, myUtils.cValTN(myRow("ImprestEmployeeID"))) = False Then
            If myUtils.cValTN(myViewAdv.MainGrid.GetColSum("Amount")) <> myUtils.cValTN(myRow("AmountTotPay")) Then Me.AddError("", "Please enter Advance for Expenses and Advance amount should be equal to Payment Amount.")
        End If

        If myUtils.cValTN(myRow("AmountTotPay")) < 0 Then
            Me.AddError("AmountTotPay", "Amount can not be Less then Zero.")
        End If

        Me.myViewPP.MainGrid.CheckValid("Amount")


        If Not myFuncs.CheckColumnAmount(myView.MainGrid.myDV.Table, {"Amount", "TDSAmount", "WCTAmount", "AmountCess", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.") ' Not Included in Validation (AmountDiscount, AmountEXV)
        If Not myFuncs.CheckColumnAmount(myViewWR.MainGrid.myDS.Tables(0), {"AmountWO", "AmountPen", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Amount can not be Less then Zero")
        If Not myFuncs.CheckColumnAmount(myViewRR.MainGrid.myDS.Tables(0), {"Amount", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Amount can Not be Less Then Zero") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewDR.MainGrid.myDV.Table, {"AmountWo", "AmountPen", "AmountRet", "TDSAmount", "AmountCess", "Balance"}) Then Me.AddError("", "Amount can Not be Less Then Zero") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewRW.MainGrid.myDS.Tables(0), {"AmountWO", "AmountPen", "Balance"}) Then Me.AddError("", "Amount can Not be Less then Zero")
        If Not myFuncs.CheckColumnAmount(myViewAdv.MainGrid.myDS.Tables(0), {"Amount", "Balance"}) Then Me.AddError("", "Amount can Not be Less then Zero")
        If Not myFuncs.CheckColumnAmount(myViewPP.MainGrid.myDV.Table, {"AmountWo", "AmountPen", "Amount", "Balance"}) Then Me.AddError("", "Amount can Not be Less then Zero") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewDetPI.MainGrid.myDV.Table, {"Amount"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewDetDR.MainGrid.myDV.Table, {"Amount"}) Then Me.AddError("", "Negative Amount Not Allowed.")



        myView.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewWR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewRR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewDR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewRW.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewPP.MainGrid.CheckValid("", "", , "<CHECK COND=""AdvDated &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Advance Date.""/>")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))) Then
                Me.AddError("VouchNum", "Accounting entry passed. Voucher can't be update.")
            End If

            If myFuncs.CheckStatus(myContext, "AdvancePaymentID", myUtils.cValTN(myRow("PaymentID"))) Then
                Me.AddError("VouchNum", "This Payment Voucher is in used. Can't be Updated.")
            End If

            myRow("PostingDate") = myRow("Dated")

            Dim ObjPaymentVendor As New clsPaymentVendor(myContext)
            ObjPaymentVendor.LoadVouch(myUtils.cValTN(myRow("PaymentID")))
            myRow("DocType") = ObjPaymentVendor.DocType

            Dim ObjVouch As New clsVoucherNum(myContext)
            ObjVouch.GetNewSerialNo("PaymentID", "PV", myRow.Row)

            If Me.CanSave Then
                Dim PaymentTypeDescrip As String = Me.SelectedRow("PaymentType")("Descrip")
                Dim PaymentDescrip As String = myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy") & ", Type: " & PaymentTypeDescrip & ""

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Payment Where PaymentID = " & frmIDX)
                    frmIDX = myRow("PaymentID")

                    myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select, "PaymentItemType=PI")
                    myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PI"), "Select PaymentItemID, PaymentID, InvoiceID, PaymentItemType, Fac, Amount, AmountEXV, TDSAmount,WCTAmount, AmountCess, AmountDiscount from PaymentItem", True)

                    myUtils.ChangeAll(myViewWR.MainGrid.myDS.Tables(0).Select, "PaymentItemType=WR")
                    Me.myViewWR.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewRR.MainGrid.myDS.Tables(0).Select, "PaymentItemType=RR")
                    Me.myViewRR.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewDR.MainGrid.myDV.Table.Select, "PaymentItemType=DR")
                    myUtils.ChangeAll(myViewDR.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DR"), "Select PaymentItemID, PaymentID, InvoiceID, PaymentItemType, WCTAmount, Amount, Fac, AmountWo, AmountPen, AmountRet, TDSAmount, AmountCess, AmountDiscount from PaymentItem", True)

                    myUtils.ChangeAll(myViewRW.MainGrid.myDS.Tables(0).Select, "PaymentItemType=RW")
                    Me.myViewRW.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewAdv.MainGrid.myDS.Tables(0), "Select TourVouchItemID, PaymentID, AdvanceVouchID, Amount from TourVouchItem")

                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DetPI"), "Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DetDR"), "Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet", True)

                    myUtils.ChangeAll(myViewPP.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myUtils.ChangeAll(myViewPP.MainGrid.myDV.Table.Select, "PaymentItemType=PP")
                    myContext.Provider.objSQLHelper.SaveResults(myViewPP.MainGrid.myDV.Table, "Select PaymentItemID,PaymentID, AdvancePaymentID, InvoiceID, Fac, PaymentItemType, Amount, AmountPen, AmountWO, AmountDiscount from PaymentItem")

                    Dim Oret As clsProcOutput = ObjPaymentVendor.HandleWorkflowState(myRow.Row)
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
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, sql As String = "", strSys As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            strSys = "<SYS ID=""InvoiceID""/>"
            Dim Str As String = ""
            If myContext.SQL.Exists(Params, "@campusid") AndAlso myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params)) > 0 Then Str = " and CampusID = " & myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params))
            Select Case SelectionKey.Trim.ToUpper
                Case "PI"
                    sql = myContext.SQL.PopulateSQLParams("VendorID = @vendorid " & Str & " and CompanyID = @companyid and (isnull(BalanceAmount,0) > 0 or InvoiceID in (@oldinvoiceidcsv)) and InvoiceID Not in (@invoiceidcsv) and PostingDate <= '@postingdate' and isnull(IsProcessed,0) = 1", Params)
                Case "WR"
                    sql = myContext.SQL.PopulateSQLParams("VendorID = @vendorid " & Str & " and CompanyID = @companyid and (isNull(WOffAmount,0) > 0 or InvoiceID in (@oldinvoiceidcsv)) and InvoiceID Not in (@invoiceidcsv) and PostingDate <= '@postingdate' and isNull(IsProcessed,0) = 1", Params)
                Case "RR"
                    sql = myContext.SQL.PopulateSQLParams("VendorID = @vendorid " & Str & " and CompanyID = @companyid and (isNull(RetainedAmount,0) > 0 or InvoiceID in (@oldinvoiceidcsv)) and InvoiceID Not in (@invoiceidcsv) and PostingDate <= '@postingdate' and isNull(IsProcessed,0) = 1", Params)
                Case "DR"
                    sql = myContext.SQL.PopulateSQLParams("VendorID = @vendorid " & Str & " and CompanyID = @companyid and (isnull(BalanceAmount,0) > 0 or InvoiceID in (@oldinvoiceidcsv)) and InvoiceID Not in (@invoiceidcsv) and PostingDate <= '@postingdate' and isnull(IsProcessed,0) = 1", Params)
                Case "RW"
                    sql = myContext.SQL.PopulateSQLParams("VendorID = @vendorid " & Str & " and CompanyID = @companyid and (isNull(RetainedAmount,0) > 0 or InvoiceID in (@oldinvoiceidcsv)) and InvoiceID Not in (@invoiceidcsv) and PostingDate <= '@postingdate' and isNull(IsProcessed,0) = 1", Params)
                Case "TOURVOUCH"
                    sql = myContext.SQL.PopulateSQLParams("(isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) " & Str & " and  CompanyID  = @companyid and EmployeeID = @employeeid and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1", Params)
                    strSys = "<SYS ENT=""TA""/>"
                Case "PP"
                    sql = myContext.SQL.PopulateSQLParams("VendorID = @vendorid " & Str & " and CompanyID = @companyid and isNull(BalanceAmount,0) > 0 and PaymentID Not in (@paymentidcsv) and Dated <= '@dated' and IsProcessed = 1", Params)
                    strSys = "<SYS ID=""PaymentID""/>"
            End Select
            If sql.Trim.Length > 0 Then Model = myContext.Provider.GenerateSelectionModel(vwState, strSys, True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
        End If
        Return Model
    End Function

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim objTourVouchProc As New clsTourVouchProc(myContext), ObjPaymentVendor As New clsPaymentVendor(myContext)
        Dim Dated As DateTime
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ID", Params))
        If myContext.SQL.Exists(Params, "@Dated") Then Dated = myUtils.cDateTN(myContext.SQL.ParamValue("@Dated", Params), Now.Date)
        Select Case DataKey.Trim.ToUpper
            Case "PI"
                ObjPaymentVendor.PopulatePreBalanceDue(dt.Select, ID, Dated)
            Case "WR"
                ObjPaymentVendor.PopulatePreBalanceWR(dt.Select, ID, Dated)
            Case "RR"
                ObjPaymentVendor.PopulatePreBalanceRetained(dt.Select, ID, Dated)
            Case "DR"
                ObjPaymentVendor.PopulatePreBalanceDue(dt.Select, ID, Dated)
            Case "RW"
                ObjPaymentVendor.PopulatePreBalanceRetained(dt.Select, ID, Dated)
            Case "GENERATEPREBAL"
                objTourVouchProc.PopulatePreBalanceAdv(dt.Select, "PaymentID", ID, "TourVouchID")
            Case "PP"
                dt.Columns("paymentid").ColumnName = "advancepaymentid"
                ObjPaymentVendor.PopulatePreBalancePP(dt.Select, ID, Dated)
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