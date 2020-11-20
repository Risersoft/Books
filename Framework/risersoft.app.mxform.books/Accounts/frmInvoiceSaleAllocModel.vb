Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmInvoiceSaleAllocModel
    Inherits clsFormDataModel
    Dim myViewWC As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Advance")
        myViewWC = Me.GetViewModel("WC")
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

        sql = "SELECT  CustomerID, CustDescrip, CustomerClass FROM  slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String, objIVProcFico As New clsIVProcFICO("IS", myContext)

        Me.FormPrepared = False
        If prepIDX > 0 Then Me.FormPrepared = True Else Me.FormPrepared = False

        sql = "Select *, 0.00 as PostBalance from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""\D\r #,#00.00; \C\r #,#00.00;\Z\e\r\o""/>"
        sql = " Select PaymentItemID, PaymentID, InvoiceID, OpenAmountAdj, NewAmount, PaymentItemType, VouchNum, Dated, PaymentInfo, 0.00 as PreBalance, Amount, TDSAmount, WCTAmount, 0.00 as Balance from accListPaymentItem() Where InvoiceID = " & prepIDX & " and PaymentItemType = 'IP'"
        myView.MainGrid.QuickConf(sql, True, "1-1-2-1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItem"" IDFIELD=""PaymentItemID""><COL KEY ="" PaymentID, InvoiceID, PaymentItemType, Amount, TDSAmount""/><COL KEY=""WCTAmount"" CAPTION=""GST-TDS""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)
        objIVProcFico.PopulatePreBalanceAdv(myView.MainGrid.myDS.Tables(0).Select, frmIDX, Now.Date)

        myViewWC.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Balance"" FORMAT=""\D\r #,#00.00; \C\r #,#00.00;\Z\e\r\o""/>"
        sql = "Select InvoiceItemWCID, InvoiceID, PurchInvoiceID, DocType, SalesOrderID, openamountwcalloc, amountTot, InvoiceNum, InvoiceDate, 0.00 as PreBalance, Amount, 0.00 as Balance from accListInvoiceItemWC()  Where InvoiceID = " & prepIDX & ""
        myViewWC.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""InvoiceItemWC"" IDFIELD=""InvoiceItemWCID""><COL KEY ="" InvoiceID, PurchInvoiceID, Amount""/></BAND>"
        myViewWC.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)
        objIVProcFico.PopulatePreBalanceWC(myViewWC.MainGrid.myDS.Tables(0).Select, frmIDX, Now.Date)

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))

        objIVProcFico.PopulatePreBalanceDue(myRow.Row)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("CustomerID") Is Nothing Then Me.AddError("CustomerID", "Please select Customer")


        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")

        If Not myFuncs.CheckColumnAmount(myView.MainGrid.myDS.Tables(0), {"Amount", "Balance", "TDSAmount", "WCTAmount"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewWC.MainGrid.myDS.Tables(0), {"Amount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")
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
                Dim InvoiceSaleDescrip As String = myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, "PaymentItemType=IP")
                    Me.myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDS.Tables(0), "Select PaymentID, InvoiceID, PaymentItemType, Amount, TDSAmount, WCTAmount from PaymentItem")

                    Me.myContext.Provider.objSQLHelper.SaveResults(myViewWC.MainGrid.myDS.Tables(0), "Select InvoiceItemWCID, InvoiceID, PurchInvoiceID, Amount  from InvoiceItemWC")

                    Dim objIVProcFico As New clsIVProcFICO("IS", myContext)
                    Dim Oret As clsProcOutput = objIVProcFico.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoiceSaleDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoiceSaleDescrip, Oret.Message)
                        Me.AddError("", Oret.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceSaleDescrip, e.Message)
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
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CustomerID = @customerid and CompanyID = @companyid and isnull(BalanceAmount,0) > 0 and Dated <= '@dated' and PaymentID Not in (@paymentidcsv) and IsProcessed = 1 and PaymentType <> 'R'", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PaymentID""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "invoice"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid and InvoiceID Not in (@purchinvoiceidcsv) and isNull(WCBalance,0) > 0", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""InvoiceID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim objIVProcFico As New clsIVProcFICO("IS", myContext)
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("ID", Params))
        Dim Dated As DateTime = myUtils.cDateTN(myContext.SQL.ParamValue("Dated", Params), Now.Date)
        Select Case DataKey.Trim.ToLower
            Case "generateprebal"
                objIVProcFico.PopulatePreBalanceAdv(dt.Select, ID, Dated)
            Case "generateprebalwc"
                objIVProcFico.PopulatePreBalanceWC(dt.Select, ID, Dated)
        End Select
    End Sub
End Class
