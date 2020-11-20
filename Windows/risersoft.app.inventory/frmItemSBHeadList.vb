Imports Infragistics.Win.UltraWinTree
Imports System.Windows.Forms
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmItemSBHeadList
    Inherits frmMax
    Dim myViewCat As New clsWinView

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGridSBHead)
        myViewCat.SetGrid(Me.UltraGridSubCategories)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmItemSBHeadListModel = Me.InitData("frmItemSBHeadListModel", oview, prepMode, prepIdx, strXML)

        If Me.BindModel(objModel, oview) Then
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            WinTreeUtils.BuildBOMTree(Me.UltraTree1, "ItemSBHeadID,SBHeadName,Remark", "0-6-2")

            myView.PrepEdit(Me.Model.GridViews("SBHead"))
            myViewCat.PrepEdit(Me.Model.GridViews("SubCategories"))

            WinTreeUtils.MakeTree(Me.UltraTree1.Nodes, Nothing, myView.mainGrid.myDv.Table, "ItemSBHeadID", "SBHeadName", "pItemSBHeadID", "", New WinTreeUtils.dSetNodeProps(AddressOf SetNodeProp), Nothing)
            WinTreeUtils.ExpandNodes(Me.UltraTree1, 2)
            Me.UltraTree1.ActiveNode = Me.UltraTree1.Nodes(0)
            Me.UltraTree1.ActiveNode.Selected = True
            Return True
        End If
        Return False
    End Function

    Private Sub SetNodeProp(xNode As UltraTreeNode, rNode As DataRow)
        xNode.SetCellValue(xNode.DataColumnSetResolved.Columns("remark"), rNode("remark"))
    End Sub

    Public Overrides Function VSave() As Boolean
        VSave = False
        Me.InitError()
        WinFormUtils.InitTabBacks(Me.UltraTabControl1)
        If Me.CanSave Then
            VSave = True
        End If
        Me.Refresh()
    End Function

    Private Sub btn_reLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reLoad.Click
        Dim objModel As frmItemSBHeadListModel = Me.InitData("frmItemSBHeadListModel", pView, frmMode, frmIDX, "")
        Me.BindModel(objModel, Me.pView)
    End Sub

    Private Sub frmMatPos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.UltraTree1.Width = Me.Width / 2
    End Sub

    Private Sub UltraTree1_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles UltraTree1.AfterSelect
        Dim str1 As String, oNode As UltraTreeNode

        If Me.UltraTree1.SelectedNodes.Count = 0 Then oNode = Me.UltraTree1.Nodes(0) Else oNode = Me.UltraTree1.SelectedNodes(0)
        Me.UltraLabel1.Text = WinTreeUtils.NodeAddress(oNode, "SBHeadName")
        str1 = myUtils.cStrTN(oNode.GetCellValue(oNode.DataColumnSetResolved.Columns("ItemSBHeadID")))
        myView.mainGrid.myDv.RowFilter = "pItemSBHeadID in (" & str1 & ")"
        myViewCat.mainGrid.myDv.RowFilter = "ItemSBHeadID in (" & str1 & ")"
    End Sub
End Class