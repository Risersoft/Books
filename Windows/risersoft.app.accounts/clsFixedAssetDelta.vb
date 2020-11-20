Imports risersoft.app.shared
Imports risersoft.app.mxent
Public Class clsFixedAssetDelta
    Dim OMasterData As New clsMasterDataFICO

    Public Function GetFixedAssetTransDelta(ByVal r1 As DataRow, ByVal dtDelta As DataTable)
        Dim rr1(), r2, r3 As DataRow

        rr1 = dtDelta.Select()
        If rr1.Length > 0 Then
            myUtils.DeleteRows(dtDelta.Select)
        Else
            r2 = myUtils.CopyOneRow(r1, dtDelta)
            If Not myUtils.NullNot(r2) Then
                r2("DeltaType") = myUtils.cStrTN(r1("TransType"))
                If myUtils.cStrTN(r1("TransType")) = "WU" Then
                    r2("SumAmount") = myUtils.cValTN(r1("Amount"))
                ElseIf myUtils.cStrTN(r1("TransType")) = "WO" Then
                    r2("SumAmount") = myUtils.cValTN(r1("Amount")) * (-1)
                ElseIf myUtils.cStrTN(r1("TransType")) = "TR" Then
                    r2("SumAmount") = myUtils.cValTN(r1("Amount")) * (-1)

                    r3 = myUtils.CopyOneRow(r1, dtDelta)
                    If Not myUtils.NullNot(r3) Then
                        r3("FixedAssetID") = r1("NewFixedAssetID")
                        r3("DeltaType") = myUtils.cStrTN(r1("TransType"))
                        r3("SumAmount") = myUtils.cValTN(r1("Amount"))
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Public Function GetFixedAssetOpeningValDelta(ByVal r1 As DataRow, ByVal dtDelta As DataTable)
        Dim rr1(), r2 As DataRow
        Dim PostPeriodID As Integer

        Dim CompanyID As Integer = myApp.myCommonData.GetCompanyIDFromCampus(r1("CampusID"))
        Dim r3 As DataRow = myApp.myCommonData.rCompany(CompanyID)

        If Not myUtils.NullNot(CompanyID) Then
            PostPeriodID = OMasterData.GetPostPeriodID(CompanyID, r3("FinStartDate"))
        End If

        rr1 = dtDelta.Select()
        If rr1.Length > 0 Then
            rr1(0)("Dated") = myUtils.cStrTN(r1("PurchDate"))
            rr1(0)("Amount") = myUtils.cValTN(r1("OpeningValue"))
            rr1(0)("SumAmount") = myUtils.cValTN(r1("OpeningValue"))
            rr1(0)("PostPeriodID") = PostPeriodID
        Else
            r2 = myUtils.CopyOneRow(r1, dtDelta)
            If Not myUtils.NullNot(r2) Then
                r2("DeltaType") = "OB"
                r2("Dated") = myUtils.cStrTN(r1("PurchDate"))
                r2("Amount") = myUtils.cValTN(r1("OpeningValue"))
                r2("SumAmount") = myUtils.cValTN(r1("OpeningValue"))
                r2("PostPeriodID") = PostPeriodID
            End If
        End If
        Return True
    End Function

    Public Function GetFixedAssetInvoiceDelta(ByVal dtInvoice As DataTable, ByVal dt As DataTable, ByVal PostPeriodID As Integer, ByVal InvoiceDate As Date, ByVal DeltaType As String)
        Dim r2 As DataRow

        For Each r1 As DataRow In dtInvoice.Select("FixedAssetID is Not NULL")
            Dim rr3() As DataRow = dt.Select("InvoiceItemID = " & r1("InvoiceItemID") & "")

            If rr3.Length > 0 Then
                If DeltaType = "IS" Then
                    rr3(0)("DeltaType") = "IS"
                    rr3(0)("SumAmount") = myUtils.cValTN(r1("AmountTot")) * (-1)
                ElseIf DeltaType = "IP" Then
                    rr3(0)("DeltaType") = "IP"
                    rr3(0)("SumAmount") = myUtils.cValTN(r1("AmountTot"))
                End If

                rr3(0)("Dated") = myUtils.cStrTN(InvoiceDate)
                rr3(0)("Amount") = myUtils.cValTN(r1("AmountTot"))
                rr3(0)("PostPeriodID") = PostPeriodID
            Else
                r2 = myUtils.CopyOneRow(r1, dt)
                If Not myUtils.NullNot(r2) Then
                    If DeltaType = "IS" Then
                        r2("DeltaType") = "IS"
                        r2("SumAmount") = myUtils.cValTN(r1("AmountTot")) * (-1)
                    ElseIf DeltaType = "IP" Then
                        r2("DeltaType") = "IP"
                        r2("SumAmount") = myUtils.cValTN(r1("AmountTot"))
                    End If

                    r2("Dated") = myUtils.cStrTN(InvoiceDate)
                    r2("Amount") = myUtils.cValTN(r1("AmountTot"))
                    r2("PostPeriodID") = PostPeriodID
                End If
            End If
        Next
        Return True
    End Function

    Public Function CheckNegativeAmount(ByVal Dt As DataTable)
        Dim Sql As String = "", dtDelta As DataTable, Resulst As Boolean = False
        Sql = "Select SumAmount from FixedAssetDelta "
        dtDelta = SqlHelper.ExecuteDataset(myApp.dbConn, CommandType.Text, Sql).Tables(0)


        For Each r2 As DataRow In Dt.Select
            For Each r1 As DataRow In dtDelta.Select("FixedAssetID = " & myUtils.cStrTN(r2("FixedAsesetID")) & " and Dated >= " & myUtils.cStrTN(r2("Dated")) & "  and Dated <= " & Now.Date & "")
                If myUtils.cValTN(r1("SumAmount")) - myUtils.cValTN(r2("Amount")) < 0 Then
                    MsgBox("Asset can't issue due to Negative Amount.", MsgBoxStyle.Information, myApp.Vars("AppName"))
                    Resulst = False
                Else
                    Resulst = True
                End If
            Next
        Next
        Return Resulst
    End Function
End Class
