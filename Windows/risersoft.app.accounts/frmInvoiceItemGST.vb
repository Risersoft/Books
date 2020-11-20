Imports risersoft.app.shared
Imports ug = Infragistics.Win.UltraWinGrid

Public Class frmInvoiceItemGST
    Inherits frmMax
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Me.InitForm()
    End Sub

    Private Sub InitForm()

    End Sub

    Public Overloads Function BindModel(NewModel As clsFormDataModel) As Boolean
        myWinSQL.AssignCmb(NewModel.dsCombo, "ZeroTax", "", Me.cmb_GstTaxType)
        myWinSQL.AssignCmb(NewModel.dsCombo, "TY", "", Me.cmb_ty)
        Return True
    End Function

    Public Overrides Function PrepFormRow(ByVal r1 As DataRow) As Boolean
        Me.FormPrepared = False
        If Me.BindData(r1) Then
            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        Me.InitError()
        If Not cm Is Nothing Then cm.EndCurrentEdit()
        VSave = True
    End Function

    Public Sub CalculateGSTAmount(CtlPricingChild1 As ctlPricingChild, IsExport As Boolean, TaxCredit As String)
        txt_CAMT.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("CGST", "TotalValueCalc")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("CGSTR", "TotalValueCalc")), 2)
        txt_SAMT.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("SGST", "TotalValueCalc")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("SGSTR", "TotalValueCalc")), 2)
        txt_IAMT.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGST", "TotalValueCalc")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGSTR", "TotalValueCalc")), 2)
        txt_CSAMT.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("CESSGST", "TotalValueCalc")), 2)

        '' Only One Tax Should be apply for 1 Item GST or RCM.
        txt_RT.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGST", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("SGST", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("CGST", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGSTR", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("SGSTR", "PerValue")), 2) + Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("CGSTR", "PerValue")), 2)

        If myUtils.IsInList(myUtils.cStrTN(TaxCredit), "Y") Then
            txt_tx_c.Value = myUtils.cValTN(txt_CAMT.Value)
            txt_tx_s.Value = myUtils.cValTN(txt_SAMT.Value)
            txt_tx_i.Value = myUtils.cValTN(txt_IAMT.Value)
            txt_tx_cs.Value = myUtils.cValTN(txt_CSAMT.Value)
        Else
            txt_tx_c.Value = DBNull.Value
            txt_tx_s.Value = DBNull.Value
            txt_tx_i.Value = DBNull.Value
            txt_tx_cs.Value = DBNull.Value
        End If

        If myUtils.cValTN(txt_IAMT.Value) > 0 Then
            txt_TXVAL.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGST", "AmountBase")), 2)
        Else
            txt_TXVAL.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("SGST", "AmountBase")), 2)
            If txt_TXVAL.Value = 0 Then txt_TXVAL.Value = Math.Round(myUtils.cValTN(CtlPricingChild1.GetElementField("IGST", "AmountBase")), 2)
        End If
        cm.EndCurrentEdit()
        HandleZeroRated(myUtils.cValTN(txt_RT.Value), IsExport)
    End Sub

    Public Sub HandleZeroRated(RT As Decimal, IsExport As Boolean)
        If myUtils.cValTN(RT) = 0 AndAlso IsExport = False Then
            lblZeroRated.Visible = True
            cmb_GstTaxType.Visible = True
        Else
            lblZeroRated.Visible = False
            cmb_GstTaxType.Visible = False
            cmb_GstTaxType.Value = DBNull.Value
        End If
    End Sub

    Public Function CheckValid() As Boolean
        If (myUtils.cValTN(txt_RT.Value)) > 0 OrElse ((myUtils.cValTN(txt_RT.Value)) = 0 AndAlso (Not myUtils.IsInList(myUtils.cStrTN(cmb_GstTaxType.Value), "Oth", "UnReg"))) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class