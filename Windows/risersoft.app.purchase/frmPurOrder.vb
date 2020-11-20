Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.inventory
Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmPurOrder
    Inherits frmMax
    Friend fItem As frmPurOrderItem
    Dim dvVendor, dvDivision, dvCamp, dvInvCamp As DataView, SelVendorID As Integer
    Dim rrMatReq() As DataRow, objModel As frmPurOrderModel

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
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Notes"), Me.UEGB_ItemList)

        fItem = New frmPurOrderItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        myView.SetGrid(Me.UltraGridItemList)

        fItem.Enabled = False
        EnableBtn(False)
    End Sub

    Private Sub EnableBtn(Bool As Boolean)
        btnAdd.Enabled = Bool
        btnDel.Enabled = Bool
        btnSelectDocument.Enabled = Bool
        ButtonExecute.Enabled = Bool
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        objModel = Me.InitData("frmPurOrderModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            myWinSQL.AssignCmb(Me.dsCombo, "Status", "", Me.cmb_StatusNum)
            myWinSQL.AssignCmb(Me.dsCombo, "ShipTerms", "", Me.cmb_ShipTerms)
            Me.cmb_PayTerms.ValueList = Me.Model.ValueLists("payterms").ComboList
            myWinSQL.AssignCmb(Me.dsCombo, "OrderType", "", Me.Cmb_OrderType)
            myWinSQL.AssignCmb(Me.dsCombo, "MatReqType", "", Me.cmbMatReqType)

            If myUtils.IsInList(myUtils.cStrTN(myRow("OrderType")), "PO") Then
                lblInvoiceCampus.Visible = True
                cmb_InvoiceCampusID.Visible = True
            End If

            Me.Text = myUtils.cStrTN(myWinSQL2.ParamValue("@FormText", Me.Model.ModelParams))
            If myUtils.cValTN(myRow("PidUnitId")) > 0 Then fItem.btnAddOther.Visible = False

            HandleOrderType(myUtils.cStrTN(myRow("OrderType")))
            HandleCurrency(myUtils.cStrTN(myRow("Currency")))

            If myView.mainGrid.myDv.Table.Select.Length > 0 Then
                EnableBtn(Not IsNothing(cmb_CampusId.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)))
                ReadOnlyCtl(True)
            End If

            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If

            HideCtlApp(risersoft.app.mxform.myFuncs.ProgramName(Me.Controller))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub HideCtlApp(ProductName As String)
        btnSelectDocument.Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
        ButtonExecute.Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
        lblRefDoc.Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
        txtRefDocTypeCode.Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
        lblReqType.Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
        cmbMatReqType.Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
        'fItem.UltraTabControl1.Tabs("Cost").Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("PurItems"))

            fItem.BindModel(NewModel)
            fItem.fPurItemAccAss.InitPanel(Me.fItem, Me, NewModel)

            Me.CtlPricing1.InitData("PurOrderID", myUtils.cValTN(frmIDX), "OrderDate", "PurItemID", Me.dsForm.Tables("PurItems"), fItem.CtlPricingChild1)
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusId,, 2)
            dvInvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_InvoiceCampusID,, 2)
            dvVendor = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID, , 2)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)
            HandleDate(myUtils.cDateTN(myRow("OrderDate"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_CampusId, myUtils.cValTN(myRow("campusid")))
            WinFormUtils.ValidateComboValue(cmb_InvoiceCampusID, myUtils.cValTN(myRow("InvoiceCampusID")))
            HandleCampus()
            WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

            If myUtils.cValTN(myRow("VendorID")) > 0 Then
                SelVendorID = myUtils.cValTN(myRow("VendorID"))
                WinFormUtils.ValidateComboValue(cmb_VendorID, myUtils.cValTN(myRow("VendorID")))
            End If
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

        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridItemList_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)
        fItem.CtlPricingChild1.HandleChildRowSelect()

        fItem.myView.mainGrid.myDv.RowFilter = "PurItemID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("PurItemID").Value)
        fItem.myViewMatReq.mainGrid.myDv.RowFilter = "PurItemID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("PurItemID").Value)
        fItem.fPurItemAccAss.myView.mainGrid.myDv.RowFilter = "PurItemID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("PurItemID").Value)

        HandlePurItemHist()
        fItem.HandlePurItemHist(btnDel, CtlPricing1)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")

            fItem.Enabled = True
            ReadOnlyCtl(True)
            fItem.Focus()
        End If
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Cmb_OrderType_Leave(sender As Object, e As EventArgs) Handles Cmb_OrderType.Leave, Cmb_OrderType.AfterCloseUp
        HandleOrderType(myUtils.cStrTN(Cmb_OrderType.Value))
    End Sub

    Private Sub HandleOrderType(OrderType As String)
        If Not myUtils.IsInList(myUtils.cStrTN(OrderType), "") Then
            If myUtils.IsInList(myUtils.cStrTN(OrderType), "PO", "JWO") Then
                dvVendor.RowFilter = "VendorType = 'MS'"

                Me.UltraTabControl1.Tabs("Pricing").Visible = True
                fItem.UltraTabControl1.Tabs("Pricing").Visible = True
            ElseIf myUtils.IsInList(myUtils.cStrTN(OrderType), "LPO") Then
                dvVendor.RowFilter = "VendorType = 'EM'"
                txt_Discount.Visible = False
                lblDiscount.Visible = False

                Me.UltraTabControl1.Tabs("Pricing").Visible = False
                fItem.UltraTabControl1.Tabs("Pricing").Visible = False
            End If

            EnableBtn(Not IsNothing(cmb_CampusId.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)))
            ButtonExecute.Enabled = False
        End If
    End Sub

    Private Sub btnSelectDocument_Click(sender As Object, e As EventArgs) Handles btnSelectDocument.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_CampusId.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@PidUnitID", myUtils.cValTN(myRow("PIDUnitID")), GetType(Integer), False))
        If myUtils.IsInList(myUtils.cStrTN(Cmb_OrderType.Value), "JWO") Then
            Params.Add(New clsSQLParam("@isagainstjwo", 1, GetType(Integer), False))
        Else
            Params.Add(New clsSQLParam("@isagainstjwo", 0, GetType(Integer), False))
        End If
        rrMatReq = Me.AdvancedSelect("matreq", Params)
        If Not rrMatReq Is Nothing AndAlso rrMatReq.Length > 0 Then
            txtRefDocTypeCode.Text = myUtils.cStrTN(rrMatReq(0)("SNum"))
            cmbMatReqType.Value = rrMatReq(0)("MatReqType")

            ButtonExecute.Enabled = True
        End If
    End Sub

    Private Sub ButtonExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExecute.Click
        Dim ObjGetMatVouch As New clsGetRecordsMatVouch()
        If Not myUtils.NullNot(txtRefDocTypeCode.Value) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@matreqid", myUtils.cValTN(rrMatReq(0)("MatReqID")), GetType(Integer), False))
            Dim Model As clsViewModel = Me.GenerateParamsModel("matreqitem", Params)
            Dim dtMatReqItems As DataTable = ObjGetMatVouch.ShowGridItems(Model, "MatReqItemID")
            Dim ds As New DataSet
            myUtils.AddTable(ds, dtMatReqItems, "matreqitem")
            myUtils.AddTable(ds, rrMatReq(0).Table, "matreq")
            If dtMatReqItems.Rows.Count > 0 Then
                Me.Model.DatasetCollection.AddUpd("generatedata", ds)
                Me.OperateBindModel("generate")
                ReadOnlyCtl(True)
                fItem.cmb_ClassType.ReadOnly = True
                txtRefDocTypeCode.Value = DBNull.Value
            End If
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
            ReadOnlyCtl(False)

            EnableBtn(Not IsNothing(cmb_CampusId.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)))
        End If
    End Sub

    Private Sub ReadOnlyCtl(Bool As Boolean)
        Cmb_OrderType.ReadOnly = Bool
        cmb_CampusId.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
        fItem.Enabled = Bool
    End Sub

    Private Sub cmb_Currency_Leave(sender As Object, e As EventArgs) Handles cmb_Currency.Leave, cmb_Currency.AfterCloseUp
        HandleCurrency(myUtils.cStrTN(cmb_Currency.Value))
    End Sub

    Private Sub HandleCurrency(Currency As String)
        If myUtils.IsInList(myUtils.cStrTN(cmb_Currency.Value), "INR") Then
            txt_ExchangeRate.ReadOnly = True
            txt_ExchangeRate.Text = 1
        ElseIf myUtils.IsInList(myUtils.cStrTN(cmb_Currency.Value), "") Then
            txt_ExchangeRate.ReadOnly = True
            txt_ExchangeRate.Text = String.Empty
        Else
            txt_ExchangeRate.ReadOnly = False
            txt_ExchangeRate.Text = String.Empty
        End If
    End Sub

    Private Sub HandlePurItemHist()
        Dim rr1() As DataRow = dsForm.Tables("PurItemHist").Select()
        If rr1.Length > 0 Then
            If Not IsNothing(cmb_VendorID) Then WinFormUtils.SetReadOnly(cmb_VendorID, False, False)
            If Not IsNothing(cmb_InvoiceCampusID) Then WinFormUtils.SetReadOnly(cmb_InvoiceCampusID, False, False)
            If Not IsNothing(CtlPricing1) Then CtlPricing1.IsReadOnly = True
        End If
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = "", Filter, StrFilter As String
        If Not IsNothing(cmb_CampusId.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)
            If (String.IsNullOrEmpty(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value))) And (Not myUtils.IsInList(myUtils.cStrTN(PartyTaxAreaCode), "IMP")) Then
                StrFilter = "ProcType = 'PS' and RCHRG = 'Y'"
            Else
                StrFilter = "ProcType = 'PS' and isNull(IsUnreg,0) = 0"
            End If

            Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("OrderDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_CampusId.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
            CtlPricing1.SetProcTypeFilter(Filter)
        Else
            CtlPricing1.SetProcTypeFilter("1=0")
        End If
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_CampusId.Leave, cmb_CampusId.AfterCloseUp
        EnableBtn(Not IsNothing(cmb_CampusId.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)))
        ProcTypeFilter()
        HandleCampus()
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        EnableBtn(Not IsNothing(cmb_CampusId.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)))
        ProcTypeFilter()
        FillVendorTerms()
    End Sub

    Private Sub btnRate_Click(sender As Object, e As EventArgs) Handles btnRate.Click
        If Not IsNothing(fItem.myRow) AndAlso Not IsNothing(cmb_VendorID.SelectedRow) Then
            Dim dt1 As DataTable = Me.Model.DatasetCollection("supp").Tables("SuppItems")
            Dim rr1() As DataRow = dt1.Select("VendorID = " & myUtils.cValTN(cmb_VendorID.Value) & " and ItemID = " & myUtils.cValTN(fItem.myRow("ItemID")) & "")
            If Not IsNothing(rr1) AndAlso (rr1.Length > 0) Then
                fItem.CtlPricingChild1.SetElementField("bp", "ValuePerQty", myUtils.cValTN(rr1(0)("Rate")))
            End If
        End If
    End Sub

    Private Sub cmb_InvoiceCampusID_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceCampusID.Leave, cmb_InvoiceCampusID.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub FillVendorTerms()
        If (Not Me.cmb_VendorID.SelectedRow Is Nothing) Then
            If myUtils.cValTN(SelVendorID) <> myUtils.cValTN(cmb_VendorID.Value) Then
                Me.cmb_ShipTerms.Value = myUtils.cStrTN(Me.cmb_VendorID.SelectedRow.Cells("shipterms").Value)
                Me.cmb_PayTerms.Value = myUtils.cStrTN(Me.cmb_VendorID.SelectedRow.Cells("payterms").Value)
                Me.cmb_TransMode.Value = myUtils.cStrTN(Me.cmb_VendorID.SelectedRow.Cells("transmode").Value)
                Me.txt_Discount.Text = myUtils.cStrTN(Me.cmb_VendorID.SelectedRow.Cells("discount").Value)
                Me.cmb_FreightIns.Value = myUtils.cStrTN(Me.cmb_VendorID.SelectedRow.Cells("FreightIns").Value)

                SelVendorID = myUtils.cValTN(cmb_VendorID.Value)

                Dim ds As DataSet = Me.GenerateIDOutput("suppitems", myUtils.cValTN(cmb_VendorID.Value)).Data
                Me.Model.DatasetCollection.AddUpd("supp", ds)
            End If
        End If
    End Sub

    Private Sub HandleCampus()
        If Not IsNothing(cmb_CampusId.SelectedRow) Then txtNotes.Text = myUtils.cStrTN(cmb_CampusId.SelectedRow.Cells("PONote").Value)
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_CampusId, cmb_InvoiceCampusID)

        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Private Sub dt_OrderDate_Leave(sender As Object, e As EventArgs) Handles dt_OrderDate.Leave, dt_OrderDate.AfterCloseUp
        HandleDate(dt_OrderDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
        dvInvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
    End Sub
End Class