Imports Infragistics.Win.UltraWinGrid

Public Class frmCostAssign
    Inherits frmMax
    Public myViewWBS, myViewCost As New clsWinView
    Protected Friend fp, fMat As frmMax, ItemIDField, fpDateField As String, fpCampusID As Integer, ItemRow As DataRow

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(Me.UltraGridLot)
        myViewWBS.SetGrid(Me.UltraGridWBS)
        myViewCost.SetGrid(Me.UltraGridCost)
    End Sub

    Public Sub InitPanel(fParentItem As frmMax, fParentMat As frmMax, NewModel As clsFormDataModel, LotKey As String, WBSKey As String, CostKey As String)
        'To be called on prepform of voucher form
        Me.FormPrepared = False
        fMat = fParentMat
        fp = fParentItem
        Me.BindingContext = fp.BindingContext

        myView.PrepEdit(NewModel.GridViews(LotKey))
        myViewWBS.PrepEdit(NewModel.GridViews(WBSKey))
        myViewCost.PrepEdit(NewModel.GridViews(CostKey))


        Me.FormPrepared = True
    End Sub

    Public Sub HandleItem(IDFieldName As String, DateField As String, CampusID As Integer, rItem As DataRow)
        'to be called on prepform of voucher item form
        If Not IsNothing(fp) Then fp.cm.EndCurrentEdit()
        ItemIDField = IDFieldName
        fpDateField = DateField
        fpCampusID = CampusID
        ItemRow = rItem
    End Sub

    Private Function SelectMyView(Optional ByRef Key As String = "", Optional ByRef IDField As String = "") As clsWinView
        Dim Oview As clsWinView = Nothing
        If Not IsNothing(UltraTabControl1.SelectedTab) Then
            Key = myUtils.cStrTN(UltraTabControl1.SelectedTab.Key).Trim.ToLower
            Select Case Key
                Case "costlot"
                    Oview = myView
                    IDField = "ProdLotID"
                Case "costwbs"
                    Oview = myViewWBS
                    IDField = "WBSElementID"
                Case "costcenter"
                    Oview = myViewCost
                    IDField = "CostCenterID"
            End Select
        End If
        Return Oview
    End Function

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Dim Key As String = "", IDField As String = ""
        Dim Oview As clsWinView = SelectMyView(Key, IDField)
        If Not IsNothing(Oview) Then

            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@campusid", fpCampusID, GetType(Integer), False))
            Params.Add(New clsSQLParam("@Dated", Format(fMat.myRow(fpDateField), "dd-MMM-yyyy"), GetType(DateTime), False))
            Dim rr1() As DataRow = fMat.AdvancedSelect(Key, Params)
            If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
                For Each r2 As DataRow In rr1
                    Dim r3 As DataRow = myUtils.CopyOneRow(r2, Oview.mainGrid.myDv.Table)
                    r3(ItemIDField) = myUtils.cValTN(ItemRow(ItemIDField))
                    r3("IDField") = IDField
                Next
            End If
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim Oview As clsWinView = SelectMyView()
        If Not IsNothing(Oview) Then
            Oview.mainGrid.ButtonAction("del")
        End If
    End Sub

    Private Sub btnMakeEq_Click(sender As Object, e As EventArgs) Handles btnMakeEq.Click
        Dim Count As Integer
        For Each View As clsWinView In New clsWinView() {myView, myViewWBS, myViewCost}
            Count = Count + View.mainGrid.myDv.Table.Select(ItemIDField & " = " & myUtils.cValTN(ItemRow(ItemIDField))).Length
        Next

        If Count > 0 Then
            Dim PerValue As Double = 100 / Count
            For Each View As clsWinView In New clsWinView() {myView, myViewWBS, myViewCost}
                myUtils.ChangeAll(View.mainGrid.myDv.Table.Select(ItemIDField & " = " & myUtils.cValTN(ItemRow(ItemIDField))), "PerValue=" & PerValue)
            Next
        End If
    End Sub
End Class