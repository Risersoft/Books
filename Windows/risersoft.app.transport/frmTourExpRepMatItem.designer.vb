<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmTourExpRepMatItem
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
        Me.cmb_ExpClass = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_TransType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_Amount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Particulars = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ExpClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_TransType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Amount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Particulars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmb_ExpClass
        '
        Me.cmb_ExpClass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ExpClass.Location = New System.Drawing.Point(462, 44)
        Me.cmb_ExpClass.Name = "cmb_ExpClass"
        Me.cmb_ExpClass.Size = New System.Drawing.Size(249, 23)
        Me.cmb_ExpClass.TabIndex = 27
        '
        'cmb_TransType
        '
        Me.cmb_TransType.Location = New System.Drawing.Point(96, 44)
        Me.cmb_TransType.Name = "cmb_TransType"
        Me.cmb_TransType.Size = New System.Drawing.Size(216, 23)
        Me.cmb_TransType.TabIndex = 26
        '
        'txt_Amount
        '
        Me.txt_Amount.Location = New System.Drawing.Point(96, 74)
        Me.txt_Amount.Name = "txt_Amount"
        Me.txt_Amount.Size = New System.Drawing.Size(216, 22)
        Me.txt_Amount.TabIndex = 25
        '
        'UltraLabel4
        '
        Appearance1.FontData.SizeInPoints = 8.25!
        Appearance1.TextHAlignAsString = "Right"
        Appearance1.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance1
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(50, 78)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel4.TabIndex = 24
        Me.UltraLabel4.Text = "Amount"
        '
        'UltraLabel2
        '
        Appearance2.FontData.SizeInPoints = 8.25!
        Appearance2.TextHAlignAsString = "Right"
        Appearance2.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance2
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(379, 48)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(80, 14)
        Me.UltraLabel2.TabIndex = 23
        Me.UltraLabel2.Text = "Expence Class"
        '
        'UltraLabel1
        '
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance3
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(32, 48)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(61, 14)
        Me.UltraLabel1.TabIndex = 22
        Me.UltraLabel1.Text = "Trans Type"
        '
        'UltraLabel3
        '
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance4
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(35, 18)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(58, 14)
        Me.UltraLabel3.TabIndex = 21
        Me.UltraLabel3.Text = "Particulars"
        '
        'txt_Particulars
        '
        Me.txt_Particulars.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Particulars.Location = New System.Drawing.Point(96, 15)
        Me.txt_Particulars.Name = "txt_Particulars"
        Me.txt_Particulars.Size = New System.Drawing.Size(615, 22)
        Me.txt_Particulars.TabIndex = 18
        '
        'frmTourExpRepMatItem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Transaction"
        Me.ClientSize = New System.Drawing.Size(727, 112)
        Me.Controls.Add(Me.cmb_ExpClass)
        Me.Controls.Add(Me.cmb_TransType)
        Me.Controls.Add(Me.txt_Particulars)
        Me.Controls.Add(Me.txt_Amount)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.UltraLabel4)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Name = "frmTourExpRepMatItem"
        Me.Text = "Transaction"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ExpClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_TransType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Amount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Particulars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_Particulars As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Amount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmb_ExpClass As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmb_TransType As Infragistics.Win.UltraWinGrid.UltraCombo

#End Region
End Class

