Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmInvoicePurchAllocModel
    Inherits clsFormDataModel
    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Advance")
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

        sql = "SELECT  VendorID, VendorName, VendorClass FROM  purListVendor() Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim objIVProcFico As New clsIVProcFICO("IP", myContext)
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepIDX > 0 Then Me.FormPrepared = True Else Me.FormPrepared = False

        sql = "Select *, 0.00 as PostBalance from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        sql = " Select PaymentItemID, Payment.PaymentID, InvoiceID, OpenAmountAdj, NewAmount, PaymentItemType, VouchNum, Dated, PaymentInfo, 0.00 as PreBalance, PaymentItem.Amount, PaymentItem.TDSAmount, PaymentItem.WCTAmount, 0.00 as Balance from PaymentItem Inner join Payment on PaymentItem.PaymentID = Payment.PaymentID Where InvoiceID = " & frmIDX & " and PaymentItemType = 'IP'"
        myView.MainGrid.QuickConf(sql, True, "1-1-2-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY ="" PaymentID, InvoiceID, PaymentItemType, Amount, TDSAmount""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)
        objIVProcFico.PopulatePreBalanceAdv(myView.MainGrid.myDS.Tables(0).Select, frmIDX, Now.Date)

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))

        objIVProcFico.PopulatePreBalanceDue(myRow.Row)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")


        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")


        If Not myFuncs.CheckColumnAmount(myView.MainGrid.myDS.Tables(0), {"Amount", "Balance", "TDSAmount", "WCTAmount"}) Then Me.AddError("", "Negative Amount Not Allowed.")

        If myUtils.cValTN(myRow("PostBalance")) < 0 Then Me.AddError("PostBalance", "Post Balance can not be less then Zero.")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("InvoiceNum", "Accounting entry passed. Invoice can't be update.")
            End If
            If Me.CanSave Then
                Dim InvoicePurchDescrip As String = " Allocation for: " & myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")
                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, "PaymentItemType=IP")
                    myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, "InvoiceID =" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDS.Tables(0), "Select PaymentID, InvoiceID, PaymentItemType, Amount, TDSAmount,WCTAmount from PaymentItem")

                    Dim objIVProcFico As New clsIVProcFICO("IP", myContext)
                    Dim Oret As clsProcOutput = objIVProcFico.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoicePurchDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, Oret.Message)
                        Me.AddError("", Oret.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoicePurchDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "payment"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("VendorID = @vendorid and CompanyID = @companyid and isNull(BalanceAmount,0) > 0 and PaymentID Not in (@paymentidcsv) and Dated <= '@dated' and IsProcessed = 1 and PaymentType <> 'R'", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PaymentID""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("ID", Params))
        Dim Dated As DateTime = myUtils.cDateTN(myContext.SQL.ParamValue("Dated", Params), Now.Date)

        Select Case DataKey.Trim.ToLower
            Case "generateprebal"
                Dim objIVProcFico As New clsIVProcFICO("IP", myContext)
                objIVProcFico.PopulatePreBalanceAdv(dt.Select, ID, Dated)
        End Select
    End Sub
End Class
