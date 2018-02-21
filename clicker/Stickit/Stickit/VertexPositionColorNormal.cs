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

namespace Stickit
{
    #region VPCN Definition
    public struct VertexPositionColorNormal
    {
        public Vector3 Position;
        public Color Color;
        public Vector3 Normal;
        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
            (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
            );

        public VertexPositionColorNormal(Vector3 position, Color color)
        {
            this.Position = position;
            this.Color = color;
            this.Normal = Vector3.Zero;
        }

        public VertexPositionColorNormal(Vector3 position, Color color,Vector3 normal)
        {
            this.Position = position;
            this.Color = color;
            this.Normal = normal;
        }

        public VertexPositionColorNormal(VertexPositionColorNormal old,
                                                                      Vector3 normal)
        {
            this.Position = old.Position;
            this.Color = old.Color;
            this.Normal = Vector3.Normalize(normal + old.Normal);
        }
                

        //The offest in each vertex element represents the "distance" from the first byte in the decleration.
        //The first one is 0. the second one's distance is equal to the first one's "length",
        //and the third's distance is the second one's distance plus its length.
    }
    #endregion
}
