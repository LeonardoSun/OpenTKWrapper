using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace App1
{
    [Activity(Label = "App1",
        MainLauncher = true,
        Icon = "@drawable/icon",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden
#if __ANDROID_11__
		,HardwareAccelerated=false
#endif
        )]
    public class MainActivity : Activity
    {
        GLView view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create our OpenGL view, and display it
            view = new GLView(this);
            SetContentView(view);
        }

        protected override void OnPause()
        {
            view.OnPause();
            base.OnPause();
        }

        protected override void OnResume()
        {
            view.OnResume();
            base.OnResume();
        }
    }
}

