Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmPaymentCustomerModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim myViewWR, myViewRR, myViewDR, myViewRW, myViewAdv, myViewSR, myViewOR, myViewSW, myViewOW, myViewCR, myViewCW, myViewDetPI, myViewDetDR, myViewPP, myViewDetRR, myViewDetRW, myViewDetOW As clsViewModel

    Protected Overrides Sub InitViews()
        ''''Transaction ----------
        myView = Me.GetViewModel("PI")
        myViewWR = Me.GetViewModel("WR")
        myViewRR = Me.GetViewModel("RR")
        myViewSR = Me.GetViewModel("SR")
        myViewOR = Me.GetViewModel("OR")
        myViewCR = Me.GetViewModel("CR")
        myViewAdv = Me.GetViewModel("Adv")
        myViewDetPI = Me.GetViewModel("DetPI")
        myViewDetDR = Me.GetViewModel("DetDR")

        '''''Information-----------
        myViewDR = Me.GetViewModel("DR")
        myViewRW = Me.GetViewModel("RW")
        myViewSW = Me.GetViewModel("SW")
        myViewOW = Me.GetViewModel("OW")
        myViewCW = Me.GetViewModel("CW")
        myViewPP = Me.GetViewModel("PP")
        myViewDetRR = Me.GetViewModel("DetRR")
        myViewDetRW = Me.GetViewModel("DetRW")
        myViewDetOW = Me.GetViewModel("DetOW")

    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CompanyID, CompName,FinStartDate  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = "SELECT  CustomerID, CustDescrip, CustomerClass FROM  slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "InvType", "")
        Me.AddLookupField("PaymentType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentType").TableName)

        sql = "Select CampusID, DispName, CompanyID, WODate,CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CashCampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select BankAccountID, AccountName, GlAccountId, companyid, ShortName from BankAccount"
        Me.AddLookupField("BankAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BankAccount").TableName)

        Dim str1 As String = myUtils.CombineWhereOR(False, "isnull(imprestenabled,0)=1", "employeeid in (select imprestemployeeid from payment)")
        sql = "Select employeeid, empcode, fullname, onrolls, companyid,JoinDate,LeaveDate from hrpListAllEmp() where " & str1 & " order by fullname"
        Me.AddLookupField("ImprestEmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "emp").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "Mode", "Codeword <> 'WO'")
        Me.AddLookupField("PaymentMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentMode").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String, ObjPaymentCust As New clsPaymentCustomer(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Payment Where PaymentID = " & prepIDX
        Me.InitData(sql, "InvoiceID,PaymentType", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
        Else
            Dim rPostPeriod As DataRow = ObjPaymentCust.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        ObjPaymentCust.LoadVouch(myUtils.cValTN(myRow("PaymentID")))

        Me.BindDataTable(myUtils.cValTN(prepIDX))


        '----------Transaction

        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountCess"" CAPTION=""CESS Deduct"" NOEDIT=""True""/><COL KEY=""AmountInterest"" CAPTION=""Interest""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/><COL KEY=""AmountEXV"" CAPTION=""Exchange Rate""/><COL KEY=""InvoiceTypeCode"" CAPTION=""Invoice Type"" NOEDIT=""True""/>"
        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1-1-1-1", True)

        myViewWR.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, Amount, BillOf, DocType, AmountDiscount, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWO, AmountPen, TDSAmount,WCTAmount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'WR'"
        myViewWR.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewWR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewRR.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/><COL KEY=""AmountCess"" CAPTION=""CESS Deduct"" NOEDIT=""True""/><COL KEY=""AmountInterest"" CAPTION=""Interest""/>"
        myViewRR.MainGrid.BindGridData(Me.dsForm, 8)
        myViewRR.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1", True)


        myViewSR.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, BillOf, DocType, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, Amount, TDSAmount,WCTAmount, AmountDiscount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'SR'"
        myViewSR.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewSR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewOR.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, BillOf, DocType, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, Amount, TDSAmount,WCTAmount, AmountDiscount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'OR'"
        myViewOR.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewOR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCR.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, BillOf, DocType, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, Amount, TDSAmount,WCTAmount, AmountDiscount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'CR'"
        myViewCR.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewCR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)



        '---------Information
        myViewDR.MainGrid.MainConf("rhfactor") = 2
        myViewDR.MainGrid.MainConf("FORMATXML") = "<COL KEY=""InvoiceNum"" CAPTION=""Invoice No.""/><COL KEY=""InvoiceDate"" CAPTION=""Invoice Date""/><COL KEY=""PreBalance"" CAPTION=""Pre Balance""/><COL KEY=""Balance"" FORMAT=""\D\r #,#00.00; \C\r #,#00.00;\Z\e\r\o""/>"
        myViewDR.MainGrid.BindGridData(Me.dsForm, 2)
        myViewDR.MainGrid.QuickConf("", True, "1-.8-1-1.1-1-1-1-1-1-1-.9-.9-.9-.9-.9-1.2", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountRet"" CAPTION=""Retention""/><COL KEY=""AmountSec"" CAPTION=""Security""/><COL KEY=""AmountOth"" CAPTION=""Other""/><COL KEY=""AmountCESSRet"" CAPTION=""CESS Retained""/><COL KEY=""AmountCess"" CAPTION=""CESS Deduct""  NOEDIT=""True""/><COL KEY=""AmountInterest"" CAPTION=""Interest""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/><COL KEY=""InvoiceTypeCode"" CAPTION=""Invoice Type"" NOEDIT=""True""/></BAND>"
        myViewDR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewRW.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewRW.MainGrid.BindGridData(Me.dsForm, 9)
        myViewRW.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountCess"" CAPTION=""CESS Deduct"" NOEDIT=""True""/><COL KEY=""AmountInterest"" CAPTION=""Interest""/></BAND>"
        myViewRW.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewSW.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, Amount, BillOf, DocType,PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWO, AmountPen, TDSAmount,WCTAmount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'SW'"
        myViewSW.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/></BAND>"
        myViewSW.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewOW.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewOW.MainGrid.BindGridData(Me.dsForm, 10)
        myViewOW.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountCess"" CAPTION=""CESS Deduct""  NOEDIT=""True""/></BAND>"
        myViewOW.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCW.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, Amount, BillOf, DocType,PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWO, AmountPen, TDSAmount,WCTAmount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'CW'"
        myViewCW.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, InvoiceID, Amount""/><COL KEY=""TDSAmount"" CAPTION=""TDS""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/></BAND>"
        myViewCW.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        sql = " Select PaymentItemID, PaymentID, SalesOrderID,Fac, PaymentItemType, OrderNum, OrderDate, Amount from AccListPaymentItem()  Where PaymentID = " & prepIDX & " and PaymentItemType = 'AA'"
        myViewAdv.MainGrid.QuickConf(sql, True, "1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, Fac, PaymentItemType, SalesOrderID, Amount""/></BAND>"
        myViewAdv.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDetPI.MainGrid.BindGridData(Me.dsForm, 3)
        myViewDetPI.MainGrid.QuickConf("", True, "1-1", True)
        sql = "Select Class from AccountClass where InDeduction = 1 Order By Class"
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemDet"" IDFIELD=""PaymentItemDetID""><COL KEY =""PaymentItemDetID, PaymentItemID, Amount""/><COL KEY=""ValuationClass""  CAPTION=""Valuation Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewDetPI.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDetDR.MainGrid.BindGridData(Me.dsForm, 4)
        myViewDetDR.MainGrid.QuickConf("", True, "1-1", True)
        sql = "Select Class from AccountClass where InDeduction = 1 Order By Class"
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemDet"" IDFIELD=""PaymentItemDetID""><COL KEY =""PaymentItemDetID, PaymentItemID, Amount""/><COL KEY=""ValuationClass""  CAPTION=""Valuation Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewDetDR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewPP.MainGrid.MainConf("formatxml") = "<COL KEY=""AdvVouchNum"" CAPTION=""Voucher No.""/><COL KEY=""AdvDated"" CAPTION=""Dated""/><COL KEY=""AdvPaymentInfo"" CAPTION=""PaymentInfo""/><COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewPP.MainGrid.BindGridData(Me.dsForm, 5)
        myViewPP.MainGrid.QuickConf("", True, "1-1-1-2-1-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""Fac, PaymentID, AdvancePaymentID, InvoiceID, PaymentItemType, Amount""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/><COL KEY=""AmountInterest"" CAPTION=""Interest""/></BAND>"
        myViewPP.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDetRR.MainGrid.BindGridData(Me.dsForm, 6)
        myViewDetRR.MainGrid.QuickConf("", True, "1-1", True)
        sql = "Select Class from AccountClass where InDeduction = 1 Order By Class"
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemDet"" IDFIELD=""PaymentItemDetID""><COL KEY =""PaymentItemDetID, PaymentItemID, Amount""/><COL KEY=""ValuationClass""  CAPTION=""Valuation Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewDetRR.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDetRW.MainGrid.BindGridData(Me.dsForm, 7)
        myViewDetRW.MainGrid.QuickConf("", True, "1-1", True)
        sql = "Select Class from AccountClass where InDeduction = 1 Order By Class"
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemDet"" IDFIELD=""PaymentItemDetID""><COL KEY =""PaymentItemDetID, PaymentItemID, Amount""/><COL KEY=""ValuationClass""  CAPTION=""Valuation Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewDetRW.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewDetOW.MainGrid.BindGridData(Me.dsForm, 11)
        myViewDetOW.MainGrid.QuickConf("", True, "1-1", True)
        sql = "Select Class from AccountClass where InDeduction = 1 Order By Class"
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemDet"" IDFIELD=""PaymentItemDetID""><COL KEY =""PaymentItemDetID, PaymentItemID, Amount""/><COL KEY=""ValuationClass""  CAPTION=""Valuation Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewDetOW.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)


        If myUtils.cValTN(Me.vBag("InvoiceID")) > 0 AndAlso frmMode = EnumfrmMode.acAddM Then
            GenerateInvoice(myUtils.cValTN(Me.vBag("InvoiceID")))
        End If

        ObjPaymentCust.PopulatePreBalanceDue(myView.MainGrid.myDV.Table.Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceWR(myViewWR.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceRetained(myViewRR.MainGrid.myDV.Table.Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceSecurity(myViewSR.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceOther(myViewOR.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceDue(myViewDR.MainGrid.myDV.Table.Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceRetained(myViewRW.MainGrid.myDV.Table.Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceSecurity(myViewSW.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceOther(myViewOW.MainGrid.myDV.Table.Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceCessRet(myViewCR.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalanceCessRet(myViewCW.MainGrid.myDS.Tables(0).Select, frmIDX, myRow("dated"))
        ObjPaymentCust.PopulatePreBalancePP(myViewPP.MainGrid.myDV.Table.Select, frmIDX, myRow("Dated"))

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub GenerateInvoice(InvoiceID As Integer)
        Dim Sql As String = "Select InvoiceID, DivisionID, CustomerID, CompanyID, BillOf, DocType, PostingDate, InvoiceTypeCode, InvoiceNum, InvoiceDate from AccListInvoice() Where InvoiceID = " & myUtils.cValTN(InvoiceID)
        Dim rr1() As DataRow = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0).Select
        If rr1.Length > 0 Then
            myRow("DivisionID") = myUtils.cValTN(rr1(0)("DivisionID"))
            myRow("CustomerID") = myUtils.cValTN(rr1(0)("CustomerID"))
            myRow("CompanyID") = myUtils.cValTN(rr1(0)("CompanyID"))
            Dim r2 As DataRow = myUtils.CopyOneRow(rr1(0), myView.MainGrid.myDV.Table)
        End If
    End Sub

    Private Sub BindDataTable(ByVal PaymentID As Integer)
        Dim ds As DataSet, Sql1, Sql2, Sql3, Sql4, Sql5, Sql6, Sql7, Sql8, Sql9, Sql10, Sql11, Sql As String

        Sql1 = " Select PaymentItemID, PaymentID, InvoiceID, Fac, PaymentItemType, BillOf, DocType, PostingDateIV as PostingDate, InvoiceTypeCode, InvoiceNum, InvoiceDate, 0.0 as PreBalance, Amount, AmountEXV, TDSAmount, WCTAmount, AmountCess, AmountInterest, AmountDiscount, 0.0 as Balance from AccListPaymentItem() Where PaymentID = " & PaymentID & " and PaymentItemType = 'PI' "
        Sql2 = " Select PaymentItemID, PaymentID, InvoiceID, ReverseInvoiceID, AmountTot, Fac, PaymentItemType, Amount, BillOf, DocType, PostingDateIV as PostingDate, InvoiceTypeCode, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWo, AmountPen, AmountRet, AmountSec,AmountCESSRet, AmountOth, TDSAmount, WCTAmount, AmountCess, AmountInterest, AmountDiscount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & PaymentID & " and PaymentItemType = 'DR'"
        Sql3 = " Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet where PaymentItemID in (Select PaymentItemID from PaymentItem where PaymentID = " & PaymentID & " and paymentitemtype='PI')"
        Sql4 = " Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet where PaymentItemID in (Select PaymentItemID from PaymentItem where PaymentID = " & PaymentID & " and paymentitemtype='DR')"
        Sql5 = " Select PaymentItemID, PaymentID, AdvancePaymentID, InvoiceID, OpenAmountAdj, NewAmount, Fac, PaymentItemType, AdvPaymentType, AdvVouchNum, AdvDated, AdvPaymentInfo, 0.00 as PreBalance, AmountWo, AmountPen, AmountDiscount, Amount, AmountInterest, 0.00 as Balance from AccListPaymentItem() Where PaymentID = " & PaymentID & " and PaymentItemType = 'PP' "
        Sql6 = " Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet where PaymentItemID in (Select PaymentItemID from PaymentItem where PaymentID = " & PaymentID & " and paymentitemtype='RR')"
        Sql7 = " Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet where PaymentItemID in (Select PaymentItemID from PaymentItem where PaymentID = " & PaymentID & " and paymentitemtype='RW')"
        Sql8 = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, BillOf, DocType, PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, Amount, TDSAmount,WCTAmount,AmountCess, AmountInterest, AmountDiscount, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & PaymentID & " and PaymentItemType = 'RR'"
        Sql9 = " Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, Amount, BillOf, DocType,PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWO, AmountPen, TDSAmount,WCTAmount,AmountCess, AmountInterest, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & PaymentID & " and PaymentItemType = 'RW'"
        Sql10 = "Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, InvoiceTypeCode, Amount, BillOf, DocType,PostingDateIV as PostingDate, InvoiceNum, InvoiceDate, 0.0 as PreBalance, AmountWO, AmountPen, TDSAmount,WCTAmount,AmountCess, 0.0 as Balance from AccListPaymentItem()  Where PaymentID = " & PaymentID & " and PaymentItemType = 'OW'"
        Sql11 = " Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet where PaymentItemID in (Select PaymentItemID from PaymentItem where PaymentID = " & PaymentID & " and paymentitemtype='OW')"

        Sql = Sql1 & "; " & Sql2 & "; " & Sql3 & "; " & Sql4 & "; " & Sql5 & "; " & Sql6 & "; " & Sql7 & "; " & Sql8 & "; " & Sql9 & "; " & Sql10 & "; " & Sql11
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "PI", 0)
        myUtils.AddTable(Me.dsForm, ds, "DR", 1)
        myUtils.AddTable(Me.dsForm, ds, "DetPI", 2)
        myUtils.AddTable(Me.dsForm, ds, "DetDR", 3)
        myUtils.AddTable(Me.dsForm, ds, "PP", 4)
        myUtils.AddTable(Me.dsForm, ds, "DetRR", 5)
        myUtils.AddTable(Me.dsForm, ds, "DetRW", 6)
        myUtils.AddTable(Me.dsForm, ds, "RR", 7)
        myUtils.AddTable(Me.dsForm, ds, "RW", 8)
        myUtils.AddTable(Me.dsForm, ds, "OW", 9)
        myUtils.AddTable(Me.dsForm, ds, "DetOW", 10)

        myContext.Tables.SetAuto(Me.dsForm.Tables("PI"), Me.dsForm.Tables("DetPI"), "PaymentItemID", "_DetPI")
        myContext.Tables.SetAuto(Me.dsForm.Tables("DR"), Me.dsForm.Tables("DetDR"), "PaymentItemID", "_DetDR")
        myContext.Tables.SetAuto(Me.dsForm.Tables("RR"), Me.dsForm.Tables("DetRR"), "PaymentItemID", "_DetRR")
        myContext.Tables.SetAuto(Me.dsForm.Tables("RW"), Me.dsForm.Tables("DetRW"), "PaymentItemID", "_DetRW")
        myContext.Tables.SetAuto(Me.dsForm.Tables("OW"), Me.dsForm.Tables("DetOW"), "PaymentItemID", "_DetOW")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("CompanyID") Is Nothing Then Me.AddError("CompanyID", "Please Select Company")
        If Me.SelectedRow("Divisionid") Is Nothing Then Me.AddError("Divisionid", "Please Select Division")
        If Me.SelectedRow("PaymentType") Is Nothing Then Me.AddError("PaymentType", "Please Select Payment Type")
        If Me.SelectedRow("CustomerId") Is Nothing Then Me.AddError("CustomerId", "Please Select Customer")
        If myUtils.cValTN(Me.myRow("NewAmount")) < 0 Then Me.AddError("NewAmount", "Advance Amount can Not be less than zero")
        If myUtils.cValTN(Me.myRow("NewTDSAmount")) < 0 Then Me.AddError("NewTDSAmount", "Advance TDS Amount can Not be less than zero")
        If myUtils.cValTN(Me.myRow("NewWCTAmount")) < 0 Then Me.AddError("NewWCTAmount", "Advance GST-TDS can Not be less than zero")
        If CheckAdvAmount() = False Then Me.AddError("NewAmount", "Advance Amount can Not be less than Order Amount")

        If Not Me.SelectedRow("CompanyID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.myRow("CompanyID")), Me.myRow("Dated"), PPFinal)

            If myRow("Dated") >= Me.SelectedRow("CompanyID")("FinStartDate") Then
                If myUtils.IsInList(myUtils.cStrTN(Me.myRow("PaymentType")), "T") Then myFuncs.ValidatePaymentMode(Me)
            End If
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")

        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("PaymentType")), "S") AndAlso myUtils.cValTN(myRow("AmountTotPay")) <> 0 Then Me.AddError("AmountTotPay", "Debit Amount Should be Equal to Credit Amount.")
        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")
        Me.myViewPP.MainGrid.CheckValid("Amount")


        If Not myFuncs.CheckColumnAmount(myView.MainGrid.myDV.Table, {"Amount", "TDSAmount", "WCTAmount", "AmountCess", "AmountInterest", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.") ' Not Included in Validation (AmountDiscount, AmountEXV)
        If Not myFuncs.CheckColumnAmount(myViewWR.MainGrid.myDS.Tables(0), {"AmountWO", "AmountPen", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewRR.MainGrid.myDV.Table, {"Amount", "TDSAmount", "WCTAmount", "AmountCess", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewSR.MainGrid.myDS.Tables(0), {"Amount", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewOR.MainGrid.myDS.Tables(0), {"Amount", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewCR.MainGrid.myDS.Tables(0), {"Amount", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewDR.MainGrid.myDV.Table, {"AmountWo", "AmountPen", "AmountRet", "AmountSec", "AmountCESSRet", "AmountOth", "TDSAmount", "WCTAmount", "AmountCess", "AmountInterest", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.") ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewRW.MainGrid.myDV.Table, {"AmountWO", "AmountPen", "TDSAmount", "WCTAmount", "AmountCess", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewSW.MainGrid.myDS.Tables(0), {"AmountWO", "AmountPen", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewOW.MainGrid.myDV.Table, {"AmountWO", "AmountPen", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewCW.MainGrid.myDS.Tables(0), {"AmountWO", "AmountPen", "TDSAmount", "WCTAmount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewPP.MainGrid.myDV.Table, {"AmountWo", "AmountPen", "Amount", "AmountInterest", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")  ' Not Included in Validation (AmountDiscount)
        If Not myFuncs.CheckColumnAmount(myViewDetPI.MainGrid.myDV.Table, {"Amount"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewDetDR.MainGrid.myDV.Table, {"Amount"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewDetRR.MainGrid.myDV.Table, {"Amount"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewDetRW.MainGrid.myDV.Table, {"Amount"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewDetOW.MainGrid.myDV.Table, {"Amount"}) Then Me.AddError("", "Negative Amount Not Allowed.")


        myView.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewWR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewRR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewSR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewOR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewCR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewDR.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewRW.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewSW.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewOW.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewCW.MainGrid.CheckValid("", "", , "<CHECK COND=""PostingDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Invoice Date.""/>")
        myViewAdv.MainGrid.CheckValid("", "", , "<CHECK COND=""OrderDate &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Order Date.""/>")
        myViewPP.MainGrid.CheckValid("", "", , "<CHECK COND=""AdvDated &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Advance Date.""/>")


        Dim rr1() As DataRow = myViewDR.MainGrid.myDV.Table.Select("InvoiceTypeCode = 'IR'")
        If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
            CheckRevrseInvoice()
        End If

        Return Me.CanSave
    End Function

    Private Sub CheckRevrseInvoice()
        Dim rr1() As DataRow = myViewDR.MainGrid.myDV.Table.Select("InvoiceTypeCode = 'IR' and Balance <> 0")
        If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
            Me.AddError("", "Balance of Reverse Invoice should be Zero.")
        End If


        For Each r1 As DataRow In myViewDR.MainGrid.myDV.Table.Select("ReverseInvoiceID is Not NULL")
            Dim AmountPen, AmountWO As Decimal

            For Each View As clsViewModel In New clsViewModel() {myViewRW, myViewDR}
                AmountPen = AmountPen + GetAmtPenWO(View.MainGrid.myDV.Table, myUtils.cValTN(r1("ReverseInvoiceID")), "AmountPen")
                AmountWO = AmountWO + GetAmtPenWO(View.MainGrid.myDV.Table, myUtils.cValTN(r1("ReverseInvoiceID")), "AmountWO")
            Next


            For Each View As clsViewModel In New clsViewModel() {myViewSW, myViewOW, myViewCW}
                AmountPen = AmountPen + GetAmtPenWO(View.MainGrid.myDS.Tables(0), myUtils.cValTN(r1("ReverseInvoiceID")), "AmountPen")
                AmountWO = AmountWO + GetAmtPenWO(View.MainGrid.myDS.Tables(0), myUtils.cValTN(r1("ReverseInvoiceID")), "AmountWO")
            Next

            If myUtils.cValTN(r1("AmountPen")) <> AmountPen Then Me.AddError("", "Penalty Amount of Reverse invoice should be equal to Base Invoice.")
            If myUtils.cValTN(r1("AmountWO")) <> AmountWO Then Me.AddError("", "Bad Debt Amount of Reverse invoice should be equal to Base Invoice.")
            If myUtils.cValTN(r1("AmountTot")) <> (AmountPen + AmountWO) Then Me.AddError("", "Please enter correct amount.")
        Next


        Dim AmtPen, AmtWO As Decimal
        For Each View As clsViewModel In New clsViewModel() {myViewDR, myViewRW, myViewSW, myViewOW, myViewCW}
            AmtPen = AmtPen + myUtils.cValTN(View.MainGrid.GetColSum("AmountPen", "InvoiceTypeCode <> 'IR'"))
            AmtWO = AmtWO + myUtils.cValTN(View.MainGrid.GetColSum("AmountWo", "InvoiceTypeCode <> 'IR'"))
        Next

        If myUtils.cValTN(myViewDR.MainGrid.GetColSum("AmountPen", "InvoiceTypeCode = 'IR'")) <> AmtPen Then Me.AddError("", "Penalty Amount of Reverse invoice should be equal to Base Invoice.")
        If myUtils.cValTN(myViewDR.MainGrid.GetColSum("AmountWo", "InvoiceTypeCode = 'IR'")) <> AmtWO Then Me.AddError("", "Bad Debt Amount of Reverse invoice should be equal to Base Invoice.")
        If myViewDR.MainGrid.GetColSum("AmountTot", "InvoiceTypeCode = 'IR'") <> (AmtPen + AmtWO) Then Me.AddError("", "Please enter correct amount.")
    End Sub

    Private Function GetAmtPenWO(dt As DataTable, ReverseInvoiceID As Integer, AmtField As String) As Decimal
        Dim Amount As Decimal
        Dim rr1() As DataRow = dt.Select("InvoiceID = " & ReverseInvoiceID & "")
        If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
            Amount = myUtils.cValTN(rr1(0)(AmtField))
        End If
        Return Amount
    End Function

    Private Function CheckAdvAmount() As Boolean
        CheckAdvAmount = True
        Dim Amt As Decimal = myViewAdv.MainGrid.GetColSum("Amount", "")
        If (myUtils.cValTN(Me.myRow("NewAmount")) + myUtils.cValTN(Me.myRow("NewTDSAmount"))) < myUtils.cValTN(Amt) Then
            CheckAdvAmount = False
        End If
        Return CheckAdvAmount
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))) Then
                Me.AddError("VouchNum", "Accounting entry passed. Voucher can't be update.")
            End If
            If Me.CanSave Then
                myRow("PostingDate") = myRow("Dated")

                Dim ObjPaymentCust As New clsPaymentCustomer(myContext)
                ObjPaymentCust.LoadVouch(myUtils.cValTN(myRow("PaymentID")))
                myRow("DocType") = ObjPaymentCust.DocType

                Dim ObjVouch As New clsVoucherNum(myContext)
                ObjVouch.GetNewSerialNo("PaymentID", "PC", myRow.Row)

                Dim PaymentTypeDescrip As String = Me.SelectedRow("PaymentType")("Descrip")
                Dim PaymentDescrip As String = myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy") & ", Type: " & PaymentTypeDescrip & ""

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Payment Where PaymentID = " & frmIDX)
                    frmIDX = myRow("PaymentID")

                    myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select, "PaymentItemType=PI")
                    myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PI"), "Select PaymentItemID, PaymentID, InvoiceID, Fac, PaymentItemType,  Amount, AmountEXV, TDSAmount, WCTAmount, AmountCess, AmountInterest, AmountDiscount from PaymentItem", True)

                    myUtils.ChangeAll(myViewWR.MainGrid.myDS.Tables(0).Select, "PaymentItemType=WR")
                    Me.myViewWR.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewRR.MainGrid.myDV.Table.Select, "PaymentItemType=RR")
                    myUtils.ChangeAll(myViewRR.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("RR"), "Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType,  Amount, TDSAmount,WCTAmount,AmountCess, AmountInterest, AmountDiscount from PaymentItem", True)

                    myUtils.ChangeAll(myViewSR.MainGrid.myDS.Tables(0).Select, "PaymentItemType=SR")
                    Me.myViewSR.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewOR.MainGrid.myDS.Tables(0).Select, "PaymentItemType=OR")
                    Me.myViewOR.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewCR.MainGrid.myDS.Tables(0).Select, "PaymentItemType=CR")
                    Me.myViewCR.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewDR.MainGrid.myDV.Table.Select, "PaymentItemType=DR")
                    myUtils.ChangeAll(myViewDR.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DR"), "Select PaymentItemID, PaymentID, InvoiceID, Fac, PaymentItemType, Amount,  AmountWo, AmountPen, AmountRet, AmountSec,AmountCESSRet, AmountOth, TDSAmount, WCTAmount, AmountCess, AmountInterest, AmountDiscount from PaymentItem", True)

                    myUtils.ChangeAll(myViewRW.MainGrid.myDV.Table.Select, "PaymentItemType=RW")
                    myUtils.ChangeAll(myViewRW.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("RW"), "Select PaymentItemID, PaymentID, InvoiceID,Fac, PaymentItemType, Amount, AmountWO, AmountPen, TDSAmount,WCTAmount,AmountCess,AmountInterest from PaymentItem", True)

                    myUtils.ChangeAll(myViewCW.MainGrid.myDS.Tables(0).Select, "PaymentItemType=CW")
                    Me.myViewCW.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myUtils.ChangeAll(myViewSW.MainGrid.myDS.Tables(0).Select, "PaymentItemType=SW")
                    Me.myViewSW.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)


                    myUtils.ChangeAll(myViewOW.MainGrid.myDV.Table.Select, "PaymentItemType=OW")
                    myUtils.ChangeAll(myViewOW.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("OW"), "Select PaymentItemID,PaymentID,Fac,PaymentItemType,InvoiceID,Amount,TDSAmount,WCTAmount,AmountPen,AmountWO,AmountCess from PaymentItem", True)

                    myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentItemType=AA")
                    Me.myViewAdv.MainGrid.SaveChanges(, "PaymentID = " & frmIDX)

                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DetPI"), "Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DetDR"), "Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DetRR"), "Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DetRW"), "Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("DetOW"), "Select PaymentItemDetID, PaymentItemID, ValuationClass, Amount from PaymentItemDet", True)

                    myUtils.ChangeAll(myViewPP.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myUtils.ChangeAll(myViewPP.MainGrid.myDV.Table.Select, "PaymentItemType=PP")
                    myContext.Provider.objSQLHelper.SaveResults(myViewPP.MainGrid.myDV.Table, "Select PaymentItemID,PaymentID, AdvancePaymentID, InvoiceID, Fac, PaymentItemType, Amount,AmountPen,AmountWO, AmountDiscount,AmountInterest from PaymentItem")

                    Dim Oret As clsProcOutput = ObjPaymentCust.HandleWorkflowState(myRow.Row)
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

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim ObjPaymentCust As New clsPaymentCustomer(myContext)
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("ID", Params))
        Dim Dated As DateTime = myUtils.cDateTN(myContext.SQL.ParamValue("Dated", Params), Now.Date)

        Select Case DataKey.Trim.ToUpper
            Case "PI", "DR"
                ObjPaymentCust.PopulatePreBalanceDue(dt.Select, ID, Dated)
            Case "WR"
                ObjPaymentCust.PopulatePreBalanceWR(dt.Select, ID, Dated)
            Case "RR", "RW"
                ObjPaymentCust.PopulatePreBalanceRetained(dt.Select, ID, Dated)
            Case "SR", "SW"
                ObjPaymentCust.PopulatePreBalanceSecurity(dt.Select, ID, Dated)
            Case "OR", "OW"
                ObjPaymentCust.PopulatePreBalanceOther(dt.Select, ID, Dated)
            Case "CR", "CW"
                ObjPaymentCust.PopulatePreBalanceCessRet(dt.Select, ID, Dated)
            Case "PP"
                dt.Columns("paymentid").ColumnName = "advancepaymentid"
                ObjPaymentCust.PopulatePreBalancePP(dt.Select, ID, Dated)
        End Select
    End Sub

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, sql As String = "", sysID As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            sysID = "InvoiceID"
            Select Case SelectionKey.Trim.ToUpper
                Case "PI"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isnull(BalanceAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isnull(IsProcessed,0) = 1", Params)
                Case "WR"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(WOffAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "RR"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(RetainedAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "SR"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(SecurityAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "CR"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(CessRetAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "OR"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(OtherAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "DR"
                    sql = myContext.SQL.PopulateSQLParams("CustomerID = @customerid and CompanyID = @companyid and (isnull(BalanceAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isnull(IsProcessed,0) = 1", Params)
                Case "RW"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(RetainedAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "SW"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(SecurityAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "OW"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(OtherAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "CW"
                    sql = myContext.SQL.PopulateSQLParams("InvoiceTypeCode <> 'IR' and CustomerID = @customerid and CompanyID = @companyid and (isNull(CessRetAmount,0) > 0 or InvoiceID in (@oldidcsv)) and InvoiceID Not in (@idcsv) and PostingDate <= '@date' and isNull(IsProcessed,0) = 1", Params)
                Case "ADV"
                    sysID = "SalesOrderID"
                    sql = myContext.SQL.PopulateSQLParams("CustomerID = @customerid and CompanyID = @companyid and SalesOrderID Not in (@idcsv) and OrderDate <= '@date'", Params)
                Case "PP"
                    sysID = "PaymentID"
                    sql = myContext.SQL.PopulateSQLParams("CustomerID = @customerid and CompanyID = @companyid and isNull(BalanceAmount,0) > 0 and PaymentID Not in (@paymentidcsv) and Dated <= '@dated' and IsProcessed = 1", Params)
            End Select
            If sql.Trim.Length > 0 Then Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""" & sysID & """/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
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
