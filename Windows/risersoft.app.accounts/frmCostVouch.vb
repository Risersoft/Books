Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform

Public Class frmCostVouch
    Inherits frmMax
    Friend fItem As frmCostVouchItem

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_ItemDetail.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)

        fItem = New frmCostVouchItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        fItem.Enabled = False
        myView.SetGrid(Me.UltraGridItemList)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCostVouchModel = Me.InitData("frmCostVouchModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            FillDrCrTotalAmount()

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Items"))
            myWinSQL.AssignCmb(Me.dsCombo, "CostVouchType", "", Me.cmb_AccVouchType)
            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        FillDrCrTotalAmount()
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

    Private Sub UltraGridItemList_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
        FillDrCrTotalAmount()
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)
        fItem.Enabled = True
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Table.Select.Length = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")

            fItem.Focus()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            fItem.Enabled = False
        End If
    End Sub

    Public Function FillDrCrTotalAmount()
        If myView.mainGrid.myDv.Table.Rows.Count > 0 Then
            txtDrAmount.Value = DBNull.Value
            txtCrAmount.Value = DBNull.Value

            For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select
                If myUtils.cStrTN(r1("AmountDC")) = "D" Then
                    txtDrAmount.Value = myUtils.cValTN(txtDrAmount.Value) + myUtils.cValTN(r1("Amount"))
                ElseIf myUtils.cStrTN(r1("AmountDC")) = "C" Then
                    txtCrAmount.Value = myUtils.cValTN(txtCrAmount.Value) + myUtils.cValTN(r1("Amount"))
                End If
            Next
            cm.EndCurrentEdit()
        End If
        Return True
    End Function

    Private Sub cmb_CompanyID_Leave(sender As Object, e As EventArgs) Handles cmb_CompanyID.Leave
        If Not IsNothing(cmb_CompanyID.Value) Then fItem.HandleCompanyID(myUtils.cValTN(cmb_CompanyID.Value))
    End Sub
End Class
