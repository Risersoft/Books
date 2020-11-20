Imports risersoft.app.shared
Imports risersoft.app.mxform

Public Class frmCostElemGroup
    Inherits frmMax
    Dim oSort As clsWinSort, dv As DataView

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridItemList)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        oSort = New clsWinSort(myView, Me.btnUp, Me.btnDown, Nothing, "SortNumber")
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCostElemGroupModel = Me.InitData("frmCostElemGroupModel", oView, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oView) Then

            If frmMode = EnumfrmMode.acEditM Then
                If myUtils.cValTN(myRow("pCostElemGroupID")) = 0 Then
                    txt_SortNumber.Visible = True
                    lblSortNumber.Visible = True
                End If
            End If

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Items"))
            oSort.renumber()

            dv = myWinSQL.AssignCmb(Me.dsCombo, "CostElemGroup", "", Me.cmb_pCostElemGroupID,, 2)
            dv.RowFilter = "CostElemGroupID <> " & frmIDX

            myWinSQL.AssignCmb(Me.dsCombo, "GroupType", "", Me.cmb_GroupType)

            Return True
        End If
        Return False
    End Function

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "edit", GetType(frmCostElemGroup), "CostElemGroupID", "")
    End Sub

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