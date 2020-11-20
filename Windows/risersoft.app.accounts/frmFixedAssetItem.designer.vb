<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmFixedAssetItem
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
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance102 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.dt_Dated = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.Panel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_NewFixedAssetID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_AssetType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmb_AssetClass = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txt_AssetName = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_EntryType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.ClientArea.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmb_NewFixedAssetID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_AssetType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_AssetClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_AssetName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_EntryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dt_Dated
        '
        Me.dt_Dated.FormatString = "ddd dd MMM yyyy"
        Me.dt_Dated.Location = New System.Drawing.Point(92, 108)
        Me.dt_Dated.Name = "dt_Dated"
        Me.dt_Dated.NullText = "Not Defined"
        Me.dt_Dated.Size = New System.Drawing.Size(159, 22)
        Me.dt_Dated.TabIndex = 7
        '
        'UltraLabel1
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance6
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(55, 112)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(34, 14)
        Me.UltraLabel1.TabIndex = 6
        Me.UltraLabel1.Text = "Dated"
        '
        'Panel1
        '
        '
        'Panel1.ClientArea
        '
        Me.Panel1.ClientArea.Controls.Add(Me.btnSave)
        Me.Panel1.ClientArea.Controls.Add(Me.btnCancel)
        Me.Panel1.ClientArea.Controls.Add(Me.btnOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 194)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(589, 36)
        Me.Panel1.TabIndex = 12
        '
        'btnSave
        '
        Appearance7.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance7
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(385, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 36)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnCancel
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance3
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(453, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 36)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Appearance13.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance13
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(521, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 36)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'cmb_NewFixedAssetID
        '
        Me.cmb_NewFixedAssetID.Location = New System.Drawing.Point(92, 148)
        Me.cmb_NewFixedAssetID.Name = "cmb_NewFixedAssetID"
        Me.cmb_NewFixedAssetID.Size = New System.Drawing.Size(481, 23)
        Me.cmb_NewFixedAssetID.TabIndex = 11
        '
        'UltraLabel5
        '
        Appearance102.FontData.SizeInPoints = 8.25!
        Appearance102.TextHAlignAsString = "Right"
        Appearance102.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance102
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(26, 152)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(63, 14)
        Me.UltraLabel5.TabIndex = 10
        Me.UltraLabel5.Text = "Fixed Asset"
        '
        'UltraLabel6
        '
        Appearance1.FontData.SizeInPoints = 8.25!
        Appearance1.TextHAlignAsString = "Right"
        Appearance1.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance1
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(308, 71)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(64, 14)
        Me.UltraLabel6.TabIndex = 4
        Me.UltraLabel6.Text = "Asset Class"
        '
        'UltraLabel7
        '
        Appearance14.FontData.SizeInPoints = 8.25!
        Appearance14.TextHAlignAsString = "Right"
        Appearance14.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance14
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(28, 71)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(61, 14)
        Me.UltraLabel7.TabIndex = 2
        Me.UltraLabel7.Text = "Asset Type"
        '
        'cmb_AssetType
        '
        Me.cmb_AssetType.Location = New System.Drawing.Point(92, 67)
        Me.cmb_AssetType.Name = "cmb_AssetType"
        Me.cmb_AssetType.ReadOnly = True
        Me.cmb_AssetType.Size = New System.Drawing.Size(159, 23)
        Me.cmb_AssetType.TabIndex = 3
        '
        'cmb_AssetClass
        '
        Me.cmb_AssetClass.Location = New System.Drawing.Point(375, 67)
        Me.cmb_AssetClass.Name = "cmb_AssetClass"
        Me.cmb_AssetClass.ReadOnly = True
        Me.cmb_AssetClass.Size = New System.Drawing.Size(198, 23)
        Me.cmb_AssetClass.TabIndex = 5
        '
        'txt_AssetName
        '
        Me.txt_AssetName.Location = New System.Drawing.Point(92, 27)
        Me.txt_AssetName.Name = "txt_AssetName"
        Me.txt_AssetName.ReadOnly = True
        Me.txt_AssetName.Size = New System.Drawing.Size(481, 22)
        Me.txt_AssetName.TabIndex = 1
        '
        'UltraLabel8
        '
        Appearance15.FontData.SizeInPoints = 8.25!
        Appearance15.TextHAlignAsString = "Right"
        Appearance15.TextVAlignAsString = "Middle"
        Me.UltraLabel8.Appearance = Appearance15
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(23, 31)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(66, 14)
        Me.UltraLabel8.TabIndex = 0
        Me.UltraLabel8.Text = "Asset Name"
        '
        'cmb_EntryType
        '
        Me.cmb_EntryType.Location = New System.Drawing.Point(375, 107)
        Me.cmb_EntryType.Name = "cmb_EntryType"
        Me.cmb_EntryType.ReadOnly = True
        Me.cmb_EntryType.Size = New System.Drawing.Size(198, 23)
        Me.cmb_EntryType.TabIndex = 9
        '
        'UltraLabel2
        '
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.TextHAlignAsString = "Right"
        Appearance11.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance11
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(313, 111)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(59, 14)
        Me.UltraLabel2.TabIndex = 8
        Me.UltraLabel2.Text = "Entry Type"
        '
        'frmFixedAssetItem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Transfer"
        Me.ClientSize = New System.Drawing.Size(589, 230)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.cmb_EntryType)
        Me.Controls.Add(Me.UltraLabel6)
        Me.Controls.Add(Me.UltraLabel7)
        Me.Controls.Add(Me.cmb_AssetType)
        Me.Controls.Add(Me.cmb_AssetClass)
        Me.Controls.Add(Me.txt_AssetName)
        Me.Controls.Add(Me.UltraLabel8)
        Me.Controls.Add(Me.UltraLabel5)
        Me.Controls.Add(Me.cmb_NewFixedAssetID)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dt_Dated)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFixedAssetItem"
        Me.Text = "Transfer"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ClientArea.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.cmb_NewFixedAssetID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_AssetType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_AssetClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_AssetName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_EntryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dt_Dated As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents Panel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmb_NewFixedAssetID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_AssetType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmb_AssetClass As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txt_AssetName As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_EntryType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel

#End Region
End Class

