Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmItemSBHeadListModel
    Inherits clsFormDataModel
    Dim myViewCat As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("SBHead")
        myViewCat = Me.GetViewModel("SubCategories")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim sql As String, dt1, dt2 As DataTable, oBS As New clsMasterDataMM(myContext)
        dt1 = oBS.ItemSBHeadTable

        myUtils.MakeDSfromTable(dt1, False)
        myView.MainGrid.MainConf("showrownumber") = True
        myView.MainGrid.BindGridData(dt1.DataSet, 0)
        myView.MainGrid.QuickConf("", True, "6-2", , "SB Head")

        sql = "select SubCatID, ItemSBHeadID, InitialCode, SubCatName from ItemSubcats order by InitialCode"
        dt2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)

        myViewCat.MainGrid.MainConf("showrownumber") = True
        myViewCat.MainGrid.BindGridData(dt2.DataSet, 0)
        myViewCat.MainGrid.QuickConf("", True, "1-4", , "Sub Categories")

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function
End Class
