Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports Infragistics.Win.UltraWinGrid

Public Class frmMatVouchRes
    Inherits frmMax
    Friend myViewIssue As New clsWinView, fMatItem As frmMax, fMat As frmMax, ObjGetMatVouch As New clsGetRecordsMatVouch()

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(UltraGridResReceipt)
        myViewIssue.SetGrid(UltraGridResIssue)

        UltraTabControl2.Tabs("Receipt").Visible = False
        UltraTabControl2.Tabs("Issue").Visible = False
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myWinSQL.AssignCmb(NewModel.dsCombo, "Reserve", "", Me.cmb_ReserveGRBehave)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Reserve", "", Me.cmb_ReserveGIBehave)
        Return True
    End Function
    Public Sub InitPanel(fParentItem As frmMax, fParentMat As frmMax)
        fMatItem = fParentItem
        fMat = fParentMat
        Me.BindingContext = fMatItem.BindingContext       'Very important for fp.cm EndCurrentEdit / Refresh to work.
    End Sub

    Public Sub SetTabCantrol(MvtType As String, r1 As DataRow)
        btnAddLotsRec.Enabled = False
        btnAddLotsIssue.Enabled = False

        ObjGetMatVouch.HandleFormTab(UltraTabControl2.Tabs("Receipt"), myUtils.IsInList(myUtils.cStrTN(MvtType), "GR", "TP"))
        ObjGetMatVouch.HandleFormTab(UltraTabControl2.Tabs("Issue"), myUtils.IsInList(myUtils.cStrTN(MvtType), "GI", "TP"))

        If myUtils.IsInList(myUtils.cStrTN(MvtType), "GR") AndAlso myUtils.IsInList(myUtils.cStrTN(r1("ReserveGRBehave")), "") Then
            cmb_ReserveGRBehave.Value = "N"
        ElseIf myUtils.IsInList(myUtils.cStrTN(MvtType), "GI") AndAlso myUtils.IsInList(myUtils.cStrTN(r1("ReserveGIBehave")), "") Then
            cmb_ReserveGIBehave.Value = "N"
        ElseIf myUtils.IsInList(myUtils.cStrTN(MvtType), "TP") Then
            If myUtils.IsInList(myUtils.cStrTN(r1("ReserveGRBehave")), "") Then cmb_ReserveGRBehave.Value = "A"
            If myUtils.IsInList(myUtils.cStrTN(r1("ReserveGIBehave")), "") Then cmb_ReserveGIBehave.Value = "N"
        End If
    End Sub

    Private Sub cmb_ReserveGRBehave_Leave(sender As Object, e As EventArgs) Handles cmb_ReserveGRBehave.Leave, cmb_ReserveGRBehave.AfterCloseUp
        btnAddLotsRec.Enabled = False
        If myUtils.IsInList(myUtils.cStrTN(cmb_ReserveGRBehave.Value), "M") Then
            btnAddLotsRec.Enabled = True
            UltraGridResReceipt.Enabled = True
        ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_ReserveGRBehave.Value), "A") Then
            UltraGridResReceipt.Enabled = True
        Else
            UltraGridResReceipt.Enabled = False
        End If
    End Sub

    Private Sub cmb_ReserveGIBehave_Leave(sender As Object, e As EventArgs) Handles cmb_ReserveGIBehave.Leave, cmb_ReserveGIBehave.AfterCloseUp
        btnAddLotsIssue.Enabled = False
        If myUtils.IsInList(myUtils.cStrTN(cmb_ReserveGIBehave.Value), "M") Then
            btnAddLotsIssue.Enabled = True
            UltraGridResIssue.Enabled = True
        ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_ReserveGIBehave.Value), "A") Then
            UltraGridResIssue.Enabled = True
        Else
            UltraGridResIssue.Enabled = False
        End If
    End Sub

    Private Sub btnAddLotsRec_Click(sender As Object, e As EventArgs) Handles btnAddLotsRec.Click
        Dim rr() As DataRow, Params As New List(Of clsSQLParam)
        rr = fMat.AdvancedSelect("prodlot", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In rr
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDv.Table)
                r2("MatVouchItemID") = myUtils.cValTN(fMatItem.myRow("MatVouchItemID"))
                r2("ResItemType") = "MR"
            Next
        End If
    End Sub

    Private Sub btnAddLotsIssue_Click(sender As Object, e As EventArgs) Handles btnAddLotsIssue.Click
        Dim rr() As DataRow, Params As New List(Of clsSQLParam)
        rr = fMat.AdvancedSelect("prodlot", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In rr
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewIssue.mainGrid.myDv.Table)
                r2("MatVouchItemID") = myUtils.cValTN(fMatItem.myRow("MatVouchItemID"))
                r2("ResItemType") = "MI"
            Next
        End If
    End Sub
End Class
