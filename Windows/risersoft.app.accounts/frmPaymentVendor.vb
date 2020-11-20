Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmPaymentVendor
    Inherits frmMax
    Dim dv, dvCamp As DataView, myViewWR, myViewRR, myViewDR, myViewRW, myViewAdv, myViewPP As New clsWinView, fPaymentMode As frmPaymentMode
    Dim strPI, strWR, strRR, strDR, strRW As String

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)

        fPaymentMode = New frmPaymentMode
        AddHandler fPaymentMode.PaymentModeChanged, AddressOf PaymentModeChanged
        fPaymentMode.AddtoTab(Me.UltraTabControl1, "Mode", True)

        myView.SetGrid(UltraGridPI)
        myViewWR.SetGrid(UltraGridWR)
        myViewRR.SetGrid(UltraGridRR)
        myViewDR.SetGrid(UltraGridDR)
        myViewRW.SetGrid(UltraGridRW)
        myViewAdv.SetGrid(Me.UltraGridAdv)
        myViewPP.SetGrid(UltraGridPP)
    End Sub

    Private Sub PaymentModeChanged(sender As Object, PaymentMode As String, IgnoreExpenseVoucher As Boolean)
        If myUtils.IsInList(myUtils.cStrTN(PaymentMode), "IM") AndAlso IgnoreExpenseVoucher = False Then
            UltraTabControl2.Tabs("TA").Visible = True
            EnableCtl()
        Else
            UltraTabControl2.Tabs("TA").Visible = False
        End If
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPaymentVendorModel = Me.InitData("frmPaymentVendorModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If frmMode = EnumfrmMode.acAddM AndAlso myUtils.cValTN(myRow("CompanyID")) = 0 AndAlso cmb_CompanyID.Rows.Count = 1 Then myRow("CompanyID") = myUtils.cValTN(cmb_CompanyID.Rows(0).Cells("CompanyID").Value)
            fPaymentMode.InitPanel(Me, False)
            fPaymentMode.HandleItem(myUtils.cValTN(myRow("CompanyID")), myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))

            CalculateBalance(myView.mainGrid.myDv.Table, "PI")
            CalculateBalance(myViewWR.mainGrid.myDS.Tables(0), "WR")
            CalculateBalance(myViewRR.mainGrid.myDS.Tables(0), "RR")
            CalculateBalance(myViewDR.mainGrid.myDv.Table, "DR")
            CalculateBalance(myViewRW.mainGrid.myDS.Tables(0), "RW")
            CalculateBalancePP(myUtils.cStrTN(myRow("PaymentType")))

            If prepMode = EnumfrmMode.acEditM Then strPI = myUtils.MakeCSV(myView.mainGrid.myDv.Table.Select, "InvoiceID") Else strPI = 0
            If prepMode = EnumfrmMode.acEditM Then strWR = myUtils.MakeCSV(myViewWR.mainGrid.myDS.Tables(0).Select, "InvoiceID") Else strWR = 0
            If prepMode = EnumfrmMode.acEditM Then strRR = myUtils.MakeCSV(myViewRR.mainGrid.myDS.Tables(0).Select, "InvoiceID") Else strRR = 0
            If prepMode = EnumfrmMode.acEditM Then strDR = myUtils.MakeCSV(myViewDR.mainGrid.myDv.Table.Select, "InvoiceID") Else strDR = 0
            If prepMode = EnumfrmMode.acEditM Then strRW = myUtils.MakeCSV(myViewRW.mainGrid.myDS.Tables(0).Select, "InvoiceID") Else strRW = 0

            HandlePaymentType(myUtils.cStrTN(myRow("PaymentType")))
            HandleCompanyID(myUtils.cValTN(myRow("CompanyID")))


            CalculateAmount(myUtils.cStrTN(myRow("PaymentType")))
            CalculateBalanceAdv()

            ReadOnlyCtl()

            If frmMode = EnumfrmMode.acEditM Then
                myRow("ForceAVSysNum") = DBNull.Value
                ChkGetMissingNo.Visible = False
            End If

            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) AndAlso ((myView.mainGrid.myDv.Table.Select.Length > 0) OrElse (myViewWR.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewRR.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewDR.mainGrid.myDv.Table.Select.Length > 0) OrElse (myViewRW.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewPP.mainGrid.myDv.Table.Select.Length > 0)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If

            Me.ActiveControl = cmb_PaymentType
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("PI"))
            myViewWR.PrepEdit(Me.Model.GridViews("WR"))
            myViewRR.PrepEdit(Me.Model.GridViews("RR"))
            myViewDR.PrepEdit(Me.Model.GridViews("DR"))
            myViewRW.PrepEdit(Me.Model.GridViews("RW"))
            myViewAdv.PrepEdit(Me.Model.GridViews("Adv"))
            myViewPP.PrepEdit(Me.Model.GridViews("PP"))

            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            dv = myWinSQL.AssignCmb(Me.dsCombo, "PaymentType", "", Me.cmb_PaymentType, , 1)
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID)

            GridBind(myUtils.cStrTN(myRow("PaymentType")))

            fPaymentMode.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()

        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
        CalculateBalanceAdv()

        If Me.ValidateData() Then
            If ChkGetMissingNo.Checked Then
                GetMissingVoucherNo()
            End If

            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub cmb_PaymentType_Leave(sender As Object, e As EventArgs) Handles cmb_PaymentType.Leave, cmb_PaymentType.AfterCloseUp
        HandlePaymentType(myUtils.cStrTN(cmb_PaymentType.Value))
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub HandlePaymentType(PaymentType As String)
        EnableBtn(True)
        UltraTabControl1.Tabs("Mode").Visible = True
        VisableControlPaymentType(myUtils.cStrTN(PaymentType))
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T", "R", "S") Then
            UltraTabControl2.Tabs("PI").Selected = True
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "I") Then
            UltraTabControl2.Tabs("DR").Selected = True
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "") Then
            EnableBtn(False)
        End If
    End Sub

    Private Sub EnableBtn(Enable As Boolean)
        btnAdd.Enabled = Enable
        btnDel.Enabled = Enable
        UltraTabControl2.Enabled = Enable
    End Sub

    Private Sub EnableControls(ByVal Bool As Boolean)
        cmb_VendorId.ReadOnly = Bool
        cmb_PaymentType.ReadOnly = Bool
        cmb_CompanyID.ReadOnly = Bool
        cmb_campusid.ReadOnly = Bool
    End Sub

    Private Sub VisableControlPaymentType(PaymentType As String)
        ''''''''Transaction 
        UltraTabControl2.Tabs("PI").Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "T", "R", "S")
        UltraTabControl2.Tabs("WR").Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "T")
        UltraTabControl2.Tabs("RR").Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "T")
        txtAmountEXV.Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "T")
        lblAmountEXV.Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "T")
        UltraTabControl1.Tabs("Mode").Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "T", "R")

        ''''''''Information and Return
        UltraTabControl2.Tabs("PP").Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I", "R", "S")
        txt_AmountTotPay.ReadOnly = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I", "R", "S", "")
        txt_TDSAmount.ReadOnly = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I", "R", "S", "")
        txt_TDSBaseAmount.ReadOnly = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I", "R", "S", "")
        txt_WCTAmount.ReadOnly = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I", "R", "S", "")

        GridBind(PaymentType)

        btnAddDeduction.Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I", "T")

        ''''''''Information Tab
        UltraTabControl2.Tabs("DR").Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I")
        UltraTabControl2.Tabs("RW").Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "I")
    End Sub

    Private Sub GridBind(PaymentType As String)
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "R", "S") Then
            myView.mainGrid.MainConf("HIDECOLS") = "AmountEXV, TDSAmount,WCTAmount, AmountCess,AmountDiscount"
        End If
        myView.mainGrid.Genwidth(True)
        Dim str1 As String = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""Amount,TDSAmount,WCTAmount,AmountEXV,AmountDiscount""/></BAND>"
        myView.mainGrid.PrepEdit(str1)


        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "R", "S") Then
            myViewPP.mainGrid.MainConf("HIDECOLS") = "AmountPen, AmountWO, AmountDiscount"
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "I") Then
            myViewPP.mainGrid.MainConf("HIDECOLS") = "Amount"
        End If
        myViewPP.mainGrid.Genwidth(True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY =""PaymentID, AdvancePaymentID, InvoiceID, Fac, PaymentItemType, Amount""/><COL KEY=""AmountPen"" CAPTION=""Penalty""/><COL KEY=""AmountWO"" CAPTION=""Bad Debt""/><COL KEY=""AmountDiscount"" CAPTION=""Discount""/></BAND>"
        myViewPP.mainGrid.PrepEdit(str1)
    End Sub

    Private Sub cmb_CompanyID_Leave(sender As Object, e As EventArgs) Handles cmb_CompanyID.Leave, cmb_CompanyID.AfterCloseUp
        HandleCompanyID(myUtils.cValTN(cmb_CompanyID.Value))
        fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
    End Sub

    Private Sub HandleCompanyID(CompanyID As Integer)
        cm.EndCurrentEdit()
        UltraTabControl2.Tabs("OP").Visible = False
        txt_OpenAdjAmount.Value = DBNull.Value
        If myUtils.cValTN(CompanyID) > 0 Then
            Dim rCompany As DataRow = Me.Controller.CommonData.rCompany(CompanyID)
            UltraTabControl2.Tabs("OP").Visible = (myRow("Dated") < rCompany("FinStartDate"))

            dvCamp.RowFilter = " CompanyID = " & myUtils.cValTN(CompanyID) & " and " & risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, myRow("Dated"), "WODate", "CompletedOn", "CampusID", 12)
        End If
    End Sub

    Private Sub txt_AmountTotPay_Leave(sender As Object, e As EventArgs) Handles txt_AmountTotPay.Leave, txt_AmountTotPay.AfterEditorButtonCloseUp
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub txt_TDSAmount_Leave(sender As Object, e As EventArgs) Handles txt_TDSAmount.Leave, txt_TDSAmount.AfterEditorButtonCloseUp
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub txt_WCTAmount_Leave(sender As Object, e As EventArgs) Handles txt_WCTAmount.Leave, txt_WCTAmount.AfterEditorButtonCloseUp
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub UltraGridPI_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridPI.AfterCellUpdate, UltraGridWR.AfterCellUpdate, UltraGridRR.AfterCellUpdate, UltraGridDR.AfterCellUpdate, UltraGridRW.AfterCellUpdate
        Dim Key As String = ""
        Dim dt As DataTable = SelectMyView(Key)
        CalculateBalance(dt, Key)
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub UltraGridPP_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridPP.AfterCellUpdate
        CalculateBalancePP(myUtils.cStrTN(cmb_PaymentType.Value))
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub CalculateAmount(PaymentType As String)
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillAmtDue.Text = myView.mainGrid.Model.GetColSumProduct("Fac,Amount", "") Else txtBillAmtDue.Text = myViewDR.mainGrid.Model.GetColSumProduct("Fac,Amount", "")
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillTDSDue.Text = myView.mainGrid.Model.GetColSumProduct("Fac,TDSAmount", "") Else txtBillTDSDue.Text = myViewDR.mainGrid.Model.GetColSumProduct("Fac,TDSAmount", "")
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillWCTDue.Text = myView.mainGrid.Model.GetColSumProduct("Fac,WCTAmount", "") Else txtBillWCTDue.Text = myViewDR.mainGrid.Model.GetColSumProduct("Fac,WCTAmount", "")
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtAmountEXV.Text = myView.mainGrid.Model.GetColSumProduct("Fac,AmountEXV", "") Else txtAmountEXV.Text = 0

        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then
            txtBillDiscountDue.Text = myView.mainGrid.Model.GetColSumProduct("Fac,AmountDiscount", "") + myViewWR.mainGrid.Model.GetColSumProduct("Fac,AmountDiscount", "") + myViewRR.mainGrid.Model.GetColSumProduct("Fac,AmountDiscount", "")
        Else
            txtBillDiscountDue.Text = myViewDR.mainGrid.Model.GetColSumProduct("Fac,AmountDiscount", "")
        End If

        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillAmtWR.Text = myViewWR.mainGrid.Model.GetColSumProduct("Fac,Amount", "") Else txtBillAmtWR.Text = 0
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillTDSWR.Text = myViewWR.mainGrid.Model.GetColSumProduct("Fac,TDSAmount", "") Else txtBillTDSWR.Text = 0
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillWCTWR.Text = myViewWR.mainGrid.Model.GetColSumProduct("Fac,WCTAmount", "") Else txtBillWCTWR.Text = 0


        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillAmtRet.Text = myViewRR.mainGrid.Model.GetColSumProduct("Fac,Amount", "") Else txtBillAmtRet.Text = myViewRW.mainGrid.Model.GetColSumProduct("Fac,Amount", "")
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillTDSRet.Text = myViewRR.mainGrid.Model.GetColSumProduct("Fac,TDSAmount", "") Else txtBillTDSRet.Text = myViewRW.mainGrid.Model.GetColSumProduct("Fac,TDSAmount", "")
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txtBillWCTRet.Text = myViewRR.mainGrid.Model.GetColSumProduct("Fac,WCTAmount", "") Else txtBillWCTRet.Text = myViewRW.mainGrid.Model.GetColSumProduct("Fac,WCTAmount", "")


        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "I") Then txt_AmountTotPay.Text = 0
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "I") Then txt_TDSAmount.Text = 0
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "I") Then txt_WCTAmount.Text = 0
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "I") Then txt_TDSBaseAmount.Text = 0

        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "R", "S") Then
            txt_AmountTotPay.Text = myViewPP.mainGrid.Model.GetColSumProduct("Fac,Amount", "") +
                (-1 * myView.mainGrid.Model.GetColSumProduct("Fac,Amount", "")) + myUtils.cValTN((txt_AmountRO.Value))
        End If

        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txt_NewAmount.Text = myUtils.cValTN(txt_AmountTotPay.Text) - (myUtils.cValTN(txtBillAmtDue.Text) + myUtils.cValTN(txtBillAmtWR.Text) + myUtils.cValTN(txtBillAmtRet.Text) + myUtils.cValTN(txt_OpenAdjAmount.Text) + myUtils.cValTN(txt_AmountRO.Text)) Else txt_NewAmount.Text = 0
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txt_NewTDSAmount.Text = myUtils.cValTN(txt_TDSAmount.Text) - (myUtils.cValTN(txtBillTDSDue.Text) + myUtils.cValTN(txtBillTDSWR.Text) + myUtils.cValTN(txtBillTDSRet.Text)) Else txt_NewTDSAmount.Text = 0
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "T") Then txt_NewWCTAmount.Text = myUtils.cValTN(txt_WCTAmount.Text) - (myUtils.cValTN(txtBillWCTDue.Text) + myUtils.cValTN(txtBillWCTWR.Text) + myUtils.cValTN(txtBillWCTRet.Text)) Else txt_NewWCTAmount.Text = 0
    End Sub

    Private Sub ReadOnlyCtl()
        If (myView.mainGrid.myDv.Table.Select.Length > 0) OrElse (myViewWR.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewRR.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewDR.mainGrid.myDv.Table.Select.Length > 0) OrElse (myViewRW.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0) OrElse (myViewPP.mainGrid.myDv.Table.Select.Length > 0) Then
            EnableControls(True)
        Else
            EnableControls(False)
        End If
    End Sub

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleCompanyID(myUtils.cValTN(cmb_CompanyID.Value))
        fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
    End Sub

    Private Function SelectMyView(Optional ByRef Key As String = "", Optional ByRef OldInvID As String = "", Optional ByRef Oview As clsWinView = Nothing) As DataTable
        Dim dt As DataTable = Nothing
        If Not IsNothing(UltraTabControl2.SelectedTab) Then
            Key = UltraTabControl2.SelectedTab.Key
            Select Case myUtils.cStrTN(UltraTabControl2.SelectedTab.Key).Trim.ToUpper
                Case "PI"
                    dt = myView.mainGrid.myDv.Table
                    Oview = myView
                    OldInvID = strPI
                Case "WR"
                    dt = myViewWR.mainGrid.myDS.Tables(0)
                    Oview = myViewWR
                    OldInvID = strWR
                Case "RR"
                    dt = myViewRR.mainGrid.myDS.Tables(0)
                    Oview = myViewRR
                    OldInvID = strRR
                Case "DR"
                    dt = myViewDR.mainGrid.myDv.Table
                    Oview = myViewDR
                    OldInvID = strDR
                Case "RW"
                    dt = myViewRW.mainGrid.myDS.Tables(0)
                    Oview = myViewRW
                    OldInvID = strRW
                Case "TA"
                    dt = myViewAdv.mainGrid.myDS.Tables(0)
                    Oview = myViewAdv
                Case "PP"
                    dt = myViewPP.mainGrid.myDv.Table
                    Oview = myViewPP
            End Select
        End If
        Return dt
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim Key As String = "", OldInvoiceID As String = ""
        Dim dt As DataTable = SelectMyView(Key, OldInvoiceID)
        If Not IsNothing(dt) Then
            If myUtils.IsInList(myUtils.cStrTN(Key), "TA") Then
                AdvanceSelectADV()
            ElseIf myUtils.IsInList(myUtils.cStrTN(Key), "PP") Then
                AdvanceSelectPP()
            Else
                AdvanceSelect(Key, OldInvoiceID, dt)
            End If
            CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
        End If
    End Sub

    Private Sub AdvanceSelect(Key As String, OldInvoiceID As String, dt As DataTable)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_campusid.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@postingdate", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@invoiceidcsv", myUtils.MakeCSV(dt.Select, "InvoiceID"), GetType(Integer), True))
        Params.Add(New clsSQLParam("@oldinvoiceidcsv", OldInvoiceID, GetType(Integer), True))

        Dim Params2 As New List(Of clsSQLParam)
        Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
        Params2.Add(New clsSQLParam("@Dated", Format(Me.dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))

        Dim rr1() As DataRow = Me.PopulateDataRows(Key, Me.AdvancedSelect(Key, Params), Params2)
        If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
            For Each r2 As DataRow In rr1
                Dim r3 As DataRow = myUtils.CopyOneRow(r2, dt)
            Next
            CalculateBalance(dt, Key)
            ReadOnlyCtl()
        End If
    End Sub

    Private Sub AdvanceSelectADV()
        If (Not myUtils.NullNot(fPaymentMode.cmb_ImprestEmployeeID.Value)) AndAlso (Not myUtils.NullNot(cmb_CompanyID.Value)) Then
            If Not IsNothing(myViewAdv) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(fPaymentMode.cmb_ImprestEmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewAdv.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))
                Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_campusid.Value), GetType(Integer), False))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("TOURVOUCH", Params), Params2)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewAdv.mainGrid.myDS.Tables(0))
                        r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    Next
                    CalculateBalanceAdv()
                End If
            End If
            EnableCtl()
        End If
    End Sub

    Private Sub AdvanceSelectPP()
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@paymentidcsv", myUtils.MakeCSV(myViewPP.mainGrid.myDv.Table.Select, "AdvancePaymentID"), GetType(Integer), True))
        Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_campusid.Value), GetType(Integer), False))

        Dim Params2 As New List(Of clsSQLParam)
        Params2.Add(New clsSQLParam("ID", frmIDX, GetType(Integer), False))
        Params2.Add(New clsSQLParam("Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))

        Dim rr1() As DataRow = Me.PopulateDataRows("PP", Me.AdvancedSelect("PP", Params), Params2)
        If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
            For Each r2 As DataRow In rr1
                Dim r3 As DataRow = myUtils.CopyOneRow(r2, myViewPP.mainGrid.myDv.Table)
                r3("PaymentID") = frmIDX
                r3("AdvVouchNum") = myUtils.cStrTN(r2("VouchNum"))
                r3("AdvPaymentInfo") = myUtils.cStrTN(r2("PaymentInfo"))
                r3("AdvDated") = myUtils.cStrTN(r2("Dated"))
                r3("AdvPaymentType") = myUtils.cStrTN(r2("PaymentType"))
            Next
            CalculateBalancePP(myUtils.cStrTN(cmb_PaymentType.Value))
        End If
    End Sub

    Private Sub CalculateBalance(dt As DataTable, Optional ByVal key As String = "")
        Dim Amt As Decimal = 0

        For Each r1 As DataRow In dt.Select
            Dim VendorCredit As Boolean = myFuncsBase.VendorCredit(r1)

            If VendorCredit = False Then
                r1("Fac") = -1
            Else
                r1("Fac") = 1
            End If

            Amt = 0
            If r1.Table.Columns.Contains("AmountWO") Then
                Amt = myUtils.cValTN(Amt) + myUtils.cValTN(r1("AmountWO"))
            End If

            If r1.Table.Columns.Contains("AmountPen") Then
                Amt = myUtils.cValTN(Amt) + myUtils.cValTN(r1("AmountPen"))
            End If

            If r1.Table.Columns.Contains("AmountRet") Then
                Amt = myUtils.cValTN(Amt) + myUtils.cValTN(r1("AmountRet"))
            End If

            If (Not r1.Table.Columns.Contains("AmountWO")) AndAlso (Not r1.Table.Columns.Contains("AmountPen")) AndAlso (Not r1.Table.Columns.Contains("AmountRet")) Then
                Amt = myUtils.cValTN(r1("Amount"))
            End If

            If myUtils.IsInList(myUtils.cStrTN(key), "PI") Then
                r1("Balance") = myUtils.cValTN(r1("PreBalance")) - (myUtils.cValTN(Amt) + myUtils.cValTN(r1("TDSAmount")) + myUtils.cValTN(r1("WCTAmount")) + myUtils.cValTN(r1("AmountCess")) + myUtils.cValTN(r1("AmountEXV")) + myUtils.cValTN(r1("AmountDiscount")))
            ElseIf myUtils.IsInList(myUtils.cStrTN(key), "DR") Then
                r1("Balance") = myUtils.cValTN(r1("PreBalance")) - (myUtils.cValTN(Amt) + myUtils.cValTN(r1("TDSAmount")) + myUtils.cValTN(r1("WCTAmount")) + myUtils.cValTN(r1("AmountCess")) + myUtils.cValTN(r1("AmountDiscount")))
            ElseIf myUtils.IsInList(myUtils.cStrTN(key), "WR", "RR") Then
                r1("Balance") = myUtils.cValTN(r1("PreBalance")) - (myUtils.cValTN(Amt) + myUtils.cValTN(r1("TDSAmount")) + myUtils.cValTN(r1("WCTAmount")) + myUtils.cValTN(r1("AmountDiscount")))
            Else
                r1("Balance") = myUtils.cValTN(r1("PreBalance")) - (myUtils.cValTN(Amt) + myUtils.cValTN(r1("TDSAmount")) + myUtils.cValTN(r1("WCTAmount")))
            End If
        Next
    End Sub

    Private Sub CalculateBalanceAdv()
        For Each r1 As DataRow In myViewAdv.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub CalculateBalancePP(PaymentType As String)
        For Each r1 As DataRow In myViewPP.mainGrid.myDv.Table.Select
            If myUtils.IsInList(myUtils.cStrTN(r1("AdvPaymentType")), "R") Then
                r1("Fac") = -1
            Else
                r1("Fac") = 1
            End If
            If myUtils.IsInList(myUtils.cStrTN(PaymentType), "I") Then r1("Amount") = myUtils.cValTN(r1("AmountPen")) + myUtils.cValTN(r1("AmountWO")) + myUtils.cValTN(r1("AmountDiscount"))
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub txt_OpenAdjAmount_Leave(sender As Object, e As EventArgs) Handles txt_OpenAdjAmount.Leave, txt_OpenAdjAmount.AfterEditorButtonCloseUp
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub txt_AmountRO_Leave(sender As Object, e As EventArgs) Handles txt_AmountRO.Leave
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim Oview As clsWinView = Nothing
        Dim dt As DataTable = SelectMyView(,, Oview)
        If Not IsNothing(Oview) Then
            Oview.mainGrid.ButtonAction("del")
        End If

        Dim Key As String = UltraTabControl2.SelectedTab.Key
        If myUtils.IsInList(myUtils.cStrTN(Key), "TA") Then
            EnableCtl()
        Else
            ReadOnlyCtl()
        End If
        CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub EnableCtl()
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            WinFormUtils.SetReadOnly(fPaymentMode, False, True)
        Else
            WinFormUtils.SetReadOnly(fPaymentMode, False, False)
        End If
    End Sub

    Private Sub UltraGridAdv_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridAdv.AfterCellUpdate
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            CalculateBalanceAdv()
            CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
        End If
    End Sub

    Private Sub txt_Remark_Leave(sender As Object, e As EventArgs) Handles txt_Remark.Leave
        cm.EndCurrentEdit()
        fPaymentMode.txt_PaymentInfo.Value = fPaymentMode.SetPaymentInfo(myUtils.cStrTN(Me.myRow("Remark")))
    End Sub

    Private Sub btnAddDeduction_Click(sender As Object, e As EventArgs) Handles btnAddDeduction.Click
        Dim oView As clsView = Nothing
        Select Case myUtils.cStrTN(UltraTabControl2.SelectedTab.Key).Trim.ToUpper
            Case "PI"
                oView = myView
            Case "DR"
                oView = myViewDR
        End Select

        Dim f As New frmPaymentItemDet
        f.fMat = Me
        oView.ContextRow = oView.mainGridBase.ActiveRow
        If Not IsNothing(oView.ContextRow) Then
            If f.PrepForm(oView, EnumfrmMode.acEditM, oView.ContextRow.CellValue("paymentitemid")) Then
                f.ShowDialog()
                oView.ContextRow.CellValue("amountcess") = f.myView.mainGrid.Model.GetColSum("amount", "paymentitemid=" & oView.ContextRow.CellValue("paymentitemid"))
            End If
        End If
    End Sub

    Private Sub UltraTabControl2_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl2.ActiveTabChanged
        If Me.FormPrepared AndAlso (e.Tab.Key = "PI" OrElse e.Tab.Key = "DR") Then
            btnAddDeduction.Visible = myUtils.IsInList(myUtils.cStrTN(myRow("PaymentType")), "I", "T")
        Else
            btnAddDeduction.Visible = False
        End If

        If Me.FormPrepared AndAlso (e.Tab.Key = "PI" OrElse e.Tab.Key = "TA" OrElse e.Tab.Key = "PP") Then
            btnCopyAmt.Visible = True
        Else
            btnCopyAmt.Visible = False
        End If
    End Sub

    Private Sub btnCopyAmt_Click(sender As Object, e As EventArgs) Handles btnCopyAmt.Click
        Dim Key As String = "", OldInvoiceID As String = ""
        Dim dt As DataTable = SelectMyView(Key, OldInvoiceID)
        If Not IsNothing(dt) AndAlso myUtils.IsInList(Key, "PP", "PI", "TA") Then
            For Each r1 As DataRow In dt.Select("Amount is Null")
                r1("Amount") = myUtils.cValTN(r1("PreBalance"))
            Next
            If myUtils.IsInList(myUtils.cStrTN(Key), "TA") Then
                CalculateBalanceAdv()
            ElseIf myUtils.IsInList(myUtils.cStrTN(Key), "PP") Then
                CalculateBalancePP(myUtils.cStrTN(cmb_PaymentType.Value))
            Else
                CalculateBalance(dt, Key)
            End If
            CalculateAmount(myUtils.cStrTN(cmb_PaymentType.Value))
        End If
    End Sub

    Private Sub GetMissingVoucherNo()
        Dim FormatType As String = ""
        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "CA") Then FormatType = "C" Else FormatType = "J"
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@FormatType", "'" & myUtils.cStrTN(FormatType) & "'", GetType(String), False))
        Dim oRet As clsProcOutput = GenerateParamsOutput("missingdocsysnum", Params)
        If oRet.Success Then
            myRow("ForceAVSysNum") = oRet.ID
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub
End Class