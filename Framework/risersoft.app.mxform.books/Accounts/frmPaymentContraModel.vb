Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmPaymentContraModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False, myViewAdv, myViewAdvReq As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
        myViewAdv = Me.GetViewModel("Adv")
        myViewAdvReq = Me.GetViewModel("TA")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CompanyID, CompName  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "InvType", "CodeWord = 'T'")
        Me.AddLookupField("PaymentType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentType").TableName)

        sql = "Select CampusID, DispName, CompanyID, WODate,CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CashCampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("PaymentItemContra", "CashCampusID", "Campus")

        sql = "Select BankAccountID, AccountName, GlAccountId, companyid, ShortName from BankAccount"
        Me.AddLookupField("BankAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BankAccount").TableName)
        Me.AddLookupField("PaymentItemContra", "BankAccountID", "BankAccount")

        Dim str1 As String = myUtils.CombineWhereOR(False, "isnull(imprestenabled,0)=1", "employeeid in (select imprestemployeeid from payment)")
        sql = "Select employeeid, empcode, fullname, onrolls, companyid,JoinDate,LeaveDate,IgnoreExpenseVoucher from hrpListAllEmp() where " & str1 & " order by fullname"
        Me.AddLookupField("ImprestEmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "emp").TableName)
        Me.AddLookupField("PaymentItemContra", "ImprestEmployeeID", "emp")

        sql = myFuncsBase.CodeWordSQL("Payment", "Mode", "Codeword <> 'WO'")
        Me.AddLookupField("PaymentMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentMode").TableName)
        Me.AddLookupField("PaymentItemContra", "PaymentMode", "PaymentMode")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, ObjPaymentContra As New clsPaymentContra(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Payment Where PaymentID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
            myRow("PaymentType") = "T"
        Else
            Dim rPostPeriod As DataRow = ObjPaymentContra.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Me.BindDataTable(myUtils.cValTN(prepIDX))
        ObjPaymentContra.LoadVouch(myUtils.cValTN(myRow("PaymentID")))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1", True)
        Dim str1 As String = "<BAND INDEX = ""0"" TABLE = ""PaymentItemContra"" IDFIELD=""PaymentItemContraID""><COL KEY =""PaymentID, PaymentMode,PaymentInfo, BankAccountID, CashCampusID, ImprestEmployeeID, PayDate, PayText, Amount""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        myViewAdv.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = "Select TourVouchItemID, PaymentID, AdvanceVouchID,OpenAmountAdj,AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & frmIDX & " and AdvanceVouchID is Not NULL"
        myViewAdv.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount""/></BAND>"
        myViewAdv.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        sql = "Select TourVouchID, PaymentID, EmployeeID, FullName, Dated, TotalPayment from slsListTourVouch() Where PaymentID = " & myUtils.cValTN(prepIDX) & " and isNull(isAdvance, 0) =1"
        myViewAdvReq.MainGrid.MainConf("FORMATXML") = "<COL KEY=""FullName"" CAPTION=""Employee Name""/>"
        myViewAdvReq.MainGrid.QuickConf(sql, True, "3-1-1")
        myViewAdvReq.MainGrid.myDS.Tables(0).TableName = "TourVouchAdv"

        Dim objTourVouchProc As New clsTourVouchProc(myContext)
        objTourVouchProc.PopulatePreBalanceAdv(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID", frmIDX, "AdvanceVouchID")

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal PaymentID As Integer)
        Dim ds As DataSet, Sql As String

        Sql = " Select PaymentItemContraID, PaymentID,CompanyID, BankAccountID, PaymentInfo, CashCampusID, ImprestEmployeeID, PaymentMode, PaymentModeDescrip, DispName, AccountName, FullName, PayDate, PayText, Amount from accListPaymentItemContra() Where PaymentID = " & PaymentID & ""
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "PaymentItemContra", 0)
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("CompanyID") Is Nothing Then Me.AddError("CompanyID", "Please select Company")
        If Me.SelectedRow("PaymentType") Is Nothing Then Me.AddError("PaymentType", "Please select PaymentType")

        If Not Me.SelectedRow("CompanyID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.myRow("CompanyID")), Me.myRow("Dated"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")
        If myUtils.cValTN(Me.myRow("AmountTotPay")) <> myUtils.cValTN(myView.MainGrid.GetColSum("Amount")) Then Me.AddError("AmountTotPay", "Enter Correct Amount")

        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")
        myFuncs.ValidatePaymentMode(Me)

        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "IM") AndAlso myFuncs.IgnoreExpenseVoucher(myContext, myUtils.cValTN(myRow("ImprestEmployeeID"))) = False Then
            If myUtils.cValTN(myViewAdv.MainGrid.GetColSum("Amount")) <> myUtils.cValTN(myRow("AmountTotPay")) Then Me.AddError("", "Please enter Advance for Expenses and Advance amount should be equal to Payment Amount.")
        End If

        For Each r1 As DataRow In myView.MainGrid.myDV.Table.Select("PaymentMode = 'IM'")
            If myFuncs.IgnoreExpenseVoucher(myContext, myUtils.cValTN(r1("ImprestEmployeeID"))) = False Then
                If myUtils.cValTN(myViewAdvReq.MainGrid.GetColSum("TotalPayment", "EmployeeID = " & myUtils.cValTN(r1("ImprestEmployeeID")) & "")) <> myUtils.cValTN(r1("Amount")) Then Me.AddError("", "Please enter Advance for Expenses and Advance amount should be equal to Payment Amount.")
            End If
        Next

        If Not myFuncs.CheckColumnAmount(myViewAdv.MainGrid.myDS.Tables(0), {"Amount", "Balance"}) Then Me.AddError("", "Amount can Not be Less then Zero")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim ObjPaymentContra As New clsPaymentContra(myContext)
            ObjPaymentContra.LoadVouch(myUtils.cValTN(myRow("PaymentID")))
            myRow("DocType") = ObjPaymentContra.DocType
            myRow("PostingDate") = myRow("Dated")
            If myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))) Then
                Me.AddError("VouchNum", "Accounting entry passed. Voucher can't be update.")
            End If

            If Me.CanSave Then
                Dim ObjVouch As New clsVoucherNum(myContext)
                ObjVouch.GetNewSerialNo("PaymentID", "PCO", myRow.Row)

                Dim PaymentTypeDescrip As String = Me.SelectedRow("PaymentType")("Descrip")
                Dim PaymentDescrip As String = myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy") & ", Type: " & PaymentTypeDescrip & ""

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Payment Where PaymentID = " & frmIDX)
                    frmIDX = myRow("PaymentID")

                    myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDV.Table, "Select PaymentID, PaymentMode,PaymentInfo, BankAccountID, CashCampusID, ImprestEmployeeID, PayDate, PayText, Amount from PaymentItemContra")

                    myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewAdv.MainGrid.myDS.Tables(0), "Select TourVouchItemID, PaymentID, AdvanceVouchID, Amount from TourVouchItem")



                    myUtils.ChangeAll(myViewAdvReq.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    Dim TourVouchIDCSV As String = myUtils.MakeCSV(",", myUtils.MakeCSV(myViewAdvReq.MainGrid.myDS.Tables(0).Select(), "TourVouchID"))

                    Dim Sql As String = "Update TourVouch set PaymentID=Null where PaymentID=" & frmIDX & " and TourVouchID not in (" & TourVouchIDCSV & ")"
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)

                    Sql = "Update TourVouch set PaymentID=" & frmIDX & " where TourVouchID in (" & TourVouchIDCSV & ")"
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)



                    Dim Oret As clsProcOutput = ObjPaymentContra.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(PaymentDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(PaymentDescrip, Oret.Message)
                        Me.AddError("", Oret.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(PaymentDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToUpper
                Case "TOURVOUCH"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("(isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) and CompanyID  = @companyid and EmployeeID = @employeeid and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TA""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(Sql) & "</SQLWHERE2></MODROW>")
                Case "ADVREQ"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim Sql1 As String = myContext.SQL.PopulateSQLParams("Select TourVouchID,EmployeeID,PaymentID,CompanyID, DivisionID, TotalPayment, FullName,Dated,VouchNum,PaymentVouchNum,TotalAmount,BalanceDate,BalanceAmount,Remark from slsListTourVouch() where ((PaymentID is NULL and TransferTourVouchID is Null and isnull(isopening,0) = 0 and isNull(BalanceAmount,0) > 0) or PaymentID = @PaymentID) and CompanyID = @CompanyID and EmployeeID = @EmployeeID and IsAdvance = 1 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@tourvouchidcsv) and PostingDate <= '@Dated'", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.QuickConf(Sql1, True, "1.5-.9-.7-.7-1-.9-1-1")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim objTourVouchProc As New clsTourVouchProc(myContext)
        Dim Dated As DateTime
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ID", Params))
        If myContext.SQL.Exists(Params, "@Dated") Then Dated = myUtils.cDateTN(myContext.SQL.ParamValue("@Dated", Params), Now.Date)
        Select Case DataKey.Trim.ToUpper
            Case "GENERATEPREBAL"
                objTourVouchProc.PopulatePreBalanceAdv(dt.Select, "PaymentID", ID, "TourVouchID")
        End Select
    End Sub

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim Sql As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "missingdocsysnum"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CompanyID", Params))
                    Dim FormatType As String = myContext.SQL.ParamValue("@FormatType", Params)
                    oRet.ID = myFuncs.MissingDocSysNum(myContext, Dated, CompanyID, FormatType)
            End Select
        End If
        Return oRet
    End Function
End Class
