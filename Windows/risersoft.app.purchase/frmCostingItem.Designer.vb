<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmCostingItem
	Inherits frmMax

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitForm()
    End Sub

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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents dt_PurchPriceLastUpd As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_ItemUnitID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmb_subcatID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txt_CostItemName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_PurchasePrice As Infragistics.Win.UltraWinEditors.UltraTextEditor
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.dt_PurchPriceLastUpd = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmb_ItemUnitID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb_subcatID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_PurchasePrice = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_CostItemName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.dt_PurchPriceLastUpd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ItemUnitID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_subcatID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_PurchasePrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CostItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 238)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(656, 48)
        Me.Panel4.TabIndex = 10
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance1
        Me.btnSave.Location = New System.Drawing.Point(368, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 32)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance2
        Me.btnCancel.Location = New System.Drawing.Point(464, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 32)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance3
        Me.btnOK.Location = New System.Drawing.Point(560, 8)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        '
        'dt_PurchPriceLastUpd
        '
        Me.dt_PurchPriceLastUpd.FormatString = "dddd dd MMM yyyy"
        Me.dt_PurchPriceLastUpd.Location = New System.Drawing.Point(400, 128)
        Me.dt_PurchPriceLastUpd.Name = "dt_PurchPriceLastUpd"
        Me.dt_PurchPriceLastUpd.NullText = "Not Defined"
        Me.dt_PurchPriceLastUpd.Size = New System.Drawing.Size(184, 21)
        Me.dt_PurchPriceLastUpd.TabIndex = 9
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label20.Location = New System.Drawing.Point(328, 129)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 16)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "Last Updated"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label5.Location = New System.Drawing.Point(48, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Unit"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_ItemUnitID
        '
        Me.cmb_ItemUnitID.Location = New System.Drawing.Point(160, 32)
        Me.cmb_ItemUnitID.Name = "cmb_ItemUnitID"
        Me.cmb_ItemUnitID.ReadOnly = True
        Me.cmb_ItemUnitID.Size = New System.Drawing.Size(96, 22)
        Me.cmb_ItemUnitID.TabIndex = 1
        Me.cmb_ItemUnitID.Text = "UltraCombo4"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(32, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 24)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Item Sub Category"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_subcatID
        '
        Me.cmb_subcatID.DataMember = "Items"
        Me.cmb_subcatID.DisplayMember = "SubCatName"
        Me.cmb_subcatID.Location = New System.Drawing.Point(160, 96)
        Me.cmb_subcatID.MaxDropDownItems = 15
        Me.cmb_subcatID.Name = "cmb_subcatID"
        Me.cmb_subcatID.ReadOnly = True
        Me.cmb_subcatID.Size = New System.Drawing.Size(376, 22)
        Me.cmb_subcatID.TabIndex = 5
        Me.cmb_subcatID.Text = "UltraCombo1"
        Me.cmb_subcatID.ValueMember = "SubcatID"
        '
        'txt_PurchasePrice
        '
        Me.txt_PurchasePrice.Location = New System.Drawing.Point(160, 128)
        Me.txt_PurchasePrice.Name = "txt_PurchasePrice"
        Me.txt_PurchasePrice.Size = New System.Drawing.Size(142, 21)
        Me.txt_PurchasePrice.TabIndex = 7
        Me.txt_PurchasePrice.Text = "UltraTextEditor8"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label10.Location = New System.Drawing.Point(16, 128)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(136, 16)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Purchase Price"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_CostItemName
        '
        Appearance4.FontData.SizeInPoints = 10.0!
        Me.txt_CostItemName.Appearance = Appearance4
        Me.txt_CostItemName.Location = New System.Drawing.Point(160, 64)
        Me.txt_CostItemName.Name = "txt_CostItemName"
        Me.txt_CostItemName.ReadOnly = True
        Me.txt_CostItemName.Size = New System.Drawing.Size(456, 24)
        Me.txt_CostItemName.TabIndex = 3
        Me.txt_CostItemName.Text = "UltraTextEditor1"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(56, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Description"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCostingItem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Costing Item"
        Me.ClientSize = New System.Drawing.Size(656, 286)
        Me.Controls.Add(Me.dt_PurchPriceLastUpd)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmb_ItemUnitID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmb_subcatID)
        Me.Controls.Add(Me.txt_PurchasePrice)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txt_CostItemName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "frmCostingItem"
        Me.Text = "Costing Item"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.dt_PurchPriceLastUpd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ItemUnitID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_subcatID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_PurchasePrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CostItemName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
End Class

