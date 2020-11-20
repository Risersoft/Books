Imports risersoft.app.mxform

Public Class frmInvoiceOpen
    Inherits frmMax
    Dim dvDivision, dvCamp, dvDelCamp As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoiceOpenModel = Me.InitData("frmInvoiceOpenModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            dvDelCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_deliverycampusid,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Customer", "", Me.cmb_CustomerID)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
            dvDivision = myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID, , 2)

            If Not myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "") Then
                EnableCtl(myRow("DocType"))

                If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then lblSalesOrder.Text = Me.GenerateIDOutput("salesorder", myUtils.cValTN(myRow("SalesOrderID"))).Description
                If myUtils.cValTN(myRow("campusid")) > 0 Then cmb_campusid.Value = myUtils.cValTN(myRow("campusid"))
                If myUtils.cValTN(myRow("deliverycampusid")) > 0 Then cmb_deliverycampusid.Value = myUtils.cValTN(myRow("deliverycampusid"))

                HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))
                HandleCampus()

                If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                    btnSave.Enabled = False
                    btnOK.Enabled = False
                End If
            End If
            HideCtlApp(myFuncs.ProgramName(Me.Controller))

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myWinSQL.AssignCmb(NewModel.dsCombo, "TY", "", Me.cmb_Ty)
            Return True
        End If
        Return False
    End Function

    Private Sub HideCtlApp(ProductName As String)
        PanelSO.Visible = Not myUtils.IsInList(myUtils.cStrTN(ProductName), "BooksNirvana")
    End Sub

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()
        If Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub EnableCtl(DocType As String)
        cmb_VendorID.Visible = False
        lblVendor.Visible = False
        lblCustomer.Visible = False
        cmb_CustomerID.Visible = False
        If myUtils.IsInList(myUtils.cStrTN(DocType), "IP") Then
            cmb_VendorID.Visible = True
            lblVendor.Visible = True
            cmb_deliverycampusid.Value = DBNull.Value
            cmb_deliverycampusid.Visible = False
            lblDelCamp.Visible = False
        Else
            lblCustomer.Visible = True
            cmb_CustomerID.Visible = True
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        cm.EndCurrentEdit()
        If Not cmb_campusid.SelectedRow Is Nothing Then
            Dim Params As New List(Of clsSQLParam)
            If myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "IS") Then Params.Add(New clsSQLParam("@CustomerId", myUtils.cValTN(cmb_CustomerID.Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyId").Value), GetType(Integer), False))
            Params.Add(New clsSQLParam("@InvoiceDate", Format(dt_InvoiceDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
            Params.Add(New clsSQLParam("@SalesOrderID", myUtils.cValTN(myRow("SalesOrderID")), GetType(Integer), False))
            Dim rr1() As DataRow = Me.AdvancedSelect("salesorder", Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                myRow("SalesOrderID") = myUtils.cValTN(rr1(0)("SalesOrderID"))
                lblSalesOrder.Text = "Sales Order :- " & myUtils.cStrTN(rr1(0)("OrderNum")) & " / " & myUtils.cStrTN(rr1(0)("OrderDate"))
            End If
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myRow("SalesOrderID") = DBNull.Value
        lblSalesOrder.Text = "Select Sales Order"
    End Sub

    Private Sub HandleCampus()
        If myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "IS") Then
            dvDivision.RowFilter = myCommonUtils.FilterDivision(Me.Controller, Me.fRow, cmb_campusid, cmb_deliverycampusid)
            If frmMode = EnumfrmMode.acAddM AndAlso cmb_DivisionID.Rows.Count = 1 Then cmb_DivisionID.Value = myUtils.cValTN(cmb_DivisionID.Rows(0).Cells("DivisionID").Value)
            If cmb_DivisionID.SelectedRow Is Nothing Then cmb_DivisionID.Value = DBNull.Value
        End If
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
    End Sub
End Class