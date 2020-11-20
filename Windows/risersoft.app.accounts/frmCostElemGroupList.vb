Imports Infragistics.Win.UltraWinTree
Imports System.Windows.Forms
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmCostElemGroupList
    Inherits frmMax
    Dim myVueElem As New clsWinView, focusnum As Integer = 1
    Dim oSort As clsWinSort

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGridGroup)
        myVueElem.SetGrid(Me.UltraGridCostElem)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel)
        oSort = New clsWinSort(myView, Me.btnUp, Me.btnDown, Me.btnRenumber, "sortNumber")
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmCostElemGroupListModel = Me.InitData("frmCostElemGroupListModel", oView, EnumfrmMode.acEditM, prepIdx, strXML)

        If Me.BindModel(objModel, oView) Then
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            WinTreeUtils.BuildBOMTree(Me.UltraTree1, "CostElemGroupID,ChildGroupIDs,GroupName,GroupType", "0-0-6-1")

            myView.PrepEdit(Me.Model.GridViews("Groups"))
            oSort.renumber()
            myVueElem.PrepEdit(Me.Model.GridViews("CostElem"))

            WinTreeUtils.MakeTree(Me.UltraTree1.Nodes, Nothing, myView.mainGrid.myDv.Table, "CostElemGroupID", "GroupName", "pCostElemGroupID", "", New WinTreeUtils.dSetNodeProps(AddressOf SetNodeProp), Nothing)
            WinTreeUtils.ExpandNodes(Me.UltraTree1, 2)
            Me.UltraTree1.ActiveNode = Me.UltraTree1.Nodes(0)
            Me.UltraTree1.ActiveNode.Selected = True
            Return True
        End If
        Return False
    End Function

    Private Sub SetNodeProp(xNode As UltraTreeNode, rNode As DataRow)
        xNode.SetCellValue(xNode.DataColumnSetResolved.Columns("grouptype"), rNode("grouptype"))
        xNode.SetCellValue(xNode.DataColumnSetResolved.Columns("childgroupids"), rNode("childgroupids"))
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If focusnum = 1 Then
            Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "add", GetType(frmCostElemGroup), "CostElemGroupID", "")
        Else
            Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueElem, "add", GetType(frmCostElement), "CostElementID", "")
        End If
    End Sub

    Private Sub btn_reLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reLoad.Click
        Dim objModel As frmCostElemGroupListModel = Me.InitData("frmCostElemGroupListModel", pView, frmMode, frmIDX, "")
        Me.BindModel(objModel, Me.pView)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If focusnum = 1 Then
            Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "edit", GetType(frmCostElemGroup), "CostElemGroupID", "")
        Else
            Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueElem, "edit", GetType(frmCostElement), "CostElementID", "")
        End If
    End Sub

    Private Sub frmMatPos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.UltraTree1.Width = Me.Width / 2
    End Sub

    Private Sub UltraTree1_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles UltraTree1.AfterSelect
        Dim oNode As UltraTreeNode

        If Me.UltraTree1.SelectedNodes.Count = 0 Then oNode = Me.UltraTree1.Nodes(0) Else oNode = Me.UltraTree1.SelectedNodes(0)
        Me.UltraLabel1.Text = WinTreeUtils.NodeAddress(oNode, "GroupName")
        Dim str1 As String = myUtils.cStrTN(oNode.GetCellValue(oNode.DataColumnSetResolved.Columns("CostElemGroupID")))
        myView.mainGrid.myDv.RowFilter = "pCostElemGroupID in (" & str1 & ")"

        Dim str2 As String = myUtils.cStrTN(oNode.GetCellValue(oNode.DataColumnSetResolved.Columns("ChildGroupIDs")))
        myVueElem.mainGrid.myDv.RowFilter = "CostElemGroupID in (" & str2 & ")"
    End Sub

    Private Sub UltraGrid1_GotFocus(sender As Object, e As EventArgs) Handles UltraGridGroup.GotFocus
        focusnum = 1
    End Sub

    Private Sub UltraGrid2_GotFocus(sender As Object, e As EventArgs) Handles UltraGridCostElem.GotFocus
        focusnum = 2
    End Sub
End Class