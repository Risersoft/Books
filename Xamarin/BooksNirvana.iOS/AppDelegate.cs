using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace GSTNirvana.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch(Exception ex)
            { }
        }
    }
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            //UIBarButtonItem.Appearance.SetBackButtonTitlePositionAdjustment(new UIOffset(-100, -60), UIBarMetrics.Default);
            return base.FinishedLaunching(app, options);
        }
    }
}
