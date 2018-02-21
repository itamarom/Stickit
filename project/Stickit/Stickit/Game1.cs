using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Processors;
using XmlLib;

namespace Stickit
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        ActionCollection ac;

        #region EVENTS
        public static event UpdateDelegate UpdateEvent;
        public static event DrawDelegate Draw3DEvent;
        public static event DrawDelegate Draw2DEvent;
        #endregion

        #region VARS
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Axis axis;

        BVHClip gc;

        GroundedClip gcb;
        BVHActionPlayer player;
        WalkingClip wc;
        BVHPlayer playerb;

        bool isAfterIntro;
        Texture2D intro;

        List<DrawableModel> trees = new List<DrawableModel>();
        List<BaseClip> bc = new List<BaseClip>();

        double dfps, ufps;
        #endregion

        Sea sea;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            S.init(graphics, Content);
            S.Proj = Matrix.CreatePerspectiveFieldOfView(1, S.gd.Viewport.AspectRatio, 0.1f, 1000f);
            Dictionary<string, BVHContent> bla = S.bvhDic;
            //  graphics.PreferredBackBufferWidth = 1200;
            // graphics.PreferredBackBufferHeight = 900;
            //  graphics.ApplyChanges();

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            int oiaseg = 5;
            int wut = oiaseg + 9;

            ac = new ActionCollection(
                Content.Load<ActionCollection>
                ("xml/stand"),
                  Content.Load<ActionCollection>
                ("xml/walk"),
            Content.Load<ActionCollection>
                      ("xml/punch"),
           Content.Load<ActionCollection>
                ("xml/jump"),
           Content.Load<ActionCollection>
                ("xml/leap"),
           Content.Load<ActionCollection>
                ("xml/land"),
                      Content.Load<ActionCollection>
                ("xml/sitting"));



            playerb = new BVHPlayer("07_05", Color.Red);
            player = new BVHActionPlayer(ac.other[0], Color.Blue);

            axis = new Axis(S.gd, 500, S.View, S.Proj);
            #region Unused math functions for terrain
            //z = (sin(x)*cos(z)-sin(x)*sin(z))/(x^2+z^2+1)
            //MathFunc mf2 = new MathFunc(new Func<float, float, float>((x, z) =>
            //    {
            //        x *= 0.05f;
            //        z *= 0.05f;
            //        float one = (float)(Math.Sin(x + S.rnd.NextDouble()) * Math.Cos(z + S.rnd.NextDouble()));
            //        float two = (float)(Math.Sin(x + S.rnd.NextDouble()) * Math.Sin(z + S.rnd.NextDouble()));
            //        float divide = (float)(Math.Pow(Math.Abs(Math.Sin(x)), 2) + Math.Pow(Math.Abs(Math.Sin(z)), 2)) + 1;
            //        return (one - two) / divide * 20;
            //    }));
            //MathFunc mfc = new MathFunc(new Func<float, float, float>((x, z) =>
            //{
            //    x *= 0.05f;
            //    z *= 0.05f;
            //    float one = (float)(Math.Sin(x) * Math.Cos(z));
            //    float two = (float)(Math.Sin(x) * Math.Sin(z));
            //    float divide = (float)(Math.Pow(Math.Abs(Math.Sin(x)), 2) + Math.Pow(Math.Abs(Math.Sin(z)), 2)) + 1;
            //    return (one - two) / divide * 10;
            //}));
            //MathFunc kk = new MathFunc(new Func<float, float, float>((x, z) =>
            //  {
            //      return 120;
            //  }));

            #endregion

            #region INIT terrain
            MapFunc map = new MapFunc(Content.Load<Texture2D>("ding"), 0, 150, true);
            //  MapFunc map = new MapFunc(Content.Load<Texture2D>("kkk"), 0, 150, true);

            S.terrain = new AnimatedTerrain(256, 256);

            S.terrain.apply_height_func_over_rect(map, new Rectangle(0, 0, 256, 256));
            /*        S.terrain.apply_height_func_over_rect(
                        new MathFunc(
                            new Func<float, float, float>(
                                (x, z) =>
                                {
                                    return (float)Math.Sin((double)(new Vector3(x, 0, z) - S.terrain.Size / 2).Length());
                                })), new Rectangle(0, 0, 128, 128));*/

            S.terrain.recalculate_normals();
            //  S.terrain.make_it_circle();
            S.terrain.apply_color_texs(Content.Load<Texture2D>("lavab1"),
                Content.Load<Texture2D>("lavab2"),
                Content.Load<Texture2D>("lavab3"),
                Content.Load<Texture2D>("lavab2"));
            S.terrain.apply_height_func_over_delta(
                new MapFunc(
                Content.Load<Texture2D>("water"), 0, 10, true), -0.5f);
            //S.terrain.update_vertices_colors((int)(map.HighestPoint * 0.8f), 0.9f);
            S.terrain.make_it_circle();
            S.terrain.init_buffers();
            #endregion

            /* sea = new Sea(S.cm.Load<Texture2D>("lava1"),
                S.cm.Load<Texture2D>("lava2"),
                S.cm.Load<Texture2D>("lava3"),
                S.cm.Load<Texture2D>("lava2")); */

            /*     sea = new Sea(S.cm.Load<Texture2D>("sea1"),
         S.cm.Load<Texture2D>("sea2"),
         S.cm.Load<Texture2D>("sea3"),
         S.cm.Load<Texture2D>("sea2"));*/

            intro = Content.Load<Texture2D>("intro");

            gc = new BVHClip(
                 Vector3.UnitY * 180,
                 0.4f,
                 0.2f, player,
                false, 0.17f, ac);

            gcb = new GroundedClip(
                Vector3.UnitY * 180 + Vector3.UnitX * 10,
                0.2f, playerb,
                false, 0f);

            wc = new WalkingClip(gc.Position + Vector3.UnitX * 10,
                gc.WalkingSpeed * 0.7f,
                gc.Scale,
                new BVHActionPlayer(ac.other[0], Color.Gray),
                true, 0, gc,
                5,
                ac);

            S.camera = new Camera3D(gc);

            trees.Add(new DrawableModel(Content.Load<Model>("lowpolypalmii")));
            trees.Add(new DrawableModel(Content.Load<Model>("tree stomp sculpture")));
            //  trees.Add(new DrawableModel(Content.Load<Model>("lowpolytree1")));
            // trees.Add(new DrawableModel(Content.Load<Model>("palm.fbx")));

            for (int t = 0; t < trees.Count; t++)
            {
                for (int i = 0; i < 20; i++)
                {
                    Vector3 pos = Vector3.Transform(Vector3.UnitX, Matrix.CreateRotationY(
                        (float)S.rnd.NextDouble() * MathHelper.TwoPi)) * (float)S.rnd.NextDouble() * S.terrain.Width / 2;
                    pos.Y = S.terrain.get_y(pos.X, pos.Z, true);

                    GroundedClip a = new GroundedClip(pos, 1, trees[t], true, 0);
                    a.RotationY = (float)S.rnd.NextDouble() * MathHelper.TwoPi;
                    a.Scale = 1 + (float)S.rnd.NextDouble() * 5f;
                    bc.Add(a);
                    Game1.Draw3DEvent += a.Draw;
                    Game1.UpdateEvent += a.Update;

                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (S.clicked_key(Keys.Escape))
                Exit();

            S.update();

            if (isAfterIntro)
            {
                if (S.clicked_key(Keys.Enter))
                    graphics.ToggleFullScreen();


                if (IsActive)
                    S.camera.Update();

                if (UpdateEvent != null)
                    UpdateEvent(gameTime);

                ufps = 1000 / gameTime.ElapsedGameTime.TotalMilliseconds;

                if (S.kb.IsKeyDown(Keys.N))
                {
                    player.Frame = 7800;
                }


                if (S.kb.IsKeyDown(Keys.Space))
                {
                    if (S.kb.IsKeyDown(Keys.T))
                        player.change_bvh("Skeletons\\L\\144_20", true);
                    else if (S.kb.IsKeyDown(Keys.R))
                        player.change_bvh("Skeletons\\C\\07_05", true);
                }


                Window.Title = player.Frame.ToString() +
                    " --- " + ufps.ToString() + " -- " + dfps.ToString();

            }
            else
            {
                if (S.clicked_key(Keys.L))
                {
                    sea = new Sea(S.cm.Load<Texture2D>("lava1"),
                        S.cm.Load<Texture2D>("lava2"),
                        S.cm.Load<Texture2D>("lava3"),
                        S.cm.Load<Texture2D>("lava2"));

                    isAfterIntro = true;
                }
                else if (S.clicked_key(Keys.S))
                {

                    sea = new Sea(S.cm.Load<Texture2D>("sea1"),
                        S.cm.Load<Texture2D>("sea2"),
                        S.cm.Load<Texture2D>("sea3"),
                        S.cm.Load<Texture2D>("sea2"));

                    isAfterIntro = true;
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            dfps = 1000 / gameTime.ElapsedGameTime.TotalMilliseconds;


            GraphicsDevice.Clear(Color.White);
            S.be.AmbientLightColor = new Vector3(0.053f, 0.098f, 0.18f);
            S.be.View = S.View;
            S.be.Projection = S.Proj;
            S.be.FogEnabled = false;
            S.be.EnableDefaultLighting();
            S.be.VertexColorEnabled = true;

            RasterizerState rs = new RasterizerState();

            if (S.kb.IsKeyDown(Keys.K))
            {
                rs.CullMode = CullMode.None;
            }
            else
                rs.CullMode = CullMode.CullCounterClockwiseFace;

            if (S.kb.IsKeyDown(Keys.G))
            {
                rs.FillMode = FillMode.WireFrame;
            }
            else
            {
                rs.FillMode = FillMode.Solid;
            }

            IsMouseVisible = true;

            S.gd.RasterizerState = rs;



            if (Draw3DEvent != null)
                Draw3DEvent();

            S.be.LightingEnabled = false;
            S.be.World = Matrix.Identity;
            S.be.Techniques[0].Passes[0].Apply();

            spriteBatch.Begin();

            if (!isAfterIntro)
            {
                spriteBatch.Draw(intro, Vector2.Zero, Color.White);
            }

            if (Draw2DEvent != null)
                Draw2DEvent();

            spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            base.Draw(gameTime);
        }
    }
}
