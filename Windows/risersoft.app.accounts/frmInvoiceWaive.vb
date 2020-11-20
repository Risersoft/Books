Imports risersoft.app.mxform

Public Class frmInvoiceWaive
    Inherits frmMax
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)

        myView.SetGrid(UltraGridWaive)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoiceWaiveModel = Me.InitData("frmInvoiceWaiveModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            CalcWaivedAmt()
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Waive"))
            myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid)
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        myView.mainGrid.ButtonAction("Add")
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        CalcWaivedAmt()
    End Sub

    Private Sub CalcWaivedAmt()
        Dim WaivedAmt As Decimal
        cm.EndCurrentEdit()
        WaivedAmt = myUtils.cValTN(myView.mainGrid.Model.GetColSum("Amount", ""))
        myRow("PostBalance") = myUtils.cValTN(myRow("PreBalance")) - myUtils.cValTN(WaivedAmt)
        txtWaivedAmt.Value = WaivedAmt
    End Sub

    Private Sub UltraGridWaive_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridWaive.AfterCellUpdate
        If myView.mainGrid.myDS.Tables(0).Rows.Count > 0 Then
            CalcWaivedAmt()
        End If
    End Sub
End Class