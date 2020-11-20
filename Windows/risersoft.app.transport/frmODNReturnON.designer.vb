Imports Infragistics.Win.UltraWinGrid
Imports risersoft.app.shared
Imports risersoft.app.mxent
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmODNReturnON
    Inherits frmMax
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.InitForm()
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_ChallanNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_ReturnedOn As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraLabel23 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_ChallanDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_VouchNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_ChallanNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_ReturnedOn = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel11 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_ReceivedByID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_ReturnChallanNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_ReturnChallanDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dt_ChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ChallanNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.dt_ReturnedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ReceivedByID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_ReturnChallanNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_ReturnChallanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraLabel23)
        Me.Panel1.Controls.Add(Me.dt_ChallanDate)
        Me.Panel1.Controls.Add(Me.txt_VouchNum)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.txt_ChallanNum)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(699, 48)
        Me.Panel1.TabIndex = 0
        '
        'UltraLabel23
        '
        Appearance1.TextHAlignAsString = "Right"
        Me.UltraLabel23.Appearance = Appearance1
        Me.UltraLabel23.AutoSize = True
        Me.UltraLabel23.Location = New System.Drawing.Point(503, 15)
        Me.UltraLabel23.Name = "UltraLabel23"
        Me.UltraLabel23.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel23.TabIndex = 4
        Me.UltraLabel23.Text = "Voucher No."
        '
        'dt_ChallanDate
        '
        Me.dt_ChallanDate.FormatString = "ddd dd MMM yyyy"
        Me.dt_ChallanDate.Location = New System.Drawing.Point(116, 12)
        Me.dt_ChallanDate.Name = "dt_ChallanDate"
        Me.dt_ChallanDate.NullText = "Not Defined"
        Me.dt_ChallanDate.ReadOnly = True
        Me.dt_ChallanDate.Size = New System.Drawing.Size(142, 21)
        Me.dt_ChallanDate.TabIndex = 0
        '
        'txt_VouchNum
        '
        Appearance2.FontData.BoldAsString = "False"
        Appearance2.FontData.ItalicAsString = "False"
        Appearance2.FontData.Name = "Arial"
        Appearance2.FontData.SizeInPoints = 8.25!
        Appearance2.FontData.StrikeoutAsString = "False"
        Appearance2.FontData.UnderlineAsString = "False"
        Me.txt_VouchNum.Appearance = Appearance2
        Me.txt_VouchNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_VouchNum.Location = New System.Drawing.Point(573, 12)
        Me.txt_VouchNum.Name = "txt_VouchNum"
        Me.txt_VouchNum.ReadOnly = True
        Me.txt_VouchNum.Size = New System.Drawing.Size(120, 21)
        Me.txt_VouchNum.TabIndex = 2
        '
        'UltraLabel2
        '
        Appearance3.TextHAlignAsString = "Right"
        Me.UltraLabel2.Appearance = Appearance3
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(305, 15)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel2.TabIndex = 0
        Me.UltraLabel2.Text = "Challan No."
        '
        'UltraLabel1
        '
        Appearance4.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance4
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(43, 15)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(70, 14)
        Me.UltraLabel1.TabIndex = 2
        Me.UltraLabel1.Text = "Challan Date"
        '
        'txt_ChallanNum
        '
        Appearance5.FontData.BoldAsString = "False"
        Appearance5.FontData.ItalicAsString = "False"
        Appearance5.FontData.Name = "Arial"
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.FontData.StrikeoutAsString = "False"
        Appearance5.FontData.UnderlineAsString = "False"
        Me.txt_ChallanNum.Appearance = Appearance5
        Me.txt_ChallanNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_ChallanNum.Location = New System.Drawing.Point(371, 12)
        Me.txt_ChallanNum.Name = "txt_ChallanNum"
        Me.txt_ChallanNum.ReadOnly = True
        Me.txt_ChallanNum.Size = New System.Drawing.Size(115, 21)
        Me.txt_ChallanNum.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 133)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(699, 41)
        Me.Panel4.TabIndex = 3
        '
        'btnSave
        '
        Appearance6.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance6
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(435, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 41)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Appearance7.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance7
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(523, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 41)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOK
        '
        Appearance8.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance8
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(611, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 41)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        '
        'UltraLabel8
        '
        Appearance9.TextHAlignAsString = "Right"
        Me.UltraLabel8.Appearance = Appearance9
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(501, 63)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(69, 14)
        Me.UltraLabel8.TabIndex = 11
        Me.UltraLabel8.Text = "Returned On"
        '
        'dt_ReturnedOn
        '
        Appearance10.FontData.BoldAsString = "False"
        Appearance10.FontData.ItalicAsString = "False"
        Appearance10.FontData.Name = "Arial"
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.FontData.StrikeoutAsString = "False"
        Appearance10.FontData.UnderlineAsString = "False"
        Me.dt_ReturnedOn.Appearance = Appearance10
        Me.dt_ReturnedOn.FormatString = "dddd dd MMM yyyy"
        Me.dt_ReturnedOn.Location = New System.Drawing.Point(573, 60)
        Me.dt_ReturnedOn.Name = "dt_ReturnedOn"
        Me.dt_ReturnedOn.NullText = "Not Defined"
        Me.dt_ReturnedOn.Size = New System.Drawing.Size(120, 21)
        Me.dt_ReturnedOn.TabIndex = 1
        '
        'UltraLabel11
        '
        Appearance11.TextHAlignAsString = "Right"
        Me.UltraLabel11.Appearance = Appearance11
        Me.UltraLabel11.AutoSize = True
        Me.UltraLabel11.Location = New System.Drawing.Point(13, 96)
        Me.UltraLabel11.Name = "UltraLabel11"
        Me.UltraLabel11.Size = New System.Drawing.Size(68, 14)
        Me.UltraLabel11.TabIndex = 37
        Me.UltraLabel11.Text = "Received By"
        '
        'cmb_ReceivedByID
        '
        Me.cmb_ReceivedByID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ReceivedByID.DisplayMember = "FULLName"
        Me.cmb_ReceivedByID.Location = New System.Drawing.Point(84, 92)
        Me.cmb_ReceivedByID.Name = "cmb_ReceivedByID"
        Me.cmb_ReceivedByID.ReadOnly = True
        Me.cmb_ReceivedByID.Size = New System.Drawing.Size(609, 22)
        Me.cmb_ReceivedByID.TabIndex = 4
        Me.cmb_ReceivedByID.ValueMember = "UserID"
        '
        'txt_ReturnChallanNum
        '
        Appearance12.FontData.BoldAsString = "False"
        Appearance12.FontData.ItalicAsString = "False"
        Appearance12.FontData.Name = "Arial"
        Appearance12.FontData.SizeInPoints = 8.25!
        Appearance12.FontData.StrikeoutAsString = "False"
        Appearance12.FontData.UnderlineAsString = "False"
        Me.txt_ReturnChallanNum.Appearance = Appearance12
        Me.txt_ReturnChallanNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_ReturnChallanNum.Location = New System.Drawing.Point(371, 60)
        Me.txt_ReturnChallanNum.Name = "txt_ReturnChallanNum"
        Me.txt_ReturnChallanNum.Size = New System.Drawing.Size(115, 21)
        Me.txt_ReturnChallanNum.TabIndex = 38
        '
        'UltraLabel3
        '
        Appearance13.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance13
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(267, 64)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(101, 14)
        Me.UltraLabel3.TabIndex = 39
        Me.UltraLabel3.Text = "Return Challan No."
        '
        'dt_ReturnChallanDate
        '
        Appearance14.FontData.BoldAsString = "False"
        Appearance14.FontData.ItalicAsString = "False"
        Appearance14.FontData.Name = "Arial"
        Appearance14.FontData.SizeInPoints = 8.25!
        Appearance14.FontData.StrikeoutAsString = "False"
        Appearance14.FontData.UnderlineAsString = "False"
        Me.dt_ReturnChallanDate.Appearance = Appearance14
        Me.dt_ReturnChallanDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_ReturnChallanDate.Location = New System.Drawing.Point(116, 60)
        Me.dt_ReturnChallanDate.Name = "dt_ReturnChallanDate"
        Me.dt_ReturnChallanDate.NullText = "Not Defined"
        Me.dt_ReturnChallanDate.Size = New System.Drawing.Size(142, 21)
        Me.dt_ReturnChallanDate.TabIndex = 40
        '
        'UltraLabel4
        '
        Appearance15.TextHAlignAsString = "Right"
        Me.UltraLabel4.Appearance = Appearance15
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(6, 63)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(107, 14)
        Me.UltraLabel4.TabIndex = 41
        Me.UltraLabel4.Text = "Return Challan Date"
        '
        'frmODNReturnON
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Return Detail"
        Me.ClientSize = New System.Drawing.Size(699, 174)
        Me.Controls.Add(Me.UltraLabel4)
        Me.Controls.Add(Me.dt_ReturnChallanDate)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.dt_ReturnedOn)
        Me.Controls.Add(Me.UltraLabel8)
        Me.Controls.Add(Me.txt_ReturnChallanNum)
        Me.Controls.Add(Me.UltraLabel11)
        Me.Controls.Add(Me.cmb_ReceivedByID)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.Name = "frmODNReturnON"
        Me.Text = "Return Detail"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dt_ChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ChallanNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.dt_ReturnedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ReceivedByID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_ReturnChallanNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_ReturnChallanDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraLabel23 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_VouchNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_ChallanDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraLabel11 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_ReceivedByID As UltraCombo
    Friend WithEvents txt_ReturnChallanNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_ReturnChallanDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel

#End Region
End Class

