Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmImprestAdjustModel
    Inherits clsFormDataModel
    Dim PPFinal As Boolean = False, myViewAE As clsViewModel

    Protected Overrides Sub InitViews()
        myView = Me.GetViewModel("Exp")
        myViewAE = Me.GetViewModel("AE")
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

    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String, objPaymentTravel As New clsPaymentTravel(myContext)
        Dim objTourVouchProc As New clsTourVouchProc(myContext)

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0
        sql = "Select * from Payment Where PaymentID = " & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        myRow("EmployeeID") = 1266

        sql = "Select Acc.PaymentID,DocType,Acc.Dated,FullName,PartyName,AmountTotPay from accListPayment() as Acc Inner join hrpListAllEmp() as Emp on acc.ImprestEmployeeID = Emp.EmployeeID where DocType in ( 'PV','PPU','PCO','PT') and PaymentMode = 'IM' and Acc.PaymentID Not in (Select isnull(PaymentID,0) from TourVouchItem) and Emp.EmployeeID in (Select EmployeeID from TourVouch where IsAdvance = 1 and BalanceAmount > 0 and EmployeeID not in (99,21) and EmployeeID in (" & myUtils.cValTN(myRow("EmployeeID")) & ")) Order by FullName"
        myView.MainGrid.MainConf("FORMATXML") = "<COL KEY=""FullName"" CAPTION=""Employee Name""/>"
        myView.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1")
        myView.MainGrid.myDS.Tables(0).TableName = "TourVouchAdv"


        myViewAE.MainGrid.MainConf("formatxml") = "<COL KEY=""Balance"" FORMAT=""0.00""/>"
        sql = "Select TourVouchItemID,TourVouchID,'Adjust Voucher' as Remark, '' as PaymentID, AdvanceVouchID, '' as VouchNum, Dated, 0.00 as PreBalance, Amount, 0.00 as Balance from slsListTourVouchItem() Where PaymentID  = " & 0 & ""
        myViewAE.MainGrid.QuickConf(sql, True, "1-1-1-1-1-1-1", True)
        Dim str1 As String = "<BAND IDFIELD=""TourVouchItemID"" TABLE=""TourVouchItem"" INDEX=""0""><COL KEY=""TourVouchItemID,PaymentID, AdvanceVouchID, Amount""/></BAND>"
        myViewAE.MainGrid.PrepEdit(str1, EnumEditType.acCommandAndEdit)

        Me.ModelParams.Add(New clsSQLParam("@Status", myFuncs.CheckStatus(myContext, "PaymentID", myUtils.cValTN(myRow("PaymentID"))), GetType(Boolean), False))
        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Not myFuncs.CheckColumnAmount(myViewAE.MainGrid.myDS.Tables(0), {"Amount", "Balance"}) Then Me.AddError("", "Amount can Not be Less then Zero")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False

        If Me.Validate Then
            If Me.CanSave Then
                Try
                    For Each r1 As DataRow In myViewAE.MainGrid.myDS.Tables(0).Select("PaymentID is Not NULL")
                        myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "PaymentID", frmIDX)

                        Dim objTourVouchProc As New clsTourVouchProc(myContext)
                        Dim dt As DataTable = myViewAE.MainGrid.myDS.Tables(0).Clone
                        Dim r2 As DataRow = myUtils.CopyOneRow(r1, dt)
                        myContext.Provider.objSQLHelper.SaveResults(dt, "Select TourVouchItemID, PaymentID, AdvanceVouchID, Amount, Remark from TourVouchItem")

                        objTourVouchProc.LoadVouch(myUtils.cValTN(r1("AdvanceVouchID")))
                        Dim Oret As clsProcOutput = objTourVouchProc.HandleWorkflowState(r1)
                        If Oret.Success Then
                            frmMode = EnumfrmMode.acEditM
                            myContext.Provider.dbConn.CommitTransaction("Adjust Voucher", frmIDX)
                            VSave = True
                        Else
                            myContext.Provider.dbConn.RollBackTransaction("Adjust Voucher", Oret.Message)
                            Me.AddError("", Oret.Message)
                        End If
                    Next
                Catch e As Exception
                    myContext.Provider.dbConn.RollBackTransaction("Adjust Voucher", e.Message)
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
            Select Case SelectionKey.Trim.ToLower
                Case "ae"
                    Dim Sql As String = myContext.SQL.PopulateSQLParams("(isnull(PaymentID,0) > 0 or isnull(TransferTourVouchID,0) > 0 or isopening = 1) and IsNull(BalanceAmount,0) > 0 and TourVouchID Not in (@tourvouchidcsv) and IsProcessed = 1 and EmployeeID = @EmployeeID", Params)
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
End Class