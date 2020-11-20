Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmRejectNote
    Inherits frmMax
    Friend fItem As frmRejectNoteItem
    Dim dvCamp As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(UltraGridItemList)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)

        fItem = New frmRejectNoteItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)

        fItem.Enabled = False
        txt_NoteNum.ReadOnly = True
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmRejectNoteModel = Me.InitData("frmRejectNoteModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)

            CtlReadOnly()

            HandleDate(myUtils.cDateTN(myRow("NoteDate"), DateTime.MinValue))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            fItem.myView.PrepEdit(Me.Model.GridViews("PurItemHist"))
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        cm.EndCurrentEdit()
        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        If fItem.PrepForm(r1) Then
            fItem.myView.mainGrid.myDv.RowFilter = "RejectNoteItemID = " & myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells("RejectNoteItemID").Value)
            fItem.Enabled = True
        End If
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If myView.mainGrid.myDv.Count > 0 Then
            If fItem.VSave Then
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub btnSelectDocument_Click(sender As Object, e As EventArgs) Handles btnSelectDocument.Click
        If myUtils.cValTN(cmb_campusid.Value) > 0 AndAlso myUtils.cValTN(cmb_VendorID.Value) > 0 Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@campusid", myUtils.cValTN(cmb_campusid.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@invoiceid", myUtils.cValTN(myRow("InvoiceID")), GetType(Integer), False))
            Dim rr() As DataRow = Me.AdvancedSelect("invoice", Params)

            If Not rr Is Nothing AndAlso rr.Length > 0 Then
                myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select())

                myRow("InvoiceID") = myUtils.cValTN(rr(0)("invoiceid"))
                txt_InvoiceNum.Text = myUtils.cValTN(rr(0)("InvoiceNum"))
                dt_InvoiceDate.Value = myUtils.cValTN(rr(0)("InvoiceDate"))
                Params.Clear()
                Params.Add(New clsSQLParam("@invoiceid", myUtils.cValTN(myRow("InvoiceID")), GetType(Integer), False))
                Params.Add(New clsSQLParam("@rejectnoteid", frmIDX, GetType(Integer), False))

                Dim ds As DataSet = Me.GenerateParamsOutput("rejectnoteitem", Params).Data
                myUtils.CopyRows(ds.Tables(0).Select, myView.mainGrid.myDv.Table)
                AddHistoryGrid(ds.Tables(1))
                CtlReadOnly()
            End If
        End If
    End Sub

    Private Sub AddHistoryGrid(dt As DataTable)
        Dim rr1() As DataRow = dt.Select
        If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
            For Each r1 As DataRow In rr1
                Dim rr2() As DataRow = myView.mainGrid.myDv.Table.Select("PurItemID = " & myUtils.cValTN(r1("PurItemID")))
                r1("RejectNoteItemID") = myUtils.cValTN(rr2(0)("RejectNoteItemID"))
                myUtils.CopyOneRow(r1, fItem.myView.mainGrid.myDv.Table)
                fItem.myView.mainGrid.myDv.Table.AcceptChanges()
            Next
        End If
    End Sub

    Private Sub CtlReadOnly()
        If myView.mainGrid.myDv.Table.Select.Length > 0 Then
            cmb_campusid.ReadOnly = True
            cmb_VendorID.ReadOnly = True
        End If
    End Sub

    Private Sub dt_NoteDate_Leave(sender As Object, e As EventArgs) Handles dt_NoteDate.Leave, dt_NoteDate.AfterCloseUp
        HandleDate(dt_NoteDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
    End Sub
End Class