Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmPaymentContraItem
    Inherits frmMax
    Friend fMat As frmMax, fPaymentMode As frmPaymentMode

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        fPaymentMode = New frmPaymentMode
        AddHandler fPaymentMode.PaymentModeChanged, AddressOf PaymentModeChanged
        fPaymentMode.AddtoTab(Me.UltraTabControl1, "mode", True)
        myView.SetGrid(UltraGridAdvReq)
    End Sub

    Private Sub PaymentModeChanged(sender As Object, PaymentMode As String, IgnoreExpenseVoucher As Boolean)
        If myUtils.IsInList(myUtils.cStrTN(PaymentMode), "IM") AndAlso IgnoreExpenseVoucher = False Then
            UltraTabControl2.Tabs("Adv").Visible = True
        Else
            UltraTabControl2.Tabs("Adv").Visible = False
        End If
    End Sub

    Public Overloads Sub BindModel(NewModel As clsFormDataModel)
        myView.PrepEdit(NewModel.GridViews("TA"))
    End Sub

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            myRow("CompanyID") = myUtils.cValTN(fMat.myRow("CompanyID"))
            fPaymentMode.InitPanel(Me, False)
            fPaymentMode.HandleItem(myUtils.cValTN(myRow("CompanyID")), myUtils.cDateTN(fMat.myRow("Dated"), DateTime.MinValue))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        If IsNothing(myRow) Then
            VSave = True
        Else
            app.mxform.myFuncs.ValidatePaymentMode(fMat.Model, myRow.Row, fMat.Model.Name)
            If fMat.Model.CanSave Then
                cm.EndCurrentEdit()
                myRow("DispName") = myUtils.cStrTN(fPaymentMode.cmb_CashCampusID.Text)
                myRow("FullName") = myUtils.cStrTN(fPaymentMode.cmb_ImprestEmployeeID.Text)
                myRow("AccountName") = myUtils.cStrTN(fPaymentMode.cmb_BankAccountID.Text)
                myRow("PaymentModeDescrip") = myUtils.cStrTN(fPaymentMode.cmb_PaymentMode.Text)
                VSave = True
            Else
                Me.SetError(fMat.Model, myRow.Row)
            End If
        End If
    End Function

    Private Sub btnAddAdvReq_Click(sender As Object, e As EventArgs) Handles btnAddAdvReq.Click
        Me.InitError()
        If myUtils.NullNot(fPaymentMode.cmb_ImprestEmployeeID.Value) Then WinFormUtils.AddError(fPaymentMode.cmb_ImprestEmployeeID, "Select Employee")
        If Me.CanSave() Then
            If Not IsNothing(myView) Then
                If fMat.Name.ToUpper = "FRMPAYMENTHR" Then
                    CType(fMat, frmPaymentHR).AdvanceSelectTA()
                    EnableCtl()
                Else
                    CType(fMat, frmPaymentContra).AdvanceSelectTA()
                End If
            End If
        End If
    End Sub

    Private Sub btnDelAdvReq_Click(sender As Object, e As EventArgs) Handles btnDelAdvReq.Click
        If Not IsNothing(myView) Then
            myView.mainGrid.ButtonAction("del")
            If fMat.Name.ToUpper = "FRMPAYMENTHR" Then
                EnableCtl()
            End If
        End If
    End Sub

    Private Sub UltraGridAdvReq_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridAdvReq.AfterCellUpdate
        If fMat.Name.ToUpper = "FRMPAYMENTHR" Then
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then
                CType(fMat, frmPaymentHR).CalculateBalanceTA()
                EnableCtl()
            End If
        End If
    End Sub

    Public Sub EnableCtl()
        Dim rr1() As DataRow = myView.mainGrid.myDv.Table.Select("PaymentItemContraID = " & myUtils.cValTN(myRow("PaymentItemContraID")) & "")
        If rr1.Length = 0 Then
            WinFormUtils.SetReadOnly(fPaymentMode, False, True)
        Else
            WinFormUtils.SetReadOnly(fPaymentMode, False, False)
        End If
    End Sub
End Class