using FFImageLoading.Forms.Platform;
using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;

namespace EBZ.Mobile.Android
{
    [Activity(Label = "EBZ.Mobile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);
            ToastConfig.DefaultDuration = TimeSpan.FromSeconds(5.0);
            ToastConfig.DefaultPosition = ToastPosition.Top;

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}

