<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInvoicePOSPayment
    Inherits frmMax

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab9 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl12 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.lblWCT = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_WCTAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblTDS = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_TDSAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_AmountTotPay = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.txt_VouchNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_Dated = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        CType(Me.txt_WCTAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TDSAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_AmountTotPay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraTabPageControl12
        '
        Me.UltraTabPageControl12.Location = New System.Drawing.Point(2, 19)
        Me.UltraTabPageControl12.Name = "UltraTabPageControl12"
        Me.UltraTabPageControl12.Size = New System.Drawing.Size(939, 169)
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txt_VouchNum)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.dt_Dated)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraLabel7)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.lblWCT)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txt_WCTAmount)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.lblTDS)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txt_TDSAmount)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.UltraLabel5)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.txt_AmountTotPay)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(943, 37)
        Me.UltraPanel1.TabIndex = 54
        '
        'lblWCT
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.lblWCT.Appearance = Appearance8
        Me.lblWCT.AutoSize = True
        Me.lblWCT.Location = New System.Drawing.Point(782, 14)
        Me.lblWCT.Name = "lblWCT"
        Me.lblWCT.Size = New System.Drawing.Size(54, 14)
        Me.lblWCT.TabIndex = 25
        Me.lblWCT.Text = "GST-TDS"
        '
        'txt_WCTAmount
        '
        Me.txt_WCTAmount.Location = New System.Drawing.Point(839, 10)
        Me.txt_WCTAmount.Name = "txt_WCTAmount"
        Me.txt_WCTAmount.Size = New System.Drawing.Size(96, 21)
        Me.txt_WCTAmount.TabIndex = 24
        '
        'lblTDS
        '
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.TextHAlignAsString = "Right"
        Appearance9.TextVAlignAsString = "Middle"
        Me.lblTDS.Appearance = Appearance9
        Me.lblTDS.AutoSize = True
        Me.lblTDS.Location = New System.Drawing.Point(643, 13)
        Me.lblTDS.Name = "lblTDS"
        Me.lblTDS.Size = New System.Drawing.Size(27, 14)
        Me.lblTDS.TabIndex = 23
        Me.lblTDS.Text = "TDS"
        '
        'txt_TDSAmount
        '
        Me.txt_TDSAmount.Location = New System.Drawing.Point(673, 10)
        Me.txt_TDSAmount.Name = "txt_TDSAmount"
        Me.txt_TDSAmount.Size = New System.Drawing.Size(96, 21)
        Me.txt_TDSAmount.TabIndex = 22
        '
        'UltraLabel5
        '
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.TextHAlignAsString = "Right"
        Appearance10.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance10
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(485, 13)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel5.TabIndex = 20
        Me.UltraLabel5.Text = "Amount"
        '
        'txt_AmountTotPay
        '
        Me.txt_AmountTotPay.Location = New System.Drawing.Point(531, 10)
        Me.txt_AmountTotPay.Name = "txt_AmountTotPay"
        Me.txt_AmountTotPay.Size = New System.Drawing.Size(96, 21)
        Me.txt_AmountTotPay.TabIndex = 21
        '
        'UltraTabControl1
        '
        Appearance4.BackColor = System.Drawing.SystemColors.Control
        Me.UltraTabControl1.ActiveTabAppearance = Appearance4
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl12)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 37)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance5.BackColor = System.Drawing.SystemColors.Control
        Appearance5.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance5
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTabControl1.Size = New System.Drawing.Size(943, 190)
        Me.UltraTabControl1.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(50)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003
        Me.UltraTabControl1.TabIndex = 55
        Me.UltraTabControl1.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab9.Key = "Payment"
        UltraTab9.TabPage = Me.UltraTabPageControl12
        UltraTab9.Text = "Payment"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab9})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(939, 169)
        '
        'txt_VouchNum
        '
        Me.txt_VouchNum.Location = New System.Drawing.Point(85, 10)
        Me.txt_VouchNum.Name = "txt_VouchNum"
        Me.txt_VouchNum.ReadOnly = True
        Me.txt_VouchNum.Size = New System.Drawing.Size(97, 21)
        Me.txt_VouchNum.TabIndex = 26
        '
        'dt_Dated
        '
        Me.dt_Dated.FormatString = "ddd dd MMM yyyy"
        Me.dt_Dated.Location = New System.Drawing.Point(230, 10)
        Me.dt_Dated.Name = "dt_Dated"
        Me.dt_Dated.NullText = "Not Defined"
        Me.dt_Dated.Size = New System.Drawing.Size(106, 21)
        Me.dt_Dated.TabIndex = 27
        '
        'UltraLabel2
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance6
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(193, 14)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(34, 14)
        Me.UltraLabel2.TabIndex = 29
        Me.UltraLabel2.Text = "Dated"
        '
        'UltraLabel7
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance7
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(9, 14)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(73, 14)
        Me.UltraLabel7.TabIndex = 28
        Me.UltraLabel7.Text = "Voucher Num"
        '
        'frmInvoicePOSPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Caption = "Invoice Payment"
        Me.ClientSize = New System.Drawing.Size(943, 227)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.UltraPanel1)
        Me.Name = "frmInvoicePOSPayment"
        Me.Text = "Invoice Payment"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.PerformLayout()
        Me.UltraPanel1.ResumeLayout(False)
        CType(Me.txt_WCTAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TDSAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_AmountTotPay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents lblWCT As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_WCTAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents lblTDS As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_TDSAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_AmountTotPay As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl12 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents txt_VouchNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_Dated As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
End Class
