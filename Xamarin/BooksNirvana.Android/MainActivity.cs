using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Risersoft.Framework.Droid.Dependency;
using Android.Support.V7.App;
using Android.Util;
using System.Threading.Tasks;
using Android.Content;
using Risersoft.Framework.Droid;
using Risersoft.Framework.Dependency;

namespace GSTNirvana.Droid
{

    [Activity(Label = "BooksNirvana", Icon = "@drawable/icon", Theme = "@style/MyTheme.Splash", MainLauncher = true,NoHistory =true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");

        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(3000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(MainApplication.Context, typeof(MainActivity)));
            this.Finish();
        }
    }

    [Activity(Label = "Booksirvana", Theme = "@style/MyTheme",ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation =ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            LoadApplication(new App());
            DependencyService.Register<StatusBarService>();
            DependencyService.Register<SaveFile>();
            DependencyService.Register<StorageService>();
           // DependencyService.Register<RefreshTokenService>();
            DependencyService.Register<ImplementCrossCropPage>();
            DependencyService.Register<SavePic>();
            DependencyService.Register<ConfigService>();
            DependencyService.Register<CloseApplication>();
            //ImmersiveMode();
        }
        private void ImmersiveMode()
        {
            var activity = (Activity)Forms.Context;
            Android.Views.View decorview = activity.Window.DecorView;
            var uiOptions = (int)decorview.SystemUiVisibility;
            var newUiOptions = (int)uiOptions;
            newUiOptions = newUiOptions | (int)SystemUiFlags.Fullscreen | (int)SystemUiFlags.ImmersiveSticky | (int)SystemUiFlags.LayoutHideNavigation | (int)SystemUiFlags.HideNavigation | (int)SystemUiFlags.LayoutStable | (int)SystemUiFlags.LayoutFullscreen;
            decorview.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}

