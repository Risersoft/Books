Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmTourAdvReqItem
    Inherits frmMax
    Friend fMat As frmTourAdvReq

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()

    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myWinSQL.AssignCmb(NewModel.dsCombo, "PartyMain", "", Me.cmb_MainPartyID)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.FormPrepared = True
            If Not myUtils.IsInList(myUtils.cStrTN(myRow("Organization")), "") Then
                optMainParty.CheckedIndex = 1
            Else
                optMainParty.CheckedIndex = 0
            End If
            HandleMainParty(optMainParty.CheckedIndex)
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

    Public Sub HandleMainParty(Opt As Integer)
        cmb_MainPartyID.ReadOnly = True
        txt_Organization.ReadOnly = True

        If Opt = 0 Then
            txt_Organization.Text = String.Empty
            cmb_MainPartyID.ReadOnly = False
        Else
            cmb_MainPartyID.Value = DBNull.Value
            txt_Organization.ReadOnly = False
        End If

    End Sub

    Private Sub optMainParty_ValueChanged(sender As Object, e As EventArgs) Handles optMainParty.ValueChanged
        HandleMainParty(optMainParty.CheckedIndex)
    End Sub
End Class