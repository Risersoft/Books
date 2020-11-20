Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmCostingItemModel
    Inherits clsFormDataModel
    Dim myViewItem As clsViewModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String
        Sql = "select distinct subcatid, subcatname, unitname,hasdefscost from invlistitemsubcats() order by subcatname"
        Me.AddLookupField("subcatID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "subcat").TableName)

        Sql = "select itemunitid, unitname from itemunits order by unitname"
        Me.AddLookupField("ItemUnitID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "ItemUnit").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        If prepMode = EnumfrmMode.acEditM Then
            sql = "Select * from costingitems where costingitemid = " & prepIDX
            Me.InitData(sql, "subcatid", oView, prepMode, prepIDX, strXML, "costingitemid", "description")
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.CanSave Then
            Dim CostItemDescrip As String = " Costing Item Code: " & myUtils.cStrTN(myRow("CostItemCode")) & " Name: " & myUtils.cStrTN(myRow("CostItemName"))
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "costingitemid", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from costingitems where costingitemid = " & frmIDX)
                myContext.Provider.dbConn.CommitTransaction(CostItemDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(CostItemDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
