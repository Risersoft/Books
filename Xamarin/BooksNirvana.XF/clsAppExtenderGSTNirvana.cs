using GSTNirvana.Global;
using risersoft.shared;
using risersoft.shared.portable.Models.Nav;
using risersoft.shared.Providers;
using Risersoft.Framework.Global;
using Risersoft.Framework.Pages;
using Risersoft.Framework.Pages.Framework;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GSTNirvana
{
    public class clsAppExtenderGSTNirvana : clsXamAppExtendBase

    {
        public override string StartupAppCode()
        {
            
            return "nbks";
        }

        public override IForm AboutBox()
        {
            throw new NotImplementedException();
        }


        public override clsCollecString<Type> dicFormModelTypes()
        {
            throw new NotImplementedException();
        }

        public override clsCollecString<Type> dicReportProviderTypes(clsAppController myContext)
        {
            throw new NotImplementedException();
        }

        public override clsCollecString<Type> dicTaskProviderTypes()
        {
            throw new NotImplementedException();
        }

        public override clsCollecString<Type> dicWOInfoTypes()
        {
            throw new NotImplementedException();
        }
        public override Page AddPage(string str1)
        {
            switch (str1.Trim().ToLower())
            {
                case "main":
                   return new MainPage();
                default:
                    return new Page();
            }
        }

        public override string ProgramName()
        {
            return "BooksNirvana";
        }
        public override string ProgramCode()
        {
            return "gst";
        }
        public override IAppDataProvider CreateDataProvider(clsAppController context, IAsyncWCFCallBack cb)
        {
            //InstanceContext cbcontext = new InstanceContext(cb);
            //var str1 = AppConstants.WCFServiceHost();
            //var Provider = ChannelProxyFactory.CreateNetHttp<IAppDataProviderDuplexClient>(cbcontext, str1, context.Police as IServiceAuthorizer);
            //return Provider;
            var str1 = AppConstants.RestServiceHost();
            
            var provider = new clsAppDataProviderREST(context,str1,(clsXamPolice)context.Police);
            return provider;

        }

        public override IForm frmDel(clsView pView, string IDX, string sysentXML)
        {
            throw new NotImplementedException();
        }
    }

}
