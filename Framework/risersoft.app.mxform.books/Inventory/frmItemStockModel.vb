Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmItemStockModel
    Inherits clsFormDataModel
    Dim myViewItems As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("SBHead")
        myViewItems = Me.GetViewModel("Items")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        Dim oBS As New clsMatStock(myContext)

        Me.dsForm = oBS.StockStatement(1, Now.Date, "IT", "")
        myView.MainGrid.MainConf("showrownumber") = True
        myView.MainGrid.BindGridData(dsForm, 0)
        myView.MainGrid.QuickConf("", True, "6-1-1-1-1-2", , "SB Head")

        myViewItems.MainGrid.MainConf("showrownumber") = True
        myViewItems.MainGrid.BindGridData(dsForm, 1)
        myViewItems.MainGrid.QuickConf("", True, "1-1-1-1-2-6-1-1-1-1-1-1", , "Items")

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function
End Class
