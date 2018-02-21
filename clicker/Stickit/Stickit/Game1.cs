using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;
//using System.Drawing;
using Processors;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Point = System.Drawing.Point;
using Microsoft.VisualBasic;
using XmlLib;
using System.Reflection;

namespace Stickit
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region EVENTS
        public static event UpdateDelegate UpdateEvent;
        public static event DrawDelegate Draw3DEvent;
        public static event DrawDelegate Draw2DEvent;
        #endregion
        #region VARS
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Axis axis;

        /// <summary>
        /// Static for access from bvhAction.
        /// </summary>
        public static List<int> chosenBones = new List<int>();

        VertexPositionColor[] mouseRay;

        NCylinder attackDirCylinder;

        MouseState prevMs = Mouse.GetState();

        BaseClip gc;

        List<DrawableModel> trees = new List<DrawableModel>();
        List<BaseClip> bc = new List<BaseClip>();

        double dfps, ufps;
        #endregion
        public static IntPtr ParentHandle;
        public static string filename;
        //ntrol cntrl;
        int cntrl = 0;
        const bool MOVE_ITEMS = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }
        public Game1(int width, int height)
            : this()
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();

            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = ParentHandle;
        }

        protected override void Initialize()
        {
            Form MyGameForm = (Form)Form.FromHandle(Window.Handle);
            MyGameForm.FormBorderStyle = FormBorderStyle.None;
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        [STAThreadAttribute]
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            S.init(graphics, Content);
            S.Proj = Matrix.CreatePerspectiveFieldOfView(1, S.gd.Viewport.AspectRatio, 0.1f, 1000f);
            Dictionary<string, BVHContent> bla = BVHPlayer.bvhDic;


            attackDirCylinder = new NCylinder(
                0.3f, 0.01f, 5, 6, Color.Red);

            S.player = new BVHPlayer(new BVHContent(
                                      File.ReadAllText(filename),
                                      null,
                                      MainForm.saveRootMovement));

            S.player.Bvh.Skeleton.calc_cylinder_matrices(S.player.Bvh);
            S.player.animate = false;

            axis = new Axis(S.gd, 500, S.View, S.Proj);

            gc = new GroundedClip(
                 Vector3.UnitY * 180,
                 0.2f, S.player,
                false, 0.17f);

            Game1.Draw3DEvent += gc.Draw;
            Game1.UpdateEvent += gc.Update;

            S.camera = new Camera3D(gc);
        }

        protected override void Update(GameTime gameTime)
        {
            S.Xna.X = Window.ClientBounds.X;
            S.Xna.Y = Window.ClientBounds.Y;

            if (S.action != null)
                S.player.fpl = S.action.FramesPerLoop;

            S.update();

            S.camera.Update();

            if (S.kb.IsKeyDown(Keys.N) && S.prvkb.IsKeyUp(Keys.N))
            {

            }
            if (S.kb.IsKeyDown(Keys.M) && S.prvkb.IsKeyUp(Keys.M))
            {

            }

            if (UpdateEvent != null)
                UpdateEvent(gameTime);

            

            ufps = 1000 / gameTime.ElapsedGameTime.TotalMilliseconds;

            if (S.kb.IsKeyDown(Keys.Space))
            {
                if (S.kb.IsKeyDown(Keys.T))
                    S.player.change_bvh("Skeletons\\L\\144_20", true);
                else if (S.kb.IsKeyDown(Keys.R))
                    S.player.change_bvh("Skeletons\\C\\07_05", true);
            }

            if (S.stopFrame != -1 && S.player.Frame >= S.stopFrame)
            {
                if (S.action != null && S.action.IsCyclic)
                {
                    S.player.Frame = S.action.StartFrame;
                }
                else
                {
                    S.player.Frame = S.stopFrame;
                    S.stopFrame = -1;
                    S.player.animate = false;
                }
            }

            /* Window.Title = S.camera.zoom.ToString() +
                 "   " + S.camera.usedZoom.ToString() +
                  "   " + S.camera.usedCamPos.ToString();*/
            //S.player.animate = S.kb.IsKeyDown(Keys.Space);
            //S.player.animate = drc != Vector3.Zero;
            //S.playerb.animate = drc != Vector3.Zero;
            /* Window.Title =
                 "M and N for floor and ceiling of S.camera angle. --" +
                 S.player.Frame.ToString() +
                 " --- " + ufps.ToString() + " -- " + dfps.ToString()
                 + " - " + S.camera.AngleA.ToString() + " - " + S.camera.AngleB.ToString();
             */

            /* Window.Title =
                 "frm: " + S.player.Frame.ToString() +
                 "dfps: " + dfps.ToString() + " ufps: " + ufps.ToString();*/

            //  mouseRay = null;

            //  chosenBones = new List<int>();
            if (S.isMarkingBones &&
                S.ms.LeftButton == ButtonState.Pressed
                && S.prvms.LeftButton == ButtonState.Released)
            {

                Ray msRay = S.camera.get_mouse_ray();

                mouseRay = new VertexPositionColor[2];
                mouseRay[0].Position = msRay.Position;
                mouseRay[1].Position = msRay.Position
                    + msRay.Direction * 100;

                mouseRay[0].Color = Color.Red;
                mouseRay[1].Color = Color.Green;

                for (float d = 0; d < S.camera.zoom + 5; d += 0.15f)
                {
                    Vector3 pos = msRay.Position +
                        msRay.Direction * d;

                    for (int i = 0; i < S.player.Bvh.Skeleton.BoneCount; i++)
                    {

                        Matrix mx = Matrix.Invert(
                       S.player.Bvh.Skeleton.CylinderScaleOffset[i] *
                            S.player.absBoneMX[i] * S.player.world);

                        Vector3 relative =
                            Vector3.Transform(pos, mx);

                        if (BVHPlayer.blueCylinder.is_in_cylinder(
                            relative))
                        {
                            if (chosenBones.Contains(i))
                                chosenBones.Remove(i);
                            else
                                chosenBones.Add(i);
                            // break;
                        }

                    }
                }

                if (S.action is BVHAttackAction)
                {
                    ((BVHAttackAction)S.action)
                        .collisionBones =
                        chosenBones.ToList();
                }


            }
            Control window = Control.FromHandle(Window.Handle);
            if (MOVE_ITEMS)
            {
                if (S.ms.MiddleButton == ButtonState.Pressed
                             && S.prvms.MiddleButton == ButtonState.Released)
                {
                    cntrl--;
                }
                if (cntrl < window.Controls.Count && cntrl >= 0)
                {
                    window.Controls[cntrl].Location = new Point(
                     (int)S.ms.X + 20,
                     (int)S.ms.Y);
                    //    Window.Title = S.ms.X + " , " + S.ms.Y;
                    Window.Title = S.ms.LeftButton + " , " + S.prvms.LeftButton;

                    if (S.ms.LeftButton == ButtonState.Pressed
                        && S.prvms.LeftButton == ButtonState.Released)
                    {
                        cntrl++;

                        string str = string.Empty;

                        for (int i = 0; i < cntrl; i++)
                        {
                            str += "window.Controls[" + i.ToString() + "].Location = new Point("
                                + window.Controls[i].Location.X +
                                ", " + window.Controls[i].Location.Y + ");\n";

                            //  window.Controls[i].Location.ToString() + "\n";
                        }
                        Clipboard.SetText(str);
                    }
                }

                for (int i = 0; i < window.Controls.Count; i++)
                {
                    window.Controls[i].Invalidate();
                }
            }



            base.Update(gameTime);
        }

        /*private Vector3 get_cam_pos(float angleA, float angleB, float zoom, Vector3 target)
        {
            Vector3 camPos = Vector3.UnitY * zoom;

            camPos = Vector3.Transform(camPos, Matrix.CreateRotationX(angleB));
            camPos = Vector3.Transform(camPos, Matrix.CreateRotationY(angleA));

            camPos += target;
            return camPos;
        }*/


        protected override void Draw(GameTime gameTime)
        {
            Texture2D te = new Texture2D(GraphicsDevice,
                1, 1);
            te.SetData<Color>(new Color[] { Color.White });

            spriteBatch.Begin();
            Vector2 point = new Vector2(S.ms.X, S.ms.Y);
            Control.FromHandle(Window.Handle).Visible = true;
            Control.FromHandle(Window.Handle).Location =
                new Point((int)S.Winform.X, (int)S.Winform.Y);

            spriteBatch.Draw(te, new Rectangle(
                S.ms.X, S.ms.Y, 30, 30), Color.White);

            Window.Title = point.ToString() +
                " -- " + S.Xna.ToString() + " -- " + S.Winform.ToString();

            spriteBatch.End();

            dfps = 1000 / gameTime.ElapsedGameTime.TotalMilliseconds;
            gc.Position = Vector3.Zero;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            S.be.View = S.View;
            S.be.Projection = S.Proj;
            if (S.kb.IsKeyDown(Keys.C))
            {
                S.be.FogEnabled = true;
                S.be.FogColor = new Vector3(0.5f, 0.3f, 0.8f);
                S.be.FogStart = 0;
                S.be.FogEnd = S.be.FogStart + 140;
            }
            else
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

            spriteBatch.Begin();

            if (Draw2DEvent != null)
                Draw2DEvent();

            spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            if (Draw3DEvent != null)
                Draw3DEvent();

            BVHAttackAction action =
           S.action as BVHAttackAction;

            if (action != null &&
                          action.attackDrcs.ContainsKey(S.player.Frame))
            {
                S.be.World = S.attackDirMatrix;

                S.be.Techniques[0].Passes[0].Apply();

                S.gd.SetVertexBuffer(attackDirCylinder.vBuffer);
                S.gd.Indices = attackDirCylinder.iBuffer;
                S.gd.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                    0, 0, attackDirCylinder.vCount,
                    0, attackDirCylinder.iCount / 3);
            }

            if (mouseRay != null)
            {

                S.be.World = Matrix.Identity;
                S.be.LightingEnabled = false;
                S.be.Techniques[0].Passes[0].Apply();

                S.gd.SetVertexBuffer(null);
                S.gd.Indices = null;
                S.gd.DrawUserPrimitives(PrimitiveType.LineList,
                    mouseRay, 0, mouseRay.Length / 2);
            }


            S.be.LightingEnabled = false;
            S.be.World = Matrix.Identity;
            S.be.Techniques[0].Passes[0].Apply();
            // GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
            //   c.vertices, 0, c.vertices.Length, c.indices, 0, c.indices.Length / 3);

            //S.terrain.Draw();
            axis.Draw(S.gd, S.View);

            base.Draw(gameTime);
        }
    }
}
