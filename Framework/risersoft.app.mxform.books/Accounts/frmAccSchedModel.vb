Imports risersoft.shared
Imports System.Runtime.Serialization

<DataContract>
Public Class frmAccSchedModel
    Inherits clsFormDataModel
    Protected Overrides Sub InitViews()
    End Sub
    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim vlist As New clsValueList
        vlist.Add(0, "False")
        vlist.Add(1, "True")
        Me.ValueLists.Add("BillByBill", vlist)
        Me.AddLookupField("BillByBill", "BillByBill")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0

        sql = "select * from SchedsAcc where AccSchedID =" & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myRow("SchedName").ToString.Trim.Length = 0 Then Me.AddError("SchedName", "Enter Schedule Name")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim SchedDescrip As String = myUtils.cStrTN(myRow("SchedName"))
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "AccSchedID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "select * from SchedsAcc where AccSchedID = " & frmIDX)
                frmIDX = myRow("AccSchedID")
                myContext.Provider.dbConn.CommitTransaction(SchedDescrip, frmIDX)
                frmMode = EnumfrmMode.acEditM
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(SchedDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
