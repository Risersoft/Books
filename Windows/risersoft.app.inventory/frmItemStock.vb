Imports Infragistics.Win.UltraWinTree
Imports System.Windows.Forms
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmItemStock
    Inherits frmMax
    Dim myViewItems As New clsWinView

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        myViewItems.SetGrid(Me.UltraGrid2)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmItemStockModel = Me.InitData("frmItemStockModel", oview, prepMode, prepIdx, strXML)

        If Me.BindModel(objModel, oview) Then
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            WinTreeUtils.BuildBOMTree(Me.UltraTree1, "ItemSBHeadID,SBHeadName,Qty,UnitName,AmountTot,AmountWV,Remark", "0-6-1-1-1-1-2")

            myView.PrepEdit(Me.Model.GridViews("SBHead"))
            myViewItems.PrepEdit(Me.Model.GridViews("Items"))

            Me.MakeTree(Me.UltraTree1.Nodes, Nothing, myView.mainGrid.myDv.Table)
            WinTreeUtils.ExpandNodes(Me.UltraTree1, 2)
            Me.UltraTree1.ActiveNode = Me.UltraTree1.Nodes(0)
            Me.UltraTree1.ActiveNode.Selected = True
            Return True
        End If
        Return False
    End Function

    Public Function MakeTree(ByVal oNodeCollec As TreeNodesCollection, ByVal rParent As DataRow, ByVal dtTree As DataTable) As UltraTreeNode
        Dim xNode As UltraTreeNode, str1, str2, strf As String, oSQL As New clsSQLBuilder(Me.Controller)

        If rParent Is Nothing Then
            strf = "pItemSBHeadID is null"
            xNode = Nothing
        Else
            strf = "pItemSBHeadID=" & rParent("ItemSBHeadID")
            If myUtils.cValTN(rParent("ItemUnitID")) > 0 Then strf = myUtils.CombineWhere(False, strf, "ItemUnitID=" & rParent("ItemUnitID"))
            str1 = myUtils.cStrTN(rParent("SBHeadName"))
            str2 = oSQL.RowValueCSV(rParent, "ItemSBHeadID,ItemUnitID")
            xNode = oNodeCollec.Add(str2, str1)
            If Not xNode Is Nothing Then
                win.myWinUtils.CopyOneRow(rParent, xNode)
                xNode.Override.NodeSpacingBefore = 2
                For Each strCol As String In New String() {"Qty", "UnitName", "AmountTot", "AmountWV", "Remark"}
                    xNode.SetCellValue(xNode.DataColumnSetResolved.Columns(strCol), rParent(strCol))
                Next
                oNodeCollec = xNode.Nodes
            End If
        End If
        For Each r2 As DataRow In dtTree.Select(strf, "")
            MakeTree(oNodeCollec, r2, dtTree)
        Next
        Return xNode
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        WinFormUtils.InitTabBacks(Me.UltraTabControl1)

        If Me.CanSave Then
            VSave = True
        End If
        Me.Refresh()
    End Function

    Private Sub btn_reLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reLoad.Click
        Dim objModel As frmItemStockModel = Me.InitData("frmItemStockModel", pView, frmMode, frmIDX, "")
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
        myViewItems.mainGrid.myDv.RowFilter = "ItemSBHeadID in (" & str1 & ")"
    End Sub
End Class