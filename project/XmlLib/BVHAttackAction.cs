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
using V2 = Microsoft.Xna.Framework.Vector2;
using MX = Microsoft.Xna.Framework.Matrix;
using MH = Microsoft.Xna.Framework.MathHelper;
using V3 = Microsoft.Xna.Framework.Vector3;
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
#endregion

namespace XmlLib
{
    public class BVHAttackAction : BVHAction
    {
        public Dictionary<int, Vector3>
           attackDrcs;
        public List<int> collisionBones;

        public BVHAttackAction()
        {

        }

        public BVHAttackAction(string bvhfile, ActionType type, Matrix worldMX,
            string name, int startFrame,
            int endFrame, bool isCyclic, float framesPerLoop, Dictionary<int, Vector3> attackDrcs,
            List<int> collisionBones)
            : base(bvhfile, type, worldMX, name, startFrame, endFrame, isCyclic,
            framesPerLoop)
        {
            this.attackDrcs = attackDrcs;
            this.collisionBones = collisionBones;
        }

        public BVHAttackAction(
            BVHAction t)
            : base(t.Bvhfile,t.Type, t.WorldMX, t.Name,
            t.StartFrame, t.EndFrame,t.IsCyclic,t.FramesPerLoop)
        {
            attackDrcs = new Dictionary<int, Vector3>();
            this.collisionBones = new List<int>(); ;
        }

        public override string ToXml()
        {
            #region Matrix to string
            string mx = SerializeMatrix(WorldMX);

            #endregion

            string atkDrcs = string.Empty;
            foreach (KeyValuePair<int, Vector3> kvp in
                attackDrcs)
            {
                atkDrcs +=
                    string.Format(
                    "<Item><Key>{0}</Key>" +
                    "<Value>{1} {2} {3}</Value></Item>",
                    kvp.Key, kvp.Value.X, kvp.Value.Y,
                    kvp.Value.Z);
            }

            string collBones = string.Empty;


            foreach (int bone in collisionBones)
            {
                collBones += bone.ToString() + " ";
            }

            collBones += "\n";


            return
             "<Item>" +
             GenerateXml(mx) +
             "\n\t<attackDrcs>" + atkDrcs + "</attackDrcs>" +
   "\n\t<collisionBones>" + collBones + "</collisionBones>" +

             "\n</Item>";
        }

        public override string ToString()
        {
            return base.ToString() + " (atk)";
        }
    }

}
