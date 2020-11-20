Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmODNoteSpare
    Inherits frmMax
    Friend fMat As frmODNote, fSoItemSelect As frmSOItemSelect, fPackList As frmPackList
    Dim dv, dv1, dvVC, dvHSN As DataView
    Friend WithEvents fCostAssign As risersoft.app.accounts.frmCostAssign

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        fSoItemSelect = New frmSOItemSelect
        fSoItemSelect.AddtoTab(Me.UltraTabControl1, "Serial", True)
        fSoItemSelect.fParentSp = True
        Me.UltraTabControl1.Tabs("Serial").Visible = False


        fPackList = New frmPackList
        fPackList.AddtoTab(Me.UltraTabControl1, "PackingSP", True)
        fPackList.fParent = Me
        fPackList.fParentID = "ODNoteSpareID"

        fCostAssign = New risersoft.app.accounts.frmCostAssign
        fCostAssign.AddtoTab(Me.UltraTabControl1, "Cost", True)

        AddHandler CtlPricingChild1.CellUpdated, AddressOf CellUpdated
    End Sub

    Private Sub CellUpdated(sender As Object, rChildElem As DataRow)
        If (Not IsNothing(myRow)) Then
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV")
        End If
    End Sub

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            fSoItemSelect.HandleItem()
            If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 Then fSoItemSelect.myView.mainGrid.myDv.RowFilter = "ODNoteSpareId = " & myUtils.cValTN(myRow("ODNoteSpareId")) & ""

            fCostAssign.HandleItem("ODNoteSpareID", "ChallanDate", myUtils.cValTN(fMat.myRow("CampusID")), myRow.Row)

            If myUtils.IsInList(myUtils.cStrTN(fMat.myRow("ChallanType")), risersoft.app.mxform.myFuncs.ChallanStr()) Then
                If Not IsNothing(fCostAssign.myView.mainGrid.myDv) Then fCostAssign.myView.mainGrid.myDv.RowFilter = "ODNoteSpareId = " & myUtils.cValTN(myRow("ODNoteSpareId"))
                If Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "ODNoteSpareId = " & myUtils.cValTN(myRow("ODNoteSpareId"))
                If Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "ODNoteSpareId = " & myUtils.cValTN(myRow("ODNoteSpareId"))
            End If

            If myUtils.cValTN(myRow("PIDUnitID")) > 0 Then
                    Me.UltraTabControl1.Tabs("Serial").Visible = True
                Else
                    Me.UltraTabControl1.Tabs("Serial").Visible = False
                End If

                If myUtils.IsInList(myUtils.cStrTN(myRow("ItemType")), "AC") Then
                    CtlReadOnly(False)
                Else
                    CtlReadOnly(True)
                End If

                If myUtils.IsInList(myUtils.cStrTN(myRow("ItemType")), "PS", "AC", "SP") Then
                    dvHSN.RowFilter = "Ty = 'G'"
                Else
                    dvHSN.RowFilter = "Ty = 'S'"
                End If

            WinFormUtils.ValidateComboValue(cmb_hsn_sc, myUtils.cStrTN(myRow("hsn_sc")))
            If myUtils.IsInList(myUtils.cStrTN(myRow("ItemType")), "PS") Then btnGenerate.Visible = True Else btnGenerate.Visible = False

                cmb_ValuationClass.Value = myUtils.cStrTN(myRow("ValuationClass"))
                risersoft.app.mxform.myFuncs.TransTypeFilter(dvVC, myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))

                If Not IsNothing(myRow) Then
                    fPackList.myView.mainGrid.myDv.RowFilter = "ODNoteSpareId = " & myUtils.cValTN(myRow("ODNoteSpareId"))
                    fPackList.InitSort("ODNoteSpareId = " & myUtils.cValTN(myRow("ODNoteSpareId")))
                End If

                Me.FormPrepared = True
            End If
            Return Me.FormPrepared
    End Function

    Private Sub CtlReadOnly(Bool As Boolean)
        txt_ItemDescrip.ReadOnly = Bool
        txt_itemsuffix.Visible = Bool
        lblSuffix.Visible = Bool
        cmb_TransType.ReadOnly = Bool
        cmb_ValuationClass.ReadOnly = Bool
        cmb_hsn_sc.ReadOnly = Bool
        cmb_ItemUnitID.ReadOnly = Bool
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim str As String = ""
        If Not IsNothing(fMat.myViewSpare.mainGrid.myGrid.ActiveRow) Then
            For Each r3 As DataRow In fSoItemSelect.myView.mainGrid.myDv.Table.Select("ODNoteSpareID = " & myUtils.cValTN(fMat.myViewSpare.mainGrid.myGrid.ActiveRow.Cells("ODNoteSpareID").Value) & "")
                If str.Trim.Length > 0 Then
                    If myUtils.cStrTN(r3("ProdSerialNum")).Trim.Length > 0 Then str = str & ", " & myUtils.cStrTN(r3("ProdSerialNum"))
                Else
                    str = myUtils.cStrTN(r3("ProdSerialNum"))
                End If
            Next
            txt_idenmarks.Text = myUtils.cStrTN(str)
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        fPackList.myView.PrepEdit(NewModel.GridViews("PackListSP"))
        myWinSQL.AssignCmb(NewModel.dsCombo, "ItemType", "", Me.cmb_ItemType)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID)
        dvVC = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass,, 2)
        dvHSN = myWinSQL.AssignCmb(NewModel.dsCombo, "HsnSac", "", Me.cmb_hsn_sc,, 2)
        Return True
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        If IsNothing(myRow) Then
            If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "SCSP", "TRSO", "SCSTB", "TRJB") Then
                WinFormUtils.AddError(Me.cmb_ItemType, "Please Generate Transaction")
            Else
                VSave = True
            End If
            Exit Function
        End If

        If ((Not myUtils.IsInList(myUtils.cStrTN(myRow("ItemType")), "AC")) OrElse (myUtils.IsInList(myUtils.cStrTN(myRow("ItemType")), "AC") AndAlso myUtils.cValTN(myRow("BasicRate")) <> 0)) AndAlso (cmb_hsn_sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(cmb_hsn_sc, "Please Select HSN Code")
        If (cmb_ItemUnitID.SelectedRow) Is Nothing Then WinFormUtils.AddError(cmb_ItemUnitID, "Please Select Item Unit")
        If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "TRSO", "TRJW", "TRWS") AndAlso myUtils.NullNot(txt_Nature.Value) Then WinFormUtils.AddError(txt_Nature, "Please Enter Nature")
        If Me.CanSave Then
            cm.EndCurrentEdit()

            If Not myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "RCWB") Then
                CtlPricingChild1.UpdatePricingTable(myRow.Row)
                CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV")
                SetRTValue()
            End If
            VSave = True
        End If
    End Function

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "PricingSP" Then
            CtlPricingChild1.UpdatePricingTable(myRow.Row)
        End If
    End Sub

    Private Sub SetRTValue()
        myRow("RT") = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGST", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("SGST", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("CGST", "PerValue")), 2)
    End Sub

    Private Sub cmb_TransType_Leave(sender As Object, e As EventArgs) Handles cmb_TransType.Leave, cmb_TransType.AfterCloseUp
        risersoft.app.mxform.myFuncs.TransTypeFilter(dvVC, "M", myUtils.cStrTN(cmb_TransType.Value))
        fSoItemSelect.HandleItem()
    End Sub
End Class