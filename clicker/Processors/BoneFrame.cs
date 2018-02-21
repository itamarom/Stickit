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
using System.Reflection;

namespace Processors
{
    /// <summary>
    /// re presents a frame before calculating
    /// </summary>
    public class FrameCalc
    {
        public string boneName { get; set; }
        public Vector3 Offset { get; set; }
        #region Matrix before calculation
        //position
        public float Xposition { get; set; }
        public float Yposition { get; set; }
        public float Zposition { get; set; }

        //rotation
        public float Xrotation { get; set; }
        public float Yrotation { get; set; }
        public float Zrotation { get; set; }

        private string[] rotOrder;
        private int rotOrderIndex = 0;
        #endregion

        public FrameCalc(string boneName, Vector3 offset)
        {
            this.boneName = boneName;
            this.Offset = offset;
            rotOrder = new string[3];
        }

        /// <summary>
        /// Updates matrix values.
        /// </summary>
        /// <param name="channel">Channel name</param>
        /// <param name="value">Channel value</param>
        public void update_channel_values(string channel, float value)
        {
            switch (channel)
            {
                case "Xrotation": Xrotation = value;
                    rotOrder[rotOrderIndex++] = "x"; break;
                case "Yrotation": Yrotation = value;
                    rotOrder[rotOrderIndex++] = "y"; break;
                case "Zrotation": Zrotation = value;
                    rotOrder[rotOrderIndex++] = "z"; break;
                case "Xposition": Xposition = value; break;
                case "Yposition": Yposition = value; break;
                case "Zposition": Zposition = value; break;
            }
        }

        public Matrix calc_frame()
        {
            float Xrotation = MathHelper.ToRadians(this.Xrotation);
            float Yrotation = MathHelper.ToRadians(this.Yrotation);
            float Zrotation = MathHelper.ToRadians(this.Zrotation);

            Matrix m = Matrix.Identity;

            for (int r = 0; r < rotOrder.Length; r++)
            {
                Vector3 axis = Vector3.Zero;
                float rot = 0;

                switch (rotOrder[r])
                {
                    case "x":
                        axis = Vector3.UnitX;
                        rot = Xrotation;
                        break;
                    case "y":
                        axis =  Vector3.UnitY;
                        rot = Yrotation;
                        break;
                    case "z":
                        axis = Vector3.UnitZ;
                        rot = Zrotation;
                        break;
                }

                axis = Vector3.Transform(axis, m); //transform wanted axis to find local axis.
                m *= Matrix.CreateFromAxisAngle(axis, rot);
            }

            return m * Matrix.CreateTranslation(Xposition, Yposition, Zposition) *
                              Matrix.CreateTranslation(Offset);
        }

        /// <summary>
        /// Check if frame calcs are equals.
        /// </summary>
        /// <param name="b">Frame matrix information of one bone.</param>
        /// <returns>Are A and B equal.</returns>
        public static bool operator ==(FrameCalc a, FrameCalc b)
        {
            return (a.Xposition == b.Xposition &&
                        a.Yposition == b.Yposition &&
                        a.Zposition == b.Zposition &&
                        a.Xrotation == b.Xrotation &&
                        a.Yrotation == b.Yrotation &&
                        a.Zrotation == b.Zrotation &&
                        a.Offset == b.Offset &&
                        a.rotOrder[0] == b.rotOrder[0] &&
                        a.rotOrder[1] == b.rotOrder[1] &&
                        a.rotOrder[2] == b.rotOrder[2]);
        }
        public static bool operator !=(FrameCalc a, FrameCalc b)
        {
            return !(a == b);
        }
    }
}
