Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent

Public Class frmInvoiceQtyNote
    Inherits frmMax
    Friend myViewVouch As New clsWinView
    Dim PostPeriodId As Integer, ObjVouch As New clsVoucherNum
    Dim dtMatVouchItem As DataTable, dv, dv2 As DataView, ObjIVProc As New clsIVProcBase

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        myViewVouch.SetGrid(Me.UltraGridVoucherList)
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        Dim Sql As String

        Sql = "Select CampusID, DispName, CompanyID  from Campus  Order by DispName"
        myWinSQL.AssignCmb(Me.dsCombo, "Campus", Sql, Me.cmb_campusid)

        Sql = "SELECT  Vendor.VendorID, Party.PartyName, Vendor.VendorClass FROM  Vendor INNER JOIN Party ON Vendor.PartyID = Party.PartyID Order By Party.PartyName"
        myWinSQL.AssignCmb(Me.dsCombo, "Vendor", Sql, Me.cmb_VendorID)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        dv = myFuncs.AssignCodeWordCmb(Me.dsCombo, "CodeWords", Me.cmb_InvoiceType, "Invoice", "InvoiceType", "(CodeWord = 'QC' or CodeWord = 'QD')")
        dv2 = myFuncs.AssignCodeWordCmb(Me.dsCombo, "CodeWords", Me.cmb_BillOf, "Invoice", "BillOf", "")

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)

        myView.SetGrid(UltraGridItemList)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql, str1 As String

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Sql = "Select * from InvoicePurch Where InvoicePurchID = " & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)
        If prepIDX = 0 Then
            myRow("InvoiceDate") = Now.Date
        End If

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myView.mainGrid.BindView(Me.dsForm, , 1)
        myView.myMode = EnumViewMode.acSelectM : myView.MultiSelect = True
        myView.mainGrid.myCMain.QuickConf("", True, "0-0-0-0-0-5-5-0-5-10-5-5-0-5-0", True)
        myUtils.ChangeAll(Me.dsForm.Tables("InvoiceMVItem").Select, "Sysincl = 1")
        myView.mainGrid.HighlightRows()
        Me.UltraGridItemList.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChangeOrLostFocus


        Sql = " Select InvoiceMVId, MatVouchID, InvoicePurchID, VouchNum, VouchDate from purListInvoiceMVItem()  Where InvoicePurchID = " & frmIDX & " "
        myViewVouch.myMode = EnumViewMode.acSelectM : myViewVouch.MultiSelect = True
        myViewVouch.mainGrid.myCMain.QuickConf(Sql, True, "0-0-0-5-5", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""InvoiceMV"" IDFIELD=""InvoiceMVID""><COL KEY ="" InvoiceMVId, InvoicePurchID, MatVouchID""/></BAND>"
        myViewVouch.mainGrid.PrepEdit(str1, , btnDel)
        myUtils.ChangeAll(myViewVouch.mainGrid.myDS.Tables(0).Select, "Sysincl = 1")
        myViewVouch.mainGrid.HighlightRows()
        Me.UltraGridVoucherList.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChangeOrLostFocus

        HandleInvoiceType(myUtils.cStrTN(myRow("InvoiceType")))
        If myViewVouch.mainGrid.myDS.Tables(0).Rows.Count > 0 Then
            EnableControls(True)
        End If
        txt_InvoiceNum.ReadOnly = True
        cmb_BillOf.ReadOnly = True

        Return True
    End Function

    Private Sub BindDataTable(ByVal InvoicePurchID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = " Select InvoiceMVItemId, InvoicePurchID, InvoiceSaleID, MatVouchID, MatVouchItemID, VouchNum, VouchDate, ItemID, ItemCode, ItemName, QtyRate, BasicRate, PriceSlabID, AmountTot, AmountWV from purListInvoiceMVItem() Where InvoicePurchID = " & InvoicePurchID & " "
        ds = SqlHelper.ExecuteDataset(myApp.dbConn, CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "InvoiceMVItem", 0)
    End Sub

    Public Overrides Function VSave() As Boolean
        Dim objAccVouch As New clsAVProc
        Me.InitError()
        VSave = False
        If myUtils.NullNot(Me.cmb_campusid.Value) Then myWinForms.AddError(Me.cmb_campusid, "Select a Campus")
        If myUtils.NullNot(Me.cmb_BillOf.Value) Then myWinForms.AddError(Me.cmb_BillOf, "Select a Bill Of")
        If myUtils.NullNot(Me.cmb_VendorID.Value) Then myWinForms.AddError(Me.cmb_VendorID, "Select a Vendor")
        If Me.myView.mainGrid.myGrid.Rows.Count = 0 Then myWinForms.AddError(Me.myView.mainGrid.myGrid, "Please Enter Some Transactions")
        If myUtils.IsInList(myUtils.cStrTN(cmb_BillOf.Value), "P") Then
            If txt_InvoiceNum.Text.Trim.Length = 0 Then myWinForms.AddError(txt_InvoiceNum, "Please Enter Voucher No.")
        End If


        If Me.CanSave Then
            cm.EndCurrentEdit()
            If Not IsNothing(cmb_campusid.SelectedRow) Then
                PostPeriodId = ObjIVProc.oMasterFICO.GetPostPeriodID(cmb_campusid.SelectedRow.Cells("CompanyID").Value, dt_InvoiceDate.Value)
            End If
            myRow("PostPeriodId") = PostPeriodId

            If frmMode = EnumfrmMode.acAddM And myUtils.IsInList(myUtils.cStrTN(cmb_BillOf.Value), "C") Then
                txt_InvoiceNum.Text = ObjVouch.GetNewSerialNo("IP", "", myRow.Row.Table.Rows(0), Nothing)
            End If

            objAccVouch = ObjIVProc.GenerateAccVouch(myRow.Row)

            Using dbTrans As SqlClient.SqlTransaction = myApp.dbConn.BeginTransaction
                Try

                    myRow("Balance") = myRow("AmountTot")
                    myRow("BalanceDate") = myRow("InvoiceDate")
                    SQLHelper2.SaveResults(myRow.Row.Table, Me.sqlForm, dbTrans)
                    frmIDX = myRow("InvoicePurchID")

                    Me.myViewVouch.mainGrid.SaveChanges(, "InvoicePurchID = " & frmIDX, dbTrans)

                    myUtils.ChangeAll(dsForm.Tables("InvoiceMVItem").Select, "InvoicePurchID=" & frmIDX)
                    SQLHelper2.SaveResults(dsForm.Tables("InvoiceMVItem"), "Select * from InvoiceMVItem", dbTrans)

                    If objAccVouch.VSave(dbTrans) AndAlso frmMode = EnumfrmMode.acAddM Then
                        myRow("AccVouchId") = objAccVouch.dtVouch.Rows(0)("AccVouchId")
                        SQLHelper2.SaveResults(myRow.Row.Table, Me.sqlForm, dbTrans)
                    End If

                    frmMode = EnumfrmMode.acEditM
                    dbTrans.Commit()
                    VSave = True
                Catch e As Exception
                    MsgBox(e.Message)
                    dbTrans.Rollback()
                    myWinForms.AddError(Me.btnOK, "This voucher could not be saved" & vbCrLf & " Error was " & e.Message & vbCrLf & e.StackTrace)
                End Try
            End Using
        End If
        Me.Refresh()
    End Function

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

        If Not myUtils.NullNot(cmb_campusid.Value) And Not myUtils.NullNot(cmb_VendorID.Value) Then
            Dim ds As DataSet = ObjIVProc.GenerateMatVouchList(cmb_campusid.Value, "V", cmb_VendorID.Value, myViewVouch.mainGrid.myDS.Tables(0), "ODNoteItemID Is Not NULL")

            dtMatVouchItem = ds.Tables(1)

            If UltraGridVoucherList.Rows.Count > 0 Then
                EnableControls(True)
            End If
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If UltraGridVoucherList.Rows.Count = 0 Then
            EnableControls(False)
        End If
    End Sub

    Private Sub btnDelAll_Click(sender As Object, e As EventArgs) Handles btnDelAll.Click
        myUtils.DeleteRows(myViewVouch.mainGrid.myDS.Tables(0).Select)

        If UltraGridVoucherList.Rows.Count = 0 Then
            EnableControls(False)
        End If
    End Sub

    Private Sub UltraGridVoucherList_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridVoucherList.AfterCellUpdate
        For i As Integer = 0 To UltraGridVoucherList.Rows.Count - 1
            If UltraGridVoucherList.Rows(i).Cells("Sysincl").Value = True Then
                For Each r2 As DataRow In dtMatVouchItem.Select("MatVouchID = " & UltraGridVoucherList.Rows(i).Cells("MatVouchID").Value & "")
                    Dim r3 As DataRow = myUtils.CopyOneRow(r2, Me.dsForm.Tables("InvoiceMVItem"))
                Next
            Else
                Dim rr2() As DataRow = Me.dsForm.Tables("InvoiceMVItem").Select("MatVouchID = " & myViewVouch.mainGrid.myGrid.ActiveRow.Cells("MatVouchID").Value & "")
                myUtils.DeleteRows(rr2)
            End If
        Next
    End Sub

    Private Sub UltraGridItemList_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridItemList.AfterCellUpdate
        Dim TotAmt, WVAmt As Decimal
        For i As Integer = 0 To UltraGridItemList.Rows.Count - 1
            If UltraGridItemList.Rows(i).Cells("Sysincl").Value = True Then
                TotAmt = TotAmt + UltraGridItemList.Rows(i).Cells("AmountTot").Value
                WVAmt = WVAmt + UltraGridItemList.Rows(i).Cells("AmountWV").Value
            End If
        Next

        txt_AmountTot.Text = myUtils.cValTN(TotAmt)
        txt_AmountWV.Text = myUtils.cValTN(WVAmt)
    End Sub

    Private Sub EnableControls(ByVal Bool As Boolean)
        cmb_campusid.ReadOnly = Bool
        cmb_VendorID.ReadOnly = Bool
    End Sub

    Private Sub cmb_InvoiceType_Leave(sender As Object, e As EventArgs) Handles cmb_InvoiceType.Leave
        HandleInvoiceType(cmb_InvoiceType.Value)
    End Sub

    Private Sub HandleInvoiceType(InvoiceType As String)
        If Not myUtils.NullNot(InvoiceType) Then
            If myUtils.IsInList(myUtils.cStrTN(InvoiceType), "QC") Then
                cmb_BillOf.Value = "P"
                txt_InvoiceNum.ReadOnly = False
            Else
                cmb_BillOf.Value = "C"
                txt_InvoiceNum.ReadOnly = True
            End If
        End If
    End Sub
End Class