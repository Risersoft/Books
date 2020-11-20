Imports ug = Infragistics.Win.UltraWinGrid
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPackList
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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UltraGridItems = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel8 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnPaste = New Infragistics.Win.Misc.UltraButton()
        Me.btnCopy = New Infragistics.Win.Misc.UltraButton()
        Me.btnCopyFrom = New Infragistics.Win.Misc.UltraButton()
        Me.btnLeft = New Infragistics.Win.Misc.UltraButton()
        Me.btnRight = New Infragistics.Win.Misc.UltraButton()
        Me.btnRenumber = New Infragistics.Win.Misc.UltraButton()
        Me.btnDown = New Infragistics.Win.Misc.UltraButton()
        Me.btnUp = New Infragistics.Win.Misc.UltraButton()
        Me.btnDelItem = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddSubSerial = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddSerial = New Infragistics.Win.Misc.UltraButton()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.UltraGridItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel8.ClientArea.SuspendLayout()
        Me.UltraPanel8.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UltraGridItems)
        Me.Panel2.Controls.Add(Me.UltraPanel8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(901, 295)
        Me.Panel2.TabIndex = 9
        '
        'UltraGridItems
        '
        Me.UltraGridItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridItems.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.UltraGridItems.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridItems.Name = "UltraGridItems"
        Me.UltraGridItems.Size = New System.Drawing.Size(901, 264)
        Me.UltraGridItems.TabIndex = 55
        Me.UltraGridItems.Text = "UltraGrid1"
        '
        'UltraPanel8
        '
        '
        'UltraPanel8.ClientArea
        '
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnPaste)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnCopy)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnCopyFrom)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnLeft)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnRight)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnRenumber)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnDown)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnUp)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnDelItem)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnAddSubSerial)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnAddSerial)
        Me.UltraPanel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel8.Location = New System.Drawing.Point(0, 264)
        Me.UltraPanel8.Name = "UltraPanel8"
        Me.UltraPanel8.Size = New System.Drawing.Size(901, 31)
        Me.UltraPanel8.TabIndex = 54
        '
        'btnPaste
        '
        Appearance4.FontData.BoldAsString = "True"
        Me.btnPaste.Appearance = Appearance4
        Me.btnPaste.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnPaste.Location = New System.Drawing.Point(679, 0)
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Size = New System.Drawing.Size(74, 31)
        Me.btnPaste.TabIndex = 52
        Me.btnPaste.Text = "Paste"
        '
        'btnCopy
        '
        Appearance5.FontData.BoldAsString = "True"
        Me.btnCopy.Appearance = Appearance5
        Me.btnCopy.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCopy.Location = New System.Drawing.Point(753, 0)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(74, 31)
        Me.btnCopy.TabIndex = 51
        Me.btnCopy.Text = "Copy"
        '
        'btnCopyFrom
        '
        Appearance6.FontData.BoldAsString = "True"
        Me.btnCopyFrom.Appearance = Appearance6
        Me.btnCopyFrom.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCopyFrom.Location = New System.Drawing.Point(827, 0)
        Me.btnCopyFrom.Name = "btnCopyFrom"
        Me.btnCopyFrom.Size = New System.Drawing.Size(74, 31)
        Me.btnCopyFrom.TabIndex = 50
        Me.btnCopyFrom.Text = "Copy From"
        '
        'btnLeft
        '
        Me.btnLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnLeft.Location = New System.Drawing.Point(525, 0)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(75, 31)
        Me.btnLeft.TabIndex = 49
        Me.btnLeft.Text = "Make Main"
        '
        'btnRight
        '
        Me.btnRight.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnRight.Location = New System.Drawing.Point(450, 0)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(75, 31)
        Me.btnRight.TabIndex = 48
        Me.btnRight.Text = "Make Sub"
        '
        'btnRenumber
        '
        Me.btnRenumber.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnRenumber.Location = New System.Drawing.Point(375, 0)
        Me.btnRenumber.Name = "btnRenumber"
        Me.btnRenumber.Size = New System.Drawing.Size(75, 31)
        Me.btnRenumber.TabIndex = 47
        Me.btnRenumber.Text = "Re-No."
        '
        'btnDown
        '
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDown.Location = New System.Drawing.Point(300, 0)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(75, 31)
        Me.btnDown.TabIndex = 46
        Me.btnDown.Text = "Move Down"
        '
        'btnUp
        '
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnUp.Location = New System.Drawing.Point(225, 0)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(75, 31)
        Me.btnUp.TabIndex = 45
        Me.btnUp.Text = "Move Up"
        '
        'btnDelItem
        '
        Me.btnDelItem.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelItem.Location = New System.Drawing.Point(150, 0)
        Me.btnDelItem.Name = "btnDelItem"
        Me.btnDelItem.Size = New System.Drawing.Size(75, 31)
        Me.btnDelItem.TabIndex = 44
        Me.btnDelItem.Text = "&Delete"
        '
        'btnAddSubSerial
        '
        Me.btnAddSubSerial.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddSubSerial.Location = New System.Drawing.Point(75, 0)
        Me.btnAddSubSerial.Name = "btnAddSubSerial"
        Me.btnAddSubSerial.Size = New System.Drawing.Size(75, 31)
        Me.btnAddSubSerial.TabIndex = 43
        Me.btnAddSubSerial.Text = "&Add Sub Serial"
        '
        'btnAddSerial
        '
        Me.btnAddSerial.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddSerial.Location = New System.Drawing.Point(0, 0)
        Me.btnAddSerial.Name = "btnAddSerial"
        Me.btnAddSerial.Size = New System.Drawing.Size(75, 31)
        Me.btnAddSerial.TabIndex = 42
        Me.btnAddSerial.Text = "&Add Serial"
        '
        'frmPackList
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.Caption = "PACKING LIST & VEHICLE REQUIRMENT FORM"
        Me.ClientSize = New System.Drawing.Size(901, 295)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmPackList"
        Me.Text = "PACKING LIST & VEHICLE REQUIRMENT FORM"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.UltraGridItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel8.ClientArea.ResumeLayout(False)
        Me.UltraPanel8.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraGridItems As ug.UltraGrid
    Friend WithEvents UltraPanel8 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnLeft As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnRight As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnRenumber As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDown As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnUp As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDelItem As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddSubSerial As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddSerial As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCopyFrom As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnPaste As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCopy As Infragistics.Win.Misc.UltraButton

#End Region
End Class

