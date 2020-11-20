Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmInvoiceHRCModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False, myViewVouch, myViewCostLot, myViewCostWBS, myViewCostCenter As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
        myViewVouch = Me.GetViewModel("Vouchers")
        myViewCostLot = Me.GetViewModel("CostLot")
        myViewCostWBS = Me.GetViewModel("CostWBS")
        myViewCostCenter = Me.GetViewModel("CostCenter")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, CompanyID,TaxAreaCode, DivisionCodeList, WODate, CompletedOn,TaxAreaID, gstregid,CampusCode from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("DeliveryCampusID", "Campus")

        sql = "SELECT  VendorID, VendorName, VendorClass,TaxAreaCode, GSTIN, ImportAllow FROM  purListVendor() where VendorType = 'HC' Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass = 'Service' and CodeWord Not in ('SUN')  Order by CodeClass "
        Me.AddLookupField("InvoiceItem", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransType").TableName)

        sql = "Select Class as ClassCode, Class, ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass Where Class = 'LABOUR'"
        Me.AddLookupField("InvoiceItem", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "CodeWord in ('S')")
        Me.AddLookupField("InvoiceItem", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        sql = "select TaxAreaID, Descrip,TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", "Tag2 = 'IP'")
        Me.AddLookupField("GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "GSTTypecode", "Tag2= 'IP'")
        Me.AddLookupField("GSTInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = "Select Code, Code + '-' + Description as Description, Ty from HsnSac Order by Code"
        Me.AddLookupField("Hsn_Sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass, Tag from CodeWords  where CodeClass = 'Invoice' and CodeType in ('B2B',  'CDN')  Order by CodeClass"
        Me.AddLookupField("GSTInvoiceSubType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceSubType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxCredit", "")
        Me.AddLookupField("TaxCredit", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxCredit").TableName)

        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("InvoiceItem", "ItemUnitID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

        sql = "Select * from systemoptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objIVProcHC As New clsIVProcHC(myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
        Else
            Dim rPostPeriod As DataRow = objIVProcHC.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        Me.BindDataTable(myUtils.cValTN(prepIDX))
        objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("InvoiceItem"))

        objIVProcHC.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""QtyRate"" CAPTION=""Qty""/>"
        myView.MainGrid.QuickConf("", True, "2-1-1-1", True)
        Dim str1 As String = "<BAND INDEX = ""0"" TABLE = ""InvoiceItem"" IDFIELD=""InvoiceItemID""><COL KEY ="" InvoiceItemID, InvoiceID, ClassType, TransType, ValuationClass, PriceSlabID, BasicRate, AmountBasic, Description, AmountTot, AmountWV,TaxCredit,ItemUnitID""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        myViewVouch.MainGrid.BindGridData(Me.dsForm, 2)
        myViewVouch.MainGrid.QuickConf("", True, "2-2-2-1", True)

        myViewCostLot.MainGrid.MainConf("FORMATXML") = "<COL KEY=""LotNum"" CAPTION=""Lot No.""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostLot.MainGrid.BindGridData(Me.dsForm, 5)
        myViewCostLot.MainGrid.QuickConf("", True, "2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, TenantID, PIDField, PIDValue, ProdLotID, PerValue""/></BAND>"
        myViewCostLot.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostWBS.MainGrid.MainConf("FORMATXML") = "<COL KEY=""SerialNum"" CAPTION=""Serial No""/><COL KEY=""WoInfo"" CAPTION=""Work Order""/><COL KEY=""WBSElemType"" CAPTION=""Element Type""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostWBS.MainGrid.BindGridData(Me.dsForm, 6)
        myViewCostWBS.MainGrid.QuickConf("", True, "2-2-2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, TenantID, PIDField, PIDValue, WBSElementID, PerValue""/></BAND>"
        myViewCostWBS.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewCostCenter.MainGrid.MainConf("FORMATXML") = "<COL KEY=""CostCenterName"" CAPTION=""Cost Center Name""/><COL KEY=""PerValue"" CAPTION=""% Value""/>"
        myViewCostCenter.MainGrid.BindGridData(Me.dsForm, 7)
        myViewCostCenter.MainGrid.QuickConf("", True, "2-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""CostAssign"" IDFIELD=""CostAssignID""><COL KEY =""CostAssignID, TenantID, PIDField, PIDValue, CostCenterID, PerValue""/></BAND>"
        myViewCostCenter.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))
        objPricingCalcBase.CheckPriceSlabElement()
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal InvoiceID As Integer)
        Dim ds As DataSet, Sql, Sql1, Sql3, Sql4, Sql5, Sql6, Sql7, Sql8 As String

        Sql1 = "Select InvoiceItemID, PriceSlabID, PPSubType,HSN_SC, BasicRate, ItemUnitID, AmountBasic, ClassType, TransType, TaxCredit, ValuationClass,InvoiceID, Description, QtyRate, AmountTot, AmountWV from InvoiceItem Where InvoiceID = " & InvoiceID & " "
        Sql3 = "Select PaymentHRVouchID, InvoiceID, VouchDate, VouchTypeDescrip,  VouchDescrip,  Payee, TotalAmount from hrpListPaymentHRVouch() where InvoiceID = " & InvoiceID & " "
        Sql4 = "Select * from InvoiceItemGST Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql5 = "Select * from InvoiceGstCalc where invoiceid = " & InvoiceID
        Sql6 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as InvoiceItemID, CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'InvoiceID' and PIDValue = " & InvoiceID & ""
        Sql = Sql1 & ";" & Sql3 & ";" & Sql4 & ";" & Sql5 & ";" & Sql6 & ";" & Sql7 & ";" & Sql8
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)


        myUtils.AddTable(Me.dsForm, ds, "InvoiceItem", 0)
        myUtils.AddTable(Me.dsForm, ds, "PaymentHRVouch", 1)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGST", 2)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceGstCalc", 3)
        myUtils.AddTable(Me.dsForm, ds, "CostLot", 4)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 5)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 6)
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("InvoiceItemGST"), "InvoiceItemID", "_InvoiceItemGST")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("CostLot"), "InvoiceItemID", "_CostLot")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("CostWBS"), "InvoiceItemID", "_CostWBS")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("CostCenter"), "InvoiceItemID", "_CostCenter")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Contractor")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If myRow("InvoiceNum").ToString.Trim.Length = 0 Then Me.AddError("InvoiceNum", "Enter Invoice No.")
        If Me.myView.MainGrid.myDV.Table.Rows.Count = 0 Then Me.AddError("", "Please Enter Some Transactions")
        If Me.myViewVouch.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Vouchers")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso Me.SelectedRow("GSTInvoiceType") Is Nothing Then Me.AddError("GSTInvoiceType", "Please select GST Invoice Type")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2B", "CDN", "CDNUR") AndAlso Me.SelectedRow("GSTInvoiceSubType") Is Nothing Then Me.AddError("GSTInvoiceSubType", "Please select GST Invoice Sub Type")
        If Not Me.SelectedRow("CampusID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")

        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) < myUtils.cValTN(Me.myRow("TDSAmount")) Then Me.AddError("TDSAmount", "'TDS Base Amount' should be greater than 'TDS Amount'")
        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) > 0 AndAlso myUtils.cValTN(Me.myRow("TDSAmount")) = 0 Then
            Me.AddError("TDSAmount", "Please enter 'TDS Amount' or Remove 'TDS Base Amount'")
        End If

        If CheckTotalAmt() = False Then Me.AddError("", "Vouchers Amount Should be equal to Items Amount")

        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")

        Dim oRet = myFuncs.CheckZeroTaxType(myUtils.cStrTN(myRow("GSTInvoiceSubType")), dsForm.Tables("InvoiceItemGST"))
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not oRet.Success) Then Me.AddError("", oRet.Message)

        If myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables("InvoiceItem"), "InvoiceItemID", myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct %.")
        Return Me.CanSave
    End Function

    Private Function CheckTotalAmt() As Boolean
        Dim TotAmt, BasicAmt As Decimal
        TotAmt = myUtils.cValTN(myViewVouch.MainGrid.GetColSum("TotalAmount", "InvoiceID is Not NULL"))
        BasicAmt = myUtils.cValTN(myView.MainGrid.GetColSum("AmountBasic"))
        If myUtils.cValTN(TotAmt) = myUtils.cValTN(BasicAmt) Then
            CheckTotalAmt = True
        Else
            CheckTotalAmt = False
        End If
        Return CheckTotalAmt
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("InvoiceNum", "Accounting entry passed. Invoice can't be update.")
            End If

            If myFuncs.CheckPayment(myContext, myUtils.cValTN(myRow("InvoiceID"))) Then
                Me.AddError("", "Payment done. Not allow to edit")
            End If

            myRow("InvoiceTypeCode") = "PH"
            myRow("Billof") = "P"
            If Not Me.SelectedRow("DeliveryCampusID") Is Nothing Then
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("DeliveryCampusID")("TaxAreaID"))
            Else
                myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CampusID")("TaxAreaID"))
            End If

            myFuncs.SetPreGST(myContext, myRow.Row)
            If myUtils.IsInList(myRow("P_gst"), "y") Then
                myRow("GSTInvoiceType") = DBNull.Value
                myRow("GSTInvoiceSubType") = DBNull.Value
            End If

            Dim oProc As New clsGSTInvoicePurch(myContext)
            oProc.CalcVouchActionRP(Me.SelectedRow("CampusID")("gstregid"), myRow("postperiodid"), myRow.Row)
            myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))

            Dim objIVProcHC As New clsIVProcHC(myContext)
            objIVProcHC.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))
            If Me.SelectedRow("CampusID") Is Nothing OrElse objIVProcHC.IsVouchDateAfterFinStart(myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), myRow("PostingDate")) = False Then
                Me.AddError("PostingDate", "Posting Date can not be less then Company Start Date.")
            End If

            myRow("DocType") = objIVProcHC.DocType
            myRow("TY") = myFuncs.SetTY(dsForm.Tables("InvoiceItem"))
            If objIVProcHC.GetInvoiceTypeID(myRow.Row) = False Then Me.AddError("", "Combination Not Available")
            objIVProcHC.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("InvoiceItem"))

            Dim rr1() As DataRow = dsForm.Tables("InvoiceItem").Select("ValuationClass = 'OutStanding'")
            If rr1.Length > 0 Then
                myRow("HasOutStanding") = 1
            Else
                myRow("HasOutStanding") = DBNull.Value
            End If

            If Me.CanSave Then
                Dim objPricingCalcBase As New clsPricingCalcBase(myContext)
                objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("InvoiceItem"))
                objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "AmountWV", "")
                For Each r2 As DataRow In dsForm.Tables("InvoiceItem").Select()
                    objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "AmountBasic", "")
                Next
                objPricingCalcBase.PopulateAccountingKeys(myRow("postingdate"))

                Dim InvoiceDescrip As String = myRow("InvoiceNum").ToString & " Dt. " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "Select * from Invoice Where InvoiceID = " & frmIDX)
                    frmIDX = myRow("InvoiceID")

                    myUtils.ChangeAll(dsForm.Tables("InvoiceItem").Select, "InvoiceID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItem"), "Select InvoiceItemID, InvoiceID, PriceSlabID, PPSubType, BasicRate,ItemUnitID, AmountBasic, ClassType, TransType, ValuationClass, Description, QtyRate, AmountTot, AmountWV, TaxCredit from InvoiceItem")

                    dsForm.Tables("paymenthrvouch").AcceptChanges()
                    myUtils.ChangeAll(dsForm.Tables("PaymentHRVouch").Select(), "InvoiceID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PaymentHRVouch"), "Select PaymentHRVouchID, InvoiceID from PaymentHRVouch")

                    Dim PaymentHRVouchIDCSV As String = myUtils.MakeCSV(dsForm.Tables("paymenthrvouch").Select(), "PaymentHRVouchID")
                    Dim sql As String = "Update PaymentHRVouch set InvoiceId = Null where InvoiceId=" & frmIDX & " and PaymentHRVouchID not in (" & PaymentHRVouchIDCSV & ")"
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(myContext.Provider.dbConn, CommandType.Text, sql)

                    If Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("P_gst")), "Y") Then
                        myUtils.ChangeAll(dsForm.Tables("InvoiceItemGST").Select, "InvoiceID=" & frmIDX)
                        myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "InvoiceID=" & frmIDX)

                        oProc.PopulateCalc(frmIDX, myRow.Row, Me.SelectedRow("CampusID"), dsForm.Tables("InvoiceItemGST"), dsForm.Tables("InvoiceGstCalc"), Nothing, Nothing, Me.dsCombo)

                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGST"), "Select * from InvoiceItemGST", True)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")
                    End If


                    ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)

                    objPricingCalcBase.VSave()
                    objIVProcHC.SavePaymentHRVouch(dsForm.Tables("PaymentHRVouch"))
                    Dim oRet As clsProcOutput = objIVProcHC.HandleWorkflowState(myRow.Row)
                    If oRet.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoiceDescrip, frmIDX)
                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoiceDescrip, oRet.Message)
                        Me.AddError("", oRet.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Private Sub ChangeColRowwise(dt As DataTable, InvoiceID As Integer)
        myUtils.ChangeAll(dt.Select, "PIDField=InvoiceID")
        myUtils.ChangeAll(dt.Select, "PIDValue=" & InvoiceID)
        myUtils.ChangeAll(dt.Select, "pIDFieldItem=InvoiceItemID")

        For Each r1 In dt.Select
            r1("pIDValueItem") = r1("InvoiceItemID")
        Next
    End Sub

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "paymenthrvouch"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CompanyID = @companyid  and ContractorID = @contractorid and (InvoiceId is Null or InvoiceID = @invoiceid) and PayDueType = 'I'  and PaymentHRVouchID Not in (@paymenthrvouchidcsv)", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PaymentHRVouchID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
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
            End Select
        End If
        Return Model
    End Function

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
