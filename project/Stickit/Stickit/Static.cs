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
using Processors;
using XmlLib;

namespace Stickit
{
    public delegate void UpdateDelegate(GameTime gameTime);
    public delegate void DrawDelegate();
    public delegate void ActionComplete(BVHAction previous, BVHAction current);

    static class S
    {
        public static Dictionary<string, BVHContent> bvhDic = new Dictionary<string, BVHContent>();

        #region VARS
        public static GraphicsDevice gd;
        public static GraphicsDeviceManager gdm;
        public static BasicEffect be;
        public static Matrix View, Proj;
        public static Random rnd = new Random();
        public static AnimatedTerrain terrain;
        public static MouseState ms, prvms;
        public static KeyboardState kb, prvkb;
        public static Vector3 gravity = -Vector3.UnitY*0.07f;
        public static ContentManager cm;
        public static Camera3D camera;
        #endregion

        public static void init(GraphicsDeviceManager gdm,ContentManager cm)
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

        public static bool clicked_key(Keys key)
        {
            return S.kb.IsKeyDown(key)
                && S.prvkb.IsKeyUp(key);
        }
        public static bool released_key(Keys key)
        {
            return S.kb.IsKeyUp(key)
                && S.prvkb.IsKeyDown(key);
        }
        public static bool clicked_left_button()
        {
            return S.ms.LeftButton == ButtonState.Pressed
                 && S.prvms.LeftButton == ButtonState.Released;
        }
        public static bool creleased_left_button()
        {
            return S.ms.LeftButton == ButtonState.Pressed
                 && S.prvms.LeftButton == ButtonState.Released;
        }
    }
}
