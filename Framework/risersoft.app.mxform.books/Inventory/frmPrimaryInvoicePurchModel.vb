Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.app.mxform.gst

<DataContract>
Public Class frmPrimaryInvoicePurchModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False
    Dim myViewItemGST, myViewAcc As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        myViewItemGST = Me.GetViewModel("ItemGST")
        myViewAcc = Me.GetViewModel("Acc")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, CompanyID, DivisionCodeList, WODate, CompletedOn,TaxAreaID, GstRegID, CampusCode from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("DeliveryCampusID", "Campus")

        sql = "SELECT  CustomerID, CustDescrip, CustomerClass, TaxAreaCode,GSTIN,TaxAreaID FROM  slsListCustomer() where CustomerID in (Select CustomerID from invListMatVouch() where VouchTypeCode = 'GR')  Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = "SELECT  VendorID, VendorName, VendorClass, TaxAreaCode, GSTIN, VendorType FROM  purListVendor() where (VendorType in ('MS','EM') or (PartyType = 'A')) Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", "Tag2 = 'IP'")
        Me.AddLookupField("GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "GSTTypecode", "Tag2= 'IP'")
        Me.AddLookupField("GSTInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = "Select Code, Code + '-' + Description as Description, Ty from HsnSac Order by Code"
        Me.AddLookupField("Hsn_Sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass, Tag from CodeWords  where CodeClass = 'Invoice' and CodeType in ('B2B', 'CDN')  Order by CodeClass"
        Me.AddLookupField("GSTInvoiceSubType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceSubType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

        sql = "Select * from systemoptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql, str1 As String, ObjIVProc As New clsIVProcBase("IP", myContext)

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "InvoiceTypeCode,MatVouchID", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
        Else
            Dim rPostPeriod As DataRow = ObjIVProc.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QC") Then
            myRow("Billof") = "C"
        Else
            myRow("Billof") = "P"
        End If

        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myView.MainGrid.MainConf("formatxml") = "<COL KEY=""VouchNum"" CAPTION=""Voucher No""/><COL KEY=""VouchDate"" CAPTION=""Voucher Date""/>"
        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""InvoiceMV"" IDFIELD=""InvoiceMVID""><COL KEY ="" InvoiceMVId, InvoiceID, MatVouchID""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        myViewItemGST.MainGrid.MainConf("rhfactor") = 2
        myViewItemGST.MainGrid.MainConf("formatxml") = "<COL KEY=""TY"" CAPTION=""Item Type""/><COL KEY=""RT"" CAPTION=""Tax Rate""/><COL KEY=""TXVAL"" CAPTION=""Taxable Value""/><COL KEY=""IAMT"" CAPTION=""IGST""/><COL KEY=""CAMT"" CAPTION=""CGST""/><COL KEY=""SAMT"" CAPTION=""SGST""/><COL KEY=""CSAMT"" CAPTION=""CESS""/><COL KEY=""tx_i"" CAPTION=""IGST Credit""/><COL KEY=""tx_c"" CAPTION=""CGST Credit""/><COL KEY=""tx_s"" CAPTION=""SGST Credit""/><COL KEY=""tx_cs"" CAPTION=""CESS Credit""/><COL KEY=""ClassType"" CAPTION=""Class Type""/><COL KEY=""StockStage"" CAPTION=""Stock Stage""/><COL KEY=""TransType"" CAPTION=""Trans Type""/><COL KEY=""ValuationClass"" CAPTION=""Valuation Class""/><COL KEY=""GSTR3B31Section"" CAPTION=""GSTR3B31 Section""/><COL KEY=""GSTR3B32Section"" CAPTION=""GSTR3B32 Section""/><COL KEY=""GSTR3B4ASection"" CAPTION=""GSTR3B4A Section""/><COL KEY=""GSTR3B4DSection"" CAPTION=""GSTR3B4D Section""/><COL KEY=""GSTR3B5Section"" CAPTION=""GSTR3B5 Section""/>"
        myViewItemGST.MainGrid.BindGridData(Me.dsForm, 2)
        myViewItemGST.MainGrid.QuickConf("", True, ".6-.6-.6-1.4-.6-.7-1-1-1-1-1-1-1-1-1-.9-.9-.9-.9-.9", True)


        myViewAcc.MainGrid.MainConf("formatxml") = "<COL KEY=""VoucherNum"" CAPTION=""Voucher No.""/><COL KEY=""VouchDate"" CAPTION=""Voucher Date""/><COL KEY=""AccCode"" CAPTION=""A/C Code""/><COL KEY=""AccName"" CAPTION=""A/C Name""/>"
        myViewAcc.MainGrid.QuickConf(GetSql(prepIDX), True, "1-1-1-3-1-1", True)


        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))

        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            Me.AddDataSet("CDNInv", "Select * from Invoice where InvoiceID = " & myUtils.cValTN(myRow("CDNInvoiceID")))
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal InvoiceID As Integer)
        Dim ds As DataSet, Sql, Sql1, Sql2, Sql5 As String
        Sql1 = "Select MatVouchID, 0 as StatusNum, InvoiceMVId, TaxAreaType, RCHRG, PriceProcID, InvoiceID, PInvoiceID, VouchNum, VouchDate, AmountWV, AmountTot from purListInvoiceMV() Where InvoiceID = " & InvoiceID
        Sql2 = "Select InvoiceItemGSTID,MatVouchID, InvoiceItemGST.MatVouchItemID, InvoiceID, GstTaxType,ZeroTax,ClassType,StockStage,TransType,ValuationClass, InvoiceItemGST.TY,  RT, TXVAL, IAMT, CAMT, SAMT, CSAMT, tx_i, tx_c, tx_s, tx_cs, GSTR3B31Section, GSTR3B32Section, GSTR3B4ASection,GSTR3B4DSection,GSTR3B5Section from InvoiceItemGST left join MatVouchItem on InvoiceItemGST.MatVouchItemID = MatVouchItem.MatVouchItemID left join PriceSlab on MatVouchItem.PriceSlabID = PriceSlab.PriceSlabID Where InvoiceID = " & InvoiceID
        Sql5 = "Select * from InvoiceGstCalc where invoiceid = " & InvoiceID
        Sql = Sql1 & "; " & Sql2 & "; " & Sql5
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "InvoiceMV", 0)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGST", 1)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceGstCalc", 2)
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "PM") AndAlso Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC") AndAlso Me.SelectedRow("CustomerID") Is Nothing Then Me.AddError("CustomerID", "Please select Customer")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso Me.SelectedRow("GSTInvoiceType") Is Nothing Then Me.AddError("GSTInvoiceType", "Please select GST Invoice Type")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2B", "CDN", "CDNUR") AndAlso Me.SelectedRow("GSTInvoiceSubType") Is Nothing Then Me.AddError("GSTInvoiceSubType", "Please select GST Invoice Sub Type")
        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("BillOf")), "P") Then
            If myRow("InvoiceNum").ToString.Trim.Length = 0 Then Me.AddError("InvoiceNum", "Enter Invoice No.")
        End If

        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) < myUtils.cValTN(Me.myRow("TDSAmount")) Then Me.AddError("TDSAmount", "'TDS Base Amount' should be greater than 'TDS Amount'")
        If myUtils.cValTN(Me.myRow("TDSBaseAmount")) > 0 AndAlso myUtils.cValTN(Me.myRow("TDSAmount")) = 0 Then
            Me.AddError("TDSAmount", "Please enter 'TDS Amount' or Remove 'TDS Base Amount'")
        End If

        If Not Me.SelectedRow("CampusID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")

        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "QD", "QC") Then
            If myUtils.cValTN(myRow("CDNInvoiceID")) = 0 Then Me.AddError("", "Please Select Original Invoice")
        End If

        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")

        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")


        Dim oRet = myFuncs.CheckZeroTaxType(myUtils.cStrTN(myRow("GSTInvoiceSubType")), dsForm.Tables("InvoiceItemGST"))
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not oRet.Success) Then Me.AddError("", oRet.Message)

        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "PM") AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "IMPG") Then
            If myUtils.NullNot(myRow("boe_num")) Then Me.AddError("boe_num", "Please Enter BOE No.")
            If myUtils.NullNot(myRow("boe_dt")) Then Me.AddError("boe_dt", "Please Select BOE Date")
            If myUtils.NullNot(myRow("boe_val")) Then Me.AddError("boe_val", "Please Enter BOE Value")
            If myUtils.NullNot(myRow("port_code")) Then Me.AddError("port_code", "Please Enter Port Code")
        End If

        Return Me.CanSave
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

            myFuncs.SetPreGST(myContext, myRow.Row)
            If myUtils.IsInList(myRow("P_gst"), "y") Then
                myRow("GSTInvoiceType") = DBNull.Value
                myRow("GSTInvoiceSubType") = DBNull.Value
            End If



            If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC") Then
                myRow("DocType") = "IS"
                If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 Then myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CustomerID")("TaxAreaID"))
            Else
                myRow("DocType") = "IP"
                If Not Me.SelectedRow("DeliveryCampusID") Is Nothing Then
                    myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("DeliveryCampusID")("TaxAreaID"))
                Else
                    myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CampusID")("TaxAreaID"))
                End If
            End If

            Dim ObjIVProc As New clsIVProcBase("IP", myContext)
            If Me.SelectedRow("CampusID") Is Nothing OrElse ObjIVProc.IsVouchDateAfterFinStart(myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), myRow("PostingDate")) = False Then
                Me.AddError("PostingDate", "Posting Date can not be less then Company Start Date.")
            End If
            ObjIVProc.DocType = myRow("doctype")
            If ObjIVProc.GetInvoiceTypeID(myRow.Row) = False Then Me.AddError("", "Combination Not Available")

            Dim oProc As New clsGSTInvoicePurch(myContext)
            oProc.CalcVouchActionRP(Me.SelectedRow("CampusID")("gstregid"), myRow("postperiodid"), myRow.Row)

            If Me.CanSave Then
                If myRow("BillOf") = "C" Then
                    Dim DocNumSysType As String = myFuncs.GetDocNumSysType(myContext, myUtils.cValTN(myRow("InvoiceTypeID")))
                    Dim ObjVouch As New clsVoucherNum(myContext)
                    ObjVouch.GetNewSerialNo("InvoiceID", DocNumSysType, myRow.Row)
                End If
                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))

                myRow("BalanceAmount") = myRow("AmountTot")
                myRow("BalanceDate") = myRow("InvoiceDate")
                myRow("TY") = "G"

                Dim InvoiceDescrip As String = " InvoiceNum: " & myRow("InvoiceNum").ToString & " Dt: " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    frmIDX = myRow("InvoiceID")

                    myUtils.ChangeAll(dsForm.Tables("InvoiceMV").Select, "InvoiceID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceMV"), "Select * from InvoiceMV")

                    myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select(), "PInvoiceID=" & frmIDX)
                    myUtils.ChangeAll(myView.MainGrid.myDV.Table.Select(), "StatusNum=2")
                    myContext.Provider.objSQLHelper.SaveResults(myView.MainGrid.myDV.Table, "Select MatVouchID,StatusNum, PInvoiceID from MatVouch")

                    Dim MatVouchID As String = myUtils.MakeCSV(myView.MainGrid.myDV.Table.Select, "MatVouchID")
                    Dim sql As String = "Update MatVouch set PInvoiceID=NULL, StatusNum = 1 where PInvoiceID = " & frmIDX & " and MatVouchID Not in (" & MatVouchID & ")"
                    myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql)

                    If Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("P_gst")), "Y") Then
                        myUtils.ChangeAll(dsForm.Tables("InvoiceItemGST").Select, "InvoiceID=" & frmIDX)
                        myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "InvoiceID=" & frmIDX)

                        Dim rCDN = oProc.GetFirstRow(Me.DatasetCollection, "cdninv")
                        oProc.PopulateCalc(frmIDX, myRow.Row, Me.SelectedRow("CampusID"), dsForm.Tables("InvoiceItemGST"), dsForm.Tables("InvoiceGstCalc"), rCDN, Nothing, Me.dsCombo)

                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGST"), "Select InvoiceItemGSTID, MatVouchItemID, InvoiceID, GstTaxType, TXVAL, TY,  RT, IAMT, CAMT, SAMT, CSAMT, tx_i, tx_c, tx_s, tx_cs, GSTR3B31Section, GSTR3B32Section, GSTR3B4ASection,GSTR3B4DSection,GSTR3B5Section from InvoiceItemGST", True)
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")

                    End If



                    Dim Oret As clsProcOutput = ObjIVProc.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoiceDescrip, frmIDX)
                        myViewAcc.MainGrid.QuickConf(GetSql(frmIDX), True, "1-1-1-3-1-1", True)

                        VSave = True
                    Else
                        myContext.Provider.dbConn.RollBackTransaction(InvoiceDescrip, Oret.Message)
                        Me.AddError("", Oret.Message)
                    End If
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(InvoiceDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, sqlWhere As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "matvouch"
                    Dim InvoiceID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@invoiceid", Params))
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@campusid", Params))
                    Dim VendorID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@vendorid", Params))
                    Dim CustomerID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@customerid", Params))
                    Dim MatVouchIDCSV As String = myContext.SQL.ParamValue("@matvouchidcsv", Params)
                    Dim InvoiceTypeCode As String = myUtils.cStrTN(myContext.SQL.ParamValue("@invoicetypecode", Params))
                    Dim DivisionID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@DivisionID", Params))
                    Dim PriceProcID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@priceprocid", Params))
                    Dim ObjIVProc As New clsIVProcBase("IP", myContext)
                    Dim strFilter1 As String = ObjIVProc.GenerateMatVouchFilter(InvoiceID, MatVouchIDCSV, InvoiceTypeCode)
                    Dim strFilter2 As String = "VouchTypeCode = 'GR'"
                    Dim strFilter3 As String = ""
                    If PriceProcID > 0 Then
                        strFilter3 = "PriceProcID = " & PriceProcID & ""
                    End If

                    Dim strFilter As String = myUtils.CombineWhere(False, strFilter1, strFilter2, strFilter3)
                    If myUtils.IsInList(InvoiceTypeCode, "PM") Then sqlWhere = ObjIVProc.GenerateMatVouchSQLWhere(CampusID, "V", VendorID, strFilter)
                    If myUtils.IsInList(InvoiceTypeCode, "QD", "QC") Then sqlWhere = ObjIVProc.GenerateMatVouchSQLWhere(CampusID, "C", CustomerID, strFilter)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""MatVouchID""/>", False, , "<MODROW><SQLWHERE2>" & sqlWhere & " and isnull(PriceSlabID,0) > 0 and Isnull(DivisionID,0) = " & DivisionID & "</SQLWHERE2></MODROW>")
                Case "invoice"
                    Dim InvoiceDate As Date = myContext.SQL.ParamValue("@InvoiceDate", Params)
                    Model = New clsViewModel(vwState, myContext)
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("DocType = 'IS' and InvoiceTypeCode in ( 'PM') and CustomerID = (@CustomerID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "' and InvoiceDate >= '" & Format(DateAdd("m", -18, InvoiceDate), "dd-MMM-yyyy") & "'", Params)
                    Dim Sql As String = "SELECT InvoiceID, InvoiceNum, InvoiceDate, AmountTot  From Invoice where " & sql1
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "1-1-1-1")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "rmatvouch"
                Dim Sql As String = "Select * from invlistmatvouch() Where MatVouchID = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
        End Select
        Return oRet
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "matvouchitem"
                    Dim MatVouchIDCSV As String = myUtils.cStrTN(myContext.SQL.ParamValue("@MatVouchIDCSV", Params))
                    Dim dic As New clsCollecString(Of String)
                    dic.Add("MatVouchItem", "select * from MatVouchItem where MatVouchID in (" & MatVouchIDCSV & ")")
                    dic.Add("PriceElemCalc", "select * from accListPriceElemCalc() where pIDField = 'MatVouchID' and pIDValue in (" & MatVouchIDCSV & ")")
                    oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
            End Select
        End If
        Return oRet
    End Function

    Private Function GetSql(InvoiceID As Integer) As String
        Dim Sql As String = "Select InvoiceID, AccVouchID, VoucherNum, VouchDate, AccCode, AccName, Type, Amount from accListVouchItem() where invoiceid = " & myUtils.cValTN(InvoiceID) & " and ISNULL(IsClearingAcc,0) = 0 Order By Type Desc, AccName"
        Return Sql
    End Function
End Class