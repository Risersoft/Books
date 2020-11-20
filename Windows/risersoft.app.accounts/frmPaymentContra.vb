Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmPaymentContra
    Inherits frmMax
    Friend fItem As frmPaymentContraItem
    Dim fPaymentMode As frmPaymentMode
    Dim PPFinal As Boolean = False, myViewAdv As New clsWinView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)

        fItem = New frmPaymentContraItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        fItem.Enabled = False

        fPaymentMode = New frmPaymentMode
        AddHandler fPaymentMode.PaymentModeChanged, AddressOf PaymentModeChanged
        fPaymentMode.AddtoTab(Me.UltraTabControl1, "Mode", True)

        myView.SetGrid(UltraGridItemList)
        myViewAdv.SetGrid(Me.UltraGridAdv)
    End Sub

    Private Sub PaymentModeChanged(sender As Object, PaymentMode As String, IgnoreExpenseVoucher As Boolean)
        If myUtils.IsInList(myUtils.cStrTN(PaymentMode), "IM") AndAlso IgnoreExpenseVoucher = False Then
            UltraTabControl2.Tabs("TA").Visible = True
            EnableCtl()
        Else
            UltraTabControl2.Tabs("TA").Visible = False
        End If
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPaymentContraModel = Me.InitData("frmPaymentContraModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If frmMode = EnumfrmMode.acAddM AndAlso cmb_CompanyID.Rows.Count = 1 Then myRow("CompanyID") = myUtils.cValTN(cmb_CompanyID.Rows(0).Cells("CompanyID").Value)
            fPaymentMode.InitPanel(Me, False)
            fPaymentMode.HandleItem(myUtils.cValTN(myRow("CompanyID")), myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))

            CalculateBalanceAdv()

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

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Items"))
            myViewAdv.PrepEdit(Me.Model.GridViews("Adv"))

            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            myWinSQL.AssignCmb(Me.dsCombo, "PaymentType", "", Me.cmb_PaymentType)

            fPaymentMode.BindModel(NewModel)
            fItem.BindModel(NewModel)
            fItem.fPaymentMode.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Table.Select.Length = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")
            fItem.Focus()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            fItem.FormPrepared = False
            fItem.Enabled = False
        End If
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        If fItem.PrepForm(r1) Then
            fItem.Enabled = True
        End If
    End Sub

    Private Sub UltraGridItemList_BeforeRowDeactivate(sender As Object, e As ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub cmb_CompanyID_Leave(sender As Object, e As EventArgs) Handles cmb_CompanyID.Leave, cmb_CompanyID.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleCampus()
    End Sub

    Private Sub HandleCampus()
        fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
        For Each r1 As DataRow In dsForm.Tables("PaymentItemContra").Select
            r1("CompanyID") = myUtils.cValTN(cmb_CompanyID.Value)
        Next
        If Not IsNothing(fItem.myRow) Then
            fItem.fPaymentMode.HandleItem(myUtils.cValTN(cmb_CompanyID.Value), myUtils.cDateTN(dt_Dated.Value, DateTime.MinValue))
        End If
    End Sub

    Private Sub txt_Remark_Leave(sender As Object, e As EventArgs) Handles txt_Remark.Leave
        cm.EndCurrentEdit()
        fPaymentMode.txt_PaymentInfo.Value = fPaymentMode.SetPaymentInfo(myUtils.cStrTN(Me.myRow("Remark")))
    End Sub

    Private Sub btnAddTA_Click(sender As Object, e As EventArgs) Handles btnAddTA.Click
        If (Not myUtils.NullNot(fPaymentMode.cmb_ImprestEmployeeID.Value)) AndAlso (Not myUtils.NullNot(cmb_CompanyID.Value)) Then
            If Not IsNothing(myViewAdv) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(fPaymentMode.cmb_ImprestEmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewAdv.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("TOURVOUCH", Params), Params2)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewAdv.mainGrid.myDS.Tables(0))
                        r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    Next
                    CalculateBalanceAdv()
                End If
            End If
            EnableCtl()
        End If
    End Sub

    Private Sub CalculateBalanceAdv()
        For Each r1 As DataRow In myViewAdv.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub btnDelTA_Click(sender As Object, e As EventArgs) Handles btnDelTA.Click
        myViewAdv.mainGrid.ButtonAction("del")
        EnableCtl()
    End Sub

    Private Sub btnCopyAmt_Click(sender As Object, e As EventArgs) Handles btnCopyAmt.Click
        Dim Key As String = "", OldInvoiceID As String = ""
        Dim dt As DataTable = myViewAdv.mainGrid.myDS.Tables(0)
        If Not IsNothing(dt) Then
            For Each r1 As DataRow In dt.Select("Amount is Null")
                r1("Amount") = myUtils.cValTN(r1("PreBalance"))
            Next
            CalculateBalanceAdv()
        End If
    End Sub

    Private Sub EnableCtl()
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            WinFormUtils.SetReadOnly(fPaymentMode, False, True)
        Else
            WinFormUtils.SetReadOnly(fPaymentMode, False, False)
        End If
    End Sub

    Private Sub UltraGridAdv_AfterCellUpdate(sender As Object, e As EventArgs) Handles UltraGridAdv.AfterCellUpdate
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            CalculateBalanceAdv()
        End If
    End Sub

    Public Sub AdvanceSelectTA()
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(myRow("Dated"), "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@EmployeeID", myUtils.cValTN(fItem.fPaymentMode.cmb_ImprestEmployeeID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@PaymentID", frmIDX, GetType(Integer), False))
        Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(myUtils.cValTN(myRow("CompanyID"))), GetType(Integer), False))
        Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(fItem.myView.mainGrid.myDS.Tables(0).Select, "TourVouchID"), GetType(Integer), True))
        Dim rr() As DataRow = Me.AdvancedSelect("AdvReq", Params)

        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            For Each r1 As DataRow In rr
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, fItem.myView.mainGrid.myDS.Tables(0))
            Next
            fItem.myView.mainGrid.myDS.AcceptChanges()
        End If
    End Sub

    Private Sub GetMissingVoucherNo()
        Dim FormatType As String = ""
        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "CA") Then FormatType = "C" Else FormatType = "J"
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@FormatType", "'" & myUtils.cStrTN(FormatType) & "'", GetType(String), False))
        Dim oRet As clsProcOutput = GenerateParamsOutput("missingdocsysnum", Params)
        If oRet.Success Then
            myRow("ForceAVSysNum") = oRet.ID
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub
End Class