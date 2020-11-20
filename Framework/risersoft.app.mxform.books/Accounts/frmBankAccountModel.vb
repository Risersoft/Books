Imports risersoft.shared
Imports risersoft.app.mxent
Imports System.Runtime.Serialization

<DataContract>
Public Class frmBankAccountModel
    Inherits clsFormDataModel
    Protected Overrides Sub InitViews()
    End Sub
    Public Sub New(context As IProviderContext)
        MyBase.New(context)
        Me.InitViews()
        Me.InitForm()
    End Sub

    Private Sub InitForm()
        Dim sql As String

        sql = "Select CompanyID, CompName  from Company  Order by CompName"
        Me.AddLookupField("CompanyID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Company").TableName)

        sql = "Select VendorID, PartyName from Vendor Inner join Party on Vendor.PartyID = Party.PartyID where VendorType = 'B'"
        Me.AddLookupField("VendorID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "Vendor").TableName)

        sql = "Select GLAccountID, AccName from GLAccount Order By AccName"
        Me.AddLookupField("GlAccountID", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "GlAccount").TableName)

        sql = myFuncsBase.CodeWordSQL("Bank", "AccountType", "")
        Me.AddLookupField("AccountType", myUtils.AddTable(Me.dsCombo, myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql), "AccountType").TableName)
    End Sub

    Public Overrides Function PrepForm(oView As clsViewModel, ByVal prepMode As EnumfrmMode, ByVal prepIDX As String, Optional ByVal strXML As String = "") As Boolean
        Dim sql As String

        Me.FormPrepared = False
        If prepMode = EnumfrmMode.acAddM Then prepIDX = 0

        sql = "select * from BankAccount where BankAccountId =" & prepIDX
        Me.InitData(sql, "", oView, prepMode, prepIDX, strXML)

        Me.FormPrepared = True
        Return Me.FormPrepared
    End Function

    Public Overrides Function Validate() As Boolean
        Me.InitError()

        If Me.myRow("AccountNum").ToString.Trim.Length = 0 Then Me.AddError("AccountNum", "Enter Account No")
        If Me.SelectedRow("CompanyID") Is Nothing Then Me.AddError("CompanyID", "Select Company Name")
        If Me.SelectedRow("AccountType") Is Nothing Then Me.AddError("AccountType", "Select Account Type")
        If Me.SelectedRow("VendorID") Is Nothing Then Me.AddError("VendorID", "Select Party Name")
        If Me.SelectedRow("GlAccountID") Is Nothing Then Me.AddError("GlAccountID", "Select Account Name")
        Return Me.CanSave
    End Function

    Public Overrides Function VSave() As Boolean
        VSave = False
        If Me.Validate Then
            Dim BankAccDescrip As String = myRow("AccountNum").ToString
            Try
                myRow("AccountName") = myUtils.cStrTN(Me.SelectedRow("VendorID")("PartyName")) & " (" & myRow("AccountNum") & ")"
                myContext.Provider.dbConn.BeginTransaction(myContext, Me.Name, Me.frmMode.ToString, "BankAccountId", frmIDX)
                myContext.Provider.objSQLHelper.SaveResults(myRow.Row.Table, "select * from BankAccount where BankAccountId = " & frmIDX)
                frmMode = EnumfrmMode.acEditM
                frmIDX = myRow("BankAccountId")
                BankAccDescrip = " No: " & myRow("AccountNum") & ", Name: " & myRow("AccountName").ToString
                myContext.Provider.dbConn.CommitTransaction(BankAccDescrip, frmIDX)
                VSave = True
            Catch e As Exception
                myContext.Provider.dbConn.RollBackTransaction(BankAccDescrip, e.Message)
                Me.AddException("", e)
            End Try
        End If
    End Function
End Class
