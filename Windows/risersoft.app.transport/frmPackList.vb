Imports System.ComponentModel

Public Class frmPackList
    Inherits frmMax
    Friend fParentMat, fParent As frmMax, fParentID As String
    Public oSort As clsWinSort2

    Private Sub InitForm()
        myView.SetGrid(UltraGridItems)
    End Sub

    Public Sub InitSort(strFilter As String)
        oSort = New clsWinSort2(myView, Me.btnUp, Me.btnDown, Me.btnLeft, Me.btnRight, Me.btnRenumber, "SerialNum", "SubSerialNum")
        oSort.strFilter = strFilter
        oSort.renumber()
    End Sub

    Private Sub btnAddSerial_Click(sender As Object, e As EventArgs) Handles btnAddSerial.Click
        If fParentID.Trim.Length > 0 Then
            Dim r1 As DataRow = myTables.AddNewRow(myView.mainGrid.myDv.Table)
            r1("serialnum") = myUtils.MaxVal(myView.mainGrid.myDv.Table, "serialnum") + 1
            r1(fParentID) = fParent.myRow(fParentID)
        End If
    End Sub

    Private Sub btnAddSubSerial_Click(sender As Object, e As EventArgs) Handles btnAddSubSerial.Click
        If fParentID.Trim.Length > 0 Then
            Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
            If r1 Is Nothing Then
                MsgBox("Select a Serial No.", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
            Else
                Dim r2 As DataRow = myTables.AddNewRow(myView.mainGrid.myDv.Table)
                r2("serialnum") = r1("serialnum")
                r2("subserialnum") = myUtils.MaxVal(myView.mainGrid.myDv.Table.Select("serialnum=" & r1("serialnum")), "subserialnum") + 1
                r2(fParentID) = fParent.myRow(fParentID)
            End If
        End If
    End Sub

    Private Sub btnDelItem_Click(sender As Object, e As EventArgs) Handles btnDelItem.Click
        If Not myView.mainGrid.myGrid.ActiveRow Is Nothing Then
            myView.mainGrid.ButtonAction("del")
            oSort.renumber()
        End If
    End Sub

    Private Sub UltraGridItems_BeforeRowDeactivate(sender As Object, e As CancelEventArgs) Handles UltraGridItems.BeforeRowDeactivate
        If Not myView.mainGrid.myGrid.ActiveRow Is Nothing Then
            If myUtils.cBoolTN(myView.mainGrid.myGrid.ActiveRow.Cells("IsMax").Value) = True Then
                For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select
                    r1("IsMax") = False
                Next
                myView.mainGrid.myGrid.ActiveRow.Cells("IsMax").Value = True
            End If
        End If
    End Sub

    Private Sub btnCopyFrom_Click(sender As Object, e As EventArgs) Handles btnCopyFrom.Click
        Dim Params, Params1, Params2 As New List(Of clsSQLParam)
        Dim rr() As DataRow = fParentMat.AdvancedSelect("odnote", Params)
        If (Not rr Is Nothing) AndAlso rr.Length > 0 Then
            Params1.Add(New clsSQLParam("@idfield", "'" & fParentID & "'", GetType(String), False))
            Params1.Add(New clsSQLParam("@odnoteid", myUtils.cValTN(rr(0)("ODNoteID")), GetType(Integer), False))
            Dim rr1() As DataRow = fParentMat.AdvancedSelect("odnotitemspare", Params1)
            If (Not rr1 Is Nothing) AndAlso rr1.Length > 0 Then

                Params2.Add(New clsSQLParam("@idfield", "'" & fParentID & "'", GetType(String), False))
                Params2.Add(New clsSQLParam("@idvalue", myUtils.cValTN(rr1(0)(fParentID)), GetType(Integer), False))
                Dim dt As DataTable = fParentMat.GenerateParamsOutput("odnotepack", Params2).Data.Tables(0)
                For Each r1 As DataRow In dt.Select
                    Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDv.Table, , , fParentID, myUtils.cValTN(fParent.myRow(fParentID)))
                    r2("ODNoteID") = DBNull.Value
                Next
                oSort.renumber()
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        If Not myView.mainGrid.myGrid.ActiveRow Is Nothing Then
            risersoft.app.mxform.myFuncs.PackListIDField = fParentID
            risersoft.app.mxform.myFuncs.PackListIDValue = myUtils.cValTN(myView.mainGrid.myGrid.ActiveRow.Cells(fParentID).Value)
        End If
    End Sub

    Private Sub btnPaste_Click(sender As Object, e As EventArgs) Handles btnPaste.Click
        If Not myUtils.IsInList(myUtils.cStrTN(risersoft.app.mxform.myFuncs.PackListIDField), "") Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@idfield", "'" & risersoft.app.mxform.myFuncs.PackListIDField & "'", GetType(String), False))
            Params.Add(New clsSQLParam("@idvalue", myUtils.cValTN(risersoft.app.mxform.myFuncs.PackListIDValue), GetType(Integer), False))
            Dim dt As DataTable = fParentMat.GenerateParamsOutput("odnotepack", Params).Data.Tables(0)
            For Each r1 As DataRow In dt.Select
                Dim r2 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDv.Table, , , fParentID, myUtils.cValTN(fParent.myRow(fParentID)))
                r2("ODNoteID") = DBNull.Value
            Next
            oSort.renumber()
            risersoft.app.mxform.myFuncs.PackListIDField = String.Empty
            risersoft.app.mxform.myFuncs.PackListIDValue = 0
        End If
    End Sub
End Class