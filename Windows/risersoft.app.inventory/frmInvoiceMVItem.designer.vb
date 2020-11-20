<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmInvoiceMVItem
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
        Me.components = New System.ComponentModel.Container()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtlPricingChild1 = New risersoft.app.[shared].ctlPricingChild()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.lblTaxCredit = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_TaxCredit = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        CType(Me.cmb_TaxCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.CtlPricingChild1)
        Me.UltraTabPageControl4.Controls.Add(Me.UltraPanel2)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(2, 19)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(904, 220)
        '
        'CtlPricingChild1
        '
        Me.CtlPricingChild1.BasicRateField = False
        Me.CtlPricingChild1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtlPricingChild1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.CtlPricingChild1.IsReadOnly = False
        Me.CtlPricingChild1.Location = New System.Drawing.Point(0, 33)
        Me.CtlPricingChild1.Name = "CtlPricingChild1"
        Me.CtlPricingChild1.Size = New System.Drawing.Size(904, 187)
        Me.CtlPricingChild1.TabIndex = 1
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.lblTaxCredit)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.cmb_TaxCredit)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(904, 33)
        Me.UltraPanel2.TabIndex = 54
        '
        'lblTaxCredit
        '
        Appearance1.FontData.SizeInPoints = 8.25!
        Appearance1.TextHAlignAsString = "Right"
        Appearance1.TextVAlignAsString = "Middle"
        Me.lblTaxCredit.Appearance = Appearance1
        Me.lblTaxCredit.AutoSize = True
        Me.lblTaxCredit.Location = New System.Drawing.Point(424, 9)
        Me.lblTaxCredit.Name = "lblTaxCredit"
        Me.lblTaxCredit.Size = New System.Drawing.Size(56, 14)
        Me.lblTaxCredit.TabIndex = 31
        Me.lblTaxCredit.Text = "Tax Credit"
        '
        'cmb_TaxCredit
        '
        Me.cmb_TaxCredit.Location = New System.Drawing.Point(483, 4)
        Me.cmb_TaxCredit.Name = "cmb_TaxCredit"
        Me.cmb_TaxCredit.Size = New System.Drawing.Size(129, 23)
        Me.cmb_TaxCredit.TabIndex = 30
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(904, 220)
        '
        'UltraTabControl1
        '
        Appearance2.BackColor = System.Drawing.SystemColors.Control
        Me.UltraTabControl1.ActiveTabAppearance = Appearance2
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance3.BackColor = System.Drawing.SystemColors.Control
        Appearance3.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance3
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTabControl1.Size = New System.Drawing.Size(908, 241)
        Me.UltraTabControl1.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(80)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003
        Me.UltraTabControl1.TabIndex = 2
        Me.UltraTabControl1.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab2.Key = "Pricing"
        UltraTab2.TabPage = Me.UltraTabPageControl4
        UltraTab2.Text = "Pricing"
        UltraTab5.Key = "GST"
        UltraTab5.TabPage = Me.UltraTabPageControl2
        UltraTab5.Text = "GST"
        UltraTab1.Key = "Cost"
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Cost Assignment"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab2, UltraTab5, UltraTab1})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(904, 220)
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(904, 220)
        '
        'frmInvoiceMVItem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Transaction"
        Me.ClientSize = New System.Drawing.Size(908, 241)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Name = "frmInvoiceMVItem"
        Me.Text = "Transaction"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ClientArea.PerformLayout()
        Me.UltraPanel2.ResumeLayout(False)
        CType(Me.cmb_TaxCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtlPricingChild1 As risersoft.app.shared.ctlPricingChild
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents lblTaxCredit As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_TaxCredit As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl

#End Region
End Class

