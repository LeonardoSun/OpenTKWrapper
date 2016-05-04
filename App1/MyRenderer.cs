using System;
using Android.Opengl;
using Javax.Microedition.Khronos.Egl;
using Javax.Microedition.Khronos.Opengles;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES11;
using OpenTK.Platform;
using OpenTK.Platform.Android;
using Android.Graphics;
using Android.Views;

namespace App1
{
    internal class MyRenderer : Java.Lang.Object, GLSurfaceView.IRenderer
    {
        float[] square_vertices = {
            -0.5f, -0.5f,
            0.5f, -0.5f,
            -0.5f, 0.5f,
            0.5f, 0.5f,
        };

        byte[] square_colors = {
            255, 255,   0, 255,
            0,   255, 255, 255,
            0,     0,    0,  0,
            255,   0,  255, 255,
        };

        private Android.Opengl.EGLDisplay eglDisplay;
        private Android.Opengl.EGLConfig eglConfig;
        private Android.Opengl.EGLContext eglContext;
        private Android.Opengl.EGLSurface eglSurface;

        //GLSurfaceView m_view;
        public event EventHandler FirstRender;

        bool first = true;

        public MyRenderer()//GLSurfaceView view
        {
            //m_view = view;
        }

        ~MyRenderer()
        {
            //EGLDestroy();
        }

        public void OnDrawFrame(IGL10 gl)
        {
            if(first)
            {
                FirstRender(this, null);
                first = false;
            }
            //GLES20.GlClear(GLES20.GlColorBufferBit);
            //return;
                GL.MatrixMode(All.Projection);
                GL.LoadIdentity();
                GL.Ortho(-1.0f, 1.0f, -1.5f, 1.5f, -1.0f, 1.0f);
                GL.MatrixMode(All.Modelview);
                GL.Rotate(3.0f, 0.0f, 0.0f, 1.0f);

                GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
                GL.Clear((uint)All.ColorBufferBit);

            for (int i=0;i< 10000;i++)
            {
                //Java.Nio.FloatBuffer floatBuf = Java.Nio.FloatBuffer.Wrap(square_vertices);
                // Java.Lang.IllegalArgumentException: Must use a native order direct Buffer
                GL.VertexPointer(2, All.Float, 0, square_vertices);
                GL.EnableClientState(All.VertexArray);

                //Java.Nio.ByteBuffer byteBuf = Java.Nio.ByteBuffer.Wrap(square_colors);
                GL.ColorPointer(4, All.UnsignedByte, 0, square_colors);
                GL.EnableClientState(All.ColorArray);

                GL.DrawArrays(All.TriangleStrip, 0, 4);

            }
            //EGL14.EglSwapBuffers(eglDisplay, eglSurface);
        }

        public void OnSurfaceChanged(IGL10 gl, int width, int height)
        {
            //GLES20.GlViewport(0, 0, width, height);
            GL.Viewport(0, 0, width, height);
        }

        public void OnSurfaceCreated(IGL10 gl, Javax.Microedition.Khronos.Egl.EGLConfig config)
        {
            //GLES20.GlClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            //EGLCreate(m_view.Holder);
        }
        public void EGLCreate(ISurfaceHolder holder)
        {
            int[] num_config = new int[1];
            Android.Opengl.EGLConfig[] configs = new Android.Opengl.EGLConfig[1];
            int[] configSpec = {
EGL10.EglRedSize,            8,
EGL10.EglGreenSize,        8,
EGL10.EglBlueSize,        8,

EGL10.EglSurfaceType,     EGL10.EglWindowBit,
EGL10.EglNone
};
            eglDisplay = EGL14.EglGetDisplay(EGL14.EglDefaultDisplay);
            int[] empty = { EGL14.EglNone };
            EGL14.EglInitialize(eglDisplay, empty, 0, empty, 0);

            EGL14.EglChooseConfig(eglDisplay, configSpec, 0,
             configs, 0, 1, num_config, 0);

            eglConfig = configs[0];
            int[] empty1 = { EGL14.EglNone };
            eglContext = EGL14.EglCreateContext(eglDisplay, eglConfig,
            EGL14.EglNoContext, empty1, 0);

            int[] empty2 = { EGL14.EglNone };
            eglSurface = EGL14.EglCreateWindowSurface(eglDisplay,
            eglConfig, (Java.Lang.Object)holder, empty2, 0);

            EGL14.EglMakeCurrent(eglDisplay, eglSurface,
            eglSurface, eglContext);

            //gl = (GL10)eglContext.GL();
        }

        public void EGLDestroy()
        {
            if (eglSurface != null)
            {
                EGL14.EglMakeCurrent(eglDisplay, EGL14.EglNoSurface,
                EGL14.EglNoSurface, EGL14.EglNoContext);
                EGL14.EglDestroySurface(eglDisplay, eglSurface);
                eglSurface = null;
            }
            if (eglContext != null)
            {
                EGL14.EglDestroyContext(eglDisplay, eglContext);
                eglContext = null;
            }
            if (eglDisplay != null)
            {
                EGL14.EglTerminate(eglDisplay);
                eglDisplay = null;
            }
        }


    }
}