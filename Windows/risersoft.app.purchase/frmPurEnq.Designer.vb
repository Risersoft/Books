<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Partial Class frmPurEnq
	Inherits frmMax
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.initForm()
        myview.SetGrid(UltraGridItemList)
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
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_enqDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents cmb_VendorID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_EnqNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmb_campusid As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Company As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_SampleTextType As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_GenSpec As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_wonums As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraGridItemList As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_attentionid As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnDel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton
    Friend WithEvents txt_Encl As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem3 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
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
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Encl = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_GenSpec = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.lblSendSampleText = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_SendSampleText = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_wonums = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_SampleTextType = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmb_campusid = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Company = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_attentionid = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_enqDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.cmb_VendorID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_EnqNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnDel = New Infragistics.Win.Misc.UltraButton()
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton()
        Me.UltraGridItemList = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UEGB_Header = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel3 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UEGB_ItemList = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel1 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDown = New Infragistics.Win.Misc.UltraButton()
        Me.btnUp = New Infragistics.Win.Misc.UltraButton()
        Me.UEGB_ItemDetail = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel2 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.txt_Encl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_GenSpec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.txt_SendSampleText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_wonums, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_SampleTextType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.cmb_campusid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_attentionid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_enqDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_VendorID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_EnqNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_Header.SuspendLayout()
        Me.UltraExpandableGroupBoxPanel3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        CType(Me.UEGB_ItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_ItemList.SuspendLayout()
        Me.UltraExpandableGroupBoxPanel1.SuspendLayout()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_ItemDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel8)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_Encl)
        Me.UltraTabPageControl1.Controls.Add(Me.txt_GenSpec)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraLabel5)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(2, 19)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(830, 100)
        '
        'UltraLabel8
        '
        Appearance1.TextHAlignAsString = "Right"
        Me.UltraLabel8.Appearance = Appearance1
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(68, 15)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(60, 14)
        Me.UltraLabel8.TabIndex = 0
        Me.UltraLabel8.Text = "Enclosures"
        '
        'txt_Encl
        '
        Me.txt_Encl.AcceptsReturn = True
        Me.txt_Encl.Location = New System.Drawing.Point(131, 12)
        Me.txt_Encl.Multiline = True
        Me.txt_Encl.Name = "txt_Encl"
        Me.txt_Encl.Size = New System.Drawing.Size(691, 32)
        Me.txt_Encl.TabIndex = 1
        '
        'txt_GenSpec
        '
        Me.txt_GenSpec.AcceptsReturn = True
        Me.txt_GenSpec.Location = New System.Drawing.Point(131, 52)
        Me.txt_GenSpec.Multiline = True
        Me.txt_GenSpec.Name = "txt_GenSpec"
        Me.txt_GenSpec.Size = New System.Drawing.Size(691, 44)
        Me.txt_GenSpec.TabIndex = 3
        '
        'UltraLabel5
        '
        Appearance2.TextHAlignAsString = "Right"
        Me.UltraLabel5.Appearance = Appearance2
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(16, 56)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(112, 14)
        Me.UltraLabel5.TabIndex = 2
        Me.UltraLabel5.Text = "General Specification"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.lblSendSampleText)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_SendSampleText)
        Me.UltraTabPageControl2.Controls.Add(Me.UltraLabel4)
        Me.UltraTabPageControl2.Controls.Add(Me.txt_wonums)
        Me.UltraTabPageControl2.Controls.Add(Me.UltraLabel6)
        Me.UltraTabPageControl2.Controls.Add(Me.cmb_SampleTextType)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(830, 100)
        '
        'lblSendSampleText
        '
        Appearance3.TextHAlignAsString = "Right"
        Me.lblSendSampleText.Appearance = Appearance3
        Me.lblSendSampleText.AutoSize = True
        Me.lblSendSampleText.Location = New System.Drawing.Point(80, 45)
        Me.lblSendSampleText.Name = "lblSendSampleText"
        Me.lblSendSampleText.Size = New System.Drawing.Size(97, 14)
        Me.lblSendSampleText.TabIndex = 4
        Me.lblSendSampleText.Text = "Send Sample Text"
        '
        'txt_SendSampleText
        '
        Me.txt_SendSampleText.AcceptsReturn = True
        Me.txt_SendSampleText.Location = New System.Drawing.Point(180, 42)
        Me.txt_SendSampleText.Multiline = True
        Me.txt_SendSampleText.Name = "txt_SendSampleText"
        Me.txt_SendSampleText.Size = New System.Drawing.Size(642, 51)
        Me.txt_SendSampleText.TabIndex = 5
        '
        'UltraLabel4
        '
        Appearance4.TextHAlignAsString = "Right"
        Me.UltraLabel4.Appearance = Appearance4
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(412, 17)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(51, 14)
        Me.UltraLabel4.TabIndex = 2
        Me.UltraLabel4.Text = "W.O. No."
        '
        'txt_wonums
        '
        Me.txt_wonums.Location = New System.Drawing.Point(466, 14)
        Me.txt_wonums.Name = "txt_wonums"
        Me.txt_wonums.Size = New System.Drawing.Size(356, 21)
        Me.txt_wonums.TabIndex = 3
        '
        'UltraLabel6
        '
        Appearance5.TextHAlignAsString = "Right"
        Me.UltraLabel6.Appearance = Appearance5
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(17, 17)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(160, 14)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Send to be sent alongwith offer"
        '
        'cmb_SampleTextType
        '
        Me.cmb_SampleTextType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList
        ValueListItem1.DataValue = "Yes"
        ValueListItem2.DataValue = "No"
        ValueListItem3.DataValue = "Other"
        Me.cmb_SampleTextType.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3})
        Me.cmb_SampleTextType.Location = New System.Drawing.Point(180, 14)
        Me.cmb_SampleTextType.Name = "cmb_SampleTextType"
        Me.cmb_SampleTextType.Size = New System.Drawing.Size(156, 21)
        Me.cmb_SampleTextType.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmb_campusid)
        Me.Panel1.Controls.Add(Me.Company)
        Me.Panel1.Controls.Add(Me.cmb_attentionid)
        Me.Panel1.Controls.Add(Me.UltraLabel7)
        Me.Panel1.Controls.Add(Me.dt_enqDate)
        Me.Panel1.Controls.Add(Me.cmb_VendorID)
        Me.Panel1.Controls.Add(Me.txt_EnqNum)
        Me.Panel1.Controls.Add(Me.UltraLabel3)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(840, 72)
        Me.Panel1.TabIndex = 0
        '
        'cmb_campusid
        '
        Me.cmb_campusid.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
        Me.cmb_campusid.Location = New System.Drawing.Point(471, 13)
        Me.cmb_campusid.Name = "cmb_campusid"
        Me.cmb_campusid.Size = New System.Drawing.Size(356, 22)
        Me.cmb_campusid.TabIndex = 1
        '
        'Company
        '
        Appearance6.TextHAlignAsString = "Right"
        Me.Company.Appearance = Appearance6
        Me.Company.AutoSize = True
        Me.Company.Location = New System.Drawing.Point(422, 17)
        Me.Company.Name = "Company"
        Me.Company.Size = New System.Drawing.Size(46, 14)
        Me.Company.TabIndex = 0
        Me.Company.Text = "Campus"
        '
        'cmb_attentionid
        '
        Me.cmb_attentionid.Location = New System.Drawing.Point(471, 41)
        Me.cmb_attentionid.Name = "cmb_attentionid"
        Me.cmb_attentionid.Size = New System.Drawing.Size(356, 22)
        Me.cmb_attentionid.TabIndex = 9
        '
        'UltraLabel7
        '
        Appearance7.TextHAlignAsString = "Right"
        Me.UltraLabel7.Appearance = Appearance7
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(380, 45)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(88, 14)
        Me.UltraLabel7.TabIndex = 8
        Me.UltraLabel7.Text = "Attention Person"
        '
        'dt_enqDate
        '
        Me.dt_enqDate.FormatString = "dddd dd MMM yyyy"
        Me.dt_enqDate.Location = New System.Drawing.Point(78, 13)
        Me.dt_enqDate.Name = "dt_enqDate"
        Me.dt_enqDate.NullText = "Not Defined"
        Me.dt_enqDate.Size = New System.Drawing.Size(114, 21)
        Me.dt_enqDate.TabIndex = 3
        '
        'cmb_VendorID
        '
        Me.cmb_VendorID.Location = New System.Drawing.Point(78, 42)
        Me.cmb_VendorID.Name = "cmb_VendorID"
        Me.cmb_VendorID.Size = New System.Drawing.Size(292, 22)
        Me.cmb_VendorID.TabIndex = 7
        '
        'txt_EnqNum
        '
        Me.txt_EnqNum.Location = New System.Drawing.Point(269, 14)
        Me.txt_EnqNum.Name = "txt_EnqNum"
        Me.txt_EnqNum.Size = New System.Drawing.Size(101, 21)
        Me.txt_EnqNum.TabIndex = 5
        '
        'UltraLabel3
        '
        Appearance8.TextHAlignAsString = "Right"
        Me.UltraLabel3.Appearance = Appearance8
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(34, 46)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(41, 14)
        Me.UltraLabel3.TabIndex = 6
        Me.UltraLabel3.Text = "Vendor"
        '
        'UltraLabel2
        '
        Appearance9.TextHAlignAsString = "Right"
        Me.UltraLabel2.Appearance = Appearance9
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(5, 17)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(70, 14)
        Me.UltraLabel2.TabIndex = 2
        Me.UltraLabel2.Text = "Enquiry Date"
        '
        'UltraLabel1
        '
        Appearance10.TextHAlignAsString = "Right"
        Me.UltraLabel1.Appearance = Appearance10
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(203, 18)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel1.TabIndex = 4
        Me.UltraLabel1.Text = "Enquiry No."
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 640)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(840, 36)
        Me.Panel4.TabIndex = 4
        '
        'btnSave
        '
        Appearance11.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance11
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(576, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 36)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Appearance12.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance12
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(664, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 36)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnOK
        '
        Appearance13.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance13
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(752, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 36)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        '
        'btnDel
        '
        Me.btnDel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDel.Location = New System.Drawing.Point(72, 0)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(70, 27)
        Me.btnDel.TabIndex = 1
        Me.btnDel.Text = "&Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAdd.Location = New System.Drawing.Point(0, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(72, 27)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "&Add New"
        '
        'UltraGridItemList
        '
        Me.UltraGridItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridItemList.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.UltraGridItemList.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridItemList.Name = "UltraGridItemList"
        Me.UltraGridItemList.Size = New System.Drawing.Size(834, 138)
        Me.UltraGridItemList.TabIndex = 0
        Me.UltraGridItemList.Text = "UltraGrid1"
        '
        'UEGB_Header
        '
        Me.UEGB_Header.Controls.Add(Me.UltraExpandableGroupBoxPanel3)
        Me.UEGB_Header.Dock = System.Windows.Forms.DockStyle.Top
        Me.UEGB_Header.ExpandedSize = New System.Drawing.Size(840, 143)
        Me.UEGB_Header.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_Header.Location = New System.Drawing.Point(0, 72)
        Me.UEGB_Header.Name = "UEGB_Header"
        Me.UEGB_Header.Size = New System.Drawing.Size(840, 143)
        Me.UEGB_Header.TabIndex = 1
        Me.UEGB_Header.Text = "Header"
        '
        'UltraExpandableGroupBoxPanel3
        '
        Me.UltraExpandableGroupBoxPanel3.Controls.Add(Me.UltraTabControl1)
        Me.UltraExpandableGroupBoxPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel3.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel3.Name = "UltraExpandableGroupBoxPanel3"
        Me.UltraExpandableGroupBoxPanel3.Size = New System.Drawing.Size(834, 121)
        Me.UltraExpandableGroupBoxPanel3.TabIndex = 0
        '
        'UltraTabControl1
        '
        Appearance14.BackColor = System.Drawing.SystemColors.Control
        Me.UltraTabControl1.ActiveTabAppearance = Appearance14
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Appearance15.BackColor = System.Drawing.SystemColors.Control
        Appearance15.FontData.BoldAsString = "True"
        Me.UltraTabControl1.SelectedTabAppearance = Appearance15
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraTabControl1.Size = New System.Drawing.Size(834, 121)
        Me.UltraTabControl1.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(50)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab4.TabPage = Me.UltraTabPageControl1
        UltraTab4.Text = "General"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Notes"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab4, UltraTab2})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(830, 100)
        '
        'UEGB_ItemList
        '
        Me.UEGB_ItemList.Controls.Add(Me.UltraExpandableGroupBoxPanel1)
        Me.UEGB_ItemList.Dock = System.Windows.Forms.DockStyle.Top
        Me.UEGB_ItemList.ExpandedSize = New System.Drawing.Size(840, 187)
        Me.UEGB_ItemList.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_ItemList.Location = New System.Drawing.Point(0, 215)
        Me.UEGB_ItemList.Name = "UEGB_ItemList"
        Me.UEGB_ItemList.Size = New System.Drawing.Size(840, 187)
        Me.UEGB_ItemList.TabIndex = 2
        Me.UEGB_ItemList.Text = "Item List"
        '
        'UltraExpandableGroupBoxPanel1
        '
        Me.UltraExpandableGroupBoxPanel1.Controls.Add(Me.UltraGridItemList)
        Me.UltraExpandableGroupBoxPanel1.Controls.Add(Me.UltraPanel1)
        Me.UltraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel1.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel1.Name = "UltraExpandableGroupBoxPanel1"
        Me.UltraExpandableGroupBoxPanel1.Size = New System.Drawing.Size(834, 165)
        Me.UltraExpandableGroupBoxPanel1.TabIndex = 0
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnDown)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnUp)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnDel)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnAdd)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 138)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(834, 27)
        Me.UltraPanel1.TabIndex = 52
        '
        'btnDown
        '
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDown.Location = New System.Drawing.Point(225, 0)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(83, 27)
        Me.btnDown.TabIndex = 3
        Me.btnDown.Text = "Move Down"
        '
        'btnUp
        '
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnUp.Location = New System.Drawing.Point(142, 0)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(83, 27)
        Me.btnUp.TabIndex = 2
        Me.btnUp.Text = "Move Up"
        '
        'UEGB_ItemDetail
        '
        Me.UEGB_ItemDetail.Controls.Add(Me.UltraExpandableGroupBoxPanel2)
        Me.UEGB_ItemDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UEGB_ItemDetail.ExpandedSize = New System.Drawing.Size(840, 238)
        Me.UEGB_ItemDetail.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_ItemDetail.Location = New System.Drawing.Point(0, 402)
        Me.UEGB_ItemDetail.Name = "UEGB_ItemDetail"
        Me.UEGB_ItemDetail.Size = New System.Drawing.Size(840, 238)
        Me.UEGB_ItemDetail.TabIndex = 3
        Me.UEGB_ItemDetail.Text = "Item Details"
        '
        'UltraExpandableGroupBoxPanel2
        '
        Me.UltraExpandableGroupBoxPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel2.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel2.Name = "UltraExpandableGroupBoxPanel2"
        Me.UltraExpandableGroupBoxPanel2.Size = New System.Drawing.Size(834, 216)
        Me.UltraExpandableGroupBoxPanel2.TabIndex = 0
        '
        'frmPurEnq
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Purchase Enquiry"
        Me.ClientSize = New System.Drawing.Size(840, 676)
        Me.Controls.Add(Me.UEGB_ItemDetail)
        Me.Controls.Add(Me.UEGB_ItemList)
        Me.Controls.Add(Me.UEGB_Header)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmPurEnq"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchase Enquiry"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.txt_Encl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_GenSpec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        CType(Me.txt_SendSampleText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_wonums, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_SampleTextType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmb_campusid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_attentionid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_enqDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_VendorID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_EnqNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_Header.ResumeLayout(False)
        Me.UltraExpandableGroupBoxPanel3.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        CType(Me.UEGB_ItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_ItemList.ResumeLayout(False)
        Me.UltraExpandableGroupBoxPanel1.ResumeLayout(False)
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ResumeLayout(False)
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_ItemDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UEGB_Header As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel3 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UEGB_ItemList As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel1 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UEGB_ItemDetail As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel2 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents lblSendSampleText As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_SendSampleText As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents btnDown As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnUp As Infragistics.Win.Misc.UltraButton

#End Region
End Class

