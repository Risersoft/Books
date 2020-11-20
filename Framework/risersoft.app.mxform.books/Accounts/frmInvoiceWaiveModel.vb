Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmInvoiceWaiveModel
    Inherits clsFormDataModel
    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Waive")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String
        sql = "Select CampusID, DispName, CompanyID, WODate, CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim objIVProcFico As New clsIVProcFICO("IS", myContext)

        Me.FormPrepared = False
        If prepIDX > 0 Then Me.FormPrepared = True Else Me.FormPrepared = False

        Dim sql As String = "Select *, 0.00 as PostBalance from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "InvoiceID", oView, prepMode, prepIDX, strXML)

        sql = " Select * from InvoiceWaive Where InvoiceID = " & prepIDX
        myView.MainGrid.QuickConf(sql, True, "1-1", True)
        Dim str1 As String = "<BAND IDFIELD=""InvoiceWaiveID"" TABLE=""InvoiceWaive"" INDEX=""0""><COL KEY=""InvoiceWaiveID, InvoiceID, Dated, Amount""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        objIVProcFico.PopulatePreBalanceWO(myRow.Row)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.myView.MainGrid.myDS.Tables(0).Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")
        If myUtils.cValTN(myRow("PostBalance")) < 0 Then Me.AddError("PostBalance", "Post Balance can not be less then Zero.")

        myView.MainGrid.CheckValid("Dated, Amount", "", , "<CHECK COND=""Dated &gt;='" & myUtils.cStrTN(myRow("InvoiceDate")) & "'"" MSG=""Invoice Date Can not be less then Waived Date.""/>")
        myView.MainGrid.CheckValid("", "", , "<CHECK COND=""Dated &lt;='" & Now.Date & "'"" MSG=""Waived Date Can not be greater then current date""/>")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean

        VSave = False
        If Me.Validate Then
            If Me.CanSave Then
                Dim InvoiceSaleDescrip As String = myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")
                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)

                    myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, "InvoiceID=" & frmIDX)
                    Me.myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDS.Tables(0), "Select InvoiceID, Dated, Amount from InvoiceWaive")

                    Dim objIVProcFico As New clsIVProcFICO("IS", myContext)
                    myRow("WOffAmount") = objIVProcFico.CalculateBalanceWO(myRow.Row)
                    Me.myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select InvoiceID, WOffAmount from Invoice")

                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(InvoiceSaleDescrip, frmIDX)
                    VSave = True

                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceSaleDescrip, e.Message)
                End Try
            End If
        End If
    End Function
End Class
