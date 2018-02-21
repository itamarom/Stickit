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
    class Axis
    {
        BasicEffect e;

        private VertexPositionColor[] vertices = new VertexPositionColor[3 * 2];
        public VertexBuffer vBuffer;
        public int Length;
        public Axis(GraphicsDevice gd, float size, Matrix view,
            Matrix projection)
        {
            int h = 0;

            vertices[0] = new VertexPositionColor(Vector3.UnitY * h, Color.Red);
            vertices[1] = new VertexPositionColor(
                Vector3.UnitX * size
                + Vector3.UnitY * h, Color.Red);

            vertices[2] = new VertexPositionColor(Vector3.Zero, Color.Yellow);
            vertices[3] = new VertexPositionColor(Vector3.UnitY * size, Color.Yellow);

            vertices[4] = new VertexPositionColor(Vector3.UnitY * h, Color.Blue);
            vertices[5] = new VertexPositionColor(Vector3.UnitZ * size +
                Vector3.UnitY * h, Color.Blue);
            vBuffer = new VertexBuffer(gd, VertexPositionColor.VertexDeclaration,
                vertices.Length, BufferUsage.WriteOnly);
            vBuffer.SetData(vertices);
            Length = vertices.Length;

            e = new BasicEffect(gd);
            e.View = view;
            e.Projection = projection;
            e.World = Matrix.Identity;
            e.VertexColorEnabled = true;
        }

        public void Draw(GraphicsDevice gd,Matrix view)
        {
            e.View = view;
            e.Techniques[0].Passes[0].Apply();
            gd.SetVertexBuffer(vBuffer);

            gd.DrawPrimitives(PrimitiveType.LineList,
                0, vertices.Length / 2);
        }
    }
}
