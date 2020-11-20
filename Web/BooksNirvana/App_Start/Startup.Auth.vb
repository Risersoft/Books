Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Infrastructure
Imports Owin
Imports System.Collections.Concurrent
Imports System.Linq
Imports System.Security.Claims
Imports System.Security.Principal
Imports System.Threading.Tasks

Imports Microsoft.Owin.Infrastructure
Imports System.Collections.Generic
Imports risersoft.shared.web
Imports Microsoft.Owin.Security.DataProtection
Imports risersoft.shared.web.mvc
Imports risersoft.shared.web.common
Imports Microsoft.Owin.Security.OAuth
Imports risersoft.shared

Partial Public Class Startup

    Public Sub ConfigureAuth(app As IAppBuilder)
        ' Enable Application Sign In Cookie
        app.UseRSAuthClient()
        app.UseRSAuthBearerToken


    End Sub

End Class
