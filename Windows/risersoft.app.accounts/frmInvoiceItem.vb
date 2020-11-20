Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmInvoiceItem
    Inherits frmMax
    Public fItem As frmInvoiceItemGST
    Friend dvInvItemType As DataView
    Friend InvFICO As Boolean = False
    Dim dvVC, dvSpare, dvService, dvTransType, dvHSN, dvUnits As DataView
    Public WithEvents fSoItemSelect As frmSOItemSelect, fMat As frmMax, InvItemType As String
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
            Me.OnFinished()
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dvVC = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass, , True)
        dvInvItemType = myWinSQL.AssignCmb(NewModel.dsCombo, "InvoiceItemType", "", Me.cmb_InvoiceItemType, , True)
        myWinSQL.AssignCmb(NewModel.dsCombo, "ClassType", "", Me.cmb_ClassType, , True)
        dvTransType = myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType, , True)
        myWinSQL.AssignCmb(NewModel.dsCombo, "ReturnFY", "", Me.cmb_ReturnFY)
        dvHSN = myWinSQL.AssignCmb(NewModel.dsCombo, "HsnSac", "", Me.cmb_Hsn_Sc,, 2)
        dvUnits = myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxCredit", "", Me.cmb_TaxCredit)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            fSoItemSelect.HandleItem()
            fCostAssign.HandleItem("InvoiceItemID", "PostingDate", myUtils.cValTN(fMat.myRow("CampusID")), myRow.Row)

            If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 Then
                If Not IsNothing(fSoItemSelect.myView.mainGrid.myDv) Then fSoItemSelect.myView.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))
            Else
                If Not IsNothing(fSoItemSelect.myVueFA.mainGrid.myDv) Then fSoItemSelect.myVueFA.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))
            End If

            If myUtils.IsInList(myUtils.cStrTN(fMat.myRow("InvoiceTypeCode")), "IR") Then myRow("ReturnFY") = "C"

            If myRow("SubSortIndex").ToString.Length > 0 Then
                dvInvItemType.RowFilter = "CodeWord in ('IST', 'PIS', 'PIC'" & InvItemType & ")"
            Else
                dvInvItemType.RowFilter = "CodeWord in ('IGT', 'IGS', 'GHC','IST', 'PIS', 'PIC'" & InvItemType & ")"
            End If
            HandleInvoiceType(myUtils.cStrTN(myRow("InvoiceItemType")))

            Dim ExistCSV As String = myUtils.MakeCSV(myRow.Row.Table.Select("SortIndex = " & myUtils.cValTN(myRow("SortIndex")) & " and SubSortIndex is Not NULL"), "SubSortIndex")
            If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceItemType")), "IGT", "IGS", "GHC") AndAlso ExistCSV.Length > 0 AndAlso ExistCSV <> 0 Then
                cmb_InvoiceItemType.ReadOnly = True
            End If

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
            fItem.HandleZeroRated(myUtils.cValTN(fItem.myRow("RT")), myUtils.IsInList(myUtils.cStrTN(fMat.myRow("GSTInvoiceType")), "EXP"))

            If Not IsNothing(fCostAssign.myView.mainGrid.myDv) Then fCostAssign.myView.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))
            If Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))
            If Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "InvoiceItemID = " & myUtils.cValTN(myRow("InvoiceItemID"))

            HandleClassType(myUtils.cStrTN(myRow("ClassType")))
            HandleTransType(myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))

            If fMat.dsForm.Tables.Contains("ODNoteAdd") Then
                Dim rr1() As DataRow = fMat.dsForm.Tables("ODNoteAdd").Select()
                If rr1.Length > 0 Then
                    WinFormUtils.SetReadOnly(Me, True)
                    fItem.cmb_GstTaxType.ReadOnly = False
                End If
            End If


            cmb_TaxCredit.Visible = myUtils.IsInList(myUtils.cStrTN(fMat.myRow("InvoiceTypeCode")), "IR", "QD", "QC")
            lblTaxCredit.Visible = myUtils.IsInList(myUtils.cStrTN(fMat.myRow("InvoiceTypeCode")), "IR", "QD", "QC")

            If Not InvFICO Then
                cmb_ReturnFY.Visible = False
                lblReturnFY.Visible = False
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_InvoiceItemType, "Please Generate Transaction")
            Exit Function
        End If


        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceItemType")), "GHC", "IST", "ISP") AndAlso myUtils.cValTN(Me.txt_QtyRate.Text) <= 0 Then WinFormUtils.AddError(txt_QtyRate, "Enter Qty")
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceItemType")), "GHC", "IST", "ISP") AndAlso IsNothing(Me.cmb_ItemUnitID.SelectedRow) Then WinFormUtils.AddError(cmb_ItemUnitID, "Select Unit")
        If myUtils.NullNot(Me.cmb_InvoiceItemType.Value) Then WinFormUtils.AddError(Me.cmb_InvoiceItemType, "Select Invoice Item Type")
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceItemType")), "GHC", "IST", "IGS") AndAlso myUtils.NullNot(Me.cmb_ClassType.Value) Then WinFormUtils.AddError(Me.cmb_ClassType, "Select Class Type")
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceItemType")), "GHC", "IST", "IGS") AndAlso myUtils.NullNot(Me.cmb_TransType.Value) Then WinFormUtils.AddError(Me.cmb_TransType, "Select Trans Type")
        If Not myUtils.IsInList(myUtils.cStrTN(fMat.myRow("InvoiceTypeCode")), "PM", "QD") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceItemType")), "GHC", "IST", "IGS") AndAlso myUtils.NullNot(Me.cmb_ValuationClass.Value) Then WinFormUtils.AddError(Me.cmb_ValuationClass, "Select Valuation Class")
        If InvFICO = True AndAlso myUtils.cStrTN(Me.txt_Description.Text).Trim.Length = 0 Then WinFormUtils.AddError(Me.txt_Description, "Enter Description")

        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceItemType")), "GHC", "IST", "ISP") AndAlso fItem.CheckValid Then
            If (Me.cmb_Hsn_Sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(Me.cmb_Hsn_Sc, "Select Hsn Code")
        End If

        If myUtils.IsInList(myUtils.cStrTN(fMat.myRow("InvoiceTypeCode")), "IR", "QD", "QC") Then
            If myUtils.NullNot(Me.cmb_TaxCredit.Value) Then WinFormUtils.AddError(Me.cmb_TaxCredit, "Select Tax Credit")
        End If

        If fItem.VSave AndAlso Me.CanSave Then
            cm.EndCurrentEdit()
            CtlPricingChild1.UpdatePricingTable(myRow.Row)
            fItem.CalculateGSTAmount(CtlPricingChild1, myUtils.IsInList(myUtils.cStrTN(fMat.myRow("GSTInvoiceType")), "EXP"), myUtils.cStrTN(cmb_TaxCredit.Value))
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
            VSave = True
        End If
    End Function

    Private Sub HandleTransType(Classtype As String, TransType As String)
        risersoft.app.mxform.myFuncs.TransTypeFilter(dvVC, Classtype, TransType)
        WinFormUtils.ValidateComboValue(cmb_ValuationClass, myUtils.cStrTN(myRow("ValuationClass")))
        If (Not cmb_ValuationClass.SelectedRow Is Nothing) AndAlso (myUtils.cStrTN(cmb_ValuationClass.SelectedRow.Cells("ClassType").Value) <> myUtils.cStrTN(Classtype)) Then cmb_ValuationClass.Value = DBNull.Value
    End Sub

    Private Sub HandleClassType(ClassType As String)
        dvUnits.RowFilter = ""
        If myUtils.IsInList(myUtils.cStrTN(ClassType), "M") Then
            dvTransType.RowFilter = "CodeClass = 'Material'"
            Me.fItem.cmb_ty.Value = "G"
            dvHSN.RowFilter = "Ty = 'G'"
        ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "S") Then
            dvTransType.RowFilter = "CodeClass = 'Service'"
            Me.fItem.cmb_ty.Value = "S"
            dvHSN.RowFilter = "Ty = 'S'"
            dvUnits.RowFilter = "UnitName = 'EA'"
            If cmb_ItemUnitID.SelectedRow Is Nothing Then
                If cmb_ItemUnitID.Rows.Count = 1 Then cmb_ItemUnitID.Value = myUtils.cValTN(cmb_ItemUnitID.Rows(0).Cells("ItemUnitID").Value)
            End If
        ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "A") Then
            dvTransType.RowFilter = "CodeClass = 'Asset'"
            Me.fItem.cmb_ty.Value = "G"
            dvHSN.RowFilter = "Ty = 'G'"
        End If

        WinFormUtils.ValidateComboValue(cmb_TransType, myUtils.cStrTN(myRow("TransType")))
        If myUtils.cValTN(myRow("ItemUnitID")) > 0 Then cmb_ItemUnitID.Value = myUtils.cValTN(myRow("ItemUnitID"))
    End Sub

    Private Sub cmb_ClassType_Leave(sender As Object, e As EventArgs) Handles cmb_ClassType.Leave, cmb_ClassType.AfterCloseUp
        If Not myUtils.NullNot(cmb_ClassType.SelectedRow) Then
            fSoItemSelect.HandleItem()
            HandleClassType(cmb_ClassType.Value)
        End If
    End Sub

    Private Sub cmb_InvoiceItemType_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceItemType.Leave, cmb_InvoiceItemType.AfterCloseUp
        fSoItemSelect.HandleItem()
        HandleInvoiceType(myUtils.cStrTN(cmb_InvoiceItemType.Value))
    End Sub

    Private Sub HandleInvoiceType(InvoiceItemType As String)
        CtlPricingChild1.Enabled = False
        ReadOnlyCtl(InvoiceItemType, "")
        UltraTabControl1.Tabs("AccAss").Visible = False
        If myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "PIC", "PIS", "IGT") Then
            NullCtl()
            If myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "PIC") Then myRow("SerialNum") = DBNull.Value
        ElseIf myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "IST", "ISP") Then
            Dim rr1() As DataRow = fMat.myView.mainGrid.myDv.Table.Select("SortIndex=" & myUtils.cValTN(myRow("SortIndex")) & " and SubSortIndex is Null ")
            If rr1.Length > 0 Then
                If myUtils.IsInList(myUtils.cStrTN(rr1(0)("InvoiceItemType")), "IGS", "GHC") Then
                    ReadOnlyCtl(InvoiceItemType, myUtils.cStrTN(rr1(0)("InvoiceItemType")))
                    myRow("TransType") = myUtils.cStrTN(rr1(0)("TransType"))
                    myRow("ClassType") = myUtils.cStrTN(rr1(0)("ClassType"))
                    If myUtils.IsInList(myUtils.cStrTN(rr1(0)("InvoiceItemType")), "GHC") Then myRow("ValuationClass") = myUtils.cStrTN(rr1(0)("ValuationClass"))
                    If myUtils.IsInList(myUtils.cStrTN(rr1(0)("InvoiceItemType")), "IGS") Then
                        risersoft.app.mxform.myFuncs.TransTypeFilter(dvVC, myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))
                    End If
                End If
            End If
            CtlPricingChild1.Enabled = True
            UltraTabControl1.Tabs("AccAss").Visible = True
        End If
    End Sub

    Private Sub ReadOnlyCtl(InvoiceItemType As String, PInvoiceItemType As String)
        cmb_InvoiceItemType.ReadOnly = fSoItemSelect.SelectionCount > 0
        cmb_ClassType.ReadOnly = fSoItemSelect.SelectionCount > 0 OrElse myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "PIC", "PIS", "IGT") OrElse myUtils.IsInList(myUtils.cStrTN(PInvoiceItemType), "IGS", "GHC")
        cmb_TransType.ReadOnly = fSoItemSelect.SelectionCount > 0 OrElse myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "PIC", "PIS", "IGT") OrElse myUtils.IsInList(myUtils.cStrTN(PInvoiceItemType), "IGS", "GHC")
        cmb_ValuationClass.ReadOnly = fSoItemSelect.SelectionCount > 0 OrElse myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "PIC", "PIS", "IGT") OrElse myUtils.IsInList(myUtils.cStrTN(PInvoiceItemType), "GHC")
        txt_QtyRate.ReadOnly = fSoItemSelect.SelectionCount > 0 OrElse myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "PIC", "PIS", "IGT", "IGS")
        cmb_ItemUnitID.ReadOnly = fSoItemSelect.SelectionCount > 0 OrElse myUtils.IsInList(myUtils.cStrTN(InvoiceItemType), "PIC", "PIS", "IGT", "IGS")
    End Sub

    Private Sub btnAddHSN_Click(sender As Object, e As EventArgs) Handles btnAddHSN.Click
        Dim f As New frmHsnSac
        If f.PrepForm(myView, EnumfrmMode.acAddM, "") Then
            f.ShowDialog()
            If Not IsNothing(f.myRow) Then
                Dim nr As DataRow = myUtils.CopyOneRow(f.myRow.Row, dvHSN.Table)
                nr("Description") = f.myRow.Row("Code") & "-" & f.myRow.Row("Description")
            End If
        End If
    End Sub

    Private Sub NullCtl()
        cmb_ClassType.Value = DBNull.Value
        cmb_TransType.Value = DBNull.Value
        cmb_ValuationClass.Value = DBNull.Value
        txt_QtyRate.Value = DBNull.Value
        cmb_ItemUnitID.Value = DBNull.Value
    End Sub

    Private Sub fSoItemSelect_ItemAdded(sender As Object, e As System.EventArgs) Handles fSoItemSelect.ItemAdded
        CtlPricingChild1.Enabled = False
        ReadOnlyCtl("", "")
    End Sub

    Private Sub fSoItemSelect_ItemDeleted(sender As Object, e As System.EventArgs) Handles fSoItemSelect.ItemDeleted
        If fSoItemSelect.SelectionCount = 0 Then
            CtlPricingChild1.Enabled = False
            ReadOnlyCtl("", "")
        End If
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "Pricing" Then CtlPricingChild1.UpdatePricingTable(myRow.Row)
        If Me.FormPrepared AndAlso e.Tab.Key = "GST" Then fItem.CalculateGSTAmount(CtlPricingChild1, myUtils.IsInList(myUtils.cStrTN(fMat.myRow("GSTInvoiceType")), "EXP"), myUtils.cStrTN(cmb_TaxCredit.Value))
    End Sub

    Private Sub cmb_TransType_Leave(sender As Object, e As EventArgs) Handles cmb_TransType.Leave, cmb_TransType.AfterCloseUp
        HandleTransType(myUtils.cStrTN(cmb_ClassType.Value), myUtils.cStrTN(cmb_TransType.Value))
        fSoItemSelect.HandleItem()
    End Sub
End Class