Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform

Public Class frmInvoiceThirdParty
    Inherits frmMax
    Friend myViewVouch As New clsWinView, fItem As frmInvoiceItem
    Dim dvDivision, dvCamp, dvGSTSubTyp As DataView

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

        fItem = New frmInvoiceItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.Enabled = False
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoiceThirdPartyModel = Me.InitData("frmInvoiceThirdPartyModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If myUtils.cValTN(Me.vBag("MatVouchID")) > 0 Then
                GenerateMaterialVoucher(myUtils.cValTN(Me.vBag("MatVouchID")))
            End If
            myRow("sply_ty") = HandlePricing()

            If myViewVouch.mainGrid.myDS.Tables(0).Select.Length > 0 Then EnableControls(True)
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then cmb_VendorID.ReadOnly = True

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
            myViewVouch.PrepEdit(Me.Model.GridViews("VouchList"))

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceType", "", Me.cmb_GstInvoiceType)
            dvGSTSubTyp = myWinSQL.AssignCmb(Me.dsCombo, "GSTInvoiceSubType", "", Me.cmb_GSTInvoiceSubType,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "SupplyType", "", Me.cmb_sply_ty)
            myWinSQL.AssignCmb(Me.dsCombo, "TY", "", Me.cmb_ty)

            fItem.BindModel(NewModel)
            fItem.fItem.BindModel(NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            Me.CtlPricing1.InitData("InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", Me.dsForm.Tables("InvoiceItem"), fItem.CtlPricingChild1)
            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
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

    Private Sub GenerateMaterialVoucher(MatVouchID As Integer)
        Dim rr1() As DataRow = Me.GenerateIDOutput("rmatvouch", MatVouchID).Data.Tables(0).Select
        If rr1.Length > 0 Then
            myRow("DivisionID") = myUtils.cValTN(rr1(0)("DivisionID"))
            If myUtils.cValTN(rr1(0)("InvoiceCampusID")) > 0 Then
                myRow("CampusID") = myUtils.cValTN(rr1(0)("InvoiceCampusID"))
            Else
                myRow("CampusID") = myUtils.cValTN(rr1(0)("CampusID"))
            End If

            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
            Dim r2 As DataRow = myUtils.CopyOneRow(rr1(0), myViewVouch.mainGrid.myDS.Tables(0))


            r2("PVendorName") = myUtils.cStrTN(rr1(0)("PartyName"))
            r2("PInvoiceNum") = myUtils.cStrTN(rr1(0)("InvoiceNum"))
            r2("PInvoiceDate") = myUtils.cStrTN(rr1(0)("InvoiceDate"))
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

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Me.cm.EndCurrentEdit()
        If Not myUtils.NullNot(cmb_campusid.Value) AndAlso Not myUtils.NullNot(cmb_DivisionID.Value) AndAlso Not myUtils.NullNot(cmb_VendorID.Value) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@invoiceid", myUtils.cValTN(frmIDX), GetType(Integer), False))
            Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(myRow("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@matvouchidcsv", myUtils.MakeCSV(Me.myViewVouch.mainGrid.myDS.Tables(0).Select, "MatVouchID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(myRow("DivisionID")), GetType(Integer), False))
            Dim rr() As DataRow = Me.AdvancedSelect("matvouch", Params)
            If Not rr Is Nothing AndAlso rr.Length > 0 Then
                For Each r1 As DataRow In rr
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewVouch.mainGrid.myDS.Tables(0))
                    r2("PVendorName") = myUtils.cStrTN(r1("PartyName"))
                    r2("PInvoiceNum") = myUtils.cStrTN(r1("InvoiceNum"))
                    r2("PInvoiceDate") = myUtils.cStrTN(r1("InvoiceDate"))
                Next
            End If

            If myViewVouch.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                EnableControls(True)
            End If
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myViewVouch.mainGrid.ButtonAction("del")
        If myViewVouch.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            EnableControls(False)
        End If
    End Sub

    Private Sub btnDelAll_Click(sender As Object, e As EventArgs) Handles btnDelAll.Click
        If MsgBox("Are you sure ?" & vbCrLf & "Do you want to Delete All Data ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
            myUtils.DeleteRows(myViewVouch.mainGrid.myDS.Tables(0).Select)
            If myViewVouch.mainGrid.myDS.Tables(0).Select.Length = 0 Then
                EnableControls(False)
            End If
        End If
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDV.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")

            If myView.mainGrid.myDv.Table.Select.Length > 0 Then cmb_VendorID.ReadOnly = True
        End If
    End Sub

    Private Sub btnDelItem_Click(sender As Object, e As EventArgs) Handles btnDelItem.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
            fItem.Enabled = False
            cmb_VendorID.ReadOnly = False
        End If
    End Sub

    Private Sub EnableControls(ByVal Bool As Boolean)
        cmb_campusid.ReadOnly = Bool
        cmb_DivisionID.ReadOnly = Bool
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = "", Filter, StrFilter As String
        If Not IsNothing(cmb_campusid.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)
            If String.IsNullOrEmpty(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value)) And (Not myUtils.IsInList(myUtils.cStrTN(PartyTaxAreaCode), "IMP")) Then
                StrFilter = "ProcType = 'PS' and RCHRG = 'Y'"
            Else
                StrFilter = "ProcType = 'PS' and isNull(IsUnreg,0) = 0"
            End If

            Filter = risersoft.app.mxform.myFuncs.PriceProcFilter(myRow("InvoiceDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
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
        If Not IsNothing(cmb_campusid.SelectedRow) AndAlso (Not IsNothing(cmb_VendorID.SelectedRow)) Then
            cmb_GstInvoiceType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceTypePurch(myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value), myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value), myUtils.cStrTN(myRow("TY")))
            cmb_GSTInvoiceSubType.Value = risersoft.app.mxform.myFuncs.SetGSTInvoiceSubType(myUtils.cStrTN(myRow("GstInvoiceSubType")), myUtils.cStrTN(myRow("InvoiceTypeCode")), myUtils.cStrTN(cmb_GstInvoiceType.Value), dvGSTSubTyp.RowFilter, cmb_GSTInvoiceSubType.ReadOnly)
            HideControl(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value))
        End If
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
End Class