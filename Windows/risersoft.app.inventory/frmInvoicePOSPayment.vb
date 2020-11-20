Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports Infragistics.Win.UltraWinGrid
Public Class frmInvoicePOSPayment
    Inherits frmMax
    Friend fMat As frmInvoicePOS, fPaymentMode As frmPaymentMode

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        fPaymentMode = New frmPaymentMode
        fPaymentMode.AddtoTab(Me.UltraTabControl1, "Payment", True)
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        fPaymentMode.BindModel(NewModel)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.FormPrepared = True
            fPaymentMode.InitPanel(Me, False)
            If Not IsNothing(fMat.cmb_matdepid.SelectedRow) Then fPaymentMode.HandleItem(myUtils.cValTN(fMat.cmb_matdepid.SelectedRow.Cells("CompanyID").Value), myUtils.cDateTN(myRow("PostingDate"), DateTime.MinValue))
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If IsNothing(myRow) Then
            Exit Function
        End If

        If Me.CanSave Then
            cm.EndCurrentEdit()

            VSave = True
        End If
    End Function
End Class