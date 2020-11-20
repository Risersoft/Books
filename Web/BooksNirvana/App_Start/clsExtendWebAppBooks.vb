Imports System.Threading
Imports System.Threading.Tasks
Imports risersoft.app
Imports risersoft.app.mxform
Imports risersoft.app.mxform.books
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.agent
Imports risersoft.shared.cloud
Imports risersoft.shared.dotnetfx
Imports risersoft.shared.web
Imports risersoft.shared.web.mvc

Public Class clsExtendWebAppBooks
    Inherits clsExtendWebAppBase

    Protected Friend mQueueProvider As IQueueProvider, cts As CancellationTokenSource

    Public Overrides Function NewDBName() As String
        Return "mxentdb"
    End Function


    Public Overrides Function ProgramCode() As String
        Return "mxbooks"
    End Function

    Public Overrides Function ProgramName() As String
        Return "BooksNirvana"

    End Function

    Public Overrides Function StartupAppCode() As String
        Return "nbks"
    End Function

    Public Overrides Function dicFormModelTypes() As clsCollecString(Of Type)
        If dicFormModel Is Nothing Then
            dicFormModel = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicFormModel, GetType(frmCompanyModel).Assembly, GetType(clsFormDataModel))
            Me.AddTypeAssembly(dicFormModel, GetType(frmInvoiceSaleModel).Assembly, GetType(clsFormDataModel))
        End If
        Return dicFormModel
    End Function

    Public Overrides Function dicReportProviderTypes(myContext As clsAppController) As clsCollecString(Of Type)
        If dicReportModelProvider Is Nothing Then
            dicReportModelProvider = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicReportModelProvider, GetType(reports.erp.invReportDataProvider).Assembly, GetType(clsReportDataProviderBase))
        End If
        Return dicReportModelProvider

    End Function

    Public Overrides Function HelpSite() As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function dicTaskProviderTypes() As clsCollecString(Of Type)
        If dicTaskProvider Is Nothing Then
            dicTaskProvider = New clsCollecString(Of Type)
            Me.AddTypeAssembly(dicTaskProvider, GetType(EVTaskProvider).Assembly, GetType(clsTaskProviderBase))
            Me.AddTypeAssembly(dicTaskProvider, GetType(GSTR1TaskProvider).Assembly, GetType(clsTaskProviderBase))
        End If
        Return dicTaskProvider
    End Function

    Public Overrides Function OnAppInit(oApp As clsCoreApp) As Boolean
        Dim q = New clsLocalQueueProvider(oApp.Controller)
        mQueueProvider = q
        cts = New CancellationTokenSource
        Dim ct = cts.Token
        q.ConfigureListener(ct, Async Function(dic As Dictionary(Of String, String)) As Task(Of clsProcOutput)
                                    Return Await Task.Run(Async Function()
                                                              Await Task.Delay(2000)
                                                              Dim oRet = AgentAuthProvider.GenerateAccountInfo(oApp, dic("basehost"), dic("tenantId"))
                                                              Dim scheduler = New clsTaskScheduler(oApp, False)
                                                              Dim oRet2 = Await scheduler.ExecuteServerAccApiTask(oRet.Result.Account, oRet.Result.Env, dic("basehost"), dic("apitaskId"))
                                                              Return oRet2
                                                          End Function)
                                End Function)
        Return MyBase.OnAppInit(oApp)
    End Function
    Public Overrides Function OnAppClose(oApp As clsCoreApp) As Boolean
        cts.Cancel()
        Return MyBase.OnAppClose(oApp)
    End Function

    Public Overrides Function QueueProvider(context As IProviderContext) As IQueueProvider
        If (mQueueProvider Is Nothing) Then mQueueProvider = New clsLocalQueueProvider(context)
        Return mQueueProvider
    End Function
End Class
