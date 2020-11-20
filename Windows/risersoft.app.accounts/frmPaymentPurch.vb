Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmPaymentPurch
    Inherits frmMax
    Friend fItem As frmInvoiceItemPurch
    Dim fPaymentMode As frmPaymentMode, dvDivision, dvCamp, dvDelCamp As DataView, myViewAdv As New clsWinView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)
        Me.AddTabExpansionUEGB(Me.UltraTabControl2.Tabs("Pricing"), Me.UEGB_ItemList)

        fItem = New frmInvoiceItemPurch
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        fPaymentMode = New frmPaymentMode
        AddHandler fPaymentMode.PaymentModeChanged, AddressOf PaymentModeChanged
        fPaymentMode.AddtoTab(Me.UltraTabControl2, "Mode", True)
        fItem.Enabled = False

        myView.SetGrid(UltraGridItemList)
        myViewAdv.SetGrid(Me.UltraGridAdv)
    End Sub

    Private Sub PaymentModeChanged(sender As Object, PaymentMode As String, IgnoreExpenseVoucher As Boolean)
        If myUtils.IsInList(myUtils.cStrTN(PaymentMode), "IM") AndAlso IgnoreExpenseVoucher = False Then
            TabCantrol.Tabs("TA").Visible = True
            If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                EnableCtl(False)
            End If
        Else
            TabCantrol.Tabs("TA").Visible = False
        End If
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPaymentPurchModel = Me.InitData("frmPaymentPurchModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            myRow("sply_ty") = HandlePricing()
            fItem.UltraTabControl1.Tabs("Details").Selected = True

            CalculateBalance()

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
            myViewAdv.PrepEdit(Me.Model.GridViews("Adv"))

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_DeliveryCampusID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)

            fItem.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)
            fPaymentMode.BindModel(NewModel)
            fPaymentMode.InitPanel(Me, True)
            fPaymentMode.HandleItem(myUtils.cValTN(myRow("CompanyID")), myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))
            fItem.fSoItemSelect.InitPanel(0, 0, Me.fItem, Me, NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            Me.CtlPricing1.InitData("PaymentID", myUtils.cValTN(frmIDX), "PostingDate", "PaymentItemTransId", Me.dsForm.Tables("PaymentItemTrans"), fItem.CtlPricingChild1)

            HandleDate(myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
            WinFormUtils.ValidateComboValue(cmb_VendorID, myUtils.cValTN(myRow("VendorID")))
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
            CalculateBalance()
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
            fItem.Focus()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
            fItem.Enabled = False
        End If
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1, "PaymentItemTransID", "InvoiceItemGST", "Dated")

        fItem.CtlPricingChild1.HandleChildRowSelect()
        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        cm.EndCurrentEdit()
        If Not IsNothing(cmb_campusid.SelectedRow) Then myRow("CompanyID") = myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyID").Value)
        fPaymentMode.HandleItem(myUtils.cValTN(myRow("CompanyID")), myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))

        HandleCampus()
        ProcTypeFilter()
    End Sub

    Private Sub HandleCampus()
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, Nothing)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleDate(dt_Dated.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        dvDelCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        fPaymentMode.HandleItem(myUtils.cValTN(myRow("CompanyID")), dated)
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

    Private Sub btnAddAdv_Click(sender As Object, e As EventArgs) Handles btnAddAdv.Click
        If (Not myUtils.NullNot(fPaymentMode.cmb_ImprestEmployeeID.Value)) AndAlso (Not myUtils.NullNot(cmb_campusid.Value)) Then
            If Not IsNothing(myViewAdv) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("companyid").Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(fPaymentMode.cmb_ImprestEmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewAdv.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("tourvouch", Params), Params2)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewAdv.mainGrid.myDS.Tables(0))
                        r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    Next

                    CalculateBalance()
                End If
            End If

            If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                EnableCtl(False)
            End If
        End If
    End Sub

    Private Sub btnDelAdv_Click(sender As Object, e As EventArgs) Handles btnDelAdv.Click
        myViewAdv.mainGrid.ButtonAction("del")
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            EnableCtl(True)
        End If
    End Sub

    Private Sub EnableCtl(Enabled As Boolean)
        WinFormUtils.SetReadOnly(fPaymentMode, False, Enabled)
    End Sub

    Private Sub CalculateBalance()
        For Each r1 As DataRow In myViewAdv.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub UltraGridAdv_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridAdv.AfterCellUpdate
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            CalculateBalance()
        End If
    End Sub

    Private Sub txt_Remark_Leave(sender As Object, e As EventArgs) Handles txt_Remark.Leave
        cm.EndCurrentEdit()
        fPaymentMode.txt_PaymentInfo.Value = fPaymentMode.SetPaymentInfo(myUtils.cStrTN(Me.myRow("Remark")))
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = "", Filter, StrFilter As String
        If (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            If Not IsNothing(cmb_campusid.SelectedRow) Then
                PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)
                If (String.IsNullOrEmpty(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value))) And (Not myUtils.IsInList(myUtils.cStrTN(PartyTaxAreaCode), "IMP")) Then
                    StrFilter = "ProcType = 'PS' and RCHRG = 'Y'"
                Else
                    StrFilter = "ProcType = 'PS' and isNull(IsUnreg,0) = 0"
                End If

                If Not IsNothing(cmb_DeliveryCampusID.SelectedRow) Then
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("Dated"), PartyTaxAreaCode, myUtils.cStrTN(cmb_DeliveryCampusID.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
                Else
                    Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("Dated"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
                End If
                CtlPricing1.SetProcTypeFilter(Filter)
            Else
                CtlPricing1.SetProcTypeFilter("1=0")
            End If
            lblVouchNo.Text = "Invoice No."
            txt_VouchNum.ReadOnly = False
        Else
            Me.CtlPricing1.SetProcTypeFilter("ProcType = 'PS' and RCHRG = 'Y'")
            lblVouchNo.Text = "Voucher No."
            txt_VouchNum.ReadOnly = True
        End If
    End Sub

    Private Sub cmb_DeliveryCampusID_Leave(sender As Object, e As EventArgs) Handles cmb_DeliveryCampusID.Leave, cmb_DeliveryCampusID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub GetMissingVoucherNo()
        Dim FormatType As String = ""
        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "CA") Then FormatType = "C" Else FormatType = "J"
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Dated", Format(dt_PostingDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@FormatType", "'" & myUtils.cStrTN(FormatType) & "'", GetType(String), False))
        Dim oRet As clsProcOutput = GenerateParamsOutput("missingdocsysnum", Params)
        If oRet.Success Then
            myRow("ForceAVSysNum") = oRet.ID
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub
End Class