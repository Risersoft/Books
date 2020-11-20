Imports Infragistics.Win.UltraWinGrid

Public Class frmPaymentItemDet
    Inherits frmMax
    Friend fMat As frmMax

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridDet)
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        If Me.GetSoftData(oView, prepMode, prepIDX) Then
            If myUtils.IsInList(oView.Model.Key, "PI") Then
                myView.PrepEdit(fMat.Model.GridViews("DetPI"),, btnDel)
            ElseIf myUtils.IsInList(oView.Model.Key, "DR") Then
                myView.PrepEdit(fMat.Model.GridViews("DetDR"),, btnDel)
            ElseIf myUtils.IsInList(oView.Model.Key, "RR") Then
                myView.PrepEdit(fMat.Model.GridViews("DetRR"),, btnDel)
            ElseIf myUtils.IsInList(oView.Model.Key, "RW") Then
                myView.PrepEdit(fMat.Model.GridViews("DetRW"),, btnDel)
            ElseIf myUtils.IsInList(oView.Model.Key, "OW") Then
                myView.PrepEdit(fMat.Model.GridViews("DetOW"),, btnDel)
            End If

            myView.mainGrid.myDv.RowFilter = "paymentitemid=" & frmIDX
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim r1 As DataRow = Me.Controller.Tables.AddNewRow(myView.mainGrid.myDv.Table)
        r1("paymentitemid") = frmIDX
    End Sub

    Public Overrides Function VSave() As Boolean
        VSave = False
        Me.InitError()
        Dim dt1 As DataTable = Me.Controller.Data.SelectDistinct(myView.mainGrid.myDv.Table, "valuationclass",,, myView.mainGrid.myDv.RowFilter)
        If dt1.Select.Length < myView.mainGrid.myGrid.Rows.Count Then
            WinFormUtils.AddError(Me.UltraGridDet, "Select Valid Valuation Class")
        End If
        If Me.CanSave Then
            VSave = True
        End If
        Return VSave
    End Function
End Class