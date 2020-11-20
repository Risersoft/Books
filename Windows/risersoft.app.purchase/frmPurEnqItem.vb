Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent

Public Class frmPurEnqItem
    Inherits frmMax
    Friend fMat As frmPurEnq
    Dim WithEvents ItemCodeSys As New clsCodeSystem

    Public Sub initForm()
        myView.SetGrid(Me.UltraGridParams)
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myView.PrepEdit(NewModel.GridViews("Params"))
        ItemCodeSys.SetConf(NewModel.dsCombo.Tables("Items"), Me.cmb_ItemId, Me.cmbItemName, Me.cmb_BaseUnitID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "ItemUnits", "", Me.cmb_UnitName)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_RateUnitId)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.cmb_ItemId.Value = myRow("ItemID")
            ItemCodeSys.HandleCombo(Me.cmb_ItemId, EnumWantEvent.acForceEvent)
            Me.FormPrepared = True
        End If

        Return Me.FormPrepared
    End Function

    Private Sub ItemCodeSys_ItemChanged() Handles ItemCodeSys.ItemChanged
        cm.EndCurrentEdit()
        myRow("ItemName") = myUtils.cStrTN(cmbItemName.Text)
        myRow("ItemCode") = myUtils.cStrTN(Me.cmb_ItemId.Text)

        If Not myUtils.NullNot(cmb_ItemId.SelectedRow) Then
            cmb_UnitName.Text = Me.cmb_ItemId.SelectedRow.Cells("UnitName").Value

            If Not myUtils.NullNot(cmb_ItemId.SelectedRow.Cells("OrderRateUnitId").Value) Then
                cmb_RateUnitId.Value = cmb_ItemId.SelectedRow.Cells("OrderRateUnitId").Value
            Else
                cmb_RateUnitId.Value = cmb_ItemId.SelectedRow.Cells("ItemUnitId").Value
            End If
        End If

    End Sub

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_ItemId, "Please Generate Transaction")
            Exit Function
        End If

        If myUtils.NullNot(cmb_ItemId.Text) Then WinFormUtils.AddError(cmb_ItemId, "Select Item")
        If txt_qty.Text.Trim.Length = 0 Then WinFormUtils.AddError(txt_qty, "Enter Quantity")
        If txt_rate.Text.Trim.Length = 0 Then WinFormUtils.AddError(txt_rate, "Enter Rate")

        If Me.CanSave Then
            cm.EndCurrentEdit()
            myRow("RateUnitID") = myUtils.cValTN(Me.cmb_RateUnitId.Value)

            VSave = True
        End If
    End Function

    Private Sub btnAddParam_Click(sender As Object, e As EventArgs) Handles btnAddParam.Click
        myView.mainGrid.ButtonAction("Add")
        myView.mainGrid.myGrid.ActiveRow.Cells("PurEnqItemId").Value = myUtils.cValTN(myRow("PurEnqItemId"))
    End Sub

    Private Sub btnDelParam_Click(sender As Object, e As EventArgs) Handles btnDelParam.Click
        myView.mainGrid.ButtonAction("Del")
    End Sub
End Class