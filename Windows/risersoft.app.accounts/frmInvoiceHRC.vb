Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform
Imports risersoft.app.mxent

Public Class frmInvoiceHRC
    Inherits frmMax
    Friend fItem As frmInvoiceItemPurch
    Dim myViewVouch As New clsWinView, dvDivision, dvCamp, dvDelCamp, dvGSTSubTyp As DataView, oMasterData As New clsMasterDataHRP(Me.Controller)

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(UltraGridItems)
        myViewVouch.SetGrid(UltraGridVouch)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)

        fItem = New frmInvoiceItemPurch
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.UltraTabControl1.Tabs("AccAss").Visible = False
        fItem.Enabled = False
        fItem.fMat = Me
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoiceHRCModel = Me.InitData("frmInvoiceHRCModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If Not IsNothing(cmb_VendorID.SelectedRow) Then
                myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value))
            End If

            If myViewVouch.mainGrid.myDv.Table.Select().Length > 0 Then
                EnableControls(True)
            End If

            HandleCtl()

            myRow("sply_ty") = HandlePricing()

            If frmMode = EnumfrmMode.acEditM Then
                myRow("ForceAVSysNum") = DBNull.Value
                ChkGetMissingNo.Visible = False
            End If
            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Items"))
            myViewVouch.PrepEdit(Me.Model.GridViews("Vouchers"))

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_DeliveryCampusID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "TY", "", Me.cmb_ty)

            fItem.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)
            fItem.dv.RowFilter = "CodeWord = 'S'"
            fItem.fSoItemSelect.InitPanel(0, 0, Me.fItem, Me, NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            Me.CtlPricing1.InitData("InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", Me.dsForm.Tables("InvoiceItem"), fItem.CtlPricingChild1)
            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
            WinFormUtils.ValidateComboValue(cmb_VendorID, myUtils.cValTN(myRow("VendorID")))
            WinFormUtils.ValidateComboValue(cmb_DeliveryCampusID, myUtils.cValTN(myRow("DeliveryCampusID")))
            HandleCampus()
            WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

            ProcTypeFilter()
            Return True
        End If
        Return False
    End Function

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
            If ChkGetMissingNo.Checked Then
                GetMissingVoucherNo()
            End If
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridItems_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItems.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1, "InvoiceItemID", "InvoiceItemGST", "PostingDate")

        fItem.CtlPricingChild1.HandleChildRowSelect()
        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItems_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItems.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub HandleCtl()
        EnableCtlCampus(False)
        If Not IsNothing(cmb_campusid.SelectedRow) Then
            If Not IsNothing(cmb_VendorID.SelectedRow) Then EnableCtlCampus(True)
        End If
    End Sub

    Private Sub EnableCtlCampus(Bool As Boolean)
        If myViewVouch.mainGrid.myDv.Table.Select().Length > 0 Then btnAddVouch.Enabled = True Else btnAddVouch.Enabled = Bool
        If myViewVouch.mainGrid.myDv.Table.Select().Length > 0 Then btnDelVouch.Enabled = True Else btnDelVouch.Enabled = Bool
        If myViewVouch.mainGrid.myDv.Table.Select().Length > 0 Then btnCreateItems.Enabled = True Else btnCreateItems.Enabled = Bool
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnAdd.Enabled = True Else btnAdd.Enabled = Bool
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnDel.Enabled = True Else btnDel.Enabled = Bool
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            fItem.Focus()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
            fItem.Enabled = False
            EnableCtlCampus(True)
        End If
    End Sub

    Private Sub btnAddVouch_Click(sender As Object, e As EventArgs) Handles btnAddVouch.Click
        If myUtils.cValTN(Me.cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(Me.cmb_VendorID.Value) > 0 Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@paymenthrvouchidcsv", myUtils.MakeCSV(myViewVouch.mainGrid.myDv.Table.Select, "PaymentHRVouchID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@contractorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@invoiceid", frmIDX, GetType(Integer), False))

            Dim rr1() As DataRow = Me.AdvancedSelect("paymenthrvouch", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                For Each r2 As DataRow In rr1
                    Dim rRM As DataRow = oMasterData.rRateMaster(myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyID").Value), myUtils.cDateTN(r2("VouchDate"), Now))
                    Dim r3 As DataRow = myUtils.CopyOneRow(r2, myViewVouch.mainGrid.myDv.Table)
                    r3("InvoiceID") = frmIDX
                Next
            End If
            myViewVouch.mainGrid.myDv.Table.AcceptChanges()

            If myViewVouch.mainGrid.myDv.Table.Select().Length > 0 Then
                EnableControls(True)
            End If
        End If
    End Sub

    Private Sub EnableControls(ByVal Bool As Boolean)
        cmb_campusid.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
    End Sub

    Private Sub btnDelVouch_Click(sender As Object, e As EventArgs) Handles btnDelVouch.Click
        myViewVouch.mainGrid.ButtonAction("del")
        If myViewVouch.mainGrid.myDv.Table.Select().Length = 0 Then
            EnableControls(False)
        End If
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = "", Filter, StrFilter As String
        If Not IsNothing(cmb_campusid.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            If (String.IsNullOrEmpty(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value))) And (Not myUtils.IsInList(myUtils.cStrTN(PartyTaxAreaCode), "IMP")) Then
                StrFilter = "ProcType = 'PS' and RCHRG = 'Y'"
            Else
                StrFilter = "ProcType = 'PS' and isNull(IsUnreg,0) = 0"
            End If

            PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)
            If (Not IsNothing(cmb_DeliveryCampusID.SelectedRow)) Then
                Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_DeliveryCampusID.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
            Else
                Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
            End If

            CtlPricing1.SetProcTypeFilter(Filter)
        Else
            CtlPricing1.SetProcTypeFilter("1=0")
        End If
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        HandleCtl()
        ProcTypeFilter()

        HandleCampus()
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        HandleCtl()
        ProcTypeFilter()

        If (Not IsNothing(cmb_VendorID.SelectedRow)) Then cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(cmb_ty.Value))
        cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
        If (Not IsNothing(cmb_VendorID.SelectedRow)) Then HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value))
    End Sub

    Private Sub HandleCampus()
        If Not IsNothing(cmb_campusid.SelectedRow) Then
            dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, Nothing)
            If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
            If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
        End If
    End Sub

    Private Sub btnCreateItems_Click(sender As Object, e As EventArgs) Handles btnCreateItems.Click
        Me.cm.EndCurrentEdit()
        If myUtils.cValTN(myRow("PriceSlabID")) <> 0 Then
            If myViewVouch.mainGrid.myDv.Count > 0 Then
                If myView.mainGrid.myDv.Count = 0 Then
                    fItem.INVHR = True
                    Dim gr As UltraGridRow
                    gr = myView.mainGrid.ButtonAction("add")
                    fItem.CtlPricingChild1.SetPriceSlabChild(myUtils.cValTN(myRow("PriceSlabID")))

                    Dim TotContAmt As Decimal = myUtils.cValTN(myViewVouch.mainGrid.Model.GetColSum("TotalAmount", "InvoiceID is Not NULL"))
                    fItem.CtlPricingChild1.SetElementField("bp", "ValuePerQty", TotContAmt)
                    fItem.cm.EndCurrentEdit()

                    UltraTabControl2.Tabs("Items").Selected = True
                    fItem.Focus()
                    fItem.INVHR = False
                End If
            Else
                MsgBox("Please select Vouchers. Then proceed...", MsgBoxStyle.Information, myWinApp.Vars("appname"))
            End If
        Else
            MsgBox("Please select Pricing. Then proceed...", MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

    Private Sub dt_InvoiceDate_Leave(sender As Object, e As EventArgs) Handles dt_InvoiceDate.Leave, dt_InvoiceDate.AfterCloseUp
        HandleCtl()
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        dvDelCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
    End Sub

    Private Sub HideControl(TaxAreaCode As String)
        Dim Visible As Boolean = myUtils.IsInList(TaxAreaCode, "IMP")
        cmb_ty.Visible = Visible
        lblTY.Visible = Visible
        lblBOENum.Visible = Visible
        lblBOEDate.Visible = Visible
        lblBOEVal.Visible = Visible
        txt_boe_num.Visible = Visible
        txt_boe_val.Visible = Visible
        dt_boe_dt.Visible = Visible
    End Sub

    Private Sub CtlPricing1_Leave(sender As Object, e As EventArgs) Handles CtlPricing1.Leave
        cmb_sply_ty.Value = HandlePricing()
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

    Private Sub cmb_ty_Leave(sender As Object, e As EventArgs) Handles cmb_ty.Leave, cmb_ty.AfterCloseUp
        If Not IsNothing(cmb_VendorID.SelectedRow) Then
            cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(cmb_ty.Value))
            cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
        End If
    End Sub

    Private Sub cmb_DeliveryCampusID_Leave(sender As Object, e As EventArgs) Handles cmb_DeliveryCampusID.Leave, cmb_DeliveryCampusID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub GetMissingVoucherNo()
        Dim FormatType As String = "J"
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Dated", Format(dt_InvoiceDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@FormatType", "'" & myUtils.cStrTN(FormatType) & "'", GetType(String), False))
        Dim oRet As clsProcOutput = GenerateParamsOutput("missingdocsysnum", Params)
        If oRet.Success Then
            myRow("ForceAVSysNum") = oRet.ID
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub
End Class