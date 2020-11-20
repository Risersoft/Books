Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmFixedAssetModel
    Inherits clsFormDataModel
    Dim myViewDelta As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        myViewDelta = Me.GetViewModel("Delta")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, CompanyID, WODate,CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select Class from AccountClass Where ClassType = 'A'"
        Me.AddLookupField("AssetClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AssetClass").TableName)

        sql = myFuncsBase.CodeWordSQL("Asset", "Type", "")
        Me.AddLookupField("AssetType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AssetType").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String
        Dim ObjFixedAssetProc As New clsFixedAssetProc(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from FixedAsset Where FixedAssetID = " & prepIDX
        Me.InitData(sql, "AssetType", oView, prepMode, prepIDX, strXML)

        myView.MainGrid.BindGridData(GenerateData("asset", frmIDX), 0)
        myView.MainGrid.QuickConf("", True, "1-2-2-2-2", True)

        myViewDelta.MainGrid.BindGridData(GenerateData("delta", frmIDX), 0)
        myViewDelta.MainGrid.QuickConf("", True, "1-2-2-2-2-2", True)

        ObjFixedAssetProc.LoadVouch(myUtils.cValTN(myRow("FixedAssetID")))

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusID") Is Nothing Then Me.AddError("CampusID", "Please select Campus")
        If Me.SelectedRow("AssetClass") Is Nothing Then Me.AddError("AssetClass", "Please select Asset Class")
        If Me.SelectedRow("AssetType") Is Nothing Then Me.AddError("AssetType", "Please select Asset Type")
        If myUtils.NullNot(Me.myRow("AssetName")) Then Me.AddError("AssetName", "Please select Asset Name")
        If myUtils.NullNot(Me.myRow("PurchDate")) Then Me.AddError("PurchDate", "Please select Purchase Date")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim ObjFixedAssetProc As New clsFixedAssetProc(myContext)
            ObjFixedAssetProc.LoadVouch(myUtils.cValTN(myRow("FixedAssetID")))
            ObjFixedAssetProc.GenerateVouchDelta(myRow.Row.Table)
            Dim oRet As clsProcOutput = ObjFixedAssetProc.CheckBalance()
            If Not oRet.Success Then Me.AddError("", oRet.Message)
            If Me.CanSave Then
                Dim ObjVouch As New clsVoucherNum(myContext)
                ObjVouch.GetNewSerialNo("FixedAssetID", "", myRow.Row)
                Dim AssetDescrip As String = myRow("AssetName").ToString
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "FixedAssetID", frmIDX)
                    ObjFixedAssetProc.UpdateBalance()
                    frmIDX = myRow("FixedAssetID")
                    myContext.Provider.dbConn.CommitTransaction(AssetDescrip, frmIDX)
                    frmMode = EnumfrmMode.acEditM
                    myViewDelta.RefreshGrid()
                    VSave = True
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(AssetDescrip, oRet.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateIDOutput(dataKey As String, ID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "asset"
                oRet.Data = GenerateData("asset", ID)
            Case "delta"
                oRet.Data = GenerateData("delta", ID)
            Case "delete"
                Dim ObjFixedAssetTransProc As New clsFixedAssetTransProc(myContext)
                oRet = ObjFixedAssetTransProc.TryDelete(ID)
        End Select
        Return oRet
    End Function

    Protected Overrides Function GenerateData(DataKey As String, ID As String) As DataSet
        Dim oRet As New clsProcOutput
        Select Case DataKey.Trim.ToLower
            Case "asset"
                Dim sql As String = "Select FixedAssetItemID, FixedAssetID, PostPeriodID, EntryType, EntryTypeDescrip, VouchDate, VouchNum, Amount from accListFixedAssetItem() " &
                                    " Where EntryType In ('T') and FixedAssetID = " & myUtils.cValTN(ID) & " Order By Dated, FixedAssetItemID"
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Case "delta"
                Dim sql As String = "Select FixedAssetDeltaID, FixedAssetID, FixedAssetItemID, PostPeriodID,MatVouchItemID, DeltaType, DeltaTypeDescrip, VouchDate, VouchNum, Amount, SumAmount from accListFixedAssetDelta() where FixedAssetID = " & myUtils.cValTN(ID) & " Order By Dated"
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
        End Select
        Return oRet.Data
    End Function

End Class