Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.mxform

Public Class frmAccVouch
    Inherits frmMax
    Friend fItem As frmAccVouchItem

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_ItemDetail.Name)
        Me.AddUEGB(Me.UEGB_ItemDetail, 1)

        fItem = New frmAccVouchItem
        fItem.AddToPanel(Me.UltraExpandableGroupBoxPanel2, True, System.Windows.Forms.DockStyle.Fill)
        fItem.fMat = Me

        fItem.Enabled = False
        myView.SetGrid(Me.UltraGridItemList)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmAccVouchModel = Me.InitData("frmAccVouchModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            If frmMode = EnumfrmMode.acAddM Then If cmb_CompanyID.Rows.Count = 1 Then myRow("CompanyID") = myUtils.cValTN(cmb_CompanyID.Rows(0).Cells("CompanyID").Value)
            FillDrCrTotalAmount()

            fItem.HandleDate(myUtils.cDateTN(myRow("VouchDate"), DateTime.MinValue))
            HandleVouchDate(myUtils.cDateTN(myRow("VouchDate"), DateTime.MinValue))

            If frmMode = EnumfrmMode.acEditM Then
                myRow("forcesysnum") = DBNull.Value
                ChkGetMissingNo.Visible = False
            End If

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
            myView.PrepEdit(Me.Model.GridViews("Items"))
            myWinSQL.AssignCmb(Me.dsCombo, "Company", "", Me.cmb_CompanyID)
            myWinSQL.AssignCmb(Me.dsCombo, "AccVouchType", "", Me.cmb_AccVouchType)
            myWinSQL.AssignCmb(Me.dsCombo, "AdjustType", "", Me.cmb_AdjustType)

            fItem.BindModel(NewModel)
            Return True
        End If
        Return False
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        FillDrCrTotalAmount()
        cm.EndCurrentEdit()
        If (myView.mainGrid.myDv.Count = 0 OrElse fItem.VSave) AndAlso Me.ValidateData() Then
            If ChkGetMissingNo.Checked Then
                GetMissingVoucherNo()
            End If

            If Me.SaveModel() Then
                Return True
            End If
        Else
            Me.SetError()
        End If
        Me.Refresh()
    End Function

    Private Sub UltraGridItemList_BeforeRowDeactivate(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles UltraGridItemList.BeforeRowDeactivate
        If fItem.VSave Then
        Else
            e.Cancel = True
        End If
        FillDrCrTotalAmount()
    End Sub

    Private Sub UltraGridItemList_AfterRowActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UltraGridItemList.AfterRowActivate
        Me.InitError()
        myView.mainGrid.myGrid.UpdateData()

        Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
        fItem.PrepForm(r1)
        fItem.Enabled = True
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If myView.mainGrid.myDv.Table.Select.Length = 0 OrElse fItem.VSave Then
            Dim gr As UltraGridRow
            gr = myView.mainGrid.ButtonAction("add")

            fItem.Focus()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        If myView.mainGrid.myDv.Table.Select.Length = 0 Then
            fItem.Enabled = False
        End If
    End Sub

    Public Function FillDrCrTotalAmount()
        If myView.mainGrid.myDv.Table.Rows.Count > 0 Then
            txtDrAmount.Value = DBNull.Value
            txtCrAmount.Value = DBNull.Value

            For Each r1 As DataRow In myView.mainGrid.myDv.Table.Select
                If myUtils.cStrTN(r1("AmountDC")) = "D" Then
                    txtDrAmount.Value = myUtils.cValTN(txtDrAmount.Value) + myUtils.cValTN(r1("Amount"))
                ElseIf myUtils.cStrTN(r1("AmountDC")) = "C" Then
                    txtCrAmount.Value = myUtils.cValTN(txtCrAmount.Value) + myUtils.cValTN(r1("Amount"))
                End If
            Next
            cm.EndCurrentEdit()

        End If
        Return True
    End Function

    Private Sub HandleVouchDate(Dated As Date)
        Dim rr() As DataRow = Me.Controller.CommonData.FinYearTable.Select("EnDate = '" & Format(Dated, "dd-MMM-yyyy") & "'")
        If IsNothing(rr) OrElse rr.Length = 0 Then
            PanelClVouch.Visible = False
        Else
            PanelClVouch.Visible = True
            fItem.dvCamp.RowFilter = ""
            fItem.dvEmp.RowFilter = ""
        End If
    End Sub

    Private Sub dt_VouchDate_Leave(sender As Object, e As EventArgs) Handles dt_VouchDate.Leave, dt_VouchDate.AfterCloseUp
        fItem.HandleDate(dt_VouchDate.Value)
        HandleVouchDate(dt_VouchDate.Value)
    End Sub

    Private Sub btnGenClEntry_Click(sender As Object, e As EventArgs) Handles btnGenClEntry.Click
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@AdjustType", "'" & myUtils.cStrTN(cmb_AdjustType.Value) & "'", GetType(String), False))
        Params.Add(New clsSQLParam("@dated", Format(dt_VouchDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Dim oRet As clsProcOutput = GenerateParamsOutput("generate", Params)
        If oRet.Success Then
            myUtils.DeleteRows(myView.mainGrid.myDv.Table.Select)
            For Each r1 As DataRow In oRet.Data.Tables(0).Select()
                Dim r3 As DataRow = myUtils.CopyOneRow(r1, myView.mainGrid.myDv.Table)
            Next
            FillDrCrTotalAmount()
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If
    End Sub

    Private Sub GetMissingVoucherNo()
        Dim oMap As New clsAccountMap(Me.Controller)
        Dim FormatType As String = ""
        Dim GLAccID As Integer = oMap.FindGLAccountID(myRow("VouchDate"), "CSH", "D", "CA", "")
        If myView.mainGrid.myDv.Table.Select("GLAccountID=" & GLAccID).Length > 0 Then FormatType = "C" Else FormatType = "J"

        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CompanyId", myUtils.cValTN(cmb_CompanyID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Dated", Format(dt_VouchDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@FormatType", "'" & myUtils.cStrTN(FormatType) & "'", GetType(String), False))
        Dim oRet As clsProcOutput = GenerateParamsOutput("missingdocsysnum", Params)
        If oRet.Success Then
            myRow("forcesysnum") = oRet.ID
        Else
            MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
        End If

    End Sub
End Class
