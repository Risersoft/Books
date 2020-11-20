Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmInvoiceItemPurch
    Inherits frmMax
    Friend fItem As frmInvoiceItemGST, fMat As frmMax
    Dim dvVC, dvTransType2, dvHSN As DataView
    Friend InvFICO As Boolean = False
    Friend INVHR As Boolean = False
    Friend WithEvents dvTransType, dv, dvUnits As DataView, fSoItemSelect As frmSOItemSelect, TransTypeFilter As String
    Friend WithEvents fCostAssign As frmCostAssign

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        fSoItemSelect = New frmSOItemSelect
        fSoItemSelect.AddtoTab(Me.UltraTabControl1, "accass", True)
        fSoItemSelect.ctlPricingChild = Me.CtlPricingChild1

        fCostAssign = New frmCostAssign
        fCostAssign.AddtoTab(Me.UltraTabControl1, "Cost", True)


        fItem = New frmInvoiceItemGST
        fItem.AddtoTab(Me.UltraTabControl1, "GST", True)

        AddHandler CtlPricingChild1.CellUpdated, AddressOf CellUpdated
    End Sub

    Private Sub CellUpdated(sender As Object, rChildElem As DataRow)
        If (Not IsNothing(myRow)) Then
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dvVC = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass, , True)
        dv = myWinSQL.AssignCmb(NewModel.dsCombo, "ClassType", "", Me.cmb_ClassType, , True)
        dvTransType = myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType, , 2)
        dvTransType2 = myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType2,   , 2)
        dvTransType2.RowFilter = "CodeClass = 'Service'"
        dvHSN = myWinSQL.AssignCmb(NewModel.dsCombo, "HsnSac", "", Me.cmb_Hsn_Sc,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxCredit", "", Me.cmb_TaxCredit)
        dvUnits = myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID,, 2)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow, IdField As String, TableName As String, DateField As String) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            fSoItemSelect.HandleItem()
            fCostAssign.HandleItem(IdField, DateField, myUtils.cValTN(fMat.myRow("CampusID")), myRow.Row)

            If frmMode = EnumfrmMode.acAddM AndAlso INVHR = True Then
                myRow("ClassType") = "S"
                myRow("TransType") = "Exp"
                myRow("ValuationClass") = "LABOUR"
                myRow("QtyRate") = 1
            End If

            Dim nr As DataRow
            Dim dt1 As DataTable = r1.Table.DataSet.Tables(TableName)
            Dim rr() As DataRow = dt1.Select(IdField & "=" & myUtils.cValTN(myRow(IdField)))
            If rr.Length > 0 Then
                nr = rr(0)
            Else
                nr = myTables.AddNewRow(dt1)
                nr(IdField) = myUtils.cValTN(myRow(IdField))
            End If
            fItem.PrepFormRow(nr)
            fItem.HandleZeroRated(myUtils.cValTN(fItem.myRow("RT")), False)
            If Not IsNothing(fSoItemSelect.myVueFA.mainGrid.myDv) Then fSoItemSelect.myVueFA.mainGrid.myDv.RowFilter = "" & IdField & " = " & myUtils.cValTN(myRow(IdField))

            If Not IsNothing(fCostAssign.myView.mainGrid.myDv) Then fCostAssign.myView.mainGrid.myDv.RowFilter = "" & IdField & " = " & myUtils.cValTN(myRow(IdField))
            If Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "" & IdField & " = " & myUtils.cValTN(myRow(IdField))
            If Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "" & IdField & " = " & myUtils.cValTN(myRow(IdField))

            HandleClassType(myUtils.cStrTN(myRow("ClassType")))
            HandleTransType(myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))

            If fMat.Name.ToUpper = "FRMPAYMENTSALE" OrElse myUtils.cStrTN(fMat.myRow("P_gst")) = "Y" Then
                cmb_TaxCredit.Visible = False
                lblTaxCredit.Visible = False
            Else
                cmb_TaxCredit.Visible = True
                lblTaxCredit.Visible = True
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_ValuationClass, "Please Generate Transaction")
            Exit Function
        End If

        If (Not fMat.Name.ToUpper = "FRMPAYMENTSALE") AndAlso myUtils.cStrTN(fMat.myRow("P_gst")) = "N" Then
            If myUtils.NullNot(Me.cmb_TaxCredit.Value) Then WinFormUtils.AddError(Me.cmb_TaxCredit, "Select Tax Credit")
        End If

        If fItem.CheckValid Then
            If (Me.cmb_Hsn_Sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(Me.cmb_Hsn_Sc, "Select Hsn Code")
        End If

        If IsNothing(Me.cmb_ValuationClass.SelectedRow) Then WinFormUtils.AddError(Me.cmb_ValuationClass, "Select Valuation Class")
        If IsNothing(Me.cmb_TransType.SelectedRow) Then WinFormUtils.AddError(Me.cmb_TransType, "Select Trans Type")
        If IsNothing(Me.cmb_ClassType.SelectedRow) Then WinFormUtils.AddError(Me.cmb_ClassType, "Select Class Type")
        If myUtils.cValTN(Me.txt_QtyRate.Text) = 0 Then WinFormUtils.AddError(Me.txt_QtyRate, "Enter Qty")
        If myUtils.cStrTN(Me.txt_Description.Text).Trim.Length = 0 Then WinFormUtils.AddError(Me.txt_Description, "Enter Description")
        If IsNothing(Me.cmb_ItemUnitID.SelectedRow) Then WinFormUtils.AddError(cmb_ItemUnitID, "Select Unit")

        If fItem.VSave AndAlso Me.CanSave Then
            cm.EndCurrentEdit()
            CtlPricingChild1.UpdatePricingTable(myRow.Row)
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
            fItem.CalculateGSTAmount(CtlPricingChild1, False, myUtils.cStrTN(cmb_TaxCredit.Value))
            VSave = True
        Else
            Me.ShowError()
        End If
    End Function

    Private Sub cmb_ClassType_Leave(sender As Object, e As EventArgs) Handles cmb_ClassType.Leave, cmb_ClassType.AfterCloseUp
        If Not myUtils.IsInList(myUtils.cStrTN(cmb_ClassType.Value), "") Then HandleClassType(cmb_ClassType.SelectedRow.Cells("CodeWord").Value)
    End Sub

    Private Sub HandleClassType(ClassType As String)
        dvUnits.RowFilter = ""
        fSoItemSelect.HandleItem()
        ReadOnlyCtl(False)
        cmb_TransType2.Visible = False
        lblTransType2.Visible = False
        cmb_TransType2.Value = DBNull.Value
        If Not myUtils.IsInList(myUtils.cStrTN(ClassType), "") Then
            If myUtils.IsInList(myUtils.cStrTN(ClassType), "M") Then
                dvTransType.RowFilter = "CodeClass = 'Material' " & TransTypeFilter
                Me.fItem.cmb_ty.Value = "G"
                dvHSN.RowFilter = "Ty = 'G'"
            ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "S") Then
                If myUtils.cStrTN(fMat.myRow("P_gst")) = "Y" Then
                    dvTransType.RowFilter = "CodeClass = 'Service'  " & TransTypeFilter
                Else
                    'Exp for Purchase and SUN For Sales.
                    dvTransType.RowFilter = "CodeClass = 'Service'  and CodeWord in ('Exp','SUN')"
                End If
                Me.fItem.cmb_ty.Value = "S"
                dvUnits.RowFilter = "UnitName = 'EA'"
                If cmb_ItemUnitID.SelectedRow Is Nothing Then
                    If cmb_ItemUnitID.Rows.Count = 1 Then cmb_ItemUnitID.Value = myUtils.cValTN(cmb_ItemUnitID.Rows(0).Cells("ItemUnitID").Value)
                End If
                dvHSN.RowFilter = "Ty = 'S'"
            ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "A") Then
                If InvFICO = True Then
                    cmb_TransType2.Visible = True
                    lblTransType2.Visible = True
                End If
                dvTransType.RowFilter = "CodeClass = 'Asset'  " & TransTypeFilter
                Me.fItem.cmb_ty.Value = "G"
                dvHSN.RowFilter = "Ty = 'G'"
            End If

            WinFormUtils.ValidateComboValue(cmb_TransType, myUtils.cStrTN(myRow("TransType")))
            cmb_ItemUnitID.Value = myUtils.cValTN(myRow("ItemUnitID"))
            If fSoItemSelect.SelectionCount > 0 Then ReadOnlyCtl(True)
        End If
    End Sub

    Private Sub cmb_ValuationClass_Leave(sender As Object, e As EventArgs) Handles cmb_ValuationClass.Leave, cmb_ValuationClass.AfterCloseUp
        fSoItemSelect.HandleItem()
    End Sub

    Private Sub fSoItemSelect_ItemAdded(sender As Object, e As System.EventArgs) Handles fSoItemSelect.ItemAdded
        ReadOnlyCtl(True)
    End Sub

    Private Sub fSoItemSelect_ItemDeleted(sender As Object, e As System.EventArgs) Handles fSoItemSelect.ItemDeleted
        If fSoItemSelect.SelectionCount = 0 Then ReadOnlyCtl(False)
    End Sub

    Private Sub ReadOnlyCtl(Bool As Boolean)
        cmb_ClassType.ReadOnly = Bool
        cmb_TransType.ReadOnly = Bool
        cmb_ValuationClass.ReadOnly = Bool
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "Pricing" Then CtlPricingChild1.UpdatePricingTable(myRow.Row)
        If Me.FormPrepared AndAlso e.Tab.Key = "GST" Then fItem.CalculateGSTAmount(CtlPricingChild1, False, myUtils.cStrTN(cmb_TaxCredit.Value))
    End Sub

    Private Sub cmb_TransType_Leave(sender As Object, e As EventArgs) Handles cmb_TransType.Leave, cmb_TransType.AfterCloseUp
        HandleTransType(myUtils.cStrTN(cmb_ClassType.Value), myUtils.cStrTN(cmb_TransType.Value))
        fSoItemSelect.HandleItem()
    End Sub

    Private Sub HandleTransType(Classtype As String, TransType As String)
        risersoft.app.mxform.myFuncs.TransTypeFilter(dvVC, Classtype, TransType)
        WinFormUtils.ValidateComboValue(cmb_ValuationClass, myUtils.cStrTN(myRow("ValuationClass")))
        If IsNothing(cmb_ValuationClass.SelectedRow) OrElse myUtils.cStrTN(cmb_ValuationClass.SelectedRow.Cells("ClassType").Value) <> myUtils.cStrTN(Classtype) Then cmb_ValuationClass.Value = DBNull.Value
    End Sub

    Private Sub btnAddHSN_Click(sender As Object, e As EventArgs) Handles btnAddHSN.Click
        Dim f As New frmHsnSac
        If f.PrepForm(myView, EnumfrmMode.acAddM, "") Then
            f.ShowDialog()
            If Not IsNothing(f.myRow) Then
                Dim nr As DataRow = myUtils.CopyOneRow(f.myRow.Row, dsCombo.Tables("HsnSac"))
                nr("Description") = f.myRow.Row("Code") & "-" & f.myRow.Row("Description")
                cmb_Hsn_Sc.Value = myUtils.cStrTN(f.myRow.Row("Code"))
            End If
        End If
    End Sub
End Class