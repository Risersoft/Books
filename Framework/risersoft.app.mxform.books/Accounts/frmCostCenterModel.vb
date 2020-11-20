Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmCostCenterModel
    Inherits clsFormDataModel
    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("CostCenter")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String
        sql = "Select CostCenterID, CostCenterName from CostCenter Order by CostCenterName"
        Me.AddLookupField("pCostCenterID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "pCostCenter").TableName)

        sql = "Select CompanyID, CompName  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = "Select CampusID, DispName, CompanyID from mmlistCampus() Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select DepID, DepName, CompanyID from Deps Order by DepName"
        Me.AddLookupField("DepID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Department").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Sql = "Select * from CostCenter where CostCenterID = " & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)

        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostCenterName"" CAPTION=""Name""/><COL KEY=""SortNumber"" CAPTION=""Sort Number""/>"
        Sql = "Select CostCenterID, PCostCenterID, SortNumber,CostCenterCode,CostCenterName from CostCenter where pCostCenterID = " & frmIDX
        myView.MainGrid.QuickConf(Sql, True, "1-1-3")
        Dim str1 As String = "<BAND IDFIELD=""CostCenterID"" TABLE=""CostCenter"" INDEX=""0""><COL KEY=""CostCenterID, SortNumber""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myRow("CostCenterName").ToString.Trim.Length = 0 Then Me.AddError("CostCenterName", "Enter Cost Center Name")
        If Me.myRow("CostCenterCode").ToString.Trim.Length = 0 Then Me.AddError("CostCenterCode", "Enter Cost Center Code")
        If Me.SelectedRow("CompanyID") Is Nothing Then Me.AddError("CompanyID", "Please select Company")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            GenerateSortNumber()
            Dim GLAccDescrip As String = "Name: " & myUtils.cStrTN(myRow("CostCenterName")).ToString
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CostCenterID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from CostCenter where CostCenterID = " & frmIDX)
                frmIDX = myRow("CostCenterID")

                Me.myView.MainGrid.SaveChanges(, "pCostCenterID = " & frmIDX)

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
            If myUtils.cValTN(myRow("pCostCenterID")) = 0 Then
                Dim Sql As String = "Select * from CostCenter where pCostCenterID is Null Order by SortNumber Desc"
                Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                If ds.Tables(0).Rows.Count = 0 Then
                    myRow("SortNumber") = 1
                Else
                    myRow("SortNumber") = ds.Tables(0).Rows(0)("SortNumber") + 1
                End If
            End If
        End If
    End Sub
End Class
