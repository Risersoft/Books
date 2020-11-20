Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Public Class frmPurOrderItem
    Inherits frmMax
    Protected Friend fMat As frmMax, myViewMatReq As New clsWinView, fPurItemAccAss As frmPurItemAccAss
    Dim WithEvents ItemCodeSys As New clsCodeSystem
    Protected Friend dvTransType, dvClassType, dvClass As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(UltraGridDelSchedule)
        myViewMatReq.SetGrid(UltraGridMatRequisition)

        fPurItemAccAss = New frmPurItemAccAss
        fPurItemAccAss.AddtoTab(Me.UltraTabControl1, "AssCat", True)

        AddHandler CtlPricingChild1.CellUpdated, AddressOf CellUpdated
    End Sub

    Private Sub CellUpdated(sender As Object, rChildElem As DataRow)
        If (Not IsNothing(myRow)) Then
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV")
        End If
    End Sub

    Private Sub txt_BasicRate_Leave(sender As Object, e As EventArgs) Handles txt_BasicRate.Leave
        Me.CtlPricingChild1.SetElementField("bp", "valueperqty", myUtils.cValTN(Me.txt_BasicRate.Text))
    End Sub

    Private Sub btnLookup_Click(sender As Object, e As EventArgs) Handles btnLookup.Click
        Dim fg As New frmGrid
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@ItemVmsId", myUtils.cValTN(Me.CtlItemVMS1.ItemVMSID), GetType(Integer), False))
        Params.Add(New clsSQLParam("@VendorID", myUtils.cValTN(fMat.myRow("VendorID")), GetType(Integer), False))
        Dim Model As clsViewModel = fMat.GenerateParamsModel("lookuprate", Params)
        fg.myView.PrepEdit(Model)
        If fg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(fg.myView.mainGrid.myGrid.ActiveRow)
            If Not IsNothing(r1) Then
                Me.txt_BasicRate.Text = myUtils.cValTN(r1("BasicRate"))
                Me.CtlPricingChild1.SetElementField("bp", "ValuePerQty", myUtils.cValTN(r1("BasicRate")))
                If Me.FormPrepared Then
                    UpdatePricingTable()
                End If
            End If
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myView.PrepEdit(fMat.Model.GridViews("PurItemDet"))
        myViewMatReq.PrepEdit(fMat.Model.GridViews("MatReqItemPur"), , btnDelMR)

        ItemCodeSys.SetConf(NewModel.dsCombo.Tables("Items"), Me.cmb_ItemId, Me.cmbItemName, Me.cmb_BaseUnitID, cmb_BaseUnitID2)
        dvClass = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_QtyUnitID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_RateUnitId)
        dvClassType = myWinSQL.AssignCmb(NewModel.dsCombo, "ClassType", "", Me.cmb_ClassType,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "StockStage", "", Me.cmb_StockStage)
        dvTransType = myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType, , 1)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxCredit", "", Me.cmb_TaxCredit)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.cmb_ItemId.Value = myRow("ItemID")
            ItemCodeSys.HandleCombo(Me.cmb_ItemId, EnumWantEvent.acForceEvent)
            Me.CtlItemVMS1.ItemVMSID = myUtils.cValTN(myRow("ItemVMSId"))

            If myUtils.IsInList(myUtils.cStrTN(fMat.myRow("OrderType")), "JWO") Then
                dvClassType.RowFilter = "Code='S'"
                myRow("classtype") = "S"
            Else
                dvClassType.RowFilter = ""
            End If

            HandleTotalQty(myUtils.cValTN(myRow("TotalQty")))
            HandleTransType(dvClass, myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))

            If Not IsNothing(fPurItemAccAss.myView.mainGrid.myDv) Then fPurItemAccAss.myView.mainGrid.myDv.RowFilter = "PurItemID = " & myUtils.cValTN(myRow("PurItemID"))

            If fMat.myRow("OrderDate") < risersoft.app.mxform.myFuncs.GSTLounchDate Then
                cmb_TaxCredit.Visible = False
                lblTaxCredit.Visible = False
            Else
                cmb_TaxCredit.Visible = True
                lblTaxCredit.Visible = True

                If frmMode = EnumfrmMode.acAddM Then myRow("TaxCredit") = "Y"
            End If

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub ReadOnlyControlCommon(Enabled As Boolean)
        WinFormUtils.SetReadOnly(Me.UltraTabControl1.Tabs("General").TabPage, True, Enabled)
        txt_QtyRate.ReadOnly = True
        txt_QtyRecd.ReadOnly = True
        txt_RemQuan.ReadOnly = True
        cmb_BaseUnitID.ReadOnly = True
        cmb_QtyUnitID.ReadOnly = True
        cmb_RateUnitId.ReadOnly = True
        txt_TotalQty.ReadOnly = False
        cmb_BaseUnitID2.ReadOnly = True
        dt_Datecomp.ReadOnly = True

        Me.CtlItemVMS1.Enabled = Enabled
    End Sub

    Private Sub ReadOnlyControlPurItemHist(Enabled As Boolean)
        WinFormUtils.SetReadOnly(fPurItemAccAss, True, Enabled)
    End Sub

    Public Sub HandlePurItemHist(InputControl As System.Windows.Forms.Control, CtlPricing1 As risersoft.app.shared.ctlPricingParent)
        Dim rr2() As DataRow = Nothing, rr3() As DataRow = Nothing
        Dim rr1() As DataRow = fMat.dsForm.Tables("PurItemHist").Select("PurItemID = " & myUtils.cValTN(myRow("PurItemID")))
        If Not IsNothing(myViewMatReq.mainGrid.myDv) Then rr2 = myViewMatReq.mainGrid.myDv.Table.Select("PurItemID = " & myUtils.cValTN(myRow("PurItemID")) & "")

        If rr1.Length > 0 OrElse (Not IsNothing(rr2) AndAlso rr2.Length > 0) Then
            HandleClassType(myUtils.cStrTN(myRow("Classtype")))
            Me.ReadOnlyControlCommon(False)
        Else
            Me.ReadOnlyControlCommon(True)
            If Not IsNothing(InputControl) Then InputControl.Enabled = True
            HandleClassType(myUtils.cStrTN(myRow("Classtype")))
        End If

        If rr1.Length > 0 Then
            ReadOnlyControlPurItemHist(False)
            If Not IsNothing(InputControl) Then InputControl.Enabled = False
            If Not IsNothing(CtlPricing1) Then CtlPricingChild1.IsReadOnly = True
            txt_BasicRate.ReadOnly = True
            btnLookup.Enabled = False
        Else
            If Not IsNothing(CtlPricing1) Then CtlPricingChild1.IsReadOnly = False
            txt_BasicRate.ReadOnly = False
            btnLookup.Enabled = True
        End If

        If Not IsNothing(myViewMatReq.mainGrid.myDv) Then rr3 = myViewMatReq.mainGrid.myDv.Table.Select("PurItemID = " & myUtils.cValTN(myRow("PurItemID")) & " and isNull(isCompleted,0) = 1")
        If (Not IsNothing(rr3) AndAlso rr3.Length > 0) Then
            WinFormUtils.SetReadOnly(Me.UltraTabControl1.Tabs("MatReq").TabPage, True, False)
        Else
            WinFormUtils.SetReadOnly(Me.UltraTabControl1.Tabs("MatReq").TabPage, True, True)
        End If
    End Sub

    Private Sub ItemCodeSys_ItemChanged() Handles ItemCodeSys.ItemChanged
        cm.EndCurrentEdit()
        Me.CtlItemVMS1.InitVMS(myUtils.cValTN(cmb_ItemId.Value), , EnumWantEvent.acForceEvent)
        myRow("ItemName") = myUtils.cStrTN(cmbItemName.Text)
        myRow("ItemCode") = myUtils.cStrTN(Me.cmb_ItemId.Text)
        If Not myUtils.NullNot(cmb_ItemId.SelectedRow) Then
            If Not myUtils.NullNot(cmb_ItemId.SelectedRow.Cells("OrderQtyUnitId").Value) Then
                cmb_QtyUnitID.Value = cmb_ItemId.SelectedRow.Cells("OrderQtyUnitId").Value
            Else
                cmb_QtyUnitID.Value = cmb_ItemId.SelectedRow.Cells("ItemUnitId").Value
            End If

            If Not myUtils.NullNot(cmb_ItemId.SelectedRow.Cells("OrderRateUnitId").Value) Then
                cmb_RateUnitId.Value = cmb_ItemId.SelectedRow.Cells("OrderRateUnitId").Value
            Else
                cmb_RateUnitId.Value = cmb_ItemId.SelectedRow.Cells("ItemUnitId").Value
            End If
        End If
    End Sub

    Public Overrides Function VSave() As Boolean
        VSave = False
        Me.InitError()

        cm.EndCurrentEdit()

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_ItemId, "Please Generate Transaction")
            Exit Function
        End If

        If fMat.myRow("OrderDate") > risersoft.app.mxform.myFuncs.GSTLounchDate AndAlso myUtils.NullNot(Me.cmb_TaxCredit.Value) Then WinFormUtils.AddError(Me.cmb_TaxCredit, "Select Tax Credit")
        If myUtils.NullNot(cmb_ItemId.Value) Then WinFormUtils.AddError(cmb_ItemId, "Select Item Code")
        If myUtils.NullNot(cmbItemName.Value) Then WinFormUtils.AddError(cmbItemName, "Select Item Name")
        If Me.CtlItemVMS1.ItemVMSID = 0 Then WinFormUtils.AddError(Me.CtlItemVMS1, "Select Item Specification")
        If myUtils.cValTN(txt_TotalQty.Value) <= 0 Then WinFormUtils.AddError(txt_TotalQty, "Enter Total Qty")


        If myUtils.cValTN(cmb_ItemId.SelectedRow.Cells("OrderQtyNumReq").Value) = 1 Then
            Dim r1 As DataRow = CtlItemVMS1.SelectedVMSRow()
            If myUtils.cValTN(r1("QtyOrderNum")) <= 0 Then WinFormUtils.AddError(txt_TotalQty, "Please define convert factor, Then proceed.")
        End If


        If myUtils.NullNot(cmb_ClassType.Value) Then
            WinFormUtils.AddError(cmb_ClassType, "Select Class Type")
        Else
            If myUtils.IsInList(Me.cmb_ClassType.Value, "S", "A") Then
                If myUtils.NullNot(cmb_TransType.Value) Then WinFormUtils.AddError(cmb_TransType, "Select Trans Type")
            End If
            If myUtils.IsInList(Me.cmb_ClassType.Value, "S", "M") Then
                If myUtils.NullNot(cmb_StockStage.Value) Then WinFormUtils.AddError(cmb_StockStage, "Select Stock Stage")
            End If
        End If


        myViewMatReq.mainGrid.UpdateData()
        Dim QtyMatReq As Decimal = myViewMatReq.mainGrid.Model.GetColSum("QtyPur", "PurItemID = " & myUtils.cValTN(myRow("PurItemID")))
        If myUtils.cValTN(myRow("TotalQty")) < myUtils.cValTN(QtyMatReq) Then WinFormUtils.AddError(txt_TotalQty, "Enter Total Quantity According to Material Requisition")


        If myUtils.IsInList(myUtils.cStrTN(cmb_ClassType.Value), "A") Then
            If myUtils.NullNot(cmb_ValuationClass.Value) Then WinFormUtils.AddError(cmb_ValuationClass, "Select Valuation Class")
            fPurItemAccAss.myView.mainGrid.UpdateData()
            Dim QtyAccAss As Decimal = fPurItemAccAss.myView.mainGrid.Model.GetColSum("Qty", "PurItemID = " & myUtils.cValTN(myRow("PurItemID")))
            If myUtils.cValTN(myRow("TotalQty")) < myUtils.cValTN(QtyAccAss) Then WinFormUtils.AddError(txt_TotalQty, "Enter Total Quantity According to Account Assignment")
        End If

        myView.mainGrid.UpdateData()
        Dim QtyDelSch As Decimal = myView.mainGrid.Model.GetColSum("Quantity", "PurItemID = " & myUtils.cValTN(myRow("PurItemID")))
        If myUtils.IsInList(myUtils.cStrTN(fMat.myRow("OrderType")), "LPO") Then
            If myUtils.cValTN(myRow("TotalQty")) < myUtils.cValTN(QtyDelSch) Then WinFormUtils.AddError(txt_TotalQty, "Enter Total Quantity According to Delievery Schedule")
        Else
            If myUtils.cValTN(myRow("TotalQty")) <> myUtils.cValTN(QtyDelSch) Then WinFormUtils.AddError(txt_TotalQty, "Enter Total Quantity According to Delievery Schedule")
        End If

        For Each r1 As DataRow In fMat.dsForm.Tables("PurItemDet").Select("PurItemID = " & myUtils.cValTN(myRow("PurItemID")))
            If myUtils.cValTN(r1("Quantity")) < myUtils.cValTN(r1("QtyRecd")) Then WinFormUtils.AddError(txt_TotalQty, "Material Voucher already created. Total Quantity can not be less then Recieved Quantity.")
        Next

        If Me.CanSave Then
            myRow("ItemVMSId") = Me.CtlItemVMS1.ItemVMSID
            myRow("RateUnitID") = myUtils.cValTN(Me.cmb_RateUnitId.Value)
            myRow("QtyUnitID") = myUtils.cValTN(Me.cmb_QtyUnitID.Value)
            If fMat.frmMode = EnumfrmMode.acAddM Then myRow("IsCompleted") = False
            If fMat.frmMode = EnumfrmMode.acAddM Then myRow("Status") = "InComplete"
            SetOrderNumDescrip()

            CtlPricingChild1.UpdatePricingTable(myRow.Row)
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV")
            If myUtils.cValTN(fMat.myRow("ExchangeRate")) <> 0 Then
                myRow("RateCurrency") = myUtils.cValTN(myRow("BasicRate")) / myUtils.cValTN(fMat.myRow("ExchangeRate"))
            Else
                myRow("RateCurrency") = myUtils.cValTN(myRow("BasicRate"))
            End If
            VSave = True
        End If
    End Function

    Private Sub btnAddLots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLots.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@PidUnitID", myUtils.cValTN(fMat.myRow("PIDUnitID")), GetType(Integer), False))
        Dim rr() As DataRow = fMat.AdvancedSelect("prodlot", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In rr
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, fMat.dsForm.Tables("PurItemDet"))
                r2("PurItemID") = myUtils.cValTN(myRow("PurItemID"))
            Next
        End If
    End Sub

    Private Sub cmb_ClassType_Leave(sender As Object, e As EventArgs) Handles cmb_ClassType.Leave, cmb_ClassType.AfterCloseUp
        HandleClassType(myUtils.cStrTN(cmb_ClassType.Value))
        fPurItemAccAss.HandleItem()
    End Sub

    Private Sub btnSelectReq_Click(sender As Object, e As EventArgs) Handles btnSelectReq.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(fMat.myRow("CampusID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@itemvmsid", myUtils.cValTN(myRow("ItemVMSId")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@classtype", "'" & myUtils.cStrTN(cmb_ClassType.Value) & "'", GetType(String), False))
        If myUtils.IsInList(myUtils.cStrTN(fMat.myRow("OrderType")), "JWO") Then
            Params.Add(New clsSQLParam("@isagainstjwo", 1, GetType(Integer), False))
        Else
            Params.Add(New clsSQLParam("@isagainstjwo", 0, GetType(Integer), False))
        End If
        Dim rr() As DataRow = fMat.AdvancedSelect("itemreq", Params)

        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In rr
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, fMat.dsForm.Tables("MatReqItemPur"))
                r2("PurItemID") = myUtils.cValTN(myRow("PurItemID"))
            Next
        End If
    End Sub

    Public Function CalculateQty(r1 As DataRow) As Decimal
        Dim a As Decimal, r2 As DataRow

        If Not IsNothing(r1("ItemID")) Then
            Dim dt1 As DataTable = fMat.GenerateIDOutput("itemvms", myUtils.cValTN(r1("ItemVMSID"))).Data.Tables(0)
            If dt1.Rows.Count > 0 Then
                r2 = dt1.Rows(0)
                a = myItemUnit.ConvertFactor(Me.Controller, myUtils.cValTN(r1("ItemID")), myUtils.cValTN(r2("PIDUnitID")), myUtils.cValTN(r2("VarNum")), myUtils.cValTN(r1("QtyUnitID")), myUtils.cValTN(r1("RateUnitId")))
            End If
        End If
        Return a
    End Function

    Private Sub txt_TotalQty_Leave(sender As Object, e As EventArgs) Handles txt_TotalQty.Leave
        HandleTotalQty(myUtils.cValTN(txt_TotalQty.Value))
    End Sub

    Private Sub HandleTotalQty(TotalQty As Decimal)
        Dim a As Decimal = CalculateQty(myRow.Row)
        txt_QtyRate.Value = myUtils.cValTN(TotalQty) * myUtils.cValTN(a)
    End Sub

    Private Sub btnAddOther_Click(sender As Object, e As EventArgs) Handles btnAddOther.Click
        myView.mainGrid.ButtonAction("add")
        myView.mainGrid.myGrid.ActiveRow.Cells("PurItemID").Value = myUtils.cValTN(myRow("PurItemID"))
    End Sub

    Private Sub btnDelDS_Click(sender As Object, e As EventArgs) Handles btnDelDS.Click
        myView.mainGrid.ButtonAction("del")
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "Pricing" Then
            UpdatePricingTable()
        End If
    End Sub

    Private Sub UpdatePricingTable()
        cm.EndCurrentEdit()
        CtlPricingChild1.UpdatePricingTable(myRow.Row)
    End Sub

    Private Sub cmb_ValuationClass_Leave(sender As Object, e As EventArgs) Handles cmb_ValuationClass.Leave, cmb_ValuationClass.AfterCloseUp
        fPurItemAccAss.HandleItem()
    End Sub

    Private Sub SetOrderNumDescrip()
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@totalqty", myUtils.cValTN(myRow("TotalQty")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@itemvmsid", myUtils.cValTN(myRow("ItemVMSID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@qtyunitid", myUtils.cValTN(myRow("QtyUnitID")), GetType(Integer), False))
        Dim oRet As clsProcOutput = fMat.GenerateParamsOutput("ordernumdescrip", Params)
        If oRet.Success Then
            myRow("OrderNumDescrip") = oRet.Description
        End If
    End Sub

    Private Sub HandleClassType(ClassType As String)
        Me.cmb_ValuationClass.ReadOnly = True
        If Not IsNothing(myViewMatReq.mainGrid.myDv) AndAlso myViewMatReq.mainGrid.myDv.Count = 0 Then Me.cmb_TransType.ReadOnly = False
        If Not IsNothing(myViewMatReq.mainGrid.myDv) AndAlso myViewMatReq.mainGrid.myDv.Count = 0 Then Me.cmb_StockStage.ReadOnly = False
        UltraTabControl1.Tabs("AssCat").Visible = False

        If Not myUtils.IsInList(myUtils.cStrTN(ClassType), "") Then
            If myUtils.IsInList(myUtils.cStrTN(ClassType), "A") Then
                dvTransType.RowFilter = "CodeClass = 'Asset'"
                If Not IsNothing(myViewMatReq.mainGrid.myDv) AndAlso myViewMatReq.mainGrid.myDv.Count = 0 Then cmb_ValuationClass.ReadOnly = False
                Me.cmb_StockStage.ReadOnly = True
                Me.cmb_StockStage.Value = DBNull.Value

                UltraTabControl1.Tabs("AssCat").Visible = True
            ElseIf myUtils.IsInList(myUtils.cStrTN(ClassType), "S") Then
                dvTransType.RowFilter = "CodeClass = 'Service'"
            Else
                cmb_ValuationClass.Value = DBNull.Value
                Me.cmb_TransType.ReadOnly = True
            End If
            If cmb_TransType.SelectedRow Is Nothing Then cmb_TransType.Value = DBNull.Value
        End If
    End Sub

    Private Sub cmb_TransType_Leave(sender As Object, e As EventArgs) Handles cmb_TransType.Leave, cmb_TransType.AfterCloseUp
        HandleTransType(dvClass, myUtils.cStrTN(cmb_ClassType.Value), myUtils.cStrTN(cmb_TransType.Value))
    End Sub

    Private Sub HandleTransType(dv As DataView, ClassType As String, TransType As String)
        risersoft.app.mxform.myFuncs.TransTypeFilter(dv, ClassType, TransType)
        If Not myUtils.IsInList(myUtils.cStrTN(ClassType), "") Then
            If myUtils.IsInList(myUtils.cStrTN(ClassType), "S") Then
                Me.cmb_ValuationClass.Value = "LABOUR"
            End If
        End If
        fPurItemAccAss.HandleItem()
    End Sub

    Private Sub btnCopyDate_Click(sender As Object, e As EventArgs) Handles btnCopyDate.Click
        Dim rr() As DataRow = fMat.dsForm.Tables("PurItemDet").Select("PurItemID = " & myUtils.cValTN(myRow("PurItemID")))
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In rr
                r1("DeliveryDate") = myUtils.cStrTN(rr(0)("DeliveryDate"))
            Next
        End If
    End Sub
End Class