Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmPurOrderModel
    Inherits clsFormDataModel
    Dim myViewItem, myViewMatReq, myViewAccAss As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("PurItems")
        myViewItem = Me.GetViewModel("PurItemDet")
        myViewMatReq = Me.GetViewModel("MatReqItemPur")
        myViewAccAss = Me.GetViewModel("PurAccAss")
    End Sub

    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "select StatusNum, StatusText from Status where StatusType = 'PO' order by StatusNum"
        Me.AddLookupField("StatusNum", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Status").TableName)

        sql = "Select CampusID, DispName,TaxAreaCode, DivisionCodeList, WODate, CompletedOn,PONote from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Campus").TableName)
        Me.AddLookupField("InvoiceCampusID", "Campus")

        sql = "Select VendorID, VendorName, VendorType,TaxAreaCode, ImportAllow,PartyID, Discount, PayTerms, TransMode, ShipTerms, FreightIns, GSTIN  from PurListVendor() where VendorType in ('MS','EM') Order BY VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select Term from ShipTerms order by Term"
        Me.AddLookupField("ShipTerms", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ShipTerms").TableName)

        sql = "Select terms from PayTermStd order by Terms"
        Me.ValueLists.Add("Payterms", myContext.SQL.VListFromTable(myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)))
        Me.AddLookupField("PayTerms", "PayTerms")

        sql = myFuncsBase.CodeWordSQL("Purch", "OrderType", "(CodeWord <> 'MO')")
        Me.AddLookupField("OrderType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "OrderType").TableName)

        sql = myFuncsBase.CodeWordSQL("MatReq", "ReqType", "")
        Me.AddLookupField("MatReqType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "MatReqType").TableName)

        sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Division").TableName)


        sql = "Select ItemId, ItemCode, ItemName, UnitName, UnitName2, ItemUnitId, ItemUnitID2, OrderQtyUnitId, OrderRateUnitId, OrderQtyNumReq from InvListItems()  Order by ItemName"
        Me.AddLookupField("PurItems", "ItemId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Items").TableName)

        sql = "Select Class,Class,ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass where ClassType = 'A' or (ClassType = 'S' and ClassSubType in ('P','B'))"
        Me.AddLookupField("PurItems", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ValuationClass").TableName)

        sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("PurItems", "QtyUnitID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Units").TableName)
        Me.AddLookupField("PurItems", "RateUnitId", "Units")

        sql = myFuncsBase.CodeWordSQL("AccountMap", "ClassType", "CodeWord in ('A','M','S')")
        Me.AddLookupField("PurItems", "ClassType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "ClassType").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Asset','Service') and CodeWord in ('ARO','ARW','APN', 'APU','EXP')  Order by CodeClass "
        Me.AddLookupField("PurItems", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TransType").TableName)

        sql = "Select CodeWord, DescripShort, CodeClass from CodeWords  where CodeClass in ('Material') and CodeWord in ('RM', 'CG', 'CT', 'ST')  Order by CodeClass "
        Me.AddLookupField("PurItems", "StockStage", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "StockStage").TableName)

        sql = myFuncsBase.CodeWordSQL("Invoice", "TaxCredit", "")
        Me.AddLookupField("TaxCredit", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "TaxCredit").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim ObjPurVouchProc As New clsPurVouchProc(myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)
        Dim sql, str1 As String

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from PurOrder Where PurOrderID = " & prepIDX
        Me.InitData(sql, "OrderType,PIDUnitID", oView, prepMode, prepIDX, strXML)

        If frmMode = EnumfrmMode.acAddM Then
            myRow("OrderDate") = Now.Date
            myRow("StatusNum") = 1
            myRow("Currency") = "INR"
            myRow("ExchangeRate") = 1
        End If

        Dim dt1 As DataTable, PIDInfo As String = ""
        If myUtils.cValTN(myRow("PidUnitId")) > 0 Then
            sql = "Select * from plnlistpidunit() where PidUnitId = " & myUtils.cValTN(myRow("PidUnitId"))
            dt1 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
            If dt1.Rows.Count > 0 Then PIDInfo = " for " & myUtils.cStrTN(dt1.Rows(0)("pidinfo"))
            If myUtils.IsInList(myUtils.cStrTN(myRow("OrderType")), "PO") Then PIDInfo = "Purchase Order" & PIDInfo
            If myUtils.IsInList(myUtils.cStrTN(myRow("OrderType")), "LPO") Then PIDInfo = "Local Purchase Order" & PIDInfo
            If myUtils.IsInList(myUtils.cStrTN(myRow("OrderType")), "JWO") Then PIDInfo = "JOB Work Order" & PIDInfo
        Else
            If myUtils.IsInList(myUtils.cStrTN(myRow("OrderType")), "PO") Then PIDInfo = "Stock Purchase Order"
            If myUtils.IsInList(myUtils.cStrTN(myRow("OrderType")), "LPO") Then PIDInfo = "Stock Local Purchase Order"
            If myUtils.IsInList(myUtils.cStrTN(myRow("OrderType")), "JWO") Then PIDInfo = "Stock JOB Work Order"
        End If
        Me.ModelParams.Add(New clsSQLParam("@FormText", "'" & PIDInfo & "'", GetType(String), False))


        Me.BindDataTable(myUtils.cValTN(prepIDX))

        objPricingCalcBase.InitData(myRow.Row, "PurOrderID", myUtils.cValTN(frmIDX), "OrderDate", "PurItemID", "QtyRate", Me.dsForm.Tables("PurItems"))
        ObjPurVouchProc.LoadVouch(myUtils.cValTN(myRow("PurOrderID")))

        myView.MainGrid.BindGridData(Me.dsForm, 1)
        myView.MainGrid.QuickConf("", True, "1-2-1-1-1-1-1")
        str1 = "<BAND IDFIELD=""PurItemID"" TABLE=""PurItems"" INDEX=""0""><COL KEY=""PurItemID, PurOrderID, ItemVMSID, RateUnitID, PriceSlabID, QtyUnitID, ClassType, TransType, StockStage, ValuationClass,  Datecomp, RemQuan, TotalQty, Rate, RequireTC,  IsCompleted, ModifiedBy, [Status], Remark, QtyRecd, PPSubType,OrderNumDescrip,TaxCredit""/></BAND>"
        myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

        myViewItem.MainGrid.BindGridData(Me.dsForm, 2)
        myViewItem.MainGrid.MainConf("HIDECOLS") = IIf(myUtils.cStrTN(myRow("OrderType")) = "LPO", "DeliveryDate", "")
        myViewItem.MainGrid.QuickConf("", True, "1-1-1-1")
        str1 = "<BAND IDFIELD=""PurItemDetID"" TABLE=""PurItemDet"" INDEX=""0""><COL KEY=""PurItemDetID, PurItemID, ProdLotID, Quantity, QtyRecd, DeliveryDate, ExpFinDate, Updatedate""/></BAND>"
        myViewItem.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        myViewMatReq.MainGrid.BindGridData(Me.dsForm, 3)
        myViewMatReq.MainGrid.QuickConf("", True, "1-1-1")
        str1 = "<BAND IDFIELD=""MatReqItemPurID"" TABLE=""MatReqItemPur"" INDEX=""0""><COL KEY=""MatReqItemPurID, PurItemID, MatReqItemID, QtyPur""/></BAND>"
        myViewMatReq.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Dim dtFA As DataTable = Me.dsForm.Tables("PurItemAccAss")
        myViewAccAss.MainGrid.BindGridData(dsForm, dtFA.DataSet.Tables.IndexOf(dtFA))
        myViewAccAss.MainGrid.QuickConf("", True, "2-1-1")
        str1 = "<BAND INDEX = ""0"" TABLE = ""PurItemAccAss"" IDFIELD=""PurItemAccAssID""><COL KEY ="" FixedAssetID, PurItemID, Qty ""/></BAND>"
        myViewAccAss.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Me.DatasetCollection.AddUpd("supp", Me.GenerateData("supp", myUtils.cValTN(myRow("vendorid"))))

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PurOrderID", myUtils.cValTN(myRow("PurOrderID"))), GetType(Boolean), False))

        objPricingCalcBase.CheckPriceSlabElement()
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Private Sub BindDataTable(ByVal PurOrderId As Integer)
        Dim Sql As String = "", str As String = ""
        Dim ds As DataSet, Str1, Str2, Str3, Str4, Str5 As String

        str = " Where PurItemId in (Select PurItemId from PurItems where PurOrderId = " & PurOrderId & ")"

        Str1 = " SELECT  PurItemID, PurOrderID, ItemVMSID,ItemID, ClassType, StockStage, TransType, TaxCredit, ValuationClass,  DateComp, RemQuan, QtyRecd,  QtyUnitID, RateCurrency, RateUnitID, PriceSlabID, RequireTC,  IsCompleted, ModifiedBy, [Status], AmountWV, QtyNote, PPSubType, OrderNumDescrip, ItemCode, ItemName, TotalQty, QtyRate, BasicRate, AmountTot, Remark FROM PurListOItems() where PurOrderID = " & PurOrderId & ""
        Str2 = "Select PurItemDetID, PurItemID, Updatedate, QtyNote, ExpFinDate, QtyRecd, ProdLotID, WOInfo, LotNum, DeliveryDate, Quantity from PurListItemDet() " & str & ""
        Str3 = "Select MatReqItemPurID, MatReqItemID, PurItemID,isCompleted, SNum, MRDate, QtyPur from plnListMatReqItemPur() " & str & ""
        Str4 = "Select PurItemAccAssID, PurItemID, sortindex, PurItemAccAss.FixedAssetID, QtyRecd, AssetNumber, AssetName, Qty from PurItemAccAss Inner join FixedAsset on PurItemAccAss.FixedAssetID = FixedAsset.FixedAssetID  " & str & ""
        Str5 = "Select Distinct PurItemID from PurItemHist " & str & ""

        Sql = "" & Str1 & ";" & Str2 & ";" & Str3 & ";" & Str4 & ";" & Str5 & ""
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "PurItems", 0)
        myUtils.AddTable(Me.dsForm, ds, "PurItemDet", 1)
        myUtils.AddTable(Me.dsForm, ds, "MatReqItemPur", 2)
        myUtils.AddTable(Me.dsForm, ds, "PurItemAccAss", 3)
        myUtils.AddTable(Me.dsForm, ds, "PurItemHist", 4)

        myContext.Tables.SetAuto(Me.dsForm.Tables("PurItems"), Me.dsForm.Tables("PurItemDet"), "PurItemID", "_PurItemID")
        myContext.Tables.SetAuto(Me.dsForm.Tables("PurItems"), Me.dsForm.Tables("MatReqItemPur"), "PurItemID", "_MatReqItemPur")
        myContext.Tables.SetAuto(Me.dsForm.Tables("PurItems"), Me.dsForm.Tables("PurItemAccAss"), "PurItemID", "_PurItemAccAss")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("OrderType") Is Nothing Then Me.AddError("OrderType", "Please Select Order Type")
        If Me.SelectedRow("CampusId") Is Nothing Then Me.AddError("CampusId", "Please select Campus")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Please select Vendor")
        If myUtils.NullNot(myRow("Currency")) Then Me.AddError("Currency", "Please select Currency")
        If myUtils.cStrTN(myRow("payterms")).Trim.Length = 0 Then Me.AddError("PayTerms", "Please enter Payment Terms")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")

        If myUtils.cStrTN(myRow("ExchangeRate")).Trim.Length = 0 Then Me.AddError("ExchangeRate", "Enter Exchange Rate")

        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            If myFuncs.CheckStatus(myContext, "PurOrderID", myUtils.cValTN(myRow("PurOrderID"))) Then
                Me.AddError("OrderNum", "Order Completed, can't be update.")
            End If
            Dim ObjPurVouchProc As New clsPurVouchProc(myContext)
            ObjPurVouchProc.LoadVouch(myUtils.cValTN(myRow("PurOrderID")))
            ObjPurVouchProc.GenerateVoucherDelta(myRow.Row.Table, dsForm.Tables("MatReqItemPur"))

            Dim ObjVouch As New clsVoucherNum(myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)
            ObjVouch.GetNewSerialNo("PurOrderId", myRow("orderType"), myRow.Row)
            Dim r1 As DataRow = myContext.CommonData.FinRow(myRow("OrderDate"))
            If Not myUtils.NullNot(r1) Then
                myRow("FinYearID") = r1("FinYearID")
            End If

            objPricingCalcBase.InitData(myRow.Row, "PurOrderID", myUtils.cValTN(frmIDX), "OrderDate", "PurItemID", "QtyRate", Me.dsForm.Tables("PurItems"))
            objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "AmountWV", "")
            For Each r2 As DataRow In dsForm.Tables("PurItems").Select()
                objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "", "")
            Next

            If Me.CanSave Then
                Dim PurOrderDescrip As String = " Purchase Order No: " & myUtils.cStrTN(myRow("OrderNum")) & ", Dt. " & Format(myRow("OrderDate"), "dd-MMM-yyyy")
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PurOrderId", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)
                    frmIDX = myRow("PurOrderId")

                    myUtils.ChangeAll(dsForm.Tables("PurItems").Select, "PurOrderId=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PurItems"), "Select PurItemID, PurOrderID, ItemVMSID, ClassType, StockStage, TransType, ValuationClass,  DateComp, RemQuan, QtyRecd,  QtyUnitID, RateCurrency, RateUnitID, PriceSlabID, RequireTC,  IsCompleted, ModifiedBy, [Status], AmountWV, QtyNote, PPSubType, TotalQty, QtyRate, BasicRate, AmountTot, Remark, OrderNumDescrip, TaxCredit from PurItems")
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PurItemDet"), "Select PurItemDetID, PurItemID, ProdLotID, Updatedate, QtyNote, ExpFinDate, Quantity, QtyRecd, DeliveryDate from PurItemDet", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("MatReqItemPur"), "Select MatReqItemPurID, MatReqItemID, PurItemID, QtyPur from MatReqItemPur", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("PurItemAccAss"), "Select PurItemAccAssID, PurItemID, sortindex, PurItemAccAss.FixedAssetID, Qty, QtyRecd from PurItemAccAss", True)

                    If myUtils.cValTN(Me.SelectedRow("VendorID")("PartyID")) > 0 Then
                        For Each r2 As DataRow In dsForm.Tables("PurItems").Select
                            Dim nr As DataRow
                            Dim dt1 As DataTable = Me.DatasetCollection("supp").Tables("suppitems")
                            Dim rr() As DataRow = dt1.Select("VendorID =" & myUtils.cValTN(myRow("VendorID")) & " and ItemID = " & myUtils.cValTN(r2("ItemID")) & "")
                            If Not IsNothing(rr) AndAlso rr.Length > 0 Then
                                nr = rr(0)
                            Else
                                nr = myContext.Tables.AddNewRow(dt1)
                                nr("VendorID") = myUtils.cValTN(myRow("VendorID"))
                                nr("ItemID") = myUtils.cValTN(r2("ItemID"))
                            End If
                            nr("Rate") = myUtils.cValTN(r2("BasicRate"))
                        Next
                        myContext.Provider.objSQLHelper.SaveResults(Me.DatasetCollection("supp").Tables("suppitems"), "Select * from SupplierItem")


                        Dim dt2 As DataTable = Me.DatasetCollection("supp").Tables("vendorterms")
                        Dim rr1() As DataRow = dt2.Select()
                        If Not IsNothing(rr1) AndAlso rr1.Length > 0 Then
                            rr1(0)("shipterms") = myUtils.cStrTN(myRow("shipterms"))
                            rr1(0)("payterms") = myUtils.cStrTN(myRow("payterms"))
                            rr1(0)("transmode") = myUtils.cStrTN(myRow("transmode"))
                            rr1(0)("discount") = myUtils.cStrTN(myRow("discount"))
                            rr1(0)("FreightIns") = myUtils.cStrTN(myRow("FreightIns"))
                        End If
                        myContext.Provider.objSQLHelper.SaveResults(Me.DatasetCollection("supp").Tables("vendorterms"), "Select PartyID,  Discount, PayTerms, TransMode, ShipTerms, FreightIns from Party")
                    End If

                    objPricingCalcBase.VSave()
                    ObjPurVouchProc.UpdateBalance()

                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(PurOrderDescrip, frmIDX)
                    VSave = True
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(PurOrderDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing, str2 As String = ""
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "matreqitem"
                    Dim MatReqID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@matreqid", Params))
                    Dim Sql1 As String = "SELECT MatReqID, MatReqItemID, ItemCode, ItemName, UnitName, QtyReq, QtyPur, (isnull(QtyReq,0) - isnull(QtyPur,0)) as Qty, Remark From plnListMatReqItems() Where MatReqID = " & MatReqID & " and  isnull(iscompleted,0) = 0 and isnull(QtyReq,0) > isnull(QtyPur,0)"
                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = True
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""ItemCode"" CAPTION=""Item Code""/><COL KEY=""ItemName"" CAPTION=""Item Name""/><COL KEY=""UnitName"" CAPTION=""Unit Name""/>"
                    Model.MainGrid.QuickConf(Sql1, True, "1-3-1-1-1-1-2")
                    Dim str1 As String = "<BAND IDFIELD=""MatReqItemID"" TABLE=""MatReqItem"" INDEX=""0""><COL KEY=""Qty""/></BAND>"
                    Model.MainGrid.PrepEdit(str1, EnumEditType.acEditOnly)
                Case "matreq"
                    Dim Sql2 As String = ""
                    Dim PIDUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@PidUnitID", Params))
                    If myUtils.cValTN(PIDUnitID) > 0 Then Sql2 = " and PIDUnitID = " & PIDUnitID
                    Dim sql As String = myContext.SQL.PopulateSQLParams("CampusID = @campusid and IsAgainstJWO = @isagainstjwo and isnull(isReleased,0)=1 and isnull(iscompleted,0)=0", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""MATREQID""/>", False, , "<MODROW><SQLWHERE2>" & sql & Sql2 & "</SQLWHERE2></MODROW>")
                Case "prodlot"
                    Dim Sql As String = ""
                    Dim PIDUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@PidUnitID", Params))
                    If myUtils.cValTN(PIDUnitID) > 0 Then Sql = "<MODROW><SQLWHERE2>PIDUnitID = " & PIDUnitID & "</SQLWHERE2></MODROW>"
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdLotID""/>", False,, Sql)
                Case "itemreq"
                    Dim Classtype As String = myUtils.cStrTN(myContext.SQL.ParamValue("@classtype", Params))
                    If myUtils.IsInList(Classtype, "A") Then
                        str2 = " and MatReqType = 'M'"
                    Else
                        str2 = " and (MatReqType = 'W' OR MatReqType = 'S')"
                    End If

                    Dim sql As String = myContext.SQL.PopulateSQLParams("CampusID = @campusid and ItemVMSID = @itemvmsid and isnull(iscompleted,0) = 0 and IsAgainstJWO = @isagainstjwo " & str2 & "", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""MATREQITEMID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "puraccass"
                    Dim TransType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@transtype", Params))
                    Dim ValuationClass As String = myUtils.cStrTN(myContext.SQL.ParamValue("@valuationclass", Params))
                    Dim NewFixedAssetIDcsv As String = myUtils.cStrTN(myContext.SQL.ParamValue("@newfixedassetcsv", Params))
                    Dim OldFixedAssetIDcsv As String = myUtils.cStrTN(myContext.SQL.ParamValue("@oldfixedassetcsv", Params))
                    Dim oAssetProc As New clsFixedAssetProc(myContext)
                    Dim strXML As String = oAssetProc.ModRowXML(TransType, ValuationClass, NewFixedAssetIDcsv, OldFixedAssetIDcsv)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""FIXEDASSETID""/>", True, , strXML)

                Case "lookuprate"
                    Dim ItemVmsId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ItemVmsId", Params))
                    Dim VendorID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@VendorID", Params))
                    Dim Sql As String = "select distinct OrderNum,OrderDate,BasicRate from purlistoitems() where itemvmsid=" & ItemVmsId & " and VendorID = " & VendorID & " order by orderdate desc"
                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.MainConf("FORMATXML") = "<COL KEY=""Dispname"" CAPTION=""Campus""/>"
                    Model.MainGrid.QuickConf(Sql, True, "2-2-1", True, "Select Rate")
            End Select
        End If
        Return Model
    End Function

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "itemvms"
                Dim sql As String = "Select PIDUnitID, VarNum from ItemVMS where ItemVMSID = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Case "suppitems"
                oRet.Data = Me.GenerateData("supp", frmIDX.ToString)
        End Select
        Return oRet
    End Function

    Protected Overrides Function GenerateData(DataKey As String, ID As String) As DataSet
        Dim ds As DataSet, dic As New clsCollecString(Of String)
        Select Case DataKey.Trim.ToLower
            Case "supp"
                dic.Add("suppitems", "select * from SupplierItem where VendorID = " & myUtils.cValTN(ID))
                dic.Add("vendorterms", "Select VendorID, PartyID, Discount, PayTerms, TransMode, ShipTerms, FreightIns from PurListVendor() where VendorID = " & myUtils.cValTN(ID))
                ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        End Select
        Return ds
    End Function

    Public Overrides Function GenerateParamsOutput(dataKey As String, Params As List(Of clsSQLParam)) As clsProcOutput
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "ordernumdescrip"
                    Dim TotalQty As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@totalqty", Params))
                    Dim QtyUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@qtyunitid", Params))
                    Dim ItemVMSID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@itemvmsid", Params))
                    oRet.Description = myFuncs.CalcOrderNumDescrip(myContext, ItemVMSID, TotalQty, QtyUnitID)
            End Select
        End If
        Return oRet
    End Function

    Public Overrides Sub OperateProcess(processKey As String)
        Dim ObjPurVouchProc As New clsPurVouchProc(myContext)
        Select Case processKey.Trim.ToLower
            Case "generate"
                Dim ds = Me.DatasetCollection("generatedata")
                ObjPurVouchProc.GeneratePurItems(myRow.Row, ds.Tables("matreq").Rows(0), ds.Tables("MatReqItem"), Me.dsForm)
        End Select
    End Sub
End Class