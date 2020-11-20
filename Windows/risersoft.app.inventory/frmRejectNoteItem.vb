Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmRejectNoteItem
    Inherits frmMax

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(UltraGridItemHist)
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
            cm.EndCurrentEdit()
            VSave = True
        End If
    End Function
End Class