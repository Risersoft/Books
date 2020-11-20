Imports risersoft.app.inventory
Imports risersoft.app.mxent

Public Class frmODNoteItem
    Inherits frmMax
    Friend fMat As frmODNote, dvMvtCode, dvODNSpare As DataView, fSoItemSelect As frmSOItemSelect, fPackList As frmPackList
    Dim myViewStBalCamp, myViewStBalDep, myViewReceipt As New clsWinView, StockItemID As Integer, ObjGetMatVouch As New clsGetRecordsMatVouch
    Dim dvQtyDes, dvQtySrc, dvSpSt, dvStStage, dvStStage2, dvTTyDes, dvReason, dvCamp, dvValCls, dvHSN As DataView, oMasterMM As clsMasterDataMM
    Friend WithEvents fItemSelect As frmItemSelect
    Dim WithEvents MvtCodeSystem As New clsCodeSystem
    Friend WithEvents fCostAssign As risersoft.app.accounts.frmCostAssign

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myViewStBalCamp.SetGrid(UltraGridStBalCamp)
        myViewStBalDep.SetGrid(UltraGridStBalDep)
        myViewReceipt.SetGrid(UltraGridRec)

        fItemSelect = New frmItemSelect
        fItemSelect.AddtoTab(Me.UltraTabControl1, "Material", True)

        fCostAssign = New risersoft.app.accounts.frmCostAssign
        fCostAssign.AddtoTab(Me.UltraTabControl1, "Cost", True)

        fSoItemSelect = New frmSOItemSelect
        fSoItemSelect.AddtoTab(Me.UltraTabControl1, "SalesOrder", True)
        fSoItemSelect.ctlPricingChild = Me.CtlPricingChild1

        fPackList = New frmPackList
        fPackList.AddtoTab(Me.UltraTabControl1, "PackingItem", True)
        fPackList.fParent = Me
        fPackList.fParentID = "ODNoteItemID"
        ControlStatus()

        AddHandler CtlPricingChild1.CellUpdated, AddressOf CellUpdated
    End Sub

    Private Sub CellUpdated(sender As Object, rChildElem As DataRow)
        If (Not IsNothing(myRow)) AndAlso (Not IsNothing(cmb_MvtCode.SelectedRow)) Then
            If Not myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("PricingType").Value), "BR") Then CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "", "AmountTrWV")
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dvMvtCode = New DataView(NewModel.dsCombo.Tables("MatMvtCode"))
        MvtCodeSystem.SetConf(dvMvtCode, Me.cmb_MvtCode, cmbMvtCode)

        dvReason = myWinSQL.AssignCmb(NewModel.dsCombo, "Reason", "", Me.cmb_MatMvtReasonID, , 2)

        dvCamp = myWinSQL.AssignCmb(NewModel.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Vendor", "", Me.cmb_VendorID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Customer", "", Me.cmb_CustomerID)

        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitIDEntry)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_OrderRateUnitID)

        dvQtyDes = myWinSQL.AssignCmb(NewModel.dsCombo, "QtyType", "", Me.cmb_QtyTypeDes, , 2)
        dvQtySrc = myWinSQL.AssignCmb(NewModel.dsCombo, "QtyType", "", Me.cmb_QtyTypeSrc, , 2)
        dvStStage = myWinSQL.AssignCmb(NewModel.dsCombo, "StockStage", "", Me.cmb_StockStage, , 2)
        dvStStage2 = myWinSQL.AssignCmb(NewModel.dsCombo, "StockStage", "", Me.cmb_StockStage2, , 2)
        dvTTyDes = myWinSQL.AssignCmb(NewModel.dsCombo, "TaxType", "", Me.cmb_TaxType2, , 2)
        dvSpSt = myWinSQL.AssignCmb(NewModel.dsCombo, "SpStock", "", Me.cmb_SpStock, , 2)
        dvHSN = myWinSQL.AssignCmb(NewModel.dsCombo, "HsnSac", "", Me.cmb_hsn_sc,, 2)
        dvHSN.RowFilter = "Ty = 'G'"
        dvValCls = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass,, 2)
        dvValCls.RowFilter = "ClassType = 'M'"
        myWinSQL.AssignCmb(NewModel.dsCombo, "SupplyType", "", Me.cmb_sply_ty)

        oMasterMM = New clsMasterDataMM(Me.Controller)

        fItemSelect.BindModel(NewModel)

        fPackList.myView.PrepEdit(NewModel.GridViews("PackListItem"))
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.FormPrepared = True
            fItemSelect.HandleItem(myUtils.cValTN(fMat.myRow("SalesOrderID")))
            fSoItemSelect.HandleItem()
            If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 Then fSoItemSelect.myView.mainGrid.myDv.RowFilter = "ODNoteItemID = " & myUtils.cValTN(myRow("ODNoteItemID")) & ""

            fCostAssign.HandleItem("ODNoteItemID", "ChallanDate", myUtils.cValTN(fMat.myRow("CampusID")), myRow.Row)

            If Not myUtils.IsInList(myUtils.cStrTN(fMat.myRow("ChallanType")), risersoft.app.mxform.myFuncs.ChallanStr()) Then
                If Not IsNothing(fCostAssign.myView.mainGrid.myDv) Then fCostAssign.myView.mainGrid.myDv.RowFilter = "ODNoteItemID = " & myUtils.cValTN(myRow("ODNoteItemID"))
                If Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "ODNoteItemID = " & myUtils.cValTN(myRow("ODNoteItemID"))
                If Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "ODNoteItemID = " & myUtils.cValTN(myRow("ODNoteItemID"))
            End If


            r1("ClassType") = "M"
            HandleQtyEntry()

            If Not IsNothing(fMat.cmb_ChallanType.SelectedRow) Then dvMvtCode.RowFilter = "DocSubType = '" & myUtils.cStrTN(fMat.myRow("ChallanType").Substring(0, myUtils.cValTN(fMat.cmb_ChallanType.SelectedRow.Cells("Tag3").Value))) & "' and " & ObjGetMatVouch.MvtCodeFilter(myUtils.cStrTN(fMat.myRow("TaxType")), dvMvtCode)
            If frmMode = EnumfrmMode.acAddM AndAlso cmb_MvtCode.Rows.Count = 1 Then
                cmb_MvtCode.Value = myUtils.cValTN(cmb_MvtCode.Rows(0).Cells("MatMvtCode").Value)
            End If

            If Not myUtils.NullNot(cmb_MvtCode.Value) Then
                MvtCodeLeaveEvent()
                cmbMvtCode.Value = cmb_MvtCode.Value
            End If

            HandleChallanType(myUtils.cStrTN(fMat.cmb_ChallanType.Value))

            dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, myUtils.cDateTN(fMat.myRow("ChallanDate"), DateTime.MinValue), "WODate", "CompletedOn", "CampusID")

            If Not IsNothing(myRow) Then
                fPackList.myView.mainGrid.myDv.RowFilter = "OdNoteitemID = " & myUtils.cValTN(myRow("OdNoteitemID"))
                fPackList.InitSort("OdNoteitemID = " & myUtils.cValTN(myRow("OdNoteitemID")))
            End If
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()

        End If
        Return Me.FormPrepared
    End Function

    Private Sub HandleChallanType(ChallanType As String)
        If myUtils.IsInList(ChallanType, "RM", "ST", "TS") Then
            cmb_sply_ty.Visible = True
            lblsplyty.Visible = True
        Else
            cmb_sply_ty.Visible = False
            lblsplyty.Visible = False
        End If
    End Sub

    Private Sub cmb_MvtCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_MvtCode.Leave, cmb_MvtCode.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) Then
            MvtCodeLeaveEvent()
        End If
    End Sub

    Private Sub btnAddHSN_Click(sender As Object, e As EventArgs) Handles btnAddHSN.Click
        Dim f As New frmHsnSac
        If f.PrepForm(myView, EnumfrmMode.acAddM, "") Then
            f.ShowDialog()
            If Not IsNothing(f.myRow) Then
                Dim nr As DataRow = myUtils.CopyOneRow(f.myRow.Row, dsCombo.Tables("HsnSac"))
                nr("Description") = f.myRow.Row("Code") & "-" & f.myRow.Row("Description")
                cmb_hsn_sc.Value = myUtils.cStrTN(f.myRow.Row("Code"))
            End If
        End If
    End Sub

    Public Overrides Function VSave() As Boolean
        Dim ValidateSoItem As Boolean = True
        Me.InitError()
        VSave = False

        If IsNothing(myRow) Then
            If Not myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "SCSP", "TRSO", "SCSTB", "TRJB") Then
                WinFormUtils.AddError(Me.txt_QtyEntry, "Please Generate Transaction")
            Else
                VSave = True
            End If
            Exit Function
        ElseIf myRow.Row.RowState = DataRowState.Deleted Then
            VSave = True
            Exit Function
        End If

        cm.EndCurrentEdit()

        If myUtils.NullNot(fItemSelect.cmb_ItemId.Value) Then WinFormUtils.AddError(fItemSelect.cmb_ItemId, "Please Select Item Code", Me.eBag)
        If myUtils.NullNot(cmb_MvtCode.Value) Then WinFormUtils.AddError(cmb_MvtCode, "Please Select Mvt. Code")
        If myUtils.NullNot(cmb_SpStock.Value) Then WinFormUtils.AddError(cmb_SpStock, "Please Select Sp. Stock")
        If myUtils.cValTN(txt_QtyEntry.Value) <= 0 Then WinFormUtils.AddError(txt_QtyEntry, "Please Enter Quantity")
        If Not myUtils.NullNot(cmb_ItemUnitID2.Value) AndAlso myUtils.cValTN(txt_QtySKU2.Value) = 0 Then WinFormUtils.AddError(txt_QtySKU2, "Please define Convert factor for Item")

        If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "RM", "ST", "TS") AndAlso myUtils.NullNot(cmb_sply_ty.Value) Then WinFormUtils.AddError(cmb_sply_ty, "Please Select Supply Type")
        If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "TRSO", "TRJW", "TRWS") AndAlso myUtils.NullNot(txt_Nature.Value) Then WinFormUtils.AddError(txt_Nature, "Please Enter Nature")
        If (cmb_hsn_sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(cmb_hsn_sc, "Please Select HSN Code")
        If Me.CanSave AndAlso Me.ValidateData Then
            If Not IsNothing(cmb_MvtCode.SelectedRow) Then
                If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "PID") Then
                    If myUtils.cValTN(fItemSelect.txt_pidunitid.Text) = 0 Then WinFormUtils.AddError(fItemSelect.lbl_WOInfo, "Select Work Order", Me.eBag)
                End If

                If Not myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "SCUB", "SCSP", "SCST", "TRSO", "SCSTB", "TRJB") AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "SO") Then
                    ValidateSoItem = fSoItemSelect.ValidateData(cmb_MvtCode)
                End If
            End If

            If Me.CanSave AndAlso fItemSelect.ValidateData AndAlso ValidateSoItem Then
                cm.EndCurrentEdit()
                myRow("UnitName") = myUtils.cStrTN(cmb_ItemUnitIDEntry.Text)
                If Not myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "SCSP", "TRSO", "SCSTB", "RCWB", "TRJB") Then
                    CtlPricingChild1.UpdatePricingTable(myRow.Row)
                    If Not myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("PricingType").Value), "BR") Then CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "", "AmountTrWV")
                    If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("PricingType").Value), "BR") Then myRow("AmountTot") = myUtils.cValTN(myRow("BasicRate")) * myUtils.cValTN(myRow("QtyEntry"))

                    SetRTValue()
                End If
                VSave = True
            End If
        End If
    End Function

    Public Overrides Function ValidateData() As Boolean
        Me.InitError()
        If ObjGetMatVouch.CalculateFieldType("V", True, myRow.Row, oMasterMM, "CustomerID") = "R" AndAlso myUtils.NullNot(fMat.cmb_CustomerID.Value) Then WinFormUtils.AddError(fMat.cmb_CustomerID, "Please Select Voucher Customer Name", Me.eBag)
        If ObjGetMatVouch.CalculateFieldType("V", True, myRow.Row, oMasterMM, "VendorID") = "R" AndAlso myUtils.NullNot(fMat.cmb_VendorID.Value) Then WinFormUtils.AddError(fMat.cmb_VendorID, "Please Select Voucher Vendor Name", Me.eBag)
        If ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "TaxTypeDes") = "R" AndAlso myUtils.NullNot(cmb_TaxType2.Value) Then WinFormUtils.AddError(cmb_TaxType2, "Please Select Tax Type")
        If myUtils.NullNot(cmb_StockStage.Value) Then WinFormUtils.AddError(cmb_StockStage, "Please Select Stock Stage")
        If ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "StockStage2") = "R" AndAlso myUtils.NullNot(cmb_StockStage2.Value) Then WinFormUtils.AddError(cmb_StockStage2, "Please Select Stock Stage2")
        If ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "MatMvtReasonID") = "R" AndAlso myUtils.NullNot(cmb_MatMvtReasonID.Value) Then WinFormUtils.AddError(cmb_MatMvtReasonID, "Please Select MatMvt Reason")
        If ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "CustomerID") = "R" AndAlso myUtils.NullNot(cmb_CustomerID.Value) Then WinFormUtils.AddError(cmb_CustomerID, "Please Select Customer Name")
        If ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "VendorID") = "R" AndAlso myUtils.NullNot(cmb_VendorID.Value) Then WinFormUtils.AddError(cmb_VendorID, "Please Select Vendor Name")
        If ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "CampusID") = "R" AndAlso myUtils.NullNot(cmb_campusid.Value) Then WinFormUtils.AddError(cmb_campusid, "Please Select Campus Name")
        If ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "QtyTypeSrc") = "R" AndAlso myUtils.NullNot(cmb_QtyTypeSrc.Value) Then WinFormUtils.AddError(cmb_QtyTypeSrc, "Please Select Qty Type Src.")
        If ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "QtyTypeDes") = "R" AndAlso myUtils.NullNot(cmb_QtyTypeDes.Value) Then WinFormUtils.AddError(cmb_QtyTypeDes, "Please Select Qty Type Des.")
        Return Me.CanSave
    End Function

    Private Sub txt_QtyEntry_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_QtyEntry.Leave
        HandleQtyEntry()
    End Sub

    Private Sub HandleQtyEntry()
        fItemSelect.CalculateQty()
    End Sub

    Private Sub MvtCodeLeaveEvent()
        cmb_SpStock.Value = oMasterMM.MvtCodeField("SpStockType", cmb_MvtCode.Value, "", dvSpSt, "", cmb_SpStock.Value)

        If Not myUtils.NullNot(cmb_SpStock.Value) Then
            SpStockLeaveEvent()
        End If

        If Not IsNothing(cmb_MvtCode.SelectedRow) Then myRow("MvtType") = myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("MvtType").Value)

        cmb_StockStage.Value = oMasterMM.MvtCodeField("StockStage", cmb_MvtCode.Value, "", dvStStage, ",", cmb_StockStage.Value)
        cmb_StockStage2.Value = oMasterMM.MvtCodeField("StockStage2", cmb_MvtCode.Value, "", dvStStage2, ",", cmb_StockStage2.Value)
        cmb_TaxType2.Value = oMasterMM.MvtCodeField("TaxTypeDes", cmb_MvtCode.Value, "", dvTTyDes, ",", cmb_TaxType2.Value)

        SetFieldMvtCode()
        fSoItemSelect.HandleItem()
        fItemSelect.CalculateQty()
    End Sub

    Private Sub SpStockLeaveEvent()
        cmb_QtyTypeDes.Value = oMasterMM.MvtCodeField("QtyTypeDes", cmb_MvtCode.Value, cmb_SpStock.Value, dvQtyDes, ",", cmb_QtyTypeDes.Value)
        cmb_QtyTypeSrc.Value = oMasterMM.MvtCodeField("QtyTypeSrc", cmb_MvtCode.Value, cmb_SpStock.Value, dvQtySrc, ",", cmb_QtyTypeSrc.Value)

        Me.cm.EndCurrentEdit()

        SetFieldSpStock()
    End Sub

    Private Sub SetFieldMvtCode()
        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_TaxType2, lblTaxDes, myRow.Row, "I", "TaxTypeDes", False, True)
        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_StockStage2, lblStockStage2, myRow.Row, "I", "StockStage2", False, True)
        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_MatMvtReasonID, Nothing, myRow.Row, "I", "MatMvtReasonID", False)
        If cmb_MatMvtReasonID.ReadOnly = False Then dvReason.RowFilter = "MatMvtCode = " & myUtils.cValTN(cmb_MvtCode.Value)
        If ObjGetMatVouch.HandleFormField(oMasterMM, txt_BasicRate, lblBasicRate, myRow.Row, "I", "BasicRate", False, True) = "N" Then txt_BasicRate.Value = DBNull.Value

        If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "RCWB") Then
            lblBasicRate.Visible = False
            txt_BasicRate.Visible = False
        End If
    End Sub

    Private Sub SetFieldSpStock()
        ObjGetMatVouch.HandleFormField(oMasterMM, fMat.cmb_CustomerID, Nothing, myRow.Row, "V", "CustomerID", True)
        ObjGetMatVouch.HandleFormField(oMasterMM, fMat.cmb_VendorID, Nothing, myRow.Row, "V", "VendorID", True)

        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_CustomerID, Nothing, myRow.Row, "I", "CustomerID", True)
        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_VendorID, Nothing, myRow.Row, "I", "VendorID", True)
        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_campusid, Nothing, myRow.Row, "I", "CampusID", True)

        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_QtyTypeSrc, Nothing, myRow.Row, "I", "QtyTypeSrc", True)
        ObjGetMatVouch.HandleFormField(oMasterMM, cmb_QtyTypeDes, Nothing, myRow.Row, "I", "QtyTypeDes", True)

    End Sub


    Private Sub cmb_SpStock_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_SpStock.Leave, cmb_SpStock.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) AndAlso Not myUtils.NullNot(cmb_SpStock.Value) AndAlso Not IsNothing(cmb_SpStock.SelectedRow) Then
            SpStockLeaveEvent()
        End If
    End Sub

    Private Function ControlStatus() As Boolean
        cmb_VendorID.ReadOnly = True
        cmb_campusid.ReadOnly = True
        cmb_CustomerID.ReadOnly = True
        cmb_MatMvtReasonID.ReadOnly = True

        cmb_QtyTypeSrc.ReadOnly = True
        cmb_QtyTypeDes.ReadOnly = True

        Return True
    End Function

    Private Sub cmbMvtCode_Leave(sender As Object, e As EventArgs) Handles cmbMvtCode.Leave, cmbMvtCode.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) Then
            MvtCodeLeaveEvent()
        End If
    End Sub

    Private Sub cmb_StockStage_Leave(sender As Object, e As EventArgs) Handles cmb_StockStage.Leave, cmb_StockStage.AfterCloseUp
        fSoItemSelect.HandleItem()
    End Sub

    Private Sub fItemSelect_ItemChanged(sender As Object, ByVal NewItemID As Integer) Handles fItemSelect.ItemChanged
        fSoItemSelect.HandleItem()
        fItemSelect.CalculateQty()
        If (Not IsNothing(fItemSelect.cmb_ItemId.SelectedRow)) AndAlso myUtils.NullNot(myRow("ValuationClass")) Then myRow("ValuationClass") = myUtils.cStrTN(fItemSelect.cmb_ItemId.SelectedRow.Cells("ValuationClass").Value)
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "PricingItem" Then
            If Not myUtils.IsInList(myUtils.cStrTN(fMat.cmb_ChallanType.Value), "SCSP", "TRSO", "SCSTB", "RCWB", "TRJB") Then CtlPricingChild1.UpdatePricingTable(myRow.Row)
        ElseIf Me.FormPrepared AndAlso e.Tab.Key = "Quantity" Then
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
        ElseIf Me.FormPrepared AndAlso e.Tab.Key = "Location" Then
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
        End If
    End Sub

    Private Sub SetRTValue()
        myRow("RT") = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGST", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("SGST", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("CGST", "PerValue")), 2)
    End Sub

    Private Sub GenerateStockBalance()
        fMat.cm.EndCurrentEdit()
        Me.cm.EndCurrentEdit()
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CampusID", myUtils.cValTN(fMat.myRow("CampusId")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ItemID", myUtils.cValTN(myRow("ItemID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Date", Format(fMat.myRow("ChallanDate"), "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@PIDUnitID", myUtils.cValTN(myRow("PIDUnitID")), GetType(Integer), False))

        Dim oModel As clsViewModel = fMat.GenerateParamsModel("stockbaldep", Params)
        myViewStBalDep.GenView(oModel, EnumViewCallType.acNormal)

        Dim oModel2 As clsViewModel = fMat.GenerateParamsModel("stockbalcamp", Params)
        myViewStBalCamp.GenView(oModel2, EnumViewCallType.acNormal)






        If (myUtils.cValTN(fMat.myRow("VendorID")) > 0) OrElse (myUtils.cValTN(myRow("VendorID")) > 0) Then
            Dim VendorIDCSV As String = myUtils.cValTN(fMat.myRow("VendorID")) & "," & myUtils.cValTN(myRow("VendorID"))
            Params.Add(New clsSQLParam("@VendorIDCSV", VendorIDCSV, GetType(Integer), True))
        End If

        If (myUtils.cValTN(fMat.myRow("CustomerID")) > 0) OrElse (myUtils.cValTN(myRow("CustomerID")) > 0) Then
            Dim CustomerIDCSV As String = myUtils.cValTN(fMat.myRow("CustomerID")) & "," & myUtils.cValTN(myRow("CustomerID"))
            Params.Add(New clsSQLParam("@CustomerIDCSV", CustomerIDCSV, GetType(Integer), True))
        End If

        Dim oModel1 As clsViewModel = fMat.GenerateParamsModel("receipt", Params)
        myViewReceipt.GenView(oModel1, EnumViewCallType.acNormal)

        StockItemID = myUtils.cValTN(myRow("ItemID"))
    End Sub
End Class