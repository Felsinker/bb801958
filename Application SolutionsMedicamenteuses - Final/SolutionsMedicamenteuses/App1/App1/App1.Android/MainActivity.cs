using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App1.Droid
{
    [Activity(Label = "Solutions Médicamenteuses", Icon = "@mipmap/launcher_foreground", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Window window = this.Window; window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            /*window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 87, 75));
            window.SetNavigationBarColor(Android.Graphics.Color.Rgb(0, 87, 75));
            window.SetTitleColor(Android.Graphics.Color.Rgb(0, 87, 75));*/

            LoadApplication(new App());
        }
    }
}