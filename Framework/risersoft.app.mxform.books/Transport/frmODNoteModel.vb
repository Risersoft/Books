Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization
<DataContract>
Public Class frmODNoteModel
    Inherits clsFormDataModel
    Dim myViewSpare, myViewSerialNoteItem, myViewFASO, myViewSerialNoteSp, myViewPackListSP, myViewPackListItem As clsViewModel
    Dim myViewCostLot, myViewCostWBS, myViewCostCenter As clsViewModel
    Dim PPFinal As Boolean = False, CostFielDName As String, CostTableTame As String

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Items")
        myViewSpare = Me.GetViewModel("Spare")
        myViewSerialNoteItem = Me.GetViewModel("SerialSO")
        myViewSerialNoteSp = Me.GetViewModel("SerialSp")
        myViewFASO = Me.GetViewModel("FASO")
        myViewPackListSP = Me.GetViewModel("PackListSP")
        myViewPackListItem = Me.GetViewModel("PackListItem")
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
        Dim Sql As String

        Sql = "Select CampusID, DispName,TaxAreaCode, DivisionCodeList, WODate,CompletedOn, SalesOrderID, PidUnitID from mmlistCampus()  Order by DispName"
        Me.AddLookupField("CampusID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Campus").TableName)
        Me.AddLookupField("InvoiceCampusID", "Campus")
        Me.AddLookupField("ProjectCampusID", "Campus")
        Me.AddLookupField("OdNoteItem", "CampusID", "Campus")


        Sql = "SELECT  VendorID, VendorName, TaxAreaCode, ImportAllow, TaxAreaID FROM  purListVendor() where VendorType in ('MS','EM') Order By VendorName"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Vendor").TableName)
        Me.AddLookupField("OdNoteItem", "VendorID", "Vendor ")

        Sql = "SELECT  CustomerID, CustDescrip, MainPartyID,TaxAreaCode, TaxAreaID, PartyType FROM slsListCustomer() Order By PartyName"
        Me.AddLookupField("CustomerID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Customer").TableName)
        Me.AddLookupField("OdNoteItem", "CustomerID", "Customer")

        Sql = "Select CodeWord, DescripShort, Tag, Tag3 from CodeWords where CodeClass = 'ODNote' and CodeType = 'NoteType'"
        Me.AddLookupField("ChallanType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "ChallanType").TableName)

        Sql = myFuncsBase.CodeWordSQL("Stock", "taxtype", "")
        Me.AddLookupField("TaxType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "TaxType").TableName)
        Me.AddLookupField("OdNoteItem", "TaxType2", "TaxType")

        Sql = myFuncsBase.CodeWordSQL("Invoice", "TaxType", "", "CodeWord Desc")
        Me.AddLookupField("TaxInvoiceType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "TaxInvoiceType").TableName)

        Sql = "select TaxAreaID, Descrip,TaxAreaCode from TaxArea Order by Descrip"
        Me.AddLookupField("POSTaxAreaID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "POS").TableName)


        Sql = "Select Divisionid, DivisionCode from Division order by DivisionCode"
        Me.AddLookupField("DivisionID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Division").TableName)
        '-----------------------------------------------------------------

        Sql = "Select MatDepID, DepName, CampusID, IsStore, IsShop, WODate,CompletedOn from mmListDepsMat()  Order by DepName"
        Me.AddLookupField("OdNoteItem", "MatDepID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "DepsMat").TableName)

        Sql = "Select ItemUnitID, UnitName from ItemUnits"
        Me.AddLookupField("OdNoteItem", "ItemUnitIDEntry", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Units").TableName)
        Me.AddLookupField("OdNoteItem", "ItemUnitID", "Units")
        Me.AddLookupField("OdNoteItem", "ItemUnitID2", "Units")
        Me.AddLookupField("OdNoteItem", "OrderRateUnitID", "Units")
        Me.AddLookupField("OdNoteSpare", "ItemUnitID", "Units")

        '-----------------------------------------------------------------

        Sql = "Select MatMvtReasonID, Reason, MatMvtCode from MatMvtReason  Order By Reason"
        Me.AddLookupField("OdNoteItem", "MatMvtReasonID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Reason").TableName)

        Sql = "Select Distinct MatMvtCode.MatMvtCode, Description, MvtType, DocRefType, ReasonField, EntRefType, PricingType, DOCSubType, OrderUpdateCode,TaxTypeSrc from MatMvtCode Inner Join MatMvtDocType on MatMvtCode.MatMvtCode = MatMvtDocType.MatMvtCode where MatMvtCode.MatMvtCode in (Select MatMvtCode from MatMvtDocType Where DocType = 'ODN')"
        Me.AddLookupField("OdNoteItem", "MvtCode", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "MatMvtCode").TableName)

        Sql = myFuncsBase.CodeWordSQL("Stock", "qtytype1", "")
        Me.AddLookupField("OdNoteItem", "QtyTypeDes", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "QtyType").TableName)
        Me.AddLookupField("OdNoteItem", "QtyTypeSrc", "QtyType")

        Sql = myFuncsBase.CodeWordSQL("Material", "", "(CodeType = 'tpo' or CodeType = 'tmo')")
        Me.AddLookupField("OdNoteItem", "StockStage", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "StockStage").TableName)
        Me.AddLookupField("OdNoteItem", "StockStage2", "StockStage")

        Sql = myFuncsBase.CodeWordSQL("Material", "SpStock", "")
        Me.AddLookupField("OdNoteItem", "SpStock", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "SpStock").TableName)

        '----------------frmItemSelect-------------------------------------------
        Sql = "Select ItemId, ItemCode, ItemName, UnitName, ItemUnitId, IsEto, SubCatId, ItemUnitId2, OrderQtyUnitId, OrderRateUnitId, IssueUnitId, OrderQtyNumReq, OrderQtyNumText,ValuationClass from InvListItems()  Order by ItemName"
        Me.AddLookupField("OdNoteItem", "ItemId", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Items").TableName)

        Sql = "Select VarNum as VarNumId, VarNum, VarName, ItemId, PidUnitId from PIDUnitItemVar where PidUnitItemVarId = 0 Order by VarName"
        Me.AddLookupField("OdNoteItem", "VarNum", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "VarNum").TableName)

        Sql = "Select VarNum as VarNumId, VarNum, VarName, PidUnitId from PIDUnitItemVar where PidUnitItemVarId = 0 Order by VarName"
        Me.AddLookupField("OdNoteItem", "NewVarNum", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "NewVarNum").TableName)


        Sql = "Select PartyID, PartyName, TaxAreaID from GenListParty() Order By PartyName"
        Me.AddLookupField("ConsigneeID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "Party").TableName)


        Sql = "Select UserID, FullName, JoinDate, LeaveDate, isdeleted from genListUser() order by FullName"
        Me.AddLookupField("CheckedByID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "User").TableName)

        Sql = myFuncsBase.CodeWordSQL("OdNoteSpare", "ItemType", "")
        Me.AddLookupField("OdNoteSpare", "ItemType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "ItemType").TableName)

        Sql = "Select Class as ClassCode, Class, ClassType, (','+TransTypeCSV) as TransTypeCSV from AccountClass Where ClassType In ('M', 'A', 'S') Order by Class"
        Me.AddLookupField("OdNoteItem", "ValuationClass", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "ValuationClass").TableName)
        Me.AddLookupField("OdNoteSpare", "ValuationClass", "ValuationClass")

        Sql = "Select Code, Code + ' | ' + Description, Ty from HSNsac Order By Code"
        Me.AddLookupField("OdNoteItem", "hsn_sc", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "HsnSac").TableName)
        Me.AddLookupField("OdNoteSpare", "hsn_sc", "HsnSac")

        Sql = "Select CodeWord, DescripShort from CodeWords where CodeType in ('transtype', 'tmo', 'tpo') and CodeWord in ('FG','ST','WIP','SUN')"
        Me.AddLookupField("OdNoteSpare", "TransType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "TransType").TableName)

        Sql = myFuncsBase.CodeWordSQL("Invoice", "SupplyType", "")
        Me.AddLookupField("sply_ty", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql), "SupplyType").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim objOdNoteProc As New clsODNoteProc(myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)
        Dim sql, str1 As String

        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        Me.FormPrepared = False
        Dim oRet As clsProcOutput = Me.GetRowLock(prepMode, "ODNoteID", prepIDX)
        If oRet.Success Then
            sql = "Select *, 0 as SalesOrderID, 0 as MainPartyID from OdNote Where OdNoteID = " & prepIDX
            Me.InitData(sql, "DispatchID,ChallanType", oView, prepMode, prepIDX, strXML)

            If myUtils.cValTN(myRow("DispatchID")) > 0 Then
                Dim rr() As DataRow = Me.GenerateData("salesorder", myUtils.cValTN(myRow("DispatchID"))).Tables(0).Select
                If rr.Length > 0 Then
                    If myUtils.cValTN(rr(0)("SalesOrderID")) > 0 Then myRow("SalesOrderID") = myUtils.cValTN(rr(0)("SalesOrderID"))
                    If myUtils.cValTN(rr(0)("POSTaxAreaID")) > 0 Then myRow("POSTaxAreaID") = myUtils.cValTN(rr(0)("POSTaxAreaID"))
                    If myUtils.cValTN(rr(0)("ConsigneeID")) > 0 Then myRow("ConsigneeID") = myUtils.cValTN(rr(0)("ConsigneeID"))
                    myRow("ConsigneePrefix") = myUtils.cStrTN(rr(0)("ConsigneePrefix"))
                    myRow("DeliveryTo") = myUtils.cStrTN(rr(0)("DeliveryTo"))
                End If

                If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                    Dim rr1() As DataRow = Me.GenerateData("mainparty", myUtils.cValTN(myRow("SalesOrderID"))).Tables(0).Select
                    If rr1.Length > 0 Then
                        myRow("MainPartyID") = myUtils.cValTN(rr1(0)("MainPartyID"))
                    End If
                End If
            End If

            If frmMode = EnumfrmMode.acAddM Then
                myRow("ChallanDate") = Now.Date
            Else
                Dim rPostPeriod As DataRow = objOdNoteProc.oMasterFICO.rPostPeriod(myUtils.cValTN(myRow("PostPeriodID")))
                If Not IsNothing(rPostPeriod) Then
                    PPFinal = myUtils.cBoolTN(rPostPeriod("IsFinal"))
                End If
            End If

            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "FG", "RM") Then
                myRow("IgnoreZPricing") = 1
            End If

            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), risersoft.app.mxform.myFuncs.ChallanStr()) Then
                CostFielDName = "ODNoteSpareID"
                CostTableTame = "ODNoteSpare"
            Else
                CostFielDName = "ODNoteItemID"
                CostTableTame = "ODNoteItem"
            End If

            Me.BindDataTable(myUtils.cValTN(prepIDX))

            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), risersoft.app.mxform.myFuncs.ChallanStr()) Then
                objPricingCalcBase.InitData(myRow.Row, "ODNoteID", myUtils.cValTN(frmIDX), "ChallanDate", "ODNoteSpareID", "QtyRate", Me.dsForm.Tables("ODNoteSpare"))
            Else
                objPricingCalcBase.InitData(myRow.Row, "ODNoteID", myUtils.cValTN(frmIDX), "ChallanDate", "ODNoteItemID", "QtyRate", Me.dsForm.Tables("ODNoteItem"))
            End If

            myView.MainGrid.BindGridData(Me.dsForm, 1)
            myView.MainGrid.QuickConf("", True, "1-2-1-1-1-1-1-1-1-1-1")
            str1 = "<BAND IDFIELD=""OdNoteItemId"" TABLE=""OdNoteItem"" INDEX=""0""><COL KEY=""ODNoteItemID, ODNoteID, ODNoteSpareID, ItemID, PriceSlabID, PIDUnitID, VarNum, RT, hsn_sc,nature, sply_ty, MvtCode, SrcDesTypeItem, SpStock, StockStage, StockStage2, TaxType2, VendorID, CustomerID, MatDepID, CampusID, MatMvtReasonID, QtyEntry, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, QtyRate, Remark, BasicRate, AmountTot, AmountWV, PPSubType, AmountTrWv,IdenMarks,ItemSuffix,ValuationClass""/><COL KEY=""ItemName"" CAPTION=""Item Name""/><COL KEY=""QtyEntry"" CAPTION=""Qty""/><COL KEY=""UnitName"" CAPTION=""Unit Name""/></BAND>"
            myView.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

            myViewPackListItem.MainGrid.BindGridData(Me.dsForm, 2)
            myViewPackListItem.MainGrid.QuickConf("", True, "1-1-2-1-1-1-1-1-1-1", True)
            myViewPackListItem.MainGrid.PrepEdit("<BAND TABLE=""ODNoteItemPack"" IDFIELD=""ODNoteItemPackID"" INDEX=""0""><COL KEY=""ODNoteItemPackID, ODNoteID, ODNoteItemID, Item, Qty, IdenMarks, Dimension, Rate, BeenTested, IsMax""/><COL KEY=""UnitName"" LOOKUPSQL=""Select UnitName from ItemUnits""/><COL  NOEDIT=""TRUE"" KEY=""SerialNum""/><COL  NOEDIT=""TRUE"" KEY=""SubSerialNum""/></BAND>", EnumEditType.acCommandAndEdit)

            myViewSpare.MainGrid.BindGridData(Me.dsForm, 3)
            myViewSpare.MainGrid.QuickConf("", True, "1-1-1-1-1-1")
            str1 = "<BAND INDEX = ""0"" TABLE = ""ODNoteSpare"" IDFIELD=""ODNoteSpareID""><COL KEY =""ODNoteSpareID, ODNoteID, SOSpareID, SOSerViceID, PIDUnitID, PriceSlabID,ItemUnitID,RT, hsn_sc,nature, ClassType, QtyRate, BasicRate, AmountTot, AmountWV, PPSubType, ItemType,ItemDescrip,IdenMarks,ItemSuffix, ValuationClass, TransType""/></BAND>"
            myViewSpare.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

            myViewPackListSP.MainGrid.BindGridData(Me.dsForm, 6)
            myViewPackListSP.MainGrid.QuickConf("", True, "1-1-2-1-1-1-1-1-1-1", True)
            myViewPackListSP.MainGrid.PrepEdit("<BAND TABLE=""ODNoteItemPack"" IDFIELD=""ODNoteItemPackID"" INDEX=""0""><COL KEY=""ODNoteItemPackID, ODNoteID,ODNoteSpareID, Item, Qty, IdenMarks, Dimension, Rate, BeenTested, IsMax""/><COL KEY=""UnitName"" LOOKUPSQL=""Select UnitName from ItemUnits""/><COL  NOEDIT=""TRUE"" KEY=""SerialNum""/><COL  NOEDIT=""TRUE"" KEY=""SubSerialNum""/></BAND>", EnumEditType.acCommandAndEdit)

            If myUtils.cValTN(myRow("SalesOrderID")) > 0 Then
                Dim dtPS As DataTable = dsForm.Tables("ProdSerialItem")
                myViewSerialNoteItem.MainGrid.BindGridData(dsForm, dtPS.DataSet.Tables.IndexOf(dtPS))
                myViewSerialNoteItem.MainGrid.QuickConf("", True, "1-1-1")
                str1 = "<BAND INDEX = ""0"" TABLE = ""ProdSerialItem"" IDFIELD=""ProdSerialItemID""><COL KEY ="" ProdSerialID, InvoiceItemID, ODNoteItemID ""/></BAND>"
                myViewSerialNoteItem.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

                myViewSerialNoteSp.MainGrid.BindGridData(Me.dsForm, 5)
                myViewSerialNoteSp.MainGrid.QuickConf("", True, "1-1-1")
                str1 = "<BAND INDEX = ""0"" TABLE = ""ProdSerialItem"" IDFIELD=""ProdSerialItemID""><COL KEY ="" ProdSerialID, OdNoteSpareID ""/></BAND>"
                myViewSerialNoteSp.MainGrid.PrepEdit(str1, EnumEditType.acCommandOnly)

                sql = "Select SOSpareID, SpareName, PIDInfo from slsListSOSpares() where SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID"))
                Me.AddLookupField("ODNoteSpare", "SoSpareID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SOSpare").TableName)
                Me.AddLookupField("ODNoteSpare", "PIDInfoSp", "SOSpare")

                sql = "Select SOServiceID, ServiceName, PIDInfo from slsListSOService() Where SalesOrderID = " & myUtils.cValTN(myRow("SalesOrderID"))
                Me.AddLookupField("ODNoteSpare", "SoServiceID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "SOService").TableName)
                Me.AddLookupField("ODNoteSpare", "PIDInfoSvc", "SOService")
            End If

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



            myContext.App.DelNonRelevant(Me.dsCombo.Tables("User"), "isdeleted=1", "UserID='" & myUtils.cStrTN(myRow("CheckedByID")) & "'")
            objPricingCalcBase.CheckPriceSlabElement()

            Me.FormPrepared = True
        Else
            Me.AddError("", Nothing, 0, "", "", oRet.Message)
        End If

        Return Me.FormPrepared
    End Function
    Protected Overrides Function GenerateData(DataKey As String, ID As String) As DataSet
        Dim ds As DataSet
        Select Case DataKey.Trim.ToLower
            Case "salesorder"
                Dim Sql As String = "Select * from Dispatch where DispatchID = " & myUtils.cValTN(ID) & ""
                ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

            Case "mainparty"
                Dim Sql As String = "Select CustomerID from SalesOrder where SalesOrderID = " & myUtils.cValTN(ID) & ""
                Dim ds1 As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                If ds1.Tables(0).Rows.Count > 0 Then
                    Sql = "Select MainPartyID from slsListCustomer() where CustomerID = " & myUtils.cValTN(ds1.Tables(0).Rows(0)("CustomerID")) & ""
                    ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)
                End If
        End Select
        Return ds
    End Function
    Private Sub BindDataTable(ByVal OdNoteID As Integer)
        Dim ds As DataSet, Sql, Sql1, Sql2, Sql3, Sql4, Sql5, Sql6, Sql7, Sql8, Sql9 As String

        Sql1 = "Select ODNoteItemID, ODNoteID, ODNoteSpareID, ItemID, PriceSlabID, PIDUnitID, VarNum,RT, hsn_sc,nature, sply_ty, MvtCode, MvtType, VendorID, SOSpareID, QtySOSpare, CustomerID, CampusID, MatMvtReasonID, AmountTrWv, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, StockStage2, TaxType2, ppSubType, ClassType, ValuationClass, WOInfo,IdenMarks,ItemSuffix, ItemCode, ItemName, UnitName, SpStock, StockStage, QtyEntry, QtyRate, BasicRate, AmountTot, AmountWV, Remark FROM  trpListODNoteItem()  where ODNoteID = " & OdNoteID & " Order by ItemID"
        Sql2 = "select ODNoteItemPackID, ODNoteID, ODNoteItemID, SerialNum, SubSerialNum, Item, Qty, UnitName, Rate, IdenMarks, Dimension, BeenTested, IsMax  from ODNoteItemPack  Where ODNoteItemID in (select ODNoteItemID from ODNoteItem where ODNoteID = " & OdNoteID & ")"
        Sql3 = "Select ODNoteSpareID, ODNoteID, SOSpareID, SOServiceID, PIDUnitID, PriceSlabID,RT, hsn_sc,nature, ClassType, TransType, ppSubType, UnitPrice, ValuationClass,IdenMarks,ItemSuffix, ItemUnitID, ItemType, ItemDescrip, QtyRate, BasicRate, AmountTot, AmountWV from trpListODNoteScSv()  Where ODNoteID = " & OdNoteID & ""
        Sql4 = "Select ProdSerialItemID, ProdSerialItem.ProdSerialID, WorkOrderID, ODNoteItemID, ProdSerialNum, WOInfo, LotNum from ProdSerialItem Inner join plnListProdSerial() on ProdSerialItem.ProdSerialID = plnListProdSerial.ProdSerialID  Where ODNoteItemID in (select ODNoteItemID from ODNoteItem where ODNoteID = " & OdNoteID & ")"
        Sql5 = "Select ProdSerialItemID, ProdSerialItem.ProdSerialID, WorkOrderID, ODNoteSpareID, ProdSerialNum, WOInfo, LotNum from ProdSerialItem Inner join plnListProdSerial() on ProdSerialItem.ProdSerialID = plnListProdSerial.ProdSerialID  Where ODNoteSpareID in (select ODNoteSpareID from ODNoteSpare where ODNoteID = " & OdNoteID & ")"
        Sql6 = "select ODNoteItemPackID, ODNoteID,ODNoteSpareID, SerialNum, SubSerialNum, Item, Qty, UnitName, Rate, IdenMarks, Dimension, BeenTested, IsMax  from ODNoteItemPack  Where ODNoteSpareID in (select ODNoteSpareID from ODNoteSpare where ODNoteID = " & OdNoteID & ")"
        Sql7 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as " & CostFielDName & ", ProdLotID, WoInfo, LotNum, PerValue from accListCostAssign() where isNull(ProdLotID,0) > 0 and PIDField = 'OdNoteID' and PIDValue = " & OdNoteID & ""
        Sql8 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as " & CostFielDName & ", WBSElementID, SerialNum,WBSElemType, woinfo, DescripWo, Description, PerValue from accListCostAssign() where isNull(WBSElementID,0) > 0 and PIDField = 'OdNoteID' and PIDValue = " & OdNoteID & ""
        Sql9 = "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, pIDValueItem as " & CostFielDName & ", CostCenterID,CostCenterName, PerValue from accListCostAssign() where isNull(CostCenterID,0) > 0 and PIDField = 'OdNoteID' and PIDValue = " & OdNoteID & ""
        Sql = Sql1 & "; " & Sql2 & "; " & Sql3 & "; " & Sql4 & "; " & Sql5 & "; " & Sql6 & ";" & Sql7 & ";" & Sql8 & ";" & Sql9
        ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

        myUtils.AddTable(Me.dsForm, ds, "OdNoteItem", 0)
        myUtils.AddTable(Me.dsForm, ds, "OdNoteItemPackItem", 1)
        myUtils.AddTable(Me.dsForm, ds, "ODNoteSpare", 2)
        myUtils.AddTable(Me.dsForm, ds, "ProdSerialItem", 3)
        myUtils.AddTable(Me.dsForm, ds, "Serial", 4)
        myUtils.AddTable(Me.dsForm, ds, "OdNoteItemPackSp", 5)
        myUtils.AddTable(Me.dsForm, ds, "CostLot", 6)
        myUtils.AddTable(Me.dsForm, ds, "CostWBS", 7)
        myUtils.AddTable(Me.dsForm, ds, "CostCenter", 8)

        myContext.Tables.SetAuto(Me.dsForm.Tables("OdNoteItem"), Me.dsForm.Tables("OdNoteItemPackItem"), "OdNoteItemId", "_OdNoteItemPackItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("ODNoteSpare"), Me.dsForm.Tables("OdNoteItem"), "ODNoteSpareID", "_ODNoteSpare")
        myContext.Tables.SetAuto(Me.dsForm.Tables("OdNoteItem"), Me.dsForm.Tables("ProdSerialItem"), "OdNoteItemId", "_ProdSerialItem")
        myContext.Tables.SetAuto(Me.dsForm.Tables("ODNoteSpare"), Me.dsForm.Tables("Serial"), "ODNoteSpareID", "_Serial")
        myContext.Tables.SetAuto(Me.dsForm.Tables("ODNoteSpare"), Me.dsForm.Tables("OdNoteItemPackSp"), "ODNoteSpareID", "_OdNoteItemPackSp")
        myContext.Tables.SetAuto(Me.dsForm.Tables(CostTableTame), Me.dsForm.Tables("CostLot"), CostFielDName, "_CostLot")
        myContext.Tables.SetAuto(Me.dsForm.Tables(CostTableTame), Me.dsForm.Tables("CostWBS"), CostFielDName, "_CostWBS")
        myContext.Tables.SetAuto(Me.dsForm.Tables(CostTableTame), Me.dsForm.Tables("CostCenter"), CostFielDName, "_CostCenter")
    End Sub

    Public Overrides Function Validate() As Boolean
        Me.InitError()
        If Me.SelectedRow("campusid") Is Nothing Then Me.AddError("campusid", "Please Select Campus")
        If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "SCSV") AndAlso Me.SelectedRow("CustomerID") Is Nothing Then Me.AddError("CustomerID", "Please Select Customer")
        If Me.SelectedRow("ChallanType") Is Nothing Then Me.AddError("ChallanType", "Please Select Challan Type")
        If Me.SelectedRow("TaxInvoiceType") Is Nothing Then Me.AddError("TaxInvoiceType", "Please Select Invoice Type")
        If Me.SelectedRow("TaxType") Is Nothing Then Me.AddError("TaxType", "Please Select Tax Type")
        If Me.SelectedRow("DivisionID") Is Nothing Then Me.AddError("DivisionID", "Please Select Division")

        If Me.myViewSpare.MainGrid.myDV.Table.Select.Length = 0 AndAlso Me.myView.MainGrid.myDV.Table.Select.Length = 0 Then Me.AddError("", "Please Enter Some Transactions")
        If myUtils.cValTN(myRow("Campusid")) > 0 Then
            Dim CompanyID As Integer = myContext.CommonData.GetCompanyIDFromCampus(myUtils.cValTN(myRow("Campusid")))
            myFuncs.ValidPostPeriod(myContext, Me, myUtils.cValTN(CompanyID), Me.myRow("ChallanDate"), PPFinal)
        End If

        If myUtils.cValTN(myRow("PostPeriodId")) = 0 Then Me.AddError("ChallanDate", "Please Select Valid Post Period")
        If myUtils.cValTN(myRow("MatVouchID")) > 0 Then Me.AddError("", "Material Voucher already created. Not allow to edit")
        If myUtils.cValTN(myRow("InvoiceID")) > 0 Then Me.AddError("", "Invoice already created. Not allow to edit")
        Dim rr1() As DataRow = myContext.Data.SelectDistinct(dsForm.Tables("OdNoteItem"), "MvtType", False).Select
        If rr1.Length > 1 Then
            Me.AddError("", "Invalid Mvt Code. Mvt Type should be same.")
        End If


        If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "SCSP", "TRSO", "SCSTB", "TRJB") Then
            Dim rr3() As DataRow = dsForm.Tables("OdNoteItem").Select("isnull(ODNotespareID,0) = 0")
            If rr3.Length > 0 Then
                Me.AddError("", "Please Select Spare")
            End If
        End If

        If Not myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "RC", "RV", "SCUB", "SCSP", "RCWB") Then
            '---SCSP Validation is Not mendatory because its creates by Unit.
            If myUtils.cValTN(myRow("PriceSlabID")) = 0 Then Me.AddError("", "Please Select Pricing")
            Dim rr2() As DataRow = Nothing
            If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "SCSV", "TRSO", "SCSTB", "TRJB") Then
                rr2 = dsForm.Tables("OdNoteSpare").Select("isnull(PriceSlabID,0) = 0")
            Else
                rr2 = dsForm.Tables("OdNoteItem").Select("isnull(PriceSlabID,0) = 0")
            End If
            If Not IsNothing(rr2) AndAlso rr2.Length > 0 Then
                Me.AddError("", "Please Select Pricing for Item")
            End If
        End If

        Dim rr4() As DataRow = dsForm.Tables("OdNoteSpare").Select("isnull(ValuationClass,'') = '' or isnull(TransType,'') = ''")
        If Not IsNothing(rr4) AndAlso rr4.Length > 0 Then
            Me.AddError("", "Please Select Valuation Class and TransType")
        End If

        Dim rr5() As DataRow = dsForm.Tables("ODNoteItem").Select("PriceSlabID is Not Null and isnull(ValuationClass,'') = ''")
        If Not IsNothing(rr5) AndAlso rr5.Length > 0 Then
            Me.AddError("", "Please Select Valuation Class")
        End If

        If myUtils.cValTN(myRow("DispatchID")) > 0 AndAlso Me.myRow("SerialNum").ToString.Trim.Length = 0 Then Me.AddError("SerialNum", "Please Enter Serial No.")
        If myUtils.NullNot(myRow("VehicleType")) Then Me.AddError("VehicleType", "Please Enter Vehicle Type")
        myViewPackListItem.MainGrid.CheckValid("Item,IdenMarks,Dimension,UnitName", , , "<CHECK COND=""Qty>0"" MSG=""Qty should be greater than 0""/>")
        myViewPackListSP.MainGrid.CheckValid("Item,IdenMarks,Dimension,UnitName", , , "<CHECK COND=""Qty>0"" MSG=""Qty should be greater than 0""/>")

        If myUtils.cValTN(myRow("DispatchID")) = 0 AndAlso (Not myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "TS")) Then
            If myUtils.NullNot(myRow("ConsigneeID")) AndAlso myUtils.cStrTN(myRow("ConsigneePrefix")).ToString.Trim.Length = 0 Then Me.AddError("ConsigneePrefix", "Select a consignee or fill details")
        End If

        If myUtils.cValTN(myRow("ConsigneeID")) > 0 Then
            myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("ConsigneeID")("TaxAreaID"))
        Else
            If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 AndAlso Not Me.SelectedRow("CustomerID") Is Nothing Then myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("CustomerID")("TaxAreaID"))
            If myUtils.cValTN(myRow("POSTaxAreaID")) = 0 AndAlso Not Me.SelectedRow("VendorID") Is Nothing Then myRow("POSTaxAreaID") = myUtils.cValTN(Me.SelectedRow("VendorID")("TaxAreaID"))
        End If

        If Me.SelectedRow("POSTaxAreaID") Is Nothing Then Me.AddError("POSTaxAreaID", "Please select Place of Supply.")

        If myUtils.cValTN(myRow("PriceSlabID")) > 0 AndAlso myFuncs.CheckCostVouchPerValue(myContext, Me.dsForm.Tables(CostTableTame), CostFielDName, myViewCostLot, myViewCostWBS, myViewCostCenter) = False Then Me.AddError("", "Please enter Cost Assignment with correct %")
        Return Me.CanSave
    End Function

    Private Function CheckSoSpareID() As Boolean
        CheckSoSpareID = False
        Dim SOSpareID As String = myUtils.MakeCSV(dsForm.Tables("OdNoteItem").Select, "SoSpareID")
        Dim sql As String = "select * from trpListODNoteItem() where SOSpareID in (" & SOSpareID & ") and ChallanType <> 'SCSV' and ODNoteID <> " & myUtils.cValTN(frmIDX)
        Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
        Dim rr1() As DataRow = dt.Select
        If rr1.Length > 0 Then
            CheckSoSpareID = True
        End If
        Return CheckSoSpareID
    End Function

    Public Overrides Function VSave() As Boolean
        Dim Oret As clsProcOutput

        VSave = False
        If Me.Validate Then
            Dim objOdNoteProc As New clsODNoteProc(myContext)
            Oret = objOdNoteProc.CheckSpareQtyNote(myRow.Row, myViewSpare.MainGrid.myDV.Table)
            If Oret.Success = False Then
                Me.AddError("", Oret.Message)
            End If

            If (Not IsNothing(myViewSerialNoteItem.MainGrid.myDV)) AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "SCST") Then
                Oret = objOdNoteProc.CheckProdSerialNoteItem(myRow.Row, myView.MainGrid.myDV.Table, myViewSerialNoteItem.MainGrid.myDV.Table)
                If Oret.Success = False Then
                    Me.AddError("", Oret.Message)
                End If
            End If

            If (Not IsNothing(myViewSerialNoteSp.MainGrid.myDV)) AndAlso myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), "SCSP") Then
                Oret = objOdNoteProc.CheckProdSerialNoteSp(myRow.Row, myViewSpare.MainGrid.myDV.Table, myViewSerialNoteSp.MainGrid.myDV.Table)
                If Oret.Success = False Then
                    Me.AddError("", Oret.Message)
                End If
            End If

            If Me.CanSave Then
                Dim ObjVouch As New clsVoucherNum(myContext), objPricingCalcBase As New clsPricingCalcBase(myContext)
                ObjVouch.GetNewSerialNo("OdNoteID", myUtils.cStrTN(myRow("TaxType")), myRow.Row, "VouchNum")

                If myUtils.IsInList(myUtils.cStrTN(myRow("ChallanType")), risersoft.app.mxform.myFuncs.ChallanStr()) Then
                    objPricingCalcBase.InitData(myRow.Row, "ODNoteID", myUtils.cValTN(frmIDX), "ChallanDate", "ODNoteSpareID", "QtyRate", Me.dsForm.Tables("ODNoteSpare"))
                Else
                    objPricingCalcBase.InitData(myRow.Row, "ODNoteID", myUtils.cValTN(frmIDX), "ChallanDate", "ODNoteItemID", "QtyRate", Me.dsForm.Tables("ODNoteItem"))
                End If
                objPricingCalcBase.SaveAmounts(Nothing, "", "AmountTot", "", "")
                For Each r2 As DataRow In dsForm.Tables("OdNoteItem").Select()
                    objPricingCalcBase.SaveAmounts(r2, "BasicRate", "AmountTot", "AmountWV", "", "AmountTrWV")
                Next
                objPricingCalcBase.PopulateAccountingKeys(myRow("challandate"))

                If myUtils.cValTN(myRow("AmountTot")) = 0 Then
                    myRow("AmountTot") = myUtils.cValTN(myView.MainGrid.GetColSum("AmountTot"))
                End If

                Dim OdNoteDescrip As String = " ODNote No: " & myRow("VouchNum").ToString & ", Dt. " & Format(myRow("ChallanDate"), "dd-MMM-yyyy")
                Try
                    myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "OdNoteID", frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, Me.sqlForm)

                    frmIDX = myRow("OdNoteID")

                    myUtils.ChangeAll(dsForm.Tables("OdNoteItem").Select, "OdNoteID=" & frmIDX)
                    myUtils.ChangeAll(dsForm.Tables("OdNoteItemPackItem").Select, "OdNoteID=" & frmIDX)
                    myUtils.ChangeAll(dsForm.Tables("OdNoteItemPackSp").Select, "OdNoteID=" & frmIDX)
                    myUtils.ChangeAll(dsForm.Tables("ODNoteSpare").Select, "OdNoteID=" & frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("ODNoteSpare"), "Select ODNoteSpareID, ODNoteID, SOSpareID, SOSerViceID, PIDUnitID, PriceSlabID,ItemUnitID, ClassType, QtyRate, BasicRate, AmountTot, AmountWV, PPSubType, ItemType,ItemDescrip,IdenMarks,ItemSuffix, TransType, ValuationClass,RT, hsn_sc,nature from ODNoteSpare")
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("OdNoteItem"), "Select ODNoteItemID, ODNoteID, ODNoteSpareID, ItemID, PriceSlabID,AmountTrWv, ClassType, SOSpareID, QtySOSpare, PIDUnitID, VarNum, MvtCode, VendorID, CustomerID, CampusID, MatMvtReasonID, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, StockStage2, TaxType2, ppSubType, SpStock, StockStage, QtyEntry, QtyRate, BasicRate, AmountTot, AmountWV, Remark,IdenMarks,ItemSuffix, RT, hsn_sc,nature, sply_ty,ValuationClass from OdNoteItem", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("OdNoteItemPackItem"), "Select ODNoteItemPackID, ODNoteID, ODNoteItemID, SerialNum, SubSerialNum, Item, Qty, UnitName, IdenMarks, Dimension, Rate, BeenTested, IsMax from OdNoteItemPack", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("OdNoteItemPackSp"), "Select ODNoteItemPackID, ODNoteID,ODNoteSpareID, SerialNum, SubSerialNum, Item, Qty, UnitName, IdenMarks, Dimension, Rate, BeenTested, IsMax from OdNoteItemPack", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("ProdSerialItem"), "Select ProdSerialItemID, ProdSerialID, ODNoteItemID from ProdSerialItem", True)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("Serial"), "Select ProdSerialItemID, ProdSerialID, ODNoteSpareID from ProdSerialItem", True)


                    ChangeColRowwise(dsForm.Tables("CostLot"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostLot"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, ProdLotID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostWBS"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostWBS"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, WBSElementID, PerValue from CostAssign", True)

                    ChangeColRowwise(dsForm.Tables("CostCenter"), frmIDX)
                    myContext.Provider.objSQLHelper.SaveResults(dsForm.Tables("CostCenter"), "Select CostAssignID, IDField, PIDField, PIDValue, pIDFieldItem, pIDValueItem, CostCenterID, PerValue from CostAssign", True)


                    objPricingCalcBase.VSave()

                    Dim str As String = "Select Sum(TotalValueCalc) from accListPriceElemCalc() where ElemCode In ('bed','edec','edhec' ) and  PIDField='ODNoteID' And PIDValue = " & frmIDX & " And  cidvalue Is null"
                    Dim dt As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, str).Tables(0)

                    If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                        Dim DutyAmt As Double = myUtils.cValTN(dt.Rows(0)(0))
                        Dim RG23A As Double = DutyAmt - myUtils.cValTN(myRow("PLAAmount")) - myUtils.cValTN(myRow("RG23cAmount"))
                        myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, "Update ODNote set DutyAmount = " & Math.Round(DutyAmt, 2) & ", RG23aAmount = " & Math.Round(RG23A, 2) & " where ODNoteID = " & frmIDX)
                    End If

                    frmMode = EnumfrmMode.acEditM
                    myContext.Provider.dbConn.CommitTransaction(OdNoteDescrip, frmIDX)
                    VSave = True
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction(OdNoteDescrip, e.Message)
                    Me.AddException("", e)
                End Try
            End If
        End If
    End Function

    Private Sub ChangeColRowwise(dt As DataTable, OdNoteID As Integer)
        myUtils.ChangeAll(dt.Select, "PIDField=OdNoteID")
        myUtils.ChangeAll(dt.Select, "PIDValue=" & OdNoteID)
        myUtils.ChangeAll(dt.Select, "pIDFieldItem=" & CostFielDName)

        For Each r1 In dt.Select
            r1("pIDValueItem") = r1(CostFielDName)
        Next
    End Sub

    Public Overrides Function GenerateIDOutput(dataKey As String, frmIDX As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Select Case dataKey.Trim.ToLower
            Case "pidunititemvar"
                Dim sql As String = "Select *, varnum As varnumid from pidunititemvar where itemid = " & myUtils.cValTN(frmIDX) & " order by itemid, pidunitid, varnum"
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Case "pidunit"
                Dim sql As String = "Select * from plnlistpidunit() where pidunitid = " & myUtils.cValTN(frmIDX)
                oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Case Else
                oRet.Data = Me.GenerateData(dataKey, frmIDX.ToString)
        End Select
        Return oRet
    End Function

    Public Overrides Function GenerateParamsModel(vwState As clsViewState, SelectionKey As String, Params As List(Of clsSQLParam)) As clsViewModel
        Dim Model As clsViewModel = Nothing
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case SelectionKey.Trim.ToLower
                Case "spares"       '(SCSP + SCSV)
                    Dim Str1, Str2, str3 As String
                    Dim DispatchID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@DispatchID", Params))

                    If myContext.SQL.Exists(Params, "@PidUnitID") Then
                        str3 = "IsNull(PidUnitID, 0) = " & myUtils.cValTN(myContext.SQL.ParamValue("@PidUnitID", Params)) & ""
                    Else
                        str3 = ""
                    End If

                    If DispatchID > 0 Then
                        Str2 = myContext.SQL.PopulateSQLParams("SOSpareID In (Select SOSpareID from SOSpareItem where DispatchID = @dispatchid)", Params)
                    Else
                        Str2 = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    End If

                    If myContext.SQL.Exists(Params, "@ChallanType") Then
                        Dim ChallanType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@ChallanType", Params))
                        If myUtils.IsInList(myUtils.cStrTN(ChallanType), "SCSV") Then
                            Str1 = myContext.SQL.PopulateSQLParams("SOSpareID Not In (@sospareid) And isnull(BillingLot,0) = 1", Params)
                        Else
                            Str1 = myContext.SQL.PopulateSQLParams("SOSpareID Not In (@sospareid) And isnull(BillingLot,0) = 0", Params)
                        End If
                    Else
                        Str1 = myContext.SQL.PopulateSQLParams("TransType = @transtype And BillingLot = 0", Params)
                    End If

                    Dim sql As String = myUtils.CombineWhere(False, Str1, Str2, str3)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOSpareID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "services"
                    Dim Str1, Str2, str3 As String
                    Str1 = ""

                    If myContext.SQL.Exists(Params, "@PidUnitID") Then
                        str3 = "IsNull(PidUnitID, 0) = " & myUtils.cValTN(myContext.SQL.ParamValue("@PidUnitID", Params)) & ""
                    Else
                        str3 = ""
                    End If

                    If myContext.SQL.Exists(Params, "@soserviceid") Then Str1 = myContext.SQL.PopulateSQLParams("SOServiceID Not In (@soserviceid)", Params)
                    Str2 = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    Dim sql As String = myUtils.CombineWhere(False, Str1, Str2, str3)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""SOServiceID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")

                Case "serials"
                    Dim str1, str2, str3 As String
                    Dim DispatchID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@DispatchID", Params))
                    If DispatchID > 0 Then
                        str1 = myContext.SQL.PopulateSQLParams("ProdSerialId In (Select ProdSerialId from ProdSerialItem where DispatchID = @dispatchid)", Params)
                    Else
                        str1 = myContext.SQL.PopulateSQLParams("SalesOrderID = @salesorderid", Params)
                    End If
                    str2 = ""
                    str3 = ""
                    If myContext.SQL.Exists(Params, "@WorkOrderID") Then str2 = myContext.SQL.PopulateSQLParams("WorkOrderID In (@WorkOrderID)", Params)
                    If myContext.SQL.Exists(Params, "@ProdSerialIDCSV") Then str3 = myContext.SQL.PopulateSQLParams("ProdSerialID Not In (@ProdSerialIDCSV)", Params)
                    Dim sql As String = myUtils.CombineWhere(False, str1, str2, str3)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ProdSerialID""/>", True, , "<MODROW><SQLWHERE2>" & sql & "</SQLWHERE2></MODROW>")
                Case "itempack"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("<IDX VALUE=""@odnoteid""/>", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ODNoteItemPackID""/>", True, , Sql)
                Case "pidunit"
                    Dim Sql As String
                    Dim SalesOrderID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@SalesOrderID", Params))
                    If myUtils.cValTN(SalesOrderID) > 0 Then
                        Sql = "<MODROW><SQLWHERE2>" & "SalesOrderID = " & SalesOrderID & "</SQLWHERE2></MODROW>"
                    End If
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""PIDUnitID""/>", False, , Sql)
                Case "stockbalcamp"
                    Dim ItemID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ItemID", Params))
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim Dated As Date = myContext.SQL.ParamValue("@Date", Params)
                    Dim CompanyId As Integer = myContext.CommonData.GetCompanyIDFromCampus(CampusID)
                    Dim PIDUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@PIDUnitID", Params))
                    Dim str As String = ""
                    If PIDUnitID > 0 Then
                        str = "CampusID = " & CampusID & " And ItemID = " & ItemID & " And PIDUnitID = " & PIDUnitID
                    Else
                        str = "CampusID = " & CampusID & " And ItemID = " & ItemID
                    End If

                    Model = New clsViewModel(vwState, myContext)
                    Dim objProc As New clsMVProcQtyCampus(myContext)
                    Dim dt As DataTable = objProc.ItemStockBalance(CompanyId, Dated, str, "IT", True)

                    dt = myContext.Tables.SelectTable(dt, {"DispName", "SpStock", "StockStage", "TaxType", "WoInfo", "VarNum", "Qty", "QtyTransit", "QtyTot", "UnitName"})
                    Model.MainGrid.BindGridData(dt.DataSet, 0)
                    Model.MainGrid.QuickConf("", True, "2-1-1-1-1.2-1-.8-1-1-1")
                Case "stockbaldep"
                    Dim ItemID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ItemID", Params))
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim Dated As Date = myContext.SQL.ParamValue("@Date", Params)
                    Dim CompanyId As Integer = myContext.CommonData.GetCompanyIDFromCampus(CampusID)
                    Dim PIDUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@PIDUnitID", Params))
                    Dim str As String = ""
                    If PIDUnitID > 0 Then
                        str = "CampusID = " & CampusID & "  and ItemID = " & ItemID & " and PIDUnitID = " & PIDUnitID
                    Else
                        str = "CampusID = " & CampusID & "  and ItemID = " & ItemID
                    End If

                    Model = New clsViewModel(vwState, myContext)
                    Dim objProc As New clsMVProcQtyDep(myContext)
                    Dim dt As DataTable = objProc.ItemStockBalance(CompanyId, Dated, str, "IT", True)
                    dt = myContext.Tables.SelectTable(dt, {"VendorID", "CustomerID", "DepName", "SpStock", "StockStage", "TaxType", "WoInfo", "VarNum", "QtyUR", "QtyR", "QtyQI", "QtyTransit", "QtyTot", "UnitName"})
                    Model.MainGrid.BindGridData(dt.DataSet, 0)
                    Model.MainGrid.QuickConf("", True, "2-1-1.2-1-1-1-.8-.8-.8-1.2-.8-1.2")
                Case "receipt"
                    Dim ItemID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@ItemID", Params))
                    Dim CampusID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@CampusID", Params))
                    Dim Dated As Date = myContext.SQL.ParamValue("@Date", Params)
                    Dim PIDUnitID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@PIDUnitID", Params))
                    Dim str As String = ""
                    If PIDUnitID > 0 Then
                        str = " where VouchDate < = '" & Format(Dated, "yyyy-MMM-dd") & "' and CampusID = " & CampusID & " and ItemID = " & ItemID & " and VouchTypeCode = 'GR' and PIDUnitID = " & PIDUnitID
                    Else
                        str = " where VouchDate < = '" & Format(Dated, "yyyy-MMM-dd") & "' and CampusID = " & CampusID & " and ItemID = " & ItemID & " and VouchTypeCode = 'GR'"
                    End If

                    If myContext.SQL.Exists(Params, "@VendorIDCSV") Then str = str & " and " & myContext.SQL.PopulateSQLParams("VendorID in (@VendorIDCSV)", Params)
                    If myContext.SQL.Exists(Params, "@CustomerIDCSV") Then str = str & " and " & myContext.SQL.PopulateSQLParams("CustomerID in (@CustomerIDCSV)", Params)

                    Dim Sql As String = "Select VouchTypeCode, VouchNum, VouchDate, WOInfo, VarNum, BasicRate, AmountTot, AmountWV from invListMatVouchItem()" & str
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, Sql)

                    Model = New clsViewModel(vwState, myContext)
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "1-1-1-1-1-1-1-1")
                Case "odnote"
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ODNoteID""/>", False, , "<FILTER KEY=""Dated""><VALUE VALUE1=""" & Format(DateAdd(DateInterval.Month, -1, Now), "dd-MMM-yyyy") & """ OPERTYPE=""gt""/><VALUE  OPERTYPE=""isnull"" /></FILTER>")
                Case "odnotitemspare"
                    Dim IDField As String = myUtils.cStrTN(myContext.SQL.ParamValue("@idfield", Params))
                    Dim ODNoteId As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@odnoteid", Params))
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""" & IDField & """/>", True, , "<MODROW><SQLWHERE2> ODnoteID = " & ODNoteId & " </SQLWHERE2></MODROW>")
                Case "copyodnote"
                    Dim Str1 As String = myContext.SQL.PopulateSQLParams("ODNoteID Not In (@ODNoteID) And ChallanType = 'SCSP'", Params)
                    Model = myContext.Provider.GenerateSelectionModel(vwState, "<SYS ID=""ODNoteID""/>", False, , "<MODROW><SQLWHERE2>" & Str1 & "</SQLWHERE2></MODROW>")
                Case "copyodnotespare"
                    Dim Str1 As String = myContext.SQL.PopulateSQLParams("ODNoteID =  @ODNoteID", Params)
                    Dim dic As New clsCollecString(Of String)
                    dic.Add("ODNoteSpare", "select ODNoteSpareID, ODNoteID, SOSpareID, PIDUnitID, PriceSlabID,RT, hsn_sc,nature,  ppSubType,  ValuationClass, ClassType, TransType,IdenMarks,ItemSuffix, ItemUnitID, ItemType, ItemDescrip, QtyRate, BasicRate, AmountTot, AmountWV from ODNoteSpare  where " & Str1)
                    Dim ds As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

                    Model = New clsViewModel(vwState, myContext)
                    Model.Mode = EnumViewMode.acSelectM : Model.MultiSelect = True
                    Model.MainGrid.BindGridData(ds, 0)
                    Model.MainGrid.QuickConf("", True, "3-1-1-1-1")
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
        Dim oRet As clsProcOutput = myContext.SQL.ValidateSQLParams(Params)
        If oRet.Success Then
            Select Case dataKey.Trim.ToLower
                Case "odnotepack"
                    Dim IDField As String = myUtils.cStrTN(myContext.SQL.ParamValue("@idfield", Params))
                    Dim IDValue As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@idvalue", Params))

                    Dim sql As String = "select * from ODNoteItemPack  where " & IDField & " = " & myUtils.cValTN(IDValue) & ""
                    oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
                Case "spares"
                    Dim Str1 As String = myContext.SQL.PopulateSQLParams("SOSpareID In (@sospareidcsv)", Params)
                    Dim Str2 As String = myContext.SQL.PopulateSQLParams("SOServiceID In (@soserviceidcsv)", Params)
                    Dim dic As New clsCollecString(Of String)
                    dic.Add("spares", "select * from slsListSoSpares()  where " & Str1)
                    dic.Add("service", "select * from slsListSoService()  where " & Str2)
                    oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
                Case "sospareitem"
                    Dim sql As String = myContext.SQL.PopulateSQLParams("Select SoSpareItemID, SoSpareID, Qty from SoSpareItem where SOSpareID In (@SospareIdCSV) and DispatchID = @DispatchID", Params)
                    oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
                Case "copyodnoteitem"
                    Dim Str1 As String = myContext.SQL.PopulateSQLParams("ODNoteID =  @ODNoteID and ODNoteSpareID in (@ODNoteSpareIDCSV)", Params)
                    Dim dic As New clsCollecString(Of String)
                    dic.Add("ODNoteItem", "select ODNoteItemID, ODNoteID, ODNoteSpareID, ItemID, PriceSlabID, PIDUnitID, VarNum,RT, hsn_sc,nature, sply_ty, MvtCode, MvtType, VendorID, SOSpareID, QtySOSpare, CustomerID, CampusID, MatMvtReasonID, AmountTrWv, ItemUnitIDEntry, QtyTypeSrc, QtyTypeDes, QtySKU1, QtySKU2, StockStage2, TaxType2, ppSubType, ClassType, ValuationClass, WOInfo,IdenMarks,ItemSuffix, ItemCode, ItemName, UnitName, SpStock, StockStage, QtyEntry, QtyRate, BasicRate, AmountTot, AmountWV, Remark from trplistodnoteitem()  where " & Str1)
                    oRet.Data = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
            End Select
        End If
        Return oRet
    End Function
End Class
