Imports risersoft.app.mxform
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmODNReturnON
    Inherits frmMax
    Dim dvEmp As DataView

    Private Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmODNReturnONModel = Me.InitData("frmODNReturnONModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            Dim gRow As ug.UltraGridRow
            If myUtils.NullNot(Me.cmb_ReceivedByID.Value) Then
                gRow = win.myWinUtils.FindRow(Me.cmb_ReceivedByID, "UserID", myWinApp.Controller.Police.UniqueUserID)
                If Not IsNothing(gRow) Then myRow("ReceivedByID") = myUtils.cStrTN(gRow.Cells("UserID").Value)
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            dvEmp = myWinSQL.AssignCmb(Me.dsCombo, "User", "", Me.cmb_ReceivedByID,, 2)
            dvEmp.RowFilter = risersoft.app.mxform.myFuncs.FilterTimeDependent(myUtils.cDateTN(myRow("ChallanDate"), DateTime.MinValue), "JoinDate", "LeaveDate", 0)
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
