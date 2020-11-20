Imports ug = Infragistics.Win.UltraWinGrid
Imports we = Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmGLAccount
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmb_AccSchedID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_GLAccGroupID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dt_AccDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents lblRef As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem3 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmb_IsClearingAcc = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.txt_AccName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_AccCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_SubLedgerType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.dt_AccDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.lblRef = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_GLAccGroupID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmb_AccSchedID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dt_StartDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.dt_FinishDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmb_IsClearingAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_AccName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_AccCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_SubLedgerType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_AccDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_GLAccGroupID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_AccSchedID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_StartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_FinishDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 217)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(592, 48)
        Me.Panel4.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance1
        Me.btnSave.Location = New System.Drawing.Point(304, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 32)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance2
        Me.btnCancel.Location = New System.Drawing.Point(400, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 32)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance3.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance3
        Me.btnOK.Location = New System.Drawing.Point(496, 8)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dt_FinishDate)
        Me.Panel1.Controls.Add(Me.dt_StartDate)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmb_IsClearingAcc)
        Me.Panel1.Controls.Add(Me.txt_AccName)
        Me.Panel1.Controls.Add(Me.txt_AccCode)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.UltraLabel6)
        Me.Panel1.Controls.Add(Me.cmb_SubLedgerType)
        Me.Panel1.Controls.Add(Me.dt_AccDate)
        Me.Panel1.Controls.Add(Me.lblRef)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmb_GLAccGroupID)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.cmb_AccSchedID)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(592, 218)
        Me.Panel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(59, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Account Type"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_IsClearingAcc
        '
        ValueListItem2.DataValue = False
        ValueListItem2.DisplayText = "Normal"
        ValueListItem3.DataValue = True
        ValueListItem3.DisplayText = "Clearing"
        Me.cmb_IsClearingAcc.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem2, ValueListItem3})
        Me.cmb_IsClearingAcc.Location = New System.Drawing.Point(136, 89)
        Me.cmb_IsClearingAcc.Name = "cmb_IsClearingAcc"
        Me.cmb_IsClearingAcc.Size = New System.Drawing.Size(139, 21)
        Me.cmb_IsClearingAcc.TabIndex = 7
        Me.cmb_IsClearingAcc.Text = "UltraComboEditor1"
        '
        'txt_AccName
        '
        Me.txt_AccName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_AccName.Location = New System.Drawing.Point(136, 26)
        Me.txt_AccName.Name = "txt_AccName"
        Me.txt_AccName.Size = New System.Drawing.Size(424, 21)
        Me.txt_AccName.TabIndex = 1
        Me.txt_AccName.Text = "ULTRATEXTEDITOR1"
        '
        'txt_AccCode
        '
        Me.txt_AccCode.Location = New System.Drawing.Point(136, 58)
        Me.txt_AccCode.Name = "txt_AccCode"
        Me.txt_AccCode.Size = New System.Drawing.Size(139, 21)
        Me.txt_AccCode.TabIndex = 3
        Me.txt_AccCode.Text = "UltraTextEditor1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(319, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Ledger Type"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UltraLabel6
        '
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance4
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(55, 30)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(78, 14)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Account Name"
        '
        'cmb_SubLedgerType
        '
        Appearance5.FontData.BoldAsString = "False"
        Me.cmb_SubLedgerType.Appearance = Appearance5
        Me.cmb_SubLedgerType.Location = New System.Drawing.Point(389, 88)
        Me.cmb_SubLedgerType.Name = "cmb_SubLedgerType"
        Me.cmb_SubLedgerType.Size = New System.Drawing.Size(171, 22)
        Me.cmb_SubLedgerType.TabIndex = 9
        Me.cmb_SubLedgerType.Text = "UltraCombo5"
        '
        'dt_AccDate
        '
        Me.dt_AccDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_AccDate.Location = New System.Drawing.Point(389, 58)
        Me.dt_AccDate.Name = "dt_AccDate"
        Me.dt_AccDate.NullText = "Not Defined"
        Me.dt_AccDate.Size = New System.Drawing.Size(171, 21)
        Me.dt_AccDate.TabIndex = 5
        '
        'lblRef
        '
        Me.lblRef.AutoSize = True
        Me.lblRef.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblRef.Location = New System.Drawing.Point(314, 61)
        Me.lblRef.Name = "lblRef"
        Me.lblRef.Size = New System.Drawing.Size(72, 14)
        Me.lblRef.TabIndex = 4
        Me.lblRef.Text = "Opening Date"
        Me.lblRef.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 156)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Financial Group"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_GLAccGroupID
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.cmb_GLAccGroupID.Appearance = Appearance6
        Me.cmb_GLAccGroupID.Location = New System.Drawing.Point(136, 153)
        Me.cmb_GLAccGroupID.Name = "cmb_GLAccGroupID"
        Me.cmb_GLAccGroupID.Size = New System.Drawing.Size(424, 22)
        Me.cmb_GLAccGroupID.TabIndex = 13
        Me.cmb_GLAccGroupID.Text = "UltraCombo5"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 124)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 14)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Accounts Schedule"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_AccSchedID
        '
        Appearance7.FontData.BoldAsString = "False"
        Me.cmb_AccSchedID.Appearance = Appearance7
        Me.cmb_AccSchedID.Location = New System.Drawing.Point(136, 120)
        Me.cmb_AccSchedID.Name = "cmb_AccSchedID"
        Me.cmb_AccSchedID.Size = New System.Drawing.Size(424, 22)
        Me.cmb_AccSchedID.TabIndex = 11
        Me.cmb_AccSchedID.Text = "UltraCombo5"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(101, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(32, 14)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Code"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dt_StartDate
        '
        Me.dt_StartDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_StartDate.Location = New System.Drawing.Point(136, 185)
        Me.dt_StartDate.Name = "dt_StartDate"
        Me.dt_StartDate.NullText = "Not Defined"
        Me.dt_StartDate.Size = New System.Drawing.Size(171, 21)
        Me.dt_StartDate.TabIndex = 14
        '
        'dt_FinishDate
        '
        Me.dt_FinishDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_FinishDate.Location = New System.Drawing.Point(389, 185)
        Me.dt_FinishDate.Name = "dt_FinishDate"
        Me.dt_FinishDate.NullText = "Not Defined"
        Me.dt_FinishDate.Size = New System.Drawing.Size(171, 21)
        Me.dt_FinishDate.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(78, 188)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 14)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Start Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label5.Location = New System.Drawing.Point(326, 188)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 14)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Finish Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmGLAccount
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Account"
        Me.ClientSize = New System.Drawing.Size(592, 265)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGLAccount"
        Me.Text = "Account"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmb_IsClearingAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_AccName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_AccCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_SubLedgerType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_AccDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_GLAccGroupID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_AccSchedID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_StartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_FinishDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmb_SubLedgerType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_AccCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_AccName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_IsClearingAcc As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents dt_FinishDate As we.UltraDateTimeEditor
    Friend WithEvents dt_StartDate As we.UltraDateTimeEditor

#End Region
End Class

