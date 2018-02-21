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
using VPC = Microsoft.Xna.Framework.Graphics.VertexPositionColor;
using VPNT = Microsoft.Xna.Framework.Graphics.VertexPositionNormalTexture;
using VPCN = Stickit.VertexPositionColorNormal;

namespace Stickit
{
    class Sea
    {
        VertexBuffer vBuffer;
        VertexPositionNormalTexture[] vertices;
        float scale = 2000;
        Texture2D[] tex;
        int frame = 0, interval = 0;

        public Sea(params Texture2D[] tex)
        {
            this.tex = tex;
            vertices = new VertexPositionNormalTexture[6];

            #region init rectangle
            vertices[0] = new VertexPositionNormalTexture(new Vector3(0, 0, 0),
                        Vector3.Up, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(1, 0, 0),
                Vector3.Up, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(1, 0, 1),
                Vector3.Up, new Vector2(1, 1));

            vertices[3] = new VertexPositionNormalTexture(new Vector3(1, 0, 1),
                Vector3.Up, new Vector2(1, 1));
            vertices[4] = new VertexPositionNormalTexture(new Vector3(0, 0, 1),
                Vector3.Up, new Vector2(0, 1));
            vertices[5] = new VertexPositionNormalTexture(new Vector3(0, 0, 0),
                Vector3.Up, new Vector2(0, 0));
            #endregion

            vBuffer = new VertexBuffer(S.gd, VertexPositionNormalTexture.VertexDeclaration,
                vertices.Length, BufferUsage.WriteOnly);
            vBuffer.SetData(vertices);

            Game1.UpdateEvent += new UpdateDelegate(Update);
            Game1.Draw3DEvent += new DrawDelegate(Draw);
        }

        public void Update(GameTime gameTime)
        {
            if (++interval >= 6)
            {
                interval = 0;
                frame = ++frame % tex.Length;
            }
        }

        public void Draw()
        {

            S.gd.SetVertexBuffer(vBuffer);
            S.be.AmbientLightColor = new Vector3(0, 0, 0);
            S.be.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(-scale / 2, 0, -scale / 2);
            S.be.TextureEnabled = true;
            S.be.LightingEnabled = false;
            S.be.Texture = tex[frame];
            S.be.VertexColorEnabled = false;
            S.be.CurrentTechnique.Passes[0].Apply();
            S.gd.DrawPrimitives(PrimitiveType.TriangleList, 0, vertices.Length / 3);

            S.be.TextureEnabled = false;
            S.be.VertexColorEnabled = true;
        }
    }
}
