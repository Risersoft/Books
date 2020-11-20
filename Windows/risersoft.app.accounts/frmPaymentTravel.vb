Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmPaymentTravel
    Inherits frmMax
    Dim myViewFinal, myViewReturn As New clsWinView, fPaymentMode As frmPaymentMode, oView As clsWinView = Nothing
    Dim dvEmp As DataView

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

        myView.SetGrid(UltraGridAdvance)
        myViewFinal.SetGrid(UltraGridFinal)
        myViewReturn.SetGrid(UltraGridReturn)
    End Sub

    Private Sub PaymentModeChanged(sender As Object, PaymentMode As String, IgnoreExpenseVoucher As Boolean)
        If myUtils.IsInList(myUtils.cStrTN(PaymentMode), "IM") AndAlso IgnoreExpenseVoucher = False Then
            UltraTabControl2.Tabs("AR").Visible = True
            EnableCtl()
        Else
            UltraTabControl2.Tabs("AR").Visible = myUtils.IsInList(myUtils.cStrTN(myRow("PaymentType")), "AR")
        End If
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPaymentTravelModel = Me.InitData("frmPaymentTravelModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            HandlePaymentType(myUtils.cStrTN(myRow("PaymentType")))
            fPaymentMode.InitPanel(Me, True)
            fPaymentMode.HandleItem(myUtils.cValTN(myRow("CompanyID")), myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))

            oview = SelectMyView()
            If Not IsNothing(oview) Then
                ReadOnlyCtrl(oview)
            End If

            CalculateBalanceAdv()

            If frmMode = EnumfrmMode.acEditM Then
                myRow("ForceAVSysNum") = DBNull.Value
                ChkGetMissingNo.Visible = False
            End If
            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Advance"))
            myViewFinal.PrepEdit(Me.Model.GridViews("Final"))
            myViewReturn.PrepEdit(Me.Model.GridViews("Return"))

            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            myWinSQL.AssignCmb(Me.dsCombo, "PaymentType", "", Me.cmb_PaymentType)
            dvEmp = myWinSQL.AssignCmb(Me.dsCombo, "Employee", "", Me.cmb_EmployeeID,, 2)

            HandleDate(myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))
            fPaymentMode.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
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

    Private Sub ReadOnlyCtrl(oView1 As clsWinView)
        Dim bool As Boolean = False
        If oView1.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            bool = True
        End If
        cmb_PaymentType.ReadOnly = bool
        cmb_CompanyID.ReadOnly = bool
        btnSelect.Enabled = Not bool
    End Sub

    Private Sub cmb_PaymentType_Leave(sender As Object, e As EventArgs) Handles cmb_PaymentType.Leave, cmb_PaymentType.AfterCloseUp
        HandlePaymentType(myUtils.cStrTN(cmb_PaymentType.Value))
    End Sub

    Private Sub HandlePaymentType(PaymentType As String)
        HideTabPage(PaymentType)

        cmb_EmployeeID.Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "AR")
        lblEmp.Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "AR")
        btnSelect.Visible = myUtils.IsInList(myUtils.cStrTN(PaymentType), "AR")
    End Sub

    Private Sub HideTabPage(PaymentType As String)
        UltraTabControl2.Tabs("A").Visible = False
        UltraTabControl2.Tabs("F").Visible = False
        UltraTabControl2.Tabs("AR").Visible = False

        If Not myUtils.IsInList(myUtils.cStrTN(PaymentType), "") Then UltraTabControl2.Tabs(PaymentType).Visible = True
    End Sub

    Private Sub CalculateAmt()
        Dim Amt As Decimal

        If myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "A") Then
            Amt = CalculateGrid(myView, "TotalPayment")
        ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "F") Then
            Amt = CalculateGrid(myViewFinal, "TotalPayment")
        ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "AR") Then
            Amt = CalculateGrid(myViewReturn, "Amount")
        End If

        txt_AmountTotPay.Value = myUtils.cValTN(Amt)
        myRow("AmountTotPay") = myUtils.cValTN(Amt)
    End Sub

    Private Function CalculateGrid(oView1 As clsWinView, ColumnName As String) As Decimal
        Dim Amt As Decimal

        For Each r1 As DataRow In oView1.mainGrid.myDS.Tables(0).Select
            Amt = Amt + myUtils.cValTN(r1(ColumnName))
        Next
        Return Amt
    End Function

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        oView = SelectMyView()
        If Not IsNothing(oView) Then
            oView.mainGrid.ButtonAction("del")
            CalculateAmt()
            ReadOnlyCtrl(oView)
            EnableCtl()
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim SysID As String = "", oView1 As clsWinView = Nothing
        oView1 = SelectMyView(SysID)
        If myUtils.IsInList(myUtils.cStrTN(SysID), "AR") Then
            If myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "AR") Then
                AdvanceSelectADV()
            Else
                AdvanceSelectAE()
            End If
        Else
            AdvanceSelect(SysID, oView1)
        End If
    End Sub

    Private Sub UltraGridReturn_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridReturn.AfterCellUpdate
        If myViewReturn.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            CalculateBalanceAdv()
            If myUtils.IsInList(myUtils.cStrTN(cmb_PaymentType.Value), "AR") Then CalculateAmt()
        End If
    End Sub

    Private Function SelectMyView(Optional ByRef SysID As String = "") As clsWinView
        oView = Nothing
        If Not IsNothing(UltraTabControl2.SelectedTab) Then
            Select Case myUtils.cStrTN(UltraTabControl2.SelectedTab.Key).Trim.ToUpper
                Case "A"
                    SysID = "TA"
                    oView = myView
                Case "F"
                    SysID = "TE"
                    oView = myViewFinal
                Case "AR"
                    SysID = "AR"
                    oView = myViewReturn
            End Select
        End If
        Return oView
    End Function

    Private Sub AdvanceSelect(SysID As String, oView1 As clsWinView)
        Me.InitError()

        If myUtils.NullNot(Me.cmb_PaymentType.Value) Then WinFormUtils.AddError(Me.cmb_PaymentType, "Select Payment Type")
        If myUtils.NullNot(Me.cmb_CompanyID.Value) Then WinFormUtils.AddError(Me.cmb_CompanyID, "Select Company Name")

        If Me.CanSave() Then
            If Not IsNothing(oView1) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
                Params.Add(New clsSQLParam("@paymentid", frmIDX, GetType(Integer), False))
                Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(oView1.mainGrid.myDS.Tables(0).Select, "TourVouchID"), GetType(Integer), True))
                Dim rr() As DataRow = Me.AdvancedSelect(SysID, Params)

                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, oView1.mainGrid.myDS.Tables(0))
                    Next
                    oView1.mainGrid.myDS.AcceptChanges()
                    CalculateAmt()
                    ReadOnlyCtrl(oView1)
                End If
            End If
        End If
    End Sub

    Private Sub AdvanceSelectADV()
        Me.InitError()
        If myUtils.NullNot(Me.cmb_PaymentType.Value) Then WinFormUtils.AddError(Me.cmb_PaymentType, "Select Payment Type")
        If myUtils.NullNot(Me.cmb_CompanyID.Value) Then WinFormUtils.AddError(Me.cmb_CompanyID, "Select Company Name")
        If myUtils.NullNot(Me.cmb_EmployeeID.Value) Then WinFormUtils.AddError(Me.cmb_EmployeeID, "Select Employee Name")

        If Me.CanSave() Then
            If Not IsNothing(myViewReturn) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewReturn.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))
                Params.Add(New clsSQLParam("@EmployeeID", myUtils.cValTN(cmb_EmployeeID.Value), GetType(Integer), False))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("AR", Params), Params2)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewReturn.mainGrid.myDS.Tables(0))
                        r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    Next
                    CalculateBalanceAdv()
                    CalculateAmt()
                End If
            End If
            ReadOnlyCtrl(myViewReturn)
        End If
    End Sub

    Private Sub CalculateBalanceAdv()
        For Each r1 As DataRow In myViewReturn.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub cmb_CompanyID_Leave(sender As Object, e As EventArgs) Handles cmb_CompanyID.Leave, cmb_CompanyID.AfterCloseUp
        HandleCompany()
    End Sub

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleCompany()
        HandleDate(dt_Dated.Value)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim r1 As DataRow = rEmp(Params)
        If Not IsNothing(r1) Then
            cmb_EmployeeID.Value = myUtils.cValTN(r1("EmployeeID"))
        End If
    End Sub

    Private Function rEmp(Params As List(Of clsSQLParam)) As DataRow
        Dim Model As clsViewModel = Me.GenerateParamsModel("employee", Params)
        Dim fg As New frmGrid, r1 As DataRow = Nothing
        fg.myView.PrepEdit(Model)
        fg.Size = New Drawing.Size(850, 600)
        If fg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            r1 = win.myWinUtils.DataRowFromGridRow(fg.myView.mainGrid.myGrid.ActiveRow)
        End If
        Return r1
    End Function

    Private Sub HandleCompany()
        If Not IsNothing(cmb_CompanyID.SelectedRow) Then
            fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
        End If
    End Sub

    Private Sub txt_Remark_Leave(sender As Object, e As EventArgs) Handles txt_Remark.Leave
        cm.EndCurrentEdit()
        fPaymentMode.txt_PaymentInfo.Value = fPaymentMode.SetPaymentInfo(myUtils.cStrTN(Me.myRow("Remark")))
    End Sub

    Private Sub HandleDate(dated As Date)
        dvEmp.RowFilter = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "JoinDate", "LeaveDate", 12)
    End Sub

    Private Sub AdvanceSelectAE()
        If (Not myUtils.NullNot(fPaymentMode.cmb_ImprestEmployeeID.Value)) AndAlso (Not myUtils.NullNot(cmb_CompanyID.Value)) Then
            If Not IsNothing(myViewReturn) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(fPaymentMode.cmb_ImprestEmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewReturn.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("ae", Params), Params2)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewReturn.mainGrid.myDS.Tables(0))
                        r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    Next
                    CalculateBalanceAdv()
                End If
            End If
            EnableCtl()
        End If
    End Sub

    Private Sub EnableCtl()
        If myViewReturn.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            WinFormUtils.SetReadOnly(fPaymentMode, False, True)
        Else
            WinFormUtils.SetReadOnly(fPaymentMode, False, False)
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