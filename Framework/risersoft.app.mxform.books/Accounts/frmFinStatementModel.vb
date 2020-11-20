Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmFinStatementModel
    Inherits clsFormDataModel
    Dim myVueAcc As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Groups")
        myVueAcc = Me.GetViewModel("Accounts")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CompanyID, CompName  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False

        Me.dsForm = Me.CreateStatement(myUtils.cValTN(Me.dsCombo.Tables("Company").Rows(0)("CompanyID")), Now.Date)

        myView.MainGrid.MainConf("showrownumber") = True
        myView.MainGrid.BindGridData(Me.dsForm, 0)
        myView.MainGrid.QuickConf("", True, "1-1-6-3", , "Groups")

        myVueAcc.MainGrid.MainConf("showrownumber") = True
        myVueAcc.MainGrid.BindGridData(Me.dsForm, 1)
        myVueAcc.MainGrid.QuickConf("", True, "1-4-1-3", , "Accounts")

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.CanSave Then
            Try
            Catch e As Exception
                Me.AddException("", e)
            End Try
        End If
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "generate"
                    Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@companyid", Params))
                    oRet.Data = Me.CreateStatement(CompanyID, Now.Date)
            End Select
        End If
        Return oRet
    End Function
    Public Function CreateStatement(CompanyID As Integer, Dated As DateTime) As DataSet
        Dim oAccBS As New clsAccBS(myContext)
        Dim strFilter = "GroupType IN ('A','L')"
        Dim ds = oAccBS.GenerateAdjustedTrial(CompanyID, Dated, "isnull(AdjustType,'')=''")
        Dim ds2 = oAccBS.FinancialStatement(ds.Tables("fintrial"), 0, oAccBS.oMasterFICO.AccountGroupsTable(strFilter), "GLAccGroupID")
        Return ds2
    End Function
End Class
