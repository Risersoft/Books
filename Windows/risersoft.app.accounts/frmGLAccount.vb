Imports risersoft.app.shared
Imports risersoft.app.mxform

Public Class frmGLAccount
    Inherits frmMax

    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGLAccountModel = Me.InitData("frmGLAccountModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            cmb_SubLedgerType.ReadOnly = myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "AccSched", "", Me.cmb_AccSchedID)
            myWinSQL.AssignCmb(Me.dsCombo, "GLAccGroup", "", Me.cmb_GLAccGroupID)
            myWinSQL.AssignCmb(Me.dsCombo, "SubLedgerType", "", Me.cmb_SubLedgerType)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function
End Class