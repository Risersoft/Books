Imports System.Web.Http
Imports Microsoft.Practices.Unity
Imports Unity
Imports Unity.WebApi

Public Class UnityConfig
    Public Shared Sub RegisterComponents(config As HttpConfiguration)

        Dim container = New UnityContainer()
        Helper.UnityContainer = container
        ' register all your components with the container here
        ' it Is Not necessary to register your controllers

        ' e.g. container.RegisterType<ITestService, TestService>();
        RegisterMockObjects(container)
        'RegisterObjects(container)
        config.DependencyResolver = New UnityDependencyResolver(container)

    End Sub

    Private Shared Sub RegisterObjects(container As UnityContainer)

    End Sub

    Private Shared Sub RegisterMockObjects(container As UnityContainer)
        'container.RegisterType(Of IEWBRepository, EWBRepository)
    End Sub
End Class
Public Class Helper

    Public Shared Property UnityContainer As UnityContainer
End Class