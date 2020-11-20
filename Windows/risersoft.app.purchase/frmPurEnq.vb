Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmPurEnq
    Inherits frmMax
    Friend fItem As frmPurEnqItem
    Dim oSort As clsWinSort, dvCamp As DataView

    Public Sub initForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(Me.UltraGridItemList)

        fItem = New frmPurEnqItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        oSort = New clsWinSort(myView, btnUp, btnDown, Nothing, "SNum")
        fItem.Enabled = False
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPurEnqModel = Me.InitData("frmPurEnqModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            oSort.renumber()

            HandleDate(myUtils.cDateTN(myRow("enqDate"), DateTime.MinValue))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Items"))
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Attention", "", Me.cmb_attentionid)

            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridItemList_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)

        fItem.myView.mainGrid.myDv.RowFilter = "PurEnqItemID = " & myView.mainGrid.myGrid.ActiveRow.Cells("PurEnqItemID").Value
        fItem.Enabled = True
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDV.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            oSort.renumber()
            fItem.Enabled = True
            fItem.Focus()
        End If
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
    End Sub

    Private Sub cmb_SampleTextType_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_SampleTextType.SelectionChanged
        If cmb_SampleTextType.SelectedIndex = 2 Then
            lblSendSampleText.Visible = True
            txt_SendSampleText.Visible = True
        Else
            lblSendSampleText.Visible = False
            txt_SendSampleText.Visible = False
            txt_SendSampleText.Text = cmb_SampleTextType.Text
        End If
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
    End Sub

    Private Sub dt_enqDate_Leave(sender As Object, e As EventArgs) Handles dt_enqDate.Leave, dt_enqDate.AfterCloseUp
        HandleDate(dt_enqDate.Value)
    End Sub
End Class