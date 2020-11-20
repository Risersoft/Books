Imports risersoft.shared

Public Class myExcise
    Public Shared Function FreezeChallan(f As risersoft.shared.IForm) As Boolean
        Dim fr As Boolean = False
        If f.frmMode = EnumfrmMode.acEditM AndAlso (Not f.Controller.Police.IsAllowedUser("KANOHAR\Excise", "")) Then
            'If myUtils.cStrTN(myRow("challannum")).Trim.Length > 0 Then
            If Not myUtils.NullNot(f.myRow("removaldate")) Then
                fr = True
            End If
        End If
        Return fr
    End Function
End Class
