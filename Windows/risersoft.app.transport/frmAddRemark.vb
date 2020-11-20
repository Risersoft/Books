Public Class frmAddRemark
    Inherits frmMax
    Public ItemDescrip As String, AmountTot As Decimal
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
    End Sub

    Public Overrides Function PrepFormRow(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            If myUtils.cStrTN(ItemDescrip).Length > 0 Then
                txt1.Text = ItemDescrip
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        cm.EndCurrentEdit()
        myRow("Remark") = "Remarks: " &
                 vbCrLf & "1. Material covered under this Delivery Challan is ‘" & myUtils.cStrTN(txt1.Text) & "’, on which Tax liability on entire value of sale has already been discharged vide Tax Invoice No. " & myUtils.cStrTN(txt2.Text) & " copy attached." &
                 vbCrLf & "2. Value of consignment covered under this Delivery Challan, for insurance purposes is approximate Rs. " & myUtils.cValTN(AmountTot) & "/-."
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class