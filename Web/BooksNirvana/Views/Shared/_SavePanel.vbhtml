<div id="dialog-confirm"></div>


<div Class="row marb">


    <button type="button" ng-show="visdel" id="btndele" ng-click="btndel()" Class="btn btne">Delete</button><button type="button" ng-disabled="(status=='posted')" id="btnsave" Class="btn btn-primary btnf" ng-click="btnsave()">Save</button>
    <img src="~/Content/images/progressbar.gif" ng-show="(status=='posted')" width="80" />
    <i class="fa fa-2x fa-check" id="status" ng-show="(result=='success')">Success</i>
    <i class="fa fa-2x fa-remove" id="failure" ng-show="(result=='failure')">Failure</i>
    <button type="button" id="btnback" onclick="window.history.back()" Class="btn btn-default btnf">Back</button>


</div>
