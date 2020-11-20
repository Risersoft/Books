Imports Infragistics.Win.UltraWinTree
Imports System.Windows.Forms
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmFinStatement
    Inherits frmMax
    Dim myViewAcc As New clsWinView

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGrid1)
        myViewAcc.SetGrid(Me.UltraGrid2)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmFinStatementModel = Me.InitData("frmFinStatementModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If cmb_CompanyID.Rows.Count > 0 Then cmb_CompanyID.Value = myUtils.cValTN(cmb_CompanyID.Rows(0).Cells("CompanyID").Value)
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            WinTreeUtils.BuildBOMTree(Me.UltraTree1, "GLAccGroupID,GroupName,GroupType,Balance", "0-6-1-2")

            myView.PrepEdit(Me.Model.GridViews("Groups"))
            myViewAcc.PrepEdit(Me.Model.GridViews("Accounts"))

            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            RefreshTree()
            Return True
        End If
        Return False
    End Function

    Private Sub RefreshTree()
        Me.UltraTree1.Nodes.Clear()
        WinTreeUtils.MakeTree(Me.UltraTree1.Nodes, Nothing, myView.mainGrid.myDv.Table, "GLAccGroupID", "GroupName", "pGLAccGroupID", "", New WinTreeUtils.dSetNodeProps(AddressOf SetNodeProp), Nothing)
        WinTreeUtils.ExpandNodes(Me.UltraTree1, 2)
        Me.UltraTree1.ActiveNode = Me.UltraTree1.Nodes(0)
        Me.UltraTree1.ActiveNode.Selected = True
    End Sub

    Private Sub SetNodeProp(xNode As UltraTreeNode, rNode As DataRow)
        xNode.SetCellValue(xNode.DataColumnSetResolved.Columns("grouptype"), rNode("grouptype"))
        xNode.SetCellValue(xNode.DataColumnSetResolved.Columns("Balance"), rNode("Balance"))
    End Sub

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        WinFormUtils.InitTabBacks(Me.UltraTabControl1)
        VSave = False
        If Me.CanSave() Then
            cm.EndCurrentEdit()
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub btn_reLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reLoad.Click
        Dim objModel As frmFinStatementModel = Me.InitData("frmFinStatementModel", pView, frmMode, frmIDX, "")
        Me.BindModel(objModel, Me.pView)
    End Sub

    Private Sub frmMatPos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.UltraTree1.Width = Me.Width / 2
    End Sub

    Private Sub UltraTree1_AfterSelect(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTree.SelectEventArgs) Handles UltraTree1.AfterSelect
        Dim str1 As String, oNode As UltraTreeNode

        If Me.UltraTree1.SelectedNodes.Count = 0 Then oNode = Me.UltraTree1.Nodes(0) Else oNode = Me.UltraTree1.SelectedNodes(0)
        Me.UltraLabel1.Text = WinTreeUtils.NodeAddress(oNode, "GroupName")
        str1 = myUtils.cStrTN(oNode.GetCellValue(oNode.DataColumnSetResolved.Columns("GLAccGroupID")))
        myView.mainGrid.myDv.RowFilter = "pGLAccGroupID in (" & str1 & ")"
        myViewAcc.mainGrid.myDv.RowFilter = "GLAccGroupID in (" & str1 & ")"
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
        Dim oRet As clsProcOutput = GenerateParamsOutput("generate", Params)
        If oRet.Success Then
            Me.UpdateViewData(myView, oRet)
            Me.UpdateViewData(myViewAcc, oRet)
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
        RefreshTree()
    End Sub
End Class