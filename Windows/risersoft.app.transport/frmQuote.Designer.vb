<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmQuote
	Inherits frmMax
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txt_quotekvaqty As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_quoteDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_stationto As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_stationFrom As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_VehicleType As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents btnEditQuote As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDelQuote As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddQuote As Infragistics.Win.Misc.UltraButton
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnEditQuote = New Infragistics.Win.Misc.UltraButton()
        Me.btnDelQuote = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddQuote = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_quotekvaqty = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_quoteDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_stationto = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_stationFrom = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_VehicleType = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.txt_quotekvaqty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_quoteDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_stationto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_stationFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_VehicleType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 349)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(640, 48)
        Me.Panel4.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance1
        Me.btnSave.Location = New System.Drawing.Point(352, 8)
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
        Me.btnCancel.Location = New System.Drawing.Point(448, 8)
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
        Me.btnOK.Location = New System.Drawing.Point(544, 8)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 32)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnEditQuote)
        Me.Panel3.Controls.Add(Me.btnDelQuote)
        Me.Panel3.Controls.Add(Me.btnAddQuote)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 309)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(640, 40)
        Me.Panel3.TabIndex = 1
        '
        'btnEditQuote
        '
        Me.btnEditQuote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditQuote.Location = New System.Drawing.Point(480, 8)
        Me.btnEditQuote.Name = "btnEditQuote"
        Me.btnEditQuote.Size = New System.Drawing.Size(70, 24)
        Me.btnEditQuote.TabIndex = 1
        Me.btnEditQuote.Text = "&Edit"
        '
        'btnDelQuote
        '
        Me.btnDelQuote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelQuote.Location = New System.Drawing.Point(400, 8)
        Me.btnDelQuote.Name = "btnDelQuote"
        Me.btnDelQuote.Size = New System.Drawing.Size(70, 24)
        Me.btnDelQuote.TabIndex = 0
        Me.btnDelQuote.Text = "&Delete"
        '
        'btnAddQuote
        '
        Me.btnAddQuote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddQuote.Location = New System.Drawing.Point(560, 8)
        Me.btnAddQuote.Name = "btnAddQuote"
        Me.btnAddQuote.Size = New System.Drawing.Size(72, 24)
        Me.btnAddQuote.TabIndex = 2
        Me.btnAddQuote.Text = "&Add New"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraLabel5)
        Me.Panel1.Controls.Add(Me.UltraLabel4)
        Me.Panel1.Controls.Add(Me.UltraLabel3)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.txt_quotekvaqty)
        Me.Panel1.Controls.Add(Me.dt_quoteDate)
        Me.Panel1.Controls.Add(Me.txt_stationto)
        Me.Panel1.Controls.Add(Me.txt_stationFrom)
        Me.Panel1.Controls.Add(Me.txt_VehicleType)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(640, 88)
        Me.Panel1.TabIndex = 0
        '
        'UltraLabel5
        '
        Appearance4.TextHAlignAsString = "Right"
        Me.UltraLabel5.Appearance = Appearance4
        Me.UltraLabel5.Location = New System.Drawing.Point(24, 56)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(104, 24)
        Me.UltraLabel5.TabIndex = 8
        Me.UltraLabel5.Text = "KV/Quality of T/F"
        '
        'UltraLabel4
        '
        Appearance5.TextHAlignAsString = "Right"
        Me.UltraLabel4.Appearance = Appearance5
        Me.UltraLabel4.Location = New System.Drawing.Point(360, 8)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(48, 24)
        Me.UltraLabel4.TabIndex = 2
        Me.UltraLabel4.Text = "Date"
        '
        'UltraLabel3
        '
        Appearance6.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance6
        Me.UltraLabel3.Location = New System.Drawing.Point(360, 32)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(48, 24)
        Me.UltraLabel3.TabIndex = 6
        Me.UltraLabel3.Text = "To"
        '
        'UltraLabel2
        '
        Appearance7.TextHAlignAsString = "Right"
        Me.UltraLabel2.Appearance = Appearance7
        Me.UltraLabel2.Location = New System.Drawing.Point(24, 32)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(104, 24)
        Me.UltraLabel2.TabIndex = 4
        Me.UltraLabel2.Text = "From"
        '
        'UltraLabel1
        '
        Appearance8.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance8
        Me.UltraLabel1.Location = New System.Drawing.Point(24, 8)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(104, 24)
        Me.UltraLabel1.TabIndex = 0
        Me.UltraLabel1.Text = "Vehicle Type"
        '
        'txt_quotekvaqty
        '
        Appearance9.FontData.BoldAsString = "False"
        Appearance9.FontData.ItalicAsString = "False"
        Appearance9.FontData.Name = "Arial"
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.FontData.StrikeoutAsString = "False"
        Appearance9.FontData.UnderlineAsString = "False"
        Me.txt_quotekvaqty.Appearance = Appearance9
        Me.txt_quotekvaqty.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_quotekvaqty.Location = New System.Drawing.Point(136, 56)
        Me.txt_quotekvaqty.Name = "txt_quotekvaqty"
        Me.txt_quotekvaqty.Size = New System.Drawing.Size(184, 21)
        Me.txt_quotekvaqty.TabIndex = 9
        '
        'dt_quoteDate
        '
        Appearance10.FontData.BoldAsString = "False"
        Appearance10.FontData.ItalicAsString = "False"
        Appearance10.FontData.Name = "Arial"
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.FontData.StrikeoutAsString = "False"
        Appearance10.FontData.UnderlineAsString = "False"
        Me.dt_quoteDate.Appearance = Appearance10
        Me.dt_quoteDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_quoteDate.Location = New System.Drawing.Point(416, 8)
        Me.dt_quoteDate.Name = "dt_quoteDate"
        Me.dt_quoteDate.NullText = "Not Defined"
        Me.dt_quoteDate.Size = New System.Drawing.Size(200, 21)
        Me.dt_quoteDate.TabIndex = 3
        '
        'txt_stationto
        '
        Appearance11.FontData.BoldAsString = "False"
        Appearance11.FontData.ItalicAsString = "False"
        Appearance11.FontData.Name = "Arial"
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.FontData.StrikeoutAsString = "False"
        Appearance11.FontData.UnderlineAsString = "False"
        Me.txt_stationto.Appearance = Appearance11
        Me.txt_stationto.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_stationto.Location = New System.Drawing.Point(416, 32)
        Me.txt_stationto.Name = "txt_stationto"
        Me.txt_stationto.Size = New System.Drawing.Size(200, 21)
        Me.txt_stationto.TabIndex = 7
        '
        'txt_stationFrom
        '
        Appearance12.FontData.BoldAsString = "False"
        Appearance12.FontData.ItalicAsString = "False"
        Appearance12.FontData.Name = "Arial"
        Appearance12.FontData.SizeInPoints = 8.25!
        Appearance12.FontData.StrikeoutAsString = "False"
        Appearance12.FontData.UnderlineAsString = "False"
        Me.txt_stationFrom.Appearance = Appearance12
        Me.txt_stationFrom.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_stationFrom.Location = New System.Drawing.Point(136, 32)
        Me.txt_stationFrom.Name = "txt_stationFrom"
        Me.txt_stationFrom.Size = New System.Drawing.Size(184, 21)
        Me.txt_stationFrom.TabIndex = 5
        '
        'txt_VehicleType
        '
        Appearance13.FontData.BoldAsString = "False"
        Appearance13.FontData.ItalicAsString = "False"
        Appearance13.FontData.Name = "Arial"
        Appearance13.FontData.SizeInPoints = 8.25!
        Appearance13.FontData.StrikeoutAsString = "False"
        Appearance13.FontData.UnderlineAsString = "False"
        Me.txt_VehicleType.Appearance = Appearance13
        Me.txt_VehicleType.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_VehicleType.Location = New System.Drawing.Point(136, 8)
        Me.txt_VehicleType.Name = "txt_VehicleType"
        Me.txt_VehicleType.Size = New System.Drawing.Size(184, 21)
        Me.txt_VehicleType.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGrid1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 88)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(640, 221)
        Me.Panel2.TabIndex = 12
        '
        'UltraGrid1
        '
        Me.UltraGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(640, 221)
        Me.UltraGrid1.TabIndex = 0
        Me.UltraGrid1.Text = "UltraGrid1"
        '
        'frmQuote
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "COMPARATIVE STATEMENT OF TRANSPORTER"
        Me.ClientSize = New System.Drawing.Size(640, 397)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.MaximizeBox = False
        Me.Name = "frmQuote"
        Me.Text = "COMPARATIVE STATEMENT OF TRANSPORTER"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txt_quotekvaqty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_quoteDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_stationto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_stationFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_VehicleType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
End Class

