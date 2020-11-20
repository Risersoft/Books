Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmEvalModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Dim Sql As String = "select * from TransportEval where TransportEvalID=" & myUtils.cValTN(prepIDX)
        Me.InitData(Sql, "MainPartyID", oView, prepMode, prepIDX, strXML)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.myRow("Area").ToString.Trim.Length = 0 Then Me.AddError("Area", "Please Enter Area")
        If Me.myRow("GoodsType").ToString.Trim.Length = 0 Then Me.AddError("GoodsType", "Please Enter Type of Goods Handle")
        If Me.myRow("ArrangeTime").ToString.Trim.Length = 0 Then Me.AddError("ArrangeTime", "Please Enter Lead Time For Arrange Vehicle")
        If myUtils.NullNot(myRow("Dated")) Then Me.AddError("Dated", "Please Select Date")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim Sql As String = "Select PartyID, MainPartyID, PartyName from Party where MainPartyID = " & myRow("MainPartyID")
            Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
            Dim EvalDescrip As String = " Evaluation for PartyName: " & myUtils.cStrTN(ds.Tables(0).Rows(0)("PartyName")) & ", Dt: " & Format(myRow("Dated"), "dd-MMM-yyyy")
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "TransportEvalID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, sqlForm)

                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(EvalDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(EvalDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
