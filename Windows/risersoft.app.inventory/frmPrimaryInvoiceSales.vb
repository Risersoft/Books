Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmPrimaryInvoiceSales
    Inherits frmMax
    Friend fItem As risersoft.app.accounts.frmInvoiceItem
    Friend myViewODNote, myViewRec, myViewItemCredit, MyViewForms, MyViewPayments As New clsWinView
    Dim oSort As clsWinSortIV, oSort1 As clsWinSort, dv, dvVendor, dvDivision, dvCamp, dvDelCamp, dvProjectSite, dvGSTSubTyp As DataView
    Dim PrSiteFilter As String
    Dim WithEvents CustomerCodeSys, VendorCodeSys As New clsCodeSystem

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        myViewODNote.SetGrid(Me.UltraGridVoucherList)
        myViewRec.SetGrid(Me.UltraGridRec)
        myViewItemCredit.SetGrid(Me.UltraGridItemCredit)
        myView.SetGrid(UltraGridItemList)
        MyViewForms.SetGrid(UltraGridForms)
        MyViewPayments.SetGrid(UltraGridPayment)

        fItem = New risersoft.app.accounts.frmInvoiceItem
        fItem.AddToPanel(Me.PanelInvoiceItem, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.InvItemType = ",'ISP'"
        WinFormUtils.SetReadOnly(Me.PanelInvoiceItem, True)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPrimaryInvoiceSalesModel = Me.InitData("frmPrimaryInvoiceSalesModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_deliverycampusid,, 2)
            dvProjectSite = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_ProjectCampusID,, 2)
            If myUtils.cValTN(myWinSQL2.ParamValue("@ProjectID", Me.Model.ModelParams)) > 0 Then
                dvProjectSite.RowFilter = "ProjectID = " & myUtils.cValTN(myWinSQL2.ParamValue("@ProjectID", Me.Model.ModelParams)) & ""
            Else
                dvProjectSite.RowFilter = "SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID")) & ""
            End If
            PrSiteFilter = dvProjectSite.RowFilter


            Trace.WriteLine("--Begin Vendor Code system conf --")
            dvVendor = New DataView(Me.dsCombo.Tables("Vendor"))
            VendorCodeSys.SetConf(dvVendor, Me.cmb_VendorID, Me.cmbVendorCode, Me.cmbVendorGSTIN)

            Trace.WriteLine("--Begin Customer Code system conf --")
            dv = New DataView(Me.dsCombo.Tables("Customer"))
            CustomerCodeSys.SetConf(dv, Me.cmb_CustomerID, Me.cmbCustomerCode, Me.cmbCustomerGSTIN)


            myWinSQL.AssignCmb(Me.dsCombo, "Party", "", Me.cmb_ConsigneeID)
            myWinSQL.AssignCmb(Me.dsCombo, "TaxInvoiceType", "", Me.cmb_TaxInvoiceType)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)

            If myUtils.cBoolTN(myWinSQL2.ParamValue("@RefReciept", Me.Model.ModelParams)) Then
                txt_RefReceipt.ReadOnly = True
                UltraPanelOrgInv.Visible = False
            Else
                txt_RefReceipt.ReadOnly = False
                UltraPanelOrgInv.Visible = True
            End If

            HandleBillOf(myUtils.cStrTN(myRow("BillOf")))

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                myRow("InvoiceTypeCode") = "PM"
                cmb_ProjectCampusID.Visible = True
                lblProjectCampusID.Visible = True

                lblSalesOrder.Text = Me.GenerateIDOutput("salesorderdescrip", myUtils.cValTN(myRow("SalesOrderID"))).Description

                Dim rMain As DataRow = GetMainPartyID(myUtils.cValTN(myRow("SalesOrderID")))
                If Not IsNothing(rMain) Then dv.RowFilter = "Isnull(MainPartyID,0) = " & myUtils.cValTN(rMain("MainPartyID")) & " or Isnull(MainPartyID,0) = " & myUtils.cValTN(rMain("AuthMainPartyID"))
                Me.FormPrepared = True
            Else
                Me.FormPrepared = (myUtils.cStrTN(myRow("InvoiceTypeCode")).Trim.Length > 0)
                cmb_ProjectCampusID.Visible = False
                lblProjectCampusID.Visible = False
            End If

            If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC") Then
                If myUtils.cValTN(myRow("VendorID")) > 0 Then cmb_VendorID.Value = myUtils.cValTN(myRow("VendorID"))
                If Not IsNothing(cmb_VendorID.SelectedRow) Then
                    myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value))
                    myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                End If

                If Not myUtils.NullNot(cmb_VendorID.Value) Then
                    cmbVendorCode.Value = cmb_VendorID.Value
                    cmbVendorGSTIN.Value = cmb_VendorID.Value
                End If
            Else
                If myUtils.cValTN(myRow("CustomerID")) > 0 Then cmb_CustomerID.Value = myUtils.cValTN(myRow("CustomerID"))
                If Not IsNothing(cmb_CustomerID.SelectedRow) Then
                    myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value))
                    myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                End If

                If Not myUtils.NullNot(cmb_CustomerID.Value) Then
                    cmbCustomerCode.Value = cmb_CustomerID.Value
                    cmbCustomerGSTIN.Value = cmb_CustomerID.Value
                End If
            End If
            If myUtils.IsInList(myUtils.cStrTN(myRow("GstInvoiceType")), "EXP") Then myRow("GstInvoiceSubType") = SetGSTSubTypeEXP()

            myRow("sply_ty") = HandlePricing()

            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
            WinFormUtils.ValidateComboValue(cmb_deliverycampusid, myUtils.cValTN(myRow("deliverycampusid")))
            HandleCampus()
            WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

            If myViewODNote.mainGrid.myDv.Table.Select.Length > 0 OrElse myViewRec.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                EnableControls(True)
            End If

            SOBtnDisabled()
            GenerateChallanText()

            SetOrgInvoiceNum()


            If Not myUtils.NullNot(cmb_CustomerID.Value) Then
                cmbVendorCode.Value = cmb_CustomerID.Value
                cmbVendorGSTIN.Value = cmb_CustomerID.Value
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
        UltraTabControl1.Tabs("SO").Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
    End Sub

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

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            MyViewForms.PrepEdit(Me.Model.GridViews("Forms"), btnAddForms, btnDelForms)
            myViewODNote.PrepEdit(Me.Model.GridViews("ODNote"))
            myViewRec.PrepEdit(Me.Model.GridViews("Rec"))
            myViewItemCredit.PrepEdit(Me.Model.GridViews("ItemCredit"))
            MyViewPayments.PrepEdit(Me.Model.GridViews("Payment"), btnAddPayment, btnDelPayment)

            fItem.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)

            CustVendorVisable(myUtils.cStrTN(myRow("InvoiceTypeCode")))
            fItem.fSoItemSelect.InitPanel(myUtils.cValTN(myRow("SalesOrderID")), 0, Me.fItem, Me, NewModel)

            Dim oProc As clsPricingCalcBase = Me.CtlPricing1.InitData("InvoiceId", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", Me.dsForm.Tables("InvoiceItem"), fItem.CtlPricingChild1)
            oProc.InitGroup("sortindex", "subsortindex", "invoiceitemtype")

            oSort = New clsWinSortIV(myView, Me.btnUp, Me.btnDown, Nothing, Nothing, Nothing, "SortIndex", "SubSortIndex", "SerialNum", myRow.Row, "SubSerialType")
            oSort.renumber()

            oSort1 = New clsWinSort(MyViewPayments, Nothing, Nothing, Nothing, "SortIndex")
            oSort1.renumber()
            Return True
        End If
        Return False
    End Function

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

    Private Function GetMainPartyID(SalesOrderID As Integer) As DataRow
        Dim rr1() As DataRow = Me.GenerateIDOutput("salesorder", SalesOrderID).Data.Tables(0).Select
        If rr1.Length > 0 Then
            Return rr1(0)
        Else
            Return Nothing
        End If
    End Function

    Private Sub CustVendorVisable(ByVal InvType As String)
        cmb_CustomerID.Visible = False
        cmb_VendorID.Visible = False
        lblVendor.Visible = False
        lblCustomer.Visible = False
        cmbCustomerGSTIN.Visible = False
        cmbVendorGSTIN.Visible = False
        cmbCustomerCode.Visible = False
        cmbVendorCode.Visible = False

        If myUtils.IsInList(myUtils.cStrTN(InvType), "QD", "QC") Then
            If myUtils.IsInList(myUtils.cStrTN(InvType), "QD") Then
                Me.Text = "Quantity Debit Note"
            Else
                Me.Text = "Quantity Credit Note"
            End If
            lblVendor.Visible = True
            cmb_VendorID.Visible = True
            lblReff.Visible = True
            txt_RefReceipt.Visible = True

            lblInvDate.Text = "Note Date"
            lblInvNo.Text = "Note No."

            UltraPanelOrgInv.Visible = True
            cmbVendorCode.Visible = True
            cmbVendorGSTIN.Visible = True
        Else
            lblReff.Visible = False
            txt_RefReceipt.Visible = False
            lblCustomer.Visible = True
            cmb_CustomerID.Visible = True
            UltraPanelOrgInv.Visible = False
            cmbCustomerGSTIN.Visible = True
            cmbCustomerCode.Visible = True
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

    Private Sub EnableControls(ByVal Bool As Boolean)
        cmb_campusid.ReadOnly = Bool
        cmb_CustomerID.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
        cmb_deliverycampusid.ReadOnly = Bool
        cmb_DivisionID.ReadOnly = Bool
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myUtils.cValTN(Me.cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(Me.cmb_DivisionID.Value) > 0 AndAlso (myUtils.cValTN(Me.cmb_CustomerID.Value) > 0 OrElse myUtils.cValTN(Me.cmb_VendorID.Value) > 0) Then
            cm.EndCurrentEdit()
            Dim Params As New List(Of clsSQLParam)
            Dim PriceProcID As Integer
            If myViewODNote.mainGrid.myDv.Count > 0 Then
                Dim rr3() As DataRow = myViewODNote.mainGrid.myDv.Table.Select(myFuncsBase.IVSDChallanTypeFilter)
                If rr3.Length > 0 Then
                    PriceProcID = myUtils.cValTN(rr3(0)("PriceProcID"))
                Else
                    PriceProcID = 0
                End If
            Else
                PriceProcID = 0
            End If

            Params.Add(New clsSQLParam("@odnoteidcsv", myUtils.MakeCSV(myViewODNote.mainGrid.myDv.Table.Select, "OdNoteID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@priceprocid", PriceProcID, GetType(Integer), False))
            Params.Add(New clsSQLParam("@invoicetypecode", "'" & myUtils.cStrTN(myRow("InvoiceTypeCode")) & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@salesorderid", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_campusid.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@invoiceid", frmIDX, GetType(Integer), False))
            Params.Add(New clsSQLParam("@customerid", myUtils.cValTN(cmb_CustomerID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@consigneeid", myUtils.cValTN(cmb_ConsigneeID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@deliverycampusid", myUtils.cValTN(cmb_deliverycampusid.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(cmb_DivisionID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@POSTaxAreaID", myUtils.cValTN(cmb_POSTaxAreaID.Value), GetType(Integer), False))
            If Not IsNothing(cmb_CustomerID.SelectedRow) Then Params.Add(New clsSQLParam("@PartyType", "'" & myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("PartyType").Value) & "'", GetType(String), False))

            Dim rr1() As DataRow = Me.AdvancedSelect("odnote", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                Dim strf1 As String = myUtils.CombineWhere(False, myFuncsBase.IVSDChallanTypeFilter, If(myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC"), "", "sysincl = 1"))
                Dim rr2() As DataRow = myWinData.SelectDistinct(rr1(0).Table, "PriceProcID", False,, strf1).Select
                If rr2.Length > 1 Then
                    MsgBox("Vouchers Should be same Price Proc.", MsgBoxStyle.Information, myWinApp.Vars("appname"))
                    Exit Sub
                Else
                    For Each r2 As DataRow In rr1
                        r2("invoiceid") = myUtils.cValTN(myRow("invoiceid"))
                        Dim r3 As DataRow = myUtils.CopyOneRow(r2, myViewODNote.mainGrid.myDv.Table)
                        myRow("TaxInvoiceType") = myUtils.cStrTN(r2("TaxInvoiceType"))
                        If myUtils.cValTN(rr1(0)("ConsigneeID")) > 0 Then myRow("ConsigneeID") = myUtils.cValTN(rr1(0)("ConsigneeID"))
                        myRow("ConsigneePrefix") = myUtils.cStrTN(rr1(0)("ConsigneePrefix"))
                        myRow("POSTaxAreaID") = myUtils.cValTN(rr1(0)("POSTaxAreaID"))
                        myRow("DeliveryTo") = myUtils.cStrTN(rr1(0)("DeliveryTo"))
                    Next
                End If
                GenerateChallanText()
                btnGenrate()
                RefReciept()
                cmb_sply_ty.Value = HandlePricing()
                GenerateGST(Me.dsForm.Tables("InvoiceItem"))
                If myViewODNote.mainGrid.myDv.Table.Select.Length > 0 OrElse myViewRec.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                    EnableControls(True)
                End If
                UltraTabControl2.Tabs("Items").Selected = True
            End If
        End If
    End Sub

    Private Sub GenerateChallanText()
        myRow("ChallanText") = DBNull.Value
        For Each r2 As DataRow In myViewODNote.mainGrid.myDv.Table.Select
            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanText")), "") Then
                If (Not myUtils.IsInList(myUtils.cStrTN(r2("ChallanNum")), "")) Then myRow("ChallanText") = myUtils.cStrTN(r2("ChallanNum"))
            Else
                If (Not myUtils.IsInList(myUtils.cStrTN(r2("ChallanNum")), "")) Then myRow("ChallanText") = myUtils.cStrTN(myRow("ChallanText")) & ", " & myUtils.cStrTN(r2("ChallanNum"))
            End If
        Next
    End Sub

    Private Sub GenerateGST(dtInvoiceItem As DataTable)

        For Each r3 As DataRow In dtInvoiceItem.Select
            Dim r4 As DataRow
            Dim dt1 As DataTable = dtInvoiceItem.DataSet.Tables("InvoiceItemGST")
            Dim rr() As DataRow = dt1.Select("InvoiceItemID" & "=" & myUtils.cValTN(r3("InvoiceItemID")))
            If rr.Length > 0 Then
                r4 = rr(0)
            Else
                r4 = myTables.AddNewRow(dt1)
                r4("InvoiceItemID") = myUtils.cValTN(r3("InvoiceItemID"))
            End If

            r4("CAMT") = Math.Round(SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "CGST", "TotalValueCalc"), 2)
            r4("SAMT") = Math.Round(SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "SGST", "TotalValueCalc"), 2)
            r4("IAMT") = Math.Round(SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "IGST", "TotalValueCalc"), 2)
            r4("CSAMT") = Math.Round(SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "CESSGST", "TotalValueCalc"), 2)
            r4("RT") = Math.Round(SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "IGST", "PerValue") + SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "SGST", "PerValue") + SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "CGST", "PerValue"), 2)
            r4("tx_c") = r4("CAMT")
            r4("tx_s") = r4("SAMT")
            r4("tx_i") = r4("IAMT")
            r4("tx_cs") = r4("CSAMT")


            If myUtils.cValTN(r4("IAMT")) > 0 Then
                r4("TXVAL") = Math.Round(SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "IGST", "AmountBase"), 2)
            Else
                r4("TXVAL") = Math.Round(SetColumnValue(Me.dsForm.Tables("_PriceCalc"), myUtils.cValTN(r4("InvoiceItemID")), "SGST", "AmountBase"), 2)
            End If

            If myUtils.IsInList(myUtils.cStrTN(r3("ClassType")), "M") Then
                r4("ty") = "G"
            ElseIf myUtils.IsInList(myUtils.cStrTN(r3("ClassType")), "S") Then
                r4("ty") = "S"
            ElseIf myUtils.IsInList(myUtils.cStrTN(r3("ClassType")), "A") Then
                r4("ty") = "G"
            End If
        Next

    End Sub

    Public Shared Function SetColumnValue(dt As DataTable, InvoiceItemID As Integer, ElemCode As String, ValueField As String) As Decimal
        Dim ElemValue As Decimal = 0
        Dim rr1() As DataRow = dt.Select("CIDValue = " & InvoiceItemID & "  and elemCode = '" & ElemCode & "'")
        If rr1.Length > 0 Then
            ElemValue = myUtils.cValTN(rr1(0)(ValueField))
        End If

        Return ElemValue
    End Function

    Private Sub RefReciept()
        If myViewODNote.mainGrid.myDv.Count > 0 Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@MatVouchIDCSV", myUtils.MakeCSV(myViewODNote.mainGrid.myDv.Table.Select, "MatVouchID"), GetType(Integer), True))
            Dim oRet As clsProcOutput = Me.GenerateParamsOutput("refreciept", Params)
            If oRet.Success Then
                Dim rr1() As DataRow = oRet.Data.Tables(0).Select
                If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
                    myRow("CDNInvoiceID") = myUtils.cValTN(rr1(0)("RecvPInvoiceID"))
                End If

                If oRet.Description.Trim.Length > 0 Then
                    txt_RefReceipt.ReadOnly = True
                    UltraPanelOrgInv.Visible = False
                    myRow("refreceipt") = oRet.Description
                Else
                    txt_RefReceipt.ReadOnly = False
                    UltraPanelOrgInv.Visible = True
                End If
            End If
        Else
            myRow("refreceipt") = String.Empty
        End If
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        If fItem.PrepForm(r1) Then
            fItem.CtlPricingChild1.HandleChildRowSelect()
            fItem.cmb_TaxCredit.ReadOnly = False
        End If
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnGenrate()
        Me.OperateBindModel("generate")

        win.myWinUtils.SelectRow(UltraGridItemList)
        oSort.renumber()
        CtlPricing1.SaveAmounts(myRow("postingdate"), "", "AmountTot", "AmountWV")
        myRow("AmountTot") = myUtils.cValTN(myRow("AmountTot")) - myUtils.cValTN(myViewItemCredit.mainGrid.Model.GetColSum("Amount"))
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        cm.EndCurrentEdit()
        If Not myViewODNote.mainGrid.myGrid.ActiveRow Is Nothing Then
            myViewODNote.mainGrid.ButtonAction("del")
            btnGenrate()
        End If
        RefReciept()
        GenerateChallanText()

        If myViewODNote.mainGrid.myDv.Table.Select.Length = 0 AndAlso myViewRec.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            EnableControls(False)
            myRow("ConsigneeID") = DBNull.Value
            myRow("POSTaxAreaID") = DBNull.Value
        End If

        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
        End If
    End Sub

    Private Sub UltraGridPayment_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridPayment.BeforeRowDeactivate
        MyViewPayments.mainGrid.myGrid.ActiveRow.Cells("PayAmount").Value = (myUtils.cValTN(myRow("AmountTot")) * myUtils.cValTN(MyViewPayments.mainGrid.myGrid.ActiveRow.Cells("PerValue").Value)) / 100
        oSort1.renumber()
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs)
        cm.EndCurrentEdit()
        btnGenrate()
        UltraTabControl2.Tabs("Items").Selected = True
    End Sub

    Private Sub btnAddRec_Click(sender As Object, e As EventArgs) Handles btnAddRec.Click
        If myUtils.cValTN(Me.cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(Me.cmb_CustomerID.Value) > 0 Then
            cm.EndCurrentEdit()
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@matvouchidcsv", myUtils.MakeCSV(myViewRec.mainGrid.myDS.Tables(0).Select, "MatVouchID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_campusid.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@invoiceid", frmIDX, GetType(Integer), False))
            Params.Add(New clsSQLParam("@customerid", myUtils.cValTN(cmb_CustomerID.Value), GetType(Integer), False))

            Dim rr1() As DataRow = Me.AdvancedSelect("matvouch", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                For Each r2 As DataRow In rr1
                    Dim r3 As DataRow = myUtils.CopyOneRow(r2, myViewRec.mainGrid.myDS.Tables(0))
                Next
                Me.OperateBindModel("generateitemcredit")
                CtlPricing1.SaveAmounts(myRow("postingdate"), "", "AmountTot", "AmountWV")
                myRow("AmountTot") = myUtils.cValTN(myRow("AmountTot") - myUtils.cValTN(myViewItemCredit.mainGrid.Model.GetColSum("Amount")))
            End If

            If myViewODNote.mainGrid.myDv.Table.Select.Length > 0 OrElse myViewRec.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                EnableControls(True)
            End If
        End If
    End Sub

    Private Sub btnDelRec_Click(sender As Object, e As EventArgs) Handles btnDelRec.Click
        If Not myViewRec.mainGrid.myGrid.ActiveRow Is Nothing Then
            cm.EndCurrentEdit()
            myViewRec.mainGrid.ButtonAction("del")

            myViewItemCredit.mainGrid.UpdateData()
            Me.OperateBindModel("generateitemcredit")
            CtlPricing1.SaveAmounts(myRow("postingdate"), "", "AmountTot", "AmountWV")
            myRow("AmountTot") = myUtils.cValTN(myRow("AmountTot") - myUtils.cValTN(myViewItemCredit.mainGrid.Model.GetColSum("Amount")))
        End If
        If myViewODNote.mainGrid.myDv.Table.Select.Length = 0 AndAlso myViewRec.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            EnableControls(False)
        End If
    End Sub

    Private Sub HandleCampus()
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, cmb_deliverycampusid)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub cmb_deliverycampusid_Leave(sender As Object, e As EventArgs) Handles cmb_deliverycampusid.Leave, cmb_deliverycampusid.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub dt_InvoiceDate_Leave(sender As Object, e As EventArgs) Handles dt_InvoiceDate.Leave, dt_InvoiceDate.AfterCloseUp
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        dvDelCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)

        Dim Str1 As String = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        dvProjectSite.RowFilter = myUtils.CombineWhere(False, Str1, PrSiteFilter)
    End Sub

    Private Sub cmb_CustomerID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp, cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC") Then
            If Not IsNothing(cmb_VendorID.SelectedRow) Then
                cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value))
            End If
        Else
            If Not IsNothing(cmb_CustomerID.SelectedRow) Then
                cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypeSale(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value))
            End If
        End If
        cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
        If myUtils.IsInList(myUtils.cStrTN(cmb_GstInvoiceType.Value), "EXP") Then cmb_GSTInvoiceSubType.Value = SetGSTSubTypeEXP()
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

    Private Sub btnClearConsignee_Click(sender As Object, e As EventArgs) Handles btnClearConsignee.Click
        cmb_ConsigneeID.Value = DBNull.Value
        txt_ConsigneePrefix.Text = String.Empty
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
            End If
        End If
    End Sub

    Private Sub SetOrgInvoiceNum()
        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            Dim dtCDNInv As DataTable = Me.Model.DatasetCollection("CDNInv").Tables(0)
            If Not IsNothing(dtCDNInv) AndAlso dtCDNInv.Rows.Count > 0 Then
                lblOrgInvNo.Text = myUtils.cStrTN(dtCDNInv.Rows(0)("InvoiceNum"))
                lblOrgInvDate.Text = dtCDNInv.Rows(0)("InvoiceDate")
            End If
        End If
    End Sub
End Class