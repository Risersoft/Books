Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmPaymentTravelSetlModel
    Inherits clsFormDataModel
    Dim myViewTA As clsViewModel, PPFinal As Boolean = False

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("TE")
        myViewTA = Me.GetViewModel("TA")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String

        Sql = "Select CompanyID, CompName,FinStartDate  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Company").TableName)

        Sql = "Select EmployeeID, EmpCode +' | '+ FullName, JoinDate, LeaveDate from hrpListAllEmp() where Imprestenabled = 1 Order By FullName"
        Me.AddLookupField("EmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "EMP").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String, objPaymentTravel As New clsPaymentTravel(myContext)

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

        sql = "Select TourVouchID, PaymentID, EmployeeID, FullName, Dated, TotalPayment from slsListTourVouch() Where PaymentID = " & myUtils.cValTN(prepIDX) & " and isNull(isAdvance, 0) =0"
        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""FullName"" CAPTION=""Employee Name""/>"
        myView.MainGrid.QuickConf(sql, True, "3-1-1")
        myView.MainGrid.myDS.Tables(0).TableName = "Expense Voucher"

        myViewTA.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = "Select TourVouchItemID, PaymentID, AdvanceVouchID,OpenAmountAdj,AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & frmIDX & " and AdvanceVouchID is Not NULL"
        myViewTA.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount""/></BAND>"
        myViewTA.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Dim objTourVouchProc As New clsTourVouchProc(myContext)
        objTourVouchProc.PopulatePreBalanceAdv(myViewTA.MainGrid.myDS.Tables(0).Select, "PaymentID", frmIDX, "AdvanceVouchID")

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CompanyID") Is Nothing Then Me.AddError("CompanyID", "Please Select Company")
        If Me.SelectedRow("EmployeeID") Is Nothing Then Me.AddError("EmployeeID", "Please Select Employee")

        If myUtils.cValTN(myRow("CompanyID")) > 0 Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CompanyID")), Me.myRow("Dated"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")
        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")

        If myUtils.cValTN(myRow("AmountTotPay")) <> 0 Then
            Me.AddError("AmountTotPay", "Advance Amount should be equal to Expenses Voucher Amount")
        End If

        If Not myFuncs.CheckColumnAmount(myViewTA.MainGrid.myDS.Tables(0), {"Amount", "Balance"}) Then Me.AddError("", "Negative Amount Not Allowed.")

        myViewTA.MainGrid.CheckValid("", "", , "<CHECK COND=""Dated &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Advance Date.""/>")
        myView.MainGrid.CheckValid("", "", , "<CHECK COND=""Dated &lt;='" & myUtils.cStrTN(myRow("Dated")) & "'"" MSG=""Payment Date Can not be less then Voucher Date.""/>")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then

            Dim objPaymentTravel As New clsPaymentTravel(myContext)
            objPaymentTravel.LoadVouch(myUtils.cValTN(myRow("PaymentID")))
            myRow("DocType") = objPaymentTravel.DocType
            myRow("PaymentType") = "S"
            myRow("PostingDate") = myRow("Dated")
            objPaymentTravel.GenerateVoucherDelta(myRow.Row.Table, Nothing)

            Dim ObjVouch As New clsVoucherNum(myContext)
            ObjVouch.GetNewSerialNo("PaymentID", "PT", myRow.Row)

            Dim PaymentTypeDescrip As String = "Settlement"
            Dim PaymentDescrip As String = myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy") & ", Type: " & PaymentTypeDescrip & ""
            Try
                myContext.CommonData.GetDatasetFYComp(False)
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Payment Where PaymentID = " & frmIDX)
                frmIDX = myRow("PaymentID")


                myUtils.ChangeAll(myView.MainGrid.myDS.Tables(0).Select, " PaymentID =" & frmIDX)
                Dim TourVouchIDCSV As String = myUtils.MakeCSV(myView.MainGrid.myDS.Tables(0).Select(), "TourVouchID")

                Dim Sql As String = "Update TourVouch set PaymentID=Null where PaymentID=" & frmIDX & " and TourVouchID not in (" & TourVouchIDCSV & ")"
                myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)

                Sql = "Update TourVouch set PaymentID=" & frmIDX & " where TourVouchID in (" & TourVouchIDCSV & ")"
                myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)

                myUtils.ChangeAll(myViewTA.MainGrid.myDS.Tables(0).Select, "PaymentID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewTA.MainGrid.myDS.Tables(0), "Select TourVouchItemID, PaymentID, AdvanceVouchID, Amount from TourVouchItem")

                Dim Oret As clsProcOutput = objPaymentTravel.HandleWorkflowState(myRow.Row)
                If Oret.Success Then
                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(PaymentTypeDescrip, frmIDX)
                    VSave = True
                Else
                    myContext.Provider.dbConn.RollBackTransaction(PaymentTypeDescrip, Oret.Message)
                    Me.AddError("", Oret.Message)
                End If
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(PaymentTypeDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "te"
                    Dim Sql1 As String = myContext.SQL.PopulateSQLParams("(PaymentID is NULL or PaymentID = @paymentid) and CompanyID = @CompanyID and EmployeeID =  @employeeid and isNull(IsAdvance,0) = 0 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@tourvouchidcsv) and PostingDate <= '@Dated' and IsNull(TotalPayment,0) > 0", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TE""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(Sql1) & "</SQLWHERE2></MODROW>")
                Case "ta"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("(isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) and CompanyID  = @companyid and EmployeeID = @employeeid and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TA""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(Sql) & "</SQLWHERE2></MODROW>")
                Case "employee"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str1 As String = myFuncs.FilterTimeDependent(Dated, "JoinDate", "LeaveDate", 36)
                    Dim sql1 As String = myUtils.CombineWhere(False, str1, "Imprestenabled = 1")

                    Dim Sql As String = "select EmployeeID, EmpCode, FullName, Designation, DepName from hrpListAllEmp() where " & sql1 & " order by FullName"
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""EmpCode"" CAPTION=""Employee Code""/><COL KEY=""FullName"" CAPTION=""Employee Name""/><COL KEY=""DepName"" CAPTION=""Department""/>"
                    Model.MainGrid.QuickConf(Sql, True, "1-2-1-1", True, "Employee List")
                Case "campus"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str1 As String = myFuncs.FieldFilter(myContext, Me.fRow, Dated, "WODate", "CompletedOn", "CampusID", 12)

                    Dim Sql As String = "Select CampusID, DispName from mmlistCampus()  where " & str1 & " order by DispName"
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""DispName"" CAPTION=""Campus""/>"
                    Model.MainGrid.QuickConf(Sql, True, "1", True, "Campus List")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim objTourVouchProc As New clsTourVouchProc(myContext), ObjPaymentVendor As New clsPaymentVendor(myContext)
        Dim Dated As DateTime
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ID", Params))
        If myContext.SQL.Exists(Params, "@Dated") Then Dated = myUtils.cDateTN(myContext.SQL.ParamValue("@Dated", Params), Now.Date)
        Select Case DataKey.Trim.ToUpper
            Case "GENERATEPREBAL"
                objTourVouchProc.PopulatePreBalanceAdv(dt.Select, "PaymentID", ID, "TourVouchID")
        End Select
    End Sub
End Class