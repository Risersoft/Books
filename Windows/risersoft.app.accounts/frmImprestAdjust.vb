Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmImprestAdjust
    Inherits frmMax
    Dim myViewAE As New clsWinView, oView As clsWinView = Nothing

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        myView.SetGrid(UltraGridExp)
        myViewAE.SetGrid(UltraGridAE)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmImprestAdjustModel = Me.InitData("frmImprestAdjustModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then

            CalculateBalanceAE()
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
            myView.PrepEdit(Me.Model.GridViews("Exp"))
            myViewAE.PrepEdit(Me.Model.GridViews("AE"))
            Return True
        End If
        Return False
    End Function

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

    Private Function CalculateGrid(oView1 As clsWinView, ColumnName As String) As Decimal
        Dim Amt As Decimal

        For Each r1 As DataRow In oView1.mainGrid.myDS.Tables(0).Select
            Amt = Amt + myUtils.cValTN(r1(ColumnName))
        Next
        Return Amt
    End Function

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        oView = SelectMyView()
        If Not IsNothing(oView) Then
            oView.mainGrid.ButtonAction("del")
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim SysID As String = "", oView1 As clsWinView = Nothing
        oView1 = SelectMyView(SysID)

        If myUtils.IsInList(myUtils.cStrTN(SysID), "AE") Then
            AdvanceSelectAE()
        End If
    End Sub


    Private Function SelectMyView(Optional ByRef SysID As String = "") As clsWinView
        oView = Nothing
        If Not IsNothing(UltraTabControl2.SelectedTab) Then
            Select Case myUtils.cStrTN(UltraTabControl2.SelectedTab.Key).Trim.ToUpper
                Case "Exp"
                    SysID = "Exp"
                    oView = myView
                Case "AE"
                    SysID = "AE"
                    oView = myViewAE
            End Select
        End If
        Return oView
    End Function

    Private Sub AdvanceSelectAE()
        If Not IsNothing(myViewAE) Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@tourvouchidcsv", myUtils.MakeCSV(myViewAE.mainGrid.myDS.Tables(0).Select(), "AdvanceVouchID"), GetType(Integer), True))
            Params.Add(New clsSQLParam("@EmployeeID", myUtils.cValTN(myRow("EmployeeID")), GetType(Integer), False))
            Dim Params2 As New List(Of clsSQLParam)
            Params2.Add(New clsSQLParam("@ID", frmIDX, GetType(Integer), False))
            Dim rr() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("ae", Params), Params2)
            If Not rr Is Nothing AndAlso rr.Length > 0 Then
                For Each r1 As DataRow In rr
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myViewAE.mainGrid.myDS.Tables(0))
                    r2("AdvanceVouchID") = myUtils.cValTN(r1("TourVouchID"))
                    r2("Remark") = "Adjust Voucher"
                Next
                CalculateBalanceAE()
            End If
        End If
    End Sub

    Private Sub CalculateBalanceAE()
        For Each r1 As DataRow In myViewAE.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - myUtils.cValTN(r1("Amount"))
        Next
    End Sub

    Private Sub UltraGridAE_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridAE.AfterCellUpdate
        If myViewAE.mainGrid.myDS.Tables(0).Select.Length > 0 Then
            CalculateBalanceAE()
        End If
    End Sub
End Class