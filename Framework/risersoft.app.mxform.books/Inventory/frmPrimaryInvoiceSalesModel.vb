Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
Imports risersoft.app.mxform.gst
<DataContract>
Public Class frmPrimaryInvoiceSalesModel
    Inherits clsFormDataModel
    Dim myViewODNote, myViewRec, myViewItemCredit, MyViewForms, MyViewPayments, myViewSerialSO As clsViewModel
    Dim PPFinal As Boolean = False

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("ItemList")
        MyViewForms = Me.GetViewModel("Forms")
        myViewODNote = Me.GetViewModel("ODNote")
        myViewRec = Me.GetViewModel("Rec")
        myViewItemCredit = Me.GetViewModel("ItemCredit")
        MyViewPayments = Me.GetViewModel("Payment")
        myViewSerialSO = Me.GetViewModel("SerialSO")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CampusID, DispName, Camp.CompanyID, DivisionCodeList, WODate, Camp.CompletedOn, TaxAreaID, GstRegID, Camp.SalesOrderID, PidUnitID, CampusCode, ProjectID from mmlistCampus() as Camp Left join SalesOrder on Camp.SalesOrderID = SalesOrder.SalesOrderID  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("deliverycampusid", "Campus")
        Me.AddLookupField("ProjectCampusID", "Campus")


        sql = "SELECT  VendorID, VendorName, VendorCode, VendorClass, GSTIN,TaxAreaCode, TaxAreaID FROM  purListVendor() where VendorType = 'MS' and VendorID in (Select VendorID from ODNote where ChallanType = 'PR') Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "SELECT  CustomerID, PartyName, CustomerCode, CustomerClass, MainPartyID, GSTIN,TaxAreaCode,TaxAreaID, PartyType FROM slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Customer").TableName)

        sql = "Select PartyID, PartyName, TaxAreaID from Party order by PartyName"
        Me.AddLookupField("ConsigneeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Party").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxType", "")
        Me.AddLookupField("TaxInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxInvoiceType").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)

        sql = "select TaxAreaID, Descrip,TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "POS").TableName)

        '----------------Item---------------
        sql = "Select Class as ClassCode, Class, ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass Where ClassType = 'M' or (ClassType = 'S' and ClassSubType in ('S','B')) or ClassType = 'A' Order by Class"
        Me.AddLookupField("InvoiceItem", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = "Select CodeWord, DescripShort from CodeWords where CodeClass = 'Invoice' and CodeType = 'ItemType'  Order By DescripShort"
        Me.AddLookupField("InvoiceItem", "InvoiceItemType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "InvoiceItemType").TableName)

        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "(CodeWord = 'A' or CodeWord = 'M' or CodeWord = 'S')")
        Me.AddLookupField("InvoiceItem", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Material', 'Asset', 'Service') and CodeType <> 'Type' and CodeWord in ('ASN', 'RM','CG','CT','ST','WIP','FG','SUN')  Order by CodeClass "
        Me.AddLookupField("InvoiceItem", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "ZeroTax", "Tag = 'IS'")
        Me.AddLookupField("GstTaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ZeroTax").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "GSTTypecode", "Tag= 'IS'")
        Me.AddLookupField("GSTInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TY", "")
        Me.AddLookupField("TY", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TY").TableName)

        sql = "Select Code, Code + '-' + Description as Description, Ty from HsnSac Order by Code"
        Me.AddLookupField("Hsn_Sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "HsnSac").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass, Tag from CodeWords  where CodeClass = 'Invoice' and CodeType in ('B2B', 'B2C', 'CDN', 'Exp')  Order by CodeClass"
        Me.AddLookupField("GSTInvoiceSubType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GSTInvoiceSubType").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SupplyType").TableName)

        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("InvoiceItem", "ItemUnitID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)

        sql = "Select * from gstrsection"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "section")

        sql = "Select * from systemoptions"
        myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "options")

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxCredit", "")
        Me.AddLookupField("TaxCredit", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxCredit").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim ObjIVProc As New clsIVProcSD(myContext, "IS"), objPricingCalcBase As New clsPricingCalcBase(myContext)
        Dim sql, str1 As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Invoice Where InvoiceID = " & prepIDX
        Me.InitData(sql, "SalesOrderID,InvoiceTypeCode", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("InvoiceDate") = Now.Date
            myRow("PostingDate") = Now.Date
            myRow("InvoiceID") = 1000000
        Else
            Dim rPostPeriod As DataRow = ObjIVProc.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
            If Not IsNothing(rPostPeriod) Then
                PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
            End If
        End If

        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QC") Then
            myRow("Billof") = "P"
        Else
            myRow("Billof") = "C"
        End If

        ObjIVProc.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))
        Me.BindDataTable(myUtils.cValTN(prepIDX))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "2-1-1-1-1-1")

        myViewODNote.MainGrid.BindGridData(Me.dsForm, 3)
        myViewODNote.MainGrid.QuickConf("", True, "1-1-1-1")

        sql = "Select STFormID, InvoiceID, STFormMasterID, STFormNum, STFormDate, FormGiveTake, STFormOK, STFormValue, Remark from STForm  Where InvoiceID = " & frmIDX & " "
        MyViewForms.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1", True)
        sql = "Select STFormMasterID, STFormName from STFormMaster"
        str1 = "<BAND IDFIELD=""STFormID"" TABLE=""STForm"" INDEX=""0""><COL KEY=""STFormID, InvoiceID, STFormNum, STFormDate, STFormValue, STFormOK, Remark""/><COL KEY=""STFormMasterID"" LOOKUPSQL=""" & sql & """/><COL KEY=""FormGiveTake"" VLIST=""G|T""/></BAND>"
        MyViewForms.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        sql = "Select PaymentTermID, InvoiceID, SortIndex, Description, PerValue,  PayAmount from PaymentTerm Where InvoiceID = " & frmIDX & " "
        MyViewPayments.MainGrid.QuickConf(sql, True, "1-1-1", True)
        str1 = "<BAND IDFIELD=""PaymentTermID"" TABLE=""PaymentTerm"" INDEX=""0""><COL KEY=""PaymentTermID, InvoiceID, SortIndex, PerValue, Description, PayAmount from PaymentTerm""/></BAND>"
        MyViewPayments.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
            Dim dtPS As DataTable = dsForm.Tables("ProdSerialItem")
            myViewSerialSO.MainGrid.BindGridData(dsForm, dtPS.DataSet.Tables.IndexOf(dtPS))
            myViewSerialSO.MainGrid.QuickConf("", True, "1-1-1")
            str1 = "<BAND INDEX = ""0"" TABLE = ""ProdSerialItem"" IDFIELD=""ProdSerialItemID""><COL KEY ="" ProdSerialID, InvoiceItemID, ODNoteItemID ""/></BAND>"
            myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

            sql = "Select SOSpareID, SpareName, PIDInfo from slsListSOSpares() where SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID"))
            Me.AddLookupField("ODNoteSpare", "SoSpareID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SOSpare").TableName)
            Me.AddLookupField("ODNoteSpare", "PIDInfoSp", "SOSpare")

            sql = "Select SOServiceID, ServiceName, PIDInfo from slsListSOService() Where SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID"))
            Me.AddLookupField("ODNoteSpare", "SoServiceID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SOService").TableName)
            Me.AddLookupField("ODNoteSpare", "PIDInfoSvc", "SOService")
        End If

        sql = " Select MatVouchID, InvoiceMVId, InvoiceID, PInvoiceID, AmountWV, VouchNum, VouchDate, AmountTot from purListInvoiceMV() Where InvoiceID = " & frmIDX & " "
        myViewRec.MainGrid.QuickConf(sql, True, "1-1-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""InvoiceMV"" IDFIELD=""InvoiceMVID""><COL KEY ="" InvoiceMVId, InvoiceID, MatVouchID""/></BAND>"
        myViewRec.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        sql = "Select InvoiceItemCreditID, InvoiceID, sortindex, MatVouchItemID, Description, Amount  from InvoiceItemCredit Where InvoiceID = " & frmIDX & " "
        myViewItemCredit.MainGrid.QuickConf(sql, True, "5-1", True)
        str1 = "<BAND INDEX = ""0"" TABLE = ""InvoiceItemCredit"" IDFIELD=""InvoiceItemCreditID""><COL KEY ="" InvoiceItemCreditID, InvoiceID, sortindex, MatVouchItemID, Description, Amount""/></BAND>"
        myViewItemCredit.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("InvoiceItem"))


        Dim MatVouchIDCSV As String = myUtils.MakeCSV(Me.dsForm.Tables("ODNoteAdd").Select, "MatVouchID")
        sql = "select InvoiceNum, Reason from purListItemHist() where MatVouchID in (" & MatVouchIDCSV & ") and RecvMatVouchID is not null"
        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If dt.Rows.Count > 0 Then
            Me.ModelParams.Add(New clsSQLParam("@RefReciept", True, GetType(Boolean), False))
        Else
            Me.ModelParams.Add(New clsSQLParam("@RefReciept", False, GetType(Boolean), False))
        End If
        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "InvoiceID", myUtils.cValTN(myRow("InvoiceID"))), GetType(Boolean), False))


        sql = "Select ProjectID from SalesOrder where SalesOrderID  = " & myUtils.cValTN(myRow("SalesOrderID")) & ""
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        If dt1.Rows.Count > 0 Then
            Me.ModelParams.Add(New clsSQLParam("@ProjectID", myUtils.cValTN(dt1.Rows(0)("ProjectID")), GetType(Integer), False))
        End If

        objPricingCalcBase.CheckPriceSlabElement()

        If myUtils.cValTN(myRow("CDNInvoiceID")) > 0 Then
            Me.AddDataSet("CDNInv", "Select * from Invoice where InvoiceID = " & myUtils.cValTN(myRow("CDNInvoiceID")))
        End If

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal InvoiceID As Integer)
        Dim ds As DataSet, Sql, Sql1, Sql2, Sql3, Sql4, Sql5 As String

        Sql1 = "Select InvoiceItemID, InvoiceID, SOSpareID, SOServiceID, VarNum, ItemUnitID, PIDUnitID, ItemID,TaxCredit, ClassType, TransType, ValuationClass, PriceSlabID, POSerialNum, RNPrevRate, QtyPrev, QtyPO, QtySOSpare, QtySOService, PPSubType, AmountBasic, InvoiceItemType, SortIndex, SubSortIndex, HSN_SC, SerialNum, ItemCode, Description, QtyRate, UnitName, BasicRate, AmountTot, AmountWV  from accListInvoiceItem() Where InvoiceID = " & InvoiceID & " "
        Sql2 = "Select ProdSerialItemID, ProdSerialItem.ProdSerialID, InvoiceItemID,  ProdSerialNum, WOInfo, LotNum from ProdSerialItem Inner join plnListProdSerial() on ProdSerialItem.ProdSerialID = plnListProdSerial.ProdSerialID  Where InvoiceItemID in (select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ")"
        Sql3 = "Select ODNoteID, ODNote.MatVouchID, InvoiceID, PriceProcID, PInvoiceID,ODNote.ChallanNum, ODNote.VouchNum, ODNote.ChallanDate, ChallanType, ODNote.AmountTot from ODNote left join MatVouch on ODNote.MatVouchID = MatVouch.MatVouchID Inner join PriceSlab on ODNote.PriceSlabID = PriceSlab.PriceSlabID Where ODNote.InvoiceID = " & InvoiceID & " "
        Sql4 = "Select * from InvoiceItemGST Where InvoiceItemID in (Select InvoiceItemID from InvoiceItem where InvoiceID = " & InvoiceID & ") "
        Sql5 = "Select * from InvoiceGstCalc where invoiceid = " & InvoiceID
        Sql = Sql1 & ";" & Sql2 & ";" & Sql3 & ";" & Sql4 & ";" & Sql5
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "InvoiceItem", 0)
        myUtils.AddTable(Me.dsForm, ds, "ProdSerialItem", 1)
        myUtils.AddTable(Me.dsForm, ds, "ODNoteAdd", 2)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceItemGST", 3)
        myUtils.AddTable(Me.dsForm, ds, "InvoiceGstCalc", 4)
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("ProdSerialItem"), "InvoiceItemId", "_ProdSerialItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("InvoiceItem"), Me.dsForm.Tables("InvoiceItemGST"), "InvoiceItemID", "_InvoiceItemGST")
    End Sub


    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "QD", "QC") AndAlso Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If myUtils.IsInList(myUtils.cStrTN(myRow("InvoiceTypeCode")), "PM") AndAlso Me.SelectedRow("CustomerID") Is Nothing Then Me.AddError("CustomerID", "Please select Customer")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso Me.SelectedRow("GSTInvoiceType") Is Nothing Then Me.AddError("GSTInvoiceType", "Please select GST Invoice Type")
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2C")) AndAlso Me.SelectedRow("GSTInvoiceSubType") Is Nothing Then Me.AddError("GSTInvoiceSubType", "Please select GST Invoice Sub Type")
        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("BillOf")), "P") Then
            If myRow("InvoiceNum").ToString.Trim.Length = 0 Then Me.AddError("InvoiceNum", "Enter Invoice No.")
        End If

        If myUtils.IsInList(myUtils.cStrTN(Me.myRow("InvoiceTypeCode")), "QD", "QC") Then
            If myUtils.cValTN(myRow("CDNInvoiceID")) = 0 Then Me.AddError("", "Please Select Original Invoice")
        End If

        If Not Me.SelectedRow("CampusID") Is Nothing Then
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(Me.SelectedRow("CampusID")("CompanyID")), Me.myRow("PostingDate"), PPFinal)
        End If
        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("PostingDate", "Please Select Valid Post Period")

        If myRow("PostingDate") < myRow("InvoiceDate") Then Me.AddError("PostingDate", "Posting Date can not be less then Invoice Date.")
        If myRow("PostingDate") > Now.Date Then Me.AddError("PostingDate", "Please Select Valid Posting Date.")
        If myRow("InvoiceDate") > Now.Date Then Me.AddError("InvoiceDate", "Please Select Valid Invoice Date.")

        If (Not Me.SelectedRow("DeliveryCampusID") Is Nothing) AndAlso (myUtils.cValTN(SelectedRow("DeliveryCampusID")("SalesOrderID"))) > 0 Then
            If myUtils.cValTN(SelectedRow("DeliveryCampusID")("SalesOrderID")) <> myUtils.cValTN(myRow("SalesOrderID")) Then Me.AddError("", "Please Select Correct Sales Order")
        End If

        Dim oRet = myFuncs.CheckZeroTaxType(myUtils.cStrTN(myRow("GSTInvoiceSubType")), dsForm.Tables("InvoiceItemGST"))
        If myRow("InvoiceDate") >= myFuncs.GSTLounchDate AndAlso (Not oRet.Success) Then Me.AddError("", oRet.Message)
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        Dim Oret As clsProcOutput
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
                myRow("DocType") = "IP"
                myRow("TaxInvoiceType") = DBNull.Value

                If Not Me.SelectedRow("DeliveryCampusID") Is Nothing Then
                    myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("DeliveryCampusID")("TaxAreaID"))
                Else
                    myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CampusID")("TaxAreaID"))
                End If
            Else
                myRow("DocType") = "IS"
                If myViewODNote.MainGrid.myDV.Table.Select.Length > 0 Then
                    If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then
                        myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("ConsigneeID")("TaxAreaID"))
                    Else
                        If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 Then myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CustomerID")("TaxAreaID"))
                    End If
                End If
            End If

            Dim ObjIVProc As New clsIVProcSD(myContext, "IS")
            ObjIVProc.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))
            ObjIVProc.DocType = myRow("doctype")

            If ObjIVProc.GetInvoiceTypeID(myRow.Row) = False Then Me.AddError("", "Combination Not Available")

            Dim objPricingCalcBase As New clsPricingCalcBase(myContext)
            objPricingCalcBase.InitData(myRow.Row, "InvoiceID", myUtils.cValTN(frmIDX), "PostingDate", "InvoiceItemId", "QtyRate", Me.dsForm.Tables("InvoiceItem"))
            objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "AmountWV", "")

            If myUtils.IsInList(myUtils.cStrTN(myRow("GSTInvoiceType")), "B2C") Then
                If myUtils.cValTN(myRow("AmountTot")) > 250000 AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("sply_ty")), "INTER") Then
                    myRow("GSTInvoiceSubType") = "L"
                Else
                    myRow("GSTInvoiceSubType") = "S"
                End If
            End If

            myRow("BalanceAmount") = myRow("AmountTot")
            myRow("BalanceDate") = myRow("InvoiceDate")

            ObjIVProc.GenerateLinkedData(myRow.Row.Table, dsForm.Tables("InvoiceItem"))
            Oret = ObjIVProc.CheckBalance()
            If Oret.Success = False Then Me.AddError("", Oret.Message)

            Oret = ObjIVProc.CheckSOBalance(myRow.Row, dsForm.Tables("InvoiceItem"), dsForm.Tables("ProdSerialItem"))
            If Oret.Success = False Then Me.AddError("", Oret.Message)
            If myViewODNote.MainGrid.myDV.Table.Select.Length > 0 AndAlso Me.SelectedRow("POSTaxAreaID") Is Nothing Then Me.AddError("POSTaxAreaID", "Please select Place of Supply.")

            Dim oProc As New clsGSTInvoiceSale(myContext)
            oProc.CalcVouchActionRP(Me.SelectedRow("CampusID")("gstregid"), myRow("postperiodid"), myRow.Row)

            If Me.CanSave Then
                objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "AmountWV", "")
                For Each r2 As DataRow In dsForm.Tables("InvoiceItem").Select()
                    objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "AmountBasic", "")
                Next
                objPricingCalcBase.PopulateAccountingKeys(myRow("postingdate"))
                myRow("BalanceAmount") = myRow("AmountTot")
                myRow("BalanceDate") = myRow("InvoiceDate")
                myRow("TY") = myFuncs.SetTY(dsForm.Tables("InvoiceItem"))


                If myUtils.IsInList(myUtils.cStrTN(Me.myRow("BillOf")), "C") Then
                    Dim DocNumSysType As String = myFuncs.GetDocNumSysType(myContext, myUtils.cValTN(myRow("InvoiceTypeID")))
                    Dim ObjVouch As New clsVoucherNum(myContext)
                    ObjVouch.GetNewSerialNo("InvoiceID", DocNumSysType, myRow.Row)
                End If

                myRow("uniquekey") = oProc.CalcUniqueKey(Me.SelectedRow("campusid")("campuscode"), myRow("postperiodid"), myRow("invoicenum"), myUtils.cValTN(myRow("isamendment")))

                Dim InvoiceDescrip As String = " InvoiceNum: " & myRow("InvoiceNum").ToString & " Dt: " & Format(myRow("InvoiceDate"), "dd-MMM-yyyy")

                Try
                    myContext.CommonData.GetDatasetFYComp(False)
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "InvoiceID", frmIDX)
                    myRow("AmountTot") = myUtils.cValTN(myRow("AmountTot")) - myUtils.cValTN(myViewItemCredit.MainGrid.GetColSum("Amount"))
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    frmIDX = myRow("InvoiceID")

                    myUtils.ChangeAll(dsForm.Tables("InvoiceItem").Select, "invoiceid=" & myRow("invoiceid"))
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItem"), "Select InvoiceItemID, InvoiceID, SOSpareID, VarNum, PIDUnitID, ItemID,TaxCredit, SerialNum, ClassType, TransType, ValuationClass, PriceSlabID, POSerialNum, RNPrevRate, QtyPrev, QtyPO, QtySOSpare, QtySOService, PPSubType, AmountBasic, InvoiceItemType, SortIndex, SubSortIndex, Description, QtyRate, BasicRate, AmountTot, AmountWV,HSN_SC,ItemUnitID from InvoiceItem")
                    objPricingCalcBase.VSave()

                    myUtils.ChangeAll(MyViewPayments.MainGrid.myDS.Tables(0).Select, "invoiceid =" & myRow("invoiceid"))
                    myContext.Provider.objSQLHelper.SaveResults(MyViewPayments.MainGrid.myDS.Tables(0), "Select PaymentTermID, InvoiceID, SortIndex, Description, PerValue,  PayAmount from PaymentTerm")

                    myUtils.ChangeAll(MyViewForms.MainGrid.myDS.Tables(0).Select, "invoiceid =" & myRow("invoiceid"))
                    myContext.Provider.objSQLHelper.SaveResults(MyViewForms.MainGrid.myDS.Tables(0), "Select STFormID, InvoiceID, STFormMasterID, STFormNum, STFormDate, FormGiveTake, STFormOK, STFormValue, Remark from STForm")

                    Me.myViewRec.MainGrid.SaveChanges(, "InvoiceID = " & frmIDX)
                    myUtils.ChangeAll(myViewRec.MainGrid.myDS.Tables(0).Select, "PInvoiceID =" & myRow("invoiceid"))
                    myContext.Provider.objSQLHelper.SaveResults(myViewRec.MainGrid.myDS.Tables(0), "Select MatVouchID, PInvoiceID from MatVouch")

                    Me.myViewItemCredit.MainGrid.SaveChanges(, "InvoiceID = " & frmIDX)
                    ObjIVProc.SaveODNote()

                    If Not myUtils.IsInList(myUtils.cStrTN(Me.myRow("P_gst")), "Y") Then
                        Dim InvoiceItemID As String = myUtils.MakeCSV(dsForm.Tables("InvoiceItem").Select("InvoiceItemType Not in ('PIC','PIS','IGT', 'IGS')"), "InvoiceItemID")
                        myUtils.ChangeAll(dsForm.Tables("InvoiceItemGST").Select, "InvoiceID=" & frmIDX)
                        myUtils.ChangeAll(dsForm.Tables("InvoiceGstCalc").Select, "InvoiceID=" & frmIDX)

                        Dim rCDN = oProc.GetFirstRow(Me.DatasetCollection, "cdninv")
                        oProc.PopulateCalc(frmIDX, myRow.Row, Me.SelectedRow("CampusID"), dsForm.Tables("InvoiceItemGST"), dsForm.Tables("InvoiceGstCalc"), rCDN, Nothing, Me.dsCombo)

                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceItemGST"), "Select * from InvoiceItemGST", True, "InvoiceItemID in (" & InvoiceItemID & ")")
                        myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("InvoiceGstCalc"), "Select * from InvoiceGstCalc")
                    End If


                    Oret = ObjIVProc.HandleWorkflowState(myRow.Row)
                    If Oret.Success Then
                        frmMode = EnumfrmMode.acEditM
                        myContext.Provider.dbConn.CommitTransaction(InvoiceDescrip, frmIDX)
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

    Public Overrides Sub OperateProcess(processKey As String)
        Dim ObjIVProc As New clsIVProcSD(myContext, "IS")
        ObjIVProc.LoadVouch(myUtils.cValTN(myRow("InvoiceID")))
        Select Case processKey.Trim.ToLower
            Case "generate"
                ObjIVProc.GenerateInvoiceItems(myRow.Row.Table, dsForm.Tables("InvoiceItem"))
            Case "generateitemcredit"
                ObjIVProc.GenerateInvoiceItemCredit(myRow.Row, myViewRec.MainGrid.myDS.Tables(0), myViewItemCredit.MainGrid.myDS.Tables(0))
        End Select
    End Sub

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "salesorder"
                Dim Sql As String = "Select SalesOrderID, CustomerID, MainPartyID, PaymentAuthID, MainPartyID as AuthMainPartyID from slsListOrder() Where SalesOrderID = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
            Case "salesorderdescrip"
                Dim sql As String = "Select OrderNum, OrderDate  from SalesOrder  where SalesOrderID = " & myUtils.cValTN(frmIDX)
                Dim dt2 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt2.Rows.Count > 0 Then oRet.Description = "Sales Order :- " & myUtils.cStrTN(dt2.Rows(0)("OrderNum")) & " Date - " & Format(dt2.Rows(0)("OrderDate"), "dd-MMM-yyyy")
        End Select
        Return oRet
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "odnote"
                    Dim MultiSelect As Boolean = True
                    Dim SalesOrderID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@salesorderid", Params))
                    Dim InvoiceTypeCode As String = myUtils.cStrTN(myContext.SQL.ParamValue("@invoicetypecode", Params))
                    Dim ConsigneeID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@consigneeid", Params))
                    Dim PriceProcID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@priceprocid", Params))
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim DeliveryCampusId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@deliverycampusid", Params))
                    Dim POSTaxAreaID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@POSTaxAreaID", Params))
                    Dim PartyType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@PartyType", Params))

                    Dim SqlCampus As String
                    If CampusID > 0 AndAlso DeliveryCampusId > 0 AndAlso CampusID <> DeliveryCampusId Then
                        SqlCampus = "(CampusID = @deliverycampusid and isnull(InvoiceCampusID,0) = @CampusID) and isnull(DivisionID,0) = @DivisionID"
                    Else
                        SqlCampus = "(CampusID = @CampusID and (InvoiceCampusID Is NULL or isnull(InvoiceCampusID,0) = @CampusID)) and isnull(DivisionID,0) = @DivisionID"
                    End If


                    Dim sql As String = ""
                    If myUtils.IsInList(InvoiceTypeCode, "PM") Then
                        If SalesOrderID > 0 Then
                            Dim sql1 As String = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, "SalesOrderID = " & SalesOrderID, SqlCampus, "ChallanType in ('SCSP','SCUB')", "(InvoiceId is Null or InvoiceID = @invoiceid)", "MatVouchID is Not NULL and ODNoteId Not in (@odnoteidcsv)"), Params)
                            Dim sql2 As String = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, SqlCampus, "ChallanType in ('RE', 'TR','ST','RC')", "(InvoiceId is Null or InvoiceID = @invoiceid)", "MatVouchID is Not NULL and ODNoteId Not in (@odnoteidcsv)"), Params)
                            Dim sql3 As String = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, "SalesOrderID = " & SalesOrderID, SqlCampus, "ChallanType in ('SCSV')", "(InvoiceId is Null or InvoiceID = @invoiceid)", "ODNoteId Not in (@odnoteidcsv)"), Params)
                            sql = myUtils.CombineWhereOR(False, sql1, sql2, sql3)
                        Else
                            If myUtils.IsInList(PartyType, "A") Then
                                sql = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, "CustomerID = @customerid", SqlCampus, "ChallanType in ('SP', 'SCST', 'SCSTB', 'SCUB', 'SCSP')", "(InvoiceId is Null or InvoiceID = @invoiceid)", "MatVouchID is Not NULL", "ODNoteId Not in (@odnoteidcsv)"), Params)
                            Else
                                sql = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, "CustomerID = @customerid", SqlCampus, "ChallanType in ('SP', 'SCST', 'SCSTB', 'SCUB')", "(InvoiceId is Null or InvoiceID = @invoiceid)", "MatVouchID is Not NULL", "ODNoteId Not in (@odnoteidcsv)"), Params)
                            End If
                        End If
                    ElseIf myUtils.IsInList(InvoiceTypeCode, "QD", "QC") Then
                        MultiSelect = False
                        sql = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, "VendorID = @vendorid", "ChallanType = 'PR'", "(InvoiceId is Null or InvoiceID = @invoiceid)", "MatVouchID is Not NULL", "ODNoteId Not in (@odnoteidcsv)"), Params)
                    End If

                    If ConsigneeID > 0 Then
                        sql = myUtils.CombineWhere(False, sql, "ConsigneeID = " & ConsigneeID)
                    End If

                    If POSTaxAreaID > 0 Then
                        sql = myUtils.CombineWhere(False, sql, "POSTaxAreaID = " & POSTaxAreaID)
                    End If

                    If PriceProcID > 0 Then
                        sql = myUtils.CombineWhere(False, sql, "PriceProcID = " & PriceProcID & " or ChallanType = 'RE' or ChallanType = 'TR'")
                    End If

                    Dim ODNoteIDIDCSV As String = myContext.SQL.ParamValue("@odnoteidcsv", Params)
                    If myUtils.IsInList(InvoiceTypeCode, "QD", "QC") AndAlso (Not myUtils.IsInList(ODNoteIDIDCSV, "0")) Then
                        sql = myUtils.CombineWhere(False, sql, myContext.SQL.PopulateSQLParams("ODnoteID in (Select ODNoteID from trplistODnote() Left join PurListItemHist() on trplistODnote.MatvouchID = PurListItemHist.MatvouchID where RecvPInvoiceID IN (Select RecvPInvoiceID from ODNote left join PurListItemHist() on ODNote.Matvouchid = purlistitemhist.matvouchid where ODNoteID in (@odnoteidcsv)))", Params))
                    End If

                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ODNoteID""/>", MultiSelect, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "serials"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdSerialID""/>", True, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
                Case "spares"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid and TransType = @transtype", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOSpareID""/>", False, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
                Case "services"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOServiceID""/>", False, , "<MODROW><SQLWHERE2>" & Sql & "</SQLWHERE2></MODROW>")
                Case "matvouch"
                    Dim sql As String = myContext.SQL.PopulateSQLParams(myUtils.CombineWhere(False, "CustomerID = @customerid", "PriceSlabID > 0", "VouchTypeCode = 'GR'", "CampusID = @campusid", "(PInvoiceId is Null or PInvoiceID = @invoiceid)", "MatVouchID Not in (@matvouchidcsv)"), Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""MatVouchID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "salesorder"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("MainPartyID = @MainPartyID and CompanyID = @CompanyId and SalesOrderID Not in (@SalesOrderID) and (isnull(OrderDate,'3099-Jan-01') <= '@InvoiceDate' or isnull(LOIDate,'3099-Jan-01') <= '@InvoiceDate')", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SalesOrderID""/>", False, , "<MODROW><SQLWHERE2>" & XMLUtils.ReplaceSpecialChars(sql) & "</SQLWHERE2></MODROW>")
                Case "invoice"
                    Dim InvoiceDate As Date = myContext.SQL.ParamValue("@InvoiceDate", Params)

                    Model = New clsViewModel(vwState, myContext)
                    Dim sql1 As String = myContext.SQL.PopulateSQLParams("isNull(SalesOrderID, 0) = (@SalesOrderID) and DocType = 'IP' and InvoiceTypeCode in ('PF', 'PM','OF', 'TP') and VendorID = (@VendorID) and CampusID = (@CampusID) and DivisionID = (@DivisionID) and InvoiceDate <= '" & Format(InvoiceDate, "dd-MMM-yyyy") & "' and InvoiceDate >= '" & Format(DateAdd("m", -18, InvoiceDate), "dd-MMM-yyyy") & "'", Params)
                    Dim Sql As String = "SELECT InvoiceID, InvoiceTypeCode, InvoiceNum, InvoiceDate, AmountTot  From Invoice where " & sql1
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "1-1-1")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim str As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "refreciept"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("select Distinct InvoiceNum, InvoiceDate, Reason, RecvPInvoiceID from purListItemHist() where MatVouchID in (@MatVouchIDCSV) and RecvMatVouchID is not null", Params)
                    Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql).Tables(0)
                    For Each r As DataRow In dt.Select()
                        If myUtils.cStrTN(str).Trim.Length = 0 Then
                            str = myUtils.cStrTN(r("Reason")) & " - Against Invoice No. - " & myUtils.cStrTN(r("InvoiceNum")) & " Date - " & Format(r("InvoiceDate"), "dd-MMM-yyyy")
                        Else
                            str = str & ", " & myUtils.cStrTN(r("Reason")) & " - Against Invoice No. - " & myUtils.cStrTN(r("InvoiceNum")) & " Date - " & Format(r("InvoiceDate"), "dd-MMM-yyyy")
                        End If
                    Next
                    oRet.Description = str
                    oRet.Data = dt.DataSet
            End Select
        End If
        Return oRet
    End Function
End Class
