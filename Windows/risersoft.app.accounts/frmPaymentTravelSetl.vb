Imports risersoft.app.mxform

Public Class frmPaymentTravelSetl
    Inherits frmMax
    Friend myViewTA As New clsWinView
    Dim dvEmp As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(Me.UltraGridTE)
        myViewTA.SetGrid(Me.UltraGridTA)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmPaymentTravelSetlModel = Me.InitData("frmPaymentTravelSetlModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            HandleDate(myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))

            CalculateAmount()
            CalculateBalanceAdv()
            EnableCtl()

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
            myView.PrepEdit(Me.Model.GridViews("TE"))
            myViewTA.PrepEdit(Me.Model.GridViews("TA"))

            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            dvEmp = myWinSQL.AssignCmb(Me.dsCombo, "EMP", "", Me.cmb_EmployeeID,, 2)

            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        cm.EndCurrentEdit()

        CalculateAmount()
        CalculateBalanceAdv()
        If Me.ValidateData() Then
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub EnableCtl()
        Dim Bool As Boolean = False
        If myViewTA.mainGrid.myDS.Tables(0).Select.Length > 0 OrElse myView.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            Bool = True
        End If
        cmb_CompanyID.ReadOnly = Bool
        btnSelect.Enabled = Not Bool
    End Sub

    Private Sub btnAddTA_Click(sender As Object, e As EventArgs) Handles btnAddTA.Click
        If Not myUtils.NullNot(cmb_EmployeeID.Value) AndAlso Not myUtils.NullNot(cmb_CompanyID.Value) Then
            If Not IsNothing(myViewTA) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(cmb_EmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewTA.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("ta", Params), Params2)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewTA.mainGrid.myDS.Tables(0))
                        r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    Next
                    CalculateBalanceAdv()
                End If
            End If

            EnableCtl()
        End If
    End Sub

    Private Sub CalculateBalanceAdv()
        For Each r1 As DataRow In myViewTA.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub btnDelTA_Click(sender As Object, e As EventArgs) Handles btnDelTA.Click
        myViewTA.mainGrid.ButtonAction("del")
        CalculateAmount()
        EnableCtl()
    End Sub

    Private Sub UltraGridTA_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridTA.AfterCellUpdate
        If myViewTA.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            CalculateBalanceAdv()
            CalculateAmount()
        End If
    End Sub

    Private Sub btnAddTE_Click(sender As Object, e As EventArgs) Handles btnAddTE.Click
        If Not myUtils.NullNot(cmb_EmployeeID.Value) AndAlso Not myUtils.NullNot(cmb_CompanyID.Value) Then
            If Not IsNothing(myView) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(cmb_EmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@paymentid", frmIDX, GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myView.mainGrid.myDS.Tables(0).Select(), "TourVouchID"), GetType(Integer), True))

                Dim rr() As DataRow = Me.AdvancedSelect("te", Params)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDS.Tables(0))
                    Next
                End If
            End If
            CalculateAmount()
            EnableCtl()
        End If
    End Sub

    Private Sub btnDelTE_Click(sender As Object, e As EventArgs) Handles btnDelTE.Click
        myView.mainGrid.ButtonAction("del")
        CalculateAmount()
        EnableCtl()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim r1 As DataRow = rEmp(Params)
        If Not IsNothing(r1) Then
            cmb_EmployeeID.Value = myUtils.cValTN(r1("EmployeeID"))
        End If
    End Sub

    Private Function rEmp(Params As List(Of clsSQLParam)) As DataRow
        Dim Model As clsViewModel = Me.GenerateParamsModel("employee", Params)
        Dim fg As New frmGrid, r1 As DataRow = Nothing
        fg.myView.PrepEdit(Model)
        fg.Size = New Drawing.Size(850, 600)
        If fg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            r1 = win.myWinUtils.DataRowFromGridRow(fg.myView.mainGrid.myGrid.ActiveRow)
        End If
        Return r1
    End Function

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleDate(dt_Dated.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvEmp.RowFilter = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "JoinDate", "LeaveDate", 36)
    End Sub

    Private Sub CalculateAmount()
        txt_AmountTotPay.Text = myView.mainGrid.Model.GetColSumProduct("Fac,TotalPayment", "") + (-1 * myViewTA.mainGrid.Model.GetColSumProduct("Fac,Amount", ""))
    End Sub

    Private Sub btnCopyAmt_Click(sender As Object, e As EventArgs) Handles btnCopyAmt.Click
        If Not IsNothing(myViewTA.mainGrid.myDS.Tables(0)) Then
            For Each r1 As DataRow In myViewTA.mainGrid.myDS.Tables(0).Select("Amount is Null")
                r1("Amount") = myUtils.cValTN(r1("PreBalance"))
            Next
            CalculateBalanceAdv()
            CalculateAmount()
        End If
    End Sub
End Class