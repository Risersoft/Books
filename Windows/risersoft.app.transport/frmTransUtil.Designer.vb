<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmTransUtil
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel20 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel19 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel18 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel17 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel16 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel15 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel14 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_vehDepartedOn As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_VehicleNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_GRDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_GRNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_vehArrivedOn As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents dt_UtilDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
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
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.UltraLabel20 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel19 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel18 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel17 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel16 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel15 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel14 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_vehDepartedOn = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_VehicleNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_GRDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_GRNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_vehArrivedOn = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.dt_UtilDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_transporterId = New Infragistics.Win.UltraWinGrid.UltraCombo()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.dt_vehDepartedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_VehicleNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_GRDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_GRNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_vehArrivedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_UtilDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_transporterId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 254)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(680, 48)
        Me.Panel4.TabIndex = 14
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance1
        Me.btnSave.Location = New System.Drawing.Point(392, 8)
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
        Me.btnCancel.Location = New System.Drawing.Point(488, 8)
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
        Me.btnOK.Location = New System.Drawing.Point(584, 8)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        '
        'UltraLabel20
        '
        Appearance4.TextHAlignAsString = "Center"
        Me.UltraLabel20.Appearance = Appearance4
        Me.UltraLabel20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel20.Location = New System.Drawing.Point(4, 9)
        Me.UltraLabel20.Name = "UltraLabel20"
        Me.UltraLabel20.Size = New System.Drawing.Size(672, 21)
        Me.UltraLabel20.TabIndex = 0
        Me.UltraLabel20.Text = "Transport Utilization Form (Part I)"
        '
        'UltraLabel19
        '
        Appearance5.TextHAlignAsString = "Center"
        Me.UltraLabel19.Appearance = Appearance5
        Me.UltraLabel19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel19.Location = New System.Drawing.Point(4, 144)
        Me.UltraLabel19.Name = "UltraLabel19"
        Me.UltraLabel19.Size = New System.Drawing.Size(672, 21)
        Me.UltraLabel19.TabIndex = 5
        Me.UltraLabel19.Text = "Transport Utilization Form (Part II)"
        '
        'UltraLabel18
        '
        Appearance6.TextHAlignAsString = "Right"
        Me.UltraLabel18.Appearance = Appearance6
        Me.UltraLabel18.AutoSize = True
        Me.UltraLabel18.Location = New System.Drawing.Point(349, 212)
        Me.UltraLabel18.Name = "UltraLabel18"
        Me.UltraLabel18.Size = New System.Drawing.Size(66, 14)
        Me.UltraLabel18.TabIndex = 12
        Me.UltraLabel18.Text = "Departer On"
        '
        'UltraLabel17
        '
        Appearance7.TextHAlignAsString = "Right"
        Me.UltraLabel17.Appearance = Appearance7
        Me.UltraLabel17.AutoSize = True
        Me.UltraLabel17.Location = New System.Drawing.Point(349, 187)
        Me.UltraLabel17.Name = "UltraLabel17"
        Me.UltraLabel17.Size = New System.Drawing.Size(66, 14)
        Me.UltraLabel17.TabIndex = 8
        Me.UltraLabel17.Text = "GR/LR Date"
        '
        'UltraLabel16
        '
        Appearance8.TextHAlignAsString = "Right"
        Me.UltraLabel16.Appearance = Appearance8
        Me.UltraLabel16.AutoSize = True
        Me.UltraLabel16.Location = New System.Drawing.Point(121, 213)
        Me.UltraLabel16.Name = "UltraLabel16"
        Me.UltraLabel16.Size = New System.Drawing.Size(68, 14)
        Me.UltraLabel16.TabIndex = 10
        Me.UltraLabel16.Text = "Vehicle Num"
        '
        'UltraLabel15
        '
        Appearance9.TextHAlignAsString = "Right"
        Me.UltraLabel15.Appearance = Appearance9
        Me.UltraLabel15.AutoSize = True
        Me.UltraLabel15.Location = New System.Drawing.Point(123, 188)
        Me.UltraLabel15.Name = "UltraLabel15"
        Me.UltraLabel15.Size = New System.Drawing.Size(66, 14)
        Me.UltraLabel15.TabIndex = 6
        Me.UltraLabel15.Text = "GR/LR Num"
        '
        'UltraLabel14
        '
        Appearance10.TextHAlignAsString = "Right"
        Me.UltraLabel14.Appearance = Appearance10
        Me.UltraLabel14.AutoSize = True
        Me.UltraLabel14.Location = New System.Drawing.Point(88, 105)
        Me.UltraLabel14.Name = "UltraLabel14"
        Me.UltraLabel14.Size = New System.Drawing.Size(101, 14)
        Me.UltraLabel14.TabIndex = 3
        Me.UltraLabel14.Text = "Vehicle to arrive on"
        '
        'UltraLabel13
        '
        Appearance11.TextHAlignAsString = "Right"
        Me.UltraLabel13.Appearance = Appearance11
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(155, 77)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(34, 14)
        Me.UltraLabel13.TabIndex = 1
        Me.UltraLabel13.Text = "Dated"
        '
        'dt_vehDepartedOn
        '
        Appearance12.FontData.BoldAsString = "False"
        Appearance12.FontData.ItalicAsString = "False"
        Appearance12.FontData.Name = "Arial"
        Appearance12.FontData.SizeInPoints = 8.25!
        Appearance12.FontData.StrikeoutAsString = "False"
        Appearance12.FontData.UnderlineAsString = "False"
        Me.dt_vehDepartedOn.Appearance = Appearance12
        Me.dt_vehDepartedOn.FormatString = "dddd dd MMM yyyy"
        Me.dt_vehDepartedOn.Location = New System.Drawing.Point(418, 210)
        Me.dt_vehDepartedOn.Name = "dt_vehDepartedOn"
        Me.dt_vehDepartedOn.NullText = "Not Defined"
        Me.dt_vehDepartedOn.Size = New System.Drawing.Size(200, 21)
        Me.dt_vehDepartedOn.TabIndex = 13
        '
        'txt_VehicleNum
        '
        Appearance13.FontData.BoldAsString = "False"
        Appearance13.FontData.ItalicAsString = "False"
        Appearance13.FontData.Name = "Arial"
        Appearance13.FontData.SizeInPoints = 8.25!
        Appearance13.FontData.StrikeoutAsString = "False"
        Appearance13.FontData.UnderlineAsString = "False"
        Me.txt_VehicleNum.Appearance = Appearance13
        Me.txt_VehicleNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_VehicleNum.Location = New System.Drawing.Point(192, 210)
        Me.txt_VehicleNum.Name = "txt_VehicleNum"
        Me.txt_VehicleNum.Size = New System.Drawing.Size(92, 21)
        Me.txt_VehicleNum.TabIndex = 11
        '
        'dt_GRDate
        '
        Appearance14.FontData.BoldAsString = "False"
        Appearance14.FontData.ItalicAsString = "False"
        Appearance14.FontData.Name = "Arial"
        Appearance14.FontData.SizeInPoints = 8.25!
        Appearance14.FontData.StrikeoutAsString = "False"
        Appearance14.FontData.UnderlineAsString = "False"
        Me.dt_GRDate.Appearance = Appearance14
        Me.dt_GRDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_GRDate.Location = New System.Drawing.Point(418, 184)
        Me.dt_GRDate.Name = "dt_GRDate"
        Me.dt_GRDate.NullText = "Not Defined"
        Me.dt_GRDate.Size = New System.Drawing.Size(200, 21)
        Me.dt_GRDate.TabIndex = 9
        '
        'txt_GRNum
        '
        Appearance15.FontData.BoldAsString = "False"
        Appearance15.FontData.ItalicAsString = "False"
        Appearance15.FontData.Name = "Arial"
        Appearance15.FontData.SizeInPoints = 8.25!
        Appearance15.FontData.StrikeoutAsString = "False"
        Appearance15.FontData.UnderlineAsString = "False"
        Me.txt_GRNum.Appearance = Appearance15
        Me.txt_GRNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_GRNum.Location = New System.Drawing.Point(192, 184)
        Me.txt_GRNum.Name = "txt_GRNum"
        Me.txt_GRNum.Size = New System.Drawing.Size(92, 21)
        Me.txt_GRNum.TabIndex = 7
        '
        'dt_vehArrivedOn
        '
        Appearance16.FontData.BoldAsString = "False"
        Appearance16.FontData.ItalicAsString = "False"
        Appearance16.FontData.Name = "Arial"
        Appearance16.FontData.SizeInPoints = 8.25!
        Appearance16.FontData.StrikeoutAsString = "False"
        Appearance16.FontData.UnderlineAsString = "False"
        Me.dt_vehArrivedOn.Appearance = Appearance16
        Me.dt_vehArrivedOn.FormatString = "dddd dd MMM yyyy"
        Me.dt_vehArrivedOn.Location = New System.Drawing.Point(192, 102)
        Me.dt_vehArrivedOn.Name = "dt_vehArrivedOn"
        Me.dt_vehArrivedOn.NullText = "Not Defined"
        Me.dt_vehArrivedOn.Size = New System.Drawing.Size(200, 21)
        Me.dt_vehArrivedOn.TabIndex = 4
        '
        'dt_UtilDate
        '
        Appearance17.FontData.BoldAsString = "False"
        Appearance17.FontData.ItalicAsString = "False"
        Appearance17.FontData.Name = "Arial"
        Appearance17.FontData.SizeInPoints = 8.25!
        Appearance17.FontData.StrikeoutAsString = "False"
        Appearance17.FontData.UnderlineAsString = "False"
        Me.dt_UtilDate.Appearance = Appearance17
        Me.dt_UtilDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_UtilDate.Location = New System.Drawing.Point(192, 74)
        Me.dt_UtilDate.Name = "dt_UtilDate"
        Me.dt_UtilDate.NullText = "Not Defined"
        Me.dt_UtilDate.Size = New System.Drawing.Size(200, 21)
        Me.dt_UtilDate.TabIndex = 2
        '
        'UltraLabel1
        '
        Appearance18.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance18
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(126, 49)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel1.TabIndex = 15
        Me.UltraLabel1.Text = "Transporter"
        '
        'cmb_transporterId
        '
        Me.cmb_transporterId.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.cmb_transporterId.Location = New System.Drawing.Point(192, 45)
        Me.cmb_transporterId.Name = "cmb_transporterId"
        Me.cmb_transporterId.Size = New System.Drawing.Size(426, 22)
        Me.cmb_transporterId.TabIndex = 16
        '
        'frmTransUtil
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "TRANSPORT UTILIZATION FORM"
        Me.ClientSize = New System.Drawing.Size(680, 302)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Controls.Add(Me.cmb_transporterId)
        Me.Controls.Add(Me.UltraLabel20)
        Me.Controls.Add(Me.UltraLabel19)
        Me.Controls.Add(Me.UltraLabel18)
        Me.Controls.Add(Me.UltraLabel17)
        Me.Controls.Add(Me.UltraLabel16)
        Me.Controls.Add(Me.UltraLabel15)
        Me.Controls.Add(Me.UltraLabel14)
        Me.Controls.Add(Me.UltraLabel13)
        Me.Controls.Add(Me.dt_vehDepartedOn)
        Me.Controls.Add(Me.txt_VehicleNum)
        Me.Controls.Add(Me.dt_GRDate)
        Me.Controls.Add(Me.txt_GRNum)
        Me.Controls.Add(Me.dt_vehArrivedOn)
        Me.Controls.Add(Me.dt_UtilDate)
        Me.Controls.Add(Me.Panel4)
        Me.MaximizeBox = False
        Me.Name = "frmTransUtil"
        Me.Text = "TRANSPORT UTILIZATION FORM"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.dt_vehDepartedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_VehicleNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_GRDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_GRNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_vehArrivedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_UtilDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_transporterId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_transporterId As Infragistics.Win.UltraWinGrid.UltraCombo

#End Region
End Class

