Imports ug = Infragistics.Win.UltraWinGrid
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmAccVouch
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
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.ChkGetMissingNo = New Infragistics.Win.UltraWinEditors.UltraCheckEditor()
        Me.PanelClVouch = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_AdjustType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnGenClEntry = New Infragistics.Win.Misc.UltraButton()
        Me.Panel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_CompanyID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtCrAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtDrAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Narration = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_AccVouchType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_VouchDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_VoucherNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UEGB_ItemList = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel1 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraGridItemList = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDel = New Infragistics.Win.Misc.UltraButton()
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton()
        Me.UEGB_ItemDetail = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel2 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.ClientArea.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.ChkGetMissingNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelClVouch.ClientArea.SuspendLayout()
        Me.PanelClVouch.SuspendLayout()
        CType(Me.cmb_AdjustType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.ClientArea.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCrAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDrAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Narration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_AccVouchType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_VouchDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_VoucherNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UEGB_ItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_ItemList.SuspendLayout()
        Me.UltraExpandableGroupBoxPanel1.SuspendLayout()
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.ClientArea.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_ItemDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Appearance1.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance1
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(854, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 40)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Appearance2.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance2
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(786, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 40)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance3
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(718, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 40)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel1
        '
        '
        'Panel1.ClientArea
        '
        Me.Panel1.ClientArea.Controls.Add(Me.ChkGetMissingNo)
        Me.Panel1.ClientArea.Controls.Add(Me.PanelClVouch)
        Me.Panel1.ClientArea.Controls.Add(Me.btnSave)
        Me.Panel1.ClientArea.Controls.Add(Me.btnCancel)
        Me.Panel1.ClientArea.Controls.Add(Me.btnOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 467)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(922, 40)
        Me.Panel1.TabIndex = 3
        '
        'ChkGetMissingNo
        '
        Me.ChkGetMissingNo.Dock = System.Windows.Forms.DockStyle.Right
        Me.ChkGetMissingNo.Location = New System.Drawing.Point(582, 0)
        Me.ChkGetMissingNo.Name = "ChkGetMissingNo"
        Me.ChkGetMissingNo.Size = New System.Drawing.Size(136, 40)
        Me.ChkGetMissingNo.TabIndex = 14
        Me.ChkGetMissingNo.Text = "Missed Voucher No"
        '
        'PanelClVouch
        '
        '
        'PanelClVouch.ClientArea
        '
        Me.PanelClVouch.ClientArea.Controls.Add(Me.UltraLabel8)
        Me.PanelClVouch.ClientArea.Controls.Add(Me.cmb_AdjustType)
        Me.PanelClVouch.ClientArea.Controls.Add(Me.btnGenClEntry)
        Me.PanelClVouch.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelClVouch.Location = New System.Drawing.Point(0, 0)
        Me.PanelClVouch.Name = "PanelClVouch"
        Me.PanelClVouch.Size = New System.Drawing.Size(457, 40)
        Me.PanelClVouch.TabIndex = 4
        '
        'UltraLabel8
        '
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel8.Appearance = Appearance4
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(12, 13)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(64, 14)
        Me.UltraLabel8.TabIndex = 4
        Me.UltraLabel8.Text = "Adjust Type"
        '
        'cmb_AdjustType
        '
        Me.cmb_AdjustType.Location = New System.Drawing.Point(79, 9)
        Me.cmb_AdjustType.Name = "cmb_AdjustType"
        Me.cmb_AdjustType.Size = New System.Drawing.Size(252, 23)
        Me.cmb_AdjustType.TabIndex = 5
        '
        'btnGenClEntry
        '
        Appearance5.FontData.BoldAsString = "True"
        Me.btnGenClEntry.Appearance = Appearance5
        Me.btnGenClEntry.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnGenClEntry.Location = New System.Drawing.Point(349, 0)
        Me.btnGenClEntry.Name = "btnGenClEntry"
        Me.btnGenClEntry.Size = New System.Drawing.Size(108, 40)
        Me.btnGenClEntry.TabIndex = 3
        Me.btnGenClEntry.Text = "Generate Closing Entry"
        '
        'Panel2
        '
        '
        'Panel2.ClientArea
        '
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel7)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_CompanyID)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel5)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel4)
        Me.Panel2.ClientArea.Controls.Add(Me.txtCrAmount)
        Me.Panel2.ClientArea.Controls.Add(Me.txtDrAmount)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel3)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_Narration)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel6)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_AccVouchType)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.Panel2.ClientArea.Controls.Add(Me.dt_VouchDate)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_VoucherNum)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(922, 101)
        Me.Panel2.TabIndex = 0
        '
        'UltraLabel7
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance6
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(81, 16)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel7.TabIndex = 0
        Me.UltraLabel7.Text = "Company"
        '
        'cmb_CompanyID
        '
        Me.cmb_CompanyID.Location = New System.Drawing.Point(137, 11)
        Me.cmb_CompanyID.Name = "cmb_CompanyID"
        Me.cmb_CompanyID.Size = New System.Drawing.Size(252, 23)
        Me.cmb_CompanyID.TabIndex = 1
        '
        'UltraLabel5
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance7
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(718, 79)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(58, 14)
        Me.UltraLabel5.TabIndex = 12
        Me.UltraLabel5.Text = "Cr Amount"
        '
        'UltraLabel4
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance8
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(718, 49)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(58, 14)
        Me.UltraLabel4.TabIndex = 8
        Me.UltraLabel4.Text = "Dr Amount"
        '
        'txtCrAmount
        '
        Me.txtCrAmount.Location = New System.Drawing.Point(779, 75)
        Me.txtCrAmount.Name = "txtCrAmount"
        Me.txtCrAmount.ReadOnly = True
        Me.txtCrAmount.Size = New System.Drawing.Size(130, 22)
        Me.txtCrAmount.TabIndex = 13
        '
        'txtDrAmount
        '
        Me.txtDrAmount.Location = New System.Drawing.Point(779, 45)
        Me.txtDrAmount.Name = "txtDrAmount"
        Me.txtDrAmount.ReadOnly = True
        Me.txtDrAmount.Size = New System.Drawing.Size(130, 22)
        Me.txtDrAmount.TabIndex = 9
        '
        'UltraLabel3
        '
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.TextHAlignAsString = "Right"
        Appearance9.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance9
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(83, 70)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(51, 14)
        Me.UltraLabel3.TabIndex = 10
        Me.UltraLabel3.Text = "Narration"
        '
        'txt_Narration
        '
        Me.txt_Narration.Location = New System.Drawing.Point(137, 67)
        Me.txt_Narration.Multiline = True
        Me.txt_Narration.Name = "txt_Narration"
        Me.txt_Narration.Size = New System.Drawing.Size(566, 29)
        Me.txt_Narration.TabIndex = 11
        Me.txt_Narration.Text = "UltraTextEditor1"
        '
        'UltraLabel6
        '
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.TextHAlignAsString = "Right"
        Appearance10.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance10
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(59, 44)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(75, 14)
        Me.UltraLabel6.TabIndex = 4
        Me.UltraLabel6.Text = "Voucher Type"
        '
        'cmb_AccVouchType
        '
        Me.cmb_AccVouchType.Location = New System.Drawing.Point(137, 39)
        Me.cmb_AccVouchType.Name = "cmb_AccVouchType"
        Me.cmb_AccVouchType.ReadOnly = True
        Me.cmb_AccVouchType.Size = New System.Drawing.Size(252, 23)
        Me.cmb_AccVouchType.TabIndex = 5
        '
        'UltraLabel2
        '
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.TextHAlignAsString = "Right"
        Appearance11.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance11
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(497, 15)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(73, 14)
        Me.UltraLabel2.TabIndex = 2
        Me.UltraLabel2.Text = "Voucher Date"
        '
        'UltraLabel1
        '
        Appearance12.FontData.SizeInPoints = 8.25!
        Appearance12.TextHAlignAsString = "Right"
        Appearance12.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance12
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(503, 41)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel1.TabIndex = 6
        Me.UltraLabel1.Text = "Voucher No."
        '
        'dt_VouchDate
        '
        Me.dt_VouchDate.FormatString = "ddd dd MMM yyyy"
        Me.dt_VouchDate.Location = New System.Drawing.Point(573, 11)
        Me.dt_VouchDate.Name = "dt_VouchDate"
        Me.dt_VouchDate.NullText = "Not Defined"
        Me.dt_VouchDate.Size = New System.Drawing.Size(130, 22)
        Me.dt_VouchDate.TabIndex = 3
        '
        'txt_VoucherNum
        '
        Me.txt_VoucherNum.Location = New System.Drawing.Point(573, 37)
        Me.txt_VoucherNum.Name = "txt_VoucherNum"
        Me.txt_VoucherNum.ReadOnly = True
        Me.txt_VoucherNum.Size = New System.Drawing.Size(130, 22)
        Me.txt_VoucherNum.TabIndex = 7
        Me.txt_VoucherNum.Text = "UltraTextEditor1"
        '
        'UEGB_ItemList
        '
        Me.UEGB_ItemList.Controls.Add(Me.UltraExpandableGroupBoxPanel1)
        Me.UEGB_ItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UEGB_ItemList.ExpandedSize = New System.Drawing.Size(922, 174)
        Me.UEGB_ItemList.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_ItemList.Location = New System.Drawing.Point(0, 101)
        Me.UEGB_ItemList.Name = "UEGB_ItemList"
        Me.UEGB_ItemList.Size = New System.Drawing.Size(922, 174)
        Me.UEGB_ItemList.TabIndex = 1
        Me.UEGB_ItemList.Text = "Item List"
        '
        'UltraExpandableGroupBoxPanel1
        '
        Me.UltraExpandableGroupBoxPanel1.Controls.Add(Me.UltraGridItemList)
        Me.UltraExpandableGroupBoxPanel1.Controls.Add(Me.Panel4)
        Me.UltraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel1.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel1.Name = "UltraExpandableGroupBoxPanel1"
        Me.UltraExpandableGroupBoxPanel1.Size = New System.Drawing.Size(916, 152)
        Me.UltraExpandableGroupBoxPanel1.TabIndex = 0
        '
        'UltraGridItemList
        '
        Me.UltraGridItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridItemList.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridItemList.Name = "UltraGridItemList"
        Me.UltraGridItemList.Size = New System.Drawing.Size(916, 127)
        Me.UltraGridItemList.TabIndex = 0
        '
        'Panel4
        '
        '
        'Panel4.ClientArea
        '
        Me.Panel4.ClientArea.Controls.Add(Me.btnDel)
        Me.Panel4.ClientArea.Controls.Add(Me.btnAdd)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 127)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(916, 25)
        Me.Panel4.TabIndex = 52
        '
        'btnDel
        '
        Me.btnDel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDel.Location = New System.Drawing.Point(70, 0)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(70, 25)
        Me.btnDel.TabIndex = 1
        Me.btnDel.Text = "Delete"
        '
        'btnAdd
        '
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAdd.Location = New System.Drawing.Point(0, 0)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 25)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add New"
        '
        'UEGB_ItemDetail
        '
        Me.UEGB_ItemDetail.Controls.Add(Me.UltraExpandableGroupBoxPanel2)
        Me.UEGB_ItemDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UEGB_ItemDetail.ExpandedSize = New System.Drawing.Size(922, 192)
        Me.UEGB_ItemDetail.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_ItemDetail.Location = New System.Drawing.Point(0, 275)
        Me.UEGB_ItemDetail.Name = "UEGB_ItemDetail"
        Me.UEGB_ItemDetail.Size = New System.Drawing.Size(922, 192)
        Me.UEGB_ItemDetail.TabIndex = 2
        Me.UEGB_ItemDetail.Text = "Item Details"
        '
        'UltraExpandableGroupBoxPanel2
        '
        Me.UltraExpandableGroupBoxPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel2.Location = New System.Drawing.Point(3, 19)
        Me.UltraExpandableGroupBoxPanel2.Name = "UltraExpandableGroupBoxPanel2"
        Me.UltraExpandableGroupBoxPanel2.Size = New System.Drawing.Size(916, 170)
        Me.UltraExpandableGroupBoxPanel2.TabIndex = 0
        '
        'frmAccVouch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Accounting Voucher"
        Me.ClientSize = New System.Drawing.Size(922, 507)
        Me.Controls.Add(Me.UEGB_ItemList)
        Me.Controls.Add(Me.UEGB_ItemDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Name = "frmAccVouch"
        Me.Text = "Accounting Voucher"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ClientArea.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.ChkGetMissingNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelClVouch.ClientArea.ResumeLayout(False)
        Me.PanelClVouch.ClientArea.PerformLayout()
        Me.PanelClVouch.ResumeLayout(False)
        CType(Me.cmb_AdjustType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ClientArea.ResumeLayout(False)
        Me.Panel2.ClientArea.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCrAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDrAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Narration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_AccVouchType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_VouchDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_VoucherNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UEGB_ItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_ItemList.ResumeLayout(False)
        Me.UltraExpandableGroupBoxPanel1.ResumeLayout(False)
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ClientArea.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_ItemDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents Panel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UEGB_ItemList As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel1 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UEGB_ItemDetail As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel2 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraGridItemList As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Panel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_VouchDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_VoucherNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtCrAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txtDrAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Narration As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_CompanyID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_AccVouchType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnGenClEntry As Infragistics.Win.Misc.UltraButton
    Friend WithEvents PanelClVouch As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_AdjustType As ug.UltraCombo
    Friend WithEvents ChkGetMissingNo As Infragistics.Win.UltraWinEditors.UltraCheckEditor

#End Region
End Class

