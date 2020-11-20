Imports System.ComponentModel
Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmInvoicePOS
    Inherits frmMax
    Friend fItem As frmInvoicePOSItem, fItemPayment As frmInvoicePOSPayment
    Public ObjGetMatVouch As New clsGetRecordsMatVouch
    Dim dv, dv1, dvDivision, dvMatDep, dvDelCamp, dvDelToCamp, dvGSTSubTyp As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        myView.SetGrid(UltraGridItemList)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Payment"), Me.UEGB_ItemList)

        fItem = New frmInvoicePOSItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me


        fItemPayment = New frmInvoicePOSPayment
        fItemPayment.AddtoTab(Me.UltraTabControl1, "Payment", True)
        fItemPayment.fMat = Me


        fItem.fItemRes.InitPanel(fItem, Me)
        fItem.fItemSelect.InitPanel(fItem.UltraTabControl1.Tabs("Quantity").TabPage, fItem.UltraTabControl1.Tabs("MvtCode").TabPage, Me)

        fItem.Enabled = False
        txt_InvoiceNum.ReadOnly = True
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoicePOSModel = Me.InitData("frmInvoicePOSModel", oView, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oView) Then
            GeneratePayment()
            myRow("sply_ty") = HandlePricing()

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then lblSalesOrder.Text = Me.GenerateIDOutput("salesorderdescrip", myUtils.cValTN(myRow("SalesOrderID"))).Description
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then
                ReadOnlyCtl(True)
            End If

            fItem.UltraTabControl1.Tabs("Material").Selected = True
            HideCtlApp(myFuncs.ProgramName(Me.Controller))
            EnableBtn()

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

    Private Sub HideCtlApp(ProductName As String)
        UltraTabControl1.Tabs("SO").Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))

            dvMatDep = myWinSQL.AssignCmb(Me.dsCombo, "DepsMat", "", Me.cmb_matdepid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_DeliveryCampusID,, 2)
            dvDelToCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_ProjectCampusID,, 2)

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                If myUtils.cValTN(myWinSQL2.ParamValue("@ProjectID", Me.Model.ModelParams)) > 0 Then
                    dvDelToCamp.RowFilter = "ProjectID = " & myUtils.cValTN(myWinSQL2.ParamValue("@ProjectID", Me.Model.ModelParams)) & ""
                Else
                    dvDelToCamp.RowFilter = "SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID")) & ""
                End If
            End If


            dv = myWinSQL.AssignCmb(Me.dsCombo, "VoucherType", "", Me.ComboVoucherType,, 1)
            dv.RowFilter = "codeword = 'GI'"
            ComboVoucherType.Value = ComboVoucherType.Rows(0).Cells("codeword").Value

            dv1 = myWinSQL.AssignCmb(Me.dsCombo, "MatVouchType", "", Me.cmb_MatVouchTypeID, , 1)
            dv1.RowFilter = "VouchTypeCode = 'GI' and RefDocTypeCode = 'Oth'"
            myRow("MatVouchTypeID") = cmb_MatVouchTypeID.Rows(0).Cells("MatVouchTypeID").Value


            myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID)
            myWinSQL.AssignCmb(Me.dsCombo, "Party", "", Me.cmb_ConsigneeID)
            myWinSQL.AssignCmb(Me.dsCombo, "TaxInvoiceType", "", Me.cmb_TaxInvoiceType)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)

            fItem.BindModel(NewModel)
            fItemPayment.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)
            fItem.fSoItemSelect.InitPanel(myUtils.cValTN(myRow("SalesOrderID")), 0, Me.fItem, Me, NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            Dim oProc As clsPricingCalcBase = Me.CtlPricing1.InitData("InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", Me.dsForm.Tables("VouchItem"), fItem.CtlPricingChild1)
            oProc.InitGroup("SortIndex", "SubSortIndex", "InvoiceItemType")

            WinFormUtils.ValidateComboValue(cmb_ConsigneeID, myUtils.cValTN(myRow("ConsigneeID")))
            myRow("POSTaxAreaID") = HandleConsigneeID()

            WinFormUtils.ValidateComboValue(cmb_matdepid, myUtils.cValTN(myRow("MatDepID")))
            WinFormUtils.ValidateComboValue(cmb_CustomerID, myUtils.cValTN(myRow("CustomerID")))
            WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

            HandleBillOf(myUtils.cStrTN(myRow("BillOf")))
            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            HandleCampus()
            ProcTypeFilter()

            If Not IsNothing(cmb_CustomerID.SelectedRow) Then
                myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value))
                myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                If myUtils.IsInList(myUtils.cStrTN(myRow("GstInvoiceType")), "EXP") Then myRow("GstInvoiceSubType") = SetGSTSubTypeEXP()
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
        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso fItemPayment.VSave AndAlso Me.ValidateData() Then
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

    Private Sub HandleBillOf(BillOf As String)
        If myUtils.cBoolTN(myRow("DocNumManual")) Then
            txt_InvoiceNum.ReadOnly = False
        Else
            txt_InvoiceNum.ReadOnly = True
        End If
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = "", Filter As String
        If Not IsNothing(cmb_matdepid.SelectedRow) AndAlso (Not IsNothing(cmb_CustomerID.SelectedRow)) Then
            If Not IsNothing(cmb_POSTaxAreaID.SelectedRow) Then
                PartyTaxAreaCode = myUtils.cStrTN(cmb_POSTaxAreaID.SelectedRow.Cells("TaxAreaCode").Value)
            Else
                PartyTaxAreaCode = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value)
            End If

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_matdepid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType in ('SC','SWC')")
            Else
                Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_matdepid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType = 'SC'")
            End If
            CtlPricing1.SetProcTypeFilter(Filter)
        Else
            CtlPricing1.SetProcTypeFilter("1=0")
        End If
    End Sub

    Private Sub cmb_matdepid_Leave(sender As Object, e As EventArgs) Handles cmb_matdepid.Leave
        EnableBtn()
        ProcTypeFilter()

        HandleCampus()
    End Sub

    Private Sub ReadOnlyCtl(Enable As Boolean)
        cmb_matdepid.ReadOnly = Enable
        cmb_CustomerID.ReadOnly = Enable
    End Sub

    Private Sub EnableBtn()
        If (Not IsNothing(cmb_matdepid.SelectedRow)) AndAlso (Not IsNothing(cmb_CustomerID.SelectedRow)) Then
            PanelAddSerial.Enabled = True
        Else
            PanelAddSerial.Enabled = False
        End If
    End Sub

    Private Sub HandleCampus()
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_matdepid, Nothing)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Private Sub dt_InvoiceDate_Leave(sender As Object, e As EventArgs) Handles dt_InvoiceDate.Leave, dt_InvoiceDate.AfterCloseUp
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvMatDep.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "MatDepID")
        myRow("VouchDate") = myRow("PostingDate")
    End Sub

    Private Sub dt_PostingDate_Leave(sender As Object, e As EventArgs) Handles dt_PostingDate.Leave
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub cmb_CustomerID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        EnableBtn()
        ProcTypeFilter()
        If (Not IsNothing(cmb_CustomerID.SelectedRow)) Then
            cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value))
            cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
            If myUtils.IsInList(myUtils.cStrTN(cmb_GstInvoiceType.Value), "EXP") Then cmb_GSTInvoiceSubType.Value = SetGSTSubTypeEXP()
        End If
    End Sub

    Private Sub CtlPricing1_Leave(sender As Object, e As EventArgs)
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
        ProcTypeFilter()
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
        ProcTypeFilter()
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)

        fItem.CtlPricingChild1.HandleChildRowSelect()
        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnSelectSO_Click(sender As Object, e As EventArgs) Handles btnSelectSO.Click
        cm.EndCurrentEdit()
        If Not cmb_matdepid.SelectedRow Is Nothing Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@MainPartyID", myUtils.cValTN(cmb_CustomerID.SelectedRow.Cells("MainPartyID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_matdepid.SelectedRow.Cells("CompanyId").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@InvoiceDate", Format(dt_InvoiceDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
            Params.Add(New clsSQLParam("@SalesOrderID", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("salesorder", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                myRow("SalesOrderID") = myUtils.cValTN(rr1(0)("SalesOrderID"))
                lblSalesOrder.Text = "Sales Order :- " & myUtils.cStrTN(rr1(0)("OrderNum")) & " Date - " & Format(rr1(0)("OrderDate"), "dd-MMM-yyyy")
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            gr.Cells("MatVouchID").Value = myUtils.cValTN(myRow("MatVouchID"))
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
            If MsgBox("Are you sure?" & vbCrLf & "Do you want to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
                myUtils.DeleteRows(fItem.myView.mainGrid.myDv.Table.Select("PMatVouchItemID = " & myUtils.cValTN(r1("MatVouchItemID")) & " "))
                myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select("MatVouchItemID = " & myUtils.cValTN(r1("MatVouchItemID")) & " "))
            End If
        End If

        If (myView.mainGrid.myDv.Table.Select.Length = 0) Then
            fItem.myRow = Nothing
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
        End If
    End Sub

    Private Sub btnRemoveSO_Click(sender As Object, e As EventArgs) Handles btnRemoveSO.Click
        myRow("SalesOrderID") = DBNull.Value
        lblSalesOrder.Text = "Select Sales Order"
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

    Private Sub txt_Remark_Leave(sender As Object, e As EventArgs) Handles txt_Remark.Leave
        cm.EndCurrentEdit()
        fItemPayment.fPaymentMode.txt_PaymentInfo.Value = fItemPayment.fPaymentMode.SetPaymentInfo(myUtils.cStrTN(Me.myRow("Remark")))
    End Sub

    Private Sub GeneratePayment()
        Dim nr As DataRow
        Dim dt1 As DataTable = Me.dsForm.Tables("Payment")
        Dim rr() As DataRow = dt1.Select("InvoiceID" & "=" & myUtils.cValTN(myRow("InvoiceID")))
        If rr.Length > 0 Then
            nr = rr(0)
        Else
            nr = myTables.AddNewRow(dt1)
            nr("InvoiceID") = myUtils.cValTN(myRow("InvoiceID"))
            nr("PaymentItemType") = "IP"
        End If

        fItemPayment.PrepForm(nr)
    End Sub

    Private Sub GetMissingVoucherNo()
        Dim FormatType As String = "J"
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_matdepid.SelectedRow.Cells("CompanyID").Value), GetType(Integer), False))
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