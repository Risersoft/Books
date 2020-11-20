Imports ug = Infragistics.Win.UltraWinGrid
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTourExpRep
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
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab9 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab7 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab8 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab10 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraPanel10 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraGridJD = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Panel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelJD = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddJD = New Infragistics.Win.Misc.UltraButton()
        Me.UEGB_ItemDetail = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel2 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridDD = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelDD = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddDD = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridHE = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelHE = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddHE = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridLC = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel3 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelLC = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddLC = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridMES = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel4 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelMES = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddMES = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl9 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraPanel9 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraGridMEM = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel8 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelMEM = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddMEM = New Infragistics.Win.Misc.UltraButton()
        Me.UEGB_Mat = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel1 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.UltraTabPageControl7 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridMEA = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel6 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelMEA = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddMEA = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl8 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraPanel12 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraGridTRI = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel7 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnEditTRI = New Infragistics.Win.Misc.UltraButton()
        Me.btnDelTRI = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddTRI = New Infragistics.Win.Misc.UltraButton()
        Me.UltraPanel13 = New Infragistics.Win.Misc.UltraPanel()
        Me.UltraGridAdvReq = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel14 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelAdvReq = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddAdvReq = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl10 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridTRO = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel11 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnEditTRO = New Infragistics.Win.Misc.UltraButton()
        Me.btnDelTRO = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddTRO = New Infragistics.Win.Misc.UltraButton()
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridAdv = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraPanel5 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnDelAdv = New Infragistics.Win.Misc.UltraButton()
        Me.btnAddAdv = New Infragistics.Win.Misc.UltraButton()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton()
        Me.btnSave = New Infragistics.Win.Misc.UltraButton()
        Me.Panel1 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnCostAssignment = New Infragistics.Win.Misc.UltraButton()
        Me.Panel2 = New Infragistics.Win.Misc.UltraPanel()
        Me.btnSelectCamp = New Infragistics.Win.Misc.UltraButton()
        Me.UltraLabel10 = New Infragistics.Win.Misc.UltraLabel()
        Me.dt_PostingDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel7 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_Remark = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.btnSelectEmp = New Infragistics.Win.Misc.UltraButton()
        Me.cmb_DivisionID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txt_VouchNum = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.dt_Dated = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_TourCountry = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel9 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_TotalPayment = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txt_LessAdvance = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel()
        Me.txt_TotalAmount = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_EmployeeID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmb_CampusID = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UEGB_Header = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel3 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        Me.TabCantrol = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UEGB_Cost = New Infragistics.Win.Misc.UltraExpandableGroupBox()
        Me.UltraExpandableGroupBoxPanel4 = New Infragistics.Win.Misc.UltraExpandableGroupBoxPanel()
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.UltraPanel10.ClientArea.SuspendLayout()
        Me.UltraPanel10.SuspendLayout()
        CType(Me.UltraGridJD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.ClientArea.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_ItemDetail.SuspendLayout()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.UltraGridDD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel1.ClientArea.SuspendLayout()
        Me.UltraPanel1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraGridHE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel2.ClientArea.SuspendLayout()
        Me.UltraPanel2.SuspendLayout()
        Me.UltraTabPageControl4.SuspendLayout()
        CType(Me.UltraGridLC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel3.ClientArea.SuspendLayout()
        Me.UltraPanel3.SuspendLayout()
        Me.UltraTabPageControl5.SuspendLayout()
        CType(Me.UltraGridMES, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel4.ClientArea.SuspendLayout()
        Me.UltraPanel4.SuspendLayout()
        Me.UltraTabPageControl9.SuspendLayout()
        Me.UltraPanel9.ClientArea.SuspendLayout()
        Me.UltraPanel9.SuspendLayout()
        CType(Me.UltraGridMEM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel8.ClientArea.SuspendLayout()
        Me.UltraPanel8.SuspendLayout()
        CType(Me.UEGB_Mat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_Mat.SuspendLayout()
        Me.UltraTabPageControl7.SuspendLayout()
        CType(Me.UltraGridMEA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel6.ClientArea.SuspendLayout()
        Me.UltraPanel6.SuspendLayout()
        Me.UltraTabPageControl8.SuspendLayout()
        Me.UltraPanel12.ClientArea.SuspendLayout()
        Me.UltraPanel12.SuspendLayout()
        CType(Me.UltraGridTRI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel7.ClientArea.SuspendLayout()
        Me.UltraPanel7.SuspendLayout()
        Me.UltraPanel13.ClientArea.SuspendLayout()
        Me.UltraPanel13.SuspendLayout()
        CType(Me.UltraGridAdvReq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel14.ClientArea.SuspendLayout()
        Me.UltraPanel14.SuspendLayout()
        Me.UltraTabPageControl10.SuspendLayout()
        CType(Me.UltraGridTRO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel11.ClientArea.SuspendLayout()
        Me.UltraPanel11.SuspendLayout()
        Me.UltraTabPageControl6.SuspendLayout()
        CType(Me.UltraGridAdv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraPanel5.ClientArea.SuspendLayout()
        Me.UltraPanel5.SuspendLayout()
        Me.Panel1.ClientArea.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.ClientArea.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dt_PostingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_Remark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_DivisionID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_TourCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TotalPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_LessAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_TotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_EmployeeID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmb_CampusID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_Header.SuspendLayout()
        Me.UltraExpandableGroupBoxPanel3.SuspendLayout()
        CType(Me.TabCantrol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabCantrol.SuspendLayout()
        CType(Me.UEGB_Cost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UEGB_Cost.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraPanel10)
        Me.UltraTabPageControl1.Controls.Add(Me.UEGB_ItemDetail)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(2, 19)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(998, 296)
        '
        'UltraPanel10
        '
        '
        'UltraPanel10.ClientArea
        '
        Me.UltraPanel10.ClientArea.Controls.Add(Me.UltraGridJD)
        Me.UltraPanel10.ClientArea.Controls.Add(Me.Panel4)
        Me.UltraPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPanel10.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel10.Name = "UltraPanel10"
        Me.UltraPanel10.Size = New System.Drawing.Size(998, 149)
        Me.UltraPanel10.TabIndex = 54
        '
        'UltraGridJD
        '
        Me.UltraGridJD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridJD.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridJD.Name = "UltraGridJD"
        Me.UltraGridJD.Size = New System.Drawing.Size(998, 124)
        Me.UltraGridJD.TabIndex = 0
        '
        'Panel4
        '
        '
        'Panel4.ClientArea
        '
        Me.Panel4.ClientArea.Controls.Add(Me.btnDelJD)
        Me.Panel4.ClientArea.Controls.Add(Me.btnAddJD)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 124)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(998, 25)
        Me.Panel4.TabIndex = 53
        '
        'btnDelJD
        '
        Me.btnDelJD.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelJD.Location = New System.Drawing.Point(70, 0)
        Me.btnDelJD.Name = "btnDelJD"
        Me.btnDelJD.Size = New System.Drawing.Size(70, 25)
        Me.btnDelJD.TabIndex = 1
        Me.btnDelJD.Text = "Delete"
        '
        'btnAddJD
        '
        Me.btnAddJD.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddJD.Location = New System.Drawing.Point(0, 0)
        Me.btnAddJD.Name = "btnAddJD"
        Me.btnAddJD.Size = New System.Drawing.Size(70, 25)
        Me.btnAddJD.TabIndex = 0
        Me.btnAddJD.Text = "Add New"
        '
        'UEGB_ItemDetail
        '
        Me.UEGB_ItemDetail.Controls.Add(Me.UltraExpandableGroupBoxPanel2)
        Me.UEGB_ItemDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UEGB_ItemDetail.ExpandedSize = New System.Drawing.Size(998, 147)
        Me.UEGB_ItemDetail.ExpansionIndicator = Infragistics.Win.Misc.GroupBoxExpansionIndicator.None
        Me.UEGB_ItemDetail.HeaderClickAction = Infragistics.Win.Misc.GroupBoxHeaderClickAction.None
        Me.UEGB_ItemDetail.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_ItemDetail.Location = New System.Drawing.Point(0, 149)
        Me.UEGB_ItemDetail.Name = "UEGB_ItemDetail"
        Me.UEGB_ItemDetail.Size = New System.Drawing.Size(998, 147)
        Me.UEGB_ItemDetail.TabIndex = 1
        Me.UEGB_ItemDetail.Text = "Item Details"
        '
        'UltraExpandableGroupBoxPanel2
        '
        Me.UltraExpandableGroupBoxPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel2.Location = New System.Drawing.Point(3, 17)
        Me.UltraExpandableGroupBoxPanel2.Name = "UltraExpandableGroupBoxPanel2"
        Me.UltraExpandableGroupBoxPanel2.Size = New System.Drawing.Size(992, 127)
        Me.UltraExpandableGroupBoxPanel2.TabIndex = 0
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.UltraGridDD)
        Me.UltraTabPageControl3.Controls.Add(Me.UltraPanel1)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(998, 296)
        '
        'UltraGridDD
        '
        Me.UltraGridDD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridDD.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridDD.Name = "UltraGridDD"
        Me.UltraGridDD.Size = New System.Drawing.Size(998, 271)
        Me.UltraGridDD.TabIndex = 0
        '
        'UltraPanel1
        '
        '
        'UltraPanel1.ClientArea
        '
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnDelDD)
        Me.UltraPanel1.ClientArea.Controls.Add(Me.btnAddDD)
        Me.UltraPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel1.Location = New System.Drawing.Point(0, 271)
        Me.UltraPanel1.Name = "UltraPanel1"
        Me.UltraPanel1.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel1.TabIndex = 54
        '
        'btnDelDD
        '
        Me.btnDelDD.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelDD.Location = New System.Drawing.Point(70, 0)
        Me.btnDelDD.Name = "btnDelDD"
        Me.btnDelDD.Size = New System.Drawing.Size(70, 25)
        Me.btnDelDD.TabIndex = 1
        Me.btnDelDD.Text = "Delete"
        '
        'btnAddDD
        '
        Me.btnAddDD.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddDD.Location = New System.Drawing.Point(0, 0)
        Me.btnAddDD.Name = "btnAddDD"
        Me.btnAddDD.Size = New System.Drawing.Size(70, 25)
        Me.btnAddDD.TabIndex = 0
        Me.btnAddDD.Text = "Add New"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGridHE)
        Me.UltraTabPageControl2.Controls.Add(Me.UltraPanel2)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(998, 296)
        '
        'UltraGridHE
        '
        Me.UltraGridHE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridHE.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridHE.Name = "UltraGridHE"
        Me.UltraGridHE.Size = New System.Drawing.Size(998, 271)
        Me.UltraGridHE.TabIndex = 0
        '
        'UltraPanel2
        '
        '
        'UltraPanel2.ClientArea
        '
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnDelHE)
        Me.UltraPanel2.ClientArea.Controls.Add(Me.btnAddHE)
        Me.UltraPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel2.Location = New System.Drawing.Point(0, 271)
        Me.UltraPanel2.Name = "UltraPanel2"
        Me.UltraPanel2.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel2.TabIndex = 54
        '
        'btnDelHE
        '
        Me.btnDelHE.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelHE.Location = New System.Drawing.Point(70, 0)
        Me.btnDelHE.Name = "btnDelHE"
        Me.btnDelHE.Size = New System.Drawing.Size(70, 25)
        Me.btnDelHE.TabIndex = 1
        Me.btnDelHE.Text = "Delete"
        '
        'btnAddHE
        '
        Me.btnAddHE.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddHE.Location = New System.Drawing.Point(0, 0)
        Me.btnAddHE.Name = "btnAddHE"
        Me.btnAddHE.Size = New System.Drawing.Size(70, 25)
        Me.btnAddHE.TabIndex = 0
        Me.btnAddHE.Text = "Add New"
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.UltraGridLC)
        Me.UltraTabPageControl4.Controls.Add(Me.UltraPanel3)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(998, 296)
        '
        'UltraGridLC
        '
        Me.UltraGridLC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridLC.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridLC.Name = "UltraGridLC"
        Me.UltraGridLC.Size = New System.Drawing.Size(998, 271)
        Me.UltraGridLC.TabIndex = 0
        '
        'UltraPanel3
        '
        '
        'UltraPanel3.ClientArea
        '
        Me.UltraPanel3.ClientArea.Controls.Add(Me.btnDelLC)
        Me.UltraPanel3.ClientArea.Controls.Add(Me.btnAddLC)
        Me.UltraPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel3.Location = New System.Drawing.Point(0, 271)
        Me.UltraPanel3.Name = "UltraPanel3"
        Me.UltraPanel3.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel3.TabIndex = 54
        '
        'btnDelLC
        '
        Me.btnDelLC.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelLC.Location = New System.Drawing.Point(70, 0)
        Me.btnDelLC.Name = "btnDelLC"
        Me.btnDelLC.Size = New System.Drawing.Size(70, 25)
        Me.btnDelLC.TabIndex = 1
        Me.btnDelLC.Text = "Delete"
        '
        'btnAddLC
        '
        Me.btnAddLC.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddLC.Location = New System.Drawing.Point(0, 0)
        Me.btnAddLC.Name = "btnAddLC"
        Me.btnAddLC.Size = New System.Drawing.Size(70, 25)
        Me.btnAddLC.TabIndex = 0
        Me.btnAddLC.Text = "Add New"
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Controls.Add(Me.UltraGridMES)
        Me.UltraTabPageControl5.Controls.Add(Me.UltraPanel4)
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(998, 296)
        '
        'UltraGridMES
        '
        Me.UltraGridMES.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridMES.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridMES.Name = "UltraGridMES"
        Me.UltraGridMES.Size = New System.Drawing.Size(998, 271)
        Me.UltraGridMES.TabIndex = 0
        '
        'UltraPanel4
        '
        '
        'UltraPanel4.ClientArea
        '
        Me.UltraPanel4.ClientArea.Controls.Add(Me.btnDelMES)
        Me.UltraPanel4.ClientArea.Controls.Add(Me.btnAddMES)
        Me.UltraPanel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel4.Location = New System.Drawing.Point(0, 271)
        Me.UltraPanel4.Name = "UltraPanel4"
        Me.UltraPanel4.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel4.TabIndex = 54
        '
        'btnDelMES
        '
        Me.btnDelMES.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelMES.Location = New System.Drawing.Point(70, 0)
        Me.btnDelMES.Name = "btnDelMES"
        Me.btnDelMES.Size = New System.Drawing.Size(70, 25)
        Me.btnDelMES.TabIndex = 1
        Me.btnDelMES.Text = "Delete"
        '
        'btnAddMES
        '
        Me.btnAddMES.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddMES.Location = New System.Drawing.Point(0, 0)
        Me.btnAddMES.Name = "btnAddMES"
        Me.btnAddMES.Size = New System.Drawing.Size(70, 25)
        Me.btnAddMES.TabIndex = 0
        Me.btnAddMES.Text = "Add New"
        '
        'UltraTabPageControl9
        '
        Me.UltraTabPageControl9.Controls.Add(Me.UltraPanel9)
        Me.UltraTabPageControl9.Controls.Add(Me.UEGB_Mat)
        Me.UltraTabPageControl9.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl9.Name = "UltraTabPageControl9"
        Me.UltraTabPageControl9.Size = New System.Drawing.Size(998, 296)
        '
        'UltraPanel9
        '
        '
        'UltraPanel9.ClientArea
        '
        Me.UltraPanel9.ClientArea.Controls.Add(Me.UltraGridMEM)
        Me.UltraPanel9.ClientArea.Controls.Add(Me.UltraPanel8)
        Me.UltraPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPanel9.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel9.Name = "UltraPanel9"
        Me.UltraPanel9.Size = New System.Drawing.Size(998, 149)
        Me.UltraPanel9.TabIndex = 57
        '
        'UltraGridMEM
        '
        Me.UltraGridMEM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridMEM.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridMEM.Name = "UltraGridMEM"
        Me.UltraGridMEM.Size = New System.Drawing.Size(998, 124)
        Me.UltraGridMEM.TabIndex = 56
        '
        'UltraPanel8
        '
        '
        'UltraPanel8.ClientArea
        '
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnDelMEM)
        Me.UltraPanel8.ClientArea.Controls.Add(Me.btnAddMEM)
        Me.UltraPanel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel8.Location = New System.Drawing.Point(0, 124)
        Me.UltraPanel8.Name = "UltraPanel8"
        Me.UltraPanel8.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel8.TabIndex = 55
        '
        'btnDelMEM
        '
        Me.btnDelMEM.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelMEM.Location = New System.Drawing.Point(70, 0)
        Me.btnDelMEM.Name = "btnDelMEM"
        Me.btnDelMEM.Size = New System.Drawing.Size(70, 25)
        Me.btnDelMEM.TabIndex = 1
        Me.btnDelMEM.Text = "Delete"
        '
        'btnAddMEM
        '
        Me.btnAddMEM.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddMEM.Location = New System.Drawing.Point(0, 0)
        Me.btnAddMEM.Name = "btnAddMEM"
        Me.btnAddMEM.Size = New System.Drawing.Size(70, 25)
        Me.btnAddMEM.TabIndex = 0
        Me.btnAddMEM.Text = "Add New"
        '
        'UEGB_Mat
        '
        Me.UEGB_Mat.Controls.Add(Me.UltraExpandableGroupBoxPanel1)
        Me.UEGB_Mat.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UEGB_Mat.ExpandedSize = New System.Drawing.Size(998, 147)
        Me.UEGB_Mat.ExpansionIndicator = Infragistics.Win.Misc.GroupBoxExpansionIndicator.None
        Me.UEGB_Mat.HeaderClickAction = Infragistics.Win.Misc.GroupBoxHeaderClickAction.None
        Me.UEGB_Mat.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_Mat.Location = New System.Drawing.Point(0, 149)
        Me.UEGB_Mat.Name = "UEGB_Mat"
        Me.UEGB_Mat.Size = New System.Drawing.Size(998, 147)
        Me.UEGB_Mat.TabIndex = 58
        Me.UEGB_Mat.Text = "Item Details"
        '
        'UltraExpandableGroupBoxPanel1
        '
        Me.UltraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel1.Location = New System.Drawing.Point(3, 17)
        Me.UltraExpandableGroupBoxPanel1.Name = "UltraExpandableGroupBoxPanel1"
        Me.UltraExpandableGroupBoxPanel1.Size = New System.Drawing.Size(992, 127)
        Me.UltraExpandableGroupBoxPanel1.TabIndex = 0
        '
        'UltraTabPageControl7
        '
        Me.UltraTabPageControl7.Controls.Add(Me.UltraGridMEA)
        Me.UltraTabPageControl7.Controls.Add(Me.UltraPanel6)
        Me.UltraTabPageControl7.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl7.Name = "UltraTabPageControl7"
        Me.UltraTabPageControl7.Size = New System.Drawing.Size(998, 296)
        '
        'UltraGridMEA
        '
        Me.UltraGridMEA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridMEA.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridMEA.Name = "UltraGridMEA"
        Me.UltraGridMEA.Size = New System.Drawing.Size(998, 271)
        Me.UltraGridMEA.TabIndex = 55
        '
        'UltraPanel6
        '
        '
        'UltraPanel6.ClientArea
        '
        Me.UltraPanel6.ClientArea.Controls.Add(Me.btnDelMEA)
        Me.UltraPanel6.ClientArea.Controls.Add(Me.btnAddMEA)
        Me.UltraPanel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel6.Location = New System.Drawing.Point(0, 271)
        Me.UltraPanel6.Name = "UltraPanel6"
        Me.UltraPanel6.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel6.TabIndex = 56
        '
        'btnDelMEA
        '
        Me.btnDelMEA.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelMEA.Location = New System.Drawing.Point(70, 0)
        Me.btnDelMEA.Name = "btnDelMEA"
        Me.btnDelMEA.Size = New System.Drawing.Size(70, 25)
        Me.btnDelMEA.TabIndex = 1
        Me.btnDelMEA.Text = "Delete"
        '
        'btnAddMEA
        '
        Me.btnAddMEA.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddMEA.Location = New System.Drawing.Point(0, 0)
        Me.btnAddMEA.Name = "btnAddMEA"
        Me.btnAddMEA.Size = New System.Drawing.Size(70, 25)
        Me.btnAddMEA.TabIndex = 0
        Me.btnAddMEA.Text = "Add New"
        '
        'UltraTabPageControl8
        '
        Me.UltraTabPageControl8.Controls.Add(Me.UltraPanel12)
        Me.UltraTabPageControl8.Controls.Add(Me.UltraPanel13)
        Me.UltraTabPageControl8.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl8.Name = "UltraTabPageControl8"
        Me.UltraTabPageControl8.Size = New System.Drawing.Size(998, 296)
        '
        'UltraPanel12
        '
        '
        'UltraPanel12.ClientArea
        '
        Me.UltraPanel12.ClientArea.Controls.Add(Me.UltraGridTRI)
        Me.UltraPanel12.ClientArea.Controls.Add(Me.UltraPanel7)
        Me.UltraPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraPanel12.Location = New System.Drawing.Point(0, 0)
        Me.UltraPanel12.Name = "UltraPanel12"
        Me.UltraPanel12.Size = New System.Drawing.Size(998, 157)
        Me.UltraPanel12.TabIndex = 60
        '
        'UltraGridTRI
        '
        Me.UltraGridTRI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridTRI.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridTRI.Name = "UltraGridTRI"
        Me.UltraGridTRI.Size = New System.Drawing.Size(998, 132)
        Me.UltraGridTRI.TabIndex = 58
        '
        'UltraPanel7
        '
        '
        'UltraPanel7.ClientArea
        '
        Me.UltraPanel7.ClientArea.Controls.Add(Me.btnEditTRI)
        Me.UltraPanel7.ClientArea.Controls.Add(Me.btnDelTRI)
        Me.UltraPanel7.ClientArea.Controls.Add(Me.btnAddTRI)
        Me.UltraPanel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel7.Location = New System.Drawing.Point(0, 132)
        Me.UltraPanel7.Name = "UltraPanel7"
        Me.UltraPanel7.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel7.TabIndex = 59
        '
        'btnEditTRI
        '
        Me.btnEditTRI.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnEditTRI.Location = New System.Drawing.Point(140, 0)
        Me.btnEditTRI.Name = "btnEditTRI"
        Me.btnEditTRI.Size = New System.Drawing.Size(70, 25)
        Me.btnEditTRI.TabIndex = 2
        Me.btnEditTRI.Text = "Edit"
        '
        'btnDelTRI
        '
        Me.btnDelTRI.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelTRI.Location = New System.Drawing.Point(70, 0)
        Me.btnDelTRI.Name = "btnDelTRI"
        Me.btnDelTRI.Size = New System.Drawing.Size(70, 25)
        Me.btnDelTRI.TabIndex = 1
        Me.btnDelTRI.Text = "Delete"
        '
        'btnAddTRI
        '
        Me.btnAddTRI.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddTRI.Location = New System.Drawing.Point(0, 0)
        Me.btnAddTRI.Name = "btnAddTRI"
        Me.btnAddTRI.Size = New System.Drawing.Size(70, 25)
        Me.btnAddTRI.TabIndex = 0
        Me.btnAddTRI.Text = "Add New"
        '
        'UltraPanel13
        '
        '
        'UltraPanel13.ClientArea
        '
        Me.UltraPanel13.ClientArea.Controls.Add(Me.UltraGridAdvReq)
        Me.UltraPanel13.ClientArea.Controls.Add(Me.UltraPanel14)
        Me.UltraPanel13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel13.Location = New System.Drawing.Point(0, 157)
        Me.UltraPanel13.Name = "UltraPanel13"
        Me.UltraPanel13.Size = New System.Drawing.Size(998, 139)
        Me.UltraPanel13.TabIndex = 61
        '
        'UltraGridAdvReq
        '
        Me.UltraGridAdvReq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridAdvReq.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridAdvReq.Name = "UltraGridAdvReq"
        Me.UltraGridAdvReq.Size = New System.Drawing.Size(998, 114)
        Me.UltraGridAdvReq.TabIndex = 54
        '
        'UltraPanel14
        '
        '
        'UltraPanel14.ClientArea
        '
        Me.UltraPanel14.ClientArea.Controls.Add(Me.btnDelAdvReq)
        Me.UltraPanel14.ClientArea.Controls.Add(Me.btnAddAdvReq)
        Me.UltraPanel14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel14.Location = New System.Drawing.Point(0, 114)
        Me.UltraPanel14.Name = "UltraPanel14"
        Me.UltraPanel14.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel14.TabIndex = 53
        '
        'btnDelAdvReq
        '
        Me.btnDelAdvReq.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelAdvReq.Location = New System.Drawing.Point(70, 0)
        Me.btnDelAdvReq.Name = "btnDelAdvReq"
        Me.btnDelAdvReq.Size = New System.Drawing.Size(70, 25)
        Me.btnDelAdvReq.TabIndex = 1
        Me.btnDelAdvReq.Text = "Delete"
        '
        'btnAddAdvReq
        '
        Me.btnAddAdvReq.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddAdvReq.Location = New System.Drawing.Point(0, 0)
        Me.btnAddAdvReq.Name = "btnAddAdvReq"
        Me.btnAddAdvReq.Size = New System.Drawing.Size(70, 25)
        Me.btnAddAdvReq.TabIndex = 0
        Me.btnAddAdvReq.Text = "Add New"
        '
        'UltraTabPageControl10
        '
        Me.UltraTabPageControl10.Controls.Add(Me.UltraGridTRO)
        Me.UltraTabPageControl10.Controls.Add(Me.UltraPanel11)
        Me.UltraTabPageControl10.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl10.Name = "UltraTabPageControl10"
        Me.UltraTabPageControl10.Size = New System.Drawing.Size(998, 296)
        '
        'UltraGridTRO
        '
        Me.UltraGridTRO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridTRO.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridTRO.Name = "UltraGridTRO"
        Me.UltraGridTRO.Size = New System.Drawing.Size(998, 271)
        Me.UltraGridTRO.TabIndex = 61
        '
        'UltraPanel11
        '
        '
        'UltraPanel11.ClientArea
        '
        Me.UltraPanel11.ClientArea.Controls.Add(Me.btnEditTRO)
        Me.UltraPanel11.ClientArea.Controls.Add(Me.btnDelTRO)
        Me.UltraPanel11.ClientArea.Controls.Add(Me.btnAddTRO)
        Me.UltraPanel11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel11.Location = New System.Drawing.Point(0, 271)
        Me.UltraPanel11.Name = "UltraPanel11"
        Me.UltraPanel11.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel11.TabIndex = 60
        '
        'btnEditTRO
        '
        Me.btnEditTRO.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnEditTRO.Location = New System.Drawing.Point(140, 0)
        Me.btnEditTRO.Name = "btnEditTRO"
        Me.btnEditTRO.Size = New System.Drawing.Size(70, 25)
        Me.btnEditTRO.TabIndex = 2
        Me.btnEditTRO.Text = "Edit"
        '
        'btnDelTRO
        '
        Me.btnDelTRO.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelTRO.Location = New System.Drawing.Point(70, 0)
        Me.btnDelTRO.Name = "btnDelTRO"
        Me.btnDelTRO.Size = New System.Drawing.Size(70, 25)
        Me.btnDelTRO.TabIndex = 1
        Me.btnDelTRO.Text = "Delete"
        '
        'btnAddTRO
        '
        Me.btnAddTRO.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddTRO.Location = New System.Drawing.Point(0, 0)
        Me.btnAddTRO.Name = "btnAddTRO"
        Me.btnAddTRO.Size = New System.Drawing.Size(70, 25)
        Me.btnAddTRO.TabIndex = 0
        Me.btnAddTRO.Text = "Add New"
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Controls.Add(Me.UltraGridAdv)
        Me.UltraTabPageControl6.Controls.Add(Me.UltraPanel5)
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(998, 296)
        '
        'UltraGridAdv
        '
        Me.UltraGridAdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGridAdv.Location = New System.Drawing.Point(0, 0)
        Me.UltraGridAdv.Name = "UltraGridAdv"
        Me.UltraGridAdv.Size = New System.Drawing.Size(998, 271)
        Me.UltraGridAdv.TabIndex = 0
        '
        'UltraPanel5
        '
        '
        'UltraPanel5.ClientArea
        '
        Me.UltraPanel5.ClientArea.Controls.Add(Me.btnDelAdv)
        Me.UltraPanel5.ClientArea.Controls.Add(Me.btnAddAdv)
        Me.UltraPanel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UltraPanel5.Location = New System.Drawing.Point(0, 271)
        Me.UltraPanel5.Name = "UltraPanel5"
        Me.UltraPanel5.Size = New System.Drawing.Size(998, 25)
        Me.UltraPanel5.TabIndex = 57
        '
        'btnDelAdv
        '
        Me.btnDelAdv.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDelAdv.Location = New System.Drawing.Point(70, 0)
        Me.btnDelAdv.Name = "btnDelAdv"
        Me.btnDelAdv.Size = New System.Drawing.Size(70, 25)
        Me.btnDelAdv.TabIndex = 1
        Me.btnDelAdv.Text = "Delete"
        '
        'btnAddAdv
        '
        Me.btnAddAdv.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddAdv.Location = New System.Drawing.Point(0, 0)
        Me.btnAddAdv.Name = "btnAddAdv"
        Me.btnAddAdv.Size = New System.Drawing.Size(70, 25)
        Me.btnAddAdv.TabIndex = 0
        Me.btnAddAdv.Text = "Add New"
        '
        'btnOK
        '
        Appearance1.FontData.BoldAsString = "True"
        Me.btnOK.Appearance = Appearance1
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.Location = New System.Drawing.Point(940, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(68, 33)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Appearance2.FontData.BoldAsString = "True"
        Me.btnCancel.Appearance = Appearance2
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.Location = New System.Drawing.Point(872, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 33)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Appearance3.FontData.BoldAsString = "True"
        Me.btnSave.Appearance = Appearance3
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Location = New System.Drawing.Point(804, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 33)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'Panel1
        '
        '
        'Panel1.ClientArea
        '
        Me.Panel1.ClientArea.Controls.Add(Me.btnCostAssignment)
        Me.Panel1.ClientArea.Controls.Add(Me.btnSave)
        Me.Panel1.ClientArea.Controls.Add(Me.btnCancel)
        Me.Panel1.ClientArea.Controls.Add(Me.btnOK)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 644)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1008, 33)
        Me.Panel1.TabIndex = 2
        '
        'btnCostAssignment
        '
        Appearance4.FontData.BoldAsString = "True"
        Me.btnCostAssignment.Appearance = Appearance4
        Me.btnCostAssignment.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCostAssignment.Location = New System.Drawing.Point(0, 0)
        Me.btnCostAssignment.Name = "btnCostAssignment"
        Me.btnCostAssignment.Size = New System.Drawing.Size(145, 33)
        Me.btnCostAssignment.TabIndex = 3
        Me.btnCostAssignment.Text = "Cost Assignment >>>"
        '
        'Panel2
        '
        '
        'Panel2.ClientArea
        '
        Me.Panel2.ClientArea.Controls.Add(Me.btnSelectCamp)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel10)
        Me.Panel2.ClientArea.Controls.Add(Me.dt_PostingDate)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel7)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_Remark)
        Me.Panel2.ClientArea.Controls.Add(Me.btnSelectEmp)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_DivisionID)
        Me.Panel2.ClientArea.Controls.Add(Me.Label21)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_VouchNum)
        Me.Panel2.ClientArea.Controls.Add(Me.dt_Dated)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel5)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel2)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_TourCountry)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel3)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel9)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel8)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_TotalPayment)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_LessAdvance)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel6)
        Me.Panel2.ClientArea.Controls.Add(Me.txt_TotalAmount)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel1)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_EmployeeID)
        Me.Panel2.ClientArea.Controls.Add(Me.UltraLabel4)
        Me.Panel2.ClientArea.Controls.Add(Me.cmb_CampusID)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1008, 124)
        Me.Panel2.TabIndex = 0
        '
        'btnSelectCamp
        '
        Me.btnSelectCamp.Location = New System.Drawing.Point(458, 35)
        Me.btnSelectCamp.Name = "btnSelectCamp"
        Me.btnSelectCamp.Size = New System.Drawing.Size(64, 28)
        Me.btnSelectCamp.TabIndex = 174
        Me.btnSelectCamp.Text = "Select"
        '
        'UltraLabel10
        '
        Appearance5.FontData.SizeInPoints = 8.25!
        Appearance5.TextHAlignAsString = "Right"
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel10.Appearance = Appearance5
        Me.UltraLabel10.AutoSize = True
        Me.UltraLabel10.Location = New System.Drawing.Point(277, 14)
        Me.UltraLabel10.Name = "UltraLabel10"
        Me.UltraLabel10.Size = New System.Drawing.Size(69, 14)
        Me.UltraLabel10.TabIndex = 172
        Me.UltraLabel10.Text = "Posting Date"
        '
        'dt_PostingDate
        '
        Me.dt_PostingDate.FormatString = "ddd dd MMM yyyy"
        Me.dt_PostingDate.Location = New System.Drawing.Point(349, 10)
        Me.dt_PostingDate.Name = "dt_PostingDate"
        Me.dt_PostingDate.NullText = "Not Defined"
        Me.dt_PostingDate.Size = New System.Drawing.Size(173, 22)
        Me.dt_PostingDate.TabIndex = 173
        '
        'UltraLabel7
        '
        Appearance6.FontData.SizeInPoints = 8.25!
        Appearance6.TextHAlignAsString = "Right"
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel7.Appearance = Appearance6
        Me.UltraLabel7.AutoSize = True
        Me.UltraLabel7.Location = New System.Drawing.Point(31, 101)
        Me.UltraLabel7.Name = "UltraLabel7"
        Me.UltraLabel7.Size = New System.Drawing.Size(44, 14)
        Me.UltraLabel7.TabIndex = 171
        Me.UltraLabel7.Text = "Remark"
        '
        'txt_Remark
        '
        Me.txt_Remark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Remark.Location = New System.Drawing.Point(78, 97)
        Me.txt_Remark.Name = "txt_Remark"
        Me.txt_Remark.Size = New System.Drawing.Size(915, 22)
        Me.txt_Remark.TabIndex = 170
        '
        'btnSelectEmp
        '
        Me.btnSelectEmp.Location = New System.Drawing.Point(458, 65)
        Me.btnSelectEmp.Name = "btnSelectEmp"
        Me.btnSelectEmp.Size = New System.Drawing.Size(64, 28)
        Me.btnSelectEmp.TabIndex = 167
        Me.btnSelectEmp.Text = "Select"
        '
        'cmb_DivisionID
        '
        Me.cmb_DivisionID.Location = New System.Drawing.Point(601, 38)
        Me.cmb_DivisionID.Name = "cmb_DivisionID"
        Me.cmb_DivisionID.Size = New System.Drawing.Size(178, 23)
        Me.cmb_DivisionID.TabIndex = 17
        Me.cmb_DivisionID.Text = "UltraCombo4"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.Label21.Location = New System.Drawing.Point(554, 43)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(44, 14)
        Me.Label21.TabIndex = 16
        Me.Label21.Text = "Division"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_VouchNum
        '
        Me.txt_VouchNum.Location = New System.Drawing.Point(601, 10)
        Me.txt_VouchNum.Name = "txt_VouchNum"
        Me.txt_VouchNum.ReadOnly = True
        Me.txt_VouchNum.Size = New System.Drawing.Size(178, 22)
        Me.txt_VouchNum.TabIndex = 5
        '
        'dt_Dated
        '
        Me.dt_Dated.FormatString = "ddd dd MMM yyyy"
        Me.dt_Dated.Location = New System.Drawing.Point(78, 10)
        Me.dt_Dated.Name = "dt_Dated"
        Me.dt_Dated.NullText = "Not Defined"
        Me.dt_Dated.Size = New System.Drawing.Size(174, 22)
        Me.dt_Dated.TabIndex = 3
        '
        'UltraLabel5
        '
        Appearance7.FontData.SizeInPoints = 8.25!
        Appearance7.TextHAlignAsString = "Right"
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance7
        Me.UltraLabel5.AutoSize = True
        Me.UltraLabel5.Location = New System.Drawing.Point(554, 73)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(44, 14)
        Me.UltraLabel5.TabIndex = 6
        Me.UltraLabel5.Text = "Country"
        '
        'UltraLabel2
        '
        Appearance8.FontData.SizeInPoints = 8.25!
        Appearance8.TextHAlignAsString = "Right"
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance8
        Me.UltraLabel2.AutoSize = True
        Me.UltraLabel2.Location = New System.Drawing.Point(47, 13)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(28, 14)
        Me.UltraLabel2.TabIndex = 2
        Me.UltraLabel2.Text = "Date"
        '
        'cmb_TourCountry
        '
        Me.cmb_TourCountry.Location = New System.Drawing.Point(601, 68)
        Me.cmb_TourCountry.Name = "cmb_TourCountry"
        Me.cmb_TourCountry.Size = New System.Drawing.Size(178, 23)
        Me.cmb_TourCountry.TabIndex = 7
        '
        'UltraLabel3
        '
        Appearance9.FontData.SizeInPoints = 8.25!
        Appearance9.TextHAlignAsString = "Right"
        Appearance9.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance9
        Me.UltraLabel3.AutoSize = True
        Me.UltraLabel3.Location = New System.Drawing.Point(531, 14)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(67, 14)
        Me.UltraLabel3.TabIndex = 4
        Me.UltraLabel3.Text = "Voucher No."
        '
        'UltraLabel9
        '
        Appearance10.FontData.SizeInPoints = 8.25!
        Appearance10.TextHAlignAsString = "Right"
        Appearance10.TextVAlignAsString = "Middle"
        Me.UltraLabel9.Appearance = Appearance10
        Me.UltraLabel9.AutoSize = True
        Me.UltraLabel9.Location = New System.Drawing.Point(783, 43)
        Me.UltraLabel9.Name = "UltraLabel9"
        Me.UltraLabel9.Size = New System.Drawing.Size(77, 14)
        Me.UltraLabel9.TabIndex = 12
        Me.UltraLabel9.Text = "Total Payment"
        '
        'UltraLabel8
        '
        Appearance11.FontData.SizeInPoints = 8.25!
        Appearance11.TextHAlignAsString = "Right"
        Appearance11.TextVAlignAsString = "Middle"
        Me.UltraLabel8.Appearance = Appearance11
        Me.UltraLabel8.AutoSize = True
        Me.UltraLabel8.Location = New System.Drawing.Point(785, 72)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(75, 14)
        Me.UltraLabel8.TabIndex = 14
        Me.UltraLabel8.Text = "Less Advance"
        '
        'txt_TotalPayment
        '
        Me.txt_TotalPayment.Location = New System.Drawing.Point(863, 39)
        Me.txt_TotalPayment.Name = "txt_TotalPayment"
        Me.txt_TotalPayment.ReadOnly = True
        Me.txt_TotalPayment.Size = New System.Drawing.Size(130, 22)
        Me.txt_TotalPayment.TabIndex = 13
        Me.txt_TotalPayment.Text = "UltraTextEditor2"
        '
        'txt_LessAdvance
        '
        Me.txt_LessAdvance.Location = New System.Drawing.Point(863, 68)
        Me.txt_LessAdvance.Name = "txt_LessAdvance"
        Me.txt_LessAdvance.ReadOnly = True
        Me.txt_LessAdvance.Size = New System.Drawing.Size(130, 22)
        Me.txt_LessAdvance.TabIndex = 15
        Me.txt_LessAdvance.Text = "UltraTextEditor1"
        '
        'UltraLabel6
        '
        Appearance12.FontData.SizeInPoints = 8.25!
        Appearance12.TextHAlignAsString = "Right"
        Appearance12.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance12
        Me.UltraLabel6.AutoSize = True
        Me.UltraLabel6.Location = New System.Drawing.Point(788, 14)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(72, 14)
        Me.UltraLabel6.TabIndex = 10
        Me.UltraLabel6.Text = "Total Amount"
        '
        'txt_TotalAmount
        '
        Me.txt_TotalAmount.Location = New System.Drawing.Point(863, 10)
        Me.txt_TotalAmount.Name = "txt_TotalAmount"
        Me.txt_TotalAmount.Size = New System.Drawing.Size(130, 22)
        Me.txt_TotalAmount.TabIndex = 11
        Me.txt_TotalAmount.Text = "UltraTextEditor1"
        '
        'UltraLabel1
        '
        Appearance13.FontData.SizeInPoints = 8.25!
        Appearance13.TextHAlignAsString = "Right"
        Appearance13.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance13
        Me.UltraLabel1.AutoSize = True
        Me.UltraLabel1.Location = New System.Drawing.Point(20, 72)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(55, 14)
        Me.UltraLabel1.TabIndex = 8
        Me.UltraLabel1.Text = "Employee"
        '
        'cmb_EmployeeID
        '
        Me.cmb_EmployeeID.Location = New System.Drawing.Point(78, 67)
        Me.cmb_EmployeeID.Name = "cmb_EmployeeID"
        Me.cmb_EmployeeID.ReadOnly = True
        Me.cmb_EmployeeID.Size = New System.Drawing.Size(379, 23)
        Me.cmb_EmployeeID.TabIndex = 9
        '
        'UltraLabel4
        '
        Appearance14.FontData.SizeInPoints = 8.25!
        Appearance14.TextHAlignAsString = "Right"
        Appearance14.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance14
        Me.UltraLabel4.AutoSize = True
        Me.UltraLabel4.Location = New System.Drawing.Point(29, 42)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(46, 14)
        Me.UltraLabel4.TabIndex = 0
        Me.UltraLabel4.Text = "Campus"
        '
        'cmb_CampusID
        '
        Me.cmb_CampusID.Location = New System.Drawing.Point(78, 38)
        Me.cmb_CampusID.Name = "cmb_CampusID"
        Me.cmb_CampusID.ReadOnly = True
        Me.cmb_CampusID.Size = New System.Drawing.Size(379, 23)
        Me.cmb_CampusID.TabIndex = 1
        '
        'UEGB_Header
        '
        Me.UEGB_Header.Controls.Add(Me.UltraExpandableGroupBoxPanel3)
        Me.UEGB_Header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UEGB_Header.ExpandedSize = New System.Drawing.Size(1008, 520)
        Me.UEGB_Header.ExpansionIndicator = Infragistics.Win.Misc.GroupBoxExpansionIndicator.None
        Me.UEGB_Header.HeaderClickAction = Infragistics.Win.Misc.GroupBoxHeaderClickAction.None
        Me.UEGB_Header.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_Header.Location = New System.Drawing.Point(0, 124)
        Me.UEGB_Header.Name = "UEGB_Header"
        Me.UEGB_Header.Size = New System.Drawing.Size(1008, 520)
        Me.UEGB_Header.TabIndex = 1
        Me.UEGB_Header.Text = "Details"
        '
        'UltraExpandableGroupBoxPanel3
        '
        Me.UltraExpandableGroupBoxPanel3.Controls.Add(Me.TabCantrol)
        Me.UltraExpandableGroupBoxPanel3.Controls.Add(Me.UEGB_Cost)
        Me.UltraExpandableGroupBoxPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel3.Location = New System.Drawing.Point(3, 17)
        Me.UltraExpandableGroupBoxPanel3.Name = "UltraExpandableGroupBoxPanel3"
        Me.UltraExpandableGroupBoxPanel3.Size = New System.Drawing.Size(1002, 500)
        Me.UltraExpandableGroupBoxPanel3.TabIndex = 0
        '
        'TabCantrol
        '
        Appearance15.BackColor = System.Drawing.SystemColors.Control
        Me.TabCantrol.ActiveTabAppearance = Appearance15
        Me.TabCantrol.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl1)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl3)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl2)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl4)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl5)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl6)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl7)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl8)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl9)
        Me.TabCantrol.Controls.Add(Me.UltraTabPageControl10)
        Me.TabCantrol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabCantrol.Location = New System.Drawing.Point(0, 0)
        Me.TabCantrol.Name = "TabCantrol"
        Appearance16.BackColor = System.Drawing.SystemColors.Control
        Appearance16.FontData.BoldAsString = "True"
        Me.TabCantrol.SelectedTabAppearance = Appearance16
        Me.TabCantrol.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.TabCantrol.ShowTabListButton = Infragistics.Win.DefaultableBoolean.[False]
        Me.TabCantrol.Size = New System.Drawing.Size(1002, 317)
        Me.TabCantrol.SpaceBeforeTabs = New Infragistics.Win.DefaultableInteger(50)
        Me.TabCantrol.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003
        Me.TabCantrol.TabIndex = 0
        Me.TabCantrol.TabPadding = New System.Drawing.Size(10, 0)
        UltraTab1.Key = "JD"
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Journey Details"
        UltraTab3.Key = "DD"
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "DA"
        UltraTab2.Key = "HE"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Hotel"
        UltraTab4.Key = "LC"
        UltraTab4.TabPage = Me.UltraTabPageControl4
        UltraTab4.Text = "Local Conveyance"
        UltraTab5.Key = "MES"
        UltraTab5.TabPage = Me.UltraTabPageControl5
        UltraTab5.Text = "Miscellaneous"
        UltraTab9.Key = "MEM"
        UltraTab9.TabPage = Me.UltraTabPageControl9
        UltraTab9.Text = "Material"
        UltraTab7.Key = "MEA"
        UltraTab7.TabPage = Me.UltraTabPageControl7
        UltraTab7.Text = "Asset"
        UltraTab8.Key = "TRI"
        UltraTab8.TabPage = Me.UltraTabPageControl8
        UltraTab8.Text = "Transfer Imprest"
        UltraTab10.Key = "TRO"
        UltraTab10.TabPage = Me.UltraTabPageControl10
        UltraTab10.Text = "Transfer Office"
        UltraTab6.Key = "ADV"
        UltraTab6.TabPage = Me.UltraTabPageControl6
        UltraTab6.Text = "Advance for Expenses"
        Me.TabCantrol.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab3, UltraTab2, UltraTab4, UltraTab5, UltraTab9, UltraTab7, UltraTab8, UltraTab10, UltraTab6})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(998, 296)
        '
        'UEGB_Cost
        '
        Me.UEGB_Cost.Controls.Add(Me.UltraExpandableGroupBoxPanel4)
        Me.UEGB_Cost.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.UEGB_Cost.ExpandedSize = New System.Drawing.Size(1002, 183)
        Me.UEGB_Cost.ExpansionIndicator = Infragistics.Win.Misc.GroupBoxExpansionIndicator.None
        Me.UEGB_Cost.HeaderClickAction = Infragistics.Win.Misc.GroupBoxHeaderClickAction.None
        Me.UEGB_Cost.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder
        Me.UEGB_Cost.Location = New System.Drawing.Point(0, 317)
        Me.UEGB_Cost.Name = "UEGB_Cost"
        Me.UEGB_Cost.Size = New System.Drawing.Size(1002, 183)
        Me.UEGB_Cost.TabIndex = 2
        Me.UEGB_Cost.Text = "Cost Assignment"
        '
        'UltraExpandableGroupBoxPanel4
        '
        Me.UltraExpandableGroupBoxPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraExpandableGroupBoxPanel4.Location = New System.Drawing.Point(3, 17)
        Me.UltraExpandableGroupBoxPanel4.Name = "UltraExpandableGroupBoxPanel4"
        Me.UltraExpandableGroupBoxPanel4.Size = New System.Drawing.Size(996, 163)
        Me.UltraExpandableGroupBoxPanel4.TabIndex = 0
        '
        'frmTourExpRep
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.Caption = "Expense Voucher"
        Me.ClientSize = New System.Drawing.Size(1008, 677)
        Me.Controls.Add(Me.UEGB_Header)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Name = "frmTourExpRep"
        Me.Text = "Expense Voucher"
        CType(Me.eBag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraPanel10.ClientArea.ResumeLayout(False)
        Me.UltraPanel10.ResumeLayout(False)
        CType(Me.UltraGridJD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ClientArea.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.UEGB_ItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_ItemDetail.ResumeLayout(False)
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.UltraGridDD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel1.ClientArea.ResumeLayout(False)
        Me.UltraPanel1.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.UltraGridHE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel2.ClientArea.ResumeLayout(False)
        Me.UltraPanel2.ResumeLayout(False)
        Me.UltraTabPageControl4.ResumeLayout(False)
        CType(Me.UltraGridLC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel3.ClientArea.ResumeLayout(False)
        Me.UltraPanel3.ResumeLayout(False)
        Me.UltraTabPageControl5.ResumeLayout(False)
        CType(Me.UltraGridMES, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel4.ClientArea.ResumeLayout(False)
        Me.UltraPanel4.ResumeLayout(False)
        Me.UltraTabPageControl9.ResumeLayout(False)
        Me.UltraPanel9.ClientArea.ResumeLayout(False)
        Me.UltraPanel9.ResumeLayout(False)
        CType(Me.UltraGridMEM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel8.ClientArea.ResumeLayout(False)
        Me.UltraPanel8.ResumeLayout(False)
        CType(Me.UEGB_Mat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_Mat.ResumeLayout(False)
        Me.UltraTabPageControl7.ResumeLayout(False)
        CType(Me.UltraGridMEA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel6.ClientArea.ResumeLayout(False)
        Me.UltraPanel6.ResumeLayout(False)
        Me.UltraTabPageControl8.ResumeLayout(False)
        Me.UltraPanel12.ClientArea.ResumeLayout(False)
        Me.UltraPanel12.ResumeLayout(False)
        CType(Me.UltraGridTRI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel7.ClientArea.ResumeLayout(False)
        Me.UltraPanel7.ResumeLayout(False)
        Me.UltraPanel13.ClientArea.ResumeLayout(False)
        Me.UltraPanel13.ResumeLayout(False)
        CType(Me.UltraGridAdvReq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel14.ClientArea.ResumeLayout(False)
        Me.UltraPanel14.ResumeLayout(False)
        Me.UltraTabPageControl10.ResumeLayout(False)
        CType(Me.UltraGridTRO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel11.ClientArea.ResumeLayout(False)
        Me.UltraPanel11.ResumeLayout(False)
        Me.UltraTabPageControl6.ResumeLayout(False)
        CType(Me.UltraGridAdv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraPanel5.ClientArea.ResumeLayout(False)
        Me.UltraPanel5.ResumeLayout(False)
        Me.Panel1.ClientArea.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ClientArea.ResumeLayout(False)
        Me.Panel2.ClientArea.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dt_PostingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_Remark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_DivisionID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_VouchNum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_Dated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_TourCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TotalPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_LessAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_TotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_EmployeeID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmb_CampusID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UEGB_Header, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_Header.ResumeLayout(False)
        Me.UltraExpandableGroupBoxPanel3.ResumeLayout(False)
        CType(Me.TabCantrol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabCantrol.ResumeLayout(False)
        CType(Me.UEGB_Cost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UEGB_Cost.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnSave As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Panel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents Panel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents dt_Dated As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_EmployeeID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_CampusID As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UEGB_Header As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel3 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents TabCantrol As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelJD As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddJD As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraGridJD As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraGridDD As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel1 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelDD As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddDD As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridHE As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel2 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelHE As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddHE As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridLC As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel3 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelLC As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddLC As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridMES As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel4 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelMES As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddMES As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UEGB_ItemDetail As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel2 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_TotalAmount As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridAdv As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraPanel5 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnAddAdv As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDelAdv As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel9 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_TotalPayment As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_LessAdvance As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txt_VouchNum As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmb_TourCountry As ug.UltraCombo
    Friend WithEvents cmb_DivisionID As ug.UltraCombo
    Friend WithEvents Label21 As Windows.Forms.Label
    Friend WithEvents btnSelectEmp As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl7 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridMEA As ug.UltraGrid
    Friend WithEvents UltraPanel6 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelMEA As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddMEA As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl8 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridTRI As ug.UltraGrid
    Friend WithEvents UltraPanel7 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelTRI As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddTRI As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnEditTRI As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraTabPageControl9 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridMEM As ug.UltraGrid
    Friend WithEvents UltraPanel8 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelMEM As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddMEM As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel9 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UEGB_Mat As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel1 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents UltraPanel10 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraTabPageControl10 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridTRO As ug.UltraGrid
    Friend WithEvents UltraPanel11 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnEditTRO As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnDelTRO As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddTRO As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel7 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txt_Remark As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel10 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents dt_PostingDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents UltraPanel12 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnSelectCamp As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UEGB_Cost As Infragistics.Win.Misc.UltraExpandableGroupBox
    Friend WithEvents UltraExpandableGroupBoxPanel4 As Infragistics.Win.Misc.UltraExpandableGroupBoxPanel
    Friend WithEvents btnCostAssignment As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraPanel13 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents UltraPanel14 As Infragistics.Win.Misc.UltraPanel
    Friend WithEvents btnDelAdvReq As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnAddAdvReq As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraGridAdvReq As ug.UltraGrid

#End Region
End Class

