Imports risersoft.app.shared
Imports risersoft.shared.win
Imports risersoft.app.accounts
Imports risersoft.shared
Imports risersoft.app2.shared
Imports risersoft.shared.DotnetFx

Public Class Utils
    Public Shared Sub Main(ByVal args() As String)
        myApp = New clsRSWinCloudApp(New clsExtendAppFICO)
        myWinApp.CheckInitConsole(args)
        Dim fMain As frmMax = AppStarter.StartWinFormApp(args)
        If Not fMain Is Nothing Then
            Application.Run(fMain)
        End If
    End Sub
End Class
