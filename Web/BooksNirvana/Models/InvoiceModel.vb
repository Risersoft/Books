Imports System.ComponentModel.DataAnnotations
Public Class InvoiceModel

    <Display(Name:="Id")>
    Public Property Id() As Integer
    Public Property Receiverno As String
    Public Property invoicedate() As String
    Public Property supplytypestate As String
    Public Property totaltaxvalue As String
    Public Property totalinvoicevalue As String
    Public Property uploaddate() As String
    Public Property reversecharge As String
    Public Property pos As String
    Public Property Receivernamecheck As String
    Public Property Parties As Party
    Public Property Biz As BizEntity
    Public Property Items As New List(Of InvoiceItem)

End Class

Public Class BizEntity
    Public Property BizEntityId As Integer
    Public Property DispName As String
End Class

Public Class Party
    Public Property PartyId As Integer
    Public Property PartyName As String
End Class

Public Class InvoiceItem
    Public Property rt As String
    Public Property taxval As String
    Public Property iamt As String
    Public Property camt As String
    Public Property samt As String
    Public Property csamt As String
    Public Property description As String
    Public Property ty As String
    Public Property uqc As String
    Public Property qty As String
    Public Property basicrate As String
End Class