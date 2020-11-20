Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmTourAdvReqModel
    Inherits clsFormDataModel
    Dim objTourVouchProc As New clsTourVouchProc(myContext), ObjVouch As New clsVoucherNum(myContext)
    Dim dtAllowType As DataTable, myViewAllow As clsViewModel, PPFinal As Boolean = False

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("TourStation")
        myViewAllow = Me.GetViewModel("TourAllow")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String

        Sql = "Select CampusID, DispName, CompanyID,CampusType,TaxAreaCode, DivisionCodeList, WODate, CompletedOn,FinStartDate from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Campus").TableName)

        Sql = "Select EmployeeID, EmpCode +' | '+ FullName, JoinDate, LeaveDate from hrpListAllEmp() where Imprestenabled = 1 Order By FullName"
        Me.AddLookupField("EmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "EMP").TableName)

        Sql = "Select * from CodeWords Where CodeClass = 'TourVouch' and CodeType = 'AllowType'"
        dtAllowType = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)

        Sql = "Select MainPartyID, MPName from PartyMain where PartyType = 'C'  Order by MPName"
        Me.AddLookupField("TourStation", "MainPartyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "PartyMain").TableName)

        Sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Division").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from TourVouch Where TourVouchID = " & prepIDX
        Me.InitData(sql, "IsOpening", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objTourVouchProc.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        sql = "Select TourStationID, TourVouchID, MainPartyID, Station, EstDays, StartDate, EndDate, Organization, Purpose, TravelMode, TravelFare from TourStation  Where TourVouchID = " & frmIDX & ""
        myView.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1-1")
        str1 = "<BAND IDFIELD=""TourStationID"" TABLE=""TourStation"" INDEX=""0""><COL KEY=""TourVouchID, MainPartyID, Station, EstDays, StartDate, EndDate, Organization, Purpose, TravelMode, TravelFare""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        sql = "Select TourAllowID, TourVouchID, AllowType, Station, DaysNum, DayRate, Amount from TourAllow Where TourVouchID = " & frmIDX & ""
        myViewAllow.MainGrid.QuickConf(sql, True, "1-1-1-1-1")
        sql = "Select CodeWord,DescripShort from CodeWords Where CodeClass = 'TourVouch' and CodeType = 'AllowType'"
        str1 = "<BAND IDFIELD=""TourAllowID"" TABLE=""TourAllow"" INDEX=""0""><COL KEY=""TourVouchID, Station, DaysNum, DayRate""/><COL  NOEDIT=""TRUE"" KEY=""AllowType"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewAllow.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        For Each r1 As DataRow In dtAllowType.Select()
            Dim rr1() As DataRow = myViewAllow.MainGrid.myDS.Tables(0).Select("AllowType = '" & myUtils.cStrTN(r1("CodeWord")) & "'")
            If rr1.Length = 0 Then
                Dim r2 As DataRow = myContext.Tables.AddNewRow(myViewAllow.MainGrid.myDS.Tables(0))
                r2("AllowType") = myUtils.cStrTN(r1("CodeWord"))
            End If
        Next

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "TourVouchID", myUtils.cValTN(myRow("TourVouchID"))), GetType(Boolean), False))

        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusID") Is Nothing Then Me.AddError("CampusID", "Please Select Campus")
        If Me.SelectedRow("EmployeeID") Is Nothing Then Me.AddError("EmployeeID", "Please Select Employee")
        If myUtils.cValTN(myRow("CampusID")) > 0 Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("Dated"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("Dated", "Please Select Valid Post Period")

        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")


        Dim rr2() As DataRow = myViewAllow.MainGrid.myDS.Tables(0).Select("isnull(DaysNum,0) = 0 And isnull(DayRate,0) > 0")
        If rr2.Length > 0 Then
            Me.AddError("", "Please Enter Days No.")
        End If

        If myUtils.cBoolTN(myRow("IsOpening")) = True AndAlso myRow("Dated") >= Me.SelectedRow("CampusID")("FinStartDate") Then
            Me.AddError("Dated", "Opening Date shaould be less then Company Start Date")
        End If

        If (myUtils.cValTN(myRow("PaymentID")) > 0) OrElse (myUtils.cValTN(myRow("TransferTourVouchID")) > 0) Then Me.AddError("", "Payment Done. Not allow to edit")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            myRow("PostingDate") = myRow("Dated")
            ObjVouch.GetNewSerialNo("TourVouchID", "1", myRow.Row)
            Dim TourVouchDescrip As String = " Advance for Expenses for: " & myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy")
            Try
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "TourVouchID", frmIDX)
                myRow("IsAdvance") = True
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                frmIDX = myRow("TourVouchID")

                Me.myView.MainGrid.SaveChanges(, "TourVouchID = " & frmIDX)

                myUtils.ChangeAll(myViewAllow.MainGrid.myDS.Tables(0).Select, "TourVouchID=" & frmIDX)
                Me.myViewAllow.MainGrid.SaveChanges(, "TourVouchID = " & frmIDX)

                Dim Oret As clsProcOutput = objTourVouchProc.HandleWorkflowState(myRow.Row)
                If Oret.Success Then
                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(TourVouchDescrip, frmIDX)
                    VSave = True
                Else
                    myContext.Provider.dbConn.RollBackTransaction(TourVouchDescrip, Oret.Message)
                    Me.AddError("", Oret.Message)
                End If
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(TourVouchDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "employee"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str1 As String = myFuncs.FilterTimeDependent(Dated, "JoinDate", "LeaveDate", 12)
                    Dim sql1 As String = myUtils.CombineWhere(False, str1, "Imprestenabled = 1")
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""EmployeeID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql1) & "</SQLWHERE2></MODROW>")
                Case "campus"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str1 As String = myFuncs.FieldFilter(myContext, Me.fRow, Dated, "WODate", "CompletedOn", "CampusID", 12)

                    Dim Sql As String = "Select CampusID, DispName, CampusAddress from mmlistCampus()  where " & str1 & " order by DispName"
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""DispName"" CAPTION=""Campus""/><COL KEY=""CampusAddress"" CAPTION=""Address""/>"
                    Model.MainGrid.QuickConf(Sql, True, "1-1", True, "Campus List")
            End Select
        End If
        Return Model
    End Function
End Class
