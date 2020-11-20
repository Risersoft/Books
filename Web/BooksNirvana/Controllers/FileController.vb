Imports System.Web.Mvc
Imports risersoft.shared
Imports risersoft.shared.web
Imports risersoft.shared.web.mvc
Imports risersoft.shared.web.Providers
Imports risersoft.API.GSTN.GSTR1
Imports risersoft.app.mxform.gst
Imports risersoft.app.mxent
Imports risersoft.API.GSTN
Imports Newtonsoft.Json
Imports GSTN.API.Library.Models
Imports risersoft.API.GSTN.GSTR2
Imports risersoft.app.mxform
Imports System.Threading.Tasks

Namespace Controllers

    Public Class FileController
        Inherits clsMvcControllerBase
        <HttpGet> <Authorize> <HostActionFilter>
        Async Function Sign(ReturnKey As String, GstRegID As Integer, PostPeriodID As Integer) As Task(Of ActionResult)
            Try
                If Await Me.myWebController.CheckInitModel(Me.myWebController.AppInfo, False) Then
                    Dim nr As DataRow
                    Select Case ReturnKey.Trim.ToUpper
                        Case "GSTR1"
                            Dim oProc As New clsGSTNReturnGSTR1(Me.myWebController)
                            nr = oProc.GenerateSignData(GstRegID, PostPeriodID, False)
                        Case "GSTR2"
                            Dim oProc As New clsGSTNReturnGSTR2(Me.myWebController)
                            nr = oProc.GenerateSignData(GstRegID, PostPeriodID, False)
                    End Select

                    If (Not nr Is Nothing) Then
                        Me.ViewBag("Data") = nr("hashedpayload")
                        Return View("eMudhra")
                    End If
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Function

        Async Function SuccessResponse() As Task(Of ActionResult)
            Try
                If Await Me.myWebController.CheckInitModel(Me.myWebController.AppInfo, False) Then
                    Dim sql As String = "select * from gstnsign where transactionid='" & "'"
                    Dim dt1 As DataTable = Me.myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                    Dim nr As DataRow = dt1.Rows(0)

                    If (Not nr Is Nothing) Then
                        Select Case myUtils.cStrTN(nr("returnkey")).Trim.ToUpper
                            Case "GSTR1"
                                Dim oProc As New clsGSTNReturnGSTR1(Me.myWebController)
                                oProc.FileSignedReturn(nr)
                            Case "GSTR2"
                                Dim oProc As New clsGSTNReturnGSTR2(Me.myWebController)
                                oProc.FileSignedReturn(nr)
                        End Select

                        Me.myWebController.DataProvider.objSQLHelper.SaveResults(nr.Table, "select * from gstnsign where 0=1")

                    End If
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try


        End Function


    End Class
End Namespace