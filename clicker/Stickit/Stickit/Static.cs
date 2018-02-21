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
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using VPNT = Microsoft.Xna.Framework.Graphics.VertexPositionNormalTexture;
using VPCN = Stickit.VertexPositionColorNormal;
using XmlLib;

namespace Stickit
{
    public delegate void UpdateDelegate(GameTime gameTime);
    public delegate void DrawDelegate();
    public delegate void GameActivated();

    static class S
    {
        public static Vector2 Winform;
        public static Vector2 Xna;
        public static Vector2 distance;

        public static bool isMarkingBones = false;

        #region VARS
        public static GraphicsDevice gd;
        public static GraphicsDeviceManager gdm;
        public static BasicEffect be;
        public static Matrix View, Proj;
        public static Random rnd = new Random();
        public static Terrain terrain;
        public static MouseState ms, prvms;
        public static KeyboardState kb, prvkb;
        public static Vector3 gravity = -Vector3.UnitY * 0.07f;
        public static ContentManager cm;
        #endregion

        #region EDITOR VARS
       public static BVHAction action;
       public static BVHPlayer player;
       public static int stopFrame;
       public static Camera3D camera;
       public static Matrix attackDirMatrix;
        #endregion

        public static void init(GraphicsDeviceManager gdm, ContentManager cm)
        {
            S.gdm = gdm;
            S.gd = S.gdm.GraphicsDevice;
            S.cm = cm;
            S.be = new BasicEffect(S.gd);
        }

        public static void update()
        {
            S.prvms = S.ms;
            S.ms = Mouse.GetState();
            S.prvkb = S.kb;
            S.kb = Keyboard.GetState();
        }

        public static Matrix point_to_axis_angle(Vector3 offset)
        {
            #region point to axis angle
            Vector3 normal = Vector3.Normalize(offset);
            float length = offset.Length();

            float angle =
           (float)Math.Atan2(
           (double)new Vector2(normal.X, normal.Z).Length(),
           normal.Y);

            Vector3 axis = new Vector3(normal.Z, 0,
                -normal.X);

            axis.Normalize();

            if (float.IsNaN(axis.X) || float.IsNaN(axis.Y)
                || float.IsNaN(axis.Z))
            {
                axis = Vector3.UnitX;
                angle = MathHelper.Pi;
            }
            #endregion

            return Matrix.CreateFromAxisAngle(axis, angle);
        }
    }
}
