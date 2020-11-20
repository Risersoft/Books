Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports risersoft.shared.Extensions

Public Class frmMatVouch
    Inherits frmMax
    Friend fItem As frmMatVouchItem, ObjGetMatVouch As New clsGetRecordsMatVouch
    Dim myViewRefDoc As New clsWinView, dv, dvTTySrc, dvDivision, dvMatDep, dvInvCamp, dvVendor As DataView, rSelVouch As DataRow

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        myView.SetGrid(Me.UltraGridItemList)
        myViewRefDoc.SetGrid(Me.UltraGridRefDoc)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)
        Me.AddTabExpansionUEGB(Me.UltraTabControl1.Tabs("Pricing"), Me.UEGB_ItemList)

        fItem = New frmMatVouchItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        fItem.fItemRes.InitPanel(fItem, Me)
        fItem.fItemSelect.InitPanel(fItem.UltraTabControl1.Tabs("Quantity").TabPage, fItem.UltraTabControl1.Tabs("MvtCode").TabPage, Me)

        fItem.fItemBOM.fItemRes.InitPanel(fItem.fItemBOM, Me)
        fItem.fItemBOM.fItemSelect.InitPanel(fItem.fItemBOM.UltraTabControl1.Tabs("Quantity").TabPage, fItem.fItemBOM.UltraTabControl1.Tabs("MvtCode").TabPage, Me)

        btnAdd.Enabled = False
        btnDel.Enabled = False

        CustVendEnable()
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmMatVouchModel = Me.InitData("frmMatVouchModel", oview, prepMode, prepIdx, strXML)

        If Me.BindModel(objModel, oview) Then
            Me.cmb_ChallanPending.ValueList = Me.Model.ValueLists("ChallanPending").ComboList

            If frmMode = EnumfrmMode.acAddM AndAlso cmb_TaxType.Rows.Count = 1 Then myRow("TaxType") = myUtils.cStrTN(cmb_TaxType.Rows(0).Cells("CodeWord").Value)

            Dim r1 As DataRow = Me.Model.SelectedRow("MatVouchTypeID")
            If Not r1 Is Nothing Then ComboVoucherType.Value = myUtils.cStrTN(r1("VouchTypeCode"))

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                lblSalesOrder.Text = Me.GenerateIDOutput("salesorderdescrip", myUtils.cValTN(myRow("SalesOrderID"))).Description
            End If

            HandleVoucherType(myUtils.cStrTN(ComboVoucherType.Value))
            HandleMatVouchType(myUtils.cValTN(myRow("MatVouchTypeID")))

            If (myView.mainGrid.myGrid.Rows.Count > 0) OrElse (myViewRefDoc.mainGrid.myGrid.Rows.Count > 0) Then
                btnAdd.Enabled = True
                btnDel.Enabled = True
                EnableControl(True)
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myView.mainGrid.myDv.RowFilter = "PMatVouchItemID is Null"
            myViewRefDoc.PrepEdit(Me.Model.GridViews("RefDoc"))

            dvMatDep = myWinSQL.AssignCmb(Me.dsCombo, "DepsMat", "", Me.cmb_matdepid,, 2)
            dvInvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_InvoiceCampusID,, 2)
            dvVendor = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID)
            dv = myWinSQL.AssignCmb(Me.dsCombo, "MatVouchType", "", Me.cmb_MatVouchTypeID, , 1)
            myWinSQL.AssignCmb(Me.dsCombo, "VoucherType", "", Me.ComboVoucherType)
            dvTTySrc = myWinSQL.AssignCmb(Me.dsCombo, "TaxType", "", Me.cmb_TaxType, , 1)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            myWinSQL.AssignCmb(Me.dsCombo, "Transporter", "", Me.cmb_TransporterID)

            Dim rr1() As DataRow = myWinData.SelectDistinct(myViewRefDoc.mainGrid.myDv.Table, "PInvoiceID", False,, "PInvoiceID is Not NULL").Select
            If rr1.Length > 0 Then
                btnDelRefDoc.Enabled = False
            End If

            fItem.BindModel(NewModel)
            fItem.fCostAssign.InitPanel(Me.fItem, Me, NewModel, "CostLot", "CostWBS", "CostCenter")

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                Dim ds As DataSet = Me.GenerateIDOutput("sparesoitem", myUtils.cValTN(myRow("SalesOrderID"))).Data
                myUtils.AddTable(dsCombo, ds, "SOSpare")
            End If

            fItem.fSoItemSelect.InitPanel(myUtils.cValTN(myRow("SalesOrderID")), 0, Me.fItem, Me, NewModel)

            Me.CtlPricing1.InitData("MatVouchId", myUtils.cValTN(frmIDX), "VouchDate", "MatVouchItemId", Me.dsForm.Tables("VouchItem"), fItem.CtlPricingChild1)
            HandleDate(myUtils.cDateTN(myRow("VouchDate"), DateTime.MinValue))
            WinFormUtils.ValidateComboValue(cmb_matdepid, myUtils.cValTN(myRow("MatDepID")))
            WinFormUtils.ValidateComboValue(cmb_InvoiceCampusID, myUtils.cValTN(myRow("InvoiceCampusID")))
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
        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Public Sub CustVendEnable()
        cmb_CustomerID.ReadOnly = True

        cmb_CustomerID.Value = DBNull.Value
        cmb_VendorID.Value = DBNull.Value

        fItem.Enabled = False
        UltraTabControl1.Tabs("Pricing").Visible = False
        UltraTabControl1.Tabs("Doc").Visible = False
        UltraTabControl1.Tabs("SO").Visible = False
        cmb_DivisionID.Visible = False
        lblDivision.Visible = False
    End Sub

    Private Sub ComboVoucherType_Leave(sender As Object, e As EventArgs) Handles ComboVoucherType.Leave, ComboVoucherType.AfterCloseUp
        HandleVoucherType(myUtils.cStrTN(ComboVoucherType.Value))
    End Sub

    Private Sub HandleVoucherType(Voucher As String)
        If IsNothing(cmb_MatVouchTypeID.SelectedRow) OrElse Not myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("VouchTypeCode").Value), myUtils.cStrTN(Voucher)) Then
            cmb_MatVouchTypeID.Value = DBNull.Value
            cmb_MatVouchTypeID.Text = String.Empty
            Me.txt_DefMvtCode.Value = DBNull.Value
            Me.txtRefDocTypeCode.Value = DBNull.Value
            dv.RowFilter = "VouchTypeCode = '" & myUtils.cStrTN(Voucher) & "'"
            HandleMatVouchType(myUtils.cValTN(cmb_MatVouchTypeID.Value))
            btnAdd.Enabled = False
            btnDel.Enabled = False
        End If
    End Sub

    Private Sub HandleMatVouchType(MatVouchTypeID As Integer)
        If MatVouchTypeID > 0 Then cmb_MatVouchTypeID.Value = MatVouchTypeID
        cmb_InvoiceCampusID.Visible = False
        lblInvoiceCampus.Visible = False
        cmb_VendorID.ReadOnly = True
        If Not IsNothing(cmb_MatVouchTypeID.SelectedRow) AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("VouchTypeCode").Value), "GR") AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "PO", "LPO", "JWO", "SO") Then
            cmb_InvoiceCampusID.Visible = True
            lblInvoiceCampus.Visible = True
            If myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "LPO", "PO") Then cmb_VendorID.ReadOnly = False
            UltraTabControl1.Tabs("SO").Visible = myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "SO")
        End If
    End Sub

    Private Sub cmb_MatVouchTypeID_Leave(sender As Object, e As EventArgs) Handles cmb_MatVouchTypeID.Leave, cmb_MatVouchTypeID.AfterCloseUp
        If Not myUtils.NullNot(cmb_MatVouchTypeID.Value) Then
            Me.txt_DefMvtCode.Text = myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("DefMvtCode").Value)
            HandleMatVouchType(myUtils.cValTN(cmb_MatVouchTypeID.Value))
        End If
        ButtonEnable(Not myUtils.NullNot(cmb_MatVouchTypeID.Value))
    End Sub

    Private Sub cmb_matdepid_Leave(sender As Object, e As EventArgs) Handles cmb_matdepid.Leave, cmb_matdepid.AfterCloseUp
        ButtonEnable(Not myUtils.NullNot(cmb_MatVouchTypeID.Value))
        HandleCampus()
    End Sub

    Private Sub cmb_TaxType_Leave(sender As Object, e As EventArgs) Handles cmb_TaxType.Leave, cmb_TaxType.AfterCloseUp
        ButtonEnable(Not myUtils.NullNot(cmb_MatVouchTypeID.Value))
    End Sub

    Private Sub ButtonEnable(Enable As Boolean)
        If Enable = True Then
            If (Not myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "ODN")) AndAlso Not myUtils.NullNot(cmb_matdepid.Value) AndAlso Not myUtils.NullNot(cmb_TaxType.Value) Then btnAdd.Enabled = True Else btnAdd.Enabled = False
            If Not myUtils.NullNot(cmb_matdepid.Value) AndAlso Not myUtils.NullNot(cmb_TaxType.Value) Then btnDel.Enabled = True Else btnDel.Enabled = False
        Else
            btnAdd.Enabled = False
            btnDel.Enabled = False
        End If
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
            fItem.ReCallOrderGrid(myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("MvtCode").Value), myUtils.cStrTN(myView.mainGrid.myGrid.ActiveRow.Cells("QtyTypeDes").Value), fItem.myViewOrderData)
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)
        ProcTypeFilter()
        If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
            cmb_CustomerID.ReadOnly = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            gr.Cells("MatVouchID").Value = myUtils.cValTN(myRow("MatVouchID"))
            If (myView.mainGrid.myDv.Table.Select.Length > 0) OrElse (myViewRefDoc.mainGrid.myDv.Table.Select.Length > 0) Then
                EnableControl(True)
            End If
        End If
    End Sub

    Private Sub EnableControl(ByVal Bool As Boolean)
        If myUtils.cStrTN(txt_VouchNum.Text).Trim.Length > 0 Then ComboVoucherType.ReadOnly = True Else ComboVoucherType.ReadOnly = Bool
        cmb_MatVouchTypeID.ReadOnly = Bool
        cmb_matdepid.ReadOnly = Bool
        cmb_TaxType.ReadOnly = Bool
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        If Not IsNothing(myView.mainGrid.myGrid.ActiveRow) Then
            Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)

            If myUtils.cValTN(r1("OdNoteItemID")) > 0 Then
                MsgBox("Delete OD Note through Reference Document.", MsgBoxStyle.Information, myWinApp.Vars("appname"))
            Else
                If MsgBox("Are you sure?" & vbCrLf & "Do you want to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
                    myUtils.DeleteRows(fItem.myView.mainGrid.myDv.Table.Select("PMatVouchItemID = " & myUtils.cValTN(r1("MatVouchItemID")) & " "))
                    myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select("MatVouchItemID = " & myUtils.cValTN(r1("MatVouchItemID")) & " "))
                End If
            End If
        End If

        If (myView.mainGrid.myDv.Table.Select.Length = 0) AndAlso (myViewRefDoc.mainGrid.myDv.Table.Select.Length = 0) Then
            fItem.myRow = Nothing
            CtlPricing1.oProc.UpdatePricingTable(Nothing)
            fItem.FormPrepared = False
            EnableControl(False)
            CustVendEnable()
            fItem.ControlStatus()
            If Not IsNothing(cmb_MatVouchTypeID.SelectedRow) AndAlso myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "LPO", "PO") Then cmb_VendorID.ReadOnly = False
        End If
    End Sub

    Private Sub btnDelRefDoc_Click(sender As Object, e As EventArgs) Handles btnDelRefDoc.Click
        If Not IsNothing(myViewRefDoc.mainGrid.myGrid.ActiveRow) Then
            Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewRefDoc.mainGrid.myGrid.ActiveRow)
            If myUtils.cValTN(r1("OdNoteID")) > 0 Then
                If MsgBox("This will delete all items related to this OD Note. Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
                    myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select("ODNoteID = " & myUtils.cValTN(r1("OdNoteID"))))
                    myUtils.DeleteRows(myViewRefDoc.mainGrid.myDv.Table.Select("ODNoteID = " & myUtils.cValTN(r1("OdNoteID"))))
                End If
            Else
                If MsgBox("This will delete all items in this voucher. Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("appname")) = MsgBoxResult.Yes Then
                    myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select)
                    If myUtils.cValTN(r1("IDNoteID")) > 0 Then
                        myUtils.DeleteRows(myViewRefDoc.mainGrid.myDv.Table.Select("IDNoteID = " & myUtils.cValTN(r1("IDNoteID"))))
                    ElseIf myUtils.cValTN(r1("MatVouchID")) > 0 Then
                        myUtils.DeleteRows(myViewRefDoc.mainGrid.myDv.Table.Select("MatVouchID = " & myUtils.cValTN(r1("MatVouchID"))))
                    ElseIf myUtils.cValTN(r1("IndentID")) > 0 Then
                        myUtils.DeleteRows(myViewRefDoc.mainGrid.myDv.Table.Select("IndentID = " & myUtils.cValTN(r1("IndentID"))))
                    End If
                End If
            End If
        End If

        If (myView.mainGrid.myDv.Table.Select.Length = 0) AndAlso (myViewRefDoc.mainGrid.myDv.Table.Select.Length = 0) Then
            fItem.FormPrepared = False
            EnableControl(False)
            CustVendEnable()
            fItem.ControlStatus()
        End If
    End Sub

    Private Sub btnSelectDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectDocument.Click
        If Not myUtils.NullNot(ComboVoucherType.Value) AndAlso Not myUtils.NullNot(cmb_MatVouchTypeID.Value) AndAlso Not myUtils.NullNot(cmb_matdepid.Value) AndAlso Not myUtils.NullNot(cmb_TaxType.Value) Then
            cm.EndCurrentEdit()
            If myUtils.IsInList(myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value), "LPO", "PO") AndAlso myUtils.NullNot(cmb_VendorID.Value) Then
                Exit Sub
            End If
            rSelVouch = SelectDoc(myRow.Row, win.myWinUtils.DataRowFromGridRow(cmb_MatVouchTypeID.SelectedRow), win.myWinUtils.DataRowFromGridRow(cmb_matdepid.SelectedRow))
        End If
    End Sub

    Public Function SelectDoc(rMatVouch As DataRow, rMatVouchType As DataRow, rMatDep As DataRow) As DataRow
        Dim RefDocTypeCode, VouchTypeCode As String
        Dim rrSelVouch() As DataRow = Nothing

        RefDocTypeCode = myUtils.cStrTN(rMatVouchType("RefDocTypeCode"))
        VouchTypeCode = myUtils.cStrTN(rMatVouchType("VouchTypeCode"))

        If (Not myUtils.IsInList(myUtils.cStrTN(RefDocTypeCode), "Oth")) AndAlso (Not myUtils.IsInList(myUtils.cStrTN(RefDocTypeCode), "")) Then
            Dim Params As New List(Of clsSQLParam)
            If myUtils.cValTN(rMatVouch("VendorID")) > 0 Then Params.Add(New clsSQLParam("@vendorid", rMatVouch("VendorID"), GetType(Integer), False))
            If myUtils.cValTN(rMatVouch("VendorID")) > 0 Then Params.Add(New clsSQLParam("@MainPartyID", myUtils.cValTN(cmb_VendorID.SelectedRow.Cells("MainPartyID").Value), GetType(Integer), False))
            If myUtils.cValTN(rMatVouch("VendorID")) > 0 Then Params.Add(New clsSQLParam("@VendorType", "'" & myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("VendorType").Value) & "'", GetType(String), False))
            If myUtils.cValTN(rMatVouch("CustomerID")) > 0 Then Params.Add(New clsSQLParam("@customerid", rMatVouch("CustomerID"), GetType(Integer), False))
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then Params.Add(New clsSQLParam("@InvoiceCampusID", myUtils.cValTN(rMatVouch("InvoiceCampusID")), GetType(Integer), False))
            If myView.mainGrid.myDv.Table.Select.Length > 0 Then Params.Add(New clsSQLParam("@DivisionID", myUtils.cValTN(rMatVouch("DivisionID")), GetType(Integer), False))

            Params.Add(New clsSQLParam("@puritemidcsv", myUtils.MakeCSV(Me.dsForm.Tables("Pur").Select, "PurItemID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(rMatDep("CampusID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@matdepid", myUtils.cValTN(cmb_matdepid.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@matvouchid", myUtils.cValTN(rMatVouch("MatVouchID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@ordertype", "'" & myUtils.cStrTN(RefDocTypeCode) & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@matvouchitemidcsv", myUtils.MakeCSV(Me.dsForm.Tables("VouchItem").Select, "MatVouchItemID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@odnoteitemidcsv", myUtils.MakeCSV(Me.dsForm.Tables("VouchItem").Select, "OdNoteItemID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@vouchtypecode", "'" & myUtils.cStrTN(rMatVouchType("VouchTypeCode")) & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@itemidcsv", myUtils.MakeCSV(Me.dsForm.Tables("VouchItem").Select, "ItemID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@TaxType", "'" & rMatVouch("TaxType") & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@VouchDate", Format(rMatVouch("VouchDate"), "dd-MMM-yyyy"), GetType(DateTime), False))
            rrSelVouch = Me.AdvancedSelect(RefDocTypeCode, Params)

            '''''''''''''rSelVouch.Table is Used in OperateProcess So Multi Select Should be False.......
            If Not rrSelVouch Is Nothing AndAlso rrSelVouch.Length > 0 Then
                If myUtils.IsInList(myUtils.cStrTN(VouchTypeCode), "GR") Then
                    If myUtils.IsInList(myUtils.cStrTN(RefDocTypeCode), "IDN") Then
                        txtRefDocTypeCode.Value = myUtils.cStrTN(rrSelVouch(0)("NoteNum")) & " Dated: " & myUtils.cStrTN(rrSelVouch(0)("NoteDate"))
                    Else
                        txtRefDocTypeCode.Value = myUtils.cStrTN(rrSelVouch(0)("OrderNum")) & " Dated: " & myUtils.cStrTN(rrSelVouch(0)("OrderDate"))
                    End If
                Else
                    If myUtils.IsInList(myUtils.cStrTN(RefDocTypeCode), "MV") Then
                        txtRefDocTypeCode.Value = myUtils.cStrTN(rrSelVouch(0)("VouchNum")) & " Dated: " & myUtils.cStrTN(rrSelVouch(0)("VouchDate"))
                    ElseIf myUtils.IsInList(myUtils.cStrTN(RefDocTypeCode), "ODN") Then
                        txtRefDocTypeCode.Value = myUtils.cStrTN(rrSelVouch(0)("VouchNum")) & " Dated: " & myUtils.cStrTN(rrSelVouch(0)("ChallanDate"))
                    ElseIf myUtils.IsInList(myUtils.cStrTN(RefDocTypeCode), "MI") Then
                        txtRefDocTypeCode.Value = myUtils.cStrTN(rrSelVouch(0)("IndentNum")) & " Dated: " & myUtils.cStrTN(rrSelVouch(0)("IndentDate"))
                    End If
                End If
            End If
        End If
        If Not rrSelVouch Is Nothing AndAlso rrSelVouch.Length > 0 Then Return rrSelVouch(0) Else Return Nothing
    End Function

    Private Sub ButtonExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExecute.Click
        Dim Params1 As New List(Of clsSQLParam), oProc As New clsMatVouchProc(Me.Controller)
        If Not myUtils.NullNot(txtRefDocTypeCode.Value) Then
            Dim dtSql As DataTable
            Dim RefTypeCode As String = myUtils.cStrTN(cmb_MatVouchTypeID.SelectedRow.Cells("RefDocTypeCode").Value)
            If myUtils.IsInList(RefTypeCode, "PO", "LPO", "MO", "JWO") Then
                Dim dtPurItems As DataTable
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@purorderid", myUtils.cValTN(rSelVouch("PurOrderID")), GetType(Integer), False))
                Params.Add(New clsSQLParam("@puritemidcsv", myUtils.MakeCSV(Me.dsForm.Tables("Pur").Select, "PurItemID"), GetType(Integer), True))
                Dim Model As clsViewModel = Me.GenerateParamsModel("puritem", Params)
                dtPurItems = ObjGetMatVouch.ShowGridItems(Model, "PurItemID")

                Params1.Clear()
                Params1.Add(New clsSQLParam("@purorderid", myUtils.cValTN(rSelVouch("PurOrderID")), GetType(Integer), False))
                Params1.Add(New clsSQLParam("@ordertype", "'" & myUtils.cStrTN(rSelVouch("OrderType")) & "'", GetType(String), False))
                Params1.Add(New clsSQLParam("@puritemidcsv", myUtils.MakeCSV(dtPurItems.Select, "PurItemID"), GetType(Integer), True))
                dtSql = Me.GenerateParamsOutput(RefTypeCode, Params1).Data.Tables(0)
                oProc.ReplaceValues(dtPurItems, "puritemid", "qty", dtSql, "puritemid", "qtyentry", "", "")
            ElseIf myUtils.IsInList(RefTypeCode, "MI") Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@indentid", myUtils.cValTN(rSelVouch("IndentID")), GetType(Integer), False))
                Dim Model As clsViewModel = Me.GenerateParamsModel("indentitem", Params)
                Dim dtIndentItems As DataTable = ObjGetMatVouch.ShowGridItems(Model, "IndentItemID")

                Params1.Clear()
                Params1.Add(New clsSQLParam("@indentid", myUtils.cValTN(rSelVouch("IndentID")), GetType(Integer), False))
                dtSql = Me.GenerateParamsOutput("MI", Params1).Data.Tables(0)
                RefDocGrid(dtSql)
            Else
                Params1.Clear()
                If myUtils.IsInList(RefTypeCode, "IDN") Then
                    Params1.Add(New clsSQLParam("@idnoteid", myUtils.cValTN(rSelVouch("IDNoteID")), GetType(Integer), False))
                ElseIf myUtils.IsInList(RefTypeCode, "ODN") Then
                    Params1.Add(New clsSQLParam("@odnoteid", myUtils.cValTN(rSelVouch("ODNoteID")), GetType(Integer), False))
                ElseIf myUtils.IsInList(RefTypeCode, "MV") Then
                    Params1.Add(New clsSQLParam("@matvouchid", myUtils.cValTN(rSelVouch("MatVouchID")), GetType(Integer), False))
                End If
                dtSql = Me.GenerateParamsOutput(RefTypeCode, Params1).Data.Tables(0)
                RefDocGrid(dtSql)
            End If
            If dtSql.Rows.Count > 0 Then
                If dtSql.Columns.Contains("QtyRateFac") Then
                    For Each r2 As DataRow In dtSql.Select
                        r2("QtyRate") = myUtils.cValTN(r2("QtyEntry")) * myUtils.cValTN(r2("QtyRateFac"))
                    Next
                End If


                Dim ds As New DataSet
                myUtils.AddTable(ds, dtSql, "dtSql")
                myUtils.AddTable(ds, rSelVouch.Table, "rSelVouch")
                Me.Model.DatasetCollection.AddUpd("generatedata", ds)
                Me.OperateBindModel("generate")
                Dim rr1() As DataRow = Me.Model.DatasetCollection("generatedata").Tables("MatVouchItem").Select

                Me.CtlPricing1.SyncPriceSlabParent(myUtils.cValTN(myRow("priceslabid")))
                cm.EndCurrentEdit()
                FillVendorCustByExecute(RefTypeCode)
                txtRefDocTypeCode.Value = DBNull.Value

                If myUtils.IsInList(RefTypeCode, "MV") Then
                    myRow("ChallanNum") = myUtils.cStrTN(rSelVouch("ChallanNum"))
                    myRow("ChallanDate") = rSelVouch("ChallanDate")
                    myRow("GRNum") = myUtils.cStrTN(rSelVouch("GRNum"))
                    myRow("PriceSlabID") = DBNull.Value
                    If myUtils.cValTN(rSelVouch("TransporterID")) > 0 Then myRow("TransporterID") = myUtils.cValTN(rSelVouch("TransporterID"))


                    myUtils.ChangeAll(dsForm.Tables("VouchItem").Select, "ReserveGRBehave=A")
                    myUtils.ChangeAll(dsForm.Tables("VouchItem").Select, "ReserveGIBehave=A")
                End If

                For Each rMVItem As DataRow In rr1
                    fItem.PrepMatVouchItem(rMVItem)
                Next
            End If
        End If
    End Sub

    Private Sub RefDocGrid(dtSql As DataTable)
        If dtSql.Rows.Count > 0 Then
            Dim r3 As DataRow = myUtils.CopyOneRow(rSelVouch, myViewRefDoc.mainGrid.myDv.Table)
            r3("DocType") = myUtils.cStrTN(cmb_MatVouchTypeID.Text)
        End If
    End Sub

    Private Sub FillVendorCustByExecute(RefDocTypeCode As String)
        If Not rSelVouch Is Nothing Then
            If rSelVouch.Table.Columns.Contains("VendorID") AndAlso RefDocTypeCode <> "LPO" Then
                If myUtils.cValTN(rSelVouch("VendorID")) <> 0 Then myRow("VendorID") = myUtils.cValTN(rSelVouch("VendorID"))
            End If

            If rSelVouch.Table.Columns.Contains("CustomerID") Then
                If myUtils.cValTN(rSelVouch("CustomerID")) <> 0 Then myRow("CustomerID") = myUtils.cValTN(rSelVouch("CustomerID"))
            End If

            If rSelVouch.Table.Columns.Contains("InvoiceCampusID") Then
                If myUtils.cValTN(rSelVouch("InvoiceCampusID")) <> 0 Then myRow("InvoiceCampusID") = myUtils.cValTN(rSelVouch("InvoiceCampusID"))
            End If

            If rSelVouch.Table.Columns.Contains("DivisionID") Then
                If myUtils.cValTN(rSelVouch("DivisionID")) <> 0 Then myRow("DivisionID") = myUtils.cValTN(rSelVouch("DivisionID"))
            End If

            If rSelVouch.Table.Columns.Contains("PriceSlabID") Then myRow("PriceSlabID") = myUtils.cValTN(rSelVouch("PriceSlabID"))
            EnableControl(True)
        End If
        If myUtils.IsInList(RefDocTypeCode, "ODN") Then myRow("priceslabid") = DBNull.Value
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub cmb_CustomerID_Leave(sender As Object, e As EventArgs) Handles cmb_CustomerID.Leave, cmb_CustomerID.AfterCloseUp
        ProcTypeFilter()
    End Sub

    Private Sub ProcTypeFilter()
        Dim PartyTaxAreaCode As String = "", Filter As String = ""
        If Not IsNothing(fItem.cmb_MvtCode.SelectedRow) Then
            If Not IsNothing(cmb_CustomerID.SelectedRow) Then
                PartyTaxAreaCode = myUtils.cStrTN(cmb_CustomerID.SelectedRow.Cells("TaxAreaCode").Value)
                Filter = myFuncs.PriceProcFilter(myRow("VouchDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_matdepid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), "ProcType = '" & myUtils.cStrTN(fItem.cmb_MvtCode.SelectedRow.Cells("PricingType").Value) & "'")
            ElseIf Not IsNothing(cmb_VendorID.SelectedRow) Then
                Dim StrFilter As String
                PartyTaxAreaCode = myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("TaxAreaCode").Value)

                If (String.IsNullOrEmpty(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("GSTIN").Value))) And (Not myUtils.IsInList(myUtils.cStrTN(PartyTaxAreaCode), "IMP")) Then
                    StrFilter = "ProcType = '" & myUtils.cStrTN(fItem.cmb_MvtCode.SelectedRow.Cells("PricingType").Value) & "' and RCHRG = 'Y'"
                Else
                    StrFilter = "ProcType = '" & myUtils.cStrTN(fItem.cmb_MvtCode.SelectedRow.Cells("PricingType").Value) & "' and isNull(IsUnreg,0) = 0"
                End If


                If myUtils.IsInList(myUtils.cStrTN(cmb_VendorID.SelectedRow.Cells("VendorType").Value), "EM") Then
                    Filter = myFuncs.PriceProcFilter(myRow("VouchDate"), "", "", CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter)
                Else
                    Filter = myFuncs.PriceProcFilter(myRow("VouchDate"), PartyTaxAreaCode, myUtils.cStrTN(cmb_matdepid.SelectedRow.Cells("TaxAreaCode").Value), CtlPricing1.oProc.dsCombo.Tables("PriceProc"), StrFilter, myUtils.cBoolTN(cmb_VendorID.SelectedRow.Cells("ImportAllow").Value))
                End If
            End If
            CtlPricing1.SetProcTypeFilter(Filter)
        Else
            CtlPricing1.SetProcTypeFilter("1=0")
        End If
    End Sub

    Private Sub cmb_InvoiceCampusID_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceCampusID.Leave, cmb_InvoiceCampusID.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub HandleCampus()
        Me.cm.EndCurrentEdit()
        dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_matdepid, cmb_InvoiceCampusID)
        If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
        If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
    End Sub

    Private Sub dt_VouchDate_Leave(sender As Object, e As EventArgs) Handles dt_VouchDate.Leave, dt_VouchDate.AfterCloseUp
        HandleDate(dt_VouchDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvMatDep.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "MatDepID")
        dvInvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
    End Sub

    Private Sub btnSelectSO_Click(sender As Object, e As EventArgs) Handles btnSelectSO.Click
        cm.EndCurrentEdit()
        If (Not cmb_matdepid.SelectedRow Is Nothing) AndAlso (Not cmb_CustomerID.SelectedRow Is Nothing) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@MainPartyID", myUtils.cValTN(cmb_CustomerID.SelectedRow.Cells("MainPartyID").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_matdepid.SelectedRow.Cells("CompanyId").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@VouchDate", Format(dt_VouchDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
            Params.Add(New clsSQLParam("@SalesOrderID", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("salesorder", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                myRow("SalesOrderID") = myUtils.cValTN(rr1(0)("SalesOrderID"))
                lblSalesOrder.Text = "Sales Order :- " & myUtils.cStrTN(rr1(0)("OrderNum")) & " Date - " & Format(rr1(0)("OrderDate"), "dd-MMM-yyyy")

                Dim ds As DataSet = Me.GenerateIDOutput("sparesoitem", myUtils.cValTN(myRow("SalesOrderID"))).Data
                myUtils.AddTable(dsCombo, ds, "SOSpare")

                fItem.fSoItemSelect.InitPanel(myUtils.cValTN(myRow("SalesOrderID")), 0, Me.fItem, Me, Me.Model)
                cmb_CustomerID.ReadOnly = True
                fItem.fSoItemSelect.HandleItem()
            End If
        End If
    End Sub

    Private Sub btnRemoveSO_Click(sender As Object, e As EventArgs) Handles btnRemoveSO.Click
        myRow("SalesOrderID") = DBNull.Value
        lblSalesOrder.Text = "Select Sales Order"
        cmb_CustomerID.ReadOnly = False
    End Sub
End Class