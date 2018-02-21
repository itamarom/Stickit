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


namespace XmlLib
{
    public enum ActionType
    {
        Other,
        Damaged,
        Attack,
        Defense
    }

    public class BVHAction
    {
        public ActionType Type { get; set; }
        public string Bvhfile { get; set; }
        public Matrix WorldMX { get; set; }
        public string Name { get; set; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public bool IsCyclic { get; set; }
        public float FramesPerLoop { get; set; }
        public List<Skill> Skills = new List<Skill>();

        public BVHAction()
        {

        }

        public BVHAction(string bvhfile, ActionType type, Matrix worldMX, 
            string name, int startFrame, int endFrame,
            bool isCyclic, float framesPerLoop)
        {
            this.Bvhfile = bvhfile;
            this.WorldMX = worldMX;
            this.Name = name;
            this.StartFrame = startFrame;
            this.EndFrame = endFrame;
            this.Type = type;
            this.IsCyclic = isCyclic;
            this.FramesPerLoop = framesPerLoop;
        }

        public override string ToString()
        {
            return this.Name;
        }

        protected string GenerateXml(string matrix)
        {
            string xml = string.Format(
            "\n\t<Type>{0}</Type>" +
             "\n\t<Bvhfile>{1}</Bvhfile>" +
            "\n\t<WorldMX>{2}</WorldMX>" +
            "\n\t<Name>{3}</Name>" +
            "\n\t<StartFrame>{4}</StartFrame>" +
            "\n\t<EndFrame>{5}</EndFrame>"+
            "\n\t<IsCyclic>{6}</IsCyclic>" +
             "\n\t<FramesPerLoop>{7}</FramesPerLoop>",
            Type, Bvhfile, matrix, Name, StartFrame, EndFrame,
            IsCyclic.ToString().ToLower(),
            FramesPerLoop.ToString().ToLower());

            xml += "\n\t<Skills>";
            foreach (Skill s in Skills)
            {
                xml += "<Item>" + s.ToXml() + "</Item>";
            }
            xml += "\n\t</Skills>";
            return xml.Replace("\t", "    "); ;
        }

        public virtual string ToXml()
        {
            string mx = SerializeMatrix(WorldMX);

            return "<Item>" +
            GenerateXml(mx) +
            "\n</Item>".Replace("\t", "    ");
        }

        public string SerializeMatrix(Matrix mx)
        {
            string str = string.Empty;

            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    str += " " + typeof(Matrix).GetField(
                            "M" + i.ToString() + j.ToString()).GetValue(mx);
                }
            }

            return str;
        }
    }
}
