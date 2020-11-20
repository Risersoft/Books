Imports risersoft.app.mxform

Public Class frmInvoiceRateNote
    Inherits frmMax
    Friend myViewVouch As New clsWinView, fItem As frmInvoiceMVItem
    Dim dtMatVouchItem As DataTable, dv, dv2, dvDivision, dvCamp, dvDelCamp, dvGSTSubTyp As DataView
    Dim oSort As clsWinSort

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        myViewVouch.SetGrid(Me.UltraGridVoucherList)
        myView.SetGrid(UltraGridItemList)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)

        fItem = New frmInvoiceMVItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.Enabled = False
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoiceRateNoteModel = Me.InitData("frmInvoiceRateNoteModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            dv = myWinSQL.AssignCmb(Me.dsCombo, "InvoiceTypeCode", "", Me.cmb_InvoiceTypeCode, , 1)
            dv2 = myWinSQL.AssignCmb(Me.dsCombo, "BillOf", "", Me.cmb_BillOf, , 1)
            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)

            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "TY", "", Me.cmb_ty)

            cmb_BillOf.Value = myUtils.cStrTN(myRow("BillOf"))
            HandleInvoiceType(myUtils.cStrTN(myRow("InvoiceTypeCode")))

            If myViewVouch.mainGrid.myDv.Count > 0 Then
                EnableControls(True)
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
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myView.mainGrid.HighlightRows()
            Me.UltraGridItemList.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChangeOrLostFocus
            myViewVouch.PrepEdit(Me.Model.GridViews("VouchList"))

            oSort = New clsWinSort(myView, btnUp, btnDown, Nothing, "SerialNum")
            oSort.renumber()
            fItem.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_DeliveryCampusID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)

            Me.CtlPricing1.InitData("InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceMVItemId", Me.dsForm.Tables("InvoiceMVItem"), fItem.CtlPricingChild1)
            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
            WinFormUtils.ValidateComboValue(cmb_DeliveryCampusID, myUtils.cValTN(myRow("DeliveryCampusID")))
            WinFormUtils.ValidateComboValue(cmb_VendorID, myUtils.cValTN(myRow("VendorID")))
            HandleCampus()
            WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

            If Not IsNothing(cmb_VendorID.SelectedRow) Then
                myRow("GstInvoiceType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
                myRow("GstInvoiceSubType") = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(myRow("GstInvoiceType")), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)

                HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value))
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Me.cm.EndCurrentEdit()
        If (Not myUtils.NullNot(cmb_campusid.Value)) AndAlso (Not myUtils.NullNot(cmb_DivisionID.Value)) AndAlso (Not myUtils.NullNot(cmb_VendorID.Value)) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@invoiceid", myUtils.cValTN(frmIDX), GetType(Integer), False))
            Params.Add(New clsSQLParam("@invoicetypecode", "'" & myUtils.cStrTN(cmb_InvoiceTypeCode.Value) & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(myRow("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(myRow("VendorID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@matvouchidcsv", myUtils.MakeCSV(Me.myViewVouch.mainGrid.myDv.Table.Select, "MatVouchID"), GetType(Integer), True))
            If Me.myViewVouch.mainGrid.myDv.Count > 0 AndAlso (myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("AllowMultiOrigInv").Value) = False) Then Params.Add(New clsSQLParam("@pinvoiceid", myUtils.cValTN(Me.myViewVouch.mainGrid.myDv(0)("PInvoiceID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(myRow("DivisionID")), GetType(Integer), False))

            Dim rr() As DataRow = Me.AdvancedSelect("matvouch", Params)
            If Not rr Is Nothing AndAlso rr.Length > 0 Then
                For Each r1 As DataRow In rr
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewVouch.mainGrid.myDv.Table)
                Next
                myRow("CDNInvoiceID") = myUtils.cValTN(rr(0)("PInvoiceID"))

                GenerateMatVouchItems(myUtils.MakeCSV(rr, "MatVouchID"))
                oSort.renumber()
                If myViewVouch.mainGrid.myGrid.Rows.Count > 0 Then
                    EnableControls(True)
                End If

                Dim oRet As clsProcOutput = Me.GenerateIDOutput("PurOrder", myUtils.cValTN(Me.myRow("CDNInvoiceID")))
                If oRet.Success Then
                    If oRet.Description.Trim.Length > 0 Then
                        myRow("OtherRef") = myUtils.cStrTN(oRet.Description)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub GenerateMatVouchItems(MatVouchIDCSV As String)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@matvouchidcsv", MatVouchIDCSV, GetType(Integer), True))
        Dim oRet As clsProcOutput = Me.GenerateParamsOutput("generate", Params)
        If oRet.Success Then
            Dim fG As New frmGrid
            fG.myView.myMode = EnumViewMode.acSelectM : fG.myView.MultiSelect = True
            fG.myView.mainGrid.BindView(oRet.Data,, 1)
            fG.myView.mainGrid.QuickConf("", True, "1-2-1-1-1-1-1-1", True)
            fG.Size = New Drawing.Size(850, 600)
            Do
                If fG.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Dim rr1() As DataRow = fG.myView.mainGrid.myDv.Table.Select("sysincl=1")
                    If rr1.Length = 0 Then
                        MsgBox("Please Select Items")
                    Else
                        For Each r1 In fG.myView.mainGrid.myDv.Table.Select("sysincl=1")
                            myUtils.CopyOneRow(r1, Me.dsForm.Tables("InvoiceMVItem"))
                        Next
                        Exit Do
                    End If
                Else
                    Exit Do
                End If
            Loop
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myViewVouch.mainGrid.ButtonAction("del")

        Dim Matvouchid As String = myUtils.MakeCSV(Me.myViewVouch.mainGrid.myDv.Table.Select, "MatVouchID")
        myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select("MatvouchID Not in (" & Matvouchid & ")"))
        oSort.renumber()
        If myViewVouch.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            EnableControls(False)
        End If
    End Sub

    Private Sub btnDelAll_Click(sender As Object, e As EventArgs) Handles btnDelAll.Click
        If MsgBox("Are you sure ?" & vbCrLf & "Do you want to Delete All Data ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            myUtils.DeleteRows(myViewVouch.mainGrid.myDv.Table.Select)
            myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select)
            oSort.renumber()
            If myViewVouch.mainGrid.myDv.Table.Select.Length = 0 Then
                CtlPricing1.oProc.UpdatePricingTable(Nothing)
                EnableControls(False)
            End If
            fItem.Enabled = True
        End If
    End Sub

    Private Sub UltraGridVoucherList_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridVoucherList.AfterCellUpdate
        For Each r1 As DataRow In myViewVouch.mainGrid.myDv.Table.Select
            If r1("Sysincl") = True Then
                For Each r2 As DataRow In dtMatVouchItem.Select("MatVouchID = " & myUtils.cValTN(r1("MatVouchID")))
                    Dim r3 As DataRow = myUtils.CopyOneRow(r2, Me.dsForm.Tables("InvoiceMVItem"))
                Next
            Else
                Dim rr2() As DataRow = Me.dsForm.Tables("InvoiceMVItem").Select("MatVouchID = " & myViewVouch.mainGrid.myGrid.ActiveRow.Cells("MatVouchID").Value)
                myUtils.DeleteRows(rr2)
            End If
        Next
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)

        fItem.CtlPricingChild1.HandleChildRowSelect()
        fItem.Enabled = True
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

    Private Sub cmb_InvoiceType_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceTypeCode.Leave, cmb_InvoiceTypeCode.AfterCloseUp
        HandleInvoiceType(cmb_InvoiceTypeCode.Value)
    End Sub

    Private Sub HandleInvoiceType(InvoiceTypeCode As String)
        If myUtils.IsInList(myUtils.cStrTN(InvoiceTypeCode), "SB") Then
            myRow("BillOf") = "P"
            cmb_BillOf.Value = "P"
            cmb_BillOf.ReadOnly = True
        Else
            cmb_BillOf.ReadOnly = False
        End If
        HandleBillOf(myUtils.cStrTN(cmb_BillOf.Value))
    End Sub

    Private Sub EnableControls(ByVal Bool As Boolean)
        cmb_campusid.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
        cmb_DivisionID.ReadOnly = Bool
        cmb_InvoiceTypeCode.ReadOnly = Bool
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = "", Filter, StrFilter As String
        If Not IsNothing(cmb_campusid.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)

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


            CtlPricing1.SetProcTypeFilter(Filter)
        Else
            CtlPricing1.SetProcTypeFilter("1=0")
        End If
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        ProcTypeFilter()
        HandleCampus()
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        ProcTypeFilter()
        cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
        cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
        HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value))
    End Sub

    Private Sub HandleCampus()
        If Not IsNothing(cmb_campusid.SelectedRow) Then
            dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, Nothing)

            If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
            If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
        End If
    End Sub

    Private Sub dt_InvoiceDate_Leave(sender As Object, e As EventArgs) Handles dt_InvoiceDate.Leave, dt_InvoiceDate.AfterCloseUp
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        dvDelCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
    End Sub

    Private Sub HideControl(TaxAreaCode As String)
        Dim Visible As Boolean
        If myUtils.IsInList(TaxAreaCode, "IMP") Then
            Visible = True
        Else
            Visible = False
        End If
        cmb_ty.Visible = Visible
        lblTY.Visible = Visible
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
End Class