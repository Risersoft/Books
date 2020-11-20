Imports risersoft.app.mxent
Imports System.Windows.Forms

Public Class clsGetRecordsMatVouch
    Public Overloads Function CalculateFieldType(FieldLevel As String, BoolSpSt As Boolean, rMVI As DataRow, oMaster As clsMasterDataMM, fieldName As String) As String
        Return Me.CalculateFieldType(FieldLevel, FieldLevel, BoolSpSt, rMVI, oMaster, fieldName)
    End Function

    Public Overloads Function CalculateFieldType(FieldLevelCheck As String, FieldLevelMC As String, BoolSpSt As Boolean, rMVI As DataRow, oMaster As clsMasterDataMM, fieldName As String) As String
        Dim sr As DataRow = Nothing, SpStType As String = ""
        If BoolSpSt = True Then SpStType = myUtils.cStrTN(rMVI("SpStock")) Else SpStType = ""

        If myUtils.IsInList(myUtils.cStrTN(FieldLevelCheck), "I") Then
            sr = oMaster.GetMvtCodeFldDataRow(myUtils.cValTN(rMVI("MvtCode")), myUtils.cStrTN(SpStType), FieldLevelMC, fieldName)
        Else
            For Each r2 In rMVI.Table.Select
                sr = oMaster.GetMvtCodeFldDataRow(myUtils.cValTN(r2("MvtCode")), SpStType, FieldLevelMC, fieldName)
                If Not IsNothing(sr) Then
                    If myUtils.IsInList(myUtils.cStrTN(sr("FieldType")), "R") Then
                        Exit For
                    End If
                End If
            Next
        End If
        If (Not sr Is Nothing) Then Return sr("fieldtype")
    End Function

    Private Sub SetFieldType(FieldType As String, InputControl As System.Windows.Forms.Control, LabelControl As System.Windows.Forms.Control, HideCols As Boolean)
        If Not IsNothing(InputControl) Then
            If myUtils.IsInList(myUtils.cStrTN(FieldType), "") Then
                If Not IsNothing(LabelControl) AndAlso HideCols = True Then
                    InputControl.Visible = False
                    LabelControl.Visible = False
                Else
                    WinFormUtils.SetReadOnly(InputControl, False, False)
                End If
            Else
                If Not IsNothing(LabelControl) AndAlso HideCols = True Then
                    If FieldType = "N" Then InputControl.Visible = False Else InputControl.Visible = True
                    If FieldType = "N" Then LabelControl.Visible = False Else LabelControl.Visible = True
                Else
                    If FieldType = "N" Then WinFormUtils.SetReadOnly(InputControl, False, False) Else WinFormUtils.SetReadOnly(InputControl, False, True)
                End If
            End If
        End If
    End Sub

    Public Function HandleFormField(oMaster As clsMasterDataMM, InputControl As Control, LabelControl As Control, rMVI As DataRow, FieldLevel As String, fieldName As String, BoolSpSt As Boolean, Optional HideCols As Boolean = False) As String
        Dim FieldType As String = CalculateFieldType(FieldLevel, BoolSpSt, rMVI, oMaster, fieldName)
        SetFieldType(FieldType, InputControl, LabelControl, HideCols)

        If (Not FieldType Is Nothing) Then Return FieldType
    End Function

    Public Sub HandleFormTab(TabPage As Infragistics.Win.UltraWinTabControl.UltraTab, Condition As Boolean)
        If Condition = True Then TabPage.Visible = True Else TabPage.Visible = False
    End Sub

    Public Function ShowGridItems(Model As clsViewModel, IDField As String) As DataTable
        Dim fG As New frmGrid, dt As New DataTable
        fG.myView.PrepEdit(Model)
        dt = fG.myView.mainGrid.myDS.Tables(0).Clone
        fG.Size = New Drawing.Size(850, 600)
        Do
            If fG.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim rr1() As DataRow = fG.myView.mainGrid.myDS.Tables(0).Select("sysincl=1 and isnull(Qty,0)>0")
                If rr1.Length = 0 Then
                    MsgBox("Selected item has zero qty. Please enter qty.")
                Else
                    For Each r1 In fG.myView.mainGrid.myDS.Tables(0).Select("sysincl=1")
                        win.myWinUtils.CopyOneGridRow(win.myWinUtils.FindRow(fG.myView.mainGrid.myGrid, IDField, r1(IDField)), dt)
                    Next
                    Exit Do
                End If
            Else
                Exit Do
            End If
        Loop
        Return dt
    End Function

    Public Function MvtCodeFilter(TaxType As String, dv As DataView) As String
        Dim CodeList As New List(Of String)
        For Each r1 As DataRow In dv.Table.Select
            Dim arr() As String = Split(r1("TaxTypeSrc"), ",")
            If myUtils.IsInList(TaxType, arr) Then
                CodeList.Add(r1("matmvtcode"))
            End If
        Next
        Dim str1 As String = myUtils.MakeCSV(CodeList, ",")
        If str1.Trim.Length = 0 Then str1 = "0"

        Dim str2 As String = "MatMvtCode in (" & str1 & ")"

        Return str2
    End Function
End Class