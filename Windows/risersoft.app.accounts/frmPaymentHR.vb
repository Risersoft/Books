Imports risersoft.app.mxform
Imports Infragistics.Win.UltraWinGrid

Public Class frmPaymentHR
    Inherits frmMax
    Dim myViewAL, myViewLP, myViewBP, myViewTV, myViewMode, myViewAdv As New clsWinView, fPaymentMode As frmPaymentMode, oView As clsWinView = Nothing
    Friend fItem As frmPaymentContraItem

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Additional"), Me.UEGB_ItemList)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Mode"), Me.UEGB_ItemList)


        fItem = New frmPaymentContraItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel4, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.Enabled = False

        fPaymentMode = New frmPaymentMode
        AddHandler fPaymentMode.PaymentModeChanged, AddressOf PaymentModeChanged
        fPaymentMode.AddToPanel(Me.UltraExpandableGroupBoxPanel5, True, System.Windows.Forms.DockStyle.Fill)

        myView.SetGrid(UltraGridPV)
        myViewAL.SetGrid(UltraGridAL)
        myViewLP.SetGrid(UltraGridLP)
        myViewBP.SetGrid(UltraGridBP)
        myViewTV.SetGrid(UltraGridTV)
        myViewMode.SetGrid(UltraGridMode)
        myViewAdv.SetGrid(Me.UltraGridAdv)

        cmb_EmployeeID.ReadOnly = True

        AddHandler fItem.txt_Amount.Leave, AddressOf txt_Amount_Leave
        AddHandler fItem.txt_Amount.AfterEditorButtonCloseUp, AddressOf txt_Amount_Leave
    End Sub

    Private Sub PaymentModeChanged(sender As Object, PaymentMode As String, IgnoreExpenseVoucher As Boolean)
        If myUtils.IsInList(myUtils.cStrTN(PaymentMode), "IM") AndAlso IgnoreExpenseVoucher = False Then
            UltraGroupBox1.Enabled = True
            EnableCtl()
        Else
            UltraGroupBox1.Enabled = False
        End If
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPaymentHRModel = Me.InitData("frmPaymentHRModel", oView, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oView) Then
            fPaymentMode.InitPanel(Me, True)
            Me.FormPrepared = True
            If myUtils.cValTN(Me.vBag("PaymentHRVouchID")) > 0 Then
                GeneratePaymentHR(myUtils.cValTN(Me.vBag("PaymentHRVouchID")))
            End If

            HandlePaymentType(myUtils.cStrTN(myRow("PaymentType")), myUtils.cValTN(myRow("CompanyID")), myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))
            oView = SelectMyView()
            If Not IsNothing(oView) Then
                ReadOnlyCtrl(oView.mainGrid.myDS.Tables(0))
            End If

            CalculateBalanceAdv()
            CalculateBalanceTA()

            CalculateAmt(myUtils.cStrTN(myRow("PaymentType")))

            If frmMode = EnumfrmMode.acEditM Then
                myRow("ForceAVSysNum") = DBNull.Value
                ChkGetMissingNo.Visible = False
            End If

            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If
        End If
        Return Me.FormPrepared
    End Function

    Private Sub GeneratePaymentHR(PaymentHRVouchID As Integer)
        Dim rr1() As DataRow = Me.GenerateIDOutput("PaymentHRVouch", PaymentHRVouchID).Data.Tables(0).Select("PaymentID is NULL")
        If rr1.Length > 0 Then
            myRow("EmployeeID") = myUtils.cValTN(rr1(0)("EmployeeID"))
            myRow("CompanyID") = myUtils.cValTN(rr1(0)("CompanyID"))
            myRow("VendorID") = myUtils.cValTN(rr1(0)("ContractorID"))
            If myUtils.cValTN(myRow("EmployeeID")) > 0 Then
                myRow("PaymentType") = "E"
            ElseIf myUtils.cValTN(myRow("VendorID")) > 0 Then
                myRow("PaymentType") = "V"
            Else
                myRow("PaymentType") = "C"
            End If
            Dim r2 As DataRow = myUtils.CopyOneRow(rr1(0), myView.mainGrid.myDS.Tables(0))
            CalculateAmt(myUtils.cStrTN(myRow("PaymentType")))
        Else
            MsgBox("Payment Already Created.", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
            Me.FormPrepared = False
        End If
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("PV"))
            myViewAL.PrepEdit(Me.Model.GridViews("AL"))
            myViewLP.PrepEdit(Me.Model.GridViews("LP"))
            myViewBP.PrepEdit(Me.Model.GridViews("BP"))
            myViewTV.PrepEdit(Me.Model.GridViews("TV"))
            myViewMode.PrepEdit(Me.Model.GridViews("Mode"))
            myViewAdv.PrepEdit(Me.Model.GridViews("Adv"))

            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            myWinSQL.AssignCmb(Me.dsCombo, "Employee", "", Me.cmb_EmployeeID)
            myWinSQL.AssignCmb(Me.dsCombo, "PaymentType", "", Me.cmb_PaymentType)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)

            fPaymentMode.BindModel(NewModel)
            fItem.BindModel(NewModel)
            fItem.fPaymentMode.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If (myViewMode.mainGrid.myDv.Table.Select.Length = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
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

    Private Sub ReadOnlyCtrl(dt As DataTable)
        Dim bool As Boolean = False
        btnSelect.Enabled = True
        If dt.Select.Length > 0 Then
            bool = True
            btnSelect.Enabled = False
        End If
        cmb_CompanyID.ReadOnly = bool
        cmb_PaymentType.ReadOnly = bool
        cmb_VendorID.ReadOnly = bool
    End Sub

    Private Sub cmb_PaymentType_Leave(sender As Object, e As EventArgs) Handles cmb_PaymentType.Leave, cmb_PaymentType.AfterCloseUp
        HandlePaymentType(myUtils.cStrTN(cmb_PaymentType.Value), myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
    End Sub

    Private Sub HandlePaymentType(PaymentType As String, CompanyID As Integer, Dated As Date)
        HideTabPage(False)

        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "C", "V") Then
            UltraTabControl2.Tabs("PV").Visible = True
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "E") Then
            UltraTabControl2.Tabs("PV").Visible = True
            UltraTabControl2.Tabs("AL").Visible = True
            UltraTabControl2.Tabs("LP").Visible = True
            UltraTabControl2.Tabs("TV").Visible = True
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "B") Then
            UltraTabControl2.Tabs("BP").Visible = True
        End If
        EnableCtl(PaymentType, CompanyID, Dated)
    End Sub

    Private Sub EnableCtl(PaymentType As String, CompanyID As Integer, Dated As Date)
        Dim bool As Boolean = False

        oView = SelectMyView()
        If Not IsNothing(oView) Then
            If oView.mainGrid.myDS.Tables(0).Select.Length = 0 Then
                bool = True
            End If
        End If
        Me.cm.EndCurrentEdit()

        If bool = True Then cmb_CompanyID.ReadOnly = False
        lblContractor.Visible = True
        cmb_VendorID.Visible = True
        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "C") Then
            If frmMode = EnumfrmMode.acAddM AndAlso cmb_CompanyID.Rows.Count = 1 Then myRow("CompanyID") = myUtils.cValTN(cmb_CompanyID.Rows(0).Cells("CompanyID").Value)
            If bool = True Then cmb_EmployeeID.Value = DBNull.Value
            If bool = True Then cmb_VendorID.Value = DBNull.Value
            btnAddEmp.Visible = True
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "V") Then
            If bool = True Then cmb_VendorID.ReadOnly = False
            If bool = True Then cmb_EmployeeID.Value = DBNull.Value
            HideCtl(False)
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "E") Then
            HideCtl(True)
            cmb_CompanyID.ReadOnly = True
            If bool = True Then btnSelect.Enabled = True
            If bool = True Then cmb_CompanyID.Value = DBNull.Value
            If bool = True Then cmb_VendorID.Value = DBNull.Value
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "B") Then
            If frmMode = EnumfrmMode.acAddM AndAlso cmb_CompanyID.Rows.Count = 1 Then myRow("CompanyID") = myUtils.cValTN(cmb_CompanyID.Rows(0).Cells("CompanyID").Value)
            If bool = True Then cmb_EmployeeID.Value = DBNull.Value
            If bool = True Then cmb_VendorID.Value = DBNull.Value
        End If

        fPaymentMode.HandleItem(CompanyID, Dated)
    End Sub

    Private Sub HideCtl(Bool As Boolean)
        lblContractor.Visible = Not Bool
        cmb_VendorID.Visible = Not Bool

        lblEmp.Visible = Bool
        cmb_EmployeeID.Visible = Bool
        btnSelect.Visible = Bool
    End Sub

    Private Sub HideTabPage(Bool As Boolean)
        UltraTabControl2.Tabs("PV").Visible = Bool
        UltraTabControl2.Tabs("AL").Visible = Bool
        UltraTabControl2.Tabs("LP").Visible = Bool
        UltraTabControl2.Tabs("BP").Visible = Bool
        UltraTabControl2.Tabs("TV").Visible = Bool
        btnSelect.Visible = Bool
        btnAddEmp.Visible = False
        cmb_VendorID.ReadOnly = True
    End Sub

    Private Sub CalculateAmt(PaymentType As String)
        Dim Amt As Decimal = 0, AmtDiff As Decimal = 0

        If myUtils.IsInList(myUtils.cStrTN(PaymentType), "C", "V") Then
            Amt = CalculateGrid(myView, "TotalAmount")
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "E") Then
            Amt = CalculateGrid(myView, "TotalAmount")
            Amt = Amt + CalculateGrid(myViewAL, "Amount")
            Amt = Amt + CalculateGrid(myViewLP, "Amount")
            Amt = Amt + CalculateGrid(myViewTV, "TotalPayment")
        ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "B") Then
            Amt = CalculateGrid(myViewBP, "Amount")
            AmtDiff = CalculateGrid(myViewBP, "AmountDiff")
        End If
        Dim RoundUpAmt As Decimal = Math.Ceiling(Amt + AmtDiff)
        myRow("AmountRO") = RoundUpAmt - Amt - AmtDiff       'rounding off.
        myRow("AmountTotPay") = RoundUpAmt
        txt_AmountTotPay.Value = RoundUpAmt


        CalculateNewAmt()
    End Sub

    Private Function CalculateGrid(oView1 As clsWinView, AmtField As String) As Decimal
        Dim Amt As Decimal
        Amt = oView1.mainGrid.Model.GetColSum(AmtField)
        Return Amt
    End Function

    Private Sub btnAddEmp_Click(sender As Object, e As EventArgs) Handles btnAddEmp.Click
        Me.InitError()
        cm.EndCurrentEdit()
        If myUtils.NullNot(Me.cmb_PaymentType.Value) Then WinFormUtils.AddError(Me.cmb_PaymentType, "Select Payment Type")
        If myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "C", "B") AndAlso myUtils.NullNot(Me.cmb_CompanyID.Value) Then WinFormUtils.AddError(Me.cmb_CompanyID, "Select Company Name")

        If Me.CanSave() Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@paymentid", frmIDX, GetType(Integer), False))
            Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@idcsv", myUtils.MakeCSV(oView.mainGrid.myDS.Tables(0).Select, "PaymentHRVouchID"), GetType(Integer), True))
            Dim rr1() As DataRow = Me.AdvancedSelect("PVALLEMP", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                For Each r1 As DataRow In rr1
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDS.Tables(0))
                Next
                myView.mainGrid.myDS.AcceptChanges()
                CalculateAmt(myUtils.cStrTN(cmb_PaymentType.Value))
                ReadOnlyCtrl(myView.mainGrid.myDS.Tables(0))
            End If
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim key As String = ""
        oView = SelectMyView(, key)
        If Not IsNothing(oView) Then
            oView.mainGrid.ButtonAction("del")
            oView.mainGrid.myDS.AcceptChanges()
            CalculateAmt(myUtils.cStrTN(cmb_PaymentType.Value))
            ReadOnlyCtrl(oView.mainGrid.myDS.Tables(0))
        End If
    End Sub

    Private Sub cmb_CompanyID_Leave(sender As Object, e As EventArgs) Handles cmb_CompanyID.Leave, cmb_CompanyID.AfterCloseUp
        HandleCompany()
    End Sub

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleCompany()
    End Sub

    Private Sub HandleCompany()
        fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))

        If Not IsNothing(fItem.myRow) Then
            fItem.fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
        End If
    End Sub

    Private Sub UltraGridBP_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridBP.AfterCellUpdate
        CalculateAmt(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@paymentid", frmIDX, GetType(Integer), False))
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim rr() As DataRow = Me.AdvancedSelect("employee", Params)

        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            cmb_EmployeeID.Value = myUtils.cValTN(rr(0)("EmployeeID"))

            If Not IsNothing(cmb_EmployeeID.SelectedRow) Then
                cmb_CompanyID.Value = myUtils.cValTN(cmb_EmployeeID.SelectedRow.Cells("CompanyID").Value)
                fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))

                If Not IsNothing(fItem.myRow) Then
                    fItem.fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
                End If
            End If
        End If
    End Sub

    Private Function SelectMyView(Optional ByRef SysID As String = "", Optional ByRef Key As String = "") As clsWinView
        oView = Nothing
        If Not IsNothing(UltraTabControl2.SelectedTab) Then
            Key = myUtils.cStrTN(UltraTabControl2.SelectedTab.Key)
            Select Case myUtils.cStrTN(UltraTabControl2.SelectedTab.Key).Trim.ToUpper
                Case "PV"
                    oView = myView
                    SysID = "PaymentHRVouchID"
                Case "AL"
                    oView = myViewAL
                    SysID = "EmpLoanID"
                Case "LP"
                    oView = myViewLP
                    SysID = "EmpLoanPaybackID"
                Case "BP"
                    oView = myViewBP
                    SysID = "PayPeriodBenefitID"
                Case "TV"
                    oView = myViewTV
                    SysID = "TourVouchID"
            End Select
        End If
        Return oView
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim oView1 As clsWinView = Nothing, SysID As String = "", key As String = ""
        Me.InitError()
        cm.EndCurrentEdit()
        If myUtils.NullNot(Me.cmb_PaymentType.Value) Then WinFormUtils.AddError(Me.cmb_PaymentType, "Select Payment Type")
        If myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "C", "B", "V") AndAlso myUtils.NullNot(Me.cmb_CompanyID.Value) Then WinFormUtils.AddError(Me.cmb_CompanyID, "Select Company Name")
        If myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "E") AndAlso myUtils.NullNot(Me.cmb_EmployeeID.Value) Then WinFormUtils.AddError(Me.cmb_EmployeeID, "Select Employee Name")
        If myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "V") AndAlso myUtils.NullNot(Me.cmb_VendorID.Value) Then WinFormUtils.AddError(Me.cmb_VendorID, "Select Contractor Name")
        If Me.CanSave() Then
            oView1 = SelectMyView(SysID, key)
            AdvanceSelect(oView1, SysID, key)
        End If
    End Sub

    Private Sub AdvanceSelect(oView1 As clsWinView, SysID As String, key As String)
        If Not IsNothing(oView1) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@paymentid", frmIDX, GetType(Integer), False))
            Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(cmb_EmployeeID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ContractorID", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@paymenttype", "'" & myUtils.cStrTN(cmb_PaymentType.Value) & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@idcsv", myUtils.MakeCSV(oView.mainGrid.myDS.Tables(0).Select, SysID), GetType(Integer), True))
            Params.Add(New clsSQLParam("@compstdate", Format(cmb_CompanyID.SelectedRow.Cells("finStartDate").Value, "dd-MMM-yyyy"), GetType(DateTime), False))
            Dim rr1() As DataRow = Me.AdvancedSelect(key, Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                For Each r1 As DataRow In rr1
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, oView1.mainGrid.myDS.Tables(0))
                Next
                oView1.mainGrid.myDS.AcceptChanges()
                CalculateAmt(myUtils.cStrTN(cmb_PaymentType.Value))
                ReadOnlyCtrl(oView1.mainGrid.myDS.Tables(0))
            End If
        End If
    End Sub

    Private Sub btnAddMode_Click(sender As Object, e As EventArgs) Handles btnAddMode.Click
        If myViewMode.mainGrid.myDv.Table.Select.Length = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myViewMode.mainGrid.ButtonAction("add")
            fItem.Focus()
        End If
    End Sub

    Private Sub btnDelMode_Click(sender As Object, e As EventArgs) Handles btnDelMode.Click
        myViewMode.mainGrid.ButtonAction("del")
        CalculateNewAmt()
        If myViewMode.mainGrid.myDv.Table.Select.Length = 0 Then
            fItem.FormPrepared = False
            fItem.Enabled = False
        End If
    End Sub

    Private Sub UltraGridMode_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridMode.AfterRowActivate
        Me.InitError()
        myViewMode.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewMode.mainGrid.myGrid.ActiveRow)
        If fItem.PrepForm(r1) Then
            fItem.myView.mainGrid.myDv.RowFilter = "PaymentItemContraID = " & myUtils.cValTN(myViewMode.mainGrid.myGrid.ActiveRow.Cells("PaymentItemContraID").Value)
            fItem.EnableCtl()
            fItem.Enabled = True
        End If
    End Sub

    Private Sub UltraGridMode_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridMode.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub CalculateNewAmt()
        Dim Amt As Decimal = 0
        If Not fItem.myRow Is Nothing Then fItem.cm.EndCurrentEdit()
        Amt = CalculateGrid(myViewMode, "Amount")
        txtAmount.Value = myUtils.cValTN(Amt)
        txt_NewAmount.Value = myUtils.cValTN(txt_AmountTotPay.Value) - myUtils.cValTN(Amt)
    End Sub

    Private Sub txt_Amount_Leave(sender As Object, e As EventArgs)
        CalculateNewAmt()
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        CalculateNewAmt()
    End Sub

    Private Sub txt_Remark_Leave(sender As Object, e As EventArgs) Handles txt_Remark.Leave
        cm.EndCurrentEdit()
        fPaymentMode.txt_PaymentInfo.Value = fPaymentMode.SetPaymentInfo(myUtils.cStrTN(Me.myRow("Remark")))
    End Sub

    Private Sub btnAddAdv_Click(sender As Object, e As EventArgs) Handles btnAddAdv.Click
        If (Not myUtils.NullNot(fPaymentMode.cmb_ImprestEmployeeID.Value)) AndAlso (Not myUtils.NullNot(cmb_CompanyID.Value)) Then
            If Not IsNothing(myViewAdv) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(fPaymentMode.cmb_ImprestEmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewAdv.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("Adv", Params), Params2)
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

    Private Sub btnDelAdv_Click(sender As Object, e As EventArgs) Handles btnDelAdv.Click
        myViewAdv.mainGrid.ButtonAction("del")
        EnableCtl()
    End Sub

    Private Sub EnableCtl()
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            WinFormUtils.SetReadOnly(fPaymentMode, False, True)
        Else
            WinFormUtils.SetReadOnly(fPaymentMode, False, False)
        End If
    End Sub

    Private Sub CalculateBalanceAdv()
        For Each r1 As DataRow In myViewAdv.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub UltraGridAdv_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridAdv.AfterCellUpdate
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            CalculateBalanceAdv()
            CalculateAmt(myUtils.cStrTN(cmb_PaymentType.Value))
        End If
    End Sub

    Public Sub AdvanceSelectTA()
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(fItem.fPaymentMode.cmb_ImprestEmployeeID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@idcsv", myUtils.MakeCSV(fItem.myView.mainGrid.myDv.Table.Select, "AdvanceVouchID"), GetType(Integer), True))

        Dim Params2 As New List(Of clsSQLParam)
        Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
        Dim rr1() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("TA", Params), Params2)
        If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
            For Each r1 As DataRow In rr1
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, fItem.myView.mainGrid.myDv.Table)
                r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                r2("ImprestEmployeeID") = myUtils.cValTN(r1("EmployeeID"))
                r2("PaymentItemContraID") = fItem.myRow("PaymentItemContraID")
            Next
            CalculateBalanceTA()
        End If
    End Sub

    Public Sub CalculateBalanceTA()
        For Each r1 As DataRow In fItem.myView.mainGrid.myDv.Table.Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
        CalculateAmt(myUtils.cStrTN(cmb_PaymentType.Value))
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