Imports risersoft.app.shared
Imports risersoft.app.mxform
Imports Infragistics.Win.Touch

Public Class frmCostElement
    Inherits frmMax
    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCostElementModel = Me.InitData("frmCostElementModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            SetElemType()
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "GlAccount", "", Me.cmb_GLAccountID)
            myWinSQL.AssignCmb(Me.dsCombo, "SubLedgerType", "", Me.cmb_SubLedgerType)
            myWinSQL.AssignCmb(Me.dsCombo, "CostElemType", "", Me.cmb_CostElemType)
            myWinSQL.AssignCmb(Me.dsCombo, "CostElemGroup", "", Me.cmb_CostElemGroupID)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            SetElemType()
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub SetElemType()
        cm.EndCurrentEdit()
        If myUtils.cValTN(myRow("GlAccountID")) > 0 Then
            myRow("CostElemType") = "P"
        Else
            myRow("CostElemType") = "S"
        End If
    End Sub

    Private Sub cmb_GLAccountID_Leave(sender As Object, e As EventArgs) Handles cmb_GLAccountID.Leave, cmb_GLAccountID.AfterCloseUp
        SetElemType()
    End Sub
End Class