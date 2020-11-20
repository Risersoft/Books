Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports Infragistics.Win.UltraWinGrid

Public Class frmMatVouchItem
    Inherits frmMax
    Friend fMat As frmMatVouch, fItemBOM As frmMatVouchItemBOM, fItemRes As frmMatVouchRes, fItemSelect As frmItemSelect
    Friend myViewOrderData, myViewOB, myViewStBalDep, myViewStBalCamp, myViewCovFac As New clsWinView, oMasterMM As clsMasterDataMM
    Dim dv, dvQtyDes, dvQtySrc, dvSpSt, dvClassType, dvTransType, dvStStage, dvStStage2, dvTTyDes, dvReason, dvRetFY, dvClass, dvMatDep, dvVendor, dvCustomer, dvCamp, dvHSN As DataView
    Dim StockItemID, CovFacItemID As Integer, ItemSourceODNote As Boolean = False
    Dim WithEvents MvtCodeSystem As New clsCodeSystem
    Friend WithEvents fCostAssign As risersoft.app.accounts.frmCostAssign, fSoItemSelect As frmSOItemSelect

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridBOM)
        myViewOrderData.SetGrid(UltraGridOrderData)
        myViewOB.SetGrid(UltraGridOB)
        myViewStBalDep.SetGrid(UltraGridStBalDep)
        myViewStBalCamp.SetGrid(UltraGridStBalCamp)
        myViewCovFac.SetGrid(UltraGridCovFac)

        fItemBOM = New frmMatVouchItemBOM
        fItemBOM.AddToPanel(Me.SplitContainer1.Panel2, True, System.Windows.Forms.DockStyle.Fill)
        fItemBOM.fMatItem = Me

        fItemRes = New frmMatVouchRes
        fItemRes.AddtoTab(Me.UltraTabControl1, "Reservation", True)

        fItemSelect = New frmItemSelect
        fItemSelect.AddtoTab(Me.UltraTabControl1, "Material", True)

        fCostAssign = New risersoft.app.accounts.frmCostAssign
        fCostAssign.AddtoTab(Me.UltraTabControl1, "Cost", True)

        fSoItemSelect = New frmSOItemSelect
        fSoItemSelect.AddtoTab(Me.UltraTabControl1, "SO", True)
        fSoItemSelect.ctlPricingChild = Me.CtlPricingChild1

        oMasterMM = New clsMasterDataMM(Me.Controller)

        ControlStatus()
        ControlStatusInItForm()

        AddHandler CtlPricingChild1.CellUpdated, AddressOf CellUpdated
    End Sub

    Private Sub CellUpdated(sender As Object, rChildElem As DataRow)
        If (Not IsNothing(myRow)) Then
            If (myUtils.cValTN(myRow("ODNoteID")) = 0 AndAlso (Not myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("PricingType").Value), "BR"))) Then CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
        End If
    End Sub

    Private Sub ControlStatusInItForm()
        UltraTabControl1.Tabs("Reservation").Visible = False
        UltraTabControl1.Tabs("Pricing").Visible = False
        UltraTabControl1.Tabs("OrderData").Visible = False
        UltraTabControl1.Tabs("BOM").Visible = False
        UltraTabControl1.Tabs("OpBalance").Visible = False

        fItemBOM.Enabled = False
        fItemBOM.cmb_QtyTypeDes.ReadOnly = True
        fItemBOM.cmb_QtyTypeSrc.ReadOnly = True
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dv = New DataView(NewModel.dsCombo.Tables("MatMvtCode"))
        MvtCodeSystem.SetConf(dv, Me.cmb_MvtCode, cmbMvtCode)

        dvReason = myWinSQL.AssignCmb(NewModel.dsCombo, "Reason", "", Me.cmb_MatMvtReasonID, , 2)
        dvMatDep = myWinSQL.AssignCmb(NewModel.dsCombo, "DepsMatItem", "", Me.cmb_matdepid, , 2)
        dvStStage = myWinSQL.AssignCmb(NewModel.dsCombo, "Stockstage", "", Me.cmb_StockStage, , 2)
        dvTransType = myWinSQL.AssignCmb(NewModel.dsCombo, "TransType", "", Me.cmb_TransType, , 2)
        dvCamp = myWinSQL.AssignCmb(NewModel.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "FixedAsset", "", Me.cmb_FixedAssetID)
        dvVendor = myWinSQL.AssignCmb(NewModel.dsCombo, "Vendor", "", Me.cmb_VendorID,, 2)
        dvCustomer = myWinSQL.AssignCmb(NewModel.dsCombo, "Customer", "", Me.cmb_CustomerID,, 2)
        dvClass = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass, , 2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "ForDepsMat", "", Me.cmb_ForMatDepID)

        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitIDEntry)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_OrderRateUnitID)

        dvClassType = myWinSQL.AssignCmb(NewModel.dsCombo, "ClassType", "", Me.cmb_ClassType,, 2)
        dvRetFY = myWinSQL.AssignCmb(NewModel.dsCombo, "ReturnFY", "", Me.cmb_ReturnFY, , 2)
        dvQtyDes = myWinSQL.AssignCmb(NewModel.dsCombo, "QtyTypeDes", "", Me.cmb_QtyTypeDes, , 2)
        dvQtySrc = myWinSQL.AssignCmb(NewModel.dsCombo, "QtyTypeSrc", "", Me.cmb_QtyTypeSrc, , 2)
        dvStStage2 = myWinSQL.AssignCmb(NewModel.dsCombo, "StockStage2", "", Me.cmb_StockStage2, , 2)
        dvTTyDes = myWinSQL.AssignCmb(NewModel.dsCombo, "TaxType2", "", Me.cmb_TaxType2, , 2)
        dvSpSt = myWinSQL.AssignCmb(NewModel.dsCombo, "SpStock", "", Me.cmb_SpStock, , 2)
        dvSpSt.Sort = "Tag"

        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxCredit", "", Me.cmb_TaxCredit)
        dvHSN = myWinSQL.AssignCmb(NewModel.dsCombo, "HSNSac", "", Me.cmb_Hsn_Sc,, 2)

        myViewOrderData.PrepEdit(NewModel.GridViews("Order"))
        myViewOB.PrepEdit(NewModel.GridViews("Opening"), , Me.btnDelOB)
        myView.PrepEdit(NewModel.GridViews("BOM"))

        fItemRes.myViewIssue.PrepEdit(NewModel.GridViews("ResIssueItem"), , fItemRes.btnDelIssue)
        fItemRes.myView.PrepEdit(NewModel.GridViews("ResRecItem"), , fItemRes.btnDelRec)

        fItemBOM.BindModel(NewModel)
        fItemRes.BindModel(NewModel)
        fItemSelect.BindModel(NewModel)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Dim strVouchTypeCode As String = ""
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.FormPrepared = True
            myWinSQL.ClearBindings(fItemBOM)
            fItemSelect.HandleItem()
            fCostAssign.HandleItem("MatVouchItemID", "VouchDate", myUtils.cValTN(fMat.cmb_matdepid.SelectedRow.Cells("CampusID").Value), myRow.Row)

            If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 AndAlso (Not IsNothing(cmb_MvtCode.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("DocRefType").Value), "SO") Then fSoItemSelect.HandleItem()
            If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 Then
                If Not IsNothing(fSoItemSelect.myView.mainGrid.myDv) Then fSoItemSelect.myView.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))
            End If

            If Not fMat.cmb_MatVouchTypeID.SelectedRow Is Nothing Then strVouchTypeCode = myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("VouchTypeCode").Value)

            If Not myUtils.IsInList(myUtils.cStrTN(strVouchTypeCode), "") Then
                If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "Oth") Then
                    dv.RowFilter = "MvtType = '" & myUtils.cStrTN(strVouchTypeCode) & "' and (DocType is Null or AllowOther = 1) and " & fMat.ObjGetMatVouch.MvtCodeFilter(fMat.myRow("TaxType"), dv)
                Else
                    dv.RowFilter = "MvtType = '" & myUtils.cStrTN(strVouchTypeCode) & "' and DocType = '" & myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value) & "' and " & fMat.ObjGetMatVouch.MvtCodeFilter(fMat.myRow("TaxType"), dv)
                End If
            End If

            myView.mainGrid.myDv.RowFilter = "pMatVouchItemID = " & myUtils.cValTN(myRow("matvouchitemid"))
            ClearFItemBOM()
            If Not IsNothing(fCostAssign.myView.mainGrid.myDv) Then fCostAssign.myView.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))
            If Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))
            If Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))

            Dim rr1() As DataRow = fMat.dsForm.Tables("VouchItem").Select("OdNoteItemID is Not Null")
            Me.Enabled = True
            If rr1.Length > 0 Then
                ItemSourceODNote = True
                ctlPrepForm()
                WinFormUtils.SetReadOnly(Me, True)

                WinFormUtils.SetReadOnly(UltraTabControl1.Tabs("Reservation").TabPage, True, True)
                WinFormUtils.SetReadOnly(UltraTabControl1.Tabs("OrderData").TabPage, True, True)
            Else
                ItemSourceODNote = False
                WinFormUtils.SetReadOnly(Me, True, True)
                ctlPrepForm()
            End If

            myViewOrderData.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))
            fItemRes.myViewIssue.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))
            fItemRes.myView.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))
            myViewOB.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))

            dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, myUtils.cDateTN(fMat.myRow("VouchDate"), DateTime.MinValue), "WODate", "CompletedOn", "CampusID")

            CtlPricingChild1.HandleChildRowSelect()
            HandleOrderDataItem()

            risersoft.app.mxform.myFuncs.TransTypeFilter(dvClass, myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(CovFacItemID)) Then GenerateCoveringFac()

            CalculateTotalQtyBOM()
        End If
        Return Me.FormPrepared
    End Function

    Public Sub PrepMatVouchItem(rMVItem As DataRow)
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, Nothing, Nothing, rMVItem, "I", "CustomerID", True) = "N" Then rMVItem("CustomerID") = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, Nothing, Nothing, rMVItem, "I", "VendorID", True) = "N" Then rMVItem("VendorID") = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, Nothing, Nothing, rMVItem, "I", "CampusID", True) = "N" Then rMVItem("CampusID") = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, Nothing, Nothing, rMVItem, "I", "MatDepID", True) = "N" Then rMVItem("MatDepID") = DBNull.Value

        Dim rMvtCode As DataRow = oMasterMM.GetMvtCodeDataRow(myUtils.cValTN(rMVItem("MvtCode")))
        Dim rItemID() As DataRow = fMat.dsCombo.Tables("Items").Select("ItemID = " & myUtils.cValTN(rMVItem("ItemID")) & "")
        fItemSelect.CalculateQtyRow(rMVItem, rItemID(0), rMvtCode)
    End Sub

    Private Function HandleOrderDataItem() As Boolean
        myViewOrderData.mainGrid.myGrid.UpdateData()
        If myViewOrderData.mainGrid.myDv.Count > 0 Then
            fItemSelect.MakeReadOnly(True)
            cmb_matdepid.ReadOnly = True

            If myViewOrderData.mainGrid.myDv.Table.Select("MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID")) & " and PIDUnitID is Not Null").Length > 0 Then
                fItemSelect.btnSelect.Enabled = False
            Else
                fItemSelect.btnSelect.Enabled = True
            End If

        Else
            If ItemSourceODNote = False Then fItemSelect.MakeReadOnly(False)
            If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "MatDepID") = "R" Then cmb_matdepid.ReadOnly = False
        End If
        Return True
    End Function

    Private Sub ctlPrepForm()
        fItemSelect.cmb_BaseUnitID.ReadOnly = True
        ControlStatus()
        SetFieldMvtCode()
        SetFieldSpStock()
        ClassTypeLeaveEvent()

        If Not myUtils.NullNot(cmb_MvtCode.Value) Then
            MvtCodeLeaveEvent()
            cmbMvtCode.Value = cmb_MvtCode.Value
        End If
    End Sub

    Private Sub cmb_MvtCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_MvtCode.Leave, cmb_MvtCode.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) Then
            If ItemSourceODNote = False Then MvtCodeLeaveEvent()
        End If
    End Sub

    Public Overrides Function VSave() As Boolean
        Dim ValidateSoItem As Boolean = True
        Dim rCompany As DataRow, TotalQty As Decimal
        Me.InitError()
        VSave = False
        EndCurrentEdit()

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_MvtCode, "Please Generate Transaction")
            Exit Function
        End If

        If cmb_TransType.SelectedRow Is Nothing Then cmb_TransType.Value = DBNull.Value

        If myUtils.NullNot(fItemSelect.cmb_ItemId.Value) Then WinFormUtils.AddError(fItemSelect.cmb_ItemId, "Please Select Item Code", Me.eBag)
        If myUtils.NullNot(cmb_MvtCode.Value) Then WinFormUtils.AddError(cmb_MvtCode, "Please Select Mvt. Code")
        If myUtils.NullNot(cmb_SpStock.Value) Then WinFormUtils.AddError(cmb_SpStock, "Please Select Sp. Stock")
        If Not myUtils.NullNot(cmb_ItemUnitID2.Value) AndAlso myUtils.cValTN(txt_QtySKU1.Value) > 0 AndAlso myUtils.cValTN(txt_QtySKU2.Value) = 0 Then WinFormUtils.AddError(txt_QtySKU2, "Please define Convert factor for Item")


        Dim CompanyId As Integer = Me.Controller.CommonData.GetCompanyIDFromDepMat(fMat.cmb_matdepid.Value)
        rCompany = Me.Controller.CommonData.rCompany(CompanyId)

        If rCompany("FinStartDate") <= fMat.dt_VouchDate.Value AndAlso myUtils.cValTN(txt_QtyEntry.Value) <= 0 Then WinFormUtils.AddError(txt_QtyEntry, "Please Enter Quantity")
        If myUtils.cValTN(txt_QtyEntry.Value) > 0 AndAlso myUtils.cValTN(txt_QtySKU1.Value) = 0 Then WinFormUtils.AddError(txt_QtySKU1, "Please Enter Qty SKU1")

        If (Not IsNothing(Me.cmb_MatMvtReasonID.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(Me.cmb_MatMvtReasonID.SelectedRow.Cells("ReasonCode").Value), "M", "P", "C") AndAlso myUtils.NullNot(cmb_ForMatDepID.Value) Then WinFormUtils.AddError(cmb_ForMatDepID, "Select Issuing For Department.")

        If Me.CanSave AndAlso Me.ValidateData Then
            If myUtils.IsInList(myUtils.cStrTN(Me.cmb_ClassType.Value), "A", "S") Then
                If myUtils.NullNot(cmb_ValuationClass.Value) Then WinFormUtils.AddError(cmb_ValuationClass, "Select Valuation Class")
            Else
                cmb_ValuationClass.Value = myUtils.cStrTN(fItemSelect.cmb_ItemId.SelectedRow.Cells("ValuationClass").Value)
            End If

            If Not IsNothing(cmb_MvtCode.SelectedRow) Then
                If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "PID") Then
                    If myUtils.cValTN(fItemSelect.txt_pidunitid.Text) = 0 Then WinFormUtils.AddError(fItemSelect.lbl_WOInfo, "Select Work Order", Me.eBag)

                ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "OB") Then
                    If myUtils.cValTN(fMat.cmb_matdepid.Value) > 0 Then
                        If (Month(rCompany("FinStartDate")) > Month(fMat.dt_VouchDate.Value)) AndAlso (Year(rCompany("FinStartDate")) > Year(fMat.dt_VouchDate.Value)) Then WinFormUtils.AddError(fMat.dt_VouchDate, "Select a Correct Voucher Date for Op. Balance", Me.eBag)
                    End If

                    TotalQty = myUtils.cValTN(fMat.dsForm.Tables("OB").Compute("sum(Qty)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))))
                    If myUtils.cValTN(myRow("QtyEntry")) <> myUtils.cValTN(TotalQty) Then WinFormUtils.AddError(txt_QtyEntry, "Enter Quantity According to Op. Balance")
                End If
            End If

            If myUtils.IsInList(myUtils.cStrTN(fItemRes.cmb_ReserveGRBehave.Value), "M") OrElse myUtils.IsInList(myUtils.cStrTN(fItemRes.cmb_ReserveGIBehave.Value), "M") Then
                If Not myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "AS") Then
                    TotalQty = myUtils.cValTN(fMat.dsForm.Tables("Res").Compute("sum(Qty)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))))
                    If myUtils.cValTN(myRow("QtySKU1")) <> myUtils.cValTN(TotalQty) Then WinFormUtils.AddError(txt_QtySKU1, "Enter Quantity According to Reservation")
                End If
            End If


            If Not IsNothing(cmb_MvtCode.SelectedRow) Then
                TotalQty = 0
                If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "U") Then
                    TotalQty = myUtils.cValTN(fMat.dsForm.Tables("Pur").Compute("sum(QtyRecd)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))))
                    If myUtils.cValTN(myRow("QtyEntry")) <> myUtils.cValTN(TotalQty) Then WinFormUtils.AddError(txt_QtyEntry, "Enter Quantity According to Order History")
                ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "Q") Then
                    If myUtils.IsInList(myUtils.cStrTN(cmb_QtyTypeDes.Value), "R") Then
                        TotalQty = myUtils.cValTN(fMat.dsForm.Tables("Pur").Compute("sum(QtyRej)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))))
                    ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_QtyTypeDes.Value), "UR") Then
                        TotalQty = myUtils.cValTN(fMat.dsForm.Tables("Pur").Compute("sum(QtyOK)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID")))) + myUtils.cValTN(fMat.dsForm.Tables("Pur").Compute("sum(QtyDevi)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))))
                    End If
                    If myUtils.cValTN(myRow("QtyEntry")) <> myUtils.cValTN(TotalQty) Then WinFormUtils.AddError(txt_QtyEntry, "Enter Quantity According to Order History")
                ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "I") Then
                    TotalQty = myUtils.cValTN(fMat.dsForm.Tables("Pur").Compute("sum(QtyIssue)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))))
                    If myUtils.cValTN(myRow("QtyEntry")) <> myUtils.cValTN(TotalQty) Then
                        If MsgBox("Do you want to save withount PO Reference ?", MsgBoxStyle.YesNo, myWinApp.Vars("appname")) = MsgBoxResult.No Then
                            WinFormUtils.AddError(txt_QtyEntry, "Enter Quantity According to Order History")
                        End If
                    End If
                End If

                If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("DocRefType").Value), "SO") Then
                    ValidateSoItem = fSoItemSelect.ValidateData(cmb_MvtCode)
                End If
            End If

            Dim rr1() As DataRow = myViewOB.mainGrid.myDv.Table.Select("MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID")) & "  and Dated is Null")
            If rr1.Length > 0 Then
                WinFormUtils.AddError(btnAddOB, "Please Select OP Balance Date.")
            End If

            If Me.CanSave AndAlso fItemBOM.VSave AndAlso fItemSelect.ValidateData AndAlso ValidateSoItem Then
                cm.EndCurrentEdit()
                fItemSelect.CalculateQty()
                CtlPricingChild1.UpdatePricingTable(myRow.Row)
                If (myUtils.cValTN(myRow("ODNoteID")) = 0 AndAlso (Not IsNothing(cmb_MvtCode.SelectedRow)) AndAlso Not myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("PricingType").Value), "BR")) Then CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
                VSave = True
            End If
        End If
    End Function

    Public Overrides Function ValidateData() As Boolean
        Me.InitError()
        If fMat.ObjGetMatVouch.CalculateFieldType("V", True, myRow.Row, oMasterMM, "CustomerID") = "R" AndAlso myUtils.NullNot(fMat.cmb_CustomerID.Value) Then WinFormUtils.AddError(fMat.cmb_CustomerID, "Please Select Voucher Customer Name", Me.eBag)
        If fMat.ObjGetMatVouch.CalculateFieldType("V", True, myRow.Row, oMasterMM, "VendorID") = "R" AndAlso myUtils.NullNot(fMat.cmb_VendorID.Value) Then WinFormUtils.AddError(fMat.cmb_VendorID, "Please Select Vendor Name", Me.eBag)
        If fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "TaxTypeDes") = "R" AndAlso myUtils.NullNot(cmb_TaxType2.Value) Then WinFormUtils.AddError(cmb_TaxType2, "Please Select Tax Type")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "ClassType") = "R" AndAlso myUtils.NullNot(cmb_ClassType.Value) Then WinFormUtils.AddError(cmb_ClassType, "Please Select Class Type")
        If myUtils.IsInList(myUtils.cStrTN(myRow("classtype")), "s", "m", "") AndAlso myUtils.NullNot(cmb_StockStage.Value) Then WinFormUtils.AddError(cmb_StockStage, "Please Select Stock Stage")
        If myUtils.IsInList(myUtils.cStrTN(myRow("classtype")), "a") AndAlso myUtils.NullNot(cmb_TransType.Value) Then WinFormUtils.AddError(cmb_TransType, "Please Select Trans Type")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "StockStage2") = "R" AndAlso myUtils.NullNot(cmb_StockStage2.Value) Then WinFormUtils.AddError(cmb_StockStage2, "Please Select Stock Stage2")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "FixedAssetID") = "R" AndAlso myUtils.NullNot(cmb_FixedAssetID.Value) Then WinFormUtils.AddError(cmb_FixedAssetID, "Please Select Fixed Asset")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "MatMvtReasonID") = "R" AndAlso myUtils.NullNot(cmb_MatMvtReasonID.Value) Then WinFormUtils.AddError(cmb_MatMvtReasonID, "Please Select MatMvt Reason")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "ReturnFY") = "R" AndAlso myUtils.NullNot(cmb_ReturnFY.Value) Then WinFormUtils.AddError(cmb_ReturnFY, "Please Select ReturnFY")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "MfgCharges") = "R" AndAlso myUtils.cValTN(txt_mfgCharges.Value) = 0 Then WinFormUtils.AddError(txt_mfgCharges, "Please Enter Mfg Charges")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "CustomerID") = "R" AndAlso myUtils.NullNot(cmb_CustomerID.Value) Then WinFormUtils.AddError(cmb_CustomerID, "Please Select Customer Name")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "VendorID") = "R" AndAlso myUtils.NullNot(cmb_VendorID.Value) Then WinFormUtils.AddError(cmb_VendorID, "Please Select Vendor Name")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "CampusID") = "R" AndAlso myUtils.NullNot(cmb_campusid.Value) Then WinFormUtils.AddError(cmb_campusid, "Please Select Campus Name")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "MatDepID") = "R" AndAlso myUtils.NullNot(cmb_matdepid.Value) Then WinFormUtils.AddError(cmb_matdepid, "Please Select Department")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "QtyTypeSrc") = "R" AndAlso myUtils.NullNot(cmb_QtyTypeSrc.Value) Then WinFormUtils.AddError(cmb_QtyTypeSrc, "Please Select Qty Type Src.")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "QtyTypeDes") = "R" AndAlso myUtils.NullNot(cmb_QtyTypeDes.Value) Then WinFormUtils.AddError(cmb_QtyTypeDes, "Please Select Qty Type Des.")
        If myUtils.IsInList(myUtils.cStrTN(fMat.ObjGetMatVouch.CalculateFieldType("V", False, myRow.Row, oMasterMM, "Pricing")), "R") AndAlso fMat.myRow("VouchDate") > risersoft.app.mxform.myFuncs.GSTLounchDate AndAlso myUtils.NullNot(Me.cmb_TaxCredit.Value) Then WinFormUtils.AddError(Me.cmb_TaxCredit, "Select Tax Credit")
        If myUtils.IsInList(myUtils.cStrTN(fMat.ObjGetMatVouch.CalculateFieldType("V", False, myRow.Row, oMasterMM, "Pricing")), "R") AndAlso fMat.myRow("VouchDate") > risersoft.app.mxform.myFuncs.GSTLounchDate AndAlso (Me.cmb_Hsn_Sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(Me.cmb_Hsn_Sc, "Select HSN Code")
        Return Me.CanSave
    End Function

    Private Sub txt_QtyEntry_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_QtyEntry.Leave
        fItemSelect.CalculateQty()
    End Sub

    Private Sub btnAddOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddOrder.Click
        Dim rr() As DataRow, Params As New List(Of clsSQLParam), Key As String = ""
        If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "MO") Then
            Params.Add(New clsSQLParam("@matdepid", myUtils.cValTN(cmb_matdepid.Value), GetType(Integer), False))
        Else
            If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "U") Then
                Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(fMat.cmb_VendorID.Value), GetType(Integer), False))
            ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "Q", "I") Then
                Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
            End If
        End If
        Params.Add(New clsSQLParam("@InvoiceCampusID", myUtils.cValTN(fMat.cmb_InvoiceCampusID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(fMat.cmb_DivisionID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@itemid", myUtils.cValTN(myRow("ItemID")), GetType(Integer), False))
        If myUtils.cValTN(fItemSelect.cmb_varnum.Value) > 0 Then Params.Add(New clsSQLParam("@pidunitid", myUtils.cValTN(myRow("PIDUnitID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@varnum", myUtils.cValTN(myRow("VarNum")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@classtype", "'" & myUtils.cStrTN(myRow("classtype")) & "'", GetType(String), False))
        Params.Add(New clsSQLParam("@stockstage", "'" & myUtils.cStrTN(myRow("stockstage")) & "'", GetType(String), False))
        Params.Add(New clsSQLParam("@transtype", "'" & myUtils.cStrTN(myRow("TransType")) & "'", GetType(String), False))

        If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "U") Then
            Params.Add(New clsSQLParam("@ordertype", "'" & myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value) & "'", GetType(String), False))
            Key = "OrderDataU"
        ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "Q", "I") Then
            Key = "OrderDataQ"
        End If

        rr = fMat.AdvancedSelect(Key, Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            Dim r2 As DataRow = myUtils.CopyOneRow(rr(0), myViewOrderData.mainGrid.myDv.Table, , , "MatVouchItemID", myUtils.cValTN(myRow("MatVouchItemID")))
            r2("MatVouchItemID") = myUtils.cValTN(myRow("MatVouchItemID"))
            If myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "Q", "I") Then r2("QtyRecd") = DBNull.Value
            If rr(0).Table.Columns.Contains("MatVouchID") Then r2("RecvMatVouchID") = myUtils.cValTN(rr(0)("MatVouchID"))
            HandleOrderDataItem()
            MvtCodeLeaveEvent()
        End If
    End Sub

    Private Sub UltraGridBOM_AfterRowActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles UltraGridBOM.AfterRowActivate
        If Me.FormPrepared Then
            Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
            If myUtils.cValTN(myRow("MatVouchItemId")) > 0 Then
                r1("PMatVouchItemId") = myUtils.cValTN(myRow("MatVouchItemId"))
                r1("MatVouchId") = myUtils.cValTN(myRow("MatVouchId"))
            End If
            myView.mainGrid.myGrid.UpdateData()
            If fItemBOM.PrepForm(r1) Then
                fItemBOM.fItemRes.myViewIssue.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myView.mainGrid.myGrid.ActiveRow.Cells("MatVouchItemID").Value
                fItemBOM.fItemRes.myView.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myView.mainGrid.myGrid.ActiveRow.Cells("MatVouchItemID").Value
                fItemBOM.Enabled = True
            End If
        End If
    End Sub

    Public Sub CalculateTotalQtyBOM()
        If myUtils.cValTN(myRow("MatVouchItemId")) > 0 Then
            lblTotQty.Text = "Total Qty - " & myUtils.cValTN(myView.mainGrid.Model.GetColSum("QtyEntry", "PMatVouchItemId = " & myUtils.cValTN(myRow("MatVouchItemId")) & ""))
        End If
    End Sub

    Private Sub UltraGridBOM_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridBOM.BeforeRowDeactivate
        If fItemBOM.VSave Then
            CalculateTotalQtyBOM()
        Else
            e.Cancel = True
        End If
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

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItemBOM.VSave Then
            cm.EndCurrentEdit()
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
        End If
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        ClearFItemBOM()
    End Sub

    Private Sub ClearFItemBOM()
        If myView.mainGrid.myDv.Count = 0 Then
            fItemBOM.myRow = Nothing
            myWinSQL.ClearBindings(fItemBOM)
            myWinSQL.ClearValue(fItemBOM)
            fItemBOM.Enabled = False
        End If
    End Sub

    Public Function ControlStatus() As Boolean
        If cmb_MvtCode.SelectedRow Is Nothing Then cmbMvtCode.Value = DBNull.Value
        cmb_ValuationClass.ReadOnly = True
        fItemSelect.TableLayoutPanel2.Visible = False

        txt_QtySKU1.ReadOnly = True
        txt_QtySKU2.ReadOnly = True
        txt_QtyRate.ReadOnly = True

        cmb_ItemUnitIDEntry.ReadOnly = True
        cmb_ItemUnitID.ReadOnly = True
        cmb_ItemUnitID2.ReadOnly = True
        cmb_OrderRateUnitID.ReadOnly = True

        fSoItemSelect.cmb_SoSpareID.ReadOnly = True
        fSoItemSelect.cmb_PIDInfoSp.ReadOnly = True
        Return True
    End Function

    Private Sub cmb_SpStock_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_SpStock.Leave, cmb_SpStock.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) AndAlso Not myUtils.NullNot(cmb_SpStock.Value) AndAlso Not IsNothing(cmb_SpStock.SelectedRow) Then
            If ItemSourceODNote = False Then SpStockLeaveEvent()
        End If
    End Sub

    Private Sub cmbMvtCode_Leave(sender As Object, e As EventArgs) Handles cmbMvtCode.Leave, cmbMvtCode.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) Then
            If ItemSourceODNote = False Then MvtCodeLeaveEvent()
        End If
    End Sub

    Private Sub MvtCodeLeaveEvent()
        cmb_SpStock.Value = oMasterMM.MvtCodeField("SpStockType", cmb_MvtCode.Value, "", dvSpSt, "", cmb_SpStock.Value)
        cmb_StockStage.Value = oMasterMM.MvtCodeField("StockStage", cmb_MvtCode.Value, "", dvStStage, ",", cmb_StockStage.Value)
        cmb_StockStage2.Value = oMasterMM.MvtCodeField("StockStage2", cmb_MvtCode.Value, "", dvStStage2, ",", cmb_StockStage2.Value)
        cmb_TaxType2.Value = oMasterMM.MvtCodeField("TaxTypeDes", cmb_MvtCode.Value, "", dvTTyDes, ",", cmb_TaxType2.Value)

        If Not myUtils.NullNot(cmb_SpStock.Value) Then
            SpStockLeaveEvent()
        End If

        SetFieldMvtCode()

        If (Not IsNothing(cmb_MvtCode.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "PID2") Then
            fItemSelect.TableLayoutPanel2.Visible = True
        End If

        If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 AndAlso (Not IsNothing(cmb_MvtCode.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("DocRefType").Value), "SO") Then fSoItemSelect.HandleItem()
        fItemSelect.CalculateQty()
    End Sub

    Private Sub SetFieldMvtCode()
        Dim TabBool As Boolean

        fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_TaxType2, lblTaxDes, myRow.Row, "I", "TaxTypeDes", False, True)
        fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_StockStage2, lblStockStage2, myRow.Row, "I", "StockStage2", False, True)
        fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_ReturnFY, lblReturnFY, myRow.Row, "I", "ReturnFY", False, True)

        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_FixedAssetID, Nothing, myRow.Row, "I", "FixedAssetID", False) = "N" Then cmb_FixedAssetID.Value = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_MatMvtReasonID, Nothing, myRow.Row, "I", "MatMvtReasonID", False) = "N" Then
            cmb_MatMvtReasonID.Value = DBNull.Value
            cmb_ForMatDepID.Value = DBNull.Value
            cmb_ForMatDepID.ReadOnly = True
        Else
            cmb_ForMatDepID.ReadOnly = False
        End If

        fMat.ObjGetMatVouch.HandleFormField(oMasterMM, txt_BasicRate, lblBasicRate, myRow.Row, "I", "BasicRate", False, True)

        If cmb_MatMvtReasonID.ReadOnly = False Then dvReason.RowFilter = "MatMvtCode = " & myUtils.cValTN(cmb_MvtCode.Value)
        cmb_ReturnFY.Value = oMasterMM.CheckSelectedValue(dvRetFY, "", "", myUtils.cStrTN(cmb_ReturnFY.Value), "P")

        If Not IsNothing(cmb_MvtCode.SelectedRow) Then TabBool = myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "OB") Else TabBool = False
        fMat.ObjGetMatVouch.HandleFormTab(UltraTabControl1.Tabs("OpBalance"), TabBool)
        If UltraTabControl1.Tabs("OpBalance").Visible = True Then
            cmb_ItemUnitID.Value = cmb_ItemUnitIDEntry.Value
            fItemSelect.CalculateQty()
        End If

        If Not IsNothing(cmb_MvtCode.SelectedRow) Then TabBool = myUtils.IsInList(myUtils.cStrTN(fMat.ObjGetMatVouch.CalculateFieldType("I", "V", False, myRow.Row, oMasterMM, "Pricing")), "R") Else TabBool = False
        fMat.ObjGetMatVouch.HandleFormTab(UltraTabControl1.Tabs("Pricing"), TabBool)

        TabBool = myUtils.IsInList(myUtils.cStrTN(fMat.ObjGetMatVouch.CalculateFieldType("V", False, myRow.Row, oMasterMM, "Pricing")), "R")
        fMat.ObjGetMatVouch.HandleFormTab(fMat.UltraTabControl1.Tabs("Pricing"), TabBool)
        fMat.cmb_DivisionID.Visible = TabBool
        fMat.lblDivision.Visible = TabBool

        If fMat.myRow("VouchDate") < risersoft.app.mxform.myFuncs.GSTLounchDate Then
            HideCtl(False)
        Else
            HideCtl(TabBool)
            If TabBool = True AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_TaxCredit.Value), "") Then Me.cmb_TaxCredit.Value = "Y"
        End If

        If Not IsNothing(cmb_MvtCode.SelectedRow) Then TabBool = myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("DocRefType").Value), "SO") Else TabBool = False
        fMat.ObjGetMatVouch.HandleFormTab(UltraTabControl1.Tabs("SO"), TabBool)

        TabBool = myUtils.IsInList(myUtils.cStrTN(fMat.ObjGetMatVouch.CalculateFieldType("V", False, myRow.Row, oMasterMM, "ChallanNum")), "R")
        fMat.ObjGetMatVouch.HandleFormTab(fMat.UltraTabControl1.Tabs("Doc"), TabBool)
        If TabBool = False Then
            fMat.cmb_ChallanPending.Value = DBNull.Value
            fMat.cmb_TransporterID.Value = DBNull.Value
            fMat.txt_ChallanNum.Text = String.Empty
            fMat.txt_GRNum.Text = String.Empty
            fMat.dt_ChallanDate.Value = DBNull.Value
        End If

    End Sub

    Private Sub HideCtl(Visible As Boolean)
        Me.cmb_TaxCredit.Visible = Visible
        Me.lblTaxCredit.Visible = Visible
        lblHSN.Visible = Visible
        btnAddHSN.Visible = Visible
        cmb_Hsn_Sc.Visible = Visible
    End Sub

    Private Sub SpStockLeaveEvent()
        cmb_QtyTypeDes.Value = oMasterMM.MvtCodeField("QtyTypeDes", cmb_MvtCode.Value, cmb_SpStock.Value, dvQtyDes, ",", cmb_QtyTypeDes.Value)
        cmb_QtyTypeSrc.Value = oMasterMM.MvtCodeField("QtyTypeSrc", cmb_MvtCode.Value, cmb_SpStock.Value, dvQtySrc, ",", cmb_QtyTypeSrc.Value)
        cmb_ClassType.Value = oMasterMM.MvtCodeField("ClassType", cmb_MvtCode.Value, cmb_SpStock.Value, dvClassType, ",", cmb_ClassType.Value)

        EndCurrentEdit()
        SetFieldSpStock()
        ClassTypeLeaveEvent()
        ReCallOrderGrid(myUtils.cValTN(cmb_MvtCode.Value), myUtils.cStrTN(cmb_QtyTypeDes.Value), myViewOrderData)

        If myViewOrderData.mainGrid.myDv.Table.Select.Length > 0 Then
            fMat.cmb_VendorID.ReadOnly = True
            fMat.cmb_MatVouchTypeID.ReadOnly = True
        End If
    End Sub

    Private Sub SetFieldSpStock()
        Dim TabBool As Boolean, str1 As String
        Dim r3 As DataRow = oMasterMM.GetMvtCodeDataRow(cmb_MvtCode.Value)
        Dim r4 As DataRow = oMasterMM.GetMvtCodeSPDataRow(cmb_MvtCode.Value, cmb_SpStock.Value)

        If Not IsNothing(r4) Then TabBool = Not myUtils.IsInList(myUtils.cStrTN(r4.Item("BOMMvtCode")), "") Else TabBool = False
        fMat.ObjGetMatVouch.HandleFormTab(UltraTabControl1.Tabs("BOM"), TabBool)

        If Not IsNothing(r3) Then TabBool = (myUtils.IsInList(myUtils.cStrTN(r3.Item("OrderUpdateCode")), "U", "Q", "I") AndAlso Not myUtils.IsInList(myUtils.cStrTN(r4("SpStockType")), "J", "K")) Else TabBool = False
        fMat.ObjGetMatVouch.HandleFormTab(UltraTabControl1.Tabs("OrderData"), TabBool)


        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, fMat.cmb_CustomerID, Nothing, myRow.Row, "V", "CustomerID", True) = "N" Then fMat.cmb_CustomerID.Value = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, fMat.cmb_VendorID, Nothing, myRow.Row, "V", "VendorID", True) = "N" Then fMat.cmb_VendorID.Value = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, txt_mfgCharges, lblMfgCharges, myRow.Row, "I", "MfgCharges", True, True) = "N" Then txt_mfgCharges.Value = DBNull.Value
        str1 = fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_ClassType, Nothing, myRow.Row, "I", "ClassType", True)
        If str1 = "N" Then
            Me.cmb_ClassType.Value = "M"
            Me.HandleClassType(r4)
        End If

        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_CustomerID, Nothing, myRow.Row, "I", "CustomerID", True) = "N" Then cmb_CustomerID.Value = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_VendorID, Nothing, myRow.Row, "I", "VendorID", True) = "N" Then cmb_VendorID.Value = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_campusid, Nothing, myRow.Row, "I", "CampusID", True) = "N" Then cmb_campusid.Value = DBNull.Value
        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_matdepid, Nothing, myRow.Row, "I", "MatDepID", True) = "N" Then cmb_matdepid.Value = DBNull.Value


        fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_QtyTypeSrc, Nothing, myRow.Row, "I", "QtyTypeSrc", True)
        fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_QtyTypeDes, Nothing, myRow.Row, "I", "QtyTypeDes", True)

        FilterDep(myUtils.cDateTN(fMat.myRow("VouchDate"), DateTime.MinValue), r4)
    End Sub

    Private Sub FilterDep(dated As Date, r4 As DataRow)
        Dim Str2 As String = "0=1"
        Dim str1 As String = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "MatDepID")
        If Not IsNothing(r4) AndAlso Not IsNothing(fMat.cmb_matdepid.SelectedRow) Then
            If myUtils.IsInList(myUtils.cStrTN(r4("SrcDesTypeItem")), "H") Then
                Str2 = "CampusID = " & myUtils.cValTN(fMat.cmb_matdepid.SelectedRow.Cells("CampusID").Value) & " and matdepid <> " & myUtils.cValTN(fMat.cmb_matdepid.Value) & " and IsShop=1"
            ElseIf myUtils.IsInList(myUtils.cStrTN(r4("SrcDesTypeItem")), "I") Then
                Str2 = "CampusID = " & myUtils.cValTN(fMat.cmb_matdepid.SelectedRow.Cells("CampusID").Value) & " and matdepid <> " & myUtils.cValTN(fMat.cmb_matdepid.Value) & " and IsStore=1"
            ElseIf myUtils.IsInList(myUtils.cStrTN(r4("SrcDesTypeItem")), "T") Then
                Str2 = "CampusID = " & myUtils.cValTN(fMat.cmb_matdepid.SelectedRow.Cells("CampusID").Value) & " and (matdepid = " & myUtils.cValTN(fMat.cmb_matdepid.Value) & " or IsShop=1)"
            End If
        End If
        dvMatDep.RowFilter = myUtils.CombineWhere(False, str1, Str2)
    End Sub

    Public Sub ReCallOrderGrid(ByVal matMvtCode As Integer, ByVal QtyTypeDes As String, ByVal myViewOrderData As clsWinView)
        If myUtils.cValTN(matMvtCode) > 0 Then
            Dim r3 As DataRow = oMasterMM.GetMvtCodeDataRow(myUtils.cValTN(matMvtCode))

            If myUtils.IsInList(myUtils.cStrTN(r3.Item("OrderUpdateCode")), "U") Then
                myViewOrderData.mainGrid.MainConf("HIDECOLS") = "QtyRej, QtyDevi, QtyOK,QtyIssue"
            ElseIf myUtils.IsInList(myUtils.cStrTN(r3.Item("OrderUpdateCode")), "Q") Then
                If myUtils.IsInList(myUtils.cStrTN(QtyTypeDes), "R") Then
                    myViewOrderData.mainGrid.MainConf("HIDECOLS") = "QtyRecd, QtyDevi, QtyOK, RecdTCinSt,QtyIssue"
                ElseIf myUtils.IsInList(myUtils.cStrTN(QtyTypeDes), "UR") Then
                    myViewOrderData.mainGrid.MainConf("HIDECOLS") = "QtyRecd, QtyRej, RecdTCinSt,QtyIssue"
                End If
            ElseIf myUtils.IsInList(myUtils.cStrTN(r3.Item("OrderUpdateCode")), "I") Then
                myViewOrderData.mainGrid.MainConf("HIDECOLS") = "QtyRej, QtyDevi, QtyOK,QtyRecd,RecdTCinSt"
            End If
        End If
        myViewOrderData.mainGrid.Genwidth(True)

        Dim str1 As String = "<BAND IDFIELD=""PurItemHistID"" TABLE=""PurItemHist"" INDEX=""0""><COL KEY=""PurItemHistID, RecvMatVouchID, PurItemID,RecdTCInST,MatVouchItemID,Dated,QtyRecd,QtyRej,QtyOK,QtyDevi,RecdTCInQly,QtyIssue""/><COL KEY=""RecdTCInSt"" CAPTION=""Stores TC"" VLIST=""True;Received|False;Not Received""/></BAND>"
        myViewOrderData.mainGrid.PrepEdit(str1)
    End Sub

    Private Sub cmb_ClassType_Leave(sender As Object, e As EventArgs) Handles cmb_ClassType.Leave, cmb_ClassType.AfterCloseUp
        If ItemSourceODNote = False Then ClassTypeLeaveEvent()
    End Sub

    Private Function ClassTypeLeaveEvent() As Boolean
        If Not IsNothing(cmb_MvtCode.SelectedRow) AndAlso Not IsNothing(cmb_SpStock.SelectedRow) Then
            If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("DocRefType").Value), "SO") Then fSoItemSelect.HandleItem()

            Dim r4 As DataRow = oMasterMM.GetMvtCodeSPDataRow(cmb_MvtCode.Value, cmb_SpStock.Value)

            fMat.ObjGetMatVouch.HandleFormTab(UltraTabControl1.Tabs("Reservation"), myUtils.IsInList(myUtils.cStrTN(cmb_ClassType.Value), "M", "") AndAlso myUtils.IsInList(myUtils.cStrTN(r4("ValueUpdateCode")), "U"))
            If Not IsNothing(cmb_MvtCode.SelectedRow) Then fItemRes.SetTabCantrol(cmb_MvtCode.SelectedRow.Cells("MvtType").Value, myRow.Row)
            HandleClassType(r4)
        Else
            fMat.ObjGetMatVouch.HandleFormTab(UltraTabControl1.Tabs("Reservation"), False)
        End If
        Return True
    End Function

    Private Function HandleClassType(rMvtSp As DataRow) As Boolean
        cm.EndCurrentEdit()
        cmb_ValuationClass.ReadOnly = True
        cmb_TransType.ReadOnly = False
        Me.cmb_StockStage.ReadOnly = False
        If myUtils.IsInList(myUtils.cStrTN(cmb_ClassType.Value), "S") Then
            Me.cmb_ValuationClass.Value = "LABOUR"
            dvTransType.RowFilter = "CodeClass = 'Service' "
            dvHSN.RowFilter = "Ty = 'S'"
        ElseIf myUtils.IsInList(myUtils.cStrTN(Me.cmb_ClassType.Value), "A") Then
            cmb_ValuationClass.ReadOnly = False
            Me.cmb_StockStage.ReadOnly = True
            cmb_StockStage.Value = DBNull.Value
            dvTransType.RowFilter = "CodeClass = 'Asset' "
            dvHSN.RowFilter = "Ty = 'G'"
        Else
            Me.cmb_ClassType.Value = "M"
            cmb_TransType.ReadOnly = True
            dvTransType.RowFilter = "CodeClass = 'Material' "
            dvHSN.RowFilter = "Ty = 'G'"
        End If

        Return True
    End Function

    Private Sub btnAddOB_Click(sender As Object, e As EventArgs) Handles btnAddOB.Click
        myViewOB.mainGrid.ButtonAction("Add")
        myViewOB.mainGrid.myGrid.ActiveRow.Cells("MatVouchItemId").Value = myUtils.cValTN(myRow("MatVouchItemId"))
    End Sub

    Private Sub UltraGridOB_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridOB.AfterCellUpdate
        If myUtils.IsInList(e.Cell.Column.Key, "qty") Then
            Dim rItemID() As DataRow = fMat.dsCombo.Tables("Items").Select("ItemID = " & myUtils.cValTN(myRow("ItemID")) & "")
            If (Not IsNothing(rItemID)) AndAlso rItemID.Length > 0 Then
                Dim a As Decimal = fItemSelect.ConvertFactor(myUtils.cValTN(fItemSelect.FieldValue("ItemUnitId2", rItemID(0))), myUtils.cValTN(fItemSelect.FieldValue("ItemUnitID", rItemID(0))), myUtils.cValTN(myRow("ItemID")), myUtils.cValTN(myRow("PIDUnitID")), myUtils.cValTN(myRow("VarNum")))
                e.Cell.Row.Cells("Qty2").Value = myUtils.cValTN(e.Cell.Value) * myUtils.cValTN(a)
            End If
        End If
    End Sub

    Private Sub btnDelOD_Click(sender As Object, e As EventArgs) Handles btnDelOD.Click
        myViewOrderData.mainGrid.ButtonAction("Del")
        If myViewOrderData.mainGrid.myDv.Table.Select.Length = 0 Then MvtCodeLeaveEvent()
        HandleOrderDataItem()
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "Pricing" Then
            EndCurrentEdit()
            CtlPricingChild1.UpdatePricingTable(myRow.Row)
        ElseIf Me.FormPrepared AndAlso e.Tab.Key = "Quantity" Then
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
        ElseIf Me.FormPrepared AndAlso e.Tab.Key = "Location" Then
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
            Dim r4 As DataRow = oMasterMM.GetMvtCodeSPDataRow(cmb_MvtCode.Value, cmb_SpStock.Value)
            If Not IsNothing(r4) Then
                If myUtils.cStrTN(r4("MvtType")) = "GI" AndAlso myUtils.IsInList(myUtils.cStrTN(r4("SpStockType")), "J") Then
                    Dim str3 As String = myUtils.MakeCSV(myViewStBalDep.mainGrid.myDS.Tables(0).Select, "CustomerID")
                    dvCustomer.RowFilter = "CustomerID in (" & str3 & ")"
                ElseIf myUtils.cStrTN(r4("MvtType")) = "GI" AndAlso myUtils.IsInList(myUtils.cStrTN(r4("SpStockType")), "K") Then
                    Dim str3 As String = myUtils.MakeCSV(myViewStBalDep.mainGrid.myDS.Tables(0).Select, "VendorID")
                    dvVendor.RowFilter = "VendorID  in (" & str3 & ")"
                Else
                    dvVendor.RowFilter = ""
                    dvCustomer.RowFilter = ""
                End If
            End If
        ElseIf Me.FormPrepared AndAlso e.Tab.Key = "CovFac" Then
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(CovFacItemID)) Then GenerateCoveringFac()
        End If
    End Sub

    Private Sub GenerateStockBalance()
        EndCurrentEdit()
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CampusID", myUtils.cValTN(fMat.cmb_matdepid.SelectedRow.Cells("CampusID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@CampusID2", myUtils.cValTN(Me.myRow("CampusID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ItemID", myUtils.cValTN(myRow("ItemID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Date", Format(fMat.myRow("VouchDate"), "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@PIDUnitID", myUtils.cValTN(myRow("PIDUnitID")), GetType(Integer), False))
        Dim oModel As clsViewModel = fMat.GenerateParamsModel("stockbaldep", Params)
        myViewStBalDep.GenView(oModel, EnumViewCallType.acNormal)

        Dim oModel1 As clsViewModel = fMat.GenerateParamsModel("stockbalcamp", Params)
        myViewStBalCamp.GenView(oModel1, EnumViewCallType.acNormal)

        StockItemID = myUtils.cValTN(myRow("ItemID"))
    End Sub

    Private Sub cmb_QtyTypeDes_Leave(sender As Object, e As EventArgs) Handles cmb_QtyTypeDes.Leave, cmb_QtyTypeDes.AfterCloseUp
        ReCallOrderGrid(myUtils.cValTN(cmb_MvtCode.Value), myUtils.cStrTN(cmb_QtyTypeDes.Value), myViewOrderData)
    End Sub

    Private Sub EndCurrentEdit()
        If Not IsNothing(fMat.myRow) Then fMat.cm.EndCurrentEdit()
        If Not IsNothing(myRow) Then Me.cm.EndCurrentEdit()
        If Not IsNothing(fItemBOM.myRow) Then fItemBOM.cm.EndCurrentEdit()
    End Sub

    Private Sub cmb_TransType_Leave(sender As Object, e As EventArgs) Handles cmb_TransType.Leave, cmb_TransType.AfterCloseUp
        risersoft.app.mxform.myFuncs.TransTypeFilter(dvClass, myUtils.cStrTN(cmb_ClassType.Value), myUtils.cStrTN(cmb_TransType.Value))
    End Sub

    Private Sub GenerateCoveringFac()
        EndCurrentEdit()

        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@ItemID", myUtils.cValTN(myRow("ItemID")), GetType(Integer), False))
        Dim oModel As clsViewModel = fMat.GenerateParamsModel("covringfactor", Params)
        myViewCovFac.GenView(oModel, EnumViewCallType.acNormal)
        CovFacItemID = myUtils.cValTN(myRow("ItemID"))
    End Sub

    Private Sub cmb_StockStage_Leave(sender As Object, e As EventArgs) Handles cmb_StockStage.Leave, cmb_StockStage.AfterCloseUp
        If myUtils.cValTN(fMat.myRow("SalesOrderID")) > 0 AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("DocRefType").Value), "SO") Then fSoItemSelect.HandleItem()
    End Sub
End Class