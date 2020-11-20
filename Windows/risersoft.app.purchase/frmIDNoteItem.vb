Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports System.Windows.Forms

Public Class frmIDNoteItem
    Inherits frmMax
    Friend fMat As frmIDNote
    Dim WithEvents ItemCodeSys As New clsCodeSystem

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridNoteHistory)
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myView.PrepEdit(fMat.Model.GridViews("NoteHistory"))

        ItemCodeSys.SetConf(NewModel.dsCombo.Tables("Items"), Me.cmb_ItemId, Me.cmbItemName, Me.cmb_BaseUnitID)
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.cmb_ItemId.Value = myRow("ItemID")
            ItemCodeSys.HandleCombo(Me.cmb_ItemId, EnumWantEvent.acForceEvent)
            myView.mainGrid.myDv.RowFilter = "PurItemID= " & myUtils.cValTN(myRow("PurItemID"))
            Me.FormPrepared = True
        End If

        Return Me.FormPrepared
    End Function

    Private Sub ItemCodeSys_ItemChanged() Handles ItemCodeSys.ItemChanged
        cm.EndCurrentEdit()

        myRow("ItemName") = myUtils.cStrTN(cmbItemName.Text)
    End Sub

    Public Overrides Function VSave() As Boolean
        VSave = False
        Me.InitError()

        If IsNothing(myRow) Then
            WinFormUtils.AddError(Me.txt_Qty, "Please Generate Transaction")
            Exit Function
        End If

        If myUtils.cValTN(txt_Qty.Value) <= 0 Then WinFormUtils.AddError(txt_Qty, "Please Enter Qty")
        Dim TotalQty As Decimal = CInt(txt_Qty.Value) + CInt(txtTotalQty.Value)
        If myUtils.cValTN(TotalQty) > myUtils.cValTN(myRow("TotalQty")) Then WinFormUtils.AddError(txt_Qty, "Quantity Can not be Greate Order Quantity")
        If Me.CanSave Then
            cm.EndCurrentEdit()
            VSave = True
        End If
    End Function

    Private Sub txt_Qty_Leave(sender As Object, e As EventArgs) Handles txt_Qty.Leave
        cm.EndCurrentEdit()
        fMat.CalcBalance()
    End Sub
End Class