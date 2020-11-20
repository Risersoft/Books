Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmGLAccGroupModel
    Inherits clsFormDataModel
    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String
        sql = "Select GLAccGroupID, GroupName from GLAccGroup Order by GroupName"
        Me.AddLookupField("pGLAccGroupID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GLAccGroup").TableName)

        sql = myFuncsBase.CodeWordSQL("Account", "GroupType", "")
        Me.AddLookupField("GroupType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GroupType").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Sql = "Select * from GLAccGroup where GLAccGroupID = " & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)

        Sql = "Select GLAccGroupID, pGLAccGroupID, SortNumber, GroupName, GroupType from GLAccGroup where pGLAccGroupID = " & frmIDX
        myView.MainGrid.QuickConf(Sql, True, "1-3-1")
        Dim str1 As String = "<BAND IDFIELD=""GLAccGroupID"" TABLE=""GLAccGroup"" INDEX=""0""><COL KEY=""GLAccGroupID, SortNumber""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myRow("GroupName").ToString.Trim.Length = 0 Then Me.AddError("GroupName", "Enter Group Name")
        If Me.SelectedRow("GroupType") Is Nothing Then Me.AddError("GroupType", "Select Group Type")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            GenerateSortNumber()
            Dim GLAccDescrip As String = "Name: " & myUtils.cStrTN(myRow("GroupName")) & ", Type: " & myRow("GroupType").ToString
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "GLAccGroupID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from GLAccGroup where GLAccGroupID = " & frmIDX)
                frmIDX = myRow("GLAccGroupID")

                Me.myView.MainGrid.SaveChanges(, "pGLAccGroupID = " & frmIDX)

                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(GLAccDescrip, frmIDX)
                VSave = True
            Catch ex As Exception
                myContext.Provider.dbConn.RollBackTransaction(GLAccDescrip, ex.Message)
                Me.AddError("", ex.Message)
            End Try
        End If
    End Function

    Private Sub GenerateSortNumber()
        If frmMode = EnumfrmMode.acAddM Then
            If myUtils.cValTN(myRow("pGlAccGroupID")) = 0 Then
                Dim Sql As String = "Select * from GlAccGroup where pGlAccGroupID is Null and GroupType = '" & myUtils.cStrTN(myRow("GroupType")) & "' Order by SortNumber Desc"
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                myRow("SortNumber") = ds.Tables(0).Rows(0)("SortNumber") + 1
            End If
        End If
    End Sub
End Class
