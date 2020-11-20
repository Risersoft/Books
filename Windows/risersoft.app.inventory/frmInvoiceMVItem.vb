Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmInvoiceMVItem
    Inherits frmMax
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
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxCredit", "", Me.cmb_TaxCredit)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            fCostAssign.HandleItem("InvoiceMVItemID", "PostingDate", myUtils.cValTN(fMat.myRow("CampusID")), myRow.Row)
            Dim nr As DataRow
            Dim dt1 As DataTable = r1.Table.DataSet.Tables("InvoiceItemGST")
            Dim rr() As DataRow = dt1.Select("MatVouchItemID" & "=" & myUtils.cValTN(myRow("MatVouchItemID")))
            If rr.Length > 0 Then
                nr = rr(0)
            Else
                nr = myTables.AddNewRow(dt1)
                nr("MatVouchItemID") = myUtils.cValTN(myRow("MatVouchItemID"))
            End If

            fItem.PrepFormRow(nr)
            fItem.HandleZeroRated(myUtils.cValTN(fItem.myRow("RT")), False)

            If Not IsNothing(fCostAssign.myView.mainGrid.myDv) Then fCostAssign.myView.mainGrid.myDv.RowFilter = "InvoiceMVItemID = " & myUtils.cValTN(myRow("InvoiceMVItemID"))
            If Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "InvoiceMVItemID = " & myUtils.cValTN(myRow("InvoiceMVItemID"))
            If Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "InvoiceMVItemID = " & myUtils.cValTN(myRow("InvoiceMVItemID"))

            HandleClassType(myUtils.cStrTN(myRow("ClassType")))

            If myUtils.cStrTN(fMat.myRow("P_gst")) = "Y" Then
                cmb_TaxCredit.Visible = False
                lblTaxCredit.Visible = False
            Else
                cmb_TaxCredit.Visible = True
                lblTaxCredit.Visible = True
            End If

            CtlPricingChild1.UpdatePricingTable(myRow.Row)
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.CtlPricingChild1, "Please Generate Transaction", Me.eBag)
            Exit Function
        End If

        If myUtils.cStrTN(fMat.myRow("P_gst")) = "N" AndAlso myUtils.NullNot(Me.cmb_TaxCredit.Value) Then WinFormUtils.AddError(Me.cmb_TaxCredit, "Select Tax Credit")

        If fItem.VSave AndAlso Me.CanSave Then
            cm.EndCurrentEdit()

            CtlPricingChild1.UpdatePricingTable(myRow.Row)
            fItem.CalculateGSTAmount(CtlPricingChild1, False, myUtils.cStrTN(myRow("TaxCredit")))
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
            VSave = True
        End If
    End Function

    Private Sub HandleClassType(ClassType As String)
        If myUtils.IsInList(myUtils.cStrTN(ClassType), "M") Then
            Me.fItem.cmb_ty.Value = "G"
        ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "S") Then
            Me.fItem.cmb_ty.Value = "S"
        ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "A") Then
            Me.fItem.cmb_ty.Value = "G"
        End If
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "Pricing" Then CtlPricingChild1.UpdatePricingTable(myRow.Row)
        If Me.FormPrepared AndAlso e.Tab.Key = "GST" Then fItem.CalculateGSTAmount(CtlPricingChild1, False, myUtils.cStrTN(cmb_TaxCredit.Value))
    End Sub
End Class