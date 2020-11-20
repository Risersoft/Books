Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmGLAccountModel
    Inherits clsFormDataModel
    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select AccSchedID, SchedName from SchedsAcc Order by SchedName"
        Me.AddLookupField("AccSchedID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AccSched").TableName)

        sql = "Select GLAccGroupID, GroupName from GLAccGroup  Order by GroupName"
        Me.AddLookupField("GLAccGroupID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GLAccGroup").TableName)

        sql = myFuncsBase.CodeWordSQL("GLAccount", "SLType", "")
        Me.AddLookupField("SubLedgerType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SubLedgerType").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from GLAccount where GLAccountID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "GLAccountID", myUtils.cValTN(myRow("GLAccountID"))), GetType(Boolean), False))

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myRow("AccName").ToString.Trim.Length = 0 Then Me.AddError("AccName", "Enter Account Name")
        If Me.myRow("AccCode").ToString.Trim.Length = 0 Then Me.AddError("AccCode", "Enter Account Code")
        If Me.SelectedRow("AccSchedID") Is Nothing Then Me.AddError("AccSchedID", "Select an accounting schedule")
        If Me.SelectedRow("GLAccGroupID") Is Nothing Then Me.AddError("GLAccGroupID", "Select a financial group")
        If myUtils.NullNot(myRow("IsClearingAcc")) Then Me.AddError("IsClearingAcc", "Select a Accounting Type")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim GLAccDescrip As String = "Code: " & myRow("AccCode").ToString & ", Name: " & myRow("AccName").ToString
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "GLAccountID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from GLAccount where GLAccountID = " & frmIDX)
                frmIDX = myRow("GLAccountID")
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
