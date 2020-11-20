Imports Infragistics.Win.UltraWinTree
Imports System.Windows.Forms
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmGLAccGroupList
    Inherits frmMax
    Dim myVueAcc, myVueMap As New clsWinView, focusnum As Integer = 1
    Dim oSort As clsWinSort

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        myVueAcc.SetGrid(Me.UltraGrid2)
        myVueMap.SetGrid(Me.UltraGrid3)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel)
        oSort = New clsWinSort(myView, Me.btnUp, Me.btnDown, Me.btnRenumber, "sortNumber")
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmGLAccGroupListModel = Me.InitData("frmGLAccGroupListModel", oview, EnumfrmMode.acEditM, prepIdx, strXML)

        If Me.BindModel(objModel, oview) Then
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            WinTreeUtils.BuildBOMTree(Me.UltraTree1, "GLAccGroupID,ChildGroupIDs,GroupName,GroupType", "0-0-6-1")

            myView.PrepEdit(Me.Model.GridViews("Groups"))
            oSort.renumber()
            myVueAcc.PrepEdit(Me.Model.GridViews("Accounts"))
            myVueMap.PrepEdit(Me.Model.GridViews("Map"))

            WinTreeUtils.MakeTree(Me.UltraTree1.Nodes, Nothing, myView.mainGrid.myDv.Table, "GLAccGroupID", "GroupName", "pGLAccGroupID", "", New WinTreeUtils.dSetNodeProps(AddressOf SetNodeProp), Nothing)
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
        If Me.UltraTabControl2.SelectedTab.Index = 0 Then
            Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "add", GetType(frmGLAccGroup), "GLAccGroupID", "")
        Else
            If focusnum = 1 Then
                Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueAcc, "add", GetType(frmGLAccount), "GLAccountID", "")
            Else
                Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueMap, "add", GetType(frmAccountKey), "AccountKeyID", "")
            End If
        End If
    End Sub

    Private Sub btn_reLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reLoad.Click
        Dim objModel As frmGLAccGroupListModel = Me.InitData("frmGLAccGroupListModel", pView, frmMode, frmIDX, "")
        Me.BindModel(objModel, Me.pView)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Me.UltraTabControl2.SelectedTab.Index = 0 Then
            Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myView, "edit", GetType(frmGLAccGroup), "GLAccGroupID", "")
        Else
            If focusnum = 1 Then
                Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueAcc, "edit", GetType(frmGLAccount), "GLAccountID", "")
            Else
                Dim nr As DataRow = WinFormUtils.ButtonActionChildForm(myVueMap, "edit", GetType(frmAccountKey), "AccountKeyID", "")
            End If
        End If
    End Sub

    Private Sub frmMatPos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.UltraTree1.Width = Me.Width / 2
    End Sub

    Private Sub UltraTree1_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles UltraTree1.AfterSelect
        Dim oNode As UltraTreeNode

        If Me.UltraTree1.SelectedNodes.Count = 0 Then oNode = Me.UltraTree1.Nodes(0) Else oNode = Me.UltraTree1.SelectedNodes(0)
        Me.UltraLabel1.Text = WinTreeUtils.NodeAddress(oNode, "GroupName")
        Dim str1 As String = myUtils.cStrTN(oNode.GetCellValue(oNode.DataColumnSetResolved.Columns("GLAccGroupID")))
        myView.mainGrid.myDv.RowFilter = "pGLAccGroupID in (" & str1 & ")"

        Dim str2 As String = myUtils.cStrTN(oNode.GetCellValue(oNode.DataColumnSetResolved.Columns("ChildGroupIDs")))
        myVueAcc.mainGrid.myDv.RowFilter = "GLAccGroupID in (" & str2 & ")"

        myVueMap.mainGrid.myDv.RowFilter = "GLAccountCode in (" & myUtils.MakeCSV(myVueAcc.mainGrid.myDv.Table.Select(myVueAcc.mainGrid.myDv.RowFilter), "acccode") & ")"
    End Sub

    Private Sub UltraGrid2_GotFocus(sender As Object, e As EventArgs) Handles UltraGrid2.GotFocus
        focusnum = 1
    End Sub

    Private Sub UltraGrid3_GotFocus(sender As Object, e As EventArgs) Handles UltraGrid3.GotFocus
        focusnum = 2
    End Sub
End Class