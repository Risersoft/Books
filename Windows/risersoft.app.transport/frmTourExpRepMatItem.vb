Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmTourExpRepMatItem
    Inherits frmMax
    Friend fMat As frmTourExpRep
    Dim dvExpClass As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dvExpClass = myWinSQL.AssignCmb(NewModel.dsCombo, "ExpClass", "", Me.cmb_ExpClass,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            risersoft.app.mxform.myFuncs.TransTypeFilter(dvExpClass, myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))
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
        If fMat.UltraGridMEM.Rows.Count > 0 Then
            cm.EndCurrentEdit()
            fMat.CalculateTotalAmount()
            fMat.CalculateLessAdvance()
        End If
    End Sub

    Private Sub cmb_TransType_Leave(sender As Object, e As EventArgs) Handles cmb_TransType.Leave, cmb_TransType.AfterCloseUp
        risersoft.app.mxform.myFuncs.TransTypeFilter(dvExpClass, "M", myUtils.cStrTN(cmb_TransType.Value))
    End Sub
End Class