using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES11;
using OpenTK.Platform;
using OpenTK.Platform.Android;
using Android.Views;
using Android.Content;
using Android.Util;
using Android.Opengl;
using System.Timers;

namespace App1
{
    class GLView : GLSurfaceView
    {
        Timer timer = new Timer(33);
        public GLView(Context context) : base(context)
        {
            //// Pick an EGLConfig with RGB8 color, 16-bit depth, no stencil,
            //// supporting OpenGL ES 2.0 or later backwards-compatible versions.
            //SetEGLConfigChooser(8, 8, 8, 0, 16, 0);
            //SetEGLContextClientVersion(2);
            start();
        }

        private MyRenderer mMyRenderer;
        bool rendered = false;

        public void start()
        {
            mMyRenderer = new MyRenderer();
            //Holder.AddCallback(this);
            //Holder.SetType(SurfaceType.Gpu);

            SetRenderer(mMyRenderer);
            timer.Elapsed += Timer_Elapsed;
            this.RenderMode = Rendermode.WhenDirty;
            mMyRenderer.FirstRender += (sender, e) =>
            {
                timer.Start();
                rendered = true;
            };
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.RequestRender();
        }

        public override void OnResume()
        {
            if (rendered)
                timer.Start();
            base.OnResume();
        }

        public override void OnPause()
        {
            timer.Stop();
            base.OnPause();
        }
    }
}
