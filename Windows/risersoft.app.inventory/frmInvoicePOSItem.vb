Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports Infragistics.Win.UltraWinGrid

Public Class frmInvoicePOSItem
    Inherits frmMax
    Friend fMat As frmInvoicePOS, fItemRes As frmMatVouchRes, fItemSelect As frmItemSelect, fItem As risersoft.app.accounts.frmInvoiceItemGST
    Friend myViewStBalDep, myViewStBalCamp As New clsWinView, oMasterMM As clsMasterDataMM
    Dim dv, dvQtySrc, dvSpSt, dvClassType, dvStStage, dvClass, dvMatDep, dvHSN As DataView
    Dim StockItemID, CovFacItemID As Integer, ItemSourceODNote As Boolean = False
    Dim WithEvents MvtCodeSystem As New clsCodeSystem
    Friend WithEvents fCostAssign As risersoft.app.accounts.frmCostAssign, fSoItemSelect As frmSOItemSelect

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myViewStBalDep.SetGrid(UltraGridStBalDep)
        myViewStBalCamp.SetGrid(UltraGridStBalCamp)

        fItem = New risersoft.app.accounts.frmInvoiceItemGST
        fItem.AddtoTab(Me.UltraTabControl1, "GST", True)

        fSoItemSelect = New frmSOItemSelect
        fSoItemSelect.AddtoTab(Me.UltraTabControl1, "accass", True)
        fSoItemSelect.ctlPricingChild = Me.CtlPricingChild1

        fItemRes = New frmMatVouchRes
        fItemRes.AddtoTab(Me.UltraTabControl1, "Reservation", True)

        fItemSelect = New frmItemSelect
        fItemSelect.AddtoTab(Me.UltraTabControl1, "Material", True)

        fCostAssign = New risersoft.app.accounts.frmCostAssign
        fCostAssign.AddtoTab(Me.UltraTabControl1, "Cost", True)

        oMasterMM = New clsMasterDataMM(Me.Controller)

        ControlStatus()
        AddHandler CtlPricingChild1.CellUpdated, AddressOf CellUpdated
    End Sub

    Private Sub CellUpdated(sender As Object, rChildElem As DataRow)
        If (Not IsNothing(myRow)) Then
            CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
        End If
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dv = New DataView(NewModel.dsCombo.Tables("MatMvtCode"))
        MvtCodeSystem.SetConf(dv, Me.cmb_MvtCode, cmbMvtCode)

        dvMatDep = myWinSQL.AssignCmb(NewModel.dsCombo, "DepsMatItem", "", Me.cmb_matdepid, , 2)
        dvStStage = myWinSQL.AssignCmb(NewModel.dsCombo, "Stockstage", "", Me.cmb_StockStage, , 2)
        dvClass = myWinSQL.AssignCmb(NewModel.dsCombo, "ValuationClass", "", Me.cmb_ValuationClass, , 2)

        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitIDEntry)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_OrderRateUnitID)

        dvClassType = myWinSQL.AssignCmb(NewModel.dsCombo, "ClassType", "", Me.cmb_ClassType,, 2)
        dvQtySrc = myWinSQL.AssignCmb(NewModel.dsCombo, "QtyTypeSrc", "", Me.cmb_QtyTypeSrc, , 2)
        dvSpSt = myWinSQL.AssignCmb(NewModel.dsCombo, "SpStock", "", Me.cmb_SpStock, , 2)
        'dvSpSt.Sort = "Tag"

        myWinSQL.AssignCmb(NewModel.dsCombo, "TaxCredit", "", Me.cmb_TaxCredit)
        dvHSN = myWinSQL.AssignCmb(NewModel.dsCombo, "HSNSac", "", Me.cmb_Hsn_Sc,, 2)

        fItemRes.myViewIssue.PrepEdit(NewModel.GridViews("ResIssueItem"), , fItemRes.btnDelIssue)
        fItemRes.myView.PrepEdit(NewModel.GridViews("ResRecItem"), , fItemRes.btnDelRec)

        fItemRes.BindModel(NewModel)
        fItemSelect.BindModel(NewModel)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Dim strVouchTypeCode As String = ""
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.FormPrepared = True
            myRow("InvoiceItemType") = "IST"
            fItemSelect.HandleItem()
            fCostAssign.HandleItem("InvoiceItemID", "VouchDate", myUtils.cValTN(fMat.cmb_matdepid.SelectedRow.Cells("CampusID").Value), myRow.Row)
            If Not fMat.cmb_MatVouchTypeID.SelectedRow Is Nothing Then strVouchTypeCode = myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("VouchTypeCode").Value)

            'If Not myUtils.IsInList(myUtils.cStrTN(strVouchTypeCode), "") Then
            '    If myUtils.IsInList(myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "Oth") Then
            '        dv.RowFilter = "MvtType = '" & myUtils.cStrTN(strVouchTypeCode) & "' and (DocType is Null or AllowOther = 1) and " & fMat.ObjGetMatVouch.MvtCodeFilter(fMat.myRow("TaxType"), dv)
            '    Else
            '        dv.RowFilter = "MvtType = '" & myUtils.cStrTN(strVouchTypeCode) & "' and DocType = '" & myUtils.cStrTN(fMat.cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value) & "' and " & fMat.ObjGetMatVouch.MvtCodeFilter(fMat.myRow("TaxType"), dv)
            '    End If
            'End If

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

            fItemRes.myViewIssue.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))
            fItemRes.myView.mainGrid.myDv.RowFilter = "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))

            CtlPricingChild1.HandleChildRowSelect()

            risersoft.app.mxform.myFuncs.TransTypeFilter(dvClass, myUtils.cStrTN(myRow("ClassType")), myUtils.cStrTN(myRow("TransType")))
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()

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

    Private Sub ctlPrepForm()
        fItemSelect.cmb_BaseUnitID.ReadOnly = True
        ControlStatus()
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
        Dim rCompany As DataRow, TotalQty As Decimal
        Me.InitError()
        VSave = False
        EndCurrentEdit()

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.cmb_MvtCode, "Please Generate Transaction")
            Exit Function
        End If

        If myUtils.NullNot(fItemSelect.cmb_ItemId.Value) Then WinFormUtils.AddError(fItemSelect.cmb_ItemId, "Please Select Item Code", Me.eBag)
        If myUtils.NullNot(cmb_MvtCode.Value) Then WinFormUtils.AddError(cmb_MvtCode, "Please Select Mvt. Code")
        If myUtils.NullNot(cmb_SpStock.Value) Then WinFormUtils.AddError(cmb_SpStock, "Please Select Sp. Stock")
        If Not myUtils.NullNot(cmb_ItemUnitID2.Value) AndAlso myUtils.cValTN(txt_QtySKU1.Value) > 0 AndAlso myUtils.cValTN(txt_QtySKU2.Value) = 0 Then WinFormUtils.AddError(txt_QtySKU2, "Please define Convert factor for Item")


        Dim CompanyId As Integer = Me.Controller.CommonData.GetCompanyIDFromDepMat(fMat.cmb_matdepid.Value)
        rCompany = Me.Controller.CommonData.rCompany(CompanyId)

        If rCompany("FinStartDate") <= fMat.dt_VouchDate.Value AndAlso myUtils.cValTN(txt_QtyEntry.Value) <= 0 Then WinFormUtils.AddError(txt_QtyEntry, "Please Enter Quantity")
        If myUtils.cValTN(txt_QtyEntry.Value) > 0 AndAlso myUtils.cValTN(txt_QtySKU1.Value) = 0 Then WinFormUtils.AddError(txt_QtySKU1, "Please Enter Qty SKU1")

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
                    If myUtils.cValTN(myRow("QtyEntry")) <> myUtils.cValTN(TotalQty) Then WinFormUtils.AddError(txt_QtyEntry, "Enter Quantity According to Order History")
                ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("OrderUpdateCode").Value), "I") Then
                    TotalQty = myUtils.cValTN(fMat.dsForm.Tables("Pur").Compute("sum(QtyIssue)", "MatVouchItemID = " & myUtils.cValTN(myRow("MatVouchItemID"))))
                    If myUtils.cValTN(myRow("QtyEntry")) <> myUtils.cValTN(TotalQty) Then
                        If MsgBox("Do you want to save withount PO Reference ?", MsgBoxStyle.YesNo, myWinApp.Vars("appname")) = MsgBoxResult.No Then
                            WinFormUtils.AddError(txt_QtyEntry, "Enter Quantity According to Order History")
                        End If
                    End If
                End If
            End If

            myRow("TransType") = myRow("StockStage")
            If fItem.VSave AndAlso Me.CanSave AndAlso fItemSelect.ValidateData Then
                cm.EndCurrentEdit()
                fItemSelect.CalculateQty()
                CtlPricingChild1.UpdatePricingTable(myRow.Row)
                fItem.CalculateGSTAmount(CtlPricingChild1, myUtils.IsInList(myUtils.cStrTN(fMat.myRow("GSTInvoiceType")), "EXP"), myUtils.cStrTN(cmb_TaxCredit.Value))
                CtlPricingChild1.SaveAmounts("BasicRate", "AmountTot", "AmountWV", "AmountBasic")
                VSave = True
            End If
        End If
    End Function

    Public Overrides Function ValidateData() As Boolean
        Me.InitError()
        If fMat.ObjGetMatVouch.CalculateFieldType("V", True, myRow.Row, oMasterMM, "CustomerID") = "R" AndAlso myUtils.NullNot(fMat.cmb_CustomerID.Value) Then WinFormUtils.AddError(fMat.cmb_CustomerID, "Please Select Voucher Customer Name", Me.eBag)
        If fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "ClassType") = "R" AndAlso myUtils.NullNot(cmb_ClassType.Value) Then WinFormUtils.AddError(cmb_ClassType, "Please Select Class Type")
        If myUtils.IsInList(myUtils.cStrTN(myRow("classtype")), "s", "m", "") AndAlso myUtils.NullNot(cmb_StockStage.Value) Then WinFormUtils.AddError(cmb_StockStage, "Please Select Stock Stage")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "MatDepID") = "R" AndAlso myUtils.NullNot(cmb_matdepid.Value) Then WinFormUtils.AddError(cmb_matdepid, "Please Select Department")
        If fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "QtyTypeSrc") = "R" AndAlso myUtils.NullNot(cmb_QtyTypeSrc.Value) Then WinFormUtils.AddError(cmb_QtyTypeSrc, "Please Select Qty Type Src.")
        If myUtils.IsInList(myUtils.cStrTN(fMat.ObjGetMatVouch.CalculateFieldType("V", False, myRow.Row, oMasterMM, "Pricing")), "R") AndAlso fMat.myRow("VouchDate") > risersoft.app.mxform.myFuncs.GSTLounchDate AndAlso myUtils.NullNot(Me.cmb_TaxCredit.Value) Then WinFormUtils.AddError(Me.cmb_TaxCredit, "Select Tax Credit")
        If myUtils.IsInList(myUtils.cStrTN(fMat.ObjGetMatVouch.CalculateFieldType("V", False, myRow.Row, oMasterMM, "Pricing")), "R") AndAlso fMat.myRow("VouchDate") > risersoft.app.mxform.myFuncs.GSTLounchDate AndAlso (Me.cmb_Hsn_Sc.SelectedRow) Is Nothing Then WinFormUtils.AddError(Me.cmb_Hsn_Sc, "Select HSN Code")
        Return Me.CanSave
    End Function

    Private Sub txt_QtyEntry_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_QtyEntry.Leave
        fItemSelect.CalculateQty()
    End Sub

    Private Sub btnAddHSN_Click(sender As Object, e As EventArgs) Handles btnAddHSN.Click
        Dim f As New frmHsnSac
        If f.PrepForm(fMat.myView, EnumfrmMode.acAddM, "") Then
            f.ShowDialog()
            If Not IsNothing(f.myRow) Then
                Dim nr As DataRow = myUtils.CopyOneRow(f.myRow.Row, dsCombo.Tables("HsnSac"))
                nr("Description") = f.myRow.Row("Code") & "-" & f.myRow.Row("Description")
                cmb_Hsn_Sc.Value = myUtils.cStrTN(f.myRow.Row("Code"))
            End If
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

        If Not myUtils.NullNot(cmb_SpStock.Value) Then
            SpStockLeaveEvent()
        End If

        If (Not IsNothing(cmb_MvtCode.SelectedRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MvtCode.SelectedRow.Cells("EntRefType").Value), "PID2") Then
            fItemSelect.TableLayoutPanel2.Visible = True
        End If

        fItemSelect.CalculateQty()
    End Sub

    Private Sub HideCtl(Visible As Boolean)
        Me.cmb_TaxCredit.Visible = Visible
        Me.lblTaxCredit.Visible = Visible
        lblHSN.Visible = Visible
        btnAddHSN.Visible = Visible
        cmb_Hsn_Sc.Visible = Visible
    End Sub

    Private Sub SpStockLeaveEvent()
        cmb_QtyTypeSrc.Value = oMasterMM.MvtCodeField("QtyTypeSrc", cmb_MvtCode.Value, cmb_SpStock.Value, dvQtySrc, ",", cmb_QtyTypeSrc.Value)
        cmb_ClassType.Value = oMasterMM.MvtCodeField("ClassType", cmb_MvtCode.Value, cmb_SpStock.Value, dvClassType, ",", cmb_ClassType.Value)

        EndCurrentEdit()
        SetFieldSpStock()
        ClassTypeLeaveEvent()
    End Sub

    Private Sub SetFieldSpStock()
        Dim str1 As String
        Dim r3 As DataRow = oMasterMM.GetMvtCodeDataRow(cmb_MvtCode.Value)
        Dim r4 As DataRow = oMasterMM.GetMvtCodeSPDataRow(cmb_MvtCode.Value, cmb_SpStock.Value)

        str1 = fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_ClassType, Nothing, myRow.Row, "I", "ClassType", True)
        If str1 = "N" Then
            Me.cmb_ClassType.Value = "M"
            Me.HandleClassType(r4)
        End If

        If fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_matdepid, Nothing, myRow.Row, "I", "MatDepID", True) = "N" Then cmb_matdepid.Value = DBNull.Value

        fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_QtyTypeSrc, Nothing, myRow.Row, "I", "QtyTypeSrc", True)
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

    Private Sub cmb_ClassType_Leave(sender As Object, e As EventArgs) Handles cmb_ClassType.Leave, cmb_ClassType.AfterCloseUp
        If ItemSourceODNote = False Then ClassTypeLeaveEvent()
    End Sub

    Private Function ClassTypeLeaveEvent() As Boolean
        If Not IsNothing(cmb_MvtCode.SelectedRow) AndAlso Not IsNothing(cmb_SpStock.SelectedRow) Then
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
        Me.cmb_StockStage.ReadOnly = False
        If myUtils.IsInList(myUtils.cStrTN(cmb_ClassType.Value), "S") Then
            Me.cmb_ValuationClass.Value = "LABOUR"
            Me.fItem.cmb_ty.Value = "S"
            dvHSN.RowFilter = "Ty = 'S'"
        ElseIf myUtils.IsInList(myUtils.cStrTN(Me.cmb_ClassType.Value), "A") Then
            cmb_ValuationClass.ReadOnly = False
            Me.cmb_StockStage.ReadOnly = True
            cmb_StockStage.Value = DBNull.Value
            Me.fItem.cmb_ty.Value = "G"
            dvHSN.RowFilter = "Ty = 'G'"
        Else
            Me.cmb_ClassType.Value = "M"
            Me.fItem.cmb_ty.Value = "G"
            dvHSN.RowFilter = "Ty = 'G'"
        End If

        Return True
    End Function

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "Pricing" Then
            EndCurrentEdit()
            CtlPricingChild1.UpdatePricingTable(myRow.Row)
        ElseIf Me.FormPrepared AndAlso e.Tab.Key = "Quantity" Then
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
        End If

        If Me.FormPrepared AndAlso e.Tab.Key = "GST" Then fItem.CalculateGSTAmount(CtlPricingChild1, myUtils.IsInList(myUtils.cStrTN(fMat.myRow("GSTInvoiceType")), "EXP"), myUtils.cStrTN(cmb_TaxCredit.Value))
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

    Private Sub EndCurrentEdit()
        If Not IsNothing(fMat.myRow) Then fMat.cm.EndCurrentEdit()
        If Not IsNothing(myRow) Then Me.cm.EndCurrentEdit()
    End Sub
End Class