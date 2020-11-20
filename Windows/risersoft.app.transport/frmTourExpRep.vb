Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform
Imports System.ComponentModel

Public Class frmTourExpRep
    Inherits frmMax
    Friend fItem As frmTourExpRepItem, fItemMem As frmTourExpRepMatItem, myViewDD, myViewHE, myViewLC, myViewMES, myViewAdv, myViewMEA, myViewTRI, myViewMEM, myViewTRO, myViewAdvReq As New clsWinView
    Dim dvEmp, dvcamp As DataView
    Friend WithEvents fCostAssign As risersoft.app.accounts.frmCostAssign

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        myView.SetGrid(Me.UltraGridJD)
        myViewDD.SetGrid(Me.UltraGridDD)
        myViewHE.SetGrid(Me.UltraGridHE)
        myViewLC.SetGrid(Me.UltraGridLC)
        myViewMES.SetGrid(Me.UltraGridMES)
        myViewMEA.SetGrid(Me.UltraGridMEA)
        myViewMEM.SetGrid(Me.UltraGridMEM)
        myViewAdv.SetGrid(Me.UltraGridAdv)
        myViewTRI.SetGrid(Me.UltraGridTRI)
        myViewTRO.SetGrid(Me.UltraGridTRO)
        myViewAdvReq.SetGrid(Me.UltraGridAdvReq)


        Me.AddUEGB(Me.UEGB_Header, 2, Me.UEGB_ItemDetail.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)
        Me.AddUEGB(Me.UEGB_Mat, 1)
        Me.AddUEGB(Me.UEGB_Cost, 1)

        fItem = New frmTourExpRepItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me
        fItem.Enabled = False

        fItemMem = New frmTourExpRepMatItem
        fItemMem.AddToPanel(Me.UltraExpandableGroupBoxPanel1, True, System.Windows.Forms.DockStyle.Fill)
        fItemMem.fMat = Me
        fItemMem.Enabled = False

        fCostAssign = New risersoft.app.accounts.frmCostAssign
        fCostAssign.AddToPanel(Me.UltraExpandableGroupBoxPanel4, True, System.Windows.Forms.DockStyle.Fill)

    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmTourExpRepModel = Me.InitData("frmTourExpRepModel", oView, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oView) Then
            If frmMode = EnumfrmMode.acAddM Then
                fItem.dt_DateFrom.Value = Now.Date
                fItem.dt_DateTo.Value = Now.Date
            End If

            If myUtils.cBoolTN(myRow("IsOpening")) Then
                txt_TotalAmount.ReadOnly = False
                TabCantrol.Tabs("JD").Visible = False
                TabCantrol.Tabs("DD").Visible = False
                TabCantrol.Tabs("HE").Visible = False
                TabCantrol.Tabs("LC").Visible = False
                TabCantrol.Tabs("MES").Visible = False
                TabCantrol.Tabs("MEA").Visible = False
                TabCantrol.Tabs("MEM").Visible = False
                TabCantrol.Tabs("ADV").Visible = False
                TabCantrol.Tabs("TRI").Visible = False
                TabCantrol.Tabs("TRO").Visible = False
            Else
                txt_TotalAmount.ReadOnly = True
            End If

            CalculateTotalAmount()
            CalculateLessAdvance()
            CalculateBalance()
            HandleDate(myUtils.cDateTN(myRow("Dated"), DateTime.MinValue))

            If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                EnableCtl(True)
            End If

            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If

            If (fCostAssign.myView.mainGrid.myDv.Table.Select.Length > 0) Or (fCostAssign.myViewCost.mainGrid.myDv.Table.Select.Length > 0) Or (fCostAssign.myViewWBS.mainGrid.myDv.Table.Select.Length > 0) Then
                UEGB_Cost.Visible = True
            Else
                UEGB_Cost.Visible = False
            End If

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("JD"))
            myViewDD.PrepEdit(Me.Model.GridViews("DD"))
            myViewHE.PrepEdit(Me.Model.GridViews("HE"))
            myViewLC.PrepEdit(Me.Model.GridViews("LC"))
            myViewMES.PrepEdit(Me.Model.GridViews("MES"))
            myViewMEA.PrepEdit(Me.Model.GridViews("MEA"))
            myViewMEM.PrepEdit(Me.Model.GridViews("MEM"))
            myViewAdv.PrepEdit(Me.Model.GridViews("Adv"))
            myViewTRI.PrepEdit(Me.Model.GridViews("TRI"))
            myViewTRO.PrepEdit(Me.Model.GridViews("TRO"))
            myViewAdvReq.PrepEdit(Me.Model.GridViews("AdvReq"))

            myViewMES.mainGrid.myDv.RowFilter = "ClassType = 'S'"
            myViewMEA.mainGrid.myDv.RowFilter = "ClassType = 'A'"
            myViewMEM.mainGrid.myDv.RowFilter = "ClassType = 'M'"

            myViewTRI.mainGrid.myDv.RowFilter = "ImprestEmployeeID is Not NULL"
            myViewTRO.mainGrid.myDv.RowFilter = "CashCampusID is Not NULL"

            dvcamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_CampusID,, 2)
            dvEmp = myWinSQL.AssignCmb(Me.dsCombo, "EMP", "", Me.cmb_EmployeeID,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "TourCountry", "", Me.cmb_TourCountry)
            myWinSQL.AssignCmb(Me.dsCombo, "Division", "", Me.cmb_DivisionID)

            fItemMem.BindModel(NewModel)

            fCostAssign.InitPanel(Me, Me, NewModel, "CostLot", "CostWBS", "CostCenter")
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False

        cm.EndCurrentEdit()
        If (myUtils.cBoolTN(myRow("IsOpening")) OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            CalculateTotalAmount()
            CalculateLessAdvance()
            CalculateBalance()
            SetStationDescrip()

            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridJD_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridJD.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnAddJD_Click(sender As Object, e As EventArgs) Handles btnAddJD.Click
        Dim r1 As DataRow = myTables.AddNewRow(myView.mainGrid.myDv.Table)
    End Sub

    Private Sub btnDelJD_Click(sender As Object, e As EventArgs) Handles btnDelJD.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            fItem.Enabled = False
        End If

        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Public Sub CalculateTotalAmount()
        If Not myUtils.cBoolTN(myRow("IsOpening")) Then
            Dim JourneyAmount, TotalDA, HotelExp, LocConv, MiscExpS, MiscExpA, ImpExp, MiscExpM, OfcExp As Decimal
            For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select
                JourneyAmount = myUtils.cValTN(JourneyAmount) + myUtils.cValTN(r1("Amount"))
            Next

            For Each r2 As DataRow In myViewDD.mainGrid.myDv.Table.Select
                TotalDA = myUtils.cValTN(TotalDA) + (myUtils.cValTN(r2("DaysNum")) * myUtils.cValTN(r2("DayRate")))
            Next

            For Each r3 As DataRow In myViewHE.mainGrid.myDv.Table.Select
                HotelExp = myUtils.cValTN(HotelExp) + myUtils.cValTN(r3("Amount"))
            Next

            For Each r4 As DataRow In myViewLC.mainGrid.myDv.Table.Select
                LocConv = myUtils.cValTN(LocConv) + myUtils.cValTN(r4("Amount"))
            Next

            For Each r5 As DataRow In myViewMES.mainGrid.myDv.Table.Select("ClassType = 'S'")
                MiscExpS = myUtils.cValTN(MiscExpS) + myUtils.cValTN(r5("Amount"))
            Next

            For Each r6 As DataRow In myViewMEA.mainGrid.myDv.Table.Select("ClassType = 'A'")
                MiscExpA = myUtils.cValTN(MiscExpA) + myUtils.cValTN(r6("Amount"))
            Next

            For Each r7 As DataRow In myViewMEM.mainGrid.myDv.Table.Select("ClassType = 'M'")
                MiscExpM = myUtils.cValTN(MiscExpM) + myUtils.cValTN(r7("Amount"))
            Next

            For Each r8 As DataRow In myViewTRI.mainGrid.myDv.Table.Select("ImprestEmployeeID is Not NULL")
                ImpExp = myUtils.cValTN(ImpExp) + myUtils.cValTN(r8("Amount"))
            Next

            For Each r9 As DataRow In myViewTRO.mainGrid.myDv.Table.Select("CashCampusID is Not NULL")
                OfcExp = myUtils.cValTN(OfcExp) + myUtils.cValTN(r9("Amount"))
            Next

            txt_TotalAmount.Text = JourneyAmount + TotalDA + HotelExp + LocConv + MiscExpS + MiscExpA + ImpExp + MiscExpM + OfcExp
            myRow("TotalAmount") = myUtils.cValTN(txt_TotalAmount.Text)
        End If
    End Sub

    Public Sub CalculateLessAdvance()
        If Not myUtils.cBoolTN(myRow("IsOpening")) Then
            Dim AdvAmount, AvAmountAdj As Decimal

            For Each r1 As DataRow In myViewAdv.mainGrid.myDS.Tables(0).Select
                AdvAmount = myUtils.cValTN(AdvAmount) + myUtils.cValTN(r1("Amount"))
            Next

            txt_LessAdvance.Text = myUtils.cValTN(AdvAmount)
            myRow("LessAdvance") = myUtils.cValTN(AdvAmount)

            AvAmountAdj = myUtils.cValTN(myRow("AvAmountAdj"))

            txt_TotalPayment.Text = myUtils.cValTN(txt_TotalAmount.Text) - (myUtils.cValTN(txt_LessAdvance.Text) + AvAmountAdj)
            myRow("TotalPayment") = myUtils.cValTN(txt_TotalPayment.Text)
        End If
    End Sub

    Private Sub btnAddAdv_Click(sender As Object, e As EventArgs) Handles btnAddAdv.Click
        If Not myUtils.NullNot(cmb_EmployeeID.Value) AndAlso Not myUtils.NullNot(cmb_CampusID.Value) Then
            If Not IsNothing(myViewAdv) Then
                Dim Params As New List(Of clsSQLParam)
                Params.Add(New clsSQLParam("@Dated", Format(dt_PostingDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
                Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CampusID.SelectedRow.Cells("companyid").Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@employeeid", myUtils.cValTN(cmb_EmployeeID.Value), GetType(Integer), False))
                Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewAdv.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))

                Dim Params2 As New List(Of clsSQLParam)
                Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
                Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("tourvouch", Params), Params2)
                If Not rr Is Nothing AndAlso rr.Length > 0 Then
                    For Each r1 As DataRow In rr
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewAdv.mainGrid.myDS.Tables(0))
                        r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    Next
                    CalculateBalance()
                End If
            End If

            If myViewAdv.mainGrid.myDS.Tables(0).Select.Length > 0 Then
                EnableCtl(True)
            End If
        End If
    End Sub

    Private Sub btnDelAdv_Click(sender As Object, e As EventArgs) Handles btnDelAdv.Click
        myViewAdv.mainGrid.ButtonAction("del")
        If myViewAdv.mainGrid.myDS.Tables(0).Select.Length = 0 Then
            EnableCtl(False)
        End If

        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub EnableCtl(bool As Boolean)
        cmb_EmployeeID.ReadOnly = bool
        btnSelectEmp.Enabled = Not bool
        btnSelectCamp.Enabled = Not bool
    End Sub

    Private Sub UltraGridJD_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridJD.AfterCellUpdate
        If UltraGridJD.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridDD_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridDD.AfterCellUpdate
        If UltraGridDD.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridHE_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridHE.AfterCellUpdate
        If UltraGridHE.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridLC_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridLC.AfterCellUpdate
        If UltraGridLC.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridMES_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridMES.AfterCellUpdate
        If UltraGridMES.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridMEA_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridMEA.AfterCellUpdate
        If UltraGridMEA.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridTRI_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridTRI.AfterCellUpdate
        If UltraGridTRI.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridTRO_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridTRO.AfterCellUpdate
        If UltraGridTRO.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
        End If
    End Sub

    Private Sub UltraGridAdv_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridAdv.AfterCellUpdate
        If UltraGridAdv.Rows.Count > 0 Then
            CalculateTotalAmount()
            CalculateLessAdvance()
            CalculateBalance()
        End If
    End Sub

    Private Sub btnAddDD_Click(sender As Object, e As EventArgs) Handles btnAddDD.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewDD.mainGrid.myDv.Table)
    End Sub

    Private Sub btnAddHE_Click(sender As Object, e As EventArgs) Handles btnAddHE.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewHE.mainGrid.myDv.Table)
    End Sub

    Private Sub btnAddLC_Click(sender As Object, e As EventArgs) Handles btnAddLC.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewLC.mainGrid.myDv.Table)
    End Sub

    Private Sub btnAddMES_Click(sender As Object, e As EventArgs) Handles btnAddMES.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewMES.mainGrid.myDv.Table)
        r1("ClassType") = "S"
        r1("TransType") = "SRV"
    End Sub

    Private Sub btnAddMEA_Click(sender As Object, e As EventArgs) Handles btnAddMEA.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewMEA.mainGrid.myDv.Table)
        r1("ClassType") = "A"
    End Sub

    Private Sub CalculateBalance()
        For Each r1 As DataRow In myViewAdv.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub btnDelDD_Click(sender As Object, e As EventArgs) Handles btnDelDD.Click
        myViewDD.mainGrid.ButtonAction("del")
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub btnDelHE_Click(sender As Object, e As EventArgs) Handles btnDelHE.Click
        myViewHE.mainGrid.ButtonAction("del")
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelectEmp.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim r1 As DataRow = rFind(Params, "employee")
        If Not IsNothing(r1) Then
            cmb_EmployeeID.Value = myUtils.cValTN(r1("EmployeeID"))
        End If
    End Sub

    Private Function rFind(Params As List(Of clsSQLParam), key As String) As DataRow
        Dim Model As clsViewModel = Me.GenerateParamsModel(key, Params)
        Dim fg As New frmGrid, r1 As DataRow = Nothing
        fg.myView.PrepEdit(Model)
        fg.Size = New Drawing.Size(850, 600)
        If fg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            r1 = win.myWinUtils.DataRowFromGridRow(fg.myView.mainGrid.myGrid.ActiveRow)
        End If
        Return r1
    End Function

    Private Sub btnDelLC_Click(sender As Object, e As EventArgs) Handles btnDelLC.Click
        myViewLC.mainGrid.ButtonAction("del")
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub btnDelMES_Click(sender As Object, e As EventArgs) Handles btnDelMES.Click
        myViewMES.mainGrid.ButtonAction("del")
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub btnDelMEA_Click(sender As Object, e As EventArgs) Handles btnDelMEA.Click
        myViewMEA.mainGrid.ButtonAction("del")
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub UltraGridMES_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridMES.BeforeRowDeactivate
        Dim str1 As String = myViewMES.mainGrid.ActiveRow.CellValue("ExpClass")
        Dim dt1 As DataTable = myViewMES.mainGrid.Model.dsLookup.Tables(0)
        Dim rr() As DataRow = dt1.Select("Class = '" & str1 & "'")
        If rr.Length = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub UltraGridMEA_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridMEA.BeforeRowDeactivate
        Dim str1 As String = myViewMEA.mainGrid.ActiveRow.CellValue("ExpClass")
        Dim dt1 As DataTable = myViewMEA.mainGrid.Model.dsLookup.Tables(0)
        Dim rr() As DataRow = dt1.Select("Class = '" & str1 & "'")
        If rr.Length = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub dt_Dated_Leave(sender As Object, e As EventArgs) Handles dt_Dated.Leave, dt_Dated.AfterCloseUp
        HandleDate(dt_Dated.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvcamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
        dvEmp.RowFilter = risersoft.app.mxform.myFuncs.FilterTimeDependent(dated, "JoinDate", "LeaveDate", 12)
    End Sub

    Private Sub btnAddTRI_Click(sender As Object, e As EventArgs) Handles btnAddTRI.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewTRI.mainGrid.myDv.Table)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim r2 As DataRow = rFind(Params, "employee")
        If Not IsNothing(r1) Then
            r1("ImprestEmployeeID") = myUtils.cValTN(r2("EmployeeID"))
        End If
    End Sub

    Private Sub btnDelTRI_Click(sender As Object, e As EventArgs) Handles btnDelTRI.Click
        myViewTRI.mainGrid.ButtonAction("del")
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub btnEditTRI_Click(sender As Object, e As EventArgs) Handles btnEditTRI.Click
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewTRI.mainGrid.myGrid.ActiveRow)
        If Not r1 Is Nothing Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
            Dim r2 As DataRow = rFind(Params, "employee")
            If Not IsNothing(r1) Then
                r1("ImprestEmployeeID") = myUtils.cValTN(r2("EmployeeID"))
            End If
        End If
    End Sub

    Private Sub UltraGridMEM_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridMEM.BeforeRowDeactivate
        If fItemMem.VSave Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnAddMEM_Click(sender As Object, e As EventArgs) Handles btnAddMEM.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewMEM.mainGrid.myDv.Table)
        r1("ClassType") = "M"
    End Sub

    Private Sub btnDelMEM_Click(sender As Object, e As EventArgs) Handles btnDelMEM.Click
        myViewMEM.mainGrid.ButtonAction("del")
        If myViewMEM.mainGrid.myDv.Table.Select.Length = 0 Then
            fItemMem.Enabled = False
        End If
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub btnAddTRO_Click(sender As Object, e As EventArgs) Handles btnAddTRO.Click
        Dim r1 As DataRow = myTables.AddNewRow(myViewTRO.mainGrid.myDv.Table)
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim rr() As DataRow = Me.AdvancedSelect("campus", Params)
        If Not rr Is Nothing AndAlso rr.Length > 0 Then
            r1("CashCampusID") = myUtils.cValTN(rr(0)("CampusID"))
        End If
    End Sub

    Private Sub btnDelTRO_Click(sender As Object, e As EventArgs) Handles btnDelTRO.Click
        myViewTRO.mainGrid.ButtonAction("del")
        CalculateTotalAmount()
        CalculateLessAdvance()
    End Sub

    Private Sub btnEditTRO_Click(sender As Object, e As EventArgs) Handles btnEditTRO.Click
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewTRO.mainGrid.myGrid.ActiveRow)
        If Not r1 Is Nothing Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
            Dim rr() As DataRow = Me.AdvancedSelect("campus", Params)
            If Not rr Is Nothing AndAlso rr.Length > 0 Then
                r1("CashCampusID") = myUtils.cValTN(rr(0)("CampusID"))
            End If
        End If
    End Sub

    Private Sub SetStationDescrip()
        Dim Str As String = "", OldStr As String = ""
        For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select

            If myUtils.IsInList(myUtils.cStrTN(r1("StationFrom")), OldStr) Then
                Str = myUtils.MakeCSV("-", Str, myUtils.cStrTN(r1("StationTo")))
            Else
                Dim str1 As String = myUtils.MakeCSV("-", myUtils.cStrTN(r1("StationFrom")), myUtils.cStrTN(r1("StationTo")))
                Str = myUtils.MakeCSV(",", Str, str1)
            End If
            OldStr = myUtils.cStrTN(r1("StationTo"))
        Next
        myRow("StationDescrip") = Str
    End Sub

    Private Sub btnSelectCamp_Click(sender As Object, e As EventArgs) Handles btnSelectCamp.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        'Dim rr() As DataRow = Me.AdvancedSelect("campus", Params)
        'If Not rr Is Nothing AndAlso rr.Length > 0 Then
        '    cmb_CampusID.Value = myUtils.cValTN(rr(0)("CampusID"))
        'End If

        Dim r1 As DataRow = rFind(Params, "campus")
        If Not IsNothing(r1) Then
            cmb_CampusID.Value = myUtils.cValTN(r1("CampusID"))
        End If

        CostAssignHandleItem(TabCantrol.ActiveTab.Key)
    End Sub

    Private Sub TabCantrol_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles TabCantrol.ActiveTabChanged
        CostAssignHandleItem(e.Tab.Key)
        RowFilter(e.Tab.Key)

        If e.Tab.Key = "ADV" Then
            UEGB_Cost.Visible = False
        Else
            If (fCostAssign.myView.mainGrid.myDv.Table.Select.Length > 0) Or (fCostAssign.myViewCost.mainGrid.myDv.Table.Select.Length > 0) Or (fCostAssign.myViewWBS.mainGrid.myDv.Table.Select.Length > 0) Then
                UEGB_Cost.Visible = True
            End If
        End If
    End Sub

    Private Sub UltraGridJD_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridJD.AfterRowActivate
        myView.mainGrid.myGrid.UpdateData()
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)

        If Not IsNothing(TabCantrol.SelectedTab) Then CostAssignHandleItem(TabCantrol.SelectedTab.Key)
        If Not IsNothing(TabCantrol.SelectedTab) Then RowFilter(TabCantrol.SelectedTab.Key)
        fItem.Enabled = True
        fItem.Focus()
    End Sub

    Private Sub UltraGridDD_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridDD.AfterRowActivate, UltraGridHE.AfterRowActivate, UltraGridLC.AfterRowActivate, UltraGridMES.AfterRowActivate, UltraGridMEA.AfterRowActivate, UltraGridTRO.AfterRowActivate
        If Not IsNothing(TabCantrol.SelectedTab) Then CostAssignHandleItem(TabCantrol.SelectedTab.Key)
        If Not IsNothing(TabCantrol.SelectedTab) Then RowFilter(TabCantrol.SelectedTab.Key)
    End Sub

    Private Sub UltraGridMEM_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridMEM.AfterRowActivate
        myViewMEM.mainGrid.myGrid.UpdateData()
        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewMEM.mainGrid.myGrid.ActiveRow)
        fItemMem.PrepForm(r1)

        If Not IsNothing(TabCantrol.SelectedTab) Then CostAssignHandleItem(TabCantrol.SelectedTab.Key)
        If Not IsNothing(TabCantrol.SelectedTab) Then RowFilter(TabCantrol.SelectedTab.Key)
        fItemMem.Enabled = True
        fItemMem.Focus()
    End Sub

    Private Sub CostAssignHandleItem(key As String)
        cm.EndCurrentEdit()
        If Me.FormPrepared AndAlso key = "JD" Then fCostAssign.HandleItem("TourJourneyID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "DD" Then fCostAssign.HandleItem("TourAllowID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewDD.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "HE" Then fCostAssign.HandleItem("TourHotelID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewHE.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "LC" Then fCostAssign.HandleItem("TourLoConvID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewLC.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "MES" Then fCostAssign.HandleItem("TourMiscExpID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewMES.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "MEA" Then fCostAssign.HandleItem("TourMiscExpID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewMEA.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "MEM" Then fCostAssign.HandleItem("TourMiscExpID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewMEM.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "TRI" Then fCostAssign.HandleItem("TourVouchItemID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewTRI.mainGrid.myGrid.ActiveRow))
        If Me.FormPrepared AndAlso key = "TRO" Then fCostAssign.HandleItem("TourVouchItemID", "Dated", myUtils.cValTN(myRow("CampusID")), win.myWinUtils.DataRowFromGridRow(myViewTRO.mainGrid.myGrid.ActiveRow))
    End Sub

    Private Sub RowFilter(key As String)
        Dim FieldName As String = "", ActiveRow As DataRow = Nothing

        If Me.FormPrepared AndAlso key = "JD" Then
            FieldName = "TourJourneyID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        End If
        If Me.FormPrepared AndAlso key = "DD" Then
            FieldName = "TourAllowID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewDD.mainGrid.myGrid.ActiveRow)
        End If
        If Me.FormPrepared AndAlso key = "HE" Then
            FieldName = "TourHotelID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewHE.mainGrid.myGrid.ActiveRow)
        End If
        If Me.FormPrepared AndAlso key = "LC" Then
            FieldName = "TourLoConvID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewLC.mainGrid.myGrid.ActiveRow)
        End If
        If Me.FormPrepared AndAlso key = "MES" Then
            FieldName = "TourMiscExpID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewMES.mainGrid.myGrid.ActiveRow)
        End If
        If Me.FormPrepared AndAlso key = "MEA" Then
            FieldName = "TourMiscExpID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewMEA.mainGrid.myGrid.ActiveRow)
        End If
        If Me.FormPrepared AndAlso key = "MEM" Then
            FieldName = "TourMiscExpID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewMEM.mainGrid.myGrid.ActiveRow)
        End If

        If Me.FormPrepared AndAlso key = "TRI" Then
            FieldName = "TourVouchItemID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewTRI.mainGrid.myGrid.ActiveRow)
        End If

        If Me.FormPrepared AndAlso key = "TRO" Then
            FieldName = "TourVouchItemID"
            ActiveRow = win.myWinUtils.DataRowFromGridRow(myViewTRO.mainGrid.myGrid.ActiveRow)
        End If

        If (Not IsNothing(fCostAssign.myView.mainGrid.myDv)) AndAlso (Not IsNothing(ActiveRow)) Then fCostAssign.myView.mainGrid.myDv.RowFilter = FieldName & " = " & myUtils.cValTN(ActiveRow(FieldName)) Else fCostAssign.myView.mainGrid.myDv.RowFilter = "0=1"
        If (Not IsNothing(fCostAssign.myViewWBS.mainGrid.myDv)) AndAlso (Not IsNothing(ActiveRow)) Then fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = FieldName & " = " & myUtils.cValTN(ActiveRow(FieldName)) Else fCostAssign.myViewWBS.mainGrid.myDv.RowFilter = "0=1"
        If (Not IsNothing(fCostAssign.myViewCost.mainGrid.myDv)) AndAlso (Not IsNothing(ActiveRow)) Then fCostAssign.myViewCost.mainGrid.myDv.RowFilter = FieldName & " = " & myUtils.cValTN(ActiveRow(FieldName)) Else fCostAssign.myViewCost.mainGrid.myDv.RowFilter = "0=1"
    End Sub

    Private Sub btnCostAssignment_Click(sender As Object, e As EventArgs) Handles btnCostAssignment.Click
        If UEGB_Cost.Visible = True Then UEGB_Cost.Visible = False Else UEGB_Cost.Visible = True
    End Sub

    Private Sub btnAddAdvReq_Click(sender As Object, e As EventArgs) Handles btnAddAdvReq.Click
        Me.InitError()

        If Not IsNothing(myViewTRI.mainGrid.myGrid.ActiveRow) Then
            Dim r3 As DataRow = win.myWinUtils.DataRowFromGridRow(myViewTRI.mainGrid.myGrid.ActiveRow)
            If myUtils.NullNot(Me.cmb_CampusID.Value) Then WinFormUtils.AddError(Me.cmb_CampusID, "Select Campus Name")

            If Me.CanSave() Then
                If Not IsNothing(myViewAdvReq) Then
                    Dim Params As New List(Of clsSQLParam)
                    Params.Add(New clsSQLParam("@Dated", Format(dt_Dated.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
                    Params.Add(New clsSQLParam("@EmployeeID", myUtils.cValTN(r3("ImprestEmployeeID")), GetType(Integer), False))
                    Params.Add(New clsSQLParam("@TransferTourVouchID", frmIDX, GetType(Integer), False))
                    Params.Add(New clsSQLParam("@CompanyID", myUtils.cValTN(cmb_CampusID.SelectedRow.Cells("companyid").Value), GetType(Integer), False))
                    Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewAdvReq.mainGrid.myDS.Tables(0).Select, "TourVouchID"), GetType(Integer), True))
                    Dim rr() As DataRow = Me.AdvancedSelect("AdvReq", Params)

                    If Not rr Is Nothing AndAlso rr.Length > 0 Then
                        For Each r1 As DataRow In rr
                            Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewAdvReq.mainGrid.myDS.Tables(0))
                        Next
                        myViewAdvReq.mainGrid.myDS.AcceptChanges()
                    End If
                End If
            End If
            CheckAdvReqData()
        End If
    End Sub

    Private Sub btnDelAdvReq_Click(sender As Object, e As EventArgs) Handles btnDelAdvReq.Click
        If Not IsNothing(myViewAdvReq) Then
            myViewAdvReq.mainGrid.ButtonAction("del")
            CheckAdvReqData()
        End If
    End Sub

    Private Sub CheckAdvReqData()
        Dim rr1() As DataRow = myViewAdvReq.mainGrid.myDS.Tables(0).Select("EmployeeID = " & myViewTRI.mainGrid.myGrid.ActiveRow.Cells("ImprestEmployeeID").Value)
        If Not IsNothing(rr1) AndAlso rr1.Length > 0 Then
            btnEditTRI.Enabled = False
        Else
            btnEditTRI.Enabled = True
        End If
    End Sub

    Private Sub UltraGridTRI_AfterRowActivate(sender As Object, e As EventArgs) Handles UltraGridTRI.AfterRowActivate
        If Not IsNothing(TabCantrol.SelectedTab) Then CostAssignHandleItem(TabCantrol.SelectedTab.Key)
        If Not IsNothing(TabCantrol.SelectedTab) Then RowFilter(TabCantrol.SelectedTab.Key)

        CheckAdvReqData()
    End Sub
End Class