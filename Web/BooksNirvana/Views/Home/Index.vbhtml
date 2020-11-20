@imports risersoft.shared.cloud.common
@Code
    ViewData("Title") = "Home Page"
    Dim baseUrl As String = Request.Url.Scheme & "://" & Request.Url.Authority & Request.ApplicationPath.TrimEnd("/")
    Dim banner As String = risersoft.shared.GlobalCore.GetConfigSetting("banner")
    If String.IsNullOrEmpty(banner) Then banner = "/Content/images/Books-banner.jpg" Else banner = baseUrl & banner

End Code
<div class="row imchn">
    <div class="col-md-8">
        <img src="@banner" style="width:650px;" class="img-responsive banner-img">
    </div>
    <div class="col-md-3 gst-banner-text">
        <h1 class="gst-banner-text-title">BooksNirvana</h1>
        <p class="lead gst-banner-text-para">Accounting Simplified.</p>
        <p><a href="/app" class="btn btn-primary btn-lg">Start Now &raquo;</a></p>

        <div class="col-md-12" style="margin-top:155px;">
            <a href="https://play.google.com/store/apps/details?id=com.risersoft.booksnirvana" target="_blank"><img class="play" src="~/Content/images/playstore-button.png" style="width:100px;"></a>&nbsp;
            <a href="#appstore" target="_blank"><img class="app" src="~/Content/images/appstore-button.png" style="width:100px;"></a>
        </div>
    </div>
</div>
<div><br /><br /></div>
<div class="row clsimgb">
    <div class="col-md-3 footer-nav footer-text">
        <div class="clsfoot">
            <h2>Finance & Accounting</h2>
            <p>
                Gain Absolute Financial Control. Tame Costs. Boost Revenues.
            </p>
        </div>
    </div>
    <div class="col-md-3 footer-nav footer-text">
        <div class="clsfoot">
            <h2>Inventory Management</h2>
            <p>
                Get a 360-degree View of Inventory across the Supply Chain.
            </p>
        </div>
    </div>
    <div class="col-md-3 footer-nav footer-text">
        <div class="clsfoot">
            <h2>Purchase and Suppliers</h2>
            <p>Make Informed and Well-Considered Purchase Decisions</p>
        </div>
    </div>
    <div class="col-md-3 footer-text">
        <div class="clsfoot">
            <h2>Multi Platform</h2>
            <p>Securely access your cloud hosted data from desktop, web & mobile.</p>
        </div>
    </div>
</div>