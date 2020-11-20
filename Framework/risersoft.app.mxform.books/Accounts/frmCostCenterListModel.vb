Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmCostCenterListModel
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
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Dim Sql As String = "Select * from Company where CompanyID = " & prepIDX
        Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)

        Dim dt1 As DataTable = CostCenterTable(myUtils.cValTN(myRow("CompanyID")))

        myView.MainGrid.MainConf("showrownumber") = True
        myView.MainGrid.BindGridData(dt1.DataSet, 0)
        myView.MainGrid.QuickConf("", True, "1-4-2-2", , "")
        myView.MainGrid.myDV.Table.AcceptChanges()

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Function CostCenterTable(CompanyID As Integer) As DataTable
        Dim sql As String = "select CostCenterID, sortnumber,pCostCenterID, 0 as sortindex,'' as ChildCostCenterIDs, Code, CostCenterName as Name, DispName as Campus, DepName as Department from AccListCostCenter() Where CompanyID = " & CompanyID & " order by pCostCenterID, SortNumber, CostCenterName"
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)


        Dim oProc As New clsListGeneratorBase(myContext)
        oProc.SetChildFilter(dt1, Nothing, "ChildCostCenterIDs", "pCostCenterID", "CostCenterID")
        dt1.AcceptChanges()
        '------copy rows as per sortindex so that bansort can be honoured in web apps.
        Dim dt2 As DataTable = dt1.Clone
        myUtils.CopyRows(dt1.Select("", "sortindex"), dt2)
        myUtils.MakeDSfromTable(dt2, False)
        Return dt2
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            Dim GLAccDescrip As String = "Name: " & myUtils.cStrTN(myRow("CompName")).ToString
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "CompanyID", frmIDX)

                myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDV.Table, "Select CostCenterID, SortNumber  from CostCenter", True)

                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(GLAccDescrip, frmIDX)
                VSave = True
            Catch ex As Exception
                myContext.Provider.dbConn.RollBackTransaction(GLAccDescrip, ex.Message)
                Me.AddError("", ex.Message)
            End Try
        End If
    End Function

    Public Overrides Function DeleteEntity(EntityKey As String, ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Select Case EntityKey.Trim.ToLower
                Case "costcenter"
                    oRet = Me.DeleteVoucher(ID, AcceptWarning)
            End Select

        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function

    Private Function DeleteVoucher(ID As Integer, AcceptWarning As Boolean) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim dic As New clsCollecString(Of String)
        dic.Add("CostCenter", "Select * from CostCenter Where CostCenterID = " & ID)
        Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

        dic = New clsCollecString(Of String)
        dic.Add("pCostCenter", "Select Count(*) as cnt from CostCenter Where pCostCenterID = " & ID)
        dic.Add("CostVouchItem", "Select Count(*) as cnt from CostVouchItem Where CostCenterID = " & ID)
        dic.Add("CostAssign", "Select Count(*) as cnt from CostAssign Where CostCenterID = " & ID)
        Dim ds1 As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        If ds.Tables("CostCenter").Rows.Count > 0 Then
            For Each dt1 As DataTable In ds1.Tables
                For Each rVouch As DataRow In dt1.Select
                    If myUtils.cValTN(rVouch("cnt")) > 0 Then
                        oRet.AddError("Can not delete. It is in use.")
                    End If
                Next
            Next
        Else
            oRet.AddError("Cannot find Record")
        End If

        If oRet.ErrorCount = 0 Then
            If AcceptWarning Then
                Try
                    Dim sql As String = "Delete from CostCenter where CostCenterID = " & ID
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql)
                    oRet.Message = "Deleted Successfully."
                Catch ex As Exception
                    oRet.AddException(ex)
                End Try
            ElseIf oRet.WarningCount = 0 Then
                oRet.AddWarning("Are you sure ?" & vbCrLf & "Do you want to Delete ?")
            End If
        End If
        Return oRet
    End Function
End Class