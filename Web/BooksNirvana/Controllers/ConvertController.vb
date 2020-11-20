Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Reflection
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web.Hosting
Imports System.Web.Http
Imports risersoft.API.GSTN
Imports Newtonsoft.Json
Imports risersoft.app.dataporter
Imports risersoft.app.mxengg
Imports risersoft.app.mxform.eto
Imports risersoft.shared
Imports risersoft.shared.portable.Models.Nav
Imports risersoft.shared.web
Imports risersoft.shared.web.Controllers
Imports risersoft.shared.dotnetfx

Namespace Controllers
    <RoutePrefix("api/Convert")>
    <HostAuthentication("Basic")>
    <Authorize>
    Public Class ConvertController
        Inherits ApiControllerBase

        'GstController is basic authentication. Hence ServiceAuthProvider.GenerateCallerInfo will return early and set nothing

        Protected Friend Function FindType(ReturnKey As String) As Type
            Dim dic As clsCollecString(Of Type)
            dic = New clsCollecString(Of Type)
            dic.Add("gstr1", GetType(GSTR1.GSTR1Total))
            dic.Add("gstr2", GetType(GSTR2.GSTR2Total))
            dic.Add("gstr3", GetType(GSTR3.GSTR3Total))

            Dim t2 As Type = Nothing
            If dic.ContainsKey(ReturnKey) Then
                t2 = dic(ReturnKey)
            End If
            Return t2
        End Function
        Protected Friend Function FindProperty(t As Type, TableName As String) As PropertyInfo
            Dim p As PropertyInfo = Nothing
            If Not t Is Nothing Then
                For Each prop In t.GetProperties
                    If myUtils.IsInList(TableName, prop.Name) Then
                        p = prop
                        Exit For
                    End If
                Next
            End If
            Return p
        End Function
        ' POST: api/GST/Json2CSV
        <HttpPost> <ActionName("Json2CSV")>
        Public Function GenerateCSV(<FromUri> ReturnKey As String, <FromUri> TableName As String) As HttpResponseMessage
            Dim JsonString As String = Me.Request.Content.ReadAsStringAsync.Result

            Dim t As Type = Me.FindType(ReturnKey)
            Dim prop As PropertyInfo = Me.FindProperty(t, TableName)
            If (Not prop Is Nothing) AndAlso (prop.PropertyType.GetInterfaces.Contains(GetType(IList))) Then
                Dim model = JsonConvert.DeserializeObject(JsonString, t)
                Dim t2 As Type = prop.PropertyType
                Dim t3 = t2.GetGenericArguments(0)
                Dim objList = prop.GetValue(model)
                Dim oProc As New clsPOCOConverter(Me.myWebController)
                Dim dt As DataTable = oProc.GenerateTable(t3, objList)

                Dim oMap As New clsMap(Me.myWebController)
                oMap.TargetSchema.LoadDataSet(dt.DataSet)
                Dim oProc2 As New clsTCAsciiDelimited(myWebController)
                oMap.TargetConnection = oProc2
                oProc2.Init(oMap)
                oProc2.Properties("Header") = True
                oProc2.Properties("FieldDelimitStyle") = "partial"
                Dim str1 As String = oProc2.GenerateString(oMap.TargetSchema.RecordTypes(dt.TableName), dt)
                Return New HttpResponseMessage() With {
            .Content = New StringContent(str1, System.Text.Encoding.UTF8, "text/plain")}

            End If
            Return New HttpResponseMessage() With {
             .Content = New StringContent("Unspecified Error", System.Text.Encoding.UTF8, "text/plain")}

        End Function
        ' POST: api/GST/CSV2Json
        <HttpPost> <ActionName("CSV2Json")>
        Public Function GenerateJson(<FromUri> ReturnKey As String, <FromUri> TableName As String) As HttpResponseMessage
            Dim CSVString As String = Me.Request.Content.ReadAsStringAsync.Result

            Dim t As Type = Me.FindType(ReturnKey)
            Dim prop As PropertyInfo = Me.FindProperty(t, TableName)
            If (Not prop Is Nothing) AndAlso (prop.PropertyType.GetInterfaces.Contains(GetType(IList))) Then
                Dim t2 As Type = prop.PropertyType
                Dim t3 = t2.GetGenericArguments(0)

                Dim oProc As New clsPOCOConverter(Me.myWebController)
                Dim dt As DataTable = oProc.GenerateBlankTable(t3)

                Dim oProc2 As New clsSCASCIIDelimited(myWebController)
                Dim oMap As New clsMap(Me.myWebController)
                oMap.SourceConnection = oProc2
                oMap.SourceSchema.LoadDataSet(dt.DataSet)
                oProc2.Init(oMap)
                oProc2.Properties("Header") = True
                oProc2.Properties("FieldDelimitStyle") = "partial"
                Dim ds As DataSet = oProc2.ReadDataSet(CSVString, oMap.SourceSchema)

                Dim model = Activator.CreateInstance(t)
                Dim lst = oProc.GenerateObjectList(t3, ds.Tables(0))
                prop.SetValue(model, lst)
                Dim str1 As String = JsonConvert.SerializeObject(model, Formatting.None,
                            New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
                Return New HttpResponseMessage() With {
            .Content = New StringContent(str1, System.Text.Encoding.UTF8, "application/json")}

            End If
            Return New HttpResponseMessage() With {
            .Content = New StringContent("Unspecified Error", System.Text.Encoding.UTF8, "text/plain")}


        End Function
    End Class
End Namespace