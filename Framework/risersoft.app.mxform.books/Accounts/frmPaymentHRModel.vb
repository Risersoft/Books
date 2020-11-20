Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmPaymentHRModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim myViewAL, myViewLP, myViewBP, myViewTV, myViewTA, myViewMode, myViewAdv As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("PV")
        myViewAL = Me.GetViewModel("AL")
        myViewLP = Me.GetViewModel("LP")
        myViewBP = Me.GetViewModel("BP")
        myViewTV = Me.GetViewModel("TV")
        myViewTA = Me.GetViewModel("TA")
        myViewMode = Me.GetViewModel("Mode")
        myViewAdv = Me.GetViewModel("Adv")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CompanyID, CompName, finStartDate  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = "Select EmployeeID, FullName, CompanyID, JoinDate, LeaveDate from hrpListAllEmp()  Order By FullName"
        Me.AddLookupField("EmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Employee").TableName)

        sql = "SELECT  VendorID, VendorName FROM  purListVendor() Where VendorType = 'HC' Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = myFuncsBase.CodeWordSQL("PaymentHR", "PaymentType", "")
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


        sql = myFuncsBase.CodeWordSQL("Payment", "Mode", "")
        Me.AddLookupField("PaymentMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentMode").TableName)
        Me.AddLookupField("PaymentItemContra", "PaymentMode", "PaymentMode")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String, objPaymentHR As New clsPaymentHR(myContext)
        Dim objTourVouchProc As New clsTourVouchProc(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Payment Where PaymentID = " & prepIDX
        Me.InitData(sql, "PaymentHRVouchID", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objPaymentHR.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        objPaymentHR.LoadVouch(myUtils.cValTN(myRow("PaymentID")))

        sql = "Select PaymentHRVouchID, PaymentID, VouchTypeDescrip,  VouchDescrip,  Payee, TotalAmount from hrpListPaymentHRVouch() Where PaymentID = " & myUtils.cValTN(prepIDX)
        myView.MainGrid.QuickConf(sql, True, "2-2-2-1")
        myView.MainGrid.myDS.Tables(0).TableName = "PaymentHRVouch"

        sql = "Select EmpLoanID, PaymentID, EmpCode, FullName, DepName, Dated, Amount from hrpListEmpLoan()  Where PaymentID = " & myUtils.cValTN(prepIDX)
        myViewAL.MainGrid.QuickConf(sql, True, "1-2-2-1-1")
        myViewAL.MainGrid.myDS.Tables(0).TableName = "EmpLoan"

        sql = "Select EmpLoanPayBackID, PaymentID, EmpCode, FullName, PayPeriodName, DeductFromPPDescrip, Amount from hrpListLoanPayBack() Where PaymentID = " & myUtils.cValTN(prepIDX)
        myViewLP.MainGrid.QuickConf(sql, True, "1-3-2-1-1")
        myViewLP.MainGrid.myDS.Tables(0).TableName = " EmpLoanPayBack"

        sql = "Select PayPeriodBenefitID, PaymentID, BenefitName, PayPeriodName,AmountDiff, Amount from hrpListPayPeriodBenefit() Where PaymentID = " & myUtils.cValTN(prepIDX)
        myViewBP.MainGrid.QuickConf(sql, True, "2-2-1-1")
        str1 = "<BAND INDEX = ""0"" TABLE = ""PayPeriodBenefit"" IDFIELD=""PayPeriodBenefitID""><COL KEY =""PaymentID,AmountDiff""/></BAND>"
        myViewBP.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)
        myViewBP.MainGrid.myDS.Tables(0).TableName = "PayPeriodBenefit"

        sql = "Select TourVouchID, PaymentID, Dated, TotalPayment from slsListTourVouch()  Where PaymentID = " & myUtils.cValTN(prepIDX) & " and isNull(isAdvance, 0) =0"
        myViewTV.MainGrid.QuickConf(sql, True, "1-1")
        myViewTV.MainGrid.myDS.Tables(0).TableName = "TourVouch"

        myViewAdv.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = "Select TourVouchItemID, PaymentID, AdvanceVouchID,ImprestEmployeeID,OpenAmountAdj, AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & frmIDX & " and AdvanceVouchID is Not NULL and PaymentItemContraID is NULL"
        myViewAdv.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount, ImprestEmployeeID""/></BAND>"
        myViewAdv.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Me.BindDataTable(myUtils.cValTN(prepIDX))
        myViewMode.MainGrid.BindGridData(Me.dsForm, 1)
        myViewMode.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1")
        str1 = "<BAND INDEX = ""0"" TABLE = ""PaymentItemContra"" IDFIELD=""PaymentItemContraID""><COL KEY =""PaymentID, PaymentMode, BankAccountID, CashCampusID, ImprestEmployeeID, PayDate, PayText, Amount,PaymentInfo""/></BAND>"
        myViewMode.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)


        myViewTA.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        myViewTA.MainGrid.BindGridData(Me.dsForm, 2)
        myViewTA.MainGrid.QuickConf("", True, "1-1-1-1-1")
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount, ImprestEmployeeID, PaymentItemContraID""/></BAND>"
        myViewTA.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)


        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))

        objTourVouchProc.PopulatePreBalanceAdv(myViewTA.MainGrid.myDV.Table.Select, "PaymentID", frmIDX, "AdvanceVouchID")
        objTourVouchProc.PopulatePreBalanceAdv(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID", frmIDX, "AdvanceVouchID")
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal PaymentID As Integer)
        Dim ds As DataSet, Sql1, Sql2, Sql As String

        Sql1 = "Select PaymentItemContraID, PaymentID,CompanyID,PaymentInfo, BankAccountID, CashCampusID, ImprestEmployeeID, PaymentMode, PaymentModeDescrip, DispName, AccountName, FullName, PayDate, PayText, Amount from accListPaymentItemContra() Where PaymentID = " & myUtils.cValTN(PaymentID) & ""
        Sql2 = "Select TourVouchItemID, PaymentID, AdvanceVouchID, ImprestEmployeeID, PaymentItemContraID,OpenAmountAdj,AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & myUtils.cValTN(PaymentID) & " and AdvanceVouchID is Not NULL and PaymentItemContraID is Not NULL"


        Sql = Sql1 & "; " & Sql2
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "PaymentItemContra", 0)
        myUtils.AddTable(Me.dsForm, ds, "TA", 1)

        myContext.Tables.SetAuto(Me.dsForm.Tables("PaymentItemContra"), Me.dsForm.Tables("TA"), "PaymentItemContraID", "_TA")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("PaymentType") Is Nothing Then Me.AddError("PaymentType", "Please select Payment Type")
        If myUtils.IsInList(myUtils.cStrTN(myRow("CompanyID")), "C", "B", "V") AndAlso Me.SelectedRow("PaymentType") Is Nothing Then Me.AddError("PaymentType", "Please select Company")
        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentType")), "E") AndAlso Me.SelectedRow("EmployeeID") Is Nothing Then Me.AddError("EmployeeID", "Please select Employee")
        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentType")), "E") AndAlso myUtils.cValTN(myRow("EmployeeID")) > 0 Then myRow("CompanyID") = myUtils.cValTN(Me.SelectedRow("EmployeeID")("CompanyID"))

        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentType")), "V") AndAlso Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Contractor")
        myFuncs.ValidatePaymentMode(Me)

        If Not Me.SelectedRow("CompanyID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.myRow("CompanyID")), Me.myRow("Dated"), PPFinal)
        End If

        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")
        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")

        If Not myFuncs.CheckColumnAmount(myViewTA.MainGrid.myDV.Table, {"Amount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")
        If Not myFuncs.CheckColumnAmount(myViewAdv.MainGrid.myDS.Tables(0), {"Amount", "Balance"}) Then Me.AddError("", "Amount can Not be Less then Zero")

        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "IM") AndAlso myFuncs.IgnoreExpenseVoucher(myContext, myUtils.cValTN(myRow("ImprestEmployeeID"))) = False Then
            If myUtils.cValTN(myViewAdv.MainGrid.GetColSum("Amount")) <> myUtils.cValTN(myRow("NewAmount")) Then Me.AddError("", "Please enter Advance for Expenses and Advance amount should be equal to Payment Amount.")
        End If


        For Each r1 As DataRow In myViewMode.MainGrid.myDV.Table.Select("PaymentMode = 'IM'")
            If myContext.Tables.GetColSum(myViewTA.MainGrid.myDV.Table.Select("PaymentItemContraID = " & myUtils.cValTN(r1("PaymentItemContraID")) & ""), "Amount") <> myUtils.cValTN(r1("Amount")) Then Me.AddError("", "Additional Payment Amount should be equal to Advance for Expense.")
        Next

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            myRow("PostingDate") = myRow("Dated")

            Dim objPaymentHR As New clsPaymentHR(myContext)
            objPaymentHR.LoadVouch(myUtils.cValTN(myRow("PaymentID")))
            myRow("DocType") = objPaymentHR.DocType
            objPaymentHR.GenerateVoucherDelta(myRow.Row.Table, Nothing)

            Dim ObjVouch As New clsVoucherNum(myContext)
            ObjVouch.GetNewSerialNo("PaymentID", "PHR", myRow.Row)

            If myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))) Then
                Me.AddError("VouchNum", "Accounting entry passed. Voucher can't be update.")
            End If

            If Me.CanSave Then
                Dim PaymentTypeDescrip As String = Me.SelectedRow("PaymentType")("Descrip")
                Dim PaymentDescrip As String = myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy") & ", Type: " & PaymentTypeDescrip & ""

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Payment Where PaymentID = " & frmIDX)
                    frmIDX = myRow("PaymentID")

                    myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDS.Tables(0), "Select PaymentHRVouchID, PaymentID from PaymentHRVouch")

                    myUtils.ChangeAll(myViewAL.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewAL.MainGrid.myDS.Tables(0), "Select EmpLoanID, PaymentID from EmpLoan")

                    myUtils.ChangeAll(myViewLP.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewLP.MainGrid.myDS.Tables(0), "Select EmpLoanPayBackID, PaymentID from EmpLoanPayBack")

                    myUtils.ChangeAll(myViewBP.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewBP.MainGrid.myDS.Tables(0), "Select PayPeriodBenefitID, PaymentID,AmountDiff from PayPeriodBenefit")

                    myUtils.ChangeAll(myViewTV.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewTV.MainGrid.myDS.Tables(0), "Select TourVouchID, PaymentID from TourVouch")

                    myUtils.ChangeAll(myViewMode.MainGrid.myDV.Table.Select, " PaymentID =" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewMode.MainGrid.myDV.Table, "Select PaymentID, PaymentMode,PaymentInfo, BankAccountID, CashCampusID, ImprestEmployeeID, PayDate, PayText, Amount from PaymentItemContra")

                    myUtils.ChangeAll(myViewTA.MainGrid.myDV.Table.Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewTA.MainGrid.myDV.Table, "Select TourVouchItemID, PaymentID, ImprestEmployeeID, PaymentItemContraID, AdvanceVouchID, Amount from TourVouchItem")

                    objPaymentHR.SaveRefDoc(myView.MainGrid.myDS.Tables(0), myViewAL.MainGrid.myDS.Tables(0), myViewLP.MainGrid.myDS.Tables(0), myViewBP.MainGrid.myDS.Tables(0), myViewTV.MainGrid.myDS.Tables(0))

                    myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewAdv.MainGrid.myDS.Tables(0), "Select TourVouchItemID, PaymentID, ImprestEmployeeID, AdvanceVouchID, Amount from TourVouchItem")

                    Dim Oret As clsProcOutput = objPaymentHR.HandleWorkflowState(myRow.Row)
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
        Dim Model As clsViewModel = Nothing, sql As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Dim PaymentID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@paymentid", Params))
            Select Case SelectionKey.Trim.ToUpper
                Case "EMPLOYEE"
                    sql = "Select distinct EmployeeID from PaymentHRVouch where PaymentID is NULL or PaymentID = " & myUtils.cValTN(PaymentID)
                    Dim str1 As String = GetCSV(sql)

                    sql = "Select distinct EmployeeID from EmpLoan where PaymentID is NULL or PaymentID = " & myUtils.cValTN(PaymentID)
                    Dim str2 As String = GetCSV(sql)

                    sql = "Select distinct EmployeeID from hrpListLoanPayBack() where PaymentID is NULL or PaymentID = " & myUtils.cValTN(PaymentID)
                    Dim str3 As String = GetCSV(sql)

                    sql = "Select distinct EmployeeID from slsListTourVouch() where PaymentID is NULL or PaymentID = " & myUtils.cValTN(PaymentID)
                    Dim str5 As String = GetCSV(sql)

                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str4 As String = myFuncs.FilterTimeDependent(Dated, "JoinDate", "LeaveDate", 12)
                    sql = "(EmployeeID in (" & str1 & ") or EmployeeID in (" & str2 & ") or EmployeeID in (" & str3 & ") or EmployeeID in (" & str5 & ")) and " & str4
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""EmployeeID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "PV"
                    Dim PaymentType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@paymenttype", Params))
                    If myUtils.IsInList(myUtils.cStrTN(PaymentType), "C") Then
                        sql = myContext.SQL.PopulateSQLParams("(PaymentID is NULL or PaymentID = " & PaymentID & ") and ContractorID is Null and EmployeeID Is NULL and CompanyID = @companyid and PayDueType = 'P' and PaymentHRVouchID not in (@idcsv)", Params)
                    ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "E") Then
                        sql = myContext.SQL.PopulateSQLParams("(PaymentID is NULL or PaymentID = " & PaymentID & ") and EmployeeID = @employeeid and PayDueType = 'P' and PaymentHRVouchID not in (@idcsv)", Params)
                    ElseIf myUtils.IsInList(myUtils.cStrTN(PaymentType), "V") Then
                        sql = myContext.SQL.PopulateSQLParams("(PaymentID is NULL or PaymentID = " & PaymentID & ") and ContractorID = @ContractorID and PayDueType = 'P' and PaymentHRVouchID not in (@idcsv)", Params)
                    End If
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PaymentHRVouchID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "PVALLEMP"
                    Dim PaymentType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@paymenttype", Params))
                    sql = myContext.SQL.PopulateSQLParams("(PaymentID is NULL or PaymentID = " & PaymentID & ") and EmployeeID Is Not NULL and CompanyID = @companyid and PayDueType = 'P' and PaymentHRVouchID not in (@idcsv)", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PaymentHRVouchID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "AL"
                    sql = myContext.SQL.PopulateSQLParams(" (PaymentID is NULL or PaymentID = " & PaymentID & ") and EmployeeID = @employeeid and EmpLoanID not in (@idcsv) and Dated >= '@compstdate'", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""EmpLoanID""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "LP"
                    sql = myContext.SQL.PopulateSQLParams(" (PaymentID is NULL or PaymentID = " & PaymentID & ") and EmployeeID = @employeeid and EmpLoanPaybackID not in (@idcsv)", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""EmpLoanPaybackID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "BP"
                    sql = myContext.SQL.PopulateSQLParams(" (PaymentID is NULL or PaymentID = " & PaymentID & ") and CompanyID = @companyid and PayPeriodBenefitID not in (@idcsv)", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PayPeriodBenefitID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "TV"
                    sql = myContext.SQL.PopulateSQLParams("(PaymentID is NULL or PaymentID = @paymentid) and IsNull(TotalPayment ,0) > 0  and EmployeeID = @employeeid and isNull(IsAdvance,0) = 0 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@idcsv)", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TE""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "TA"
                    sql = myContext.SQL.PopulateSQLParams("(isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) and IsNull(BalanceAmount,0) > 0 and EmployeeID = @employeeid and isNull(IsAdvance,0) = 1 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@idcsv)", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TA""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "ADV"
                    sql = myContext.SQL.PopulateSQLParams("(isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) and EmployeeID = @employeeid and IsNull(BalanceAmount,0) > 0 and isNull(IsAdvance,0) = 1 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@tourvouchidcsv) and  CompanyID  = @companyid", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TA""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
    End Function

    Private Function GetCSV(Sql As String) As String
        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)
        Dim Str1 As String = myUtils.MakeCSV(dt.Select(), "EmployeeID")
        Return Str1
    End Function

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "paymenthrvouch"
                Dim Sql As String = "Select * from hrpListPaymentHRVouch() Where PaymentHRVouchID = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
        End Select
        Return oRet
    End Function

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim objTourVouchProc As New clsTourVouchProc(myContext)
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ID", Params))
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
