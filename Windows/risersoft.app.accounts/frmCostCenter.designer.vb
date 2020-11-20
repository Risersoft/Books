Imports ug = Infragistics.Win.UltraWinGrid
Imports we = Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmCostCenter
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
    Friend WithEvents cmb_pCostCenterID As Infragistics.Win.UltraWinGrid.UltraCombo
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_CostCenterCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblSortNumber = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_DepID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_CampusID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.dt_EDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_CompanyID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_SortNumber = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_CostCenterName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_SDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.cmb_pCostCenterID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGridItemList = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton()
        Me.btnDown = New Infragistics.Win.Misc.UltraButton()
        Me.btnUp = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.txt_CostCenterCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_DepID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CampusID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_EDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_SortNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_CostCenterName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_SDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_pCostCenterID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnOK)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 432)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(570, 37)
        Me.Panel4.TabIndex = 0
        '
        'btnSave
        '
        Appearance1.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance1
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(306, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 37)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance2.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance2
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(394, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 37)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance3
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(482, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 37)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraLabel8)
        Me.Panel1.Controls.Add(Me.txt_CostCenterCode)
        Me.Panel1.Controls.Add(Me.lblSortNumber)
        Me.Panel1.Controls.Add(Me.UltraLabel5)
        Me.Panel1.Controls.Add(Me.UltraLabel4)
        Me.Panel1.Controls.Add(Me.cmb_DepID)
        Me.Panel1.Controls.Add(Me.UltraLabel1)
        Me.Panel1.Controls.Add(Me.UltraLabel3)
        Me.Panel1.Controls.Add(Me.cmb_CampusID)
        Me.Panel1.Controls.Add(Me.dt_EDate)
        Me.Panel1.Controls.Add(Me.UltraLabel7)
        Me.Panel1.Controls.Add(Me.cmb_CompanyID)
        Me.Panel1.Controls.Add(Me.txt_SortNumber)
        Me.Panel1.Controls.Add(Me.txt_CostCenterName)
        Me.Panel1.Controls.Add(Me.UltraLabel2)
        Me.Panel1.Controls.Add(Me.UltraLabel6)
        Me.Panel1.Controls.Add(Me.dt_SDate)
        Me.Panel1.Controls.Add(Me.cmb_pCostCenterID)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(570, 146)
        Me.Panel1.TabIndex = 0
        '
        'UltraLabel8
        '
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel8.Appearance = Appearance4
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(389, 41)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(31, 14)
        Me.UltraLabel8.TabIndex = 30
        Me.UltraLabel8.Text = "Code"
        '
        'txt_CostCenterCode
        '
        Me.txt_CostCenterCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_CostCenterCode.Location = New System.Drawing.Point(423, 37)
        Me.txt_CostCenterCode.Name = "txt_CostCenterCode"
        Me.txt_CostCenterCode.Size = New System.Drawing.Size(135, 21)
        Me.txt_CostCenterCode.TabIndex = 29
        Me.txt_CostCenterCode.Text = "ULTRATEXTEDITOR1"
        '
        'lblSortNumber
        '
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.lblSortNumber.Appearance = Appearance5
        Me.lblSortNumber.AutoSize = True
        Me.lblSortNumber.Location = New System.Drawing.Point(375, 68)
        Me.lblSortNumber.Name = "lblSortNumber"
        Me.lblSortNumber.Size = New System.Drawing.Size(46, 14)
        Me.lblSortNumber.TabIndex = 28
        Me.lblSortNumber.Text = "Sort No."
        Me.lblSortNumber.Visible = False
        '
        'UltraLabel5
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance6
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(16, 39)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(101, 14)
        Me.UltraLabel5.TabIndex = 27
        Me.UltraLabel5.Text = "Parent Cost Center"
        '
        'UltraLabel4
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance7
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(54, 123)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel4.TabIndex = 26
        Me.UltraLabel4.Text = "Department"
        '
        'cmb_DepID
        '
        Me.cmb_DepID.Location = New System.Drawing.Point(120, 119)
        Me.cmb_DepID.Name = "cmb_DepID"
        Me.cmb_DepID.Size = New System.Drawing.Size(243, 22)
        Me.cmb_DepID.TabIndex = 25
        '
        'UltraLabel1
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance8
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(370, 123)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(51, 14)
        Me.UltraLabel1.TabIndex = 19
        Me.UltraLabel1.Text = "End Date"
        '
        'UltraLabel3
        '
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.TextHAlignAsString = "Right"
        Appearance9.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance9
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(71, 94)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(46, 14)
        Me.UltraLabel3.TabIndex = 23
        Me.UltraLabel3.Text = "Campus"
        '
        'cmb_CampusID
        '
        Me.cmb_CampusID.Location = New System.Drawing.Point(120, 91)
        Me.cmb_CampusID.Name = "cmb_CampusID"
        Me.cmb_CampusID.Size = New System.Drawing.Size(243, 22)
        Me.cmb_CampusID.TabIndex = 24
        '
        'dt_EDate
        '
        Me.dt_EDate.FormatString = "ddd dd MMM yyyy"
        Me.dt_EDate.Location = New System.Drawing.Point(424, 120)
        Me.dt_EDate.Name = "dt_EDate"
        Me.dt_EDate.NullText = "Not Defined"
        Me.dt_EDate.Size = New System.Drawing.Size(135, 21)
        Me.dt_EDate.TabIndex = 20
        '
        'UltraLabel7
        '
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.TextHAlignAsString = "Right"
        Appearance10.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance10
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(64, 69)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(53, 14)
        Me.UltraLabel7.TabIndex = 21
        Me.UltraLabel7.Text = "Company"
        '
        'cmb_CompanyID
        '
        Me.cmb_CompanyID.Location = New System.Drawing.Point(120, 64)
        Me.cmb_CompanyID.Name = "cmb_CompanyID"
        Me.cmb_CompanyID.Size = New System.Drawing.Size(243, 22)
        Me.cmb_CompanyID.TabIndex = 22
        '
        'txt_SortNumber
        '
        Me.txt_SortNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_SortNumber.Location = New System.Drawing.Point(424, 65)
        Me.txt_SortNumber.Name = "txt_SortNumber"
        Me.txt_SortNumber.Size = New System.Drawing.Size(135, 21)
        Me.txt_SortNumber.TabIndex = 15
        Me.txt_SortNumber.Text = "ULTRATEXTEDITOR1"
        Me.txt_SortNumber.Visible = False
        '
        'txt_CostCenterName
        '
        Me.txt_CostCenterName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_CostCenterName.Location = New System.Drawing.Point(120, 9)
        Me.txt_CostCenterName.Name = "txt_CostCenterName"
        Me.txt_CostCenterName.Size = New System.Drawing.Size(439, 21)
        Me.txt_CostCenterName.TabIndex = 1
        Me.txt_CostCenterName.Text = "ULTRATEXTEDITOR1"
        '
        'UltraLabel2
        '
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.TextHAlignAsString = "Right"
        Appearance11.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance11
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(366, 95)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(55, 14)
        Me.UltraLabel2.TabIndex = 17
        Me.UltraLabel2.Text = "Start Date"
        '
        'UltraLabel6
        '
        Appearance12.FontData.SizeInPoints = 8.25!
        Appearance12.TextHAlignAsString = "Right"
        Appearance12.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance12
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(19, 12)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(98, 14)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Cost Center Name"
        '
        'dt_SDate
        '
        Me.dt_SDate.FormatString = "ddd dd MMM yyyy"
        Me.dt_SDate.Location = New System.Drawing.Point(424, 92)
        Me.dt_SDate.Name = "dt_SDate"
        Me.dt_SDate.NullText = "Not Defined"
        Me.dt_SDate.Size = New System.Drawing.Size(135, 21)
        Me.dt_SDate.TabIndex = 18
        '
        'cmb_pCostCenterID
        '
        Appearance13.FontData.BoldAsString = "False"
        Me.cmb_pCostCenterID.Appearance = Appearance13
        Me.cmb_pCostCenterID.Location = New System.Drawing.Point(120, 36)
        Me.cmb_pCostCenterID.Name = "cmb_pCostCenterID"
        Me.cmb_pCostCenterID.Size = New System.Drawing.Size(243, 22)
        Me.cmb_pCostCenterID.TabIndex = 13
        Me.cmb_pCostCenterID.Text = "UltraCombo5"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGridItemList)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 146)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(570, 249)
        Me.Panel2.TabIndex = 1
        '
        'UltraGridItemList
        '
        Me.UltraGridItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridItemList.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridItemList.Name = "UltraGridItemList"
        Me.UltraGridItemList.Size = New System.Drawing.Size(570, 249)
        Me.UltraGridItemList.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnEdit)
        Me.Panel3.Controls.Add(Me.btnDown)
        Me.Panel3.Controls.Add(Me.btnUp)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 395)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(570, 37)
        Me.Panel3.TabIndex = 2
        '
        'btnEdit
        '
        Me.btnEdit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnEdit.Location = New System.Drawing.Point(500, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 37)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "Edit"
        '
        'btnDown
        '
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDown.Location = New System.Drawing.Point(84, 0)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(84, 37)
        Me.btnDown.TabIndex = 7
        Me.btnDown.Text = "Move Down"
        '
        'btnUp
        '
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnUp.Location = New System.Drawing.Point(0, 0)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(84, 37)
        Me.btnUp.TabIndex = 6
        Me.btnUp.Text = "Move Up"
        '
        'frmCostCenter
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Cost Center"
        Me.ClientSize = New System.Drawing.Size(570, 469)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCostCenter"
        Me.Text = "Cost Center"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txt_CostCenterCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_DepID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CampusID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_EDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CompanyID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_SortNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_CostCenterName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_SDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_pCostCenterID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CostCenterName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents UltraGridItemList As ug.UltraGrid
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents btnDown As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnUp As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton
    Friend WithEvents txt_SortNumber As we.UltraTextEditor
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_EDate As we.UltraDateTimeEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_SDate As we.UltraDateTimeEditor
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_DepID As ug.UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_CampusID As ug.UltraCombo
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_CompanyID As ug.UltraCombo
    Friend WithEvents lblSortNumber As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_CostCenterCode As we.UltraTextEditor

#End Region
End Class

