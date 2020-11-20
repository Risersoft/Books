Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmInvoiceOpenModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, CompanyID, DivisionCodeList, WODate, CompletedOn, CampusCode from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("deliverycampusid", "Campus")

        sql = "SELECT  VendorID, VendorName, VendorClass FROM  PurListVendor() Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "SELECT  CustomerID, CustDescrip, CustomerClass FROM  slsListCustomer()  Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)


        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objIVProcFICO As clsIVProcFICO

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "DocType", oView, prepMode, prepIDX, strXML)

        If Not myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "") Then
            objIVProcFICO = New clsIVProcFICO(myRow("DocType"), myContext)
            Me.FormPrepared = True
        Else
            Me.FormPrepared = False
        End If

        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objIVProcFICO.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If
        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "IP") AndAlso Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Select a Vendor")
        If myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "IS") AndAlso Me.SelectedRow("CustomerID") Is Nothing Then Me.AddError("CustomerID", "Select a Customer")
        If myRow("InvoiceNum").ToString.Trim.Length = 0 Then Me.AddError("InvoiceNum", "Enter Invoice No.")

        If Not Me.SelectedRow("CampusID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")

        If myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "IP") Then myRow("BillOf") = "P" Else myRow("BillOf") = "C"
        myRow("InvoiceTypeCode") = "OF"

        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")
        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        Dim objIVProcFICO As clsIVProcFICO

        VSave = False
        If Me.Validate Then
            If Not myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "") Then
                objIVProcFICO = New clsIVProcFICO(myRow("DocType"), myContext)
            End If
            If myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("InvoiceNum", "Accounting entry passed. Invoice can't be update.")
            End If

            If Me.SelectedRow("CampusID") Is Nothing OrElse objIVProcFICO.IsVouchDateAfterFinStart(myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), myRow("PostingDate")) = True Then
                Me.AddError("PostingDate", "Posting Date can not be greater then Company Start Date.")
            End If


            If objIVProcFICO.GetInvoiceTypeID(myRow.Row) = False Then
                Me.AddError("InvoiceNum", "Combination Not Available")
            End If

            If myUtils.IsInList(myUtils.cStrTN(myRow("DocType")), "IP") Then
                Dim oProc As New clsGSTInvoicePurch(myContext)
                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))
            Else
                Dim oProc As New clsGSTInvoiceSale(myContext)
                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))
            End If

            If Me.CanSave Then
                Dim InvoiceOpenDescrip As String = myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")
                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Invoice Where InvoiceID = " & frmIDX)

                    Dim Oret As clsProcOutput = objIVProcFICO.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmMode = EnumfrmMode.acEditM
                        frmIDX = myRow("InvoiceID")
                        myContext.Provider.dbConn.CommitTransaction(InvoiceOpenDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoiceOpenDescrip, Oret.Message)
                        Me.AddError("", Oret.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceOpenDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, sql As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            If myContext.SQL.Exists(Params, "@CustomerId") Then
                sql = myContext.SQL.PopulateSQLParams("CustomerID = @CustomerId and CompanyID = @CompanyId and SalesOrderID Not in (@SalesOrderID) and OrderDate <= '@InvoiceDate'", Params)
            Else
                sql = myContext.SQL.PopulateSQLParams("CompanyID = @CompanyId and SalesOrderID Not in (@SalesOrderID) and (isnull(OrderDate,'3099-Jan-01') <= '@InvoiceDate' or isnull(LOIDate,'3099-Jan-01') <= '@InvoiceDate')", Params)
            End If
            If sql.Trim.Length > 0 Then Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SalesOrderID""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
        End If
        Return Model
    End Function

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "salesorder"
                Dim sql As String = "Select OrderNum, OrderDate  from SalesOrder  where SalesOrderID = " & myUtils.cValTN(frmIDX)
                Dim dt2 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt2.Rows.Count > 0 Then oRet.Description = "Sales Order :- " & myUtils.cStrTN(dt2.Rows(0)("OrderNum")) & " / " & myUtils.cStrTN(dt2.Rows(0)("OrderDate"))
        End Select
        Return oRet
    End Function
End Class
