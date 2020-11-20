Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmTourAdvReq
    Inherits frmMax
    Friend fItem As frmTourAdvReqItem, myViewAllow As New clsWinView
    Dim dvEmp, dvcamp As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(Me.UltraGridSD)
        myViewAllow.SetGrid(Me.UltraGridDD)

        Me.AddUEGB(Me.UEGB_Header, 2, Me.UEGB_ItemDetail.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)

        fItem = New frmTourAdvReqItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        fItem.Enabled = False
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmTourAdvReqModel = Me.InitData("frmTourAdvReqModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If frmMode = EnumfrmMode.acAddM Then
                myRow("Dated") = Now.Date
                fItem.dt_StartDate.Value = Now.Date
                fItem.dt_EndDate.Value = Now.Date
            End If

            If myUtils.cBoolTN(myRow("IsOpening")) Then
                TabCantrol.Tabs("OP").Visible = True
                TabCantrol.Tabs("AD").Visible = False
                TabCantrol.Tabs("SD").Visible = False
                txt_TotalAmount.ReadOnly = False
            Else
                txt_OpenAmountAdj.Value = DBNull.Value
                TabCantrol.Tabs("OP").Visible = False
                txt_TotalAmount.ReadOnly = True
            End If

            HandleDate(myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))

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
            myView.PrepEdit(Me.Model.GridViews("TourStation"))
            myViewAllow.PrepEdit(Me.Model.GridViews("TourAllow"))

            dvcamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID,, 2)
            dvEmp = myWinSQL.AssignCmb(Me.dsCombo, "EMP", "", Me.cmb_EmployeeID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID)

            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        cm.EndCurrentEdit()
        If (myUtils.cBoolTN(myRow("IsOpening")) OrElse myView.mainGrid.myDS.Tables(0).Select.Length = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            CalculateAmount()
            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridSD_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridSD.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub UltraGridSD_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridSD.AfterRowActivate
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)

        fItem.Enabled = True
        fItem.Focus()
    End Sub

    Private Sub btnAddSD_Click(sender As Object, e As EventArgs) Handles btnAddSD.Click
        Dim r1 As DataRow = myTables.AddNewRow(myView.mainGrid.myDS.Tables(0))

    End Sub

    Private Sub btnDelSD_Click(sender As Object, e As EventArgs) Handles btnDelSD.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            fItem.Enabled = False
        End If

        CalculateAmount()
    End Sub

    Private Sub CalculateAmount()
        If Not myUtils.cBoolTN(myRow("IsOpening")) Then
            Dim TravelFare, TotalAllow As Decimal
            For Each r1 As DataRow In myView.mainGrid.myDS.Tables(0).Select()
                TravelFare = myUtils.cValTN(TravelFare) + myUtils.cValTN(r1("TravelFare"))
            Next

            For Each r2 As DataRow In myViewAllow.mainGrid.myDS.Tables(0).Select()
                r2("Amount") = (myUtils.cValTN(r2("DaysNum")) * myUtils.cValTN(r2("DayRate")))
                TotalAllow = myUtils.cValTN(TotalAllow) + myUtils.cValTN(r2("Amount"))
            Next

            txt_TotalAmount.Text = TravelFare + TotalAllow

            myRow("TotalAmount") = txt_TotalAmount.Text
            myRow("TotalPayment") = txt_TotalAmount.Text
        End If
    End Sub

    Private Sub UltraGridDD_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridDD.AfterCellUpdate
        CalculateAmount()
    End Sub

    Private Sub TabCantrol_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles TabCantrol.ActiveTabChanged
        CalculateAmount()
    End Sub

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleDate(dt_Dated.Value)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim rr() As DataRow = Me.AdvancedSelect("employee", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            cmb_EmployeeID.Value = myUtils.cValTN(rr(0)("EmployeeID"))
        End If
    End Sub

    Private Sub HandleDate(dated As Date)
        dvcamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
        dvEmp.RowFilter = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "JoinDate", "LeaveDate", 12)
    End Sub

    Private Sub btnSelectCamp_Click(sender As Object, e As EventArgs) Handles btnSelectCamp.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim rr() As DataRow = Me.AdvancedSelect("campus", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            cmb_CampusID.Value = myUtils.cValTN(rr(0)("CampusID"))
        End If
    End Sub
End Class