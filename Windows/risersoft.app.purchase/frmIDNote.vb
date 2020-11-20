Imports risersoft.app.mxform
Imports System.Windows.Forms

Public Class frmIDNote
    Inherits frmMax
    Friend fItem As frmIDNoteItem
    Dim dvVendor, dvMatDep, dvCamp As DataView, rrPurOrder() As DataRow

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(Me.UltraGridItemList)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)

        fItem = New frmIDNoteItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.Enabled = False

        EnableControl(False)
    End Sub

    Private Sub EnableControl(ByVal Bool As Boolean)
        btnDel.Enabled = Bool
        btnSelectDocument.Enabled = Bool
        ButtonExecute.Enabled = Bool
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmIDNoteModel = Me.InitData("frmIDNoteModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            HandleOrderType(myUtils.cStrTN(myRow("OrderType")), myUtils.cValTN(myRow("MoMatDepID")), myUtils.cValTN(myRow("VendorID")), myUtils.cValTN(myRow("CampusID")))
            cmb_CampusId.Value = myUtils.cValTN(myRow("CampusID"))
            If myView.mainGrid.myDS.Tables(0).Select.Length > 0 Then ReadOnlyCantrol(True)

            CalcPreBalance()

            HandleDate(myUtils.cDateTN(myRow("NoteDate"), DateTime.MinValue))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Items"))
            dvVendor = myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID, , 1)
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusId,, 2)
            dvMatDep = myWinSQL.AssignCmb(Me.dsCombo, "MatDep", "", Me.cmb_MOMatDepID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "OrderType", "", Me.Cmb_OrderType)

            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If (myView.mainGrid.myDS.Tables(0).Select.Length = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridItemList_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridItemList.AfterRowActivate
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)

        For Each r3 As DataRow In fItem.myView.mainGrid.myDS.Tables(0).Select("PurItemID= " & myView.mainGrid.myGrid.ActiveRow.Cells("PurItemID").Value)
            Dim Qty As Decimal = Qty + myUtils.cValTN(r3("Qty"))
            fItem.txtTotalQty.Value = Qty
        Next
        fItem.Enabled = True
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Cmb_OrderType_Leave(sender As Object, e As EventArgs) Handles Cmb_OrderType.Leave, Cmb_OrderType.AfterCloseUp
        If myView.mainGrid.myDS.Tables(0).Select.Length = 0 Then HandleOrderType(myUtils.cStrTN(Cmb_OrderType.Value), myUtils.cValTN(cmb_MOMatDepID.Value), myUtils.cValTN(cmb_VendorID.Value), myUtils.cValTN(cmb_CampusId.Value))
    End Sub

    Private Sub HandleOrderType(OrderType As String, MatDepID As Integer, VendorID As Integer, CampusID As Integer)
        cmb_MOMatDepID.ReadOnly = True
        cmb_VendorID.ReadOnly = True
        cmb_CampusId.ReadOnly = True
        If Not myUtils.IsInList(myUtils.cStrTN(OrderType), "") Then
            cmb_CampusId.ReadOnly = False
            cmb_VendorID.ReadOnly = False
            If myUtils.IsInList(myUtils.cStrTN(OrderType), "PO", "JWO") Then
                cmb_MOMatDepID.Value = DBNull.Value
                dvVendor.RowFilter = "VendorType = 'MS'"
                If cmb_VendorID.SelectedRow Is Nothing Then cmb_VendorID.Value = DBNull.Value
            ElseIf myUtils.IsInList(myUtils.cStrTN(OrderType), "LPO") Then
                cmb_MOMatDepID.Value = DBNull.Value
                dvVendor.RowFilter = "VendorType = 'EM'"
                If cmb_VendorID.SelectedRow Is Nothing Then cmb_VendorID.Value = DBNull.Value
            ElseIf myUtils.IsInList(myUtils.cStrTN(OrderType), "MO") Then
                cmb_CampusId.ReadOnly = True
                cmb_VendorID.Value = DBNull.Value
                cmb_MOMatDepID.ReadOnly = False
                cmb_VendorID.ReadOnly = True
            End If

            If (myUtils.cValTN(MatDepID) > 0 OrElse myUtils.cValTN(VendorID) > 0) AndAlso myUtils.cValTN(CampusID) > 0 Then
                EnableControl(True)
            End If
        End If
    End Sub

    Private Sub ReadOnlyCantrol(Bool As Boolean)
        Cmb_OrderType.ReadOnly = Bool
        cmb_CampusId.ReadOnly = Bool
        cmb_MOMatDepID.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
        fItem.Enabled = Bool
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            myRow("PriceSlabID") = DBNull.Value
            ReadOnlyCantrol(False)
            HandleOrderType(myUtils.cStrTN(Cmb_OrderType.Value), myUtils.cValTN(cmb_MOMatDepID.Value), myUtils.cValTN(cmb_VendorID.Value), myUtils.cValTN(cmb_CampusId.Value))

            If fItem.cmb_ItemId.SelectedRow Is Nothing Then fItem.cmbItemName.Value = DBNull.Value
            If fItem.cmb_ItemId.SelectedRow Is Nothing Then fItem.cmb_BaseUnitID.Value = DBNull.Value
            EnableControl(True)
            btnDel.Enabled = False
        End If
    End Sub

    Private Sub cmb_MOMatDepID_Leave(sender As Object, e As EventArgs) Handles cmb_MOMatDepID.Leave, cmb_MOMatDepID.AfterCloseUp
        If ((Not myUtils.NullNot(cmb_VendorID.Value)) OrElse (Not myUtils.NullNot(cmb_MOMatDepID.Value))) AndAlso (Not myUtils.NullNot(Cmb_OrderType.Value)) Then
            If Not IsNothing(cmb_MOMatDepID.SelectedRow) Then cmb_CampusId.Value = myUtils.cValTN(cmb_MOMatDepID.SelectedRow.Cells("CampusId").Value)
            EnableControl(True)
        ElseIf myUtils.IsInList(myUtils.cStrTN(Cmb_OrderType.Value), "MO") AndAlso myUtils.NullNot(cmb_MOMatDepID.Value) Then
            cmb_CampusId.Value = DBNull.Value
        Else
            EnableControl(False)
        End If
    End Sub

    Private Sub cmb_VendorID_Leave(sender As Object, e As EventArgs) Handles cmb_VendorID.Leave, cmb_VendorID.AfterCloseUp
        If (((Not myUtils.NullNot(cmb_VendorID.Value)) AndAlso (Not myUtils.NullNot(cmb_CampusId.Value))) OrElse (Not myUtils.NullNot(cmb_MOMatDepID.Value))) AndAlso (Not myUtils.NullNot(Cmb_OrderType.Value)) Then
            EnableControl(True)
        Else
            EnableControl(False)
        End If
    End Sub

    Private Sub cmb_CampusId_Leave(sender As Object, e As EventArgs) Handles cmb_CampusId.Leave, cmb_CampusId.AfterCloseUp
        If ((Not myUtils.NullNot(cmb_MOMatDepID.Value)) OrElse (Not myUtils.NullNot(cmb_VendorID.Value))) AndAlso (Not myUtils.NullNot(cmb_CampusId.Value)) AndAlso (Not myUtils.NullNot(Cmb_OrderType.Value)) Then
            EnableControl(True)
        Else
            EnableControl(False)
        End If
    End Sub

    Private Sub btnSelectDocument_Click(sender As Object, e As EventArgs) Handles btnSelectDocument.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_CampusId.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ordertype", "'" & myUtils.cStrTN(Cmb_OrderType.Value) & "'", GetType(String), False))
        Params.Add(New clsSQLParam("@momatdepid", myUtils.cValTN(cmb_MOMatDepID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@puritemidcsv", myUtils.MakeCSV(myView.mainGrid.myDS.Tables(0).Select, "PurItemID"), GetType(Integer), True))
        Params.Add(New clsSQLParam("@priceslabid", myUtils.cValTN(myRow("PriceSlabID")), GetType(Integer), False))
        rrPurOrder = Me.AdvancedSelect("purorder", Params)
        If Not rrPurOrder Is Nothing AndAlso rrPurOrder.Length > 0 Then
            txtRefDocTypeCode.Text = myUtils.cStrTN(rrPurOrder(0)("OrderNum"))
        End If
    End Sub

    Private Sub ButtonExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExecute.Click
        If Not rrPurOrder Is Nothing AndAlso rrPurOrder.Length > 0 AndAlso txtRefDocTypeCode.Text.Trim.Length > 0 Then
            Dim dt As DataTable = Me.GenerateIDOutput("puritems", myUtils.cValTN(rrPurOrder(0)("PurOrderID"))).Data.Tables(0)
            For Each r1 As DataRow In dt.Select
                Dim rr2 As DataRow() = myView.mainGrid.myDS.Tables(0).Select("PurItemID = " & myUtils.cValTN(r1("PurItemID")))
                If rr2.Length = 0 Then
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDS.Tables(0))
                    myRow("PriceSlabID") = myUtils.cValTN(rrPurOrder(0)("PriceSlabID"))
                End If
            Next

            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@idnoteid", myUtils.cValTN(myRow("IDNoteID")), GetType(Integer), False))
            Params.Add(New clsSQLParam("@puritemidcsv", myUtils.MakeCSV(myView.mainGrid.myDS.Tables(0).Select, "PurItemID"), GetType(Integer), True))
            Dim oRet As clsProcOutput = Me.GenerateParamsOutput("notehistory", Params)
            If oRet.Success Then
                Me.UpdateViewData(fItem.myView, oRet)
            Else
                MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
            End If

            CalcPreBalance()
            txtRefDocTypeCode.Text = String.Empty
            ReadOnlyCantrol(True)
            EnableControl(True)

            fItem.Focus()
        End If
    End Sub

    Private Sub CalcPreBalance()
        For Each r1 As DataRow In myView.mainGrid.myDS.Tables(0).Select
            Dim QtyNote As Integer = 0
            For Each r2 As DataRow In fItem.myView.mainGrid.myDS.Tables(0).Select("PurItemID =" & myUtils.cValTN(r1("PurItemID")))
                QtyNote = QtyNote + myUtils.cValTN(r2("Qty"))
            Next
            r1("PreBalance") = myUtils.cValTN(r1("TotalQty")) - myUtils.cValTN(QtyNote)
        Next
        CalcBalance()
    End Sub

    Public Sub CalcBalance()
        For Each r1 As DataRow In myView.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Qty"))
        Next
    End Sub

    Private Sub frmIDNote_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnSelectDocument.Focus()
        End If
    End Sub

    Private Sub dt_NoteDate_Leave(sender As Object, e As EventArgs) Handles dt_NoteDate.Leave, dt_NoteDate.AfterCloseUp
        HandleDate(dt_NoteDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvMatDep.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "MatDepID")
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
    End Sub
End Class