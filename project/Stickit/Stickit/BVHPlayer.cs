using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Processors;
using XmlLib;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;

namespace Stickit
{
    class BVHPlayer : IDrawable
    {
        BVHContent bvh;
        public int Frame
        {
            get { return (int)fFrame; }
            set { this.fFrame = value; }
        }
        private float fFrame;

        /// <summary>
        /// Frame per loop.
        /// </summary>
        float fpl = 3f;
        Matrix[] absBoneMX;
        Matrix world;

        private NCylinder cylinder;
        public bool animate = true;


        /// <summary>
        /// Load all bvh in the Content directory into bvh dictionary
        /// </summary>
        static BVHPlayer()
        {
            string[] dirs = Directory.GetDirectories(S.cm.RootDirectory + "\\Skeletons");

            foreach (string d in dirs)
            {
                BVHContent b = load_all_bvh(d, null);
                if (b != null)
                    b.Skeleton.calc_cylinder_matrices(b);
            }
        }

        /// <summary>
        /// Load all bvh in directory by recursive
        /// </summary>
        /// <param name="dirPath">Directory path</param>
        private static BVHContent load_all_bvh(string dirPath, Skeleton rcp)
        {
            #region Go over all files in current dir and update root if changed.
            string[] files = Directory.GetFiles(dirPath);
            rcp = load_bvh_from_array(files, rcp);
            #endregion

            #region Go over all dirs and load files in them.
            string[] directories = Directory.GetDirectories(dirPath);
            foreach (string d in directories)
            {
                load_all_bvh(d, rcp);
            }
            #endregion

            if (files.Length > 0)
            {
                return S.bvhDic[files[files.Length - 1].Substring(files[files.Length - 1].LastIndexOf('\\') + 1).Replace(".bvh", "").Replace("Content\\", "")]; //return the last bvh that was loaded
            }
            else
                return null;
        }

        /// <summary>
        /// Load all bvh from string array
        /// </summary>
        /// <param name="files">Files array.</param>
        /// <param name="skl">Skeleton to be used. (if null, a new one will be analyzed)</param>
        /// <returns>New Root value. (not changed unless Root was null)</returns>
        private static Skeleton load_bvh_from_array(string[] files, Skeleton skl)
        {
            #region Go over all files and analyze them.
            foreach (string f in files)
            {
                string newf = f.Replace("Content\\", string.Empty);
                if (newf.EndsWith(".bvh"))
                {
                    List<RootMovement> rm;

                    string key = newf.Substring(newf.LastIndexOf('\\') + 1).Replace(".bvh", string.Empty);
                    if (ActionCollection.bvhToLoad.ContainsKey(key))
                    {
                        rm = ActionCollection.bvhToLoad[key];
                    }
                    else
                    {
                        rm = new List<RootMovement>();
                        rm.Add(RootMovement.KeepOnlyY);
                    }

                    BVHContent bvh;

                    #region If Root is Null, analize new one and set it as Root.
                    if (skl == null)
                    {
                        bvh = new BVHContent(
                            StringProccesing.GetFileContents(newf), null, rm);
                        skl = new Skeleton(bvh.Skeleton.Root, bvh.Skeleton.BoneChannels);

                    }
                    #endregion
                    #region If Root is not Null, use it.
                    else
                        bvh = new BVHContent(
                          StringProccesing.GetFileContents(newf), skl, rm);
                    #endregion

                    S.bvhDic.Add(key, bvh);

                }
            }
            #endregion

            return skl;
        }
        public BVHPlayer(string id, Color color)
            : this(S.bvhDic[id], color)
        { }

        public BVHPlayer(BVHContent bvh, Color color)
        {
            cylinder = new NCylinder(0.21f, 0.08f, 1, 6, color);

            change_bvh(bvh, true);
        }

        public void Animate(GameTime gameTime)
        {
            #region Advance and run frame if needed.
            if (animate)
            {
                fFrame += fpl;
                fFrame %= bvh.MXI.FrameCount;
                Frame = (int)fFrame;
                run_frame();
            }
            #endregion
        }

        public void change_bvh(string content, bool runFrame)
        {
            change_bvh(S.bvhDic[content], runFrame);
        }
        public void change_bvh(BVHContent content, bool runFrame)
        {
            if (bvh == null || content.Skeleton != bvh.Skeleton)
            {
                absBoneMX = new Matrix[content.Skeleton.BoneCount];
            }

            this.bvh = content;
            this.fFrame = 0;

            if (runFrame)
                run_frame();

        }

        /// <summary>
        /// Advances the animation by one frame.
        /// </summary>
        private void run_frame()
        {
            for (int j = 0; j < bvh.Skeleton.BoneCount; j++)
            {
                Matrix? parent = null;
                Matrix m;

                #region If not root, then set parent
                if (j > 0)
                    parent = absBoneMX[bvh[j].Parent.Id];
                #endregion

                #region Calc abs mx
                m = bvh[j].calc_abs_mx_from_frame(
                             bvh.MXI, Frame, parent);
                #endregion

                #region Update mx in array
                absBoneMX[j] = m;
                #endregion
            }
        }

        public void Draw(BaseClip owner)
        {
            #region Calculate World Matrix
            world = Matrix.CreateScale(owner.Scale)
                        * Matrix.CreateRotationX(owner.RotationX)
                        * Matrix.CreateRotationY(owner.RotationY)
                        * Matrix.CreateRotationZ(owner.RotationZ)
                        * Matrix.CreateTranslation(owner.Position);
            #endregion

            S.be.LightingEnabled = true;

            S.gd.SetVertexBuffer(cylinder.vBuffer);
            S.gd.Indices = cylinder.iBuffer;

            for (int i = 0; i < bvh.Skeleton.BoneCount; i++)
            {
                S.be.World = bvh.Skeleton.CylinderScaleOffset[i] *
                    absBoneMX[i] * world;
                S.be.CurrentTechnique.Passes[0].Apply();

                /*S.gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                     cylinder.vertices, 0, cylinder.vertices.Length,
                     cylinder.indices, 0, cylinder.indices.Length / 3,
                     VertexPositionColorNormal.VertexDeclaration);*/
                S.gd.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                    0, 0, cylinder.vCount, 0, cylinder.iCount / 3);

            }

            S.gd.SetVertexBuffer(null);
            S.gd.Indices = null;


            S.be.World = world;
            S.be.CurrentTechnique.Passes[0].Apply();

            /* S.gd.DrawUserPrimitives(PrimitiveType.LineList,
                           linesClone, b * 2, 1, VPC.VertexDeclaration);*/

            //S.gd.DrawUserPrimitives(PrimitiveType.LineList,
            //     linesClone, 0, linesClone.Length / 2, .VertexDeclaration);

            S.be.LightingEnabled = true;
        }
    }
}
