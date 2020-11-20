Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmTransUtilModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "SELECT  VendorID, VendorName FROM  PurListVendor() where VendorType = 'TR' Order By VendorName"
        Me.AddLookupField("TransporterId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Transporter").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acEditM Then
            Dim oRet As clsProcOutput = Me.GetRowLock(prepMode, "ODNoteID", prepIDX)
            If oRet.Success Then
                Dim Sql As String = "select * from ODNote where ODNoteID=" & myUtils.cValTN(prepIDX)
                Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)
                Me.FormPrepared = True
            Else
                Me.AddError("", Nothing, 0, "", "", oRet.Message)
            End If
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myRow("VehicleNum").ToString.Trim.Length = 0 Then Me.AddError("VehicleNum", "Please Enter Vehicle No.")

        If Not myUtils.NullNot(myRow("BookDate")) Then
            If Me.myRow("GRNum").ToString.Trim.Length = 0 Then Me.AddError("GRNum", "Please Enter GR No.")
            If myUtils.NullNot(myRow("GRDate")) Then Me.AddError("GRDate", "Please Select GR Date")
            If myUtils.NullNot(myRow("UtilDate")) Then Me.AddError("UtilDate", "Please Select Utilization Date")
            If myUtils.NullNot(myRow("vehArrivedOn")) Then Me.AddError("vehArrivedOn", "Please Select Arrived Date")
            If myUtils.NullNot(myRow("vehDepartedOn")) Then Me.AddError("vehDepartedOn", "Please Select Departed Date")
        End If
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim TransUtilDescrip As String = " Transport Utilization for ODNote: " & myRow("VouchNum").ToString & ", Dt. " & Format(myRow("ChallanDate"), "dd-MMM-yyyy")
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "ODNoteID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from ODNote where ODNoteID=" & myUtils.cValTN(frmIDX))
                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(TransUtilDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(TransUtilDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
