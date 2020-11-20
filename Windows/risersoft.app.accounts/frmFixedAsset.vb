Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmFixedAsset
    Inherits frmMax
    Dim myViewDelta As New clsWinView, dvCamp As DataView

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        myView.SetGrid(UltraGridItemList)
        myViewDelta.SetGrid(UltraGridDelta)

        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmFixedAssetModel = Me.InitData("frmFixedAssetModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "AssetClass", "", Me.cmb_AssetClass)
            myWinSQL.AssignCmb(Me.dsCombo, "AssetType", "", Me.cmb_AssetType)


            If myView.mainGrid.myDS.Tables(0).Rows.Count > 0 Then
                cmb_campusid.ReadOnly = True
            End If

            Dim IsAIM As Boolean = myUtils.IsInList(myUtils.cStrTN(myRow("assettype")), "AIM")
            Me.txt_InitialValue.ReadOnly = (Not IsAIM)
            Me.dt_CapitalizedOn.ReadOnly = (Not IsAIM)
            Me.txt_OpeningValue.ReadOnly = (IsAIM)
            Me.dt_OpeningDate.ReadOnly = (IsAIM)

            HandleDate(myUtils.cDateTN(myRow("PurchDate"), DateTime.MinValue))
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("ItemList"))
            myViewDelta.PrepEdit(Me.Model.GridViews("Delta"))
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
        Dim frm As New frmFixedAssetItem
        If frm.PrepForm(Me.myView, EnumfrmMode.acAddM, "", "<PARAMS FixedAssetID=""" & frmIDX & """/>") Then
            frm.ShowDialog()
            Dim oRet As clsProcOutput = Me.GenerateIDOutput("asset", frmIDX)
            If oRet.Success Then
                Me.UpdateViewData(myView, oRet)
            Else
                MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
            End If
            oRet = Me.GenerateIDOutput("delta", frmIDX)
            If oRet.Success Then
                Me.UpdateViewData(myViewDelta, oRet)
            Else
                MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
            End If
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If Not myView.mainGrid.myGrid.ActiveRow Is Nothing Then
            If MsgBox("Are you sure you want to Delete", MsgBoxStyle.YesNo + MsgBoxStyle.Question, myWinApp.Vars("AppName")) = MsgBoxResult.Yes Then
                Dim r1 As DataRow = win.myWinUtils.DataRowFromGridRow(MyBase.myView.mainGrid.myGrid.ActiveRow)
                Dim oRet As clsProcOutput = Me.GenerateIDOutput("delete", r1("fixedassetitemid"))
                If oRet.Success Then
                    oRet = Me.GenerateIDOutput("asset", frmIDX)
                    If oRet.Success Then
                        Me.UpdateViewData(myView, oRet)
                    Else
                        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
                    End If
                    oRet = Me.GenerateIDOutput("delta", frmIDX)
                    If oRet.Success Then
                        Me.UpdateViewData(myViewDelta, oRet)
                    Else
                        MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
                    End If
                Else
                    MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
                End If
            End If
        Else
            MsgBox("Select Fixed Asset Tranasaction.", MsgBoxStyle.Information, myWinApp.Vars("AppName"))
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Not Me.myView.mainGrid.myGrid.ActiveRow Is Nothing Then
            Dim frm As New frmFixedAssetItem
            If frm.PrepForm(Me.myView, EnumfrmMode.acEditM, Me.myView.mainGrid.myGrid.ActiveRow.Cells("FixedAssetItemID").Value) Then
                frm.ShowDialog()
                Dim oRet As clsProcOutput = Me.GenerateIDOutput("asset", frmIDX)
                If oRet.Success Then
                    Me.UpdateViewData(myView, oRet)
                Else
                    MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
                End If
                oRet = Me.GenerateIDOutput("delta", frmIDX)
                If oRet.Success Then
                    Me.UpdateViewData(myViewDelta, oRet)
                Else
                    MsgBox(oRet.Message, MsgBoxStyle.Information, myWinApp.Vars("appname"))
                End If
            End If
        End If
    End Sub

    Private Sub dt_PurchDate_Leave(sender As Object, e As EventArgs) Handles dt_PurchDate.Leave, dt_PurchDate.AfterCloseUp
        HandleDate(dt_PurchDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID")
    End Sub
End Class