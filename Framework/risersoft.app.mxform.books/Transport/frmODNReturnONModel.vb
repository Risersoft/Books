Imports risersoft.shared
Imports System.Runtime.Serialization
<DataContract>
Public Class frmODNReturnONModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String = "Select UserID, FullName, JoinDate, LeaveDate, isdeleted from genListUser() order by FullName"
        Me.AddLookupField("ReceivedByID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "User").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acEditM Then
            Dim oRet As clsProcOutput = Me.GetRowLock(prepMode, "ODNoteID", prepIDX)
            If oRet.Success Then
                Dim Sql As String = "Select * from OdNote where ODNoteID = " & myUtils.cValTN(prepIDX)
                Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)
                If frmMode = EnumfrmMode.acAddM Then myRow("ReturnedOn") = Now.Date

                Me.FormPrepared = True
            Else
                Me.AddError("", Nothing, 0, "", "", oRet.Message)
            End If
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If myRow("ReturnedOn") Is Nothing Then Me.AddError("ReturnedOn", "Select Returned On")
        If Me.SelectedRow("ReceivedByID") Is Nothing Then Me.AddError("ReceivedByID", "Please Select Received By")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim ChallanDescrip As String = " Challan No: " & myRow("ChallanNum").ToString & ", RetOn. " & Format(myRow("ReturnedOn"), "dd-MMM-yyyy")
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "OdNoteID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                frmMode = EnumfrmMode.acEditM
                frmIDX = myRow("OdNoteID")
                myContext.Provider.dbConn.CommitTransaction(ChallanDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(ChallanDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class