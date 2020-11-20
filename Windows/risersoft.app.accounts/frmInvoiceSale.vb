Imports System.ComponentModel
Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmInvoiceSale
    Inherits frmMax
    Friend fItem As frmInvoiceItem
    Dim myViewAdv As New clsWinView
    Dim oSort As clsWinSortIV, dvSerialType, dvDivision, dvCamp, dvDelCamp, dvDelToCamp, dvGSTSubTyp, dv As DataView
    Dim WithEvents CustomerCodeSys As New clsCodeSystem

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        myView.SetGrid(UltraGridItemList)
        myViewAdv.SetGrid(Me.UltraGridAdv)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Adv"), Me.UEGB_ItemList)

        fItem = New frmInvoiceItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.InvItemType = ""
        fItem.InvFICO = True

        fItem.Enabled = False
        txt_InvoiceNum.ReadOnly = True
        AddHandler fItem.ChildCalling, AddressOf OnChildCalling
    End Sub

    Public Sub OnChildCalling(sender As Object, e As EventArgs)
        If (Not IsNothing(myRow)) AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "IR") Then
            CtlPricing1.SaveAmounts(myRow("postingdate"), "", "AmountTot", "AmountWV")
            If myUtils.cValTN(myRow("ReverseInvoiceID")) > 0 Then CalcPostBalance(myUtils.cValTN(myRow("AmountTot")))
        End If
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoiceSaleModel = Me.InitData("frmInvoiceSaleModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            PanelAddSP.Visible = myUtils.cBoolTN(myRow("HasBOQRef"))
            PanelAddSerial.Visible = Not myUtils.cBoolTN(myRow("HasBOQRef"))

            myRow("sply_ty") = HandlePricing()

            If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
                SetOrgInvoiceNum()
            ElseIf myUtils.cValTN(myRow("ReverseInvoiceID")) > 0 Then
                SetOrgInvoiceNumIR()
            End If

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then lblSalesOrder.Text = Me.GenerateIDOutput("salesorderdescrip", myUtils.cValTN(myRow("SalesOrderID"))).Description
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then
                ReadOnlyCtl(True)
            End If

            SOBtnDisabled()

            If Not myUtils.NullNot(cmb_CustomerID.Value) Then
                cmbCustomerCode.Value = cmb_CustomerID.Value
                cmbGSTIN.Value = cmb_CustomerID.Value
            End If

            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If
            fItem.UltraTabControl1.Tabs("Details").Selected = True
            UltraTabControl1.Tabs("Adv").Visible = myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "IR")
            HideCtlApp(myFuncs.ProgramName(Me.Controller))

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub SOBtnDisabled()
        Dim rr() As DataRow = Me.dsForm.Tables("InvoiceItem").Select("SOSpareID > 0 or SOServiceID > 0")
        Dim rr1() As DataRow = Me.dsForm.Tables("ProdSerialItem").Select()
        If ((Not rr Is Nothing) AndAlso (rr.Length > 0)) OrElse ((Not rr1 Is Nothing) AndAlso (rr1.Length > 0)) Then
            btnSelectSO.Enabled = False
            btnRemoveSO.Enabled = False
        Else
            btnSelectSO.Enabled = True
            btnRemoveSO.Enabled = True
        End If
    End Sub

    Private Sub HideCtlApp(ProductName As String)
        UltraTabControl1.Tabs("SO").Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myViewAdv.PrepEdit(Me.Model.GridViews("Adv"))

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_DeliveryCampusID,, 2)
            dvDelToCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_ProjectCampusID,, 2)

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                If myUtils.cValTN(myWinSQL2.ParamValue("@ProjectID", Me.Model.ModelParams)) > 0 Then
                    dvDelToCamp.RowFilter = "ProjectID = " & myUtils.cValTN(myWinSQL2.ParamValue("@ProjectID", Me.Model.ModelParams)) & ""
                Else
                    dvDelToCamp.RowFilter = "SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID")) & ""
                End If
            End If


            Trace.WriteLine("--Begin Customer Code system conf --")
            dv = New DataView(NewModel.dsCombo.Tables("Customer"))
            CustomerCodeSys.SetConf(dv, Me.cmb_CustomerID, Me.cmbCustomerCode, Me.cmbGSTIN)



            'myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID)
            dvSerialType = myWinSQL.AssignCmb(Me.dsCombo, "SubSerialType", "", Me.cmb_SubSerialType, , 1)
            myWinSQL.AssignCmb(Me.dsCombo, "Party", "", Me.cmb_ConsigneeID)
            myWinSQL.AssignCmb(Me.dsCombo, "TaxInvoiceType", "", Me.cmb_TaxInvoiceType)

            myWinSQL.AssignCmb(Me.dsCombo, "InvoiceTypeCode", "", Me.cmb_InvoiceTypeCode)
            myWinSQL.AssignCmb(Me.dsCombo, "BillOf", "", Me.cmb_BillOf)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)

            fItem.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)
            fItem.fSoItemSelect.InitPanel(myUtils.cValTN(myRow("SalesOrderID")), 0, Me.fItem, Me, NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            Dim oProc As clsPricingCalcBase = Me.CtlPricing1.InitData("InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", Me.dsForm.Tables("InvoiceItem"), fItem.CtlPricingChild1)
            oProc.InitGroup("SortIndex", "SubSortIndex", "InvoiceItemType")

            oSort = New clsWinSortIV(myView, Me.btnUp, Me.btnDown, Me.btnLeft, Me.btnRight, Me.btnRenumber, "SortIndex", "SubSortIndex", "SerialNum", myRow.Row, "SubSerialType")
            If myUtils.cBoolTN(myRow("HasBOQRef")) Then
                oSort.PopulateSerialNum("")
            Else
                oSort.renumber()
            End If

            WinFormUtils.ValidateComboValue(cmb_BillOf, myUtils.cStrTN(myRow("BillOf")))
            WinFormUtils.ValidateComboValue(cmb_ConsigneeID, myUtils.cValTN(myRow("ConsigneeID")))
            myRow("POSTaxAreaID") = HandleConsigneeID()
            WinFormUtils.ValidateComboValue(cmb_InvoiceTypeCode, myUtils.cStrTN(myRow("InvoiceTypeCode")))

            WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
            WinFormUtils.ValidateComboValue(cmb_CustomerID, myUtils.cValTN(myRow("CustomerID")))
            WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

            HandleInvoiceType(myUtils.cStrTN(myRow("InvoiceTypeCode")))
            HandleBillOf(myUtils.cStrTN(myRow("BillOf")))
            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            HandleCampus()
            ProcTypeFilter(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("BillOf")))

            If Not IsNothing(cmb_CustomerID.SelectedRow) Then
                If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "IR") Then
                    myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                Else
                    myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value))
                End If
                myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                If myUtils.IsInList(myUtils.cStrTN(myRow("GstInvoiceType")), "EXP") Then myRow("GstInvoiceSubType") = SetGSTSubTypeEXP()
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub SetOrgInvoiceNum()
        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 OrElse myUtils.cValTN(myRow("ReverseInvoiceID")) > 0 Then
            Dim dtCDNInv As DataTable = Me.Model.DatasetCollection("CDNInv").Tables(0)
            If Not IsNothing(dtCDNInv) AndAlso dtCDNInv.Rows.Count > 0 Then
                lblOrgInvNo.Text = myUtils.cStrTN(dtCDNInv.Rows(0)("InvoiceNum"))
                lblOrgInvDate.Text = dtCDNInv.Rows(0)("InvoiceDate")
            End If
        End If
    End Sub

    Private Sub SetOrgInvoiceNumIR()
        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 OrElse myUtils.cValTN(myRow("ReverseInvoiceID")) > 0 Then
            Dim dtCDNInv As DataTable = Me.Model.DatasetCollection("CDNInv").Tables(0)
            If Not IsNothing(dtCDNInv) AndAlso dtCDNInv.Rows.Count > 0 Then
                lblOrgInvNo.Text = myUtils.cStrTN(dtCDNInv.Rows(0)("InvoiceNum"))
                lblOrgInvDate.Text = dtCDNInv.Rows(0)("InvoiceDate")
                txtPreBal.Value = myUtils.cValTN(dtCDNInv.Rows(0)("PreBalance"))
                CalcPostBalance(myUtils.cValTN(myRow("AmountTot")))
            End If
        End If
    End Sub

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()

        If myUtils.cValTN(myRow("PriceSlabID")) > 0 AndAlso (Not CtlPricing1.HasRowSelected) Then
            WinFormUtils.AddError(Me.CtlPricing1, "Please Select Pricing")
        End If
        If Not IsNothing(CtlPricing1.SlabRow) Then
            myRow("RCHRG") = myUtils.cStrTN(Me.CtlPricing1.SlabRow("RCHRG"))
            myRow("sply_ty") = HandlePricing()
        End If
        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub cmb_InvoiceTypeCode_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceTypeCode.Leave, cmb_InvoiceTypeCode.AfterCloseUp
        HandleInvoiceType(myUtils.cStrTN(cmb_InvoiceTypeCode.Value))
    End Sub

    Private Sub HandleInvoiceType(InvoiceTypeCode As String)
        cmb_BillOf.ReadOnly = myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "PF", "IR")
        UltraPanelOrgInv.Visible = Not myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "PF")
        PanelPreBal.Visible = myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "IR")

        If myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "PF") Then
            myRow("BillOf") = "C"
            cmb_BillOf.Value = "C"
            HandleBillOf(myUtils.cStrTN(myRow("BillOf")))
        ElseIf myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "IR") Then
            myRow("BillOf") = "P"
            cmb_BillOf.Value = "P"
            HandleBillOf(myUtils.cStrTN(myRow("BillOf")))
            btnSelectOrg.Text = "Select Base Invoice"
        End If
        EnableBtn()
    End Sub

    Private Sub cmb_BillOf_Leave(sender As Object, e As EventArgs) Handles cmb_BillOf.Leave, cmb_BillOf.AfterCloseUp
        HandleBillOf(myUtils.cStrTN(cmb_BillOf.Value))
        EnableBtn()
        ProcTypeFilter(myUtils.cStrTN(cmb_InvoiceTypeCode.Value), myUtils.cStrTN(cmb_BillOf.Value))
    End Sub

    Private Sub HandleBillOf(BillOf As String)
        If myUtils.IsInList(myUtils.cStrTN(BillOf), "P") Then
            txt_InvoiceNum.ReadOnly = False
        Else
            If myUtils.cBoolTN(myRow("DocNumManual")) Then
                txt_InvoiceNum.ReadOnly = False
            Else
                txt_InvoiceNum.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub ProcTypeFilter(InvoiceTypeCode As String, BillOf As String)
        Dim PartyTaxAreaCode As String = "", Filter As String
        If Not IsNothing(cmb_campusid.SelectedRow) AndAlso (Not myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "")) AndAlso (Not IsNothing(cmb_CustomerID.SelectedRow)) Then

            If myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "IR") Then
                PartyTaxAreaCode = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value)
            Else
                If Not IsNothing(cmb_POSTaxAreaID.SelectedRow) Then
                    PartyTaxAreaCode = myUtils.cStrTN(cmb_POSTaxAreaID.SelectedRow.Cells("TaxAreaCode").Value)
                Else
                    PartyTaxAreaCode = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value)
                End If
            End If


            If myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "PF") Then
                If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('SC','SP','SWC')")
                Else
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('SC','SP')")
                End If
            ElseIf myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "IR") Then
                Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('IR')")
            ElseIf myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "FD") Then
                If myUtils.IsInList(myUtils.cStrTN(BillOf), "C") Then
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('PV','SC')")
                Else
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('SR')")
                End If
            ElseIf myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "FC") Then
                If myUtils.IsInList(myUtils.cStrTN(BillOf), "P") Then
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('PV','SC')")
                Else
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('SR')")
                End If
            End If
            CtlPricing1.SetProcTypeFilter(Filter)
        Else
            CtlPricing1.SetProcTypeFilter("1=0")
        End If
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        EnableBtn()
        ProcTypeFilter(myUtils.cStrTN(cmb_InvoiceTypeCode.Value), myUtils.cStrTN(cmb_BillOf.Value))

        HandleCampus()
    End Sub

    Private Sub ReadOnlyCtl(Enable As Boolean)
        cmb_InvoiceTypeCode.ReadOnly = Enable
        cmb_campusid.ReadOnly = Enable
        cmb_CustomerID.ReadOnly = Enable
        cmb_BillOf.ReadOnly = Enable
    End Sub

    Private Sub EnableBtn()
        If Not IsNothing(cmb_campusid.SelectedRow) AndAlso (Not IsNothing(cmb_CustomerID.SelectedRow)) AndAlso (Not IsNothing(cmb_InvoiceTypeCode.SelectedRow)) AndAlso Not IsNothing(cmb_BillOf.SelectedRow) Then
            PanelAddSerial.Enabled = True
        Else
            PanelAddSerial.Enabled = False
        End If
    End Sub

    Private Sub HandleCampus()
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, Nothing)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Private Sub dt_InvoiceDate_Leave(sender As Object, e As EventArgs) Handles dt_InvoiceDate.Leave, dt_InvoiceDate.AfterCloseUp
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
    End Sub

    Private Sub cmb_CustomerID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        EnableBtn()
        ProcTypeFilter(myUtils.cStrTN(cmb_InvoiceTypeCode.Value), myUtils.cStrTN(cmb_BillOf.Value))
        If (Not IsNothing(cmb_CustomerID.SelectedRow)) Then

            If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "IR") Then
                cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
            Else
                cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value))
            End If
            cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
            If myUtils.IsInList(myUtils.cStrTN(cmb_GstInvoiceType.Value), "EXP") Then cmb_GSTInvoiceSubType.Value = SetGSTSubTypeEXP()
        End If
    End Sub

    Private Sub CtlPricing1_Leave(sender As Object, e As EventArgs) Handles CtlPricing1.Leave
        cmb_sply_ty.Value = HandlePricing()
        If myUtils.IsInList(myUtils.cStrTN(cmb_GstInvoiceType.Value), "EXP") Then cmb_GSTInvoiceSubType.Value = SetGSTSubTypeEXP()
    End Sub

    Private Function HandlePricing()
        Dim SuplyType As String = ""

        If Not IsNothing(CtlPricing1.SlabRow) Then
            If myUtils.IsInList(myUtils.cStrTN(Me.CtlPricing1.SlabRow("TaxAreaType")), "SAME") Then
                SuplyType = "INTRA"
            Else
                SuplyType = "INTER"
            End If
        End If
        Return SuplyType
    End Function

    Private Function SetGSTSubTypeEXP()
        Dim GSTInvoiceSubType As String = ""
        If Not IsNothing(CtlPricing1.SlabRow) Then
            If myUtils.cBoolTN(Me.CtlPricing1.SlabRow("ZeroTax")) Then
                GSTInvoiceSubType = "WOPAY"
            Else
                GSTInvoiceSubType = "WPAY"
            End If
        End If
        Return GSTInvoiceSubType
    End Function

    Private Sub cmb_ConsigneeID_Leave(sender As Object, e As EventArgs) Handles cmb_ConsigneeID.Leave, cmb_ConsigneeID.AfterCloseUp
        Me.cmb_POSTaxAreaID.Value = HandleConsigneeID()

        EnableBtn()
        ProcTypeFilter(myUtils.cStrTN(cmb_InvoiceTypeCode.Value), myUtils.cStrTN(cmb_BillOf.Value))
    End Sub

    Private Function HandleConsigneeID() As Integer
        Dim POSTaxAreaID As Integer
        If myUtils.cValTN(cmb_ConsigneeID.Value) > 0 Then
            POSTaxAreaID = myUtils.cValTN(cmb_ConsigneeID.SelectedRow.Cells("TaxAreaID").Value)
            cmb_POSTaxAreaID.ReadOnly = True
        Else
            POSTaxAreaID = myUtils.cValTN(myRow("POSTaxAreaID"))
            cmb_POSTaxAreaID.ReadOnly = False
        End If
        Return POSTaxAreaID
    End Function

    Private Sub cmb_POSTaxAreaID_Leave(sender As Object, e As EventArgs) Handles cmb_POSTaxAreaID.Leave, cmb_POSTaxAreaID.AfterCloseUp
        EnableBtn()
        ProcTypeFilter(myUtils.cStrTN(cmb_InvoiceTypeCode.Value), myUtils.cStrTN(cmb_BillOf.Value))
    End Sub

    Private Sub btnSelectOrg_Click(sender As Object, e As EventArgs) Handles btnSelectOrg.Click
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "IR") Then
            GetOrigInvIR()
        Else
            GetOrigInvCDN()
        End If
    End Sub

    Private Sub GetOrigInvIR()
        If myUtils.cValTN(cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(cmb_CustomerID.Value) > 0 AndAlso myUtils.cValTN(cmb_DivisionID.Value) > 0 Then
            cm.EndCurrentEdit()
            Dim Params, Params2 As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@CustomerID", myUtils.cValTN(myRow("CustomerID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CampusID", myUtils.cValTN(myRow("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(myRow("DivisionID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@InvoiceDate", Format(myRow("InvoiceDate"), "yyyy-MMM-dd"), GetType(Date), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("orginvir", Params)

            If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
                Dim rr2() As DataRow = Me.PopulateDataRows("generateprebal", rr1, Params2)
                If (Not IsNothing(rr2)) AndAlso rr2.Length > 0 Then
                    myRow("ReverseInvoiceID") = myUtils.cValTN(rr2(0)("InvoiceID"))
                    lblOrgInvNo.Text = myUtils.cStrTN(rr2(0)("InvoiceNum"))
                    lblOrgInvDate.Text = myUtils.cStrTN(Format(rr2(0)("InvoiceDate"), "dd-MMM-yyyy"))
                    txtPreBal.Value = myUtils.cValTN(rr2(0)("PreBalance"))

                    CtlPricing1.SaveAmounts(myRow("postingdate"), "", "AmountTot", "AmountWV")
                    If myUtils.cValTN(myRow("ReverseInvoiceID")) > 0 Then CalcPostBalance(myUtils.cValTN(myRow("AmountTot")))
                End If
                End If
        End If
    End Sub

    Private Sub GetOrigInvCDN()
        If myUtils.cValTN(cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(cmb_CustomerID.Value) > 0 AndAlso myUtils.cValTN(cmb_DivisionID.Value) > 0 Then
            cm.EndCurrentEdit()
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@CustomerID", myUtils.cValTN(myRow("CustomerID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CampusID", myUtils.cValTN(myRow("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(myRow("DivisionID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@SalesOrderID", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@InvoiceDate", Format(myRow("InvoiceDate"), "yyyy-MMM-dd"), GetType(Date), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("invoice", Params)
            If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
                myRow("CDNInvoiceID") = myUtils.cValTN(rr1(0)("InvoiceID"))
                myRow("SalesOrderID") = myUtils.cValTN(rr1(0)("SalesOrderID"))
                lblOrgInvNo.Text = myUtils.cStrTN(rr1(0)("InvoiceNum"))
                lblOrgInvDate.Text = myUtils.cStrTN(Format(rr1(0)("InvoiceDate"), "dd-MMM-yyyy"))
            End If
        End If
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)

        If (myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "FD") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("BillOf")), "P")) Or (myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "FC") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("BillOf")), "C")) Then
            fItem.cmb_ReturnFY.Visible = True
            fItem.lblReturnFY.Visible = True
        Else
            fItem.cmb_ReturnFY.Visible = False
            fItem.lblReturnFY.Visible = False
        End If

        fItem.CtlPricingChild1.HandleChildRowSelect()
        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnAddSerial_Click(sender As Object, e As EventArgs) Handles btnAddSerial.Click
        Dim gr As UltraGridRow
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            gr = myView.mainGrid.myGrid.DisplayLayout.Bands(0).AddNew()
            gr.Cells("SortIndex").Value = myUtils.MaxVal(myView.mainGrid.myDv.Table, "SortIndex") + 1

            myView.mainGrid.UpdateData()
            oSort.renumber()

            If myView.mainGrid.myDv.Table.Select.Length > 0 Then ReadOnlyCtl(True)

            fItem.Focus()
        End If
    End Sub

    Private Sub btnAddSubSerial_Click(sender As Object, e As EventArgs) Handles btnAddSubSerial.Click
        Dim gr, gr2 As UltraGridRow

        gr = myView.mainGrid.myGrid.ActiveRow
        If gr Is Nothing Then
            MsgBox("Select a Serial No.", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
        Else
            If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
                gr2 = myView.mainGrid.myGrid.DisplayLayout.Bands(0).AddNew()
                gr2.Cells("SortIndex").Value = gr.Cells("SortIndex").Value
                gr2.Cells("SubSortIndex").Value = myUtils.MaxVal(myView.mainGrid.myDv.Table.Select("SortIndex=" & gr.Cells("SortIndex").Value), "SubSortIndex") + 1
                gr2.RefreshSortPosition()

                myView.mainGrid.UpdateData()
                oSort.renumber()

                fItem.dvInvItemType.RowFilter = "CodeWord in ('IST', 'PIS', 'PIC')"

                If myView.mainGrid.myDv.Table.Select.Length > 0 Then ReadOnlyCtl(True)
                fItem.Focus()
            End If
        End If
    End Sub

    Private Sub btnDelItem_Click(sender As Object, e As EventArgs) Handles btnDelItem.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
            ReadOnlyCtl(False)
            fItem.Enabled = False
        End If
        oSort.renumber()
    End Sub

    Private Sub btnSelectSO_Click(sender As Object, e As EventArgs) Handles btnSelectSO.Click
        cm.EndCurrentEdit()
        If Not cmb_campusid.SelectedRow Is Nothing Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@MainPartyID", myUtils.cValTN(cmb_CustomerID.SelectedRow.Cells("MainPartyID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyId").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@InvoiceDate", Format(dt_InvoiceDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
            Params.Add(New clsSQLParam("@SalesOrderID", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("salesorder", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                myRow("SalesOrderID") = myUtils.cValTN(rr1(0)("SalesOrderID"))
                lblSalesOrder.Text = "Sales Order :- " & myUtils.cStrTN(rr1(0)("OrderNum")) & " Date - " & Format(rr1(0)("OrderDate"), "dd-MMM-yyyy")
            End If
        End If
    End Sub

    Private Sub btnRemoveSO_Click(sender As Object, e As EventArgs) Handles btnRemoveSO.Click
        myRow("SalesOrderID") = DBNull.Value
        lblSalesOrder.Text = "Select Sales Order"
    End Sub

    Private Sub BtnAddServices_Click(sender As Object, e As EventArgs) Handles BtnAddServices.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@salesorderid", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@soserviceid", myUtils.MakeCSV(dsForm.Tables("InvoiceItem").Select, "soserviceid"), GetType(Integer), True))
        If myUtils.cValTN(myRow("ProjectCampusID")) > 0 Then Params.Add(New clsSQLParam("@PidUnitID", myUtils.cValTN(cmb_ProjectCampusID.SelectedRow.Cells("PidUnitID").Value), GetType(Integer), False))
        Dim rr() As DataRow = Me.AdvancedSelect("addservices", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            Dim SoServiceIDCSV As String = myUtils.MakeCSV(dsForm.Tables("InvoiceItem").Select, "SoServiceID")
            Dim GetSoServiceIDCSV As String = myCommonUtils.GetChildWithParent(rr(0).Table, rr(0).Table, "SoServiceID", "pSoServiceID")

            For Each r1 As DataRow In rr(0).Table.Select("SoServiceID Not in (" & myUtils.cStrTN(SoServiceIDCSV) & ") and SoServiceID in (" & myUtils.cStrTN(GetSoServiceIDCSV) & ")", "FullSortIndex")
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDv.Table)
                r2("Description") = myUtils.cStrTN(r1("ServiceName"))
                r2("QtySOService") = myUtils.cValTN(r1("Qty"))
                SetRowColumn(r1, r2, "S")
                myCommonUtils.SetSortSubSort(r2, myView.mainGrid.myDv.Table, "SoServiceID", "SoSpareID")

                myView.mainGrid.UpdateData()
                oSort.reSort()
                oSort.PopulateSerialNum("")
            Next
        End If
    End Sub

    Private Sub btnAddSpares_Click(sender As Object, e As EventArgs) Handles btnAddSpares.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@salesorderid", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@sospareid", myUtils.MakeCSV(dsForm.Tables("InvoiceItem").Select("QtyRate > 0"), "sospareid"), GetType(Integer), True))
        If myUtils.cValTN(myRow("ProjectCampusID")) > 0 Then Params.Add(New clsSQLParam("@PidUnitID", myUtils.cValTN(cmb_ProjectCampusID.SelectedRow.Cells("PidUnitID").Value), GetType(Integer), False))

        Dim rr() As DataRow = Me.AdvancedSelect("addspares", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            Dim SoSpareIDCSV As String = myUtils.MakeCSV(dsForm.Tables("InvoiceItem").Select, "SoSpareID")
            Dim GetSoSpareIDCSV As String = myCommonUtils.GetChildWithParent(rr(0).Table, rr(0).Table, "SoSpareID", "pSoSpareID")

            For Each r1 As DataRow In rr(0).Table.Select("SoSpareID Not in (" & myUtils.cStrTN(SoSpareIDCSV) & ") and SoSpareID in (" & myUtils.cStrTN(GetSoSpareIDCSV) & ")", "FullSortIndex")
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDv.Table)
                r2("Description") = myUtils.cStrTN(r1("SpareName"))
                r2("QtySOSpare") = myUtils.cValTN(r1("Qty"))
                SetRowColumn(r1, r2, "M")
                myCommonUtils.SetSortSubSort(r2, myView.mainGrid.myDv.Table, "SoSpareID", "SoServiceID")

                myView.mainGrid.UpdateData()
                oSort.reSort()
                oSort.PopulateSerialNum("")
            Next
        End If
    End Sub

    Private Sub SetRowColumn(r1 As DataRow, r2 As DataRow, ClassType As String)
        If myUtils.cValTN(r1("Qty")) = 0 Then
            r2("InvoiceItemType") = "IGT"
            r2("SortIndex") = DBNull.Value
            r2("SubSortIndex") = DBNull.Value
        Else
            r2("InvoiceItemType") = "IST"
            r2("ClassType") = ClassType
            r2("QtyRate") = myUtils.cValTN(r1("Qty"))
            r2("SortIndex") = DBNull.Value
            r2("SubSortIndex") = DBNull.Value
        End If
    End Sub

    Private Sub btnDelSP_Click(sender As Object, e As EventArgs) Handles btnDelSP.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
            If myUtils.cValTN(r1("SOSpareID")) > 0 Then
                myCommonUtils.DeleteChildWithParent(myView, "SOSpareID", "PSoSpareID")
                myCommonUtils.ReSetIndexField(myView.mainGrid.myDv.Table, "SOSpareID", "PSoSpareID", "SubSortIndex")
            Else
                myCommonUtils.DeleteChildWithParent(myView, "SOServiceID", "PSOServiceID")
                myCommonUtils.ReSetIndexField(myView.mainGrid.myDv.Table, "SOServiceID", "PSOServiceID", "SubSortIndex")
            End If
            oSort.PopulateSerialNum("")
        End If
    End Sub

    Private Sub CalcPostBalance(AmountTot As Decimal)
        txt_PostBalance.Value = myUtils.cValTN(txtPreBal.Value) - AmountTot
    End Sub

    Private Sub btnAddAdv_Click(sender As Object, e As EventArgs) Handles btnAddAdv.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@paymentidcsv", myUtils.MakeCSV(myViewAdv.mainGrid.myDS.Tables(0).Select, "PaymentID"), GetType(Integer), True))
        Params.Add(New clsSQLParam("@customerid", myUtils.cValTN(cmb_CustomerID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@dated", Format(dt_PostingDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@InvoiceID", myUtils.cValTN(myRow("InvoiceID")), GetType(Integer), False))

        Dim rr1() As DataRow = AdvancedSelect("adv", Params)
        If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
            For Each r2 As DataRow In rr1
                Dim r3 As DataRow = myUtils.CopyOneRow(r2, myViewAdv.mainGrid.myDS.Tables(0))
                r3("Amount") = DBNull.Value
                r3("InvoiceID") = frmIDX
            Next
        End If
    End Sub

    Private Sub btnDelAdv_Click(sender As Object, e As EventArgs) Handles btnDelAdv.Click
        myViewAdv.mainGrid.ButtonAction("del")
    End Sub
End Class