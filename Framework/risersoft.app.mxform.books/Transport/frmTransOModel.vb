Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmTransOModel
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

        sql = "Select UserID, FullName, JoinDate, LeaveDate from genListUser() order by FullName"
        Me.AddLookupField("BookedById", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BookedBy").TableName)

        sql = "Select CampusId, DispName, WODate,CompletedOn from mmlistCampus() order by DispName"
        Me.AddLookupField("CampusId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select UserID, FullName, JoinDate, LeaveDate from genListUser() where InSalesList = 1 order by FullName"
        Me.AddLookupField("UserID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "User").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acEditM Then
            Dim oRet As clsProcOutput = Me.GetRowLock(prepMode, "ODNoteID", prepIDX)
            If oRet.Success Then
                Dim Sql As String = "select * from ODNote where ODNoteID=" & myUtils.cValTN(prepIDX)
                Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)
                FormPrepared = True
            Else
                Me.AddError("", Nothing, 0, "", "", oRet.Message)
            End If
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.myRow("BookRefNum").ToString.Trim.Length = 0 Then Me.AddError("BookRefNum", "Please Enter Book Ref No.")
        If Me.myRow("FreightCharge").ToString.Trim.Length = 0 Then Me.AddError("FreightCharge", "Please Enter Freight Charge")
        If Me.myRow("Freighttons").ToString.Trim.Length = 0 Then Me.AddError("Freighttons", "Please Enter Freight Tons")
        If Me.myRow("HaltCharge").ToString.Trim.Length = 0 Then Me.AddError("HaltCharge", "Please Enter Halt Charge")
        If Me.myRow("HaltHours").ToString.Trim.Length = 0 Then Me.AddError("HaltHours", "Please Enter Halt Hours")
        If Me.myRow("TotalWt").ToString.Trim.Length = 0 Then Me.AddError("TotalWt", "Please Enter Total Wt.")
        If myUtils.NullNot(myRow("BookDate")) Then Me.AddError("BookDate", "Please Select Book Date")
        If myUtils.NullNot(myRow("VehSendOn")) Then Me.AddError("VehSendOn", "Please Select Vehicle Send On Date")

        If myUtils.NullNot(myRow("VehicleType")) Then Me.AddError("VehicleType", "Please Enter Vehicle Type")
        If Me.SelectedRow("BookedById") Is Nothing Then Me.AddError("BookedById", "Please Select Booked By")
        If Me.SelectedRow("CampusID") Is Nothing Then Me.AddError("CampusID", "Please Select Vehicle Send To")
        If Me.SelectedRow("TransporterId") Is Nothing Then Me.AddError("TransporterId", "Please Select Transporter")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim TransODescrip As String = " Transport Order for ODNote: " & myRow("VouchNum").ToString & ", Dt. " & Format(myRow("ChallanDate"), "dd-MMM-yyyy")
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "ODNoteID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from ODNote where ODNoteID=" & myUtils.cValTN(frmIDX))
                frmMode = EnumfrmMode.acEditM
                myContext.Provider.dbConn.CommitTransaction(TransODescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(TransODescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
