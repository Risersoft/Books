<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBankAccount
    Inherits risersoft.shared.win.frmMax

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitForm()
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
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
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_GlAccountID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_InterestRate = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_AccountNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_VendorID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_CompanyID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.glaccountid = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_AccountType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.accounttype = New Infragistics.Win.Misc.UltraLabel()
        Me.Accountno = New Infragistics.Win.Misc.UltraLabel()
        Me.finpartyid = New Infragistics.Win.Misc.UltraLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        CType(Me.cmb_GlAccountID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_InterestRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_AccountNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_VendorID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_AccountType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmb_GlAccountID)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txt_InterestRate)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txt_AccountNum)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmb_VendorID)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraLabel13)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmb_CompanyID)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.glaccountid)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.cmb_AccountType)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.accounttype)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.Accountno)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.finpartyid)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(451, 210)
        Me.UltraPanel1.TabIndex = 0
        '
        'UltraLabel1
        '
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(14, 117)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(69, 14)
        Me.UltraLabel1.TabIndex = 6
        Me.UltraLabel1.Text = "Interest Rate"
        '
        'cmb_GlAccountID
        '
        Me.cmb_GlAccountID.Location = New System.Drawing.Point(86, 170)
        Me.cmb_GlAccountID.Name = "cmb_GlAccountID"
        Me.cmb_GlAccountID.Size = New System.Drawing.Size(343, 22)
        Me.cmb_GlAccountID.TabIndex = 11
        '
        'txt_InterestRate
        '
        Me.txt_InterestRate.Location = New System.Drawing.Point(86, 113)
        Me.txt_InterestRate.Name = "txt_InterestRate"
        Me.txt_InterestRate.Size = New System.Drawing.Size(148, 21)
        Me.txt_InterestRate.TabIndex = 7
        '
        'txt_AccountNum
        '
        Me.txt_AccountNum.Location = New System.Drawing.Point(86, 85)
        Me.txt_AccountNum.Name = "txt_AccountNum"
        Me.txt_AccountNum.Size = New System.Drawing.Size(148, 21)
        Me.txt_AccountNum.TabIndex = 5
        '
        'cmb_VendorID
        '
        Me.cmb_VendorID.Location = New System.Drawing.Point(86, 56)
        Me.cmb_VendorID.Name = "cmb_VendorID"
        Me.cmb_VendorID.Size = New System.Drawing.Size(343, 22)
        Me.cmb_VendorID.TabIndex = 3
        '
        'UltraLabel13
        '
        Appearance1.FontData.SizeInPoints = 8.25!
        Appearance1.TextHAlignAsString = "Right"
        Appearance1.TextVAlignAsString = "Middle"
        Me.UltraLabel13.Appearance = Appearance1
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(30, 31)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel13.TabIndex = 0
        Me.UltraLabel13.Text = "Company"
        '
        'cmb_CompanyID
        '
        Me.cmb_CompanyID.Location = New System.Drawing.Point(86, 27)
        Me.cmb_CompanyID.Name = "cmb_CompanyID"
        Me.cmb_CompanyID.Size = New System.Drawing.Size(343, 22)
        Me.cmb_CompanyID.TabIndex = 1
        '
        'glaccountid
        '
        Me.glaccountid.AutoSize = True
        Me.glaccountid.Location = New System.Drawing.Point(38, 174)
        Me.glaccountid.Name = "glaccountid"
        Me.glaccountid.Size = New System.Drawing.Size(45, 14)
        Me.glaccountid.TabIndex = 10
        Me.glaccountid.Text = "Account"
        '
        'cmb_AccountType
        '
        Appearance2.BackColor = System.Drawing.SystemColors.Window
        Appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmb_AccountType.DisplayLayout.Appearance = Appearance2
        Me.cmb_AccountType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmb_AccountType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.BorderColor = System.Drawing.SystemColors.Window
        Me.cmb_AccountType.DisplayLayout.GroupByBox.Appearance = Appearance3
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmb_AccountType.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance4
        Me.cmb_AccountType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance5.BackColor2 = System.Drawing.SystemColors.Control
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance5.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmb_AccountType.DisplayLayout.GroupByBox.PromptAppearance = Appearance5
        Me.cmb_AccountType.DisplayLayout.MaxColScrollRegions = 1
        Me.cmb_AccountType.DisplayLayout.MaxRowScrollRegions = 1
        Appearance6.BackColor = System.Drawing.SystemColors.Window
        Appearance6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmb_AccountType.DisplayLayout.Override.ActiveCellAppearance = Appearance6
        Appearance7.BackColor = System.Drawing.SystemColors.Highlight
        Appearance7.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmb_AccountType.DisplayLayout.Override.ActiveRowAppearance = Appearance7
        Me.cmb_AccountType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmb_AccountType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance8.BackColor = System.Drawing.SystemColors.Window
        Me.cmb_AccountType.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Appearance9.BorderColor = System.Drawing.Color.Silver
        Appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmb_AccountType.DisplayLayout.Override.CellAppearance = Appearance9
        Me.cmb_AccountType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmb_AccountType.DisplayLayout.Override.CellPadding = 0
        Appearance10.BackColor = System.Drawing.SystemColors.Control
        Appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance10.BorderColor = System.Drawing.SystemColors.Window
        Me.cmb_AccountType.DisplayLayout.Override.GroupByRowAppearance = Appearance10
        Appearance11.TextHAlignAsString = "Left"
        Me.cmb_AccountType.DisplayLayout.Override.HeaderAppearance = Appearance11
        Me.cmb_AccountType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmb_AccountType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance12.BackColor = System.Drawing.SystemColors.Window
        Appearance12.BorderColor = System.Drawing.Color.Silver
        Me.cmb_AccountType.DisplayLayout.Override.RowAppearance = Appearance12
        Me.cmb_AccountType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance13.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmb_AccountType.DisplayLayout.Override.TemplateAddRowAppearance = Appearance13
        Me.cmb_AccountType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmb_AccountType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmb_AccountType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmb_AccountType.Location = New System.Drawing.Point(86, 141)
        Me.cmb_AccountType.Name = "cmb_AccountType"
        Me.cmb_AccountType.Size = New System.Drawing.Size(148, 22)
        Me.cmb_AccountType.TabIndex = 9
        Me.cmb_AccountType.Text = "AccountType"
        '
        'accounttype
        '
        Me.accounttype.AutoSize = True
        Me.accounttype.Location = New System.Drawing.Point(10, 145)
        Me.accounttype.Name = "accounttype"
        Me.accounttype.Size = New System.Drawing.Size(73, 14)
        Me.accounttype.TabIndex = 8
        Me.accounttype.Text = "Account Type"
        '
        'Accountno
        '
        Me.Accountno.AutoSize = True
        Me.Accountno.Location = New System.Drawing.Point(17, 88)
        Me.Accountno.Name = "Accountno"
        Me.Accountno.Size = New System.Drawing.Size(66, 14)
        Me.Accountno.TabIndex = 4
        Me.Accountno.Text = "Account No."
        '
        'finpartyid
        '
        Me.finpartyid.AutoSize = True
        Me.finpartyid.Location = New System.Drawing.Point(20, 59)
        Me.finpartyid.Name = "finpartyid"
        Me.finpartyid.Size = New System.Drawing.Size(63, 14)
        Me.finpartyid.TabIndex = 2
        Me.finpartyid.Text = "Bank Name"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 210)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(451, 34)
        Me.Panel4.TabIndex = 1
        '
        'btnSave
        '
        Appearance14.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance14
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(187, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 34)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance15.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance15
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(275, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 34)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance16.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance16
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(363, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 34)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'frmBankAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "Bank Account"
        Me.ClientSize = New System.Drawing.Size(451, 244)
        Me.Controls.Add(Me.UltraPanel1)
        Me.Controls.Add(Me.Panel4)
        Me.Name = "frmBankAccount"
        Me.Text = "Bank Account"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        CType(Me.cmb_GlAccountID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_InterestRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_AccountNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_VendorID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_AccountType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraCombo1 As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents glaccountid As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_AccountType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents accounttype As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Accountno As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents finpartyid As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_VendorID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_CompanyID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_GlAccountID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_InterestRate As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_AccountNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
End Class
