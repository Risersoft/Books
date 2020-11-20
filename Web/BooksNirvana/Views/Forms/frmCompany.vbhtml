@imports AuthorizationFramework.Proxies
@imports risersoft.shared.portable
@imports risersoft.app.mxform
@imports risersoft.shared.portable.Proxies
@imports risersoft.shared.web.Extensions
@imports Newtonsoft.Json
@ModelType frmCompanyModel

@Code
    ViewData("Title") = "Company"
    Layout = "~/Views/Shared/_FrameworkLayout.vbhtml"
    Dim modelJson = JsonConvert.SerializeObject(Model, Formatting.Indented,
                           New JsonSerializerSettings With {.StringEscapeHandling = StringEscapeHandling.EscapeHtml})


End Code

<link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
<script src="~/Scripts/jquery-ui-1.11.4.min.js"></script>
<link href="~/Content/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="~/Content/font-awesome.css" rel="stylesheet" />
<script src="~/Scripts/bootstrap-datepicker.min.js"></script>
<div class="container cbackground">
    
</div>


@section botscripts





    
    
end section

