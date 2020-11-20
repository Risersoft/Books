<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPaymentContraItem
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
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Amount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridAdvReq = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel14 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelAdvReq = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddAdvReq = New Infragistics.Win.Misc.UltraButton()
        Me.UEGB_Header = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel3 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        CType(Me.txt_Amount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraGridAdvReq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel14.ClientArea.SuspendLayout()
        Me.UltraPanel14.SuspendLayout()
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_Header.SuspendLayout()
        Me.UltraExpandableGroupBoxPanel3.SuspendLayout()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(692, 211)
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.UltraTabControl1)
        Me.UltraTabPageControl3.Controls.Add(Me.UltraPanel1)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(692, 243)
        '
        'UltraTabControl1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Control
        Me.UltraTabControl1.ActiveTabAppearance = Appearance1
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance2.BackColor = System.Drawing.SystemColors.Control
        Appearance2.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance2
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTabControl1.Size = New System.Drawing.Size(692, 211)
        Me.UltraTabControl1.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(80)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab4.Key = "mode"
        UltraTab4.TabPage = Me.UltraTabPageControl1
        UltraTab4.Text = "Paymentmode"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab4})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(692, 211)
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraLabel6)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txt_Amount)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(692, 32)
        Me.UltraPanel1.TabIndex = 0
        '
        'UltraLabel6
        '
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance3
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(209, 8)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Amount"
        '
        'txt_Amount
        '
        Me.txt_Amount.Location = New System.Drawing.Point(255, 5)
        Me.txt_Amount.Name = "txt_Amount"
        Me.txt_Amount.Size = New System.Drawing.Size(118, 22)
        Me.txt_Amount.TabIndex = 1
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGridAdvReq)
        Me.UltraTabPageControl2.Controls.Add(Me.UltraPanel14)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(2, 19)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(692, 243)
        '
        'UltraGridAdvReq
        '
        Me.UltraGridAdvReq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridAdvReq.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridAdvReq.Name = "UltraGridAdvReq"
        Me.UltraGridAdvReq.Size = New System.Drawing.Size(692, 218)
        Me.UltraGridAdvReq.TabIndex = 56
        '
        'UltraPanel14
        '
        '
        'UltraPanel14.ClientArea
        '
        Me.UltraPanel14.ClientArea.Controls.Add(Me.btnDelAdvReq)
        Me.UltraPanel14.ClientArea.Controls.Add(Me.btnAddAdvReq)
        Me.UltraPanel14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel14.Location = New System.Drawing.Point(0, 218)
        Me.UltraPanel14.Name = "UltraPanel14"
        Me.UltraPanel14.Size = New System.Drawing.Size(692, 25)
        Me.UltraPanel14.TabIndex = 55
        '
        'btnDelAdvReq
        '
        Me.btnDelAdvReq.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelAdvReq.Location = New System.Drawing.Point(70, 0)
        Me.btnDelAdvReq.Name = "btnDelAdvReq"
        Me.btnDelAdvReq.Size = New System.Drawing.Size(70, 25)
        Me.btnDelAdvReq.TabIndex = 1
        Me.btnDelAdvReq.Text = "Delete"
        '
        'btnAddAdvReq
        '
        Me.btnAddAdvReq.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddAdvReq.Location = New System.Drawing.Point(0, 0)
        Me.btnAddAdvReq.Name = "btnAddAdvReq"
        Me.btnAddAdvReq.Size = New System.Drawing.Size(70, 25)
        Me.btnAddAdvReq.TabIndex = 0
        Me.btnAddAdvReq.Text = "Add New"
        '
        'UEGB_Header
        '
        Me.UEGB_Header.Controls.Add(Me.UltraExpandableGroupBoxPanel3)
        Me.UEGB_Header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UEGB_Header.ExpandedSize = New System.Drawing.Size(702, 284)
        Me.UEGB_Header.ExpansionIndicator = Infragistics.Win.Misc.GroupBoxExpansionIndicator.None
        Me.UEGB_Header.HeaderClickAction = Infragistics.Win.Misc.GroupBoxHeaderClickAction.None
        Me.UEGB_Header.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_Header.Location = New System.Drawing.Point(0, 0)
        Me.UEGB_Header.Name = "UEGB_Header"
        Me.UEGB_Header.Size = New System.Drawing.Size(702, 284)
        Me.UEGB_Header.TabIndex = 2
        Me.UEGB_Header.Text = "Header"
        '
        'UltraExpandableGroupBoxPanel3
        '
        Me.UltraExpandableGroupBoxPanel3.Controls.Add(Me.UltraTabControl2)
        Me.UltraExpandableGroupBoxPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel3.Location = New System.Drawing.Point(3, 17)
        Me.UltraExpandableGroupBoxPanel3.Name = "UltraExpandableGroupBoxPanel3"
        Me.UltraExpandableGroupBoxPanel3.Size = New System.Drawing.Size(696, 264)
        Me.UltraExpandableGroupBoxPanel3.TabIndex = 0
        '
        'UltraTabControl2
        '
        Appearance4.BackColor = System.Drawing.SystemColors.Control
        Me.UltraTabControl2.ActiveTabAppearance = Appearance4
        Me.UltraTabControl2.Controls.Add(Me.UltraTabSharedControlsPage2)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl2.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl2.Name = "UltraTabControl2"
        Appearance5.BackColor = System.Drawing.SystemColors.Control
        Appearance5.FontData.BoldAsString = "True"
        Me.UltraTabControl2.SelectedTabAppearance = Appearance5
        Me.UltraTabControl2.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl2.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTabControl2.Size = New System.Drawing.Size(696, 264)
        Me.UltraTabControl2.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(50)
        Me.UltraTabControl2.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003
        Me.UltraTabControl2.TabIndex = 0
        Me.UltraTabControl2.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab1.Key = "Mode"
        UltraTab1.TabPage = Me.UltraTabPageControl3
        UltraTab1.Text = "Mode"
        UltraTab2.Key = "adv"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Advance for Expenses"
        Me.UltraTabControl2.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(692, 243)
        '
        'frmPaymentContraItem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Transaction"
        Me.ClientSize = New System.Drawing.Size(702, 284)
        Me.Controls.Add(Me.UEGB_Header)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Name = "frmPaymentContraItem"
        Me.Text = "Transaction"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        CType(Me.txt_Amount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.UltraGridAdvReq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel14.ClientArea.ResumeLayout(False)
        Me.UltraPanel14.ResumeLayout(False)
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_Header.ResumeLayout(False)
        Me.UltraExpandableGroupBoxPanel3.ResumeLayout(False)
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Amount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UEGB_Header As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel3 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridAdvReq As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel14 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelAdvReq As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddAdvReq As Infragistics.Win.Misc.UltraButton

#End Region
End Class

