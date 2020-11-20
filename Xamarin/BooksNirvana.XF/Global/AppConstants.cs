using Risersoft.Framework.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GSTNirvana.Global
{
    public static partial class AppConstants
    {
        

        public static string RestServiceHost()
        {
            return DependencyService.Get<IConfiguration>().RestServiceHost();
        }

      

        public static string apiBaseUri = RestServiceHost() + "/api/ess/";

        public static string GeoCoordinateBaseUri = "https://maps.googleapis.com/maps/api/geocode/json";
        public static string GoogleApiKey = "AIzaSyCJDk7zVSngulzSCEZnf8zJaL3GqKH3cDE";
        public static string MyLeaveUrl = "MyLeaveApp";
        public static string MyLeaveBalanceUrl = "MyLeaves";
        public static string CancelMyLeave = "CancelMyLeaveApp";
        public static string PendingTeamLeave = "SubLeaveApps";
        public static string SubLeaveBalance = "SubLeaveBalance?employeeid=";
        public static string SubLeaveApp = "SubLeaveApp";
        public static string MyPerson = "MyPerson";
        public static string MyAddress = "MyAddress";
        public static string MyEducation = "MyEdu";
        public static string MyFamilyMember = "MyFam";
        public static string Payslip = "payslip?id={0}";
        public static string paysliplist = "mypayslips";
        public static string MyPunch = "MyPunch";
        public static string PunchHistory = "mypunches";
        public static string BlobType = "BlockBlob";
        public static string ProfilePicUrl = "uploadurl";
        public static string ConfirmProfilePicUrl = "confirmurl?url=";


    }
}

//https://codewala.net/2015/05/25/outputcache-doesnt-work-with-web-api-why-a-solution/
//https://stackoverflow.com/questions/21643747/httpclient-every-time-returns-the-same-string
