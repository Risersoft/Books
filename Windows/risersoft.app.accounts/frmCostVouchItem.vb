Imports risersoft.app.mxent
Public Class frmCostVouchItem
    Inherits frmMax
    Friend fMat As frmCostVouch
    Dim WithEvents AccCodeSystem As New clsCodeSystem
    Friend dv, dvWBS, dvLots, dvCostCenter As DataView
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        ControlStatus("")
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dv = New DataView(NewModel.dsCombo.Tables("CostElement"))
        AccCodeSystem.SetConf(dv, Me.cmb_CostElementID, Me.cmbCostElement)
        myWinSQL.AssignCmb(NewModel.dsCombo, "AmountDC", "", Me.cmb_AmountDC)
        myWinSQL.AssignCmb(NewModel.dsCombo, "IDField", "", Me.cmb_IDField)
        dvCostCenter = myWinSQL.AssignCmb(NewModel.dsCombo, "CostCenter", "", Me.cmb_CostCenterID,, 2)
        dvWBS = myWinSQL.AssignCmb(NewModel.dsCombo, "WBSElement", "", Me.cmb_WBSElementID,, 2)
        dvLots = myWinSQL.AssignCmb(NewModel.dsCombo, "ProdLot", "", Me.cmb_ProdLotID,, 2)

        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.cmb_CostElementID.Value = myRow("CostElementID")
            AccCodeSystem.HandleCombo(Me.cmb_CostElementID, EnumWantEvent.acForceEvent)
            If Me.frmMode = EnumfrmMode.acEditM Then cmb_CostElementID.Value = myUtils.cValTN(myRow("CostElementID"))

            HandleIDField(myUtils.cStrTN(myRow("IDField")))

            If Not IsNothing(fMat.myRow("CompanyID")) Then HandleCompanyID(myUtils.cValTN(fMat.myRow("CompanyID")))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub AccCodeSystem_ItemChanged() Handles AccCodeSystem.ItemChanged
        cm.EndCurrentEdit()

        myRow("CostElemName") = myUtils.cStrTN(cmbCostElement.Text)
        If myUtils.cValTN(cmb_CostElementID.Text) > 0 Then myRow("CostElemCode") = myUtils.cValTN(cmb_CostElementID.Text)
    End Sub

    Public Overrides Function VSave() As Boolean
        Dim str As String = ""
        Me.InitError()
        VSave = False

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_CostElementID, "Please Generate Transaction")
            Exit Function
        End If

        cm.EndCurrentEdit()
        If IsNothing(Me.cmb_CostElementID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_CostElementID, "Select Account Name")
        If IsNothing(Me.cmb_AmountDC.SelectedRow) Then WinFormUtils.AddError(Me.cmb_AmountDC, "Select DR/CR")
        If IsNothing(Me.txt_Amount.Value) Then WinFormUtils.AddError(Me.txt_Amount, "Enter Amount")
        If Not IsNothing(cmb_CostElementID.SelectedRow) Then str = myUtils.cStrTN(cmb_IDField.Value)

        If str = "CostCenterID" AndAlso IsNothing(Me.cmb_CostCenterID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_CostCenterID, "Select Cost Center")
        If str = "WBSElementID" AndAlso IsNothing(Me.cmb_WBSElementID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_WBSElementID, "Select WBS")
        If str = "ProdLotID" AndAlso IsNothing(Me.cmb_ProdLotID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_ProdLotID, "Select Lots")

        If Me.CanSave Then
            cm.EndCurrentEdit()
            VSave = True
        End If
    End Function

    Private Sub HandleIDField(IDField As String)
        ControlStatus(IDField)
        If Not myUtils.IsInList(myUtils.cStrTN(IDField), "") Then
            lblIDField.Visible = True
            If IDField = "CostCenterID" Then
                lblIDField.Text = "Cost Center"
            ElseIf IDField = "WBSElementID" Then
                lblIDField.Text = "WBS"
            ElseIf IDField = "ProdLotID" Then
                lblIDField.Text = "Lots"
            End If
        End If
    End Sub

    Private Function ControlStatus(IDField As String) As Boolean
        cmb_CostCenterID.Visible = myUtils.IsInList(myUtils.cStrTN(IDField), "CostCenterID")
        cmb_WBSElementID.Visible = myUtils.IsInList(myUtils.cStrTN(IDField), "WBSElementID")
        cmb_ProdLotID.Visible = myUtils.IsInList(myUtils.cStrTN(IDField), "ProdLotID")
        lblIDField.Visible = False


        If Not myUtils.IsInList(myUtils.cStrTN(IDField), "CostCenterID") Then cmb_CostCenterID.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(IDField), "WBSElementID") Then cmb_WBSElementID.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(IDField), "ProdLotID") Then cmb_ProdLotID.Value = DBNull.Value
        Return True
    End Function

    Private Sub cmb_IDField_Leave(sender As Object, e As EventArgs) Handles cmb_IDField.Leave, cmb_IDField.AfterCloseUp
        If Not IsNothing(cmb_IDField.SelectedRow) Then HandleIDField(myUtils.cStrTN(cmb_IDField.Value))
    End Sub

    Public Sub HandleCompanyID(CompanyID As Integer)
        dvWBS.RowFilter = "CompanyID = " & CompanyID
        dvCostCenter.RowFilter = "CompanyID = " & CompanyID
        dvLots.RowFilter = "CompanyID = " & CompanyID
    End Sub
End Class