Imports ug = Infragistics.Win.UltraWinGrid
Imports we = Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmCostElement
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmb_GLAccountID As Infragistics.Win.UltraWinGrid.UltraCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_CostElemType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_CostElemName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_CostElemCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_SubLedgerType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_GLAccountID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_CostElemGroupID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.cmb_CostElemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CostElemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CostElemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_SubLedgerType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_GLAccountID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.cmb_CostElemGroupID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmb_CostElemGroupID)
        Me.Panel1.Controls.Add(Me.UltraLabel4)
        Me.Panel1.Controls.Add(Me.UltraLabel3)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.cmb_CostElemType)
        Me.Panel1.Controls.Add(Me.txt_CostElemName)
        Me.Panel1.Controls.Add(Me.txt_CostElemCode)
        Me.Panel1.Controls.Add(Me.UltraLabel6)
        Me.Panel1.Controls.Add(Me.cmb_SubLedgerType)
        Me.Panel1.Controls.Add(Me.cmb_GLAccountID)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(592, 195)
        Me.Panel1.TabIndex = 0
        '
        'UltraLabel4
        '
        Appearance2.FontData.SizeInPoints = 8.25!
        Appearance2.TextHAlignAsString = "Right"
        Appearance2.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance2
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(318, 89)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(68, 14)
        Me.UltraLabel4.TabIndex = 18
        Me.UltraLabel4.Text = "Ledger Type"
        '
        'UltraLabel3
        '
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance3
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(55, 125)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(78, 14)
        Me.UltraLabel3.TabIndex = 17
        Me.UltraLabel3.Text = "Account Name"
        '
        'UltraLabel2
        '
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance4
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(59, 90)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(74, 14)
        Me.UltraLabel2.TabIndex = 16
        Me.UltraLabel2.Text = "Element Type"
        '
        'UltraLabel1
        '
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance5
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(57, 57)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(76, 14)
        Me.UltraLabel1.TabIndex = 15
        Me.UltraLabel1.Text = "Element Code"
        '
        'cmb_CostElemType
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.cmb_CostElemType.Appearance = Appearance6
        Me.cmb_CostElemType.Location = New System.Drawing.Point(136, 86)
        Me.cmb_CostElemType.Name = "cmb_CostElemType"
        Me.cmb_CostElemType.ReadOnly = True
        Me.cmb_CostElemType.Size = New System.Drawing.Size(139, 22)
        Me.cmb_CostElemType.TabIndex = 14
        Me.cmb_CostElemType.Text = "UltraCombo5"
        '
        'txt_CostElemName
        '
        Me.txt_CostElemName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_CostElemName.Location = New System.Drawing.Point(136, 20)
        Me.txt_CostElemName.Name = "txt_CostElemName"
        Me.txt_CostElemName.Size = New System.Drawing.Size(424, 21)
        Me.txt_CostElemName.TabIndex = 1
        Me.txt_CostElemName.Text = "ULTRATEXTEDITOR1"
        '
        'txt_CostElemCode
        '
        Me.txt_CostElemCode.Location = New System.Drawing.Point(136, 54)
        Me.txt_CostElemCode.Name = "txt_CostElemCode"
        Me.txt_CostElemCode.Size = New System.Drawing.Size(139, 21)
        Me.txt_CostElemCode.TabIndex = 3
        Me.txt_CostElemCode.Text = "UltraTextEditor1"
        '
        'UltraLabel6
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance7
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(54, 24)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(79, 14)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Element Name"
        '
        'cmb_SubLedgerType
        '
        Appearance8.FontData.BoldAsString = "False"
        Me.cmb_SubLedgerType.Appearance = Appearance8
        Me.cmb_SubLedgerType.Location = New System.Drawing.Point(389, 86)
        Me.cmb_SubLedgerType.Name = "cmb_SubLedgerType"
        Me.cmb_SubLedgerType.Size = New System.Drawing.Size(171, 22)
        Me.cmb_SubLedgerType.TabIndex = 9
        Me.cmb_SubLedgerType.Text = "UltraCombo5"
        '
        'cmb_GLAccountID
        '
        Appearance9.FontData.BoldAsString = "False"
        Me.cmb_GLAccountID.Appearance = Appearance9
        Me.cmb_GLAccountID.Location = New System.Drawing.Point(136, 121)
        Me.cmb_GLAccountID.Name = "cmb_GLAccountID"
        Me.cmb_GLAccountID.Size = New System.Drawing.Size(424, 22)
        Me.cmb_GLAccountID.TabIndex = 13
        Me.cmb_GLAccountID.Text = "UltraCombo5"
        '
        'btnOK
        '
        Appearance10.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance10
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(504, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 43)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Appearance11.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance11
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(416, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 43)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Appearance12.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance12
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(328, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 43)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 195)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(592, 43)
        Me.Panel4.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(66, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 14)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Group Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_CostElemGroupID
        '
        Appearance1.FontData.BoldAsString = "False"
        Me.cmb_CostElemGroupID.Appearance = Appearance1
        Me.cmb_CostElemGroupID.Location = New System.Drawing.Point(136, 157)
        Me.cmb_CostElemGroupID.Name = "cmb_CostElemGroupID"
        Me.cmb_CostElemGroupID.Size = New System.Drawing.Size(424, 22)
        Me.cmb_CostElemGroupID.TabIndex = 20
        Me.cmb_CostElemGroupID.Text = "UltraCombo5"
        '
        'frmCostElement
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Cost Element"
        Me.ClientSize = New System.Drawing.Size(592, 238)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCostElement"
        Me.Text = "Cost Element"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmb_CostElemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CostElemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CostElemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_SubLedgerType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_GLAccountID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.cmb_CostElemGroupID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmb_SubLedgerType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CostElemCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_CostElemName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel4 As Windows.Forms.Panel
    Friend WithEvents cmb_CostElemType As ug.UltraCombo
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents cmb_CostElemGroupID As ug.UltraCombo

#End Region
End Class

