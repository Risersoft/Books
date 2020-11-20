Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmQuoteModel
    Inherits clsFormDataModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Quote")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim Sql As String
        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acEditM Then
            Dim oRet As clsProcOutput = Me.GetRowLock(prepMode, "ODNoteID", prepIDX)
            If oRet.Success Then
                Sql = "select * from ODNote where ODNoteID=" & myUtils.cValTN(prepIDX)
                Me.InitData(Sql, "", oView, prepMode, prepIDX, strXML)
                myView.MainGrid.QuickConf("select transportquoteid,ODNoteID,transporterid,charges,remark from transportquote where TransportQuoteId is Not Null and ODNoteID=" & myUtils.cValTN(prepIDX), True, "5-1-4")
                Sql = "select VendorID, VendorName from purListVendor() where VendorType = 'TR'  ORDER BY VendorName"
                myView.MainGrid.PrepEdit("<BAND TABLE=""transportquote"" IDFIELD=""transportquoteid"" INDEX=""0""><COL KEY=""ODNoteid"" /><COL KEY=""transporterid"" LOOKUPSQL=""" & Sql & """ CAPTION=""Name of Transporter""/><COL KEY =""charges"" CAPTION=""Charges""/><COL KEY=""remark"" CAPTION=""Remarks""/></BAND>", EnumEditType.acCommandAndEdit)
                Me.FormPrepared = True
            Else
                Me.AddError("", Nothing, 0, "", "", oRet.Message)
            End If
        End If
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.myRow("VehicleType").ToString.Trim.Length = 0 Then Me.AddError("VehicleType", "Please Enter Vehicle No.")
        If Me.myRow("StationFrom").ToString.Trim.Length = 0 Then Me.AddError("StationFrom", "Please Enter From Station")
        If Me.myRow("StationTo").ToString.Trim.Length = 0 Then Me.AddError("StationTo", "Please Enter To Station")
        If Me.myRow("QuoteKvaQty").ToString.Trim.Length = 0 Then Me.AddError("QuoteKvaQty", "Please Enter KV/Qualitiy of T/F")
        If myUtils.NullNot(myRow("QuoteDate")) Then Me.AddError("QuoteDate", "Please Select Quote Date")

        If myView.MainGrid.myDS.Tables(0).Select.Length > 0 Then
            myView.MainGrid.CheckValid("TransporterID")
            myView.MainGrid.CheckValid("", , , "<CHECK COND=""charges>0"" MSG=""Charges should be greater than 0""/>")
        Else
            Me.AddError("", "Enter Challan Item Detail")
        End If
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim QuoteDescrip As String = " Comparative Statement of Transporter for ODNote No: " & myRow("VouchNum").ToString & " Dt. " & Format(myRow("ChallanDate"), "dd-MMM-yyyy")
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "ODNoteid", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, sqlForm)
                frmIDX = myRow("ODNoteid")
                frmMode = EnumfrmMode.acEditM
                myView.MainGrid.SaveChanges(, "ODNoteid=" & frmIDX)
                myContext.Provider.dbConn.CommitTransaction(QuoteDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(QuoteDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
