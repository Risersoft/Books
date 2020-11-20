Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmCostElemGroupListModel
    Inherits clsFormDataModel
    Dim myViewElem As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Groups")
        myViewElem = Me.GetViewModel("CostElem")
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
            dt1 = oBS.CostElemGroupTable("")

            myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""GroupType"" CAPTION=""Group Type""/><COL KEY=""GroupName"" CAPTION=""Group Name""/>"
            myView.MainGrid.MainConf("showrownumber") = True
            myView.MainGrid.BindGridData(dt1.DataSet, 0)
            myView.MainGrid.QuickConf("", True, "2-6", , "Cost Element Groups")

            dic.Add("elem", "select CostElementID, CostElemGroupID, CostElemCode, CostElemName from CostElement order by CostElemCode")
            ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

            myViewElem.MainGrid.MainConf("showrownumber") = True
            myViewElem.MainGrid.MainConf("sortcolsasc") = "CostElemCode"
            myViewElem.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostElemCode"" CAPTION=""Element Code""/><COL KEY=""CostElemName"" CAPTION=""Element Name""/>"
            myViewElem.MainGrid.BindGridData(ds, 0)
            myViewElem.MainGrid.QuickConf("", True, "2-4", , "Cost Elements")

            Me.FormPrepared = True
        End If
        Return Me.FormPrepared
    End Function
End Class
