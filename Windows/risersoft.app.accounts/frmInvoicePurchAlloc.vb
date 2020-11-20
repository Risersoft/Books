﻿Imports ug = Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
Imports risersoft.app.mxform

Public Class frmInvoicePurchAlloc
    Inherits frmMax
    Dim dvCamp As DataView
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Public Sub InitForm()
        WinFormUtils.SetButtonConf(Me.btnOK, Me.btnCancel, Me.btnSave)

        Me.AddUEGB(Me.UEGB_Header, 3, Me.UEGB_ItemList.Name)
        Me.AddUEGB(Me.UEGB_ItemList, 2, Me.UEGB_Header.Name)

        myView.SetGrid(UltraGridAdvance)
    End Sub

    Public Overrides Function PrepForm(oView As clsWinView, ByVal prepMode As EnumfrmMode, ByVal prepIdx As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim objModel As frmInvoicePurchAllocModel = Me.InitData("frmInvoicePurchAllocModel", oview, prepMode, prepIdx, strXML)
        If Me.BindModel(objModel, oview) Then
            CalculateBalance()
            HandleDate(myUtils.cDateTN(myRow("InvoiceDate"), DateTime.MinValue))

            If myUtils.cBoolTN(myWinSQL2.ParamValue("@Status", Me.Model.ModelParams)) AndAlso myView.mainGrid.myDS.Tables(0).Rows.Count > 0 Then
                btnSave.Enabled = False
                btnOK.Enabled = False
            End If
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function BindModel(NewModel As clsFormDataModel, oView As clsView) As Boolean
        If MyBase.BindModel(NewModel, oView) Then
            myView.PrepEdit(Me.Model.GridViews("Advance"))

            dvCamp = myWinSQL.AssignCmb(Me.dsCombo, "Campus", "", Me.cmb_campusid,, 2)
            myWinSQL.AssignCmb(Me.dsCombo, "Vendor", "", Me.cmb_VendorID)
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
        Dim Params As New List(Of clsSQLParam)
        Params.Add(New clsSQLParam("@paymentidcsv", myUtils.MakeCSV(myView.mainGrid.myDS.Tables(0).Select, "PaymentID"), GetType(Integer), True))
        Params.Add(New clsSQLParam("@vendorid", myUtils.cValTN(cmb_VendorID.Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@companyid", myUtils.cValTN(cmb_campusid.SelectedRow.Cells("CompanyID").Value), GetType(Integer), False))
        Params.Add(New clsSQLParam("@dated", Format(dt_PostingDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))

        Dim Params2 As New List(Of clsSQLParam)
        Params2.Add(New clsSQLParam("ID", frmIDX, GetType(Integer), False))
        Params2.Add(New clsSQLParam("Dated", Format(dt_PostingDate.Value, "dd-MMM-yyyy"), GetType(DateTime), False))

        Dim rr1() As DataRow = Me.PopulateDataRows("generateprebal", Me.AdvancedSelect("payment", Params), Params2)
        If Not rr1 Is Nothing AndAlso rr1.Length > 0 Then
            For Each r2 As DataRow In rr1
                Dim r3 As DataRow = myUtils.CopyOneRow(r2, myView.mainGrid.myDS.Tables(0))
                r3("Amount") = DBNull.Value
                r3("InvoiceID") = frmIDX
            Next
            CalculateBalance()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        myView.mainGrid.ButtonAction("del")
        CalculateBalance()
    End Sub

    Private Sub CalculateBalance()
        cm.EndCurrentEdit()
        Dim Amount As Decimal = 0
        For Each r1 As DataRow In myView.mainGrid.myDS.Tables(0).Select
            r1("Balance") = myUtils.cValTN(r1("PreBalance")) - (myUtils.cValTN(r1("Amount")) + myUtils.cValTN(r1("TDSAmount")) + myUtils.cValTN(r1("WCTAmount")))
            Amount = myUtils.cValTN(Amount) + myUtils.cValTN(r1("Amount")) + myUtils.cValTN(r1("TDSAmount")) + myUtils.cValTN(r1("WCTAmount"))
        Next
        myRow("PostBalance") = myUtils.cValTN(myRow("PreBalance")) - myUtils.cValTN(Amount)
    End Sub

    Private Sub UltraGridAdvance_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridAdvance.AfterCellUpdate
        CalculateBalance()
    End Sub

    Private Sub dt_InvoiceDate_Leave(sender As Object, e As EventArgs) Handles dt_InvoiceDate.Leave, dt_InvoiceDate.AfterCloseUp
        HandleDate(dt_InvoiceDate.Value)
    End Sub

    Private Sub HandleDate(dated As Date)
        dvCamp.RowFilter = risersoft.app.mxform.myFuncs.FieldFilter(Me.Controller, Me.fRow, dated, "WODate", "CompletedOn", "CampusID", 12)
    End Sub
End Class