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
    class BVHActionPlayer : IDrawable
    {
        public event ActionComplete OnActionComplete;

        public BVHAction CurrentAction
        {
            get
            {
                return this.currentAction;
            }
            set
            {
                if (currentAction == null ||
                    currentAction.Bvhfile != value.Bvhfile)
                    change_bvh(value.Bvhfile, true);
                {
                        this.currentAction = value;
                        this.Frame = this.currentAction.StartFrame;
                }
            }

        }
        private BVHAction currentAction;

        public BVHAction DefaultAction { get; set; }
        BVHContent bvh;

        #region FROM BVHPlayer
        public int Frame
        {
            get { return (int)fFrame; }
            set { this.fFrame = value; }
        }
        private float fFrame;

        /// <summary>
        /// Frame per loop.
        /// </summary>
        Matrix[] absBoneMX;
        private NCylinder cylinder;
        public bool animate = true;
        #endregion

        public BVHActionPlayer(BVHAction defaultAction, Color color)
        {
            this.CurrentAction =
            this.DefaultAction = defaultAction;

            cylinder = new NCylinder(0.21f, 0.08f, 1, 6, color);
            bvh = S.bvhDic[CurrentAction.Bvhfile];
            change_bvh(bvh, true);
        }

        public void change_bvh(string content, bool runFrame)
        {
            change_bvh(S.bvhDic[content], runFrame);
        }
        public void change_bvh(BVHContent content, bool runFrame)
        {
            if (bvh == null || absBoneMX == null || content.Skeleton != bvh.Skeleton)
            {
                absBoneMX = new Matrix[content.Skeleton.BoneCount];
            }

            this.bvh = content;
            this.fFrame = 0;
            if (runFrame)
                run_frame();
        }

        public void Animate(GameTime gameTime)
        {
            #region Advance and run frame if needed.
            if (animate)
            {
                fFrame += CurrentAction.FramesPerLoop;
                if (fFrame > CurrentAction.EndFrame)
                {
                    if (!CurrentAction.IsCyclic)
                    {
                        if (OnActionComplete != null)
                            OnActionComplete(CurrentAction, DefaultAction);

                        CurrentAction = DefaultAction;
                    }
                    else
                    {
                        if (OnActionComplete != null)
                            OnActionComplete(CurrentAction, CurrentAction);
                    }

                    fFrame = CurrentAction.StartFrame;
                }
                run_frame();
            }
            #endregion
        }

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
            Matrix world = owner.calc_world();

            S.be.LightingEnabled = true;

            S.gd.SetVertexBuffer(cylinder.vBuffer);
            S.gd.Indices = cylinder.iBuffer;

            for (int i = 0; i < bvh.Skeleton.BoneCount; i++)
            {
                  S.be.World = bvh.Skeleton.CylinderScaleOffset[i] *
                        absBoneMX[i] * currentAction.WorldMX * world;

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
