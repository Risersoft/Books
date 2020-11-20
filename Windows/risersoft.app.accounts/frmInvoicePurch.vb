Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform

Public Class frmInvoicePurch
    Inherits frmMax
    Friend fItem As frmInvoiceItemPurch
    Dim dv, dvDivision, dvCamp, dvDelCamp, dvGSTSubTyp As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(UltraGridItemList)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)

        fItem = New frmInvoiceItemPurch
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.Enabled = False
        fItem.InvFICO = True
        fItem.fMat = Me
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoicePurchModel = Me.InitData("frmInvoicePurchModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then

            If myUtils.cValTN(Me.myRow("SalesOrderID")) > 0 Then
                PanelWCDetails.Visible = True
                Dim rr() As DataRow = Me.GenerateIDOutput("order", myUtils.cValTN(Me.myRow("SalesOrderID"))).Data.Tables(0).Select
                If rr.Length > 0 Then lblWCDetails.Text = "WC Supplier Invoice  For Customer - " & myUtils.cStrTN(rr(0)("Customer")) & "  and  Order No. - " & myUtils.cStrTN(rr(0)("Ordernum"))
                fItem.dv.RowFilter = "CodeWord = 'S'"
            End If


            HandleInvoiceType(myUtils.cStrTN(myRow("InvoiceTypeCode")))

            If myView.mainGrid.myDv.Table.Select.Length > 0 Then
                ReadOnlyCtl(True)
            End If

            fItem.UltraTabControl1.Tabs("Details").Selected = True
            myRow("sply_ty") = HandlePricing()
            SetOrgInvoiceNum()

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

    Private Sub SetOrgInvoiceNum()
        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            Dim dtCDNInv As DataTable = Me.Model.DatasetCollection("CDNInv").Tables(0)
            If Not IsNothing(dtCDNInv) AndAlso dtCDNInv.Rows.Count > 0 Then
                lblOrgInvNo.Text = myUtils.cStrTN(dtCDNInv.Rows(0)("InvoiceNum"))
                lblOrgInvDate.Text = dtCDNInv.Rows(0)("InvoiceDate")
            End If
        End If
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            dvcamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_DeliveryCampusID,, 2)
            dv = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID,, 2)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)
            myWinSQL.AssignCmb(Me.dsCombo, "InvoiceTypeCode", "", Me.cmb_InvoiceTypeCode)
            myWinSQL.AssignCmb(Me.dsCombo, "BillOf", "", Me.cmb_BillOf)
            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "TY", "", Me.cmb_ty)

            fItem.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)
            fItem.fSoItemSelect.InitPanel(0, 0, Me.fItem, Me, NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            If myUtils.cValTN(Me.myRow("SalesOrderID")) > 0 Then
                Me.CtlPricing1.InitData("InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", Me.dsForm.Tables("InvoiceItem"), fItem.CtlPricingChild1)
            Else
                Me.CtlPricing1.InitData("InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", Me.dsForm.Tables("InvoiceItem"), fItem.CtlPricingChild1)
            End If

            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
            WinFormUtils.ValidateComboValue(cmb_DeliveryCampusID, myUtils.cValTN(myRow("DeliveryCampusID")))
            WinFormUtils.ValidateComboValue(cmb_VendorID, myUtils.cValTN(myRow("VendorID")))
            HandleCampus()
            WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

            ProcTypeFilter(myUtils.cValTN(Me.myRow("SalesOrderID")))

            If Not IsNothing(cmb_VendorID.SelectedRow) Then
                myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(myRow("GstInvoiceType")))
            End If
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then ReadOnlyCtl(True)

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
            ReadOnlyCtl(False)
        End If
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1, "InvoiceItemID", "InvoiceItemGST", "PostingDate")

        fItem.CtlPricingChild1.HandleChildRowSelect()
        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub ReadOnlyCtl(Bool As Boolean)
        cmb_InvoiceTypeCode.ReadOnly = Bool
        cmb_campusid.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
    End Sub

    Private Sub HandleCampus()
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, Nothing)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value

        If Not IsNothing(cmb_campusid.SelectedRow) Then
            If myUtils.IsInList(myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("CampusType").Value), "PS") Then
                fItem.TransTypeFilter = "and CodeWord in ('APN', 'APU', 'ARO', 'ARW', 'ST','CC','NC','CA','NA','Exp')"
            Else
                fItem.TransTypeFilter = ""
            End If

            If myUtils.cValTN(Me.myRow("SalesOrderID")) > 0 Then
                dv.RowFilter = "TaxAreaCode = '" & myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value) & "'"
            Else
                dv.RowFilter = ""
            End If
            If Not IsNothing(cmb_VendorID.SelectedRow) Then EnableCtlCampus(True) Else EnableCtlCampus(False)
        Else
            EnableCtlCampus(False)
        End If
    End Sub

    Private Sub EnableCtlCampus(Bool As Boolean)
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnAdd.Enabled = True Else btnAdd.Enabled = Bool
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then btnDel.Enabled = True Else btnDel.Enabled = Bool
    End Sub

    Private Sub ProcTypeFilter(SalesOrderID As Integer)
        Dim PartyTaxAreaCode As String = "", Filter, StrFilter As String

        If Not IsNothing(cmb_campusid.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)

            If myUtils.cValTN(SalesOrderID) > 0 Then
                If Not IsNothing(cmb_DeliveryCampusID.SelectedRow) Then
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_DeliveryCampusID.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType = 'PWC'")
                Else
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType = 'PWC'")
                End If
            Else
                If (String.IsNullOrEmpty(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value))) And (Not myUtils.IsInList(myUtils.cStrTN(PartyTaxAreaCode), "IMP")) Then
                    StrFilter = "ProcType = 'PS' and RCHRG = 'Y'"
                Else
                    StrFilter = "ProcType = 'PS' and isNull(IsUnreg,0) = 0"
                End If



                If Not IsNothing(cmb_DeliveryCampusID.SelectedRow) Then
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_DeliveryCampusID.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
                Else
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
                End If
            End If
            CtlPricing1.SetProcTypeFilter(Filter)
        Else
            CtlPricing1.SetProcTypeFilter("1=0")
        End If

    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        HandleCampus()
        ProcTypeFilter(myUtils.cValTN(Me.myRow("SalesOrderID")))
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        If (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            ProcTypeFilter(myUtils.cValTN(Me.myRow("SalesOrderID")))
            cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
            cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
            HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_GstInvoiceType.Value))
            EnableCtlCampus(True)
        Else
            EnableCtlCampus(False)
        End If
    End Sub

    Private Sub dt_InvoiceDate_Leave(sender As Object, e As EventArgs) Handles dt_InvoiceDate.Leave, dt_InvoiceDate.AfterCloseUp
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        dvDelCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
    End Sub

    Private Sub cmb_InvoiceTypeCode_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceTypeCode.Leave, cmb_InvoiceTypeCode.AfterCloseUp
        HandleInvoiceType(myUtils.cStrTN(cmb_InvoiceTypeCode.Value))
    End Sub

    Private Sub HandleInvoiceType(InvoiceTypeCode As String)
        If myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "PF") Then
            If myUtils.IsInList(myUtils.cStrTN(myRow("BillOf")), "C") Then
                Me.Text = "Vendor Expense"
                lblInvDate.Text = "Voucher Date"
                lblInvType.Text = "Voucher Type"
                lblInvNo.Text = "Voucher No."
            End If
            cmb_BillOf.ReadOnly = True
            HandleBillOf(myUtils.cStrTN(myRow("BillOf")))
        Else
            If myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "FD") Then
                Me.Text = "Debit Note FICO"
            ElseIf myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "FC") Then
                Me.Text = "Credit Note FICO"
            End If
            cmb_BillOf.ReadOnly = False
            lblInvDate.Text = "Note Date"
            lblInvNo.Text = "Note No."
            lblInvType.Text = "Note Type"
        End If

        If myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "FD", "FC") Then
            UltraPanelOrgInv.Visible = True
        Else
            UltraPanelOrgInv.Visible = False
        End If
    End Sub

    Private Sub cmb_BillOf_Leave(sender As Object, e As EventArgs) Handles cmb_BillOf.Leave, cmb_BillOf.AfterCloseUp
        HandleBillOf(myUtils.cStrTN(cmb_BillOf.Value))
    End Sub

    Private Sub HandleBillOf(BillOf As String)
        If myUtils.IsInList(myUtils.cStrTN(BillOf), "P") Then
            txt_InvoiceNum.ReadOnly = False
        Else
            txt_InvoiceNum.ReadOnly = True
        End If
    End Sub

    Private Sub HideControl(TaxAreaCode As String, GSTInvType As String)
        Dim Visible As Boolean = False
        If myUtils.IsInList(TaxAreaCode, "IMP") Then Visible = True
        cmb_ty.Visible = Visible
        lblTY.Visible = Visible
        If myUtils.IsInList(GSTInvType, "IMPG") Then Visible = True Else Visible = False

        lblBOENum.Visible = Visible
        lblBOEDate.Visible = Visible
        lblBOEVal.Visible = Visible
        txt_boe_num.Visible = Visible
        txt_boe_val.Visible = Visible
        dt_boe_dt.Visible = Visible
        lblPort.Visible = Visible
        txt_port_code.Visible = Visible
    End Sub

    Private Sub CtlPricing1_Leave(sender As Object, e As EventArgs) Handles CtlPricing1.Leave
        cmb_sply_ty.Value = HandlePricing()
    End Sub

    Private Sub frmInvoicePurch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        ProcTypeFilter(myUtils.cValTN(Me.myRow("SalesOrderID")))
    End Sub

    Private Sub btnSelectOrg_Click(sender As Object, e As EventArgs) Handles btnSelectOrg.Click
        If myUtils.cValTN(cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(cmb_VendorID.Value) > 0 AndAlso myUtils.cValTN(cmb_DivisionID.Value) > 0 Then
            cm.EndCurrentEdit()
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@VendorID", myUtils.cValTN(myRow("VendorID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CampusID", myUtils.cValTN(myRow("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(myRow("DivisionID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@SalesOrderID", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@InvoiceDate", Format(myRow("InvoiceDate"), "yyyy-MMM-dd"), GetType(Date), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("invoice", Params)
            If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
                myRow("CDNInvoiceID") = myUtils.cValTN(rr1(0)("InvoiceID"))
                lblOrgInvNo.Text = myUtils.cStrTN(rr1(0)("InvoiceNum"))
                lblOrgInvDate.Text = myUtils.cStrTN(Format(rr1(0)("InvoiceDate"), "dd-MMM-yyyy"))

                If myUtils.IsInList(myUtils.cStrTN(rr1(0)("InvoiceTypeCode")), "PM") Then
                    Dim oRet As clsProcOutput = Me.GenerateIDOutput("PurOrder", myUtils.cValTN(Me.myRow("CDNInvoiceID")))
                    If oRet.Success Then
                        If oRet.Description.Trim.Length > 0 Then
                            myRow("OtherRef") = myUtils.cStrTN(oRet.Description)
                        End If
                    End If
                End If
            End If
        End If
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