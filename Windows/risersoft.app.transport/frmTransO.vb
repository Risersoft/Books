Imports risersoft.app.mxform
Public Class frmTransO
    Inherits frmMax
    Dim dvCamp, dvEmp, dvBook As DataView
    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmTransOModel = Me.InitData("frmTransOModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If myUtils.cStrTN(myRow("ChallanNum")).Trim.Length > 0 Then
                btnOK.Enabled = False
                btnSave.Enabled = False
            End If

            HandleDate(myUtils.cDateTN(myRow("BookDate"), DateTime.MinValue))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(Me.dsCombo, "Transporter", "", Me.cmb_transporterId)
            dvBook = myWinSQL.AssignCmb(Me.dsCombo, "BookedBy", "", Me.cmb_BookedById,, 2)
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusId,, 2)
            dvEmp = myWinSQL.AssignCmb(Me.dsCombo, "User", "", Me.cmb_ContactUserID,, 2)
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

    Private Sub dt_BookDate_Leave(sender As Object, e As EventArgs) Handles dt_BookDate.Leave, dt_BookDate.AfterCloseUp
        HandleDate(dt_BookDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
        Dim str As String = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "JoinDate", "LeaveDate", 0)
        dvEmp.RowFilter = str
        dvBook.RowFilter = str
    End Sub
End Class