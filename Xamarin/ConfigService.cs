using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Risersoft.Framework.Dependency;
using Risersoft.Framework.Global;
using Xamarin.Forms;

[assembly: Dependency(typeof(ConfigService))]
namespace Risersoft.Framework.Dependency
{
    public class ConfigService : IConfiguration
    {
        public string LoginServiceHost()
        {
            return "http://login.risersoft.com";
            //return "http://dse3.kanohar.net:11626";
        }

        public string RestServiceHost()
        {
            return "http://booksnirvana.com";
            //return "http://dse3.kanohar.net:50016";
        }


        public string AppName()
        {
            return Globals.myXamApp.objAppExtender.ProgramName();
        }
        
        public string ClientId()
        {
            return "Books.win";
        }

        public string ClientSecret()
        {
              return "books123";
        }
        public string AppList() 
        {
            return "nbks";
        }

    }
}