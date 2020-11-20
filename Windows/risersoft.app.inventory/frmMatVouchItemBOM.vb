Imports risersoft.app.shared
Imports risersoft.app.mxent

Public Class frmMatVouchItemBOM
    Inherits frmMax
    Friend fMatItem As frmMatVouchItem, fItemSelect As frmItemSelect, fItemRes As frmMatVouchRes
    Dim dv, dvQtyDes, dvQtySrc, dvProdLotID, dvStStage, dvSpSt, dvTTyDes As DataView, oMasterMM As clsMasterDataMM, StockItemID As Integer, myViewStBalCamp As New clsWinView
    Dim WithEvents MvtCodeSystem As New clsCodeSystem

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        myView.SetGrid(UltraGridStBalDep)
        myViewStBalCamp.SetGrid(UltraGridStBalCamp)
        fItemRes = New frmMatVouchRes
        fItemRes.AddtoTab(Me.UltraTabControl1, "Reservation", True)

        fItemSelect = New frmItemSelect
        fItemSelect.AddtoTab(Me.UltraTabControl1, "Material", True)
    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        dv = New DataView(NewModel.dsCombo.Tables("MatMvtCode"))
        MvtCodeSystem.SetConf(dv, Me.cmb_MvtCode, cmbMvtCode)


        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitIDEntry)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_ItemUnitID2)
        myWinSQL.AssignCmb(NewModel.dsCombo, "Units", "", Me.cmb_OrderRateUnitID)

        dvSpSt = myWinSQL.AssignCmb(NewModel.dsCombo, "SpStock", "", Me.cmb_SpStock, , 2)
        dvQtyDes = myWinSQL.AssignCmb(NewModel.dsCombo, "QtyTypeDes", "", Me.cmb_QtyTypeDes, , 2)
        dvQtySrc = myWinSQL.AssignCmb(NewModel.dsCombo, "QtyTypeSrc", "", Me.cmb_QtyTypeSrc, , 2)
        dvStStage = myWinSQL.AssignCmb(NewModel.dsCombo, "StockStage", "", Me.cmb_StockStage, , 2)
        dvTTyDes = myWinSQL.AssignCmb(NewModel.dsCombo, "TaxType2", "", Me.cmb_TaxType2, , 2)

        fItemRes.myViewIssue.PrepEdit(NewModel.GridViews("ResIssueBom"), , fItemRes.btnDelIssue)
        fItemRes.myView.PrepEdit(NewModel.GridViews("ResRecBom"), , fItemRes.btnDelRec)

        fItemRes.BindModel(NewModel)
        fItemSelect.BindModel(NewModel)

        oMasterMM = fMatItem.oMasterMM
        Return True
    End Function

    Public Overloads Function PrepForm(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False

        If Me.BindData(r1) Then
            Me.FormPrepared = True
            fItemSelect.dv.RowFilter = ""
            fItemSelect.HandleItem()
            ControlStatus()

            Dim r3 As DataRow = fMatItem.oMasterMM.GetMvtCodeSPDataRow(fMatItem.cmb_MvtCode.Value, fMatItem.cmb_SpStock.Value)
            If Not IsDBNull(r3.Item("BOMMvtCode")) AndAlso Not IsNothing(r3.Item("BOMMvtCode")) Then
                dv.RowFilter = "MatMvtCode in (" & r3.Item("BOMMvtCode") & ")"
            End If

            If Not myUtils.NullNot(cmb_MvtCode.Value) Then
                cmbMvtCode.Value = cmb_MvtCode.Value
                MvtCodeLeaveEvent()
                SpStockLeaveEvent()
                HandleMatMvtCode()
            Else
                fItemSelect.dv.RowFilter = "0=1"
            End If

            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
        End If
        Return Me.FormPrepared
    End Function

    Private Sub SetItemFilter(ByVal IDField As String, ByVal str As String)
        Dim dtItem As DataTable = Nothing
        Dim rr() As DataRow = New DataRow() {}
        If Not IsNothing(dtItem) Then
            rr = dtItem.Select("" & IDField & " in (" & str & ")")
        End If
        If rr.Length = 0 Then
            Dim Params As New List(Of clsSQLParam)
            Params.Add(New clsSQLParam("@idcsv", str, GetType(Integer), True))
            Dim oRet As clsProcOutput = Me.fMatItem.fMat.Model.GenerateParamsOutput(IDField, Params)
            If oRet.Success Then
                dtItem = oRet.Data.Tables(0)
                rr = dtItem.Select
            End If
        End If

        Dim ItemIDCSV As String = myUtils.MakeCSV(rr, "ItemID")
        fItemSelect.dv.RowFilter = "ItemID in (" & ItemIDCSV & ")"
    End Sub

    Private Sub HandleMatMvtCode()
        If Not IsDBNull(fMatItem.cmb_MvtCode.Value) AndAlso Not IsNothing(fMatItem.cmb_MvtCode.Value) Then
            Dim r3 As DataRow = fMatItem.oMasterMM.GetMvtCodeSPDataRow(fMatItem.cmb_MvtCode.Value, fMatItem.cmb_SpStock.Value)
            If myUtils.cStrTN(r3("BOMSpStockType")) = "V" Then
                SetItemFilter("VendorID", myUtils.cValTN(fMatItem.fMat.cmb_VendorID.Value))
            Else
                Dim r4 As DataRow = fMatItem.oMasterMM.GetMvtCodeSPDataRow(Me.cmb_MvtCode.Value, Me.cmb_SpStock.Value)

                If myUtils.cStrTN(r4("SrcDesTypeItem")) = "H" Then
                    SetItemFilter("MatDepID", "" & myUtils.cValTN(fMatItem.cmb_matdepid.Value) & "")
                Else
                    SetItemFilter("MatDepID", "" & myUtils.cValTN(fMatItem.fMat.cmb_matdepid.Value) & "")
                End If
            End If
        Else
            fItemSelect.dv.RowFilter = "0=1"
        End If
    End Sub

    Public Function ControlStatus() As Boolean
        txt_QtySKU1.ReadOnly = True
        txt_QtySKU2.ReadOnly = True
        txt_QtyRate.ReadOnly = True

        cmb_ItemUnitIDEntry.ReadOnly = True
        cmb_ItemUnitID.ReadOnly = True
        cmb_ItemUnitID2.ReadOnly = True
        cmb_OrderRateUnitID.ReadOnly = True
        Return True
    End Function

    Private Sub cmb_MvtCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_MvtCode.Leave, cmb_MvtCode.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) Then
            MvtCodeLeaveEvent()
            SpStockLeaveEvent()
            HandleMatMvtCode()
        End If
    End Sub

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        VSave = False
        If Not IsNothing(myRow) Then
            If myUtils.NullNot(fItemSelect.cmb_ItemId.Value) Then WinFormUtils.AddError(fItemSelect.cmb_ItemId, "Please Select Item Code", Me.eBag)
            If myUtils.NullNot(cmb_MvtCode.Value) Then WinFormUtils.AddError(cmb_MvtCode, "Please Select Mvt. Code")
            If myUtils.NullNot(cmb_SpStock.Value) Then WinFormUtils.AddError(cmb_SpStock, "Please Select Sp. Stock")
            If myUtils.NullNot(cmb_StockStage.Value) Then WinFormUtils.AddError(cmb_StockStage, "Please Select Stock Stage")
            If Not myUtils.NullNot(cmb_ItemUnitID2.Value) AndAlso myUtils.cValTN(txt_QtySKU2.Value) = 0 Then WinFormUtils.AddError(txt_QtySKU2, "Please define Convert factor for Item")

            If myUtils.cValTN(txt_QtyEntry.Value) = 0 Then WinFormUtils.AddError(txt_QtyEntry, "Please Enter Quantity")
            If myUtils.cValTN(txt_QtyEntry.Value) > 0 AndAlso myUtils.cValTN(txt_QtySKU1.Value) = 0 Then WinFormUtils.AddError(txt_QtySKU1, "Please Enter Qty SKU1")

            If Me.CanSave AndAlso fItemSelect.ValidateData Then
                cm.EndCurrentEdit()
                If fMatItem.UltraTabControl1.Tabs("BOM").Visible = True Then
                    If fMatItem.myView.mainGrid.myGrid.Rows.Count = 0 Then WinFormUtils.AddError(fMatItem.myView.mainGrid.myGrid, "Please Enter Some Transactions")
                Else
                    VSave = True
                End If
                If Me.CanSave AndAlso Me.ValidateData Then
                    If fMatItem.UltraGridBOM.Rows.Count > 0 Then
                        myRow("UnitName") = myUtils.cStrTN(cmb_ItemUnitIDEntry.Text)
                        VSave = True
                    End If
                End If
            End If
        Else
            VSave = True
        End If
    End Function

    Public Overrides Function ValidateData() As Boolean
        Me.InitError()
        If fMatItem.fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "QtyTypeSrc") = "R" AndAlso myUtils.NullNot(cmb_QtyTypeSrc.Value) Then WinFormUtils.AddError(cmb_QtyTypeSrc, "Please Select Qty Type Src.")
        If fMatItem.fMat.ObjGetMatVouch.CalculateFieldType("I", True, myRow.Row, oMasterMM, "QtyTypeDes") = "R" AndAlso myUtils.NullNot(cmb_QtyTypeDes.Value) Then WinFormUtils.AddError(cmb_QtyTypeDes, "Please Select Qty Type Des.")
        If fMatItem.fMat.ObjGetMatVouch.CalculateFieldType("I", False, myRow.Row, oMasterMM, "TaxTypeDes") = "R" AndAlso myUtils.NullNot(cmb_TaxType2.Value) Then WinFormUtils.AddError(cmb_TaxType2, "Please Select Tax Type")
        Return Me.CanSave
    End Function

    Private Sub txt_QtyEntry_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_QtyEntry.Leave
        cm.EndCurrentEdit()
        fItemSelect.CalculateQty()
        fMatItem.CalculateTotalQtyBOM()
    End Sub

    Private Sub cmbMvtCode_Leave(sender As Object, e As EventArgs) Handles cmbMvtCode.Leave, cmbMvtCode.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) Then
            MvtCodeLeaveEvent()
            SpStockLeaveEvent()
            HandleMatMvtCode()
        End If
    End Sub

    Private Function MvtCodeLeaveEvent() As Boolean
        If Not IsDBNull(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.Value) Then
            Dim r3 As DataRow = fMatItem.oMasterMM.GetMvtCodeSPDataRow(fMatItem.cmb_MvtCode.Value, fMatItem.cmb_SpStock.Value)

            If Not IsDBNull(r3.Item("BOMSpStockType")) AndAlso Not IsNothing(r3.Item("BOMSpStockType")) Then
                cmb_SpStock.Value = fMatItem.oMasterMM.BOMMvtCodeSpFilter("BOMSpStockType", myUtils.cValTN(fMatItem.cmb_MvtCode.Value), myUtils.cStrTN(fMatItem.cmb_SpStock.Value), myUtils.cValTN(r3("BOMMvtCode")), dvSpSt, ",", cmb_SpStock.Value)
                cmb_StockStage.Value = fMatItem.oMasterMM.MvtCodeField("StockStage", cmb_MvtCode.Value, cmb_SpStock.Value, dvStStage, ",", cmb_StockStage.Value)
                cmb_TaxType2.Value = fMatItem.oMasterMM.MvtCodeField("TaxTypeDes", cmb_MvtCode.Value, "", dvTTyDes, ",", cmb_TaxType2.Value)
            End If

            fItemRes.SetTabCantrol(cmb_MvtCode.SelectedRow.Cells("MvtType").Value, myRow.Row)
        End If
        Return True
    End Function

    Private Sub cmb_SpStock_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_SpStock.Leave, cmb_SpStock.AfterCloseUp
        If Not myUtils.NullNot(cmb_MvtCode.Value) AndAlso Not IsNothing(cmb_MvtCode.SelectedRow) AndAlso Not myUtils.NullNot(cmb_SpStock.Value) AndAlso Not IsNothing(cmb_SpStock.SelectedRow) Then
            SpStockLeaveEvent()
            HandleMatMvtCode()
        End If
    End Sub

    Private Sub SpStockLeaveEvent()
        If Not IsDBNull(cmb_SpStock.Value) AndAlso Not IsNothing(cmb_SpStock.Value) Then
            cmb_QtyTypeDes.Value = fMatItem.oMasterMM.MvtCodeField("QtyTypeDes", cmb_MvtCode.Value, cmb_SpStock.Value, dvQtyDes, ",", cmb_QtyTypeDes.Value)
            cmb_QtyTypeSrc.Value = fMatItem.oMasterMM.MvtCodeField("QtyTypeSrc", cmb_MvtCode.Value, cmb_SpStock.Value, dvQtySrc, ",", cmb_QtyTypeSrc.Value)

            cm.EndCurrentEdit()

            fMatItem.fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_QtyTypeSrc, Nothing, myRow.Row, "I", "QtyTypeSrc", True)
            fMatItem.fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_QtyTypeDes, Nothing, myRow.Row, "I", "QtyTypeDes", True)
            fMatItem.fMat.ObjGetMatVouch.HandleFormField(oMasterMM, cmb_TaxType2, lblTaxDes, myRow.Row, "I", "TaxTypeDes", False, True)
            fMatItem.fMat.ObjGetMatVouch.HandleFormField(oMasterMM, txt_BasicRate, lblBasicRate, myRow.Row, "I", "BasicRate", False, True)
        End If
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged
        If Me.FormPrepared AndAlso e.Tab.Key = "Stock" Then
            If (myUtils.cValTN(myRow("ItemID")) = 0) OrElse (myUtils.cValTN(myRow("ItemID")) <> myUtils.cValTN(StockItemID)) Then GenerateStockBalance()
        End If
    End Sub

    Private Sub GenerateStockBalance()
        Me.cm.EndCurrentEdit()
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@CampusID", myUtils.cValTN(fMatItem.fMat.cmb_matdepid.SelectedRow.Cells("CampusID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@CampusID2", myUtils.cValTN(fMatItem.myRow("CampusID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@ItemID", myUtils.cValTN(myRow("ItemID")), GetType(Integer), False))
        Params.Add(New clsSQLParam("@Date", Format(fMatItem.fMat.myRow("VouchDate"), "dd-MMM-yyyy"), GetType(DateTime), False))
        Params.Add(New clsSQLParam("@PIDUnitID", myUtils.cValTN(myRow("PIDUnitID")), GetType(Integer), False))
        Dim oModel As clsViewModel = fMatItem.fMat.GenerateParamsModel("stockbaldep", Params)
        myView.GenView(oModel, EnumViewCallType.acNormal)

        Dim oModel1 As clsViewModel = fMatItem.fMat.GenerateParamsModel("stockbalcamp", Params)
        myViewStBalCamp.GenView(oModel1, EnumViewCallType.acNormal)
        StockItemID = myUtils.cValTN(myRow("ItemID"))
    End Sub
End Class