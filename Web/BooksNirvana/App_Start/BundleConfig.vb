Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.js",
                  "~/Scripts/respond.js"))

        bundles.Add(New ScriptBundle("~/bundles/ig").Include(
                  "~/Scripts/infragistics.core.js",
                  "~/Scripts/infragistics.lob.js",
                  "~/Scripts/infragistics.dv.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/site.css",
              "~/Content/rsweb.css"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js", "~/Scripts/jquery.ui-contextmenu.js"))

        bundles.Add(New StyleBundle("~/Content/themes/base/css").Include(
              "~/Content/themes/base/core.css",
              "~/Content/themes/base/resizable.css",
              "~/Content/themes/base/selectable.css",
              "~/Content/themes/base/accordion.css",
              "~/Content/themes/base/autocomplete.css",
              "~/Content/themes/base/button.css",
              "~/Content/themes/base/dialog.css",
              "~/Content/themes/base/slider.css",
              "~/Content/themes/base/tabs.css",
              "~/Content/themes/base/datepicker.css",
              "~/Content/themes/base/progressbar.css",
              "~/Content/themes/base/theme.css"))

        bundles.Add(New StyleBundle("~/Content/themes/ig/css").Include(
             "~/Content/themes/infragistics/infragistics.theme.css",
             "~/Content/themes/infragistics/infragistics.structure.css"))

        bundles.Add(New ScriptBundle("~/bundles/angular").Include(
          "~/Scripts/angular.js",
          "~/Scripts/angular-sanitize.js",
          "~/Scripts/angular-datepicker.js",
          "~/Scripts/angular-spinner.min.js",
          "~/Scripts/ui-bootstrap-tpls-0.12.1.min.js",
          "~/Scripts/rsang.js"))

    End Sub
End Module

