Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmODNote
    Inherits frmMax
    Friend fItem As frmODNoteItem, fItemSP As frmODNoteSpare, myViewSpare As New clsWinView
    Dim dvInvoiceType, dvCustomer, DviewItem, DviewSp, dvDivision, dvCamp, dvInvCamp, dvEmp, dvProjectSite As DataView, Accessories As Boolean = False

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        myView.SetGrid(Me.UltraGridItemList)
        myViewSpare.SetGrid(UltraGridSpares)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)

        fItem = New frmODNoteItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.fPackList.fParentMat = Me
        fItem.fItemSelect.InitPanel(fItem.UltraTabControl1.Tabs("Quantity").TabPage, fItem.UltraTabControl1.Tabs("MvtCode").TabPage, Me)
        fItem.Enabled = False

        fItemSP = New frmODNoteSpare
        fItemSP.AddToPanel(Me.UltraExpandableGroupBoxPanel4, True, System.Windows.Forms.DockStyle.Fill)
        fItemSP.fMat = Me
        fItemSP.fPackList.fParentMat = Me
        fItemSP.Enabled = False
    End Sub

    Private Sub ReadOnlyCtl(Bool As Boolean)
        cmb_TaxType.ReadOnly = Bool
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmODNoteModel = Me.InitData("frmODNoteModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If frmMode = EnumfrmMode.acAddM AndAlso cmb_TaxType.Rows.Count = 1 Then myRow("TaxType") = myUtils.cStrTN(cmb_TaxType.Rows(0).Cells("CodeWord").Value)

            If myUtils.cValTN(myRow("DispatchID")) > 0 Then   ' Only For ('SCSP','SCSV','SCUB','TRSO')
                Me.FormPrepared = (myUtils.cValTN(myRow("SalesOrderID")) > 0)
                dvCustomer.RowFilter = "MainPartyID = " & myUtils.cValTN(myRow("MainPartyID")) & " or PartyType = 'A'"

                txt_ConsigneePrefix.ReadOnly = True
                txt_DeliveryTo.ReadOnly = True
                cmb_ConsigneeID.ReadOnly = True
                cmb_POSTaxAreaID.ReadOnly = True
            Else
                Me.FormPrepared = True
            End If

            cmb_ChallanType.Value = myUtils.cStrTN(myRow("ChallanType"))
            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "SCSV") Then
                cmb_CustomerID.ReadOnly = False
                btnAddSerial.Visible = False
                btnAddAsc.Visible = False
            End If


            If Not IsNothing(cmb_ChallanType.SelectedRow) Then HandleChallanType(myUtils.cStrTN(myRow("ChallanType")), myUtils.cStrTN(cmb_ChallanType.SelectedRow.Cells("Tag").Value))
            If myView.mainGrid.myGrid.Rows.Count > 0 Then
                ReadOnlyCtl(True)
            End If

            AssignCmbODNSpare()
            fItem.UltraTabControl1.Tabs("Material").Selected = True

            ReadOnlyCtlSpare()
            If myUtils.cStrTN(myRow("ChallanNum")).Trim.Length > 0 Then
                btnOK.Enabled = False
                btnSave.Enabled = False
            End If
        End If
        Return Me.FormPrepared
    End Function

    Public Sub AssignCmbODNSpare()
        fItem.dvODNSpare = myWinSQL.AssignCmb(Me.dsForm, "ODNoteSpare", "", fItem.cmb_ODNoteSpareID,, 2, True)
    End Sub

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Items"))
            myViewSpare.PrepEdit(Me.Model.GridViews("Spare"))

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvInvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_InvoiceCampusID,, 2)
            dvProjectSite = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_ProjectCampusID,, 2)

            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            dvCustomer = myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID, , 2)
            myWinSQL.AssignCmb(Me.dsCombo, "ChallanType", "", Me.cmb_ChallanType)
            myWinSQL.AssignCmb(Me.dsCombo, "TaxType", "", Me.cmb_TaxType)
            dvInvoiceType = myWinSQL.AssignCmb(Me.dsCombo, "TaxInvoiceType", "", Me.cmb_TaxInvoiceType, , 2)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            dvEmp = myWinSQL.AssignCmb(Me.dsCombo, "User", "", Me.cmb_CheckedByID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Party", "", Me.cmb_ConsigneeID)
            myWinSQL.AssignCmb(Me.dsCombo, "POS", "", Me.cmb_POSTaxAreaID)

            fItemSP.BindModel(NewModel)
            fItem.BindModel(NewModel)
            fItem.fSoItemSelect.InitPanel(myUtils.cValTN(myRow("SalesOrderID")), myUtils.cValTN(myRow("DispatchID")), Me.fItem, Me, NewModel)
            fItemSP.fSoItemSelect.InitPanel(myUtils.cValTN(myRow("SalesOrderID")), myUtils.cValTN(myRow("DispatchID")), Me.fItemSP, Me, NewModel)


            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), risersoft.app.mxform.myFuncs.ChallanStr()) Then
                fItemSP.fCostAssign.InitPanel(Me.fItemSP, Me, NewModel, "CostLot", "CostWBS", "CostCenter")
            Else
                fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")
            End If

            PricingInItData(myUtils.cStrTN(myRow("ChallanType")))
            Return True
        End If
        Return False
    End Function

    Private Sub PricingInItData(ChallanType As String)
        If myUtils.IsInList(myUtils.cStrTN(ChallanType), "SCSP", "SCSV", "TRSO", "SCSTB", "RCWB", "TRJB") Then
            Me.CtlPricing1.InitData("ODNoteID", myUtils.cValTN(frmIDX), "ChallanDate", "ODNoteSpareID", Me.dsForm.Tables("ODNoteSpare"), fItemSP.CtlPricingChild1)
            CtlPricing1.Enabled = True
        Else
            Me.CtlPricing1.InitData("ODNoteID", myUtils.cValTN(frmIDX), "ChallanDate", "ODNoteItemID", Me.dsForm.Tables("ODNoteItem"), fItem.CtlPricingChild1)
        End If

        HandleDate(myUtils.cDateTN(myRow("ChallanDate"), DateTime.MinValue))
        WinFormUtils.ValidateComboValue(cmb_campusid, myUtils.cValTN(myRow("campusid")))
        WinFormUtils.ValidateComboValue(cmb_InvoiceCampusID, myUtils.cValTN(myRow("InvoiceCampusID")))
        WinFormUtils.ValidateComboValue(cmb_CustomerID, myUtils.cValTN(myRow("CustomerID")))
        WinFormUtils.ValidateComboValue(cmb_VendorID, myUtils.cValTN(myRow("VendorID")))
        HandleCampus()
        WinFormUtils.ValidateComboValue(cmb_DivisionID, myUtils.cValTN(myRow("DivisionID")))

        WinFormUtils.ValidateComboValue(cmb_ConsigneeID, myUtils.cValTN(myRow("ConsigneeID")))
        myRow("POSTaxAreaID") = HandleConsigneeID()
        WinFormUtils.ValidateComboValue(cmb_POSTaxAreaID, myUtils.cValTN(myRow("POSTaxAreaID")))

        ProcTypeFilter()
    End Sub

    Private Sub HandleChallanType(ChallanType As String, InvType As String)
        UltraTabControl2.Tabs("Spares").Visible = False
        If myView.mainGrid.myGrid.Rows.Count = 0 Then cmb_TaxInvoiceType.Value = DBNull.Value
        EnableBtn((Not myUtils.IsInList(myUtils.cStrTN(myRow("TaxInvoiceType")), "")) AndAlso myUtils.cValTN(myRow("CampusID")) > 0 AndAlso (Not myUtils.IsInList(myUtils.cStrTN(myRow("TaxType")), "")))
        If Not myUtils.IsInList(myUtils.cStrTN(ChallanType), "") Then
            PricingInItData(ChallanType)
            dvInvoiceType.RowFilter = "Tag = '" & InvType & "'"
            If frmMode = EnumfrmMode.acAddM Then
                If myUtils.IsInList(InvType, "I") Then
                    myRow("TaxInvoiceType") = "TI"
                Else
                    If cmb_TaxInvoiceType.Rows.Count = 1 Then myRow("TaxInvoiceType") = myUtils.cStrTN(cmb_TaxInvoiceType.Rows(0).Cells("CodeWord").Value)
                End If
            End If

            fItem.cmb_ODNoteSpareID.Visible = False
            fItem.lblSpares.Visible = False
            BtnAddServices.Visible = False
            btnCopyFrom.Visible = False
            If myUtils.IsInList(myUtils.cStrTN(ChallanType), "SCSP", "TRSO", "SCSTB", "RCWB", "TRJB") Then
                UltraTabControl2.Tabs("Spares").Visible = True
                UltraTabControl2.Tabs("Spares").Selected = True
                fItem.cmb_ODNoteSpareID.Visible = True
                fItem.lblSpares.Visible = True
                fItem.UltraTabControl1.Tabs("SalesOrder").Visible = False
                fItem.UltraTabControl1.Tabs("PackingItem").Visible = False
                fItem.txt_BasicRate.Visible = False
            ElseIf myUtils.IsInList(myUtils.cStrTN(ChallanType), "SCSV") Then
                UltraTabControl2.Tabs("Spares").Text = "Items"
                UltraTabControl2.Tabs("Spares").Visible = True
                UltraTabControl2.Tabs("Spares").Selected = True
                UltraTabControl2.Tabs("Items").Visible = False
                BtnAddServices.Visible = True
            ElseIf myUtils.IsInList(myUtils.cStrTN(ChallanType), "SCUB") Then
                UltraTabControl1.Tabs("Pricing").Visible = False
                fItem.UltraTabControl1.Tabs("SalesOrder").Visible = False
            ElseIf myUtils.IsInList(myUtils.cStrTN(ChallanType), "RC", "RV") Then
                UltraTabControl1.Tabs("Pricing").Visible = False
            End If

            fItem.UltraTabControl1.Tabs("PricingItem").Visible = Not myUtils.IsInList(myUtils.cStrTN(ChallanType), "RC", "RV", "SCUB", "SCSV", "SCSP", "TRSO", "SCSTB", "RCWB", "TRJB")
            fItem.UltraTabControl1.Tabs("Cost").Visible = Not myUtils.IsInList(myUtils.cStrTN(ChallanType), "RC", "RV", "SCUB", "SCSV", "SCSP", "TRSO", "SCSTB", "RCWB", "TRJB")

            If myUtils.IsInList(myUtils.cStrTN(ChallanType), "RCWB") Then
                fItemSP.UltraTabControl1.Tabs("PricingSP").Visible = False
                fItemSP.UltraTabControl1.Tabs("Cost").Visible = False
                UltraTabControl1.Tabs("Pricing").Visible = False
                btnCopyFrom.Visible = True
                btnAddSerial.Visible = False
                btnAddSpares.Visible = False
                BtnAddServices.Visible = False
                btnAddAsc.Visible = False
            End If

            If myUtils.IsInList(myUtils.cStrTN(ChallanType), "SCSTB", "TRJB") Then
                btnAddSerial.Visible = False
                btnAddSpares.Visible = False
            End If
        End If

        fItem.lblNature.Visible = myUtils.IsInList(myUtils.cStrTN(ChallanType), "TRJW", "TRSO", "TRWS")
        fItem.txt_Nature.Visible = myUtils.IsInList(myUtils.cStrTN(ChallanType), "TRJW", "TRSO", "TRWS")
        fItemSP.lblNature.Visible = myUtils.IsInList(myUtils.cStrTN(ChallanType), "TRJW", "TRSO", "TRWS")
        fItemSP.txt_Nature.Visible = myUtils.IsInList(myUtils.cStrTN(ChallanType), "TRJW", "TRSO", "TRWS")
    End Sub

    Public Overrides Function VSave() As Boolean
        Dim Bool As Boolean = True, Bool2 As Boolean = True
        Me.InitError()
        VSave = False

        cm.EndCurrentEdit()

        If (Not myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "RC", "RV", "SCUB", "RCWB")) Then
            If myUtils.cValTN(myRow("PriceSlabID")) > 0 AndAlso (Not CtlPricing1.HasRowSelected) Then
                WinFormUtils.AddError(Me.CtlPricing1, "Please Select Pricing")
            End If
        End If

        If myUtils.IsInList(myUtils.cStrTN(cmb_ChallanType.Value), "SCSP", "SCSV", "TRSO", "SCSTB", "RCWB", "TRJB") Then
            Bool = fItemSP.VSave()
        End If

        If Not myUtils.IsInList(myUtils.cStrTN(cmb_ChallanType.Value), "SCSV") Then
            Bool2 = fItem.VSave
        End If

        If (myViewSpare.mainGrid.myDv.Count = 0 OrElse Bool = True) AndAlso (myView.mainGrid.myDv.Count = 0 OrElse Bool2 = True) AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

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
        If fItem.PrepForm(r1) Then
            DviewItem = fItem.fSoItemSelect.myView.mainGrid.myDv
            If Not IsNothing(DviewItem) Then
                If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then DviewItem.RowFilter = "OdNoteitemID = " & myView.mainGrid.myGrid.ActiveRow.Cells("OdNoteitemID").Value
            End If

            fItem.CtlPricingChild1.HandleChildRowSelect()
            fItem.Enabled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            If myView.mainGrid.myDv.Count > 0 Then
                ReadOnlyCtl(True)
            End If
            fItem.fItemSelect.Focus()
        End If
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")

        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
            ReadOnlyCtl(False)

            cmb_VendorID.ReadOnly = True
            cmb_CustomerID.ReadOnly = True
            cmb_CustomerID.Value = DBNull.Value
            cmb_VendorID.Value = DBNull.Value
            fItem.Enabled = False
        End If
    End Sub

    Private Sub btnAddSpares_Click(sender As Object, e As EventArgs) Handles btnAddSpares.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@salesorderid", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@dispatchid", myUtils.cValTN(myRow("dispatchid")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@sospareid", myUtils.MakeCSV(dsForm.Tables("ODNoteSpare").Select, "sospareid"), GetType(Integer), True))
        Params.Add(New clsSQLParam("@ChallanType", "'" & myUtils.cStrTN(myRow("ChallanType")) & "'", GetType(String), False))
        If myUtils.cValTN(myRow("ProjectCampusID")) > 0 Then Params.Add(New clsSQLParam("@PidUnitID", myUtils.cValTN(cmb_ProjectCampusID.SelectedRow.Cells("PidUnitID").Value), GetType(Integer), False))
        Dim rr() As DataRow = Me.AdvancedSelect("spares", Params)

        If Not IsNothing(rr) Then
            Dim Params1 As New List(Of clsSQLParam)
            Params1.Add(New clsSQLParam("@SospareIdCSV", myUtils.MakeCSV(rr, "sospareid"), GetType(Integer), True))
            Params1.Add(New clsSQLParam("@DispatchID", myUtils.cValTN(myRow("DispatchID")), GetType(Integer), False))
            Dim dt As DataTable = Me.GenerateParamsOutput("sospareitem", Params1).Data.Tables(0)

            If Not rr Is Nothing AndAlso rr.Length > 0 Then
                For Each r1 As DataRow In rr
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewSpare.mainGrid.myDv.Table)
                    'TransType copied
                    r2("ClassType") = "M"
                    r2("ItemDescrip") = myUtils.cStrTN(r1("SpareName"))
                    r2("ItemType") = "SP"
                    r2("UnitPrice") = myUtils.cValTN(r1("UnitPrice"))
                    r2("PIDUnitID") = DBNull.Value
                    Dim rr1() As DataRow = dt.Select("SoSpareID = " & myUtils.cValTN(r1("SoSpareID")) & "")
                    r2("QtyRate") = myUtils.cValTN(rr1(0)("Qty"))
                Next
                fItemSP.Focus()
                fItemSP.Enabled = True
                ReadOnlyCtlSpare()
            End If
        End If
    End Sub

    Private Sub btnRate_Click(sender As Object, e As EventArgs) Handles btnRate.Click
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewSpare.mainGrid.myGrid.ActiveRow)
        If Not IsNothing(r1) Then
            fItemSP.CtlPricingChild1.SetElementField("bp", "ValuePerQty", myUtils.cValTN(r1("UnitPrice")))
        End If
    End Sub

    Private Sub UltraGridSpares_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridSpares.BeforeRowDeactivate
        If fItemSP.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub UltraGridSpares_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridSpares.AfterRowActivate
        Me.InitError()
        myViewSpare.mainGrid.myGrid.UpdateData()
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewSpare.mainGrid.myGrid.ActiveRow)
        If Accessories = True Then
            r1("ItemType") = "AC"
            r1("ClassType") = "M"
        End If
        If fItemSP.PrepForm(r1) Then
            DviewSp = fItemSP.fSoItemSelect.myView.mainGrid.myDv
            If Not IsNothing(DviewSp) Then
                If Not IsNothing(myViewSpare.mainGrid.myGrid.ActiveRow) Then DviewSp.RowFilter = "ODNoteSpareID = " & myViewSpare.mainGrid.myGrid.ActiveRow.Cells("ODNoteSpareID").Value
            End If
            fItemSP.CtlPricingChild1.HandleChildRowSelect()
            fItemSP.Enabled = True
        End If
    End Sub

    Private Sub BtnAddServices_Click(sender As Object, e As EventArgs) Handles BtnAddServices.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@salesorderid", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@soserviceid", myUtils.MakeCSV(dsForm.Tables("ODNoteSpare").Select, "soserviceid"), GetType(Integer), True))
        If myUtils.cValTN(myRow("ProjectCampusID")) > 0 Then Params.Add(New clsSQLParam("@PidUnitID", myUtils.cValTN(cmb_ProjectCampusID.SelectedRow.Cells("PidUnitID").Value), GetType(Integer), False))
        Dim rr() As DataRow = Me.AdvancedSelect("services", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In rr
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewSpare.mainGrid.myDv.Table)
                'TransType copied
                r2("ClassType") = "S"
                r2("ItemDescrip") = myUtils.cStrTN(r1("ServiceName"))
                r2("ItemType") = "SRV"
                r2("QtyRate") = myUtils.cValTN(r1("Quantity"))
                r2("UnitPrice") = myUtils.cValTN(r1("UnitPrice"))
            Next
            fItemSP.Focus()
            fItemSP.Enabled = True
            ReadOnlyCtlSpare()
        End If
    End Sub

    Private Sub btnAddSerial_Click(sender As Object, e As EventArgs) Handles btnAddSerial.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@salesorderid", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@dispatchid", myUtils.cValTN(myRow("dispatchid")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ProdSerialIDCSV", myUtils.MakeCSV(dsForm.Tables("Serial").Select, "ProdSerialID"), GetType(Integer), True))
        Dim rr() As DataRow = Me.AdvancedSelect("serials", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In myWinData.SelectDistinct(rr(0).Table, "PIDUnitID",,, "sysincl = 1").Select()
                Dim r2 As DataRow = Nothing
                Dim rr2() As DataRow = myViewSpare.mainGrid.myDv.Table.Select("PIDUnitId = " & myUtils.cValTN(r1("PIDUnitId")))
                If rr2.Length = 0 Then
                    r2 = myUtils.CopyOneRow(r1, myViewSpare.mainGrid.myDv.Table)
                    r2("ClassType") = "M"
                    r2("ItemDescrip") = myUtils.cStrTN(r1("DescripWO"))
                    r2("ItemType") = "PS"
                    r2("TransType") = "FG"
                End If

                Dim Count As Integer = 0
                For Each r3 As DataRow In rr(0).Table.Select("sysincl = 1 And PIDUnitId = " & myUtils.cValTN(r1("PIDUnitId")))
                    Dim r4 As DataRow = myUtils.CopyOneRow(r3, fItemSP.fSoItemSelect.myView.mainGrid.myDv.Table)
                    If rr2.Length > 0 Then r4("ODNoteSpareID") = myUtils.cValTN(rr2(0)("ODNoteSpareID")) Else r4("ODNoteSpareID") = myUtils.cValTN(r2("ODNoteSpareID"))
                    Count = Count + 1
                Next
                If rr2.Length > 0 Then rr2(0)("QtyRate") = myUtils.cValTN(rr2(0)("QtyRate")) + Count Else r2("QtyRate") = Count
            Next
            fItemSP.Focus()
            fItemSP.Enabled = True
        End If
    End Sub

    Private Sub EnableBtn(Bool As Boolean)
        btnAdd.Enabled = Bool
        btnDel.Enabled = Bool
    End Sub

    Private Sub btnAddAsc_Click(sender As Object, e As EventArgs) Handles btnAddAsc.Click
        Accessories = True
        myViewSpare.mainGrid.ButtonAction("add")
        Accessories = False
        fItemSP.Enabled = True
    End Sub

    Private Sub cmb_TaxType_Leave(sender As Object, e As EventArgs) Handles cmb_TaxType.Leave, cmb_TaxType.AfterCloseUp
        EnableBtn(myUtils.cValTN(cmb_campusid.Value) > 0 AndAlso (Not myUtils.IsInList(myUtils.cStrTN(cmb_TaxType.Value), "")))
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = ""
        Dim str As String = "ProcType = '" & myUtils.cStrTN(myRow("ChallanType")).Substring(0, 2) & "'"
        If Not IsNothing(cmb_campusid.SelectedRow) Then
            If Not IsNothing(cmb_POSTaxAreaID.SelectedRow) Then
                PartyTaxAreaCode = myUtils.cStrTN(cmb_POSTaxAreaID.SelectedRow.Cells("TaxAreaCode").Value)
            ElseIf Not IsNothing(cmb_CustomerID.SelectedRow) Then
                PartyTaxAreaCode = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value)
            ElseIf Not IsNothing(cmb_VendorID.SelectedRow) Then
                PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)
            End If

            If PartyTaxAreaCode.Trim.Length = 0 Then
                Dim Filter As String = myFuncs.PriceProcFilter(myRow("ChallanDate"), "", "", CtlPricing1.oProc.dsCombo.Tables("PriceProc"), str)
                CtlPricing1.SetProcTypeFilter(Filter)
            Else
                Dim Filter As String = ""
                If Not IsNothing(cmb_VendorID.SelectedRow) Then
                    Filter = myFuncs.PriceProcFilter(myRow("ChallanDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), str, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
                Else
                    Filter = myFuncs.PriceProcFilter(myRow("ChallanDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_campusid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), str)
                End If
                CtlPricing1.SetProcTypeFilter(Filter)
            End If
        Else
            CtlPricing1.SetProcTypeFilter(Str)
        End If
    End Sub

    Private Sub cmb_campusid_Leave(sender As Object, e As EventArgs) Handles cmb_campusid.Leave, cmb_campusid.AfterCloseUp
        EnableBtn(myUtils.cValTN(cmb_campusid.Value) > 0 AndAlso (Not myUtils.IsInList(myUtils.cStrTN(cmb_TaxType.Value), "")))
        ProcTypeFilter()

        HandleCampus()
    End Sub

    Private Sub cmb_InvoiceCampusID_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceCampusID.Leave, cmb_InvoiceCampusID.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub HandleCampus()
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, cmb_InvoiceCampusID)

        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
        dvInvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
        Dim str As String = " and SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID")) & ""
        dvProjectSite.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID") & str
        dvEmp.RowFilter = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "JoinDate", "LeaveDate", 0)
    End Sub

    Private Sub dt_ChallanDate_Leave(sender As Object, e As EventArgs) Handles dt_ChallanDate.Leave, dt_ChallanDate.AfterCloseUp
        HandleDate(dt_ChallanDate.Value)
    End Sub

    Private Sub btnSign_Click(sender As Object, e As EventArgs) Handles btnSign.Click
        Dim gRow As ug.UltraGridRow
        If myUtils.NullNot(Me.cmb_CheckedByID.Value) Then
            gRow = win.myWinUtils.FindRow(Me.cmb_CheckedByID, "UserID", myWinApp.Controller.Police.UniqueUserID)
            Me.cmb_CheckedByID.ActiveRow = gRow

        End If
    End Sub

    Private Sub cmb_CustomerID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub cmb_ConsigneeID_Leave(sender As Object, e As EventArgs) Handles cmb_ConsigneeID.Leave, cmb_ConsigneeID.AfterCloseUp
        Me.cmb_POSTaxAreaID.Value = HandleConsigneeID()
        ProcTypeFilter()
    End Sub

    Private Function HandleConsigneeID() As Integer
        Dim POSTaxAreaID As Integer
        If myUtils.cValTN(cmb_ConsigneeID.Value) > 0 Then
            POSTaxAreaID = myUtils.cValTN(cmb_ConsigneeID.SelectedRow.Cells("TaxAreaID").Value)
            cmb_POSTaxAreaID.ReadOnly = True
        Else
            POSTaxAreaID = myUtils.cValTN(myRow("POSTaxAreaID"))
            If myUtils.cValTN(myRow("DispatchID")) = 0 Then cmb_POSTaxAreaID.ReadOnly = False
        End If
        Return POSTaxAreaID
    End Function

    Private Sub cmb_POSTaxAreaID_Leave(sender As Object, e As EventArgs) Handles cmb_POSTaxAreaID.Leave, cmb_POSTaxAreaID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub btnUpdateDesc_Click(sender As Object, e As EventArgs) Handles btnUpdateDesc.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@sospareidcsv", myUtils.MakeCSV(dsForm.Tables("ODNoteSpare").Select, "sospareid"), GetType(Integer), True))
        Params.Add(New clsSQLParam("@soserviceidcsv", myUtils.MakeCSV(dsForm.Tables("ODNoteSpare").Select, "soserviceid"), GetType(Integer), True))
        Dim ds As DataSet = Me.GenerateParamsOutput("spares", Params).Data

        For Each r1 As DataRow In myViewSpare.mainGrid.myDv.Table.Select
            If myUtils.cValTN(r1("sospareid")) > 0 Then
                Dim rr1() As DataRow = ds.Tables("spares").Select("sospareid = " & myUtils.cValTN(r1("sospareid")))
                If rr1.Length > 0 Then
                    r1("ItemDescrip") = myUtils.cStrTN(rr1(0)("SpareName"))
                    r1("ItemUnitID") = myUtils.cValTN(rr1(0)("ItemUnitID"))
                    r1("Hsn_SC") = myUtils.cStrTN(rr1(0)("Hsn_SC"))
                End If
            ElseIf myUtils.cValTN(r1("soserviceid")) > 0 Then
                Dim rr1() As DataRow = ds.Tables("service").Select("soserviceid = " & myUtils.cValTN(r1("soserviceid")))
                If rr1.Length > 0 Then
                    r1("ItemDescrip") = myUtils.cStrTN(rr1(0)("ServiceName"))
                    r1("ItemUnitID") = myUtils.cValTN(rr1(0)("ItemUnitID"))
                    r1("Hsn_SC") = myUtils.cStrTN(rr1(0)("Hsn_SC"))
                End If
            End If
        Next
    End Sub

    Private Sub ReadOnlyCtlSpare()
        If myViewSpare.mainGrid.myDv.Table.Select.Length > 0 Then
            cmb_ProjectCampusID.ReadOnly = True
        Else
            cmb_ProjectCampusID.ReadOnly = False
        End If
    End Sub

    Private Sub btnCopyFrom_Click(sender As Object, e As EventArgs) Handles btnCopyFrom.Click
        Dim Params, Params1, Params2 As New List(Of clsSQLParam)

        Params.Add(New clsSQLParam("@ODNoteID", myUtils.cValTN(myRow("ODNoteID")), GetType(Integer), False))
        Dim rr() As DataRow = Me.AdvancedSelect("CopyODNote", Params)
        If (Not rr Is Nothing) AndAlso rr.Length > 0 Then

            cmb_CustomerID.Value = myUtils.cValTN(rr(0)("CustomerID"))
            Params1.Add(New clsSQLParam("@ODNoteID", myUtils.cValTN(rr(0)("ODNoteID")), GetType(Integer), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("CopyODNoteSpare", Params1)


            Params2.Add(New clsSQLParam("@ODNoteID", myUtils.cValTN(rr(0)("ODNoteID")), GetType(Integer), False))
            Params2.Add(New clsSQLParam("@ODNoteSpareIDCSV", myUtils.MakeCSV(rr1, "ODNoteSpareID"), GetType(Integer), True))
            Dim ds As DataSet = Me.GenerateParamsOutput("CopyOdNoteItem", Params2).Data
            For Each r1 As DataRow In rr1
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewSpare.mainGrid.myDv.Table)
                r2("ODNoteID") = DBNull.Value
                r2("PriceSlabID") = DBNull.Value
                r2("SOSpareID") = DBNull.Value
                r2("ItemType") = "AC"

                For Each r3 As DataRow In ds.Tables("ODNoteItem").Select("ODNoteSpareID = " & myUtils.cValTN(r1("ODNoteSpareID")) & "")
                    r3("ODNoteSpareID") = myUtils.cValTN(r2("ODNoteSpareID"))
                    Dim r4 As DataRow = myUtils.CopyOneRow(r3, myView.mainGrid.myDv.Table)
                    r4("ODNoteID") = DBNull.Value
                    r4("PriceSlabID") = DBNull.Value
                    r4("SpStock") = "J"
                Next
            Next
        End If
    End Sub

    Private Sub btnDelSpares_Click(sender As Object, e As EventArgs) Handles btnDelSpares.Click
        myViewSpare.mainGrid.ButtonAction("del")
        ReadOnlyCtlSpare()
        If myViewSpare.mainGrid.myDv.Table.Select.Length = 0 Then
            fItemSP.Enabled = False
            fItemSP.FormPrepared = False
        End If
    End Sub

    Private Sub btnAddRemark_Click(sender As Object, e As EventArgs) Handles btnAddRemark.Click
        Dim ItemDisc As String = "", Amount As Decimal = 0
        Dim rr() As DataRow = myViewSpare.mainGrid.myDv.Table.Select("ItemType = 'AC'")
        If (Not rr Is Nothing) AndAlso rr.Length > 0 Then
            ItemDisc = myUtils.cStrTN(rr(0)("ItemDescrip"))
            Amount = Math.Round(myUtils.cValTN(myViewSpare.mainGrid.Model.GetColSum("AmountTot")), 2)
        End If

        Dim fRemark As New frmAddRemark
        fRemark.ItemDescrip = ItemDisc
        fRemark.AmountTot = Amount
        fRemark.PrepFormRow(myRow.Row)
        fRemark.ShowDialog()
    End Sub
End Class