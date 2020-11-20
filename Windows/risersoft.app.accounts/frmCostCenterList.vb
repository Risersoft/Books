Imports Infragistics.Win.UltraWinTree

Public Class frmCostCenterList
    Inherits frmMax
    Dim oSort As clsWinSort

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel)
        oSort = New clsWinSort(myView, Me.btnUp, Me.btnDown, Me.btnRenumber, "sortNumber")
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCostCenterListModel = Me.InitData("frmCostCenterListModel", oview, EnumfrmMode.acEditM, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            lblCompanyName.Text = myUtils.cStrTN(myRow("CompName"))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            WinTreeUtils.BuildBOMTree(Me.UltraTree1, "CostCenterID,ChildCostCenterIDs,Name,Campus,Department", "0-0-4-2-2")

            myView.PrepEdit(Me.Model.GridViews("CostCenter"))
            oSort.renumber()

            WinTreeUtils.MakeTree(Me.UltraTree1.Nodes, Nothing, myView.mainGrid.myDv.Table, "CostCenterID", "Name", "pCostCenterID", "", New WinTreeUtils.dSetNodeProps(AddressOf SetNodeProp), Nothing)
            WinTreeUtils.ExpandNodes(Me.UltraTree1, 2)
            If Me.UltraTree1.Nodes.Count > 0 Then
                Me.UltraTree1.ActiveNode = Me.UltraTree1.Nodes(0)
                Me.UltraTree1.ActiveNode.Selected = True
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub SetNodeProp(xNode As UltraTreeNode, rNode As DataRow)
        'xNode.SetCellValue(xNode.DataColumnSetResolved.Columns("grouptype"), rNode("grouptype"))
        xNode.SetCellValue(xNode.DataColumnSetResolved.Columns("ChildCostCenterIDs"), rNode("ChildCostCenterIDs"))
    End Sub

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        WinFormUtils.InitTabBacks(Me.UltraTabControl1)
        If Me.SaveModel() Then
            Return True
        End If
        Me.Refresh()
    End Function

    Private Sub btn_reLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reLoad.Click
        Dim objModel As frmCostCenterListModel = Me.InitData("frmCostCenterListModel", pView, frmMode, frmIDX, "")
        Me.BindModel(objModel, Me.pView)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "add", GetType(frmCostCenter), "CostCenterID", "<PARAMS CompanyID=""" & myUtils.cValTN(myRow("CompanyID")) & """/>")
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "edit", GetType(frmCostCenter), "CostCenterID", "<PARAMS CompanyID=""" & myUtils.cValTN(myRow("CompanyID")) & """/>")
    End Sub

    Private Sub frmMatPos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.UltraTree1.Width = Me.Width / 2
    End Sub

    Private Sub UltraTree1_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles UltraTree1.AfterSelect
        Dim oNode As UltraTreeNode

        If Me.UltraTree1.SelectedNodes.Count = 0 Then oNode = Me.UltraTree1.Nodes(0) Else oNode = Me.UltraTree1.SelectedNodes(0)
        Me.UltraLabel1.Text = WinTreeUtils.NodeAddress(oNode, "Name",, " > ")
        Dim str1 As String = myUtils.cStrTN(oNode.GetCellValue(oNode.DataColumnSetResolved.Columns("CostCenterID")))
        myView.mainGrid.myDv.RowFilter = "pCostCenterID in (" & str1 & ")"

    End Sub

    Private Sub btnEditRoot_Click(sender As Object, e As EventArgs) Handles btnEditRoot.Click
        If Not IsNothing(UltraTree1.ActiveNode) Then
            Dim f As New frmCostCenter
            f.PrepForm(myView, EnumfrmMode.acEditM, myUtils.cValTN(UltraTree1.ActiveNode.Cells("CostCenterID").Value))
            f.ShowDialog()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim oRet = Me.DeleteEntity("costcenter", myView.mainGrid.myGrid.ActiveRow.Cells("CostCenterID").Value, False)
            If oRet.Success Then
                If MsgBox(oRet.WarningMessage, MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
                    oRet = Me.DeleteEntity("costcenter", myView.mainGrid.myGrid.ActiveRow.Cells("CostCenterID").Value, True)
                End If
            End If
            MsgBox(oRet.Message, MsgBoxStyle.DefaultButton1, myWinApp.Vars("appname"))
        End If
    End Sub
End Class