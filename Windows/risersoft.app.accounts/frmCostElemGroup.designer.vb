Imports ug = Infragistics.Win.UltraWinGrid
Imports we = Infragistics.Win.UltraWinEditors
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class frmCostElemGroup
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmb_pCostElemGroupID As Infragistics.Win.UltraWinGrid.UltraCombo
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblSortNumber = New System.Windows.Forms.Label()
        Me.txt_SortNumber = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_GroupType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_GroupName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmb_pCostElemGroupID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGridItemList = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnEdit = New Infragistics.Win.Misc.UltraButton()
        Me.btnDown = New Infragistics.Win.Misc.UltraButton()
        Me.btnUp = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.txt_SortNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_GroupType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_GroupName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_pCostElemGroupID, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel4.Location = New System.Drawing.Point(0, 374)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(570, 37)
        Me.Panel4.TabIndex = 0
        '
        'btnSave
        '
        Appearance7.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance7
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(306, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 37)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance8.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance8
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(394, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 37)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance9.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance9
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(482, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 37)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblSortNumber)
        Me.Panel1.Controls.Add(Me.txt_SortNumber)
        Me.Panel1.Controls.Add(Me.cmb_GroupType)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txt_GroupName)
        Me.Panel1.Controls.Add(Me.UltraLabel6)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmb_pCostElemGroupID)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(570, 109)
        Me.Panel1.TabIndex = 0
        '
        'lblSortNumber
        '
        Me.lblSortNumber.AutoSize = True
        Me.lblSortNumber.Location = New System.Drawing.Point(338, 81)
        Me.lblSortNumber.Name = "lblSortNumber"
        Me.lblSortNumber.Size = New System.Drawing.Size(67, 14)
        Me.lblSortNumber.TabIndex = 16
        Me.lblSortNumber.Text = "Sort Number"
        Me.lblSortNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSortNumber.Visible = False
        '
        'txt_SortNumber
        '
        Me.txt_SortNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_SortNumber.Location = New System.Drawing.Point(408, 78)
        Me.txt_SortNumber.Name = "txt_SortNumber"
        Me.txt_SortNumber.Size = New System.Drawing.Size(129, 21)
        Me.txt_SortNumber.TabIndex = 15
        Me.txt_SortNumber.Text = "ULTRATEXTEDITOR1"
        Me.txt_SortNumber.Visible = False
        '
        'cmb_GroupType
        '
        Appearance4.FontData.BoldAsString = "False"
        Me.cmb_GroupType.Appearance = Appearance4
        Me.cmb_GroupType.Location = New System.Drawing.Point(113, 77)
        Me.cmb_GroupType.Name = "cmb_GroupType"
        Me.cmb_GroupType.Size = New System.Drawing.Size(169, 22)
        Me.cmb_GroupType.TabIndex = 14
        Me.cmb_GroupType.Text = "UltraCombo5"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(47, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Group Type"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_GroupName
        '
        Me.txt_GroupName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txt_GroupName.Location = New System.Drawing.Point(113, 15)
        Me.txt_GroupName.Name = "txt_GroupName"
        Me.txt_GroupName.Size = New System.Drawing.Size(424, 21)
        Me.txt_GroupName.TabIndex = 1
        Me.txt_GroupName.Text = "ULTRATEXTEDITOR1"
        '
        'UltraLabel6
        '
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.TextHAlignAsString = "Right"
        Appearance10.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance10
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(41, 18)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(69, 14)
        Me.UltraLabel6.TabIndex = 0
        Me.UltraLabel6.Text = "Group Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 14)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Parent Group"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_pCostElemGroupID
        '
        Appearance6.FontData.BoldAsString = "False"
        Me.cmb_pCostElemGroupID.Appearance = Appearance6
        Me.cmb_pCostElemGroupID.Location = New System.Drawing.Point(113, 46)
        Me.cmb_pCostElemGroupID.Name = "cmb_pCostElemGroupID"
        Me.cmb_pCostElemGroupID.Size = New System.Drawing.Size(424, 22)
        Me.cmb_pCostElemGroupID.TabIndex = 13
        Me.cmb_pCostElemGroupID.Text = "UltraCombo5"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGridItemList)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 109)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(570, 228)
        Me.Panel2.TabIndex = 1
        '
        'UltraGridItemList
        '
        Me.UltraGridItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridItemList.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridItemList.Name = "UltraGridItemList"
        Me.UltraGridItemList.Size = New System.Drawing.Size(570, 228)
        Me.UltraGridItemList.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnEdit)
        Me.Panel3.Controls.Add(Me.btnDown)
        Me.Panel3.Controls.Add(Me.btnUp)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 337)
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
        'frmCostElemGroup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "Cost Element Group"
        Me.ClientSize = New System.Drawing.Size(570, 411)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCostElemGroup"
        Me.Text = "Cost Element Group"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txt_SortNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_GroupType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_GroupName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_pCostElemGroupID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGridItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_GroupName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As Windows.Forms.Panel
    Friend WithEvents UltraGridItemList As ug.UltraGrid
    Friend WithEvents cmb_GroupType As ug.UltraCombo
    Friend WithEvents Panel3 As Windows.Forms.Panel
    Friend WithEvents btnDown As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnUp As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnEdit As Infragistics.Win.Misc.UltraButton
    Friend WithEvents lblSortNumber As Windows.Forms.Label
    Friend WithEvents txt_SortNumber As we.UltraTextEditor

#End Region
End Class

