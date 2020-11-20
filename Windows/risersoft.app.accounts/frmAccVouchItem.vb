Imports risersoft.app.mxent
Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmAccVouchItem
    Inherits frmMax
    Friend fMat As frmAccVouch
    Dim WithEvents AccCodeSystem As New clsCodeSystem
    Friend dv, dvEmp, dvCamp As DataView
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridLedger)
        ControlStatus("")
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dv = New DataView(NewModel.dsCombo.Tables("GLAccount"))
        AccCodeSystem.SetConf(dv, Me.cmb_GLAccountID, Me.cmbGLAccount)

        myWinSQL.AssignCmb(NewModel.dsCombo, "Vendor", "", Me.cmb_VendorID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Customer", "", Me.cmb_CustomerID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "FixedAsset", "", Me.cmb_FixedAssetID)
        dvCamp = myWinSQL.AssignCmb(NewModel.dsCombo, "Campus", "", Me.cmb_CampusId,, 2)
        dvEmp = myWinSQL.AssignCmb(NewModel.dsCombo, "Employee", "", Me.cmb_EmployeeId,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "AmountDC", "", Me.cmb_AmountDC)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxAreaCode", "", Me.cmb_TaxAreaID)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            GLAccountRowFilter(fMat.myRow("VouchDate"))

            Me.cmb_GLAccountID.Value = myRow("GLAccountID")
            AccCodeSystem.HandleCombo(Me.cmb_GLAccountID, EnumWantEvent.acForceEvent)
            If Me.frmMode = EnumfrmMode.acEditM Then cmb_GLAccountID.Value = myUtils.cValTN(myRow("GLAccountID"))
            If Not IsNothing(cmb_GLAccountID.SelectedRow) AndAlso myUtils.cValTN(cmb_GLAccountID.Value) > 0 Then
                HandleGlAccount(myUtils.cStrTN(cmb_GLAccountID.SelectedRow.Cells("SubLedgerType").Value))
            End If

            UltraTabControl1.Tabs("General").Selected = True
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub GLAccountRowFilter(dated As Date)
        Dim oMap As New clsAccountMap(Me.Controller)
        Dim AP_GLAccountID As Integer = oMap.FindGLAccountID(fMat.myRow("VouchDate"), "SUP", "CR", "INV")
        Dim AR_GLAccountID As Integer = oMap.FindGLAccountID(fMat.myRow("VouchDate"), "CUS", "DR", "INV")
        Dim AI_GLAccountID As Integer = oMap.FindGLAccountID(fMat.myRow("VouchDate"), "IMP", "DR", "")
        Dim Str As String = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "StartDate", "FinishDate", 0)

        Dim rCompany As DataRow = Me.Controller.CommonData.rCompany(myUtils.cValTN(fMat.myRow("CompanyID")))

        If (Not IsNothing(rCompany)) AndAlso fMat.myRow("VouchDate") >= rCompany("FinStartDate") Then
            dv.RowFilter = "GlAccountID Not in (" & AP_GLAccountID & "," & AR_GLAccountID & "," & AI_GLAccountID & ") and " & Str
        Else
            dv.RowFilter = Str
        End If
    End Sub

    Private Sub AccCodeSystem_ItemChanged() Handles AccCodeSystem.ItemChanged
        cm.EndCurrentEdit()

        myRow("AccName") = myUtils.cStrTN(cmbGLAccount.Text)
        If myUtils.cValTN(cmb_GLAccountID.Text) > 0 Then myRow("AccCode") = myUtils.cValTN(cmb_GLAccountID.Text)
        If Not IsNothing(cmb_GLAccountID.SelectedRow) Then HandleGlAccount(myUtils.cStrTN(cmb_GLAccountID.SelectedRow.Cells("SubLedgerType").Value))

    End Sub

    Public Overrides Function VSave() As Boolean
        Dim str As String = ""
        Me.InitError()
        VSave = False

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_GLAccountID, "Please Generate Transaction")
            Exit Function
        End If

        cm.EndCurrentEdit()
        If IsNothing(Me.cmb_GLAccountID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_GLAccountID, "Select Account Name")
        If IsNothing(Me.cmb_AmountDC.SelectedRow) Then WinFormUtils.AddError(Me.cmb_AmountDC, "Select DR/CR")
        If IsNothing(Me.txt_Amount.Value) Then WinFormUtils.AddError(Me.txt_Amount, "Enter Amount")

        If Not IsNothing(cmb_GLAccountID.SelectedRow) Then str = myUtils.cStrTN(cmb_GLAccountID.SelectedRow.Cells("SubLedgerType").Value)
        If str = "V" AndAlso IsNothing(Me.cmb_VendorID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_VendorID, "Select Vendor")
        If str = "C" AndAlso IsNothing(Me.cmb_CustomerID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_CustomerID, "Select Customer")
        If str = "A" AndAlso IsNothing(Me.cmb_CampusId.SelectedRow) Then WinFormUtils.AddError(Me.cmb_CampusId, "Select Campus")
        If str = "F" AndAlso IsNothing(Me.cmb_FixedAssetID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_FixedAssetID, "Select Fixed Asset")
        If str = "E" AndAlso IsNothing(Me.cmb_EmployeeId.SelectedRow) Then WinFormUtils.AddError(Me.cmb_EmployeeId, "Select Employee")
        If str = "T" AndAlso IsNothing(Me.cmb_TaxAreaID.SelectedRow) Then WinFormUtils.AddError(Me.cmb_TaxAreaID, "Select Tax Area")

        If Me.CanSave Then
            cm.EndCurrentEdit()

            VSave = True
        End If
    End Function

    Private Sub HandleGlAccount(SubLedgerType As String)
        ControlStatus(SubLedgerType)
        If Not myUtils.IsInList(myUtils.cStrTN(SubLedgerType), "") Then
            lblIDField.Visible = True
            If SubLedgerType = "V" Then
                cmb_VendorID.Visible = True
                lblIDField.Text = "Vendor"
            ElseIf SubLedgerType = "C" Then
                cmb_CustomerID.Visible = True
                lblIDField.Text = "Customer"
            ElseIf SubLedgerType = "E" Then
                cmb_EmployeeId.Visible = True
                btnSelect.Visible = True
                lblIDField.Text = "Employee"
            ElseIf SubLedgerType = "A" Then
                cmb_CampusId.Visible = True
                lblIDField.Text = "Campus"
            ElseIf SubLedgerType = "F" Then
                cmb_FixedAssetID.Visible = True
                lblIDField.Text = "Assets"
            ElseIf SubLedgerType = "T" Then
                cmb_TaxAreaID.Visible = True
                lblIDField.Text = "Tax Area"
            End If
        End If
    End Sub

    Private Function ControlStatus(SubLedgerType As String) As Boolean
        cmb_VendorID.Visible = False
        cmb_CustomerID.Visible = False
        cmb_CampusId.Visible = False
        cmb_EmployeeId.Visible = False
        cmb_FixedAssetID.Visible = False
        cmb_TaxAreaID.Visible = False
        lblIDField.Visible = False
        btnSelect.Visible = False

        If Not myUtils.IsInList(myUtils.cStrTN(SubLedgerType), "C") Then cmb_CustomerID.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(SubLedgerType), "A") Then cmb_CampusId.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(SubLedgerType), "E") Then cmb_EmployeeId.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(SubLedgerType), "F") Then cmb_FixedAssetID.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(SubLedgerType), "V") Then cmb_VendorID.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(SubLedgerType), "T") Then cmb_TaxAreaID.Value = DBNull.Value
        Return True
    End Function

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If e.Tab.Key = "Ledger" Then
            Dim Params As New List(Of clsSQLParam)
            Dim r1 As DataRow = Me.Controller.CommonData.FinRow(fMat.dt_VouchDate.Value)
            If Not myUtils.NullNot(r1) Then
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(fMat.cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@glaccountid", myUtils.cValTN(myRow("GLAccountID")), GetType(Integer), False))
                Params.Add(New clsSQLParam("@openbaldate", Format(r1("StDate"), "dd-MMM-yyyy"), GetType(DateTime), False))
                Dim oModel As clsViewModel = fMat.GenerateParamsModel("Ledger", Params)
                myView.GenView(oModel, EnumViewCallType.acNormal)
            End If
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(fMat.dt_VouchDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim rr() As DataRow = fMat.AdvancedSelect("employee", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            Me.cm.EndCurrentEdit()
            myRow("employeeid") = myUtils.cValTN(rr(0)("EmployeeID"))
            myRow("SubLedgerTypeDescrip") = "Employee"
            myRow("SubLedgerTypeValue") = myUtils.cStrTN(rr(0)("FullName"))
            Me.cm.Refresh()
        End If
    End Sub

    Public Sub HandleDate(dated As Date)
        dvEmp.RowFilter = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "JoinDate", "LeaveDate", 12)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        GLAccountRowFilter(dated)
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        If myUtils.cValTN(cmb_VendorID.Value) > 0 Then
            Me.cm.EndCurrentEdit()
            myRow("SubLedgerTypeDescrip") = "Vendor"
            myRow("SubLedgerTypeValue") = myUtils.cStrTN(cmb_VendorID.Text)
        End If
    End Sub

    Private Sub cmb_CustomerID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        If myUtils.cValTN(cmb_CustomerID.Value) > 0 Then
            Me.cm.EndCurrentEdit()
            myRow("SubLedgerTypeDescrip") = "Customer"
            myRow("SubLedgerTypeValue") = myUtils.cStrTN(cmb_CustomerID.Text)
        End If
    End Sub

    Private Sub cmb_CampusId_Leave(sender As Object, e As EventArgs) Handles cmb_CampusId.Leave, cmb_CampusId.AfterCloseUp
        If myUtils.cValTN(cmb_CampusId.Value) > 0 Then
            Me.cm.EndCurrentEdit()
            myRow("SubLedgerTypeDescrip") = "Campus"
            myRow("SubLedgerTypeValue") = myUtils.cStrTN(cmb_CampusId.Text)
        End If
    End Sub

    Private Sub cmb_TaxAreaID_Leave(sender As Object, e As EventArgs) Handles cmb_TaxAreaID.Leave, cmb_TaxAreaID.AfterCloseUp
        If myUtils.cValTN(cmb_TaxAreaID.Value) > 0 Then
            Me.cm.EndCurrentEdit()
            myRow("SubLedgerTypeDescrip") = "Tax Area"
            myRow("SubLedgerTypeValue") = myUtils.cStrTN(cmb_TaxAreaID.Text)
        End If
    End Sub
End Class