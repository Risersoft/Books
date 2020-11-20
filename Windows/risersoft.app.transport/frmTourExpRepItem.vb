Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmTourExpRepItem
    Inherits frmMax
    Friend fMat As frmTourExpRep

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()

    End Sub

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.FormPrepared = True
        End If

        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If Me.CanSave Then
            If Not IsNothing(myRow) Then cm.EndCurrentEdit()
            VSave = True
        End If
    End Function

    Private Sub txt_Amount_Leave(sender As Object, e As EventArgs) Handles txt_Amount.Leave
        If fMat.UltraGridJD.Rows.Count > 0 Then
            cm.EndCurrentEdit()
            fMat.CalculateTotalAmount()
            fMat.CalculateLessAdvance()
        End If
    End Sub
End Class