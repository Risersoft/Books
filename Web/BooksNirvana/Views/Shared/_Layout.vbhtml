@imports risersoft.shared.web
@imports risersoft.shared.cloud.common
@imports risersoft.shared.cloud
@imports risersoft.shared.agent

@code
    Dim ctx = CType(ViewContext.Controller, IHostedController)
End Code
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - BooksNirvana</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/angular")
</head>
<body ng-app="rsApp">
    <div class="top-line">&nbsp;</div>
    <div style="background:#ffffff;height:90px">
        <div class="container">
            <div Class="row">
                <div Class="col-md-3" style="margin-top: 15px;">
                    <div class="logo-header">
                        <div class="pull-left mobo-widthtab"><a href="/" class="link-underline"><h2 class="uni-logo"><img src="~/Content/images/Nirvana.png" class="ninja-logo" />BooksNirvana</h2></a></div>
                    </div>
                </div>
                <div Class="col-md-8" style="margin-top: 15px;">
                    <div class="pull-right tagline mobo-widthtab" style="float:right">
                        <h2>Accounting Simplified</h2>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="navbar navbar-inverse navbar-default navbar-expand-md navbar-light">
        <div class="container">
            <div class="navbar-header">
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                @Html.ActionLink(ctx.Host.Brand, "Parent", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                <ul class="navbar-nav">
                    <li class="nav-item"><a class="nav-link" href="@Url.Action("BuyApp", "Home")">Home</a></li>
                    <li class="nav-item"><a class="nav-link" href="@Url.Action("TryApp", "Home")">Try</a></li>
                    <li class="nav-item"><a class="nav-link" href="@Url.Action("Explore", "Home")">Explore</a></li>
                    <li class="nav-item"><a class="nav-link" href="http://docs.booksnirvana.com/" target="_blank">Documentation</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div Class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - BooksNirvana</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @Scripts.Render("~/bundles/jqueryui")
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    @RenderSection("scripts", required:=False)

</body>
</html>
