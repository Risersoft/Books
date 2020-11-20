Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmCostElementModel
    Inherits clsFormDataModel
    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = myFuncsBase.CodeWordSQL("GLAccount", "SLType", "")
        Me.AddLookupField("SubLedgerType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SubLedgerType").TableName)

        sql = myFuncsBase.CodeWordSQL("CostVouch", "ElemType", "")
        Me.AddLookupField("CostElemType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "CostElemType").TableName)

        sql = "Select CostElemGroupID, GroupName from CostElemGroup  Order by GroupName"
        Me.AddLookupField("CostElemGroupID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "CostElemGroup").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from CostElement where CostElementID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        sql = "Select GlAccountID, AccName + ' (' + convert(Varchar,AccCode) +')'  from accListGLAccount() Where GroupType in ('I','E') and GlAccountID Not in (Select GlAccountID from CostElement where CostElementID <> " & myUtils.cValTN(frmIDX) & ")   Order by AccName"
        Me.AddLookupField("GlAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GlAccount").TableName)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myRow("CostElemName").ToString.Trim.Length = 0 Then Me.AddError("CostElemName", "Enter Element Name")
        If Me.myRow("CostElemCode").ToString.Trim.Length = 0 Then Me.AddError("CostElemCode", "Enter Element Code")
        If Me.SelectedRow("CostElemType") Is Nothing Then Me.AddError("CostElemType", "Select a Element Type")
        If Me.SelectedRow("CostElemGroupID") Is Nothing Then Me.AddError("CostElemGroupID", "Select a Group Name")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim GLAccDescrip As String = "Code: " & myRow("CostElemCode").ToString & ", Name: " & myRow("CostElemName").ToString
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CostElementID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from CostElement where CostElementID = " & frmIDX)
                frmIDX = myRow("CostElementID")
                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(GLAccDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(GLAccDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
