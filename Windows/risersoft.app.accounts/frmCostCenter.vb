Imports risersoft.app.shared
Imports risersoft.app.mxform

Public Class frmCostCenter
    Inherits frmMax
    Dim oSort As clsWinSort, dv, dvCamp, dvDep As DataView

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridItemList)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        oSort = New clsWinSort(myView, Me.btnUp, Me.btnDown, Nothing, "SortNumber")
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCostCenterModel = Me.InitData("frmCostCenterModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then

            If frmMode = EnumfrmMode.acEditM Then
                If myUtils.cValTN(myRow("pCostCenterID")) = 0 Then
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
            myView.PrepEdit(Me.Model.GridViews("CostCenter"))
            oSort.renumber()

            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID,, 2)
            dvCamp.RowFilter = "CompanyID = " & myUtils.cValTN(myRow("CompanyID"))
            dvDep = myWinSQL.AssignCmb(Me.dsCombo, "Department", "", Me.cmb_DepID,, 2)
            dvDep.RowFilter = "CompanyID = " & myUtils.cValTN(myRow("CompanyID"))
            dv = myWinSQL.AssignCmb(Me.dsCombo, "pCostCenter", "", Me.cmb_pCostCenterID,, 2)
            dv.RowFilter = "CostCenterID <> " & frmIDX

            Return True
        End If
        Return False
    End Function

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "edit", GetType(frmCostCenter), "CostCenterID", "")
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