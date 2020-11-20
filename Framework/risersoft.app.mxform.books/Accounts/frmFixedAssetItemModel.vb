Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmFixedAssetItemModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String
        sql = myFuncsBase.CodeWordSQL("Asset", "EntryType", "(CodeWord = 'T')")
        Me.AddLookupField("EntryType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "EntryType").TableName)

        sql = myFuncsBase.CodeWordSQL("Asset", "Type", "")
        Me.AddLookupField("AssetType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AssetType").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, ObjFixedAssetTransProc As New clsFixedAssetTransProc(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select FixedAssetItemID,FixedAssetID,EntryType,Dated,PostPeriodID,CompanyID,InvoiceItemID,PaymentItemTransID,NewFixedAssetID,Qty,Amount,AssetName,AssetType,AssetClass from accListFixedAssetItem() where FixedAssetItemID = " & myUtils.cValTN(prepIDX)
        Me.InitData(sql, "FixedAssetID", oView, prepMode, prepIDX, strXML)

        PPFinal = ObjFixedAssetTransProc.oMasterFICO.IsFinal(myUtils.cValTN(myRow("PostPeriodID")))
        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
            If Me.dsCombo.Tables("entrytype").Rows.Count = 1 Then myRow("entrytype") = "T"
        End If

        ObjFixedAssetTransProc.LoadVouch(myUtils.cValTN(myRow("FixedAssetItemID")), myUtils.cValTN(myRow("FixedAssetID")))
        Dim rFA As DataRow = myFuncsBase.rEntity(myRow("fixedassetid"), "fixedassetid", ObjFixedAssetTransProc.dsAsset.Tables("fa"))
        If Not rFA Is Nothing Then
            myRow("AssetName") = myUtils.cStrTN(rFA("AssetName"))
            myRow("AssetType") = myUtils.cStrTN(rFA("AssetType"))
            myRow("AssetClass") = myUtils.cStrTN(rFA("AssetClass"))
            myRow("CompanyID") = myUtils.cValTN(rFA("CompanyID"))
            If myUtils.IsInList(myUtils.cStrTN(myRow("entrytype")), "T") Then
                If ObjFixedAssetTransProc.dsAsset.Tables("Item").Select("entrytype = 'T' and FixedAssetItemID <> " & frmIDX & "").Length = 0 Then
                    Dim str3 As String = ObjFixedAssetTransProc.SQLWhere(rFA, myRow.Row)

                    sql = "Select FixedAssetID,  AssetNumber + ' | ' + AssetName from AccListFixedAsset() where " & str3
                    Me.AddLookupField("NewFixedAssetID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "NewFixedAsset").TableName)
                    Me.FormPrepared = True
                Else
                    Me.AddError("AssetName", "Multiple Transfer entry cannot be Allow")
                End If
            Else
                Me.FormPrepared = True
            End If
        End If

        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.myRow("EntryType") Is Nothing Then Me.AddError("EntryType", "Please select Entry Type")
        If myUtils.IsInList(myRow("entrytype"), "T") AndAlso Me.SelectedRow("NewFixedAssetID") Is Nothing Then Me.AddError("NewFixedAssetID", "Please select Fixed Asset")
        If myUtils.NullNot(myRow("Dated")) Then Me.AddError("Dated", "Please select Date")

        myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(myRow("CompanyID")), Me.myRow("Dated"), PPFinal)
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")

        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            Dim ObjFixedAssetTransProc As New clsFixedAssetTransProc(myContext)
            ObjFixedAssetTransProc.LoadVouch(myUtils.cValTN(myRow("FixedAssetItemID")), myUtils.cValTN(myRow("FixedAssetID")))
            ObjFixedAssetTransProc.GenerateVouchDelta(myRow.Row.Table)
            Dim oRet As clsProcOutput = ObjFixedAssetTransProc.CheckBalance()
            If Not oRet.Success Then Me.AddError("Dated", oRet.Message)
            If Me.CanSave Then
                Dim EntryTypeDescrip As String = Me.SelectedRow("EntryType")("Descrip")
                Dim AssetItemDescrip As String = " Transfer for EntryType: " & EntryTypeDescrip & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy")
                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "FixedAssetItemID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select FixedAssetItemID,FixedAssetID,EntryType,Dated,PostPeriodID,InvoiceItemID,PaymentItemTransID,NewFixedAssetID,Qty,Amount from FixedAssetItem where FixedAssetItemID = " & frmIDX)

                    oRet = ObjFixedAssetTransProc.HandleWorkflowState(myRow.Row)
                    If oRet.Success Then
                        frmMode = EnumfrmMode.acEditM
                        frmIDX = myRow("FixedAssetItemID")
                        myContext.Provider.dbConn.CommitTransaction(AssetItemDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(AssetItemDescrip, oRet.Message)
                        Me.AddError("", oRet.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(AssetItemDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function
End Class
