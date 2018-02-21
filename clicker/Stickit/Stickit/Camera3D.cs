#region USING
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using V3 = Microsoft.Xna.Framework.Vector3;
using V2 = Microsoft.Xna.Framework.Vector2;
using MX = Microsoft.Xna.Framework.Matrix;
using MH = Microsoft.Xna.Framework.MathHelper;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
#endregion

namespace Stickit
{
    class Camera3D
    {
        #region VARS
        public IFocusable focus;
        Vector3 viewPos = new Vector3(190f, 120, 0.1f);
        public Vector3 usedCamPos;
        public float zoom = 20;
        public float usedZoom = 1;
        float wheelValue = 0;
        float angleA = 0;
        float angleB = MathHelper.ToRadians(45);

        public float AngleA
        {
            get { return this.angleA; }
            set { this.angleA = value; }
        }
        public float AngleB
        {
            get { return this.angleB; }
            set { this.angleB = value; }
        }

        #endregion
        #region INIT
        public Camera3D(IFocusable focus)
        {
            this.focus = focus;
            S.View = Matrix.CreateLookAt(viewPos, new Vector3(0), Vector3.Up);
        }
        #endregion
        #region UPDATE
        public void Update()
        {
            #region CAMERA VIEW
            Vector2 centerScreen = new Vector2(
                       S.gdm.PreferredBackBufferWidth,
                       S.gdm.PreferredBackBufferHeight) / 2f;

            //centerScreen += S.Xna;
            //centerScreen -= S.Winform;

            #region ROTATE CAMERA BY MOUSE
            if (S.ms.RightButton == ButtonState.Pressed)
            {
                if (S.prvms.RightButton == ButtonState.Released)
                {
                    Mouse.SetPosition
                        ((int)centerScreen.X, (int)centerScreen.Y);
                    S.ms = Mouse.GetState();
                }

                Vector2 dMouse =
                    new Vector2(S.ms.X, S.ms.Y) - centerScreen;

                dMouse /= 30f;

                angleA += dMouse.X;
                angleB += dMouse.Y;
                Mouse.SetPosition((int)centerScreen.X, (int)centerScreen.Y);
            }
            #endregion

            if (S.kb.IsKeyDown(Keys.W))
                AngleB -= 0.08f;
            if (S.kb.IsKeyDown(Keys.S))
                AngleB += 0.08f;

            angleB = MathHelper.Clamp(
                angleB, 0.05f, MathHelper.PiOver2 - 0.05f);


            if (S.ms.ScrollWheelValue != wheelValue)
            {
                zoom += (wheelValue - S.ms.ScrollWheelValue) / 20;
                if (zoom < 0.5f)
                    zoom = 0.5f;

                wheelValue = S.ms.ScrollWheelValue;
            }

            Vector3 camPos = get_cam_pos(angleA, angleB, zoom, this.focus.Position);


            #endregion
            #region Fix CAMERA ZoOM
            usedZoom = usedZoom * 0.85f + //lerp between usedZoom and zoom.
                                                      zoom * 0.15f;


            camPos = this.focus.Position + Vector3.Normalize(camPos - this.focus.Position) *
                            usedZoom;

            //camPos -= S.terrain.Size / 2;
            #endregion
            usedCamPos = Vector3.Lerp(usedCamPos, camPos, 0.29f);

            S.View = Matrix.CreateLookAt(usedCamPos, this.focus.Position, Vector3.Up);
        }

        private Vector3 get_cam_pos(float angleA, float angleB, float zoom, Vector3 target)
        {
            Vector3 camPos = Vector3.UnitY * zoom;

            camPos = Vector3.Transform(camPos, Matrix.CreateRotationX(angleB));
            camPos = Vector3.Transform(camPos, Matrix.CreateRotationY(angleA));

            camPos += target;
            return camPos;
        }

        public Ray get_mouse_ray()
        {
            V3 a = S.gd.Viewport.Unproject(
                new V3(S.ms.X, S.ms.Y, 0), S.Proj,
                S.View, Matrix.Identity);

            V3 b = S.gd.Viewport.Unproject(
                     new V3(S.ms.X, S.ms.Y, 1), S.Proj,
                     S.View, Matrix.Identity);

            return new Ray(a, Vector3.Normalize(b - a));
        }

        #endregion
    }
}
