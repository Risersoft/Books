Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmInvoiceItem
    Inherits frmMax
    Dim dvVC, dvTransType, dvUnits, dvHSN As DataView
    Friend fItem As risersoft.app.accounts.frmInvoiceItemGST, fMat As frmMax
    Friend WithEvents fCostAssign As risersoft.app.accounts.frmCostAssign
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        fItem = New risersoft.app.accounts.frmInvoiceItemGST
        fItem.AddtoTab(Me.UltraTabControl1, "GST", True)

        fCostAssign = New risersoft.app.accounts.frmCostAssign
        fCostAssign.AddtoTab(Me.UltraTabControl1, "Cost", True)

        AddHandler CtlPricingChild1.CellUpdated, AddressOf CellUpdated
    End Sub

    Private Sub CellUpdated(sender As Object, rChildElem As DataRow)
        If (Not IsNothing(myRow)) Then
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV")
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myWinSQL.AssignCmb(NewModel.dsCombo, "ClassType", "", Me.cmb_ClassType)
        dvTransType = myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType,, 2)
        dvVC = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxCredit", "", Me.cmb_TaxCredit)
        dvHSN = myWinSQL.AssignCmb(NewModel.dsCombo, "HsnSac", "", Me.cmb_Hsn_Sc,, 2)
        dvUnits = myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID,, 2)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            fCostAssign.HandleItem("InvoiceItemID", "PostingDate", myUtils.cValTN(fMat.myRow("CampusID")), myRow.Row)
            Dim nr As DataRow
            Dim dt1 As DataTable = r1.Table.DataSet.Tables("InvoiceItemGST")
            Dim rr() As DataRow = dt1.Select("InvoiceItemID" & "=" & myUtils.cValTN(myRow("InvoiceItemID")))
            If rr.Length > 0 Then
                nr = rr(0)
            Else
                nr = myTables.AddNewRow(dt1)
                nr("InvoiceItemID") = myUtils.cValTN(myRow("InvoiceItemID"))
            End If

            fItem.PrepFormRow(nr)
            fItem.HandleZeroRated(myUtils.cValTN(fItem.myRow("RT")), False)

            If Not IsNothing(fCostAssign.myView.mainGrid.myDv) Then fCostAssign.myView.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))
            If Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))
            If Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))

            HandleClassType(myUtils.cStrTN(myRow("ClassType")))
            risersoft.app.mxform.myFuncs.TransTypeFilter(dvVC, myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))

            If myUtils.cStrTN(fMat.myRow("P_gst")) = "Y" Then
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
            WinFormUtils.AddError(Me.cmb_ClassType, "Please Generate Transaction")
            Exit Function
        End If

        cm.EndCurrentEdit()
        If myUtils.cStrTN(fMat.myRow("P_gst")) = "N" AndAlso myUtils.NullNot(Me.cmb_TaxCredit.Value) Then WinFormUtils.AddError(Me.cmb_TaxCredit, "Select Tax Credit")
        If myUtils.cValTN(Me.txt_QtyRate.Text) <= 0 Then WinFormUtils.AddError(txt_QtyRate, "Enter Qty")
        If myUtils.NullNot(Me.cmb_ClassType.Value) Then WinFormUtils.AddError(Me.cmb_ClassType, "Select Class Type")
        If myUtils.NullNot(Me.cmb_ValuationClass.Value) Then WinFormUtils.AddError(Me.cmb_ValuationClass, "Select Valuation Class")
        If myUtils.NullNot(Me.cmb_TransType.Value) Then WinFormUtils.AddError(Me.cmb_TransType, "Select Trans Type")
        If (Me.cmb_Hsn_Sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(Me.cmb_Hsn_Sc, "Select Hsn Code")
        If IsNothing(Me.cmb_ItemUnitID.SelectedRow) Then WinFormUtils.AddError(cmb_ItemUnitID, "Select Unit")

        If fItem.CheckValid Then
            If (Me.cmb_Hsn_Sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(Me.cmb_Hsn_Sc, "Select Hsn Code")
        End If

        If fItem.VSave AndAlso Me.CanSave Then
            CtlPricingChild1.UpdatePricingTable(myRow.Row)
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV")
            fItem.CalculateGSTAmount(CtlPricingChild1, False, myUtils.cStrTN(cmb_TaxCredit.Value))
            VSave = True
        End If
    End Function

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key.ToUpper = "PRICING" Then CtlPricingChild1.UpdatePricingTable(myRow.Row)
        If Me.FormPrepared AndAlso e.Tab.Key.ToUpper = "GST" Then fItem.CalculateGSTAmount(CtlPricingChild1, False, myUtils.cStrTN(cmb_TaxCredit.Value))
    End Sub

    Private Sub HandleClassType(ClassType As String)
        dvUnits.RowFilter = ""
        If myUtils.IsInList(myUtils.cStrTN(ClassType), "S") Then
            If myUtils.cStrTN(fMat.myRow("P_gst")) = "Y" Then
                dvTransType.RowFilter = "CodeClass = 'Service'"
            Else
                dvTransType.RowFilter = "CodeClass = 'Service'  and CodeWord in ('Exp')"
            End If
            Me.fItem.cmb_ty.Value = "S"
            dvUnits.RowFilter = "UnitName = 'EA'"
            If cmb_ItemUnitID.SelectedRow Is Nothing Then
                If cmb_ItemUnitID.Rows.Count = 1 Then cmb_ItemUnitID.Value = myUtils.cValTN(cmb_ItemUnitID.Rows(0).Cells("ItemUnitID").Value)
            End If
            dvHSN.RowFilter = "Ty = 'S'"
        ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "A") Then
            dvTransType.RowFilter = "CodeClass = 'Asset'"
            Me.fItem.cmb_ty.Value = "G"
            dvHSN.RowFilter = "Ty = 'G'"
        End If
        WinFormUtils.ValidateComboValue(cmb_ValuationClass, myUtils.cStrTN(myRow("ValuationClass")))
        WinFormUtils.ValidateComboValue(cmb_TransType, myUtils.cStrTN(myRow("TransType")))
        If IsNothing(cmb_ValuationClass.SelectedRow) OrElse myUtils.cStrTN(cmb_ValuationClass.SelectedRow.Cells("ClassType").Value) <> myUtils.cStrTN(ClassType) Then cmb_ValuationClass.Value = DBNull.Value
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

    Private Sub cmb_ClassType_Leave(sender As Object, e As EventArgs) Handles cmb_ClassType.Leave, cmb_ClassType.AfterCloseUp
        If Not myUtils.NullNot(cmb_ClassType.SelectedRow) Then
            cmb_TransType.ReadOnly = False

            HandleClassType(myUtils.cStrTN(cmb_ClassType.Value))
        End If
    End Sub

    Private Sub cmb_TransType_Leave(sender As Object, e As EventArgs) Handles cmb_TransType.Leave, cmb_TransType.AfterCloseUp
        risersoft.app.mxform.myFuncs.TransTypeFilter(dvVC, myUtils.cStrTN(cmb_ClassType.Value), myUtils.cStrTN(cmb_TransType.Value))
    End Sub
End Class