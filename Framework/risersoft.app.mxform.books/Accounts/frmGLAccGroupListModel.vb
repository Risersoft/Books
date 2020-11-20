Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmGLAccGroupListModel
    Inherits clsFormDataModel
    Dim myVueAcc, myVueMap As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Groups")
        myVueAcc = Me.GetViewModel("Accounts")
        myVueMap = Me.GetViewModel("Map")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim ds As DataSet, dic As New clsCollecString(Of String), dt1 As DataTable, oBS As New clsMasterDataFICO(myContext)

        If Me.InitData(ds, "", oView, prepMode, prepIDX, strXML) Then
            dt1 = oBS.AccountGroupsTable("")

            myView.MainGrid.MainConf("showrownumber") = True
            myView.MainGrid.BindGridData(dt1.DataSet, 0)
            myView.MainGrid.QuickConf("", True, "1-6", , "Groups")

            dic.Add("acc", "select GLAccountID, GLAccGroupID, AccCode, AccName, SubLedgerType from GLAccount order by AccCode")
            dic.Add("map", "select accountmap.*, accountkey.accountkeyid  from accountmap inner join accountkey on accountkey.accountkey = accountmap.accountkey")
            ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

            myVueAcc.MainGrid.MainConf("showrownumber") = True
            myVueAcc.MainGrid.MainConf("sortcolsasc") = "AccCode"
            myVueAcc.MainGrid.BindGridData(ds, 0)
            myVueAcc.MainGrid.QuickConf("", True, "1-4-1", , "Accounts")

            myVueMap.MainGrid.MainConf("showrownumber") = True
            myVueMap.MainGrid.MainConf("defaultwidfact") = 1
            myVueMap.MainGrid.MainConf("hidecols") = "accountmapid,accountkeyid,sortindex"
            myVueMap.MainGrid.MainConf("sortcolsasc") = "GLAccountCode"
            myVueMap.MainGrid.BindGridData(ds, 1)
            myVueMap.MainGrid.QuickConf("", True, "1", , "Map")

            Me.FormPrepared = True

        End If
        Return Me.FormPrepared
    End Function
End Class
