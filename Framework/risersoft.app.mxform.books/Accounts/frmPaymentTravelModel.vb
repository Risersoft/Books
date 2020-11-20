Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmPaymentTravelModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False, myViewFinal, myViewReturn As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Advance")
        myViewFinal = Me.GetViewModel("Final")
        myViewReturn = Me.GetViewModel("Return")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CompanyID, CompName,FinStartDate  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = "Select EmployeeID, EmpCode +' | '+ FullName, JoinDate, LeaveDate from hrpListAllEmp() where Imprestenabled = 1 Order By FullName"
        Me.AddLookupField("EmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Employee").TableName)

        sql = myFuncsBase.CodeWordSQL("PaymentTravel", "PaymentType", "")
        Me.AddLookupField("PaymentType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentType").TableName)

        sql = "Select CampusID, DispName, CompanyID, WODate,CompletedOn from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CashCampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)

        sql = "Select BankAccountID, AccountName, GlAccountId, companyid, ShortName from BankAccount"
        Me.AddLookupField("BankAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "BankAccount").TableName)

        Dim str1 As String = myUtils.CombineWhereOR(False, "isnull(imprestenabled,0)=1", "employeeid in (select imprestemployeeid from payment)")
        sql = "Select employeeid, empcode, fullname, onrolls, companyid,JoinDate,LeaveDate,IgnoreExpenseVoucher from hrpListAllEmp() where " & str1 & " order by fullname"
        Me.AddLookupField("ImprestEmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "emp").TableName)

        sql = myFuncsBase.CodeWordSQL("Payment", "Mode", "Codeword <> 'WO'")
        Me.AddLookupField("PaymentMode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "PaymentMode").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objPaymentTravel As New clsPaymentTravel(myContext)
        Dim objTourVouchProc As New clsTourVouchProc(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Payment Where PaymentID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objPaymentTravel.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        objPaymentTravel.LoadVouch(myUtils.cValTN(myRow("PaymentID")))

        sql = "Select TourVouchID, PaymentID, EmployeeID, FullName, Dated, TotalPayment from slsListTourVouch() Where PaymentID = " & myUtils.cValTN(prepIDX) & " and isNull(isAdvance, 0) =1"
        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""FullName"" CAPTION=""Employee Name""/>"
        myView.MainGrid.QuickConf(sql, True, "3-1-1")
        myView.MainGrid.myDS.Tables(0).TableName = "TourVouchAdv"

        sql = "Select TourVouchID, PaymentID, EmployeeID, FullName, Dated, TotalPayment from slsListTourVouch()  Where PaymentID = " & myUtils.cValTN(prepIDX) & " and isNull(isAdvance, 0) =0"
        myViewFinal.MainGrid.MainConf("FORMATXML") = "<COL KEY=""FullName"" CAPTION=""Employee Name""/>"
        myViewFinal.MainGrid.QuickConf(sql, True, "3-1-1")
        myViewFinal.MainGrid.myDS.Tables(0).TableName = "TourVouchFinal"

        myViewReturn.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/><COL KEY=""FullName"" CAPTION=""Employee Name""/>"
        sql = "Select TourVouchItemID, PaymentID, AdvanceVouchID,OpenAmountAdj,AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & frmIDX & " and AdvanceVouchID is Not NULL"
        myViewReturn.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        Dim str1 As String = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount""/></BAND>"
        myViewReturn.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        objTourVouchProc.PopulatePreBalanceAdv(myViewReturn.MainGrid.myDS.Tables(0).Select, "PaymentID", frmIDX, "AdvanceVouchID")

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.SelectedRow("PaymentType") Is Nothing Then Me.AddError("PaymentType", "Please select Payment Type")

        If Not Me.SelectedRow("CompanyID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CompanyID")), Me.myRow("Dated"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")
        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")
        myFuncs.ValidatePaymentMode(Me)


        If Not myFuncs.CheckColumnAmount(myViewReturn.MainGrid.myDS.Tables(0), {"Amount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")

        If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentType")), "AR") Then
            If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "IM") Then Me.AddError("PaymentType", "This type of voucher not allowed.")
        Else
            If myUtils.IsInList(myUtils.cStrTN(myRow("PaymentMode")), "IM") AndAlso myFuncs.IgnoreExpenseVoucher(myContext, myUtils.cValTN(myRow("ImprestEmployeeID"))) = False Then
                If myUtils.cValTN(myViewReturn.MainGrid.GetColSum("Amount")) <> myUtils.cValTN(myRow("AmountTotPay")) Then Me.AddError("", "Please enter Advance for Expenses and Advance amount should be equal to Payment Amount.")
            End If
        End If

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))) Then
                Me.AddError("VouchNum", "Accounting entry passed. Voucher can't be update.")
            End If

            Dim objPaymentTravel As New clsPaymentTravel(myContext)
            objPaymentTravel.LoadVouch(myUtils.cValTN(myRow("PaymentID")))
            myRow("NewAmount") = myUtils.cValTN(myRow("AmountTotPay"))
            myRow("DocType") = objPaymentTravel.DocType
            myRow("PostingDate") = myRow("Dated")
            objPaymentTravel.GenerateVoucherDelta(myRow.Row.Table, Nothing)

            Dim ObjVouch As New clsVoucherNum(myContext)
            ObjVouch.GetNewSerialNo("PaymentID", "PT", myRow.Row)

            If Me.CanSave Then
                Dim PaymentTypeDescrip As String = Me.SelectedRow("PaymentType")("Descrip")
                Dim PaymentDescrip As String = myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy") & ", Type: " & PaymentTypeDescrip & ""

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Payment Where PaymentID = " & frmIDX)
                    frmIDX = myRow("PaymentID")

                    myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    myUtils.ChangeAll(myViewFinal.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                    Dim TourVouchIDCSV As String = myUtils.MakeCSV(",", myUtils.MakeCSV(myView.MainGrid.myDS.Tables(0).Select(), "TourVouchID"), myUtils.MakeCSV(myViewFinal.MainGrid.myDS.Tables(0).Select(), "TourVouchID"))

                    Dim Sql As String = "Update TourVouch set PaymentID=Null where PaymentID=" & frmIDX & " and TourVouchID not in (" & TourVouchIDCSV & ")"
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)

                    Sql = "Update TourVouch set PaymentID=" & frmIDX & " where TourVouchID in (" & TourVouchIDCSV & ")"
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)

                    objPaymentTravel.SaveTourVouch(myView.MainGrid.myDS.Tables(0), myViewFinal.MainGrid.myDS.Tables(0))

                    myUtils.ChangeAll(myViewReturn.MainGrid.myDS.Tables(0).Select, "PaymentID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myViewReturn.MainGrid.myDS.Tables(0), "Select TourVouchItemID, PaymentID, AdvanceVouchID, Amount from TourVouchItem")

                    Dim Oret As clsProcOutput = objPaymentTravel.HandleWorkflowState(myRow.Row)
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
        Model = New clsViewModel(vwState, myContext)
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
            Dim str2 As String = myFuncs.FilterTimeDependent(Dated, "JoinDate", "LeaveDate", 12)
            Select Case SelectionKey.Trim.ToLower
                Case "ta"
                    Dim Sql1 As String = myContext.SQL.PopulateSQLParams("Select TourVouchID,EmployeeID,PaymentID,CompanyID, DivisionID, TotalPayment, FullName,Dated,VouchNum,PaymentVouchNum,TotalAmount,BalanceDate,BalanceAmount,Remark from slsListTourVouch() where ((PaymentID is NULL and TransferTourVouchID is NULL and isnull(isopening,0) = 0 and isNull(BalanceAmount,0) > 0) or PaymentID = @paymentid) and CompanyID = @CompanyID and EmployeeID in  (Select EmployeeID from hrpListemp() where " & str2 & ") and IsAdvance = 1 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@tourvouchidcsv) and PostingDate <= '@Dated'", Params)
                    Model.MainGrid.QuickConf(Sql1, True, "1.5-.9-.7-.7-1-.9-1-1")
                Case "te"
                    Dim Sql1 As String = myContext.SQL.PopulateSQLParams("Select TourVouchID,EmployeeID,PaymentID,CompanyID,DivisionID,FullName,Dated,VouchNum,PaymentVouchNum,TotalAmount,LessAdvance,TotalPayment,Remark from slsListTourVouch() where ((PaymentID is NULL and IsNull(TotalPayment, 0) <> 0) or PaymentID = @paymentid) and CompanyID = @CompanyID and EmployeeID in  (Select EmployeeID from hrpListemp() where " & str2 & ") and isNull(IsAdvance,0) = 0 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@tourvouchidcsv) and PostingDate <= '@Dated'", Params)
                    Model.MainGrid.QuickConf(Sql1, True, "2-1-.7-1-1-1-1-1")
                Case "ar"
                    Dim Sql1 As String = myContext.SQL.PopulateSQLParams("Select TourVouchID,EmployeeID,PaymentID,CompanyID, DivisionID,TotalPayment, OpenAmountAdj,AvAmountadj, FullName,Dated,VouchNum,PaymentVouchNum,TotalAmount,BalanceDate,BalanceAmount,Remark from slsListTourVouch() where isNull(PaymentID,0) > 0 and CompanyID  = @companyid and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1 and IsAdvance = 1 and EmployeeID = @EmployeeID", Params)
                    Model.MainGrid.QuickConf(Sql1, True, "1.5-.9-.7-.7-1-.9-1-1")
                Case "employee"
                    Dim str1 As String = myFuncs.FilterTimeDependent(Dated, "JoinDate", "LeaveDate", 12)
                    Dim sql1 As String = myUtils.CombineWhere(False, str1, "Imprestenabled = 1")


                    Dim Sql As String = "select EmployeeID, EmpCode, FullName, Designation, DepName from hrpListAllEmp() where " & sql1 & " order by FullName"
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""EmpCode"" CAPTION=""Employee Code""/><COL KEY=""FullName"" CAPTION=""Employee Name""/><COL KEY=""DepName"" CAPTION=""Department""/>"
                    Model.MainGrid.QuickConf(Sql, True, "1-2-1-1", True, "Employee List")
                Case "ae"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("(isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) and CompanyID  = @companyid and EmployeeID = @employeeid and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TA""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(Sql) & "</SQLWHERE2></MODROW>")
            End Select
        End If
        Return Model
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
