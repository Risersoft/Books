Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmTourExpRepModel
    Inherits clsFormDataModel
    Dim objTourVouchProc As New clsTourVouchProc(myContext), ObjVouch As New clsVoucherNum(myContext)
    Dim myViewDD, myViewHE, myViewLC, myViewMES, myViewAdv, myViewMEA, myViewTRI, myViewMEM, myViewTRO, myViewCostLot, myViewCostWBS, myViewCostCenter, myViewAdvReq As clsViewModel, PPFinal As Boolean = False

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("JD")
        myViewDD = Me.GetViewModel("DD")
        myViewHE = Me.GetViewModel("HE")
        myViewLC = Me.GetViewModel("LC")
        myViewMES = Me.GetViewModel("MES")
        myViewMEA = Me.GetViewModel("MEA")
        myViewMEM = Me.GetViewModel("MEM")
        myViewAdv = Me.GetViewModel("Adv")
        myViewTRI = Me.GetViewModel("TRI")
        myViewTRO = Me.GetViewModel("TRO")
        myViewCostLot = Me.GetViewModel("CostLot")
        myViewCostWBS = Me.GetViewModel("CostWBS")
        myViewCostCenter = Me.GetViewModel("CostCenter")
        myViewAdvReq = Me.GetViewModel("AdvReq")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim Sql As String

        Sql = "Select CampusID, DispName, CompanyID,CampusType,TaxAreaCode, DivisionCodeList, WODate, CompletedOn, FinStartDate from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Campus").TableName)

        Sql = "Select EmployeeID, EmpCode +' | '+ FullName, JoinDate, LeaveDate from hrpListAllEmp() where Imprestenabled = 1 Order By FullName"
        Me.AddLookupField("EmployeeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "EMP").TableName)

        Sql = myFuncsBase.CodeWordSQL("TourVouch", "TourCountry", "")
        Me.AddLookupField("TourCountry", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "TourCountry").TableName)

        Sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Division").TableName)

        Sql = "Select Class as ClassCode, Class, ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass where ClassType = 'M' and Class Not Like ('%SCRAP%') Order By Class"
        Me.AddLookupField("ExpClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "ExpClass").TableName)

        Sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass = 'material' and CodeWord in ('ST', 'CG', 'CT', 'RM')  Order by CodeClass"
        Me.AddLookupField("TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "TransType").TableName)

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from TourVouch Where TourVouchID = " & prepIDX
        Me.InitData(sql, "IsOpening", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("Dated") = Now.Date
            myRow("PostingDate") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objTourVouchProc.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1-1")
        str1 = "<BAND IDFIELD=""TourJourneyID"" TABLE=""TourJourney"" INDEX=""0""><COL KEY=""TourVouchID, StationFrom, DateFrom, TimeFrom, StationTo, DateTo, TimeTo, TravelMode, Amount, Remark""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        myViewDD.MainGrid.BindGridData(Me.dsForm, 2)
        myViewDD.MainGrid.QuickConf("", True, "1-1-1")
        str1 = "<BAND IDFIELD=""TourAllowID"" TABLE=""TourAllow"" INDEX=""0""><COL KEY=""TourAllowID, TourVouchID, Station""/><COL KEY=""DaysNum"" CAPTION=""Days Num""/><COL KEY=""DayRate"" CAPTION=""Day Rate""/></BAND>"
        myViewDD.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewHE.MainGrid.BindGridData(Me.dsForm, 3)
        myViewHE.MainGrid.QuickConf("", True, "1-1-1-1")
        str1 = "<BAND IDFIELD=""TourHotelID"" TABLE=""TourHotel"" INDEX=""0""><COL KEY=""TourHotelID, TourVouchID, Amount""/><COL KEY=""HotelName"" CAPTION=""Hotel Name""/><COL KEY=""DaysNum"" CAPTION=""Days Num""/><COL KEY=""BillNum"" CAPTION=""Bill Num""/></BAND>"
        myViewHE.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewLC.MainGrid.BindGridData(Me.dsForm, 4)
        myViewLC.MainGrid.QuickConf("", True, "1-1-1-1-1-1")
        str1 = "<BAND IDFIELD=""TourLoConvID"" TABLE=""TourLoConv"" INDEX=""0""><COL KEY=""TourLoConvID, TourVouchID, FromPoint, ToPoint,  Amount, Dated, WithWhomMet""/><COL KEY=""Mode"" VLIST=""BUS|CAR|TRAIN|PLANE""/></BAND>"
        myViewLC.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewMES.MainGrid.BindGridData(Me.dsForm, 5)
        myViewMES.MainGrid.QuickConf("", True, "1-0-1-1")
        sql = "Select Class from AccountClass where (ClassType = 'S' and ClassSubType in ('P','B')) Order By Class"
        str1 = "<BAND IDFIELD=""TourMiscExpID"" TABLE=""TourMiscExp"" INDEX=""0""><COL KEY=""TourMiscExpID, TourVouchID, Particulars, Amount, ClassType, TransType""/><COL KEY=""ExpClass"" CAPTION=""Exp Class"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewMES.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewMEA.MainGrid.BindGridData(Me.dsForm, 5)
        myViewMEA.MainGrid.QuickConf("", True, "1-1-1-1")
        sql = "Select Class from AccountClass where ClassType = 'A' Order By Class"
        Dim sql2 As String = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeWord in ('APN', 'APU', 'ARO', 'ARW')  Order by CodeClass"
        str1 = "<BAND IDFIELD=""TourMiscExpID"" TABLE=""TourMiscExp"" INDEX=""0""><COL KEY=""TourMiscExpID, TourVouchID, Particulars, Amount, ClassType""/><COL KEY=""ExpClass"" CAPTION=""Exp Class"" LOOKUPSQL=""" & sql & """/><COL KEY=""TransType"" LOOKUPSQL=""" & sql2 & """/></BAND>"
        myViewMEA.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewMEM.MainGrid.BindGridData(Me.dsForm, 5)
        myViewMEM.MainGrid.QuickConf("", True, "1-1-1-1")
        str1 = "<BAND IDFIELD=""TourMiscExpID"" TABLE=""TourMiscExp"" INDEX=""0""><COL KEY=""TourMiscExpID, TourVouchID, Particulars, Amount, ClassType,TransType""/><COL KEY=""ExpClass"" CAPTION=""Exp Class""/></BAND>"
        myViewMEM.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)


        sql = "Select TourVouchItemID, TourVouchID, AdvanceVouchID,OpenAmountAdj,AVAmountAdj,TotalAmount, AdvanceVouchNum as VouchNum, AdvanceDated as Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where TourVouchID  = " & frmIDX & " and AdvanceVouchID is Not NULL"
        myViewAdv.MainGrid.QuickConf(sql, True, "1-1-1-1-1", True)
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,TourVouchID, AdvanceVouchID, Amount""/></BAND>"
        myViewAdv.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewTRI.MainGrid.BindGridData(Me.dsForm, 6)
        myViewTRI.MainGrid.QuickConf("", True, "4-0-1-1", True)
        sql = "Select EmployeeID, EmpCode +' | '+ FullName  from hrpListAllEmp() where Imprestenabled = 1 Order By FullName"
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,TourVouchID,  Amount, Dated""/><COL NOEDIT=""TRUE"" KEY=""ImprestEmployeeID"" CAPTION=""Employee Name"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewTRI.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewTRO.MainGrid.BindGridData(Me.dsForm, 6)
        myViewTRO.MainGrid.QuickConf("", True, "4-1-1", True)
        sql = "Select CampusID, DispName  from mmlistCampus()  Order by DispName"
        str1 = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,TourVouchID,  Amount, Dated""/><COL NOEDIT=""TRUE"" KEY=""CashCampusID"" CAPTION=""Campus"" LOOKUPSQL=""" & sql & """/></BAND>"
        myViewTRO.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)


        myViewCostLot.MainGrid.MainConf("FORMATXML") = "<COL KEY=""LotNum"" CAPTION=""Lot No.""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostLot.MainGrid.BindGridData(Me.dsForm, 7)
        myViewCostLot.MainGrid.QuickConf("", True, "2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue""/></BAND>"
        myViewCostLot.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostWBS.MainGrid.MainConf("FORMATXML") = "<COL KEY=""SerialNum"" CAPTION=""Serial No""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""WBSElemType"" CAPTION=""Element Type""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostWBS.MainGrid.BindGridData(Me.dsForm, 8)
        myViewCostWBS.MainGrid.QuickConf("", True, "2-2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue""/></BAND>"
        myViewCostWBS.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostCenter.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostCenterName"" CAPTION=""Cost Center Name""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostCenter.MainGrid.BindGridData(Me.dsForm, 9)
        myViewCostCenter.MainGrid.QuickConf("", True, "2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue""/></BAND>"
        myViewCostCenter.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        sql = "Select TourVouchID, TransferTourVouchID, EmployeeID, FullName, Dated, TotalPayment from slsListTourVouch() Where TransferTourVouchID = " & myUtils.cValTN(prepIDX) & " and isNull(isAdvance, 0) =1"
        myViewAdvReq.MainGrid.MainConf("FORMATXML") = "<COL KEY=""FullName"" CAPTION=""Employee Name""/>"
        myViewAdvReq.MainGrid.QuickConf(sql, True, "3-1-1")
        myViewAdvReq.MainGrid.myDS.Tables(0).TableName = "TourVouchAdv"


        objTourVouchProc.PopulatePreBalanceAdv(myViewAdv.MainGrid.myDS.Tables(0).Select, "TourVouchID", frmIDX, "AdvanceVouchID")
        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "TourVouchID", myUtils.cValTN(myRow("TourVouchID"))), GetType(Boolean), False))

        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal TourVouchID As Integer)
        Dim ds As DataSet, Sql, Sql1, Sql2, Sql3, Sql4, Sql5, Sql6, Sql7, Sql8, Sql9 As String

        Sql1 = "Select TourJourneyID, TourVouchID, StationFrom, DateFrom, TimeFrom, StationTo, DateTo, TimeTo, TravelMode, Amount, Remark from TourJourney  Where TourVouchID = " & TourVouchID
        Sql2 = "Select TourAllowID, TourVouchID, Station, DaysNum, DayRate from TourAllow Where TourVouchID = " & TourVouchID
        Sql3 = "Select TourHotelID, TourVouchID, HotelName, BillNum, DaysNum, Amount from TourHotel  Where TourVouchID = " & TourVouchID
        Sql4 = "Select TourLoConvID, TourVouchID, Dated, FromPoint, ToPoint, Mode, Amount, WithWhomMet from TourLoConv  Where TourVouchID = " & TourVouchID
        Sql5 = "Select TourMiscExpID, TourVouchID, ClassType, Particulars, TransType, ExpClass, Amount from TourMiscExp  Where TourVouchID = " & TourVouchID
        Sql6 = "Select TourVouchItemID, TourVouchID, ImprestEmployeeID,CashCampusID, Amount, Dated from TourVouchItem Where TourVouchID  = " & TourVouchID & " and (ImprestEmployeeID is Not NULL or CashCampusID is Not NULL)"
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, Null as TourMiscExpID, Null as TourJourneyID, Null as TourAllowID, Null as TourHotelID, Null as TourLoConvID, Null as TourVouchItemID, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'TourVouchID' and PIDValue = " & TourVouchID
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, Null as TourMiscExpID, Null as TourJourneyID, Null as TourAllowID, Null as TourHotelID, Null as TourLoConvID, Null as TourVouchItemID, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'TourVouchID' and PIDValue = " & TourVouchID
        Sql9 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, Null as TourMiscExpID, Null as TourJourneyID, Null as TourAllowID, Null as TourHotelID, Null as TourLoConvID, Null as TourVouchItemID, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'TourVouchID' and PIDValue = " & TourVouchID

        Sql = Sql1 & ";" & Sql2 & ";" & Sql3 & ";" & Sql4 & ";" & Sql5 & ";" & Sql6 & ";" & Sql7 & ";" & Sql8 & ";" & Sql9
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "TourJourney", 0)
        myUtils.AddTable(Me.dsForm, ds, "TourAllow", 1)
        myUtils.AddTable(Me.dsForm, ds, "TourHotel", 2)
        myUtils.AddTable(Me.dsForm, ds, "TourLoConv", 3)
        myUtils.AddTable(Me.dsForm, ds, "TourMiscExp", 4)
        myUtils.AddTable(Me.dsForm, ds, "TourVouchItem", 5)
        myUtils.AddTable(Me.dsForm, ds, "CostLot", 6)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 7)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 8)


        SetIDValue()
        SetAuto(Me.dsForm.Tables("TourJourney"), "TourJourneyID", 0)
        SetAuto(Me.dsForm.Tables("TourAllow"), "TourAllowID", 1)
        SetAuto(Me.dsForm.Tables("TourHotel"), "TourHotelID", 2)
        SetAuto(Me.dsForm.Tables("TourLoConv"), "TourLoConvID", 3)
        SetAuto(Me.dsForm.Tables("TourMiscExp"), "TourMiscExpID", 4)
        SetAuto(Me.dsForm.Tables("TourVouchItem"), "TourVouchItemID", 5)
    End Sub

    Private Sub SetIDValue()
        For Each dt As DataTable In New DataTable() {Me.dsForm.Tables("CostLot"), Me.dsForm.Tables("CostWBS"), Me.dsForm.Tables("CostCenter")}
            For Each r2 As DataRow In dt.Select
                r2(r2("pIDFieldItem")) = r2("pIDValueItem")
            Next
        Next
    End Sub

    Private Sub SetAuto(dt As DataTable, IDField As String, Count As Integer)
        myContext.Tables.SetAuto(dt, Me.dsForm.Tables("CostLot"), IDField, "_CostLot" & Count)
        myContext.Tables.SetAuto(dt, Me.dsForm.Tables("CostWBS"), IDField, "_CostWBS" & Count)
        myContext.Tables.SetAuto(dt, Me.dsForm.Tables("CostCenter"), IDField, "_CostCenter" & Count)
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If Me.SelectedRow("CampusID") Is Nothing Then Me.AddError("CampusID", "Please Select Campus")
        If Me.SelectedRow("EmployeeID") Is Nothing Then Me.AddError("EmployeeID", "Please Select Employee")
        If myUtils.cValTN(myRow("TotalAmount")) = 0 Then Me.AddError("TotalAmount", "Please Enter Some Transaction")
        If myUtils.cValTN(myRow("TotalPayment")) < 0 Then Me.AddError("TotalPayment", "Payment can not be greater from Total Amount")

        If myUtils.cValTN(myRow("CampusID")) > 0 Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")


        If myRow("PostingDate") < myRow("Dated") Then Me.AddError("PostingDate", "Posting Date can not be less then Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("Dated") > Now.Date Then Me.AddError("Dated", "Please Select Valid Date.")

        If myUtils.cBoolTN(myRow("IsOpening")) = True AndAlso myRow("Dated") >= Me.SelectedRow("CampusID")("FinStartDate") Then
            Me.AddError("Dated", "Opening Date shaould be less then Company Start Date")
        End If

        If myUtils.cValTN(myRow("PaymentID")) > 0 Then Me.AddError("", "Payment Done. Not allow to edit")

        If Me.dsForm.Tables("TourJourney").Select.Length > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("TourJourney"), "TourJourneyID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct % for Journey Detail")
        If Me.dsForm.Tables("TourAllow").Select.Length > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("TourAllow"), "TourAllowID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct % for DA.")
        If Me.dsForm.Tables("TourHotel").Select.Length > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("TourHotel"), "TourHotelID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct % for Hotel.")
        If Me.dsForm.Tables("TourLoConv").Select.Length > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("TourLoConv"), "TourLoConvID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct % for Local Conveyance.")
        If Me.dsForm.Tables("TourMiscExp").Select.Length > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("TourMiscExp"), "TourMiscExpID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct % for Miscellaneous\Material\Asset.")
        If Me.dsForm.Tables("TourVouchItem").Select.Length > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("TourVouchItem"), "TourVouchItemID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct % for Imprest\Office.")

        If CheckAmountImpReq() = False Then Me.AddError("", "Imprest Trasfer should be equal to Advance for Expense.")

        Return Me.CanSave
    End Function

    Private Function CheckAmountImpReq() As Boolean
        Dim bool As Boolean = True
        For Each r1 In myViewTRI.MainGrid.myDV.Table.Select
            If Not myFuncs.IgnoreExpenseVoucher(myContext, myUtils.cValTN(r1("ImprestEmployeeID"))) Then
                If myUtils.cValTN(r1("Amount")) <> myUtils.cValTN(myViewAdvReq.MainGrid.GetColSum("TotalPayment", "EmployeeID = " & myUtils.cValTN(r1("ImprestEmployeeID")) & "")) Then
                    bool = False
                    Exit For
                End If
            End If
        Next
        Return bool
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            ObjVouch.GetNewSerialNo("TourVouchID", "0", myRow.Row)
            myRow("IsAdvance") = False
            Dim TourVouchDescrip As String = " Expense Voucher for: " & myRow("VouchNum").ToString & ", Dt. " & Format(myRow("Dated"), "dd-MMM-yyyy")

            Try
                If myUtils.cBoolTN(myRow("IsOpening")) Then myRow("TotalPayment") = myRow("TotalAmount")
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "TourVouchID", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)

                frmIDX = myRow("TourVouchID")

                myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDV.Table, "Select TourJourneyID, TourVouchID, StationFrom, DateFrom, TimeFrom, StationTo, DateTo, TimeTo, TravelMode, Amount, Remark from TourJourney", True)

                myUtils.ChangeAll(myViewDD.MainGrid.myDV.Table.Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewDD.MainGrid.myDV.Table, "Select TourAllowID, TourVouchID, Station, DaysNum, DayRate from TourAllow", True)

                myUtils.ChangeAll(myViewHE.MainGrid.myDV.Table.Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewHE.MainGrid.myDV.Table, "Select TourHotelID, TourVouchID, Amount, HotelName,DaysNum,BillNum from TourHotel", True)

                myUtils.ChangeAll(myViewLC.MainGrid.myDV.Table.Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewLC.MainGrid.myDV.Table, "Select TourLoConvID, TourVouchID, FromPoint, ToPoint,  Amount, Dated, WithWhomMet, Mode from TourLoConv", True)

                myUtils.ChangeAll(myViewMES.MainGrid.myDV.Table.Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewMES.MainGrid.myDV.Table, "Select TourMiscExpID,TourVouchID,Particulars,Amount,ClassType,TransType,ExpClass from TourMiscExp", True)

                myUtils.ChangeAll(myViewMEA.MainGrid.myDV.Table.Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewMEA.MainGrid.myDV.Table, "Select TourMiscExpID,TourVouchID,Particulars,Amount,ClassType,TransType,ExpClass from TourMiscExp", True)

                myUtils.ChangeAll(myViewMEM.MainGrid.myDV.Table.Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewMEM.MainGrid.myDV.Table, "Select TourMiscExpID,TourVouchID,Particulars,Amount,ClassType,TransType,ExpClass from TourMiscExp", True)

                myUtils.ChangeAll(myViewAdv.MainGrid.myDS.Tables(0).Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewAdv.MainGrid.myDS.Tables(0), "Select TourVouchItemID, TourVouchID, AdvanceVouchID, Amount from TourVouchItem")


                myUtils.ChangeAll(Me.dsForm.Tables("TourVouchItem").Select, "TourVouchID=" & frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(Me.dsForm.Tables("TourVouchItem"), "Select TourVouchItemID, TourVouchID, ImprestEmployeeID, CashCampusID, Amount, Dated from TourVouchItem")


                ChangeColRowwise(myViewCostLot.MainGrid.myDV.Table, frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewCostLot.MainGrid.myDV.Table, "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                ChangeColRowwise(myViewCostWBS.MainGrid.myDV.Table, frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewCostWBS.MainGrid.myDV.Table, "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                ChangeColRowwise(myViewCostCenter.MainGrid.myDV.Table, frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myViewCostCenter.MainGrid.myDV.Table, "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)




                myUtils.ChangeAll(myViewAdvReq.MainGrid.myDS.Tables(0).Select, " TransferTourVouchID =" & frmIDX)
                Dim TourVouchIDCSV As String = myUtils.MakeCSV(",", myUtils.MakeCSV(myViewAdvReq.MainGrid.myDS.Tables(0).Select(), "TourVouchID"))

                Dim Sql As String = "Update TourVouch set TransferTourVouchID=Null where TransferTourVouchID=" & frmIDX & " and TourVouchID not in (" & TourVouchIDCSV & ")"
                myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)

                Sql = "Update TourVouch set TransferTourVouchID=" & frmIDX & " where TourVouchID in (" & TourVouchIDCSV & ")"
                myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, Sql)



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

    Private Sub ChangeColRowwise(dt As DataTable, TourVouchID As Integer)
        myUtils.ChangeAll(dt.Select, "PIDField=TourVouchID")
        myUtils.ChangeAll(dt.Select, "PIDValue=" & TourVouchID)

        For Each r1 In dt.Select
            If myUtils.cValTN(r1("TourJourneyID")) > 0 Then r1("pIDFieldItem") = "TourJourneyID"
            If myUtils.cValTN(r1("TourAllowID")) > 0 Then r1("pIDFieldItem") = "TourAllowID"
            If myUtils.cValTN(r1("TourHotelID")) > 0 Then r1("pIDFieldItem") = "TourHotelID"
            If myUtils.cValTN(r1("TourLoConvID")) > 0 Then r1("pIDFieldItem") = "TourLoConvID"
            If myUtils.cValTN(r1("TourMiscExpID")) > 0 Then r1("pIDFieldItem") = "TourMiscExpID"
            If myUtils.cValTN(r1("TourVouchItemID")) > 0 Then r1("pIDFieldItem") = "TourVouchItemID"

            If myUtils.cValTN(r1("TourJourneyID")) > 0 Then r1("pIDValueItem") = r1("TourJourneyID")
            If myUtils.cValTN(r1("TourAllowID")) > 0 Then r1("pIDValueItem") = r1("TourAllowID")
            If myUtils.cValTN(r1("TourHotelID")) > 0 Then r1("pIDValueItem") = r1("TourHotelID")
            If myUtils.cValTN(r1("TourLoConvID")) > 0 Then r1("pIDValueItem") = r1("TourLoConvID")
            If myUtils.cValTN(r1("TourMiscExpID")) > 0 Then r1("pIDValueItem") = r1("TourMiscExpID")
            If myUtils.cValTN(r1("TourVouchItemID")) > 0 Then r1("pIDValueItem") = r1("TourVouchItemID")
        Next
    End Sub

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "tourvouch"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("CompanyID  = @companyid and (isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) and EmployeeID = @employeeid and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1 and PostingDate <= '@Dated'", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ENT=""TA""/>", True, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(Sql) & "</SQLWHERE2></MODROW>")
                Case "employee"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str1 As String = myFuncs.FilterTimeDependent(Dated, "JoinDate", "LeaveDate", 12)
                    Dim sql1 As String = myUtils.CombineWhere(False, str1, "Imprestenabled = 1")


                    Dim Sql As String = "select EmployeeID, EmpCode, FullName, Designation, DepName from hrpListAllEmp() where " & sql1 & " order by FullName"
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""EmpCode"" CAPTION=""Employee Code""/><COL KEY=""FullName"" CAPTION=""Employee Name""/><COL KEY=""DepName"" CAPTION=""Department""/>"
                    Model.MainGrid.QuickConf(Sql, True, "1-2-1-1", True, "Employee List")

                Case "campus"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim str1 As String = myFuncs.FieldFilter(myContext, Me.fRow, Dated, "WODate", "CompletedOn", "CampusID", 12)

                    Dim Sql As String = "Select CampusID, DispName, CampusAddress from mmlistCampus()  where " & str1 & " order by DispName"
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""DispName"" CAPTION=""Campus""/><COL KEY=""CampusAddress"" CAPTION=""Address""/>"
                    Model.MainGrid.QuickConf(Sql, True, "1-1", True, "Campus List")
                Case "costlot"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myContext.CommonData.GetCompanyIDFromCampus(myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params)))
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CompanyID = " & CompanyID & " and WODate <= '" & Format(Dated, "dd-MMM-yyyy") & "' and ((EDate is Null) or (EDate >= '" & Format(Dated, "dd-MMM-yyyy") & "'))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdLotID""/>", True,, "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "costwbs"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myContext.CommonData.GetCompanyIDFromCampus(myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params)))
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CompanyID = " & CompanyID & " and StartTime <= '" & Format(Dated, "dd-MMM-yyyy") & "' and ((FinishTime is Null) or (FinishTime >= '" & Format(Dated, "dd-MMM-yyyy") & "'))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""WBSElementID""/>", True,, "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2><SQLWHERE></SQLWHERE></MODROW>")
                Case "costcenter"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim CompanyID As Integer = myContext.CommonData.GetCompanyIDFromCampus(myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params)))
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CompanyID = " & CompanyID & " and Sdate <= '" & Format(Dated, "dd-MMM-yyyy") & "' and ((EDate is Null) or (EDate >= '" & Format(Dated, "dd-MMM-yyyy") & "'))", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""CostCenterID""/>", True,, "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")

                Case "advreq"
                    Dim Dated As Date = myContext.SQL.ParamValue("@Dated", Params)
                    Dim Sql1 As String = myContext.SQL.PopulateSQLParams("Select TourVouchID,EmployeeID,TransferTourVouchID,CompanyID, DivisionID, TotalPayment, FullName,Dated,VouchNum,PaymentVouchNum,TotalAmount,BalanceDate,BalanceAmount,Remark from slsListTourVouch() where ((PaymentID is NULL and TransferTourVouchID is Null and isnull(isopening,0) = 0 and isNull(BalanceAmount,0) > 0) or TransferTourVouchID = @TransferTourVouchID) and CompanyID = @CompanyID and EmployeeID = @EmployeeID and IsAdvance = 1 and isnull(IsProcessed,0) = 1 and TourVouchID not in (@tourvouchidcsv) and PostingDate <= '@Dated'", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.QuickConf(Sql1, True, "1.5-.9-.7-.7-1-.9-1-1")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Sub PopulateData(DataKey As String, dt As DataTable, Params As List(Of clsSQLParam))
        Dim ID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ID", Params))
        Select Case DataKey.Trim.ToLower
            Case "generateprebal"
                objTourVouchProc.PopulatePreBalanceAdv(dt.Select, "TourVouchID", ID, "TourVouchID")
        End Select
    End Sub
End Class
