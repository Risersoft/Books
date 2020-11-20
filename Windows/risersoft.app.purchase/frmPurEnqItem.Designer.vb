<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmPurEnqItem
	Inherits frmMax
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        myview.SetGrid(UltraGridParams)
        Me.initForm()
        'Add any initialization after the InitializeComponent() call

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab8 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmb_RateUnitId = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_BaseUnitID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_UnitName = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbItemName = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_ItemId = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_snum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLable4 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_rate = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_qty = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltaLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_ItemSuffix = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGridParams = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnDelParam = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddParam = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmb_RateUnitId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_BaseUnitID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_UnitName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ItemId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_snum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_rate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_qty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ItemSuffix, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraGridParams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(2, 19)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(742, 234)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmb_RateUnitId)
        Me.Panel1.Controls.Add(Me.UltraLabel7)
        Me.Panel1.Controls.Add(Me.UltraLabel6)
        Me.Panel1.Controls.Add(Me.cmb_BaseUnitID)
        Me.Panel1.Controls.Add(Me.cmb_UnitName)
        Me.Panel1.Controls.Add(Me.cmbItemName)
        Me.Panel1.Controls.Add(Me.cmb_ItemId)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.UltraLabel4)
        Me.Panel1.Controls.Add(Me.UltraLabel5)
        Me.Panel1.Controls.Add(Me.txt_snum)
        Me.Panel1.Controls.Add(Me.UltraLable4)
        Me.Panel1.Controls.Add(Me.txt_rate)
        Me.Panel1.Controls.Add(Me.txt_qty)
        Me.Panel1.Controls.Add(Me.UltraLabel3)
        Me.Panel1.Controls.Add(Me.UltaLabel3)
        Me.Panel1.Controls.Add(Me.txt_ItemSuffix)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(742, 234)
        Me.Panel1.TabIndex = 0
        '
        'cmb_RateUnitId
        '
        Me.cmb_RateUnitId.DisplayMember = "unitName"
        Me.cmb_RateUnitId.Location = New System.Drawing.Point(567, 114)
        Me.cmb_RateUnitId.Name = "cmb_RateUnitId"
        Me.cmb_RateUnitId.ReadOnly = True
        Me.cmb_RateUnitId.Size = New System.Drawing.Size(155, 22)
        Me.cmb_RateUnitId.TabIndex = 15
        Me.cmb_RateUnitId.TabStop = False
        Me.cmb_RateUnitId.ValueMember = "itemid"
        '
        'UltraLabel7
        '
        Appearance20.TextHAlignAsString = "Right"
        Me.UltraLabel7.Appearance = Appearance20
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(513, 118)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(51, 14)
        Me.UltraLabel7.TabIndex = 14
        Me.UltraLabel7.Text = "Rate Unit"
        '
        'UltraLabel6
        '
        Appearance10.TextHAlignAsString = "Right"
        Me.UltraLabel6.Appearance = Appearance10
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(300, 51)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel6.TabIndex = 4
        Me.UltraLabel6.Text = "Base Unit"
        '
        'cmb_BaseUnitID
        '
        Me.cmb_BaseUnitID.DisplayMember = "unitName"
        Me.cmb_BaseUnitID.Location = New System.Drawing.Point(356, 47)
        Me.cmb_BaseUnitID.Name = "cmb_BaseUnitID"
        Me.cmb_BaseUnitID.ReadOnly = True
        Me.cmb_BaseUnitID.Size = New System.Drawing.Size(129, 22)
        Me.cmb_BaseUnitID.TabIndex = 5
        Me.cmb_BaseUnitID.TabStop = False
        Me.cmb_BaseUnitID.ValueMember = "itemid"
        '
        'cmb_UnitName
        '
        Me.cmb_UnitName.DataMember = "items"
        Me.cmb_UnitName.DisplayMember = "itemCode"
        Me.cmb_UnitName.Location = New System.Drawing.Point(567, 47)
        Me.cmb_UnitName.Name = "cmb_UnitName"
        Me.cmb_UnitName.Size = New System.Drawing.Size(155, 22)
        Me.cmb_UnitName.TabIndex = 7
        Me.cmb_UnitName.ValueMember = "itemId"
        '
        'cmbItemName
        '
        Me.cmbItemName.DataMember = "items"
        Me.cmbItemName.DisplayMember = "itemName"
        Me.cmbItemName.Location = New System.Drawing.Point(88, 81)
        Me.cmbItemName.Name = "cmbItemName"
        Me.cmbItemName.Size = New System.Drawing.Size(634, 22)
        Me.cmbItemName.TabIndex = 9
        Me.cmbItemName.ValueMember = "itemId"
        '
        'cmb_ItemId
        '
        Me.cmb_ItemId.DataMember = "items"
        Me.cmb_ItemId.DisplayMember = "itemCode"
        Me.cmb_ItemId.Location = New System.Drawing.Point(88, 48)
        Me.cmb_ItemId.Name = "cmb_ItemId"
        Me.cmb_ItemId.Size = New System.Drawing.Size(177, 22)
        Me.cmb_ItemId.TabIndex = 3
        Me.cmb_ItemId.ValueMember = "itemId"
        '
        'UltraLabel1
        '
        Appearance30.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance30
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(540, 51)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(24, 14)
        Me.UltraLabel1.TabIndex = 6
        Me.UltraLabel1.Text = "Unit"
        '
        'UltraLabel4
        '
        Appearance21.TextHAlignAsString = "Right"
        Me.UltraLabel4.Appearance = Appearance21
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(25, 84)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(60, 14)
        Me.UltraLabel4.TabIndex = 8
        Me.UltraLabel4.Text = "Item Name"
        '
        'UltraLabel5
        '
        Appearance29.TextHAlignAsString = "Right"
        Me.UltraLabel5.Appearance = Appearance29
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(29, 51)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(56, 14)
        Me.UltraLabel5.TabIndex = 2
        Me.UltraLabel5.Text = "Item Code"
        '
        'txt_snum
        '
        Appearance26.TextHAlignAsString = "Right"
        Me.txt_snum.Appearance = Appearance26
        Me.txt_snum.Location = New System.Drawing.Point(88, 16)
        Me.txt_snum.Name = "txt_snum"
        Me.txt_snum.Size = New System.Drawing.Size(177, 21)
        Me.txt_snum.TabIndex = 1
        '
        'UltraLable4
        '
        Appearance22.TextHAlignAsString = "Right"
        Me.UltraLable4.Appearance = Appearance22
        Me.UltraLable4.AutoSize = True
        Me.UltraLable4.Location = New System.Drawing.Point(53, 19)
        Me.UltraLable4.Name = "UltraLable4"
        Me.UltraLable4.Size = New System.Drawing.Size(32, 14)
        Me.UltraLable4.TabIndex = 0
        Me.UltraLable4.Text = "S.No."
        '
        'txt_rate
        '
        Appearance23.TextHAlignAsString = "Right"
        Me.txt_rate.Appearance = Appearance23
        Me.txt_rate.Location = New System.Drawing.Point(356, 113)
        Me.txt_rate.Name = "txt_rate"
        Me.txt_rate.Size = New System.Drawing.Size(129, 21)
        Me.txt_rate.TabIndex = 13
        '
        'txt_qty
        '
        Appearance31.TextHAlignAsString = "Right"
        Me.txt_qty.Appearance = Appearance31
        Me.txt_qty.Location = New System.Drawing.Point(88, 114)
        Me.txt_qty.Name = "txt_qty"
        Me.txt_qty.Size = New System.Drawing.Size(177, 21)
        Me.txt_qty.TabIndex = 11
        '
        'UltraLabel3
        '
        Appearance27.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance27
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(325, 116)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(28, 14)
        Me.UltraLabel3.TabIndex = 12
        Me.UltraLabel3.Text = "Rate"
        '
        'UltaLabel3
        '
        Appearance28.TextHAlignAsString = "Right"
        Me.UltaLabel3.Appearance = Appearance28
        Me.UltaLabel3.AutoSize = True
        Me.UltaLabel3.Location = New System.Drawing.Point(39, 117)
        Me.UltaLabel3.Name = "UltaLabel3"
        Me.UltaLabel3.Size = New System.Drawing.Size(46, 14)
        Me.UltaLabel3.TabIndex = 10
        Me.UltaLabel3.Text = "Quantity"
        '
        'txt_ItemSuffix
        '
        Me.txt_ItemSuffix.AcceptsReturn = True
        Me.txt_ItemSuffix.Location = New System.Drawing.Point(88, 146)
        Me.txt_ItemSuffix.Multiline = True
        Me.txt_ItemSuffix.Name = "txt_ItemSuffix"
        Me.txt_ItemSuffix.Size = New System.Drawing.Size(634, 40)
        Me.txt_ItemSuffix.TabIndex = 17
        '
        'UltraLabel2
        '
        Appearance32.TextHAlignAsString = "Right"
        Me.UltraLabel2.Appearance = Appearance32
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(27, 149)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(58, 14)
        Me.UltraLabel2.TabIndex = 16
        Me.UltraLabel2.Text = "Item Suffix"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel3)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(742, 234)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGridParams)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(742, 200)
        Me.Panel2.TabIndex = 30
        '
        'UltraGridParams
        '
        Me.UltraGridParams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridParams.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridParams.Name = "UltraGridParams"
        Me.UltraGridParams.Size = New System.Drawing.Size(742, 200)
        Me.UltraGridParams.TabIndex = 0
        Me.UltraGridParams.Text = "UltraGrid1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnDelParam)
        Me.Panel3.Controls.Add(Me.btnAddParam)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 200)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(742, 34)
        Me.Panel3.TabIndex = 31
        '
        'btnDelParam
        '
        Me.btnDelParam.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelParam.Location = New System.Drawing.Point(64, 0)
        Me.btnDelParam.Name = "btnDelParam"
        Me.btnDelParam.Size = New System.Drawing.Size(64, 34)
        Me.btnDelParam.TabIndex = 1
        Me.btnDelParam.Text = "&Delete"
        '
        'btnAddParam
        '
        Me.btnAddParam.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddParam.Location = New System.Drawing.Point(0, 0)
        Me.btnAddParam.Name = "btnAddParam"
        Me.btnAddParam.Size = New System.Drawing.Size(64, 34)
        Me.btnAddParam.TabIndex = 0
        Me.btnAddParam.Text = "&Add New"
        '
        'UltraTabControl1
        '
        Appearance24.BackColor = System.Drawing.SystemColors.Control
        Me.UltraTabControl1.ActiveTabAppearance = Appearance24
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance25.BackColor = System.Drawing.SystemColors.Control
        Appearance25.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance25
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTabControl1.Size = New System.Drawing.Size(746, 255)
        Me.UltraTabControl1.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(80)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab8.Key = "General"
        UltraTab8.TabPage = Me.UltraTabPageControl4
        UltraTab8.Text = "General"
        UltraTab6.Key = "Params"
        UltraTab6.TabPage = Me.UltraTabPageControl1
        UltraTab6.Text = "Params"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab8, UltraTab6})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(742, 234)
        '
        'frmPurEnqItem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Enquire Item"
        Me.ClientSize = New System.Drawing.Size(746, 255)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.MaximizeBox = False
        Me.Name = "frmPurEnqItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enquire Item"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmb_RateUnitId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_BaseUnitID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_UnitName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ItemId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_snum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_rate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_qty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ItemSuffix, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGridParams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmb_UnitName As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbItemName As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmb_ItemId As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_snum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLable4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_rate As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_qty As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltaLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_ItemSuffix As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents UltraGridParams As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnAddParam As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDelParam As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_BaseUnitID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmb_RateUnitId As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel

#End Region
End Class

