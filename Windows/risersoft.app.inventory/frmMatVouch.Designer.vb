Imports ug = Infragistics.Win.UltraWinGrid
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmMatVouch
    Inherits frmMax
#Region " Windows Form Designer generated code "



    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.cmb_DivisionID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblDivision = New System.Windows.Forms.Label()
        Me.lblInvoiceCampus = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_InvoiceCampusID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_VendorID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel20 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_CustomerID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtlPricing1 = New risersoft.app.[shared].ctlPricingParent()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraGridRefDoc = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelRefDoc = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.cmb_ChallanPending = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.UltraLabel14 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_TransporterID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_GRNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel10 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_ChallanDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_ChallanNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.PanelSO = New Infragistics.Win.Misc.UltraPanel()
        Me.btnRemoveSO = New Infragistics.Win.Misc.UltraButton()
        Me.btnSelectSO = New Infragistics.Win.Misc.UltraButton()
        Me.lblSalesOrder = New Infragistics.Win.Misc.UltraLabel()
        Me.lblTaxTypeSrc = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_TaxType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_matdepid = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.Panel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.ButtonExecute = New Infragistics.Win.Misc.UltraButton()
        Me.btnSelectDocument = New Infragistics.Win.Misc.UltraButton()
        Me.txt_VouchNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_VouchDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtRefDocTypeCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_DefMvtCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_MatVouchTypeID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.ComboVoucherType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UEGB_ItemList = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel1 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraGridItemList = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDel = New Infragistics.Win.Misc.UltraButton()
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton()
        Me.UEGB_ItemDetail = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel2 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UEGB_Header = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel3 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.cmb_DivisionID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_InvoiceCampusID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_VendorID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CustomerID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.UltraGridRefDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.UltraTabPageControl4.SuspendLayout()
        CType(Me.cmb_ChallanPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_TransporterID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_GRNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_ChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ChallanNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl5.SuspendLayout()
        Me.PanelSO.ClientArea.SuspendLayout()
        Me.PanelSO.SuspendLayout()
        CType(Me.cmb_TaxType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_matdepid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.ClientArea.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.ClientArea.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_VouchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefDocTypeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_DefMvtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_MatVouchTypeID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboVoucherType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UEGB_ItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_ItemList.SuspendLayout()
        Me.UltraExpandableGroupBoxPanel1.SuspendLayout()
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.ClientArea.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_ItemDetail.SuspendLayout()
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_Header.SuspendLayout()
        Me.UltraExpandableGroupBoxPanel3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_DivisionID)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDivision)
        Me.UltraTabPageControl1.Controls.Add(Me.lblInvoiceCampus)
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_InvoiceCampusID)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel3)
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_VendorID)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel20)
        Me.UltraTabPageControl1.Controls.Add(Me.cmb_CustomerID)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(2, 19)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(998, 113)
        '
        'cmb_DivisionID
        '
        Me.cmb_DivisionID.Location = New System.Drawing.Point(652, 64)
        Me.cmb_DivisionID.Name = "cmb_DivisionID"
        Me.cmb_DivisionID.Size = New System.Drawing.Size(158, 23)
        Me.cmb_DivisionID.TabIndex = 13
        Me.cmb_DivisionID.Text = "UltraCombo4"
        '
        'lblDivision
        '
        Me.lblDivision.AutoSize = True
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblDivision.Location = New System.Drawing.Point(605, 69)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(44, 14)
        Me.lblDivision.TabIndex = 12
        Me.lblDivision.Text = "Division"
        Me.lblDivision.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvoiceCampus
        '
        Appearance1.FontData.SizeInPoints = 8.25!
        Appearance1.TextHAlignAsString = "Right"
        Appearance1.TextVAlignAsString = "Middle"
        Me.lblInvoiceCampus.Appearance = Appearance1
        Me.lblInvoiceCampus.AutoSize = True
        Me.lblInvoiceCampus.Location = New System.Drawing.Point(564, 32)
        Me.lblInvoiceCampus.Name = "lblInvoiceCampus"
        Me.lblInvoiceCampus.Size = New System.Drawing.Size(85, 14)
        Me.lblInvoiceCampus.TabIndex = 4
        Me.lblInvoiceCampus.Text = "Invoice Campus"
        Me.lblInvoiceCampus.Visible = False
        '
        'cmb_InvoiceCampusID
        '
        Me.cmb_InvoiceCampusID.Location = New System.Drawing.Point(652, 28)
        Me.cmb_InvoiceCampusID.Name = "cmb_InvoiceCampusID"
        Me.cmb_InvoiceCampusID.Size = New System.Drawing.Size(240, 23)
        Me.cmb_InvoiceCampusID.TabIndex = 5
        Me.cmb_InvoiceCampusID.Visible = False
        '
        'UltraLabel3
        '
        Appearance2.FontData.SizeInPoints = 8.25!
        Appearance2.TextHAlignAsString = "Right"
        Appearance2.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance2
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(17, 32)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(74, 14)
        Me.UltraLabel3.TabIndex = 0
        Me.UltraLabel3.Text = "Vendor Name"
        '
        'cmb_VendorID
        '
        Me.cmb_VendorID.Location = New System.Drawing.Point(94, 28)
        Me.cmb_VendorID.Name = "cmb_VendorID"
        Me.cmb_VendorID.Size = New System.Drawing.Size(423, 23)
        Me.cmb_VendorID.TabIndex = 1
        '
        'UltraLabel20
        '
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.UltraLabel20.Appearance = Appearance3
        Me.UltraLabel20.AutoSize = True
        Me.UltraLabel20.Location = New System.Drawing.Point(4, 68)
        Me.UltraLabel20.Name = "UltraLabel20"
        Me.UltraLabel20.Size = New System.Drawing.Size(87, 14)
        Me.UltraLabel20.TabIndex = 2
        Me.UltraLabel20.Text = "Customer Name"
        '
        'cmb_CustomerID
        '
        Me.cmb_CustomerID.Location = New System.Drawing.Point(94, 64)
        Me.cmb_CustomerID.Name = "cmb_CustomerID"
        Me.cmb_CustomerID.Size = New System.Drawing.Size(423, 23)
        Me.cmb_CustomerID.TabIndex = 3
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.CtlPricing1)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(998, 113)
        '
        'CtlPricing1
        '
        Me.CtlPricing1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtlPricing1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.CtlPricing1.IsReadOnly = False
        Me.CtlPricing1.Location = New System.Drawing.Point(0, 0)
        Me.CtlPricing1.Name = "CtlPricing1"
        Me.CtlPricing1.Size = New System.Drawing.Size(998, 113)
        Me.CtlPricing1.TabIndex = 0
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraPanel2)
        Me.UltraTabPageControl2.Controls.Add(Me.UltraPanel1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(998, 113)
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.UltraGridRefDoc)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(998, 88)
        Me.UltraPanel2.TabIndex = 54
        '
        'UltraGridRefDoc
        '
        Me.UltraGridRefDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridRefDoc.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridRefDoc.Name = "UltraGridRefDoc"
        Me.UltraGridRefDoc.Size = New System.Drawing.Size(998, 88)
        Me.UltraGridRefDoc.TabIndex = 0
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnDelRefDoc)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 88)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel1.TabIndex = 53
        '
        'btnDelRefDoc
        '
        Me.btnDelRefDoc.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelRefDoc.Location = New System.Drawing.Point(0, 0)
        Me.btnDelRefDoc.Name = "btnDelRefDoc"
        Me.btnDelRefDoc.Size = New System.Drawing.Size(70, 25)
        Me.btnDelRefDoc.TabIndex = 0
        Me.btnDelRefDoc.Text = "Delete"
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.cmb_ChallanPending)
        Me.UltraTabPageControl4.Controls.Add(Me.UltraLabel14)
        Me.UltraTabPageControl4.Controls.Add(Me.UltraLabel13)
        Me.UltraTabPageControl4.Controls.Add(Me.UltraLabel12)
        Me.UltraTabPageControl4.Controls.Add(Me.cmb_TransporterID)
        Me.UltraTabPageControl4.Controls.Add(Me.txt_GRNum)
        Me.UltraTabPageControl4.Controls.Add(Me.UltraLabel5)
        Me.UltraTabPageControl4.Controls.Add(Me.UltraLabel10)
        Me.UltraTabPageControl4.Controls.Add(Me.dt_ChallanDate)
        Me.UltraTabPageControl4.Controls.Add(Me.txt_ChallanNum)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(998, 113)
        '
        'cmb_ChallanPending
        '
        Me.cmb_ChallanPending.Location = New System.Drawing.Point(665, 18)
        Me.cmb_ChallanPending.Name = "cmb_ChallanPending"
        Me.cmb_ChallanPending.Size = New System.Drawing.Size(152, 22)
        Me.cmb_ChallanPending.TabIndex = 5
        Me.cmb_ChallanPending.Text = "UltraComboEditor1"
        '
        'UltraLabel14
        '
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel14.Appearance = Appearance4
        Me.UltraLabel14.AutoSize = True
        Me.UltraLabel14.Location = New System.Drawing.Point(569, 22)
        Me.UltraLabel14.Name = "UltraLabel14"
        Me.UltraLabel14.Size = New System.Drawing.Size(93, 14)
        Me.UltraLabel14.TabIndex = 4
        Me.UltraLabel14.Text = "Challan Received"
        '
        'UltraLabel13
        '
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel13.Appearance = Appearance5
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(325, 60)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel13.TabIndex = 8
        Me.UltraLabel13.Text = "Transporter"
        '
        'UltraLabel12
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel12.Appearance = Appearance6
        Me.UltraLabel12.AutoSize = True
        Me.UltraLabel12.Location = New System.Drawing.Point(67, 60)
        Me.UltraLabel12.Name = "UltraLabel12"
        Me.UltraLabel12.Size = New System.Drawing.Size(42, 14)
        Me.UltraLabel12.TabIndex = 6
        Me.UltraLabel12.Text = "GR No."
        '
        'cmb_TransporterID
        '
        Me.cmb_TransporterID.Location = New System.Drawing.Point(391, 56)
        Me.cmb_TransporterID.Name = "cmb_TransporterID"
        Me.cmb_TransporterID.Size = New System.Drawing.Size(426, 23)
        Me.cmb_TransporterID.TabIndex = 9
        '
        'txt_GRNum
        '
        Me.txt_GRNum.Location = New System.Drawing.Point(112, 56)
        Me.txt_GRNum.Name = "txt_GRNum"
        Me.txt_GRNum.Size = New System.Drawing.Size(152, 22)
        Me.txt_GRNum.TabIndex = 7
        Me.txt_GRNum.Text = "UltraTextEditor2"
        '
        'UltraLabel5
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance7
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(318, 23)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(70, 14)
        Me.UltraLabel5.TabIndex = 2
        Me.UltraLabel5.Text = "Challan Date"
        '
        'UltraLabel10
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel10.Appearance = Appearance8
        Me.UltraLabel10.AutoSize = True
        Me.UltraLabel10.Location = New System.Drawing.Point(46, 23)
        Me.UltraLabel10.Name = "UltraLabel10"
        Me.UltraLabel10.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel10.TabIndex = 0
        Me.UltraLabel10.Text = "Challan No."
        '
        'dt_ChallanDate
        '
        Me.dt_ChallanDate.FormatString = "ddd dd MMM yyyy"
        Me.dt_ChallanDate.Location = New System.Drawing.Point(391, 19)
        Me.dt_ChallanDate.Name = "dt_ChallanDate"
        Me.dt_ChallanDate.NullText = "Not Defined"
        Me.dt_ChallanDate.Size = New System.Drawing.Size(152, 22)
        Me.dt_ChallanDate.TabIndex = 3
        '
        'txt_ChallanNum
        '
        Me.txt_ChallanNum.Location = New System.Drawing.Point(112, 19)
        Me.txt_ChallanNum.Name = "txt_ChallanNum"
        Me.txt_ChallanNum.Size = New System.Drawing.Size(152, 22)
        Me.txt_ChallanNum.TabIndex = 1
        Me.txt_ChallanNum.Text = "UltraTextEditor1"
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Controls.Add(Me.PanelSO)
        Me.UltraTabPageControl5.Controls.Add(Me.lblSalesOrder)
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(998, 113)
        '
        'PanelSO
        '
        '
        'PanelSO.ClientArea
        '
        Me.PanelSO.ClientArea.Controls.Add(Me.btnRemoveSO)
        Me.PanelSO.ClientArea.Controls.Add(Me.btnSelectSO)
        Me.PanelSO.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelSO.Location = New System.Drawing.Point(0, 77)
        Me.PanelSO.Name = "PanelSO"
        Me.PanelSO.Size = New System.Drawing.Size(998, 36)
        Me.PanelSO.TabIndex = 22
        '
        'btnRemoveSO
        '
        Me.btnRemoveSO.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnRemoveSO.Location = New System.Drawing.Point(111, 0)
        Me.btnRemoveSO.Name = "btnRemoveSO"
        Me.btnRemoveSO.Size = New System.Drawing.Size(121, 36)
        Me.btnRemoveSO.TabIndex = 1
        Me.btnRemoveSO.Text = "Remove Sales Order"
        '
        'btnSelectSO
        '
        Me.btnSelectSO.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSelectSO.Location = New System.Drawing.Point(0, 0)
        Me.btnSelectSO.Name = "btnSelectSO"
        Me.btnSelectSO.Size = New System.Drawing.Size(111, 36)
        Me.btnSelectSO.TabIndex = 0
        Me.btnSelectSO.Text = "Select Sales Order"
        '
        'lblSalesOrder
        '
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.TextHAlignAsString = "Right"
        Appearance9.TextVAlignAsString = "Middle"
        Me.lblSalesOrder.Appearance = Appearance9
        Me.lblSalesOrder.AutoSize = True
        Me.lblSalesOrder.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesOrder.Location = New System.Drawing.Point(4, 37)
        Me.lblSalesOrder.Name = "lblSalesOrder"
        Me.lblSalesOrder.Size = New System.Drawing.Size(104, 14)
        Me.lblSalesOrder.TabIndex = 21
        Me.lblSalesOrder.Text = "Select Sales Order "
        '
        'lblTaxTypeSrc
        '
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.TextHAlignAsString = "Right"
        Appearance10.TextVAlignAsString = "Middle"
        Me.lblTaxTypeSrc.Appearance = Appearance10
        Me.lblTaxTypeSrc.AutoSize = True
        Me.lblTaxTypeSrc.Location = New System.Drawing.Point(685, 13)
        Me.lblTaxTypeSrc.Name = "lblTaxTypeSrc"
        Me.lblTaxTypeSrc.Size = New System.Drawing.Size(51, 14)
        Me.lblTaxTypeSrc.TabIndex = 4
        Me.lblTaxTypeSrc.Text = "Tax Type"
        '
        'cmb_TaxType
        '
        Me.cmb_TaxType.DataMember = "Items"
        Me.cmb_TaxType.DisplayMember = "ItemCode"
        Me.cmb_TaxType.Location = New System.Drawing.Point(739, 8)
        Me.cmb_TaxType.MaxDropDownItems = 15
        Me.cmb_TaxType.Name = "cmb_TaxType"
        Me.cmb_TaxType.Size = New System.Drawing.Size(158, 23)
        Me.cmb_TaxType.TabIndex = 5
        Me.cmb_TaxType.ValueMember = "ItemID"
        '
        'UltraLabel4
        '
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.TextHAlignAsString = "Right"
        Appearance11.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance11
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(33, 68)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel4.TabIndex = 6
        Me.UltraLabel4.Text = "Department"
        '
        'cmb_matdepid
        '
        Me.cmb_matdepid.Location = New System.Drawing.Point(99, 64)
        Me.cmb_matdepid.Name = "cmb_matdepid"
        Me.cmb_matdepid.Size = New System.Drawing.Size(414, 23)
        Me.cmb_matdepid.TabIndex = 7
        '
        'btnOK
        '
        Appearance12.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance12
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(940, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 33)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Appearance13.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance13
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(872, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 33)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Appearance14.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance14
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(804, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 33)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel1
        '
        '
        'Panel1.ClientArea
        '
        Me.Panel1.ClientArea.Controls.Add(Me.btnSave)
        Me.Panel1.ClientArea.Controls.Add(Me.btnCancel)
        Me.Panel1.ClientArea.Controls.Add(Me.btnOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 697)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1008, 33)
        Me.Panel1.TabIndex = 4
        '
        'Panel2
        '
        '
        'Panel2.ClientArea
        '
        Me.Panel2.ClientArea.Controls.Add(Me.ButtonExecute)
        Me.Panel2.ClientArea.Controls.Add(Me.btnSelectDocument)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_VouchNum)
        Me.Panel2.ClientArea.Controls.Add(Me.dt_VouchDate)
        Me.Panel2.ClientArea.Controls.Add(Me.lblTaxTypeSrc)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel9)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.Panel2.ClientArea.Controls.Add(Me.txtRefDocTypeCode)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel8)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_DefMvtCode)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel7)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_TaxType)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_MatVouchTypeID)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel4)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel6)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_matdepid)
        Me.Panel2.ClientArea.Controls.Add(Me.ComboVoucherType)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1008, 92)
        Me.Panel2.TabIndex = 0
        '
        'ButtonExecute
        '
        Me.ButtonExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExecute.Location = New System.Drawing.Point(910, 39)
        Me.ButtonExecute.Name = "ButtonExecute"
        Me.ButtonExecute.Size = New System.Drawing.Size(91, 48)
        Me.ButtonExecute.TabIndex = 17
        Me.ButtonExecute.Text = "Execute Selection"
        '
        'btnSelectDocument
        '
        Me.btnSelectDocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelectDocument.Location = New System.Drawing.Point(526, 39)
        Me.btnSelectDocument.Name = "btnSelectDocument"
        Me.btnSelectDocument.Size = New System.Drawing.Size(92, 48)
        Me.btnSelectDocument.TabIndex = 12
        Me.btnSelectDocument.Text = "Select Document"
        '
        'txt_VouchNum
        '
        Me.txt_VouchNum.Location = New System.Drawing.Point(360, 37)
        Me.txt_VouchNum.Name = "txt_VouchNum"
        Me.txt_VouchNum.ReadOnly = True
        Me.txt_VouchNum.Size = New System.Drawing.Size(153, 22)
        Me.txt_VouchNum.TabIndex = 9
        Me.txt_VouchNum.Text = "UltraTextEditor1"
        '
        'dt_VouchDate
        '
        Me.dt_VouchDate.FormatString = "ddd dd MMM yyyy"
        Me.dt_VouchDate.Location = New System.Drawing.Point(99, 37)
        Me.dt_VouchDate.Name = "dt_VouchDate"
        Me.dt_VouchDate.NullText = "Not Defined"
        Me.dt_VouchDate.Size = New System.Drawing.Size(141, 22)
        Me.dt_VouchDate.TabIndex = 11
        '
        'UltraLabel1
        '
        Appearance15.FontData.SizeInPoints = 8.25!
        Appearance15.TextHAlignAsString = "Right"
        Appearance15.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance15
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(290, 41)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel1.TabIndex = 8
        Me.UltraLabel1.Text = "Voucher No."
        '
        'UltraLabel9
        '
        Me.UltraLabel9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance16.FontData.SizeInPoints = 8.25!
        Appearance16.TextHAlignAsString = "Right"
        Appearance16.TextVAlignAsString = "Middle"
        Me.UltraLabel9.Appearance = Appearance16
        Me.UltraLabel9.AutoSize = True
        Me.UltraLabel9.Location = New System.Drawing.Point(625, 41)
        Me.UltraLabel9.Name = "UltraLabel9"
        Me.UltraLabel9.Size = New System.Drawing.Size(111, 14)
        Me.UltraLabel9.TabIndex = 13
        Me.UltraLabel9.Text = "Reference Document"
        '
        'UltraLabel2
        '
        Appearance17.FontData.SizeInPoints = 8.25!
        Appearance17.TextHAlignAsString = "Right"
        Appearance17.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance17
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(23, 41)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(73, 14)
        Me.UltraLabel2.TabIndex = 10
        Me.UltraLabel2.Text = "Voucher Date"
        '
        'txtRefDocTypeCode
        '
        Me.txtRefDocTypeCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRefDocTypeCode.Location = New System.Drawing.Point(739, 37)
        Me.txtRefDocTypeCode.Name = "txtRefDocTypeCode"
        Me.txtRefDocTypeCode.ReadOnly = True
        Me.txtRefDocTypeCode.Size = New System.Drawing.Size(158, 22)
        Me.txtRefDocTypeCode.TabIndex = 14
        '
        'UltraLabel8
        '
        Me.UltraLabel8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance18.TextHAlignAsString = "Right"
        Appearance18.TextVAlignAsString = "Middle"
        Me.UltraLabel8.Appearance = Appearance18
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.UltraLabel8.Location = New System.Drawing.Point(714, 68)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(126, 14)
        Me.UltraLabel8.TabIndex = 15
        Me.UltraLabel8.Text = "Default Movement Code"
        '
        'txt_DefMvtCode
        '
        Me.txt_DefMvtCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DefMvtCode.Location = New System.Drawing.Point(843, 64)
        Me.txt_DefMvtCode.Name = "txt_DefMvtCode"
        Me.txt_DefMvtCode.ReadOnly = True
        Me.txt_DefMvtCode.Size = New System.Drawing.Size(54, 22)
        Me.txt_DefMvtCode.TabIndex = 16
        '
        'UltraLabel7
        '
        Appearance19.FontData.SizeInPoints = 8.25!
        Appearance19.TextHAlignAsString = "Right"
        Appearance19.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance19
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(252, 12)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(105, 14)
        Me.UltraLabel7.TabIndex = 2
        Me.UltraLabel7.Text = "Ref Document Type"
        '
        'cmb_MatVouchTypeID
        '
        Me.cmb_MatVouchTypeID.Location = New System.Drawing.Point(360, 8)
        Me.cmb_MatVouchTypeID.Name = "cmb_MatVouchTypeID"
        Me.cmb_MatVouchTypeID.Size = New System.Drawing.Size(153, 23)
        Me.cmb_MatVouchTypeID.TabIndex = 3
        '
        'UltraLabel6
        '
        Appearance20.FontData.SizeInPoints = 8.25!
        Appearance20.TextHAlignAsString = "Right"
        Appearance20.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance20
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(21, 13)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(75, 14)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Voucher Type"
        '
        'ComboVoucherType
        '
        Me.ComboVoucherType.Location = New System.Drawing.Point(99, 8)
        Me.ComboVoucherType.Name = "ComboVoucherType"
        Me.ComboVoucherType.Size = New System.Drawing.Size(141, 23)
        Me.ComboVoucherType.TabIndex = 1
        '
        'UEGB_ItemList
        '
        Me.UEGB_ItemList.Controls.Add(Me.UltraExpandableGroupBoxPanel1)
        Me.UEGB_ItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UEGB_ItemList.ExpandedSize = New System.Drawing.Size(1008, 198)
        Me.UEGB_ItemList.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_ItemList.Location = New System.Drawing.Point(0, 248)
        Me.UEGB_ItemList.Name = "UEGB_ItemList"
        Me.UEGB_ItemList.Size = New System.Drawing.Size(1008, 198)
        Me.UEGB_ItemList.TabIndex = 2
        Me.UEGB_ItemList.Text = "Item List"
        '
        'UltraExpandableGroupBoxPanel1
        '
        Me.UltraExpandableGroupBoxPanel1.Controls.Add(Me.UltraGridItemList)
        Me.UltraExpandableGroupBoxPanel1.Controls.Add(Me.Panel4)
        Me.UltraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel1.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel1.Name = "UltraExpandableGroupBoxPanel1"
        Me.UltraExpandableGroupBoxPanel1.Size = New System.Drawing.Size(1002, 176)
        Me.UltraExpandableGroupBoxPanel1.TabIndex = 0
        '
        'UltraGridItemList
        '
        Me.UltraGridItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridItemList.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridItemList.Name = "UltraGridItemList"
        Me.UltraGridItemList.Size = New System.Drawing.Size(1002, 151)
        Me.UltraGridItemList.TabIndex = 0
        '
        'Panel4
        '
        '
        'Panel4.ClientArea
        '
        Me.Panel4.ClientArea.Controls.Add(Me.btnDel)
        Me.Panel4.ClientArea.Controls.Add(Me.btnAdd)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 151)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1002, 25)
        Me.Panel4.TabIndex = 52
        '
        'btnDel
        '
        Me.btnDel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDel.Location = New System.Drawing.Point(70, 0)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(70, 25)
        Me.btnDel.TabIndex = 1
        Me.btnDel.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAdd.Location = New System.Drawing.Point(0, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 25)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add New"
        '
        'UEGB_ItemDetail
        '
        Me.UEGB_ItemDetail.Controls.Add(Me.UltraExpandableGroupBoxPanel2)
        Me.UEGB_ItemDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UEGB_ItemDetail.ExpandedSize = New System.Drawing.Size(1008, 251)
        Me.UEGB_ItemDetail.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_ItemDetail.Location = New System.Drawing.Point(0, 446)
        Me.UEGB_ItemDetail.Name = "UEGB_ItemDetail"
        Me.UEGB_ItemDetail.Size = New System.Drawing.Size(1008, 251)
        Me.UEGB_ItemDetail.TabIndex = 3
        Me.UEGB_ItemDetail.Text = "Item Details"
        '
        'UltraExpandableGroupBoxPanel2
        '
        Me.UltraExpandableGroupBoxPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel2.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel2.Name = "UltraExpandableGroupBoxPanel2"
        Me.UltraExpandableGroupBoxPanel2.Size = New System.Drawing.Size(1002, 229)
        Me.UltraExpandableGroupBoxPanel2.TabIndex = 0
        '
        'UEGB_Header
        '
        Me.UEGB_Header.Controls.Add(Me.UltraExpandableGroupBoxPanel3)
        Me.UEGB_Header.Dock = System.Windows.Forms.DockStyle.Top
        Me.UEGB_Header.ExpandedSize = New System.Drawing.Size(1008, 156)
        Me.UEGB_Header.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_Header.Location = New System.Drawing.Point(0, 92)
        Me.UEGB_Header.Name = "UEGB_Header"
        Me.UEGB_Header.Size = New System.Drawing.Size(1008, 156)
        Me.UEGB_Header.TabIndex = 1
        Me.UEGB_Header.Text = "Header"
        '
        'UltraExpandableGroupBoxPanel3
        '
        Me.UltraExpandableGroupBoxPanel3.Controls.Add(Me.UltraTabControl1)
        Me.UltraExpandableGroupBoxPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel3.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel3.Name = "UltraExpandableGroupBoxPanel3"
        Me.UltraExpandableGroupBoxPanel3.Size = New System.Drawing.Size(1002, 134)
        Me.UltraExpandableGroupBoxPanel3.TabIndex = 0
        '
        'UltraTabControl1
        '
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Me.UltraTabControl1.ActiveTabAppearance = Appearance21
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl5)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance22.BackColor = System.Drawing.SystemColors.Control
        Appearance22.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance22
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTabControl1.Size = New System.Drawing.Size(1002, 134)
        Me.UltraTabControl1.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(50)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "General"
        UltraTab2.Key = "pricing"
        UltraTab2.TabPage = Me.UltraTabPageControl3
        UltraTab2.Text = "Pricing"
        UltraTab3.Key = "Ref"
        UltraTab3.TabPage = Me.UltraTabPageControl2
        UltraTab3.Text = "Reference"
        UltraTab4.Key = "Doc"
        UltraTab4.TabPage = Me.UltraTabPageControl4
        UltraTab4.Text = "Document"
        UltraTab5.Key = "SO"
        UltraTab5.TabPage = Me.UltraTabPageControl5
        UltraTab5.Text = "Sales Order"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3, UltraTab4, UltraTab5})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(998, 113)
        '
        'frmMatVouch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Material Voucher"
        Me.ClientSize = New System.Drawing.Size(1008, 730)
        Me.Controls.Add(Me.UEGB_ItemList)
        Me.Controls.Add(Me.UEGB_ItemDetail)
        Me.Controls.Add(Me.UEGB_Header)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Name = "frmMatVouch"
        Me.Text = "Material Voucher"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.cmb_DivisionID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_InvoiceCampusID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_VendorID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CustomerID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.UltraGridRefDoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ResumeLayout(False)
        Me.UltraTabPageControl4.ResumeLayout(False)
        Me.UltraTabPageControl4.PerformLayout()
        CType(Me.cmb_ChallanPending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_TransporterID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_GRNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_ChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ChallanNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl5.ResumeLayout(False)
        Me.UltraTabPageControl5.PerformLayout()
        Me.PanelSO.ClientArea.ResumeLayout(False)
        Me.PanelSO.ResumeLayout(False)
        CType(Me.cmb_TaxType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_matdepid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ClientArea.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ClientArea.ResumeLayout(False)
        Me.Panel2.ClientArea.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_VouchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefDocTypeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_DefMvtCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_MatVouchTypeID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboVoucherType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UEGB_ItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_ItemList.ResumeLayout(False)
        Me.UltraExpandableGroupBoxPanel1.ResumeLayout(False)
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ClientArea.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_ItemDetail.ResumeLayout(False)
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_Header.ResumeLayout(False)
        Me.UltraExpandableGroupBoxPanel3.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents Panel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UEGB_ItemList As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel1 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UEGB_ItemDetail As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel2 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraGridItemList As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UEGB_Header As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel3 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_VouchDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_VouchNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_MatVouchTypeID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents ComboVoucherType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_DefMvtCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtRefDocTypeCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents btnSelectDocument As Infragistics.Win.Misc.UltraButton
    Friend WithEvents ButtonExecute As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_matdepid As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents CtlPricing1 As risersoft.app.shared.ctlPricingParent
    Friend WithEvents UltraLabel20 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_CustomerID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_VendorID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblTaxTypeSrc As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_TaxType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraGridRefDoc As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelRefDoc As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraLabel14 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_TransporterID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_GRNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel10 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_ChallanDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_ChallanNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmb_ChallanPending As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents lblInvoiceCampus As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_InvoiceCampusID As ug.UltraCombo
    Friend WithEvents cmb_DivisionID As ug.UltraCombo
    Friend WithEvents lblDivision As Windows.Forms.Label
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents PanelSO As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnRemoveSO As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnSelectSO As Infragistics.Win.Misc.UltraButton
    Friend WithEvents lblSalesOrder As Infragistics.Win.Misc.UltraLabel

#End Region
End Class

