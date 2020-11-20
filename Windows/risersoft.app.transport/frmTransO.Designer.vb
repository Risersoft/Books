<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmTransO
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
    Friend WithEvents cmb_VehicleType As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents UltraLabel12 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel11 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel10 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_transporterId As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_BookRefNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_BookDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents cmb_BookedById As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_HaltHours As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_HaltCharge As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_Freighttons As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_FreightCharge As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_VehSendOn As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_TotalWt As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmb_CampusId As Infragistics.Win.UltraWinGrid.UltraCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem3 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem4 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem5 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem6 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem7 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem8 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem9 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
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
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_VehicleType = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.UltraLabel12 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel11 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel10 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_transporterId = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_BookRefNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_BookDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.cmb_BookedById = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_HaltHours = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_HaltCharge = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_Freighttons = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_FreightCharge = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_VehSendOn = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_TotalWt = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_CampusId = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_ContactUserID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel13 = New Infragistics.Win.Misc.UltraLabel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.cmb_VehicleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_transporterId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_BookRefNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_BookDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_BookedById, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_HaltHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_HaltCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Freighttons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_FreightCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_VehSendOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TotalWt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CampusId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_ContactUserID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 285)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(680, 48)
        Me.Panel4.TabIndex = 26
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
        'cmb_VehicleType
        '
        ValueListItem1.DataValue = "Truck 14"
        ValueListItem2.DataValue = "Truck 16"
        ValueListItem3.DataValue = "Truck 18"
        ValueListItem4.DataValue = "Truck 20"
        ValueListItem5.DataValue = "Truck 22"
        ValueListItem6.DataValue = "Trailer Low Bed"
        ValueListItem7.DataValue = "Trailer Semi Bed"
        ValueListItem8.DataValue = "Tata 407"
        ValueListItem9.DataValue = "Canter"
        Me.cmb_VehicleType.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3, ValueListItem4, ValueListItem5, ValueListItem6, ValueListItem7, ValueListItem8, ValueListItem9})
        Me.cmb_VehicleType.Location = New System.Drawing.Point(264, 177)
        Me.cmb_VehicleType.Name = "cmb_VehicleType"
        Me.cmb_VehicleType.Size = New System.Drawing.Size(213, 21)
        Me.cmb_VehicleType.TabIndex = 9
        Me.cmb_VehicleType.Text = "UltraComboEditor1"
        '
        'UltraLabel12
        '
        Appearance4.TextHAlignAsString = "Right"
        Me.UltraLabel12.Appearance = Appearance4
        Me.UltraLabel12.AutoSize = True
        Me.UltraLabel12.Location = New System.Drawing.Point(353, 228)
        Me.UltraLabel12.Name = "UltraLabel12"
        Me.UltraLabel12.Size = New System.Drawing.Size(28, 14)
        Me.UltraLabel12.TabIndex = 14
        Me.UltraLabel12.Text = "Upto"
        '
        'UltraLabel11
        '
        Appearance5.TextHAlignAsString = "Right"
        Me.UltraLabel11.Appearance = Appearance5
        Me.UltraLabel11.AutoSize = True
        Me.UltraLabel11.Location = New System.Drawing.Point(353, 252)
        Me.UltraLabel11.Name = "UltraLabel11"
        Me.UltraLabel11.Size = New System.Drawing.Size(28, 14)
        Me.UltraLabel11.TabIndex = 18
        Me.UltraLabel11.Text = "After"
        '
        'UltraLabel10
        '
        Appearance6.TextHAlignAsString = "Right"
        Me.UltraLabel10.Appearance = Appearance6
        Me.UltraLabel10.AutoSize = True
        Me.UltraLabel10.Location = New System.Drawing.Point(410, 29)
        Me.UltraLabel10.Name = "UltraLabel10"
        Me.UltraLabel10.Size = New System.Drawing.Size(121, 14)
        Me.UltraLabel10.TabIndex = 24
        Me.UltraLabel10.Text = "Booking Reference No."
        '
        'UltraLabel9
        '
        Appearance7.TextHAlignAsString = "Right"
        Me.UltraLabel9.Appearance = Appearance7
        Me.UltraLabel9.AutoSize = True
        Me.UltraLabel9.Location = New System.Drawing.Point(189, 28)
        Me.UltraLabel9.Name = "UltraLabel9"
        Me.UltraLabel9.Size = New System.Drawing.Size(72, 14)
        Me.UltraLabel9.TabIndex = 22
        Me.UltraLabel9.Text = "Booking Date"
        '
        'UltraLabel8
        '
        Appearance8.TextHAlignAsString = "Right"
        Me.UltraLabel8.Appearance = Appearance8
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(200, 54)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(61, 14)
        Me.UltraLabel8.TabIndex = 20
        Me.UltraLabel8.Text = "Booking By"
        '
        'UltraLabel7
        '
        Appearance9.TextHAlignAsString = "Right"
        Me.UltraLabel7.Appearance = Appearance9
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(176, 252)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(85, 14)
        Me.UltraLabel7.TabIndex = 16
        Me.UltraLabel7.Text = "Halting Charges"
        '
        'UltraLabel6
        '
        Appearance10.TextHAlignAsString = "Right"
        Me.UltraLabel6.Appearance = Appearance10
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(110, 228)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(151, 14)
        Me.UltraLabel6.TabIndex = 12
        Me.UltraLabel6.Text = "Total Freight Charges(In Rs.)"
        '
        'UltraLabel5
        '
        Appearance11.TextHAlignAsString = "Right"
        Me.UltraLabel5.Appearance = Appearance11
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(147, 204)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(114, 14)
        Me.UltraLabel5.TabIndex = 10
        Me.UltraLabel5.Text = "Send Vehicle on Date"
        '
        'UltraLabel4
        '
        Appearance12.TextHAlignAsString = "Right"
        Me.UltraLabel4.Appearance = Appearance12
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(191, 180)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(70, 14)
        Me.UltraLabel4.TabIndex = 8
        Me.UltraLabel4.Text = "Vehicle Type"
        '
        'UltraLabel3
        '
        Appearance13.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance13
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(44, 155)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(217, 14)
        Me.UltraLabel3.TabIndex = 6
        Me.UltraLabel3.Text = "Total Shipment Weight (Approx.) (In Tons)"
        '
        'UltraLabel2
        '
        Appearance14.TextHAlignAsString = "Right"
        Me.UltraLabel2.Appearance = Appearance14
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(174, 131)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(87, 14)
        Me.UltraLabel2.TabIndex = 4
        Me.UltraLabel2.Text = "Send Vehicle To"
        '
        'UltraLabel1
        '
        Appearance15.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance15
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(198, 80)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel1.TabIndex = 0
        Me.UltraLabel1.Text = "Transporter"
        '
        'cmb_transporterId
        '
        Me.cmb_transporterId.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.cmb_transporterId.Location = New System.Drawing.Point(264, 76)
        Me.cmb_transporterId.Name = "cmb_transporterId"
        Me.cmb_transporterId.Size = New System.Drawing.Size(380, 22)
        Me.cmb_transporterId.TabIndex = 1
        '
        'txt_BookRefNum
        '
        Appearance16.FontData.BoldAsString = "False"
        Appearance16.FontData.ItalicAsString = "False"
        Appearance16.FontData.Name = "Arial"
        Appearance16.FontData.SizeInPoints = 8.25!
        Appearance16.FontData.StrikeoutAsString = "False"
        Appearance16.FontData.UnderlineAsString = "False"
        Me.txt_BookRefNum.Appearance = Appearance16
        Me.txt_BookRefNum.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_BookRefNum.Location = New System.Drawing.Point(534, 26)
        Me.txt_BookRefNum.Name = "txt_BookRefNum"
        Me.txt_BookRefNum.Size = New System.Drawing.Size(110, 21)
        Me.txt_BookRefNum.TabIndex = 25
        '
        'dt_BookDate
        '
        Appearance17.FontData.BoldAsString = "False"
        Appearance17.FontData.ItalicAsString = "False"
        Appearance17.FontData.Name = "Arial"
        Appearance17.FontData.SizeInPoints = 8.25!
        Appearance17.FontData.StrikeoutAsString = "False"
        Appearance17.FontData.UnderlineAsString = "False"
        Me.dt_BookDate.Appearance = Appearance17
        Me.dt_BookDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_BookDate.Location = New System.Drawing.Point(264, 25)
        Me.dt_BookDate.Name = "dt_BookDate"
        Me.dt_BookDate.NullText = "Not Defined"
        Me.dt_BookDate.Size = New System.Drawing.Size(134, 21)
        Me.dt_BookDate.TabIndex = 23
        '
        'cmb_BookedById
        '
        Me.cmb_BookedById.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
        Me.cmb_BookedById.Location = New System.Drawing.Point(264, 50)
        Me.cmb_BookedById.Name = "cmb_BookedById"
        Me.cmb_BookedById.Size = New System.Drawing.Size(380, 22)
        Me.cmb_BookedById.TabIndex = 21
        '
        'txt_HaltHours
        '
        Appearance18.FontData.BoldAsString = "False"
        Appearance18.FontData.ItalicAsString = "False"
        Appearance18.FontData.Name = "Arial"
        Appearance18.FontData.SizeInPoints = 8.25!
        Appearance18.FontData.StrikeoutAsString = "False"
        Appearance18.FontData.UnderlineAsString = "False"
        Appearance18.TextHAlignAsString = "Right"
        Me.txt_HaltHours.Appearance = Appearance18
        Me.txt_HaltHours.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_HaltHours.Location = New System.Drawing.Point(384, 249)
        Me.txt_HaltHours.Name = "txt_HaltHours"
        Me.txt_HaltHours.Size = New System.Drawing.Size(93, 21)
        Me.txt_HaltHours.TabIndex = 19
        '
        'txt_HaltCharge
        '
        Appearance19.FontData.BoldAsString = "False"
        Appearance19.FontData.ItalicAsString = "False"
        Appearance19.FontData.Name = "Arial"
        Appearance19.FontData.SizeInPoints = 8.25!
        Appearance19.FontData.StrikeoutAsString = "False"
        Appearance19.FontData.UnderlineAsString = "False"
        Appearance19.TextHAlignAsString = "Right"
        Me.txt_HaltCharge.Appearance = Appearance19
        Me.txt_HaltCharge.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_HaltCharge.Location = New System.Drawing.Point(264, 249)
        Me.txt_HaltCharge.Name = "txt_HaltCharge"
        Me.txt_HaltCharge.Size = New System.Drawing.Size(80, 21)
        Me.txt_HaltCharge.TabIndex = 17
        '
        'txt_Freighttons
        '
        Appearance20.FontData.BoldAsString = "False"
        Appearance20.FontData.ItalicAsString = "False"
        Appearance20.FontData.Name = "Arial"
        Appearance20.FontData.SizeInPoints = 8.25!
        Appearance20.FontData.StrikeoutAsString = "False"
        Appearance20.FontData.UnderlineAsString = "False"
        Appearance20.TextHAlignAsString = "Right"
        Me.txt_Freighttons.Appearance = Appearance20
        Me.txt_Freighttons.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_Freighttons.Location = New System.Drawing.Point(384, 225)
        Me.txt_Freighttons.Name = "txt_Freighttons"
        Me.txt_Freighttons.Size = New System.Drawing.Size(93, 21)
        Me.txt_Freighttons.TabIndex = 15
        '
        'txt_FreightCharge
        '
        Appearance21.FontData.BoldAsString = "False"
        Appearance21.FontData.ItalicAsString = "False"
        Appearance21.FontData.Name = "Arial"
        Appearance21.FontData.SizeInPoints = 8.25!
        Appearance21.FontData.StrikeoutAsString = "False"
        Appearance21.FontData.UnderlineAsString = "False"
        Appearance21.TextHAlignAsString = "Right"
        Me.txt_FreightCharge.Appearance = Appearance21
        Me.txt_FreightCharge.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_FreightCharge.Location = New System.Drawing.Point(264, 225)
        Me.txt_FreightCharge.Name = "txt_FreightCharge"
        Me.txt_FreightCharge.Size = New System.Drawing.Size(80, 21)
        Me.txt_FreightCharge.TabIndex = 13
        '
        'dt_VehSendOn
        '
        Appearance22.FontData.BoldAsString = "False"
        Appearance22.FontData.ItalicAsString = "False"
        Appearance22.FontData.Name = "Arial"
        Appearance22.FontData.SizeInPoints = 8.25!
        Appearance22.FontData.StrikeoutAsString = "False"
        Appearance22.FontData.UnderlineAsString = "False"
        Me.dt_VehSendOn.Appearance = Appearance22
        Me.dt_VehSendOn.FormatString = "dddd dd MMM yyyy"
        Me.dt_VehSendOn.Location = New System.Drawing.Point(264, 201)
        Me.dt_VehSendOn.Name = "dt_VehSendOn"
        Me.dt_VehSendOn.NullText = "Not Defined"
        Me.dt_VehSendOn.Size = New System.Drawing.Size(213, 21)
        Me.dt_VehSendOn.TabIndex = 11
        '
        'txt_TotalWt
        '
        Appearance23.FontData.BoldAsString = "False"
        Appearance23.FontData.ItalicAsString = "False"
        Appearance23.FontData.Name = "Arial"
        Appearance23.FontData.SizeInPoints = 8.25!
        Appearance23.FontData.StrikeoutAsString = "False"
        Appearance23.FontData.UnderlineAsString = "False"
        Appearance23.TextHAlignAsString = "Right"
        Me.txt_TotalWt.Appearance = Appearance23
        Me.txt_TotalWt.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.txt_TotalWt.Location = New System.Drawing.Point(264, 152)
        Me.txt_TotalWt.Name = "txt_TotalWt"
        Me.txt_TotalWt.Size = New System.Drawing.Size(213, 21)
        Me.txt_TotalWt.TabIndex = 7
        '
        'cmb_CampusId
        '
        Me.cmb_CampusId.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.cmb_CampusId.Location = New System.Drawing.Point(264, 127)
        Me.cmb_CampusId.Name = "cmb_CampusId"
        Me.cmb_CampusId.Size = New System.Drawing.Size(213, 22)
        Me.cmb_CampusId.TabIndex = 5
        '
        'cmb_ContactUserID
        '
        Me.cmb_ContactUserID.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.cmb_ContactUserID.Location = New System.Drawing.Point(264, 101)
        Me.cmb_ContactUserID.Name = "cmb_ContactUserID"
        Me.cmb_ContactUserID.Size = New System.Drawing.Size(380, 22)
        Me.cmb_ContactUserID.TabIndex = 3
        '
        'UltraLabel13
        '
        Appearance24.TextHAlignAsString = "Right"
        Me.UltraLabel13.Appearance = Appearance24
        Me.UltraLabel13.AutoSize = True
        Me.UltraLabel13.Location = New System.Drawing.Point(191, 104)
        Me.UltraLabel13.Name = "UltraLabel13"
        Me.UltraLabel13.Size = New System.Drawing.Size(70, 14)
        Me.UltraLabel13.TabIndex = 2
        Me.UltraLabel13.Text = "Contact User"
        '
        'frmTransO
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "TRANSPORT ORDER"
        Me.ClientSize = New System.Drawing.Size(680, 333)
        Me.Controls.Add(Me.UltraLabel13)
        Me.Controls.Add(Me.cmb_ContactUserID)
        Me.Controls.Add(Me.cmb_VehicleType)
        Me.Controls.Add(Me.dt_BookDate)
        Me.Controls.Add(Me.UltraLabel12)
        Me.Controls.Add(Me.cmb_BookedById)
        Me.Controls.Add(Me.UltraLabel11)
        Me.Controls.Add(Me.UltraLabel10)
        Me.Controls.Add(Me.txt_BookRefNum)
        Me.Controls.Add(Me.UltraLabel7)
        Me.Controls.Add(Me.UltraLabel9)
        Me.Controls.Add(Me.UltraLabel6)
        Me.Controls.Add(Me.UltraLabel8)
        Me.Controls.Add(Me.UltraLabel5)
        Me.Controls.Add(Me.UltraLabel4)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Controls.Add(Me.cmb_transporterId)
        Me.Controls.Add(Me.txt_HaltHours)
        Me.Controls.Add(Me.txt_HaltCharge)
        Me.Controls.Add(Me.txt_Freighttons)
        Me.Controls.Add(Me.txt_FreightCharge)
        Me.Controls.Add(Me.dt_VehSendOn)
        Me.Controls.Add(Me.txt_TotalWt)
        Me.Controls.Add(Me.cmb_CampusId)
        Me.Controls.Add(Me.Panel4)
        Me.MaximizeBox = False
        Me.Name = "frmTransO"
        Me.Text = "TRANSPORT ORDER"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.cmb_VehicleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_transporterId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_BookRefNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_BookDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_BookedById, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_HaltHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_HaltCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Freighttons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_FreightCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_VehSendOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TotalWt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CampusId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_ContactUserID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmb_ContactUserID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel13 As Infragistics.Win.Misc.UltraLabel

#End Region
End Class

