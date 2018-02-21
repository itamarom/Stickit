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

namespace XmlLib
{
    public class Skill
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public List<KeyAction> Combo{get;set;}
        public List<BVHTargetAction> targets = new List<BVHTargetAction>();

        public Skill()
        {
            this.Max = this.Min = -1;
        }

        public Skill(int min,int max, List<KeyAction> combo)
        {
            this.Min = min;
            this.Max = max;
            this.Combo = combo;
        }

        public string ToXml()
        {
            string s = string.Format(
                "<Min>{0}</Min>\n\t" +
                "<Max>{1}</Max>\n\t" +
                "<Combo>\n\t",
                this.Min, this.Max);

            foreach (KeyAction ka in Combo)
            {
                s += "<Item>" + ka.ToXml() + "</Item>\n\t";
            }
            s += "</Combo>\n\t<targets>\n\t";

            foreach (BVHTargetAction target in targets)
            {
                s += "<Item>" + target.ToXml() + "</Item>\n\t";
            }
            s += "</targets>";
            return s.Replace("\t", "    ");
        }

        public override string ToString()
        {
            return this.Min + " to " + this.Max;
        }
    }

    public class BVHTargetAction
    {
        public string Target { get; set; }
        public int TargetFrame { get; set; }
        public BVHTargetAction(string target, int targetframe)
        {
            this.Target = target;
            this.TargetFrame = targetframe;
        }

        public BVHTargetAction()
        {

        }

        public string ToXml()
        {
            return "<Target>"+this.Target+"</Target>\n\t"+
                "<TargetFrame>"+this.TargetFrame+"</TargetFrame>\n\t";
        }

        public override string ToString()
        {
            return this.TargetFrame + " @ " + this.Target;
        }
    }
}
