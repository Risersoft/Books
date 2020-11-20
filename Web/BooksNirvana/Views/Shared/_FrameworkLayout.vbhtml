@imports risersoft.shared.web.Extensions
@imports risersoft.shared.web
@imports risersoft.shared.portable
@imports risersoft.shared.cloud.common
@imports risersoft.shared.cloud
@imports risersoft.shared.agent

@Code
    Dim _user As RSUser = Nothing
    If (Not Me.User Is Nothing) AndAlso (Not Me.User.Identity Is Nothing) Then
        _user = OwinHelper.GetRSUser(Me.User.Identity)

    End If
    Dim portal As String = risersoft.shared.GlobalCore.GetConfigSetting("portal")
    Dim ctx = CType(ViewContext.Controller, IHostedController).myWebController
End Code
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewData("Title")</title>
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryui")
    @Scripts.Render("~/bundles/bootstrap")
    <link href="~/Scripts/jquery-ui/jquery-ui.css" rel="stylesheet" />
    @RenderSection("scripts", required:=False)
    @Styles.Render("~/Content/css")
    <link href="~/Content/font-awesome.css" rel="stylesheet" />
    @Scripts.Render("~/bundles/angular")
    @Scripts.Render("~/bundles/ig")
    @Scripts.Render("~/scripts/rsweb.js")
    @*@Styles.Render("~/Content/themes/base/css")*@
    @Styles.Render("~/Content/themes/ig/css")
    <script src="/scripts/handlebars-v4.0.12.js"></script>

    @Html.RenderScripts(True, True)
    <style>
        div.spinnerTarget.backdrop {
            width: 100%;
            min-height: 1500px;
            position: absolute;
            background-color: #f7f7f7;
            opacity: 0.8;
            z-index: 99999;
        }
    </style>


</head>
<body ng-app="rsApp">
    @*<div class="top-line">&nbsp;</div>*@
    <div class="spinnerTarget" us-spinner="{top:'70%', radius:30, width:8, length: 16}" spinner-key="spinner-1" spinner-theme="smallBlue"></div>

    @If Not _user Is Nothing Then@<div Class="top-header-navigation" style="height: 27px;">
            <div class="container">

                <div class="btn-group margin-rgt10 navbar navbar-expand-md" style="float:right;margin-right: 22px;">
                    <a class="fa fa-user active" data-toggle="dropdown" style="text-decoration:none;margin-top: 5px;" href="#" aria-expanded="false">
                        <span class="caret"></span>
                    </a>
                    <ul class="dropdown-menu dropdown-men" style="left:-115px;">
                        <li> <a class="dropdown-item" href="@portal/account/change">Manage</a></li>
                        <li> <a class="dropdown-item" href="/account/Logout">Logout</a></li>
                    </ul>
                </div>

                <div class="btn-group margin-rgt10 navbar navbar-expand-md" style="float:right;margin-right: 22px;">
                    <a class="" style="text-decoration:none" href="#" aria-expanded="false">
                        Welcome @_user.FullName
                    </a>
                </div>
            </div>
        </div>
    Else
        @<ul Class="header-item">
            <li>
                <a href="/Account/Login">Login</a>
            </li>
            <li>
                <a href="@portal/User/signup">Signup</a>
            </li>
        </ul>
    End If





    <div style="background:#ffffff">
        <div class="container">
            <div Class="row">
                <div Class="col-md-4" style="margin-top: 15px;">
                    <div class="logo-header">
                        <div class="pull-left mobo-widthtab"><a href="/" class="link-underline"><h2 class="uni-logo"><img src="~/Content/images/Nirvana.png" class="ninja-logo" />BooksNirvana</h2></a></div>

                    </div>
                </div>
                <div Class="col-md-7" style="margin-top: 15px;">
                    <div class="pull-right tagline mobo-widthtab" style="float:right">
                        <h2>Accounting Simplified</h2>
                    </div>
                </div>
            </div>
        </div>
    </div>

    @Html.Menu(ctx)
    <div class="clsmargn">
        @RenderBody()
        <hr />
        <div id="dialogFilter" title="Filter">
        </div>
        <footer>
            <p>&copy; @DateTime.Now.Year - @ctx.Controller.CalcPublisher</p>
        </footer>
    </div>
    <ul id="id_context2" style="display: none;">
        <li data-command="action1">Fetching Data ...</li>
    </ul>
    @RenderSection("BotScripts", required:=False)


    <script type="text/javascript">
        $(document).ready(function () {

            $('.navbar-nav').removeClass('nav');
            $(window).bind('scroll', function () {
                if ($(window).scrollTop() > 31) {

                    $('.navbar-fixed-top').addClass('navbar-fixed-top-change');
                    $('.top-line-move').addClass('fixed');
                }
                else {

                    $('.navbar-fixed-top').removeClass('navbar-fixed-top-change');
                    $('.top-line-move').removeClass('fixed');
                }
            });
            $(window).bind('scroll', function () {
                if ($(window).scrollTop() > 50) {

                    $('.navbar-fixed-top').addClass('navbar-fixed-top-change');
                    $('.top-line-move').addClass('fixed');
                }
                else {

                    $('.navbar-fixed-top').removeClass('navbar-fixed-top-change');
                    $('.top-line-move').removeClass('fixed');
                }
            });

        });
        function tablehtml(table) {

            var thtml = "<tr>";
            for (var k in table[0]) {
                thtml += "<th>" + k + "</th>";
            }
            thtml += "</tr>";
            $.each(table, function (index, value) {
                var TableRow = "<tr>";
                $.each(value, function (key, val) {
                    TableRow += "<td>" + val + "</td>";
                });
                TableRow += "</tr>";
                thtml += TableRow;;
            });
            return thtml;
        }

    </script>
</body>
</html>
