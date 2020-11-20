<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTourExpRepItem
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
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem2 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem3 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem4 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.txt_TimeTo = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_TimeFrom = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Remark = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmb_TravelMode = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Amount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_DateTo = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.dt_DateFrom = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txt_StationTo = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_StationFrom = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TimeTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TimeFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Remark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_TravelMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Amount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_DateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_DateFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_StationTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_StationFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txt_TimeTo
        '
        Me.txt_TimeTo.Location = New System.Drawing.Point(346, 64)
        Me.txt_TimeTo.Name = "txt_TimeTo"
        Me.txt_TimeTo.Size = New System.Drawing.Size(156, 22)
        Me.txt_TimeTo.TabIndex = 11
        '
        'txt_TimeFrom
        '
        Me.txt_TimeFrom.Location = New System.Drawing.Point(89, 64)
        Me.txt_TimeFrom.Name = "txt_TimeFrom"
        Me.txt_TimeFrom.Size = New System.Drawing.Size(156, 22)
        Me.txt_TimeFrom.TabIndex = 9
        '
        'UltraLabel8
        '
        Appearance1.FontData.SizeInPoints = 8.25!
        Appearance1.TextHAlignAsString = "Right"
        Appearance1.TextVAlignAsString = "Middle"
        Me.UltraLabel8.Appearance = Appearance1
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(297, 67)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(46, 14)
        Me.UltraLabel8.TabIndex = 10
        Me.UltraLabel8.Text = "Time To"
        '
        'UltraLabel3
        '
        Appearance2.FontData.SizeInPoints = 8.25!
        Appearance2.TextHAlignAsString = "Right"
        Appearance2.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance2
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(28, 68)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(59, 14)
        Me.UltraLabel3.TabIndex = 8
        Me.UltraLabel3.Text = "Time From"
        '
        'UltraLabel2
        '
        Appearance3.FontData.SizeInPoints = 8.25!
        Appearance3.TextHAlignAsString = "Right"
        Appearance3.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance3
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(42, 97)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(44, 14)
        Me.UltraLabel2.TabIndex = 16
        Me.UltraLabel2.Text = "Remark"
        '
        'txt_Remark
        '
        Me.txt_Remark.Location = New System.Drawing.Point(89, 93)
        Me.txt_Remark.Name = "txt_Remark"
        Me.txt_Remark.Size = New System.Drawing.Size(689, 22)
        Me.txt_Remark.TabIndex = 17
        '
        'cmb_TravelMode
        '
        ValueListItem1.DataValue = "BUS"
        ValueListItem2.DataValue = "CAR"
        ValueListItem3.DataValue = "Train"
        ValueListItem4.DataValue = "Plane"
        Me.cmb_TravelMode.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem1, ValueListItem2, ValueListItem3, ValueListItem4})
        Me.cmb_TravelMode.Location = New System.Drawing.Point(606, 36)
        Me.cmb_TravelMode.Name = "cmb_TravelMode"
        Me.cmb_TravelMode.Size = New System.Drawing.Size(172, 22)
        Me.cmb_TravelMode.TabIndex = 13
        '
        'UltraLabel9
        '
        Appearance4.FontData.SizeInPoints = 8.25!
        Appearance4.TextHAlignAsString = "Right"
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel9.Appearance = Appearance4
        Me.UltraLabel9.AutoSize = True
        Me.UltraLabel9.Location = New System.Drawing.Point(536, 40)
        Me.UltraLabel9.Name = "UltraLabel9"
        Me.UltraLabel9.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel9.TabIndex = 12
        Me.UltraLabel9.Text = "Travel Mode"
        '
        'UltraLabel7
        '
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance5
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(299, 40)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(44, 14)
        Me.UltraLabel7.TabIndex = 6
        Me.UltraLabel7.Text = "Date To"
        '
        'UltraLabel6
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance6
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(28, 40)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(58, 14)
        Me.UltraLabel6.TabIndex = 4
        Me.UltraLabel6.Text = "Date From"
        '
        'UltraLabel5
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance7
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(560, 67)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(43, 14)
        Me.UltraLabel5.TabIndex = 14
        Me.UltraLabel5.Text = "Amount"
        '
        'UltraLabel1
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance8
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(287, 12)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(56, 14)
        Me.UltraLabel1.TabIndex = 2
        Me.UltraLabel1.Text = "Station To"
        '
        'UltraLabel4
        '
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.TextHAlignAsString = "Right"
        Appearance9.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance9
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(17, 12)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(69, 14)
        Me.UltraLabel4.TabIndex = 0
        Me.UltraLabel4.Text = "Station From"
        '
        'txt_Amount
        '
        Me.txt_Amount.Location = New System.Drawing.Point(606, 63)
        Me.txt_Amount.Name = "txt_Amount"
        Me.txt_Amount.Size = New System.Drawing.Size(172, 22)
        Me.txt_Amount.TabIndex = 15
        '
        'dt_DateTo
        '
        Me.dt_DateTo.FormatString = "ddd dd MMM yyyy"
        Me.dt_DateTo.Location = New System.Drawing.Point(346, 36)
        Me.dt_DateTo.Name = "dt_DateTo"
        Me.dt_DateTo.NullText = "Not Defined"
        Me.dt_DateTo.Size = New System.Drawing.Size(156, 22)
        Me.dt_DateTo.TabIndex = 7
        '
        'dt_DateFrom
        '
        Me.dt_DateFrom.FormatString = "ddd dd MMM yyyy"
        Me.dt_DateFrom.Location = New System.Drawing.Point(89, 36)
        Me.dt_DateFrom.Name = "dt_DateFrom"
        Me.dt_DateFrom.NullText = "Not Defined"
        Me.dt_DateFrom.Size = New System.Drawing.Size(156, 22)
        Me.dt_DateFrom.TabIndex = 5
        '
        'txt_StationTo
        '
        Me.txt_StationTo.Location = New System.Drawing.Point(346, 8)
        Me.txt_StationTo.Name = "txt_StationTo"
        Me.txt_StationTo.Size = New System.Drawing.Size(156, 22)
        Me.txt_StationTo.TabIndex = 3
        '
        'txt_StationFrom
        '
        Me.txt_StationFrom.Location = New System.Drawing.Point(89, 8)
        Me.txt_StationFrom.Name = "txt_StationFrom"
        Me.txt_StationFrom.Size = New System.Drawing.Size(156, 22)
        Me.txt_StationFrom.TabIndex = 1
        '
        'frmTourExpRepItem
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Transaction"
        Me.ClientSize = New System.Drawing.Size(788, 124)
        Me.Controls.Add(Me.txt_TimeTo)
        Me.Controls.Add(Me.txt_TimeFrom)
        Me.Controls.Add(Me.txt_StationTo)
        Me.Controls.Add(Me.UltraLabel8)
        Me.Controls.Add(Me.txt_StationFrom)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.dt_DateFrom)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.dt_DateTo)
        Me.Controls.Add(Me.txt_Remark)
        Me.Controls.Add(Me.txt_Amount)
        Me.Controls.Add(Me.cmb_TravelMode)
        Me.Controls.Add(Me.UltraLabel4)
        Me.Controls.Add(Me.UltraLabel9)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Controls.Add(Me.UltraLabel7)
        Me.Controls.Add(Me.UltraLabel5)
        Me.Controls.Add(Me.UltraLabel6)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Name = "frmTourExpRepItem"
        Me.Text = "Transaction"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TimeTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TimeFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Remark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_TravelMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Amount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_DateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_DateFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_StationTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_StationFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_StationTo As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_StationFrom As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dt_DateTo As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents dt_DateFrom As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents txt_Amount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_TravelMode As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Remark As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_TimeTo As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_TimeFrom As Infragistics.Win.UltraWinEditors.UltraTextEditor

#End Region
End Class

