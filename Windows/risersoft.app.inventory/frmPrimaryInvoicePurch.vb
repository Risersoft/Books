Imports System.ComponentModel
Imports risersoft.app.mxform
Public Class frmPrimaryInvoicePurch
    Inherits frmMax
    Friend fItem As risersoft.app.accounts.frmInvoiceItemGST
    Dim PPFinal As Boolean = False, dvDivision, dvCamp, dvDelCamp, dvGSTSubTyp As DataView
    Dim myViewItemGST, myViewAcc As New clsWinView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGridVoucherList)
        myViewItemGST.SetGrid(Me.UltraGridItemGST)
        myViewAcc.SetGrid(Me.UltraGridACC)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        fItem = New risersoft.app.accounts.frmInvoiceItemGST
        fItem.AddToPanel(Me.UEGB_ItemDetail.Panel, True, System.Windows.Forms.DockStyle.Fill)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPrimaryInvoicePurchModel = Me.InitData("frmPrimaryInvoicePurchModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then

            If myUtils.cValTN(Me.vBag("MatVouchID")) > 0 Then
                GenerateMaterialVoucher(myUtils.cValTN(Me.vBag("MatVouchID")))
            End If

            Me.FormPrepared = (myUtils.cStrTN(myRow("InvoiceTypeCode")).Trim.Length > 0)
            CustVendorVisable(myUtils.cStrTN(myRow("InvoiceTypeCode")))

            If myUtils.cValTN(myRow("campusid")) > 0 Then cmb_campusid.Value = myUtils.cValTN(myRow("campusid"))


            If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC") Then
                If myUtils.cValTN(myRow("CustomerID")) > 0 Then cmb_CustomerID.Value = myUtils.cValTN(myRow("CustomerID"))
                If Not IsNothing(cmb_CustomerID.SelectedRow) Then
                    myRow("Ty") = "G"
                    myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                    myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                    HideControl(myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(myRow("GstInvoiceType")))
                End If
            Else
                If myUtils.cValTN(myRow("VendorID")) > 0 Then cmb_VendorID.Value = myUtils.cValTN(myRow("VendorID"))
                If Not IsNothing(cmb_VendorID.SelectedRow) Then
                    myRow("Ty") = "G"
                    myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                    myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
                    HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(myRow("GstInvoiceType")))
                End If
            End If
            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            HandleCampus()
            HandleBillOf(myUtils.cStrTN(myRow("BillOf")))

            SetOrgInvoiceNum()

            If myUtils.cValTN(Me.vBag("MatVouchID")) > 0 Then
                GenerateGST(myView.mainGrid.myDv.Table.Select)
                myRow("sply_ty") = HandleSupplyType(myUtils.cStrTN(myView.mainGrid.myDv.Table.Rows(0)("TaxAreaType")))
            End If


            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If

            If myView.mainGrid.myDv.Table.Rows.Count > 0 Then
                EnableControls(True)
            End If

            If myUtils.cValTN(Me.vBag("MatVouchID")) > 0 Then
                Me.ActiveControl = txt_InvoiceNum
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

    Private Sub GenerateMaterialVoucher(MatVouchID As Integer)
        Dim rr1() As DataRow = Me.GenerateIDOutput("rmatvouch", MatVouchID).Data.Tables(0).Select
        If rr1.Length > 0 Then
            myRow("DivisionID") = myUtils.cValTN(rr1(0)("DivisionID"))
            If myUtils.cValTN(rr1(0)("InvoiceCampusID")) > 0 Then
                myRow("CampusID") = myUtils.cValTN(rr1(0)("InvoiceCampusID"))
            Else
                myRow("CampusID") = myUtils.cValTN(rr1(0)("CampusID"))
            End If
            myRow("VendorID") = myUtils.cValTN(rr1(0)("VendorID"))
            myRow("InvoiceDate") = myUtils.cDateTN(rr1(0)("VouchDate"), DateTime.MinValue)
            myRow("PostingDate") = myUtils.cDateTN(rr1(0)("VouchDate"), DateTime.MinValue)
            Dim r2 As DataRow = myUtils.CopyOneRow(rr1(0), myView.mainGrid.myDv.Table)
            r2("matvouchid") = MatVouchID
            If myView.mainGrid.myDv.Table.Rows.Count > 0 Then
                EnableControls(True)
                RowFilter()
                CalcTotalGst()
            End If
        End If
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myViewItemGST.PrepEdit(Me.Model.GridViews("ItemGST"))
            myViewAcc.PrepEdit(Me.Model.GridViews("Acc"))

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_DeliveryCampusID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "TY", "", Me.cmb_ty)

            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Private Sub CustVendorVisable(ByVal InvType As String)
        lblCustomer.Visible = False
        cmb_CustomerID.Visible = False
        cmb_VendorID.Visible = False
        lblVendor.Visible = False

        If myUtils.IsInList(myUtils.cStrTN(InvType), "QD", "QC") Then
            If myUtils.IsInList(myUtils.cStrTN(InvType), "QD") Then
                Me.Text = "Quantity Debit Note"
            Else
                Me.Text = "Quantity Credit Note"
            End If

            lblCustomer.Visible = True
            cmb_CustomerID.Visible = True
            UltraPanelOrgInv.Visible = True

            lblInvDate.Text = "Note Date"
            lblInvNo.Text = "Note No."
        Else
            lblVendor.Visible = True
            cmb_VendorID.Visible = True
            UltraPanelOrgInv.Visible = False
        End If
    End Sub

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        cm.EndCurrentEdit()

        If fItem.VSave AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                UltraTabControl1.Tabs("ACC").Selected = True
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        Dim Matvouchid As String = myUtils.MakeCSV(Me.myView.mainGrid.myDv.Table.Select, "MatVouchID")
        myUtils.DeleteRows(myViewItemGST.mainGrid.myDv.Table.Select("MatvouchID Not in (" & Matvouchid & ")"))
        CalcTotalGst()
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            EnableControls(False)
        End If
    End Sub

    Private Sub btnDelAll_Click(sender As Object, e As EventArgs) Handles btnDelAll.Click
        If MsgBox("Are you sure ?" & vbCrLf & "Do you want to Delete All Data ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select)
            myUtils.DeleteRows(myViewItemGST.mainGrid.myDv.Table.Select)
            CalcTotalGst()
            If myView.mainGrid.myDv.Table.Select.Length = 0 Then
                EnableControls(False)
            End If
        End If
    End Sub

    Private Sub EnableControls(ByVal Bool As Boolean)
        Dim AmtTot, AmtWV As Decimal

        cmb_campusid.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
        cmb_CustomerID.ReadOnly = Bool
        cmb_DivisionID.ReadOnly = Bool

        For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select
            AmtTot = AmtTot + myUtils.cValTN(r1("AmountTot"))
            AmtWV = AmtWV + myUtils.cValTN(r1("AmountWV"))
        Next

        txt_AmountTot.Text = myUtils.cValTN(AmtTot)
        myRow("AmountTot") = myUtils.cValTN(AmtTot)
        myRow("AmountWV") = myUtils.cValTN(AmtWV)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If (Not myUtils.NullNot(cmb_campusid.Value)) AndAlso (Not myUtils.NullNot(cmb_DivisionID.Value)) AndAlso ((Not myUtils.NullNot(cmb_VendorID.Value)) OrElse (Not myUtils.NullNot(cmb_CustomerID.Value))) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@invoiceid", myUtils.cValTN(frmIDX), GetType(Integer), False))
            Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(myRow("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(myRow("VendorID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@customerid", myUtils.cValTN(myRow("CustomerID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@matvouchidcsv", myUtils.MakeCSV(Me.myView.mainGrid.myDv.Table.Select, "MatVouchID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@invoicetypecode", "'" & myUtils.cStrTN(myRow("InvoiceTypeCode")) & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(cmb_DivisionID.Value), GetType(Integer), False))
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then Params.Add(New clsSQLParam("@priceprocid", myUtils.cValTN(Me.myView.mainGrid.myDv.Table.Select()(0)("PriceProcID")), GetType(Integer), False))

            Dim rr() As DataRow = Me.AdvancedSelect("matvouch", Params)
            If Not rr Is Nothing AndAlso rr.Length > 0 Then
                For Each r1 As DataRow In rr
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDv.Table)
                    r2("matvouchid") = r1("matvouchid")
                Next
                GenerateGST(rr)
                cmb_sply_ty.Value = HandleSupplyType(myUtils.cStrTN(rr(0)("TaxAreaType")))
            End If

            If myView.mainGrid.myDv.Table.Select.Length > 0 Then
                EnableControls(True)
            End If
        End If
    End Sub

    Private Sub GenerateGST(rr() As DataRow)
        myRow("RCHRG") = myUtils.cStrTN(rr(0)("RCHRG"))
        myRow("Ty") = "G"

        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC") Then
            If Not IsNothing(cmb_CustomerID.SelectedRow) Then
                cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                HideControl(myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_GstInvoiceType.Value))
            End If
        Else
            If Not IsNothing(cmb_VendorID.SelectedRow) Then
                cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_GstInvoiceType.Value))
            End If
        End If
        cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)

        Dim Params1 As New List(Of clsSQLParam)
        Params1.Add(New clsSQLParam("@MatVouchIDCSV", myUtils.MakeCSV(rr, "MatVouchID"), GetType(Integer), True))
        Dim oRet As clsProcOutput = Me.GenerateParamsOutput("matvouchitem", Params1)
        If oRet.Success Then
            For Each r3 As DataRow In oRet.Data.Tables("MatVouchItem").Select("PriceSlabID > 0")
                Dim r4 As DataRow = myUtils.CopyOneRow(r3, myViewItemGST.mainGrid.myDv.Table)
                r4("CAMT") = Math.Round(SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "CGST", "TotalValueCalc"), 2)
                r4("SAMT") = Math.Round(SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "SGST", "TotalValueCalc"), 2)
                r4("IAMT") = Math.Round(SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "IGST", "TotalValueCalc"), 2)
                r4("CSAMT") = Math.Round(SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "CESSGST", "TotalValueCalc"), 2)
                r4("RT") = Math.Round(SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "IGST", "PerValue") + SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "SGST", "PerValue") + SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "CGST", "PerValue"), 2)

                If myUtils.IsInList(myUtils.cStrTN(r3("TaxCredit")), "Y") Then
                    r4("tx_c") = r4("CAMT")
                    r4("tx_s") = r4("SAMT")
                    r4("tx_i") = r4("IAMT")
                    r4("tx_cs") = r4("CSAMT")
                Else
                    r4("tx_c") = DBNull.Value
                    r4("tx_s") = DBNull.Value
                    r4("tx_i") = DBNull.Value
                    r4("tx_cs") = DBNull.Value
                End If


                If myUtils.cValTN(r4("IAMT")) > 0 Then
                    r4("TXVAL") = Math.Round(SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "IGST", "AmountBase"), 2)
                Else
                    r4("TXVAL") = Math.Round(SetColumnValue(oRet.Data.Tables("PriceElemCalc"), myUtils.cValTN(r4("MatVouchItemID")), "SGST", "AmountBase"), 2)
                End If

                If myUtils.IsInList(myUtils.cStrTN(r3("ClassType")), "M") Then
                    r4("ty") = "G"
                ElseIf myUtils.IsInList(myUtils.cStrTN(r3("ClassType")), "S") Then
                    r4("ty") = "S"
                ElseIf myUtils.IsInList(myUtils.cStrTN(r3("ClassType")), "A") Then
                    r4("ty") = "G"
                End If

                If (Not myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC")) AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("VendorType").Value), "EM") Then
                    r4("GstTaxType") = "UnReg"
                End If
            Next
        End If
    End Sub

    Public Shared Function SetColumnValue(dt As DataTable, MatVouchItemID As Integer, ElemCode As String, ValueField As String) As Decimal
        Dim ElemValue As Decimal = 0
        Dim rr1() As DataRow = dt.Select("CIDValue = " & MatVouchItemID & "  and elemCode = '" & ElemCode & "'")
        If rr1.Length > 0 Then
            ElemValue = myUtils.cValTN(rr1(0)(ValueField))
        End If

        Return ElemValue
    End Function

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        HandleCampus()
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
        dvDelCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
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

    Private Function HandleSupplyType(TaxAreaType As String)
        Dim SuplyType As String = ""
        If myUtils.IsInList(myUtils.cStrTN(TaxAreaType), "SAME") Then
            SuplyType = "INTRA"
        Else
            SuplyType = "INTER"
        End If
        Return SuplyType
    End Function

    Private Sub btnSelectOrg_Click(sender As Object, e As EventArgs) Handles btnSelectOrg.Click
        If myUtils.cValTN(cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(cmb_CustomerID.Value) > 0 AndAlso myUtils.cValTN(cmb_DivisionID.Value) > 0 Then
            cm.EndCurrentEdit()
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@CustomerID", myUtils.cValTN(myRow("CustomerID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CampusID", myUtils.cValTN(myRow("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(myRow("DivisionID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@InvoiceDate", Format(myRow("InvoiceDate"), "yyyy-MMM-dd"), GetType(Date), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("invoice", Params)
            If (Not IsNothing(rr1)) AndAlso rr1.Length > 0 Then
                myRow("CDNInvoiceID") = myUtils.cValTN(rr1(0)("InvoiceID"))
                lblOrgInvNo.Text = myUtils.cStrTN(rr1(0)("InvoiceNum"))
                lblOrgInvDate.Text = myUtils.cStrTN(Format(rr1(0)("InvoiceDate"), "dd-MMM-yyyy"))
            End If
        End If
    End Sub

    Private Sub UltraGridItemGST_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemGST.AfterRowActivate
        Me.InitError()
        myViewItemGST.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewItemGST.mainGrid.myGrid.ActiveRow)
        fItem.PrepFormRow(r1)
        fItem.HandleZeroRated(myUtils.cValTN(r1("RT")), False)
        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItemGST_BeforeRowDeactivate(sender As Object, e As CancelEventArgs) Handles UltraGridItemGST.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub CalcTotalGst()
        txtIGST.Value = myUtils.cValTN(myViewItemGST.Model.MainGrid.GetColSum("IAMT"))
        txtCGST.Value = myUtils.cValTN(myViewItemGST.Model.MainGrid.GetColSum("CAMT"))
        txtSGST.Value = myUtils.cValTN(myViewItemGST.Model.MainGrid.GetColSum("SAMT"))
    End Sub

    Private Sub UltraGridVoucherList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridVoucherList.AfterRowActivate
        RowFilter()
        CalcTotalGst()
    End Sub

    Private Sub RowFilter()
        myView.mainGrid.myGrid.UpdateData()
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            myViewItemGST.mainGrid.myDv.RowFilter = "MatVouchID = " & myView.mainGrid.myGrid.ActiveRow.Cells("MatVouchID").Value
        End If
    End Sub
End Class