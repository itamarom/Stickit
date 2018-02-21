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
using VPCN = Stickit.VertexPositionColorNormal;

namespace Stickit
{
    class NCylinder
    {
        public float RadiusA { get; private set; }
        public float RadiusB { get; private set; }
        public float Length { get; private set; }

        public VertexBuffer vBuffer { get; set; }
        public IndexBuffer iBuffer { get; set; }

        public int vCount { get; set; }
        public int iCount { get; set; }

        public NCylinder(float radiusA, float radiusB, float length, int parts, Color clr)
        {
            #region Init lists
            List<VPCN> vertices = new List<VPCN>();
            List<int> indices = new List<int>();
            #endregion

            vertices.AddRange(make_circle(0, radiusA, parts, clr));
            vertices.AddRange(make_circle(length, radiusB, parts, clr));

            #region Create bottom circle indices
            for (int i = 1; i < parts; i++)
            {
                indices.Add(0);
                indices.Add(i);
                indices.Add(i + 1);
            }
                
            #region Set last
            indices.Add(0);
            indices.Add(parts);
            indices.Add(1);
            #endregion
            #endregion

            #region Create upper circle indices
            for (int i = parts; i > 0; i--)
            {
                indices.Add(vertices.Count / 2);
                indices.Add(i + vertices.Count / 2);
                indices.Add(i - 1 + vertices.Count / 2);
            }

            #region Set last
            indices.Add(vertices.Count / 2);
            indices.Add(1 + vertices.Count / 2);
            indices.Add(parts + vertices.Count / 2);
            #endregion
            #endregion

            #region Init side indices
            for (int i = 1; i < parts; i++)
            {
                add_rect(indices, vertices, i, parts);
            }

            #region Add last
            add_rect(indices, vertices, parts, 1, parts * 2 + 1, parts + 2);     
            #endregion

            #endregion

            #region Init and input to buffers
            vBuffer = new VertexBuffer(S.gd, VPCN.VertexDeclaration,
        vertices.Count, BufferUsage.WriteOnly);
            vBuffer.SetData<VPCN>(vertices.ToArray());

            iBuffer = new IndexBuffer(S.gd, IndexElementSize.ThirtyTwoBits,
                 indices.Count, BufferUsage.WriteOnly);
            iBuffer.SetData(indices.ToArray());

            vCount = vertices.Count;
            iCount = indices.Count; 
            #endregion

            this.RadiusA = radiusA;
            this.RadiusB = radiusB;
            this.Length = length;
        }

        private void add_normal_to_vertices(List<VPCN> vertices, int indexA, int indexB, int indexC)
        {
            Vector3 normal =
                Vector3.Cross(vertices[indexB].Position -
                                       vertices[indexA].Position,
                                       vertices[indexC].Position -
                                       vertices[indexA].Position);

            normal.Normalize();

            vertices[indexA] = new VPCN(vertices[indexA], normal);
            vertices[indexB] = new VPCN(vertices[indexB], normal);
            vertices[indexC] = new VPCN(vertices[indexC], normal);
        }

        private void add_rect(List<int> indices, List<VPCN> vertices, int i, int parts)
        {
            #region Calculate four indices of rectangle
            int bottomA = i;
            int bottomB = i + 1;
            int topA = i + parts + 1;
            int topB = i + 1 + parts + 1;
            #endregion

            add_rect(indices, vertices, bottomA, bottomB, topA, topB);
        }
        private void add_rect(List<int> indices, List<VPCN> vertices, int bottomA, int bottomB, int topA, int topB)
        {
            #region Add triangle A
            indices.Add(bottomA);
            indices.Add(topA);
            indices.Add(bottomB);

            add_normal_to_vertices(vertices, bottomA, topA, bottomB);
            #endregion

            #region Add triangle B
            indices.Add(topA);
            indices.Add(topB);
            indices.Add(bottomB);

            add_normal_to_vertices(vertices, topA, topB, bottomB);
            #endregion
        }

        private List<VPCN> make_circle(float height, float radius, int parts, Color clr)
        {
            List<VPCN> vertices = new List<VPCN>();

            #region Add center points
            vertices.Add(new VPCN(Vector3.UnitY * height, clr));
            #endregion

            #region Create circles
            for (int i = 0; i < parts; i++)
            {
                float angle = ((float)i / parts) * MathHelper.TwoPi;

                vertices.Add(
                    new VPCN(new Vector3(
                        (float)Math.Sin(angle) * radius, height, (float)Math.Cos(angle) * radius),
                        clr));
            }
            #endregion

            return vertices;
        }
    }
}
