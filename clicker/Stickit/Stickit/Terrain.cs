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
    class Terrain
    {
        /// <summary>
        /// The vertex buffer containing the vertices.
        /// </summary>
        VertexBuffer vBuffer;

        /// <summary>
        /// The index buffer containing the indices.
        /// </summary>
        IndexBuffer iBuffer;

        /// <summary>
        /// The indices array.
        /// </summary>
        int[] indices;

        /// <summary>
        /// The vertices array.
        /// </summary>
        VPCN[] vertices;

        /// <summary>
        /// The number of vertices on X axis.
        /// </summary>
        public int Width
        {
            get { return (int)Size.X; }
            set { Size.X = value; }
        }

        /// <summary>
        /// The number of vertices on Z axis.
        /// </summary>
        public int Height
        {
            get { return (int)Size.Z; }
            set { Size.Z = value; }
        }

        /// <summary>
        /// The size of the terrain grid, on the XZ plane.
        /// </summary>
        public Vector3 Size;


        /// <summary>
        /// Initializes a terrain with a given side.
        /// </summary>
        /// <param name="width">Number of vertices on X axis.</param>
        /// <param name="height">Number of vertices on Z axis.</param>
        public Terrain(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            init_vertices();
            init_indices();

            init_buffers();
        }

        public void Draw()
        {
            S.be.World = Matrix.CreateTranslation(-new Vector3(Width, 0, Height) / 2);
            S.be.CurrentTechnique.Passes[0].Apply();
            S.gd.Indices = iBuffer;
            S.gd.SetVertexBuffer(vBuffer);
            S.gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indices.Length / 3);
        }

        #region PUBLIC
        /// <summary>
        /// Update all vertices colors according to their height.
        /// </summary>
        public void update_vertices_colors(int maxHeight)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Color = get_color_by_height(
                    vertices[i].Position.Y, maxHeight);
            }
        }

        /// <summary>
        /// Recalculate normals for all vertices.
        /// </summary>
        public void recalculate_normals()
        {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal = Vector3.Zero;

            for (int i = 0; i < indices.Length; i += 3)
            {
                calcualate_normals(indices[i], indices[i + 1], indices[i + 2]);
            }
        }

        /// <summary>
        /// Make the terrain in the shape of a circle.
        /// </summary>
        public void make_it_circle()
        {
            float radius = Width / 2;
            Vector3 center = new Vector3(Width, 0, Height) / 2;

            List<int> nIndices = new List<int>();
            List<VPCN> nVertices = new List<VPCN>();
            nIndices.AddRange(indices);
            nVertices.AddRange(vertices);

            for (int i = nIndices.Count - 1; i >= 0; i -= 3)
            {
                Vector3 a = get_xz_vector_from_index(i);
                Vector3 b = get_xz_vector_from_index(i - 1);
                Vector3 c = get_xz_vector_from_index(i - 2);

                if ((a - center).Length() > radius ||
                    (b - center).Length() > radius ||
                    (c - center).Length() > radius)
                {
                    nIndices.RemoveAt(i);
                    nIndices.RemoveAt(i - 1);
                    nIndices.RemoveAt(i - 2);
                }
            }

            for (int i = 0; i < nVertices.Count; i++)
            {
                VPCN v = nVertices[i];
                Vector3 xzPos = v.Position * (Vector3.UnitX + Vector3.UnitZ);

                float distFromCenter = (xzPos - center).Length();

                if (distFromCenter > radius - 1.5f)
                {
                    nVertices[i] = new VPCN(xzPos, v.Color, v.Normal);
                }
            }

            indices = nIndices.ToArray();
            vertices = nVertices.ToArray();

            init_buffers();
        }

        /// <summary>
        /// Apply a height function over a given rectangle.
        /// </summary>
        /// <param name="heightFunc">The height function.</param>
        /// <param name="rect">The rectangle.</param>
        public void apply_height_func_over_rect(ITerrainFunc heightFunc, Rectangle rect)
        {
            heightFunc.init();
            for (int z = rect.Y; z < rect.Height; z++)
            {
                for (int x = rect.X; x < rect.Width; x++)
                {
                    vertices[x + z * Width].Position.Y += heightFunc.get(x, z);
                }
            }
        }

        /*/// <summary>
        /// Apply a height function over a circle.
        /// </summary>
        /// <param name="heightFunc">The height function.</param>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public void apply_height_func_over_circle(ITerrainFunc heightFunc, Vector2 center, float radius)
        {
            for (float i = -radius; i <= radius; i++)
            {
                float startJ = (float)Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(i, 2));

                for (float j = -startJ; j <= startJ; j++)
                {
                    if (i + center.X >= 0 && j + center.Y >= 0 &&
                        i + center.X < Width && j + center.Y < Height) // if in bound
                    {
                        float f = 1;
                        Vector2 v = new Vector2(i, j);
                        if (v.Length() > radius - 20)
                        {
                            f = (radius - v.Length()) / 20;
                        }

                        int index = (int)(center.X + i) + (int)(center.Y + j) * Width;

                        vertices[index].Position.Y +=
                            heightFunc.getToFit(i, j, radius) * f;

                    }
                }
            }
        }*/

        /// <summary>
        /// Returns the height of the terrain in a certain coordinate.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <param name="zeroIsCenter">If center is Vector3.Zero in given coordinates.</param>
        /// <returns>Height in given coordinate.</returns>
        public float get_y(float x, float z, bool zeroIsCenter)
        {
            #region Add half size to pos if needed.
            if (zeroIsCenter)
            {
                x += Size.X / 2;
                z += Size.Z / 2;
            } 
            #endregion

            Vector3 pos = new Vector3(x, 0, z);

            if (is_in_bounds(pos.X, pos.Z, false))
            {
                Plane p = get_plane(new Vector2(pos.X, pos.Z));

                Ray r = new Ray(pos + Vector3.UnitY * 10000, -Vector3.UnitY);

                float f = r.Intersects(p).Value;
                return (r.Position + r.Direction * f).Y;
            }

            // if not in bounds, return zero.
            return 0;
        }

        /// <summary>
        /// Used to check if an XZ coordinate is in bound of terrain.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <param name="zeroIsCenter">If center is Vector3.Zero in given coordinates.</param>
        /// <returns>whether the XZ coordinate is in bound of terrain.</returns>
        public bool is_in_bounds(float x, float z, bool zeroIsCenter)
        {
            if (zeroIsCenter)
            {
                x += Size.X / 2;
                z += Size.Z / 2;
            }

            return x >= 0 && z >= 0 && x < Width && z <= Height;
        }
        #endregion

        #region PRIVATE
        /// <summary>
        /// Calculates a vertex' color by its height.
        /// </summary>
        /// <param name="h">Vertex height.</param>
        /// <returns>Vertex color.</returns>
        private Color get_color_by_height(float h, int maxHeight)
        {
            float f = h / maxHeight;

            if (f < 0.4f)
                return Color.DarkGreen;
            else if (f < 0.7f)
            {
                return Color.Lerp(Color.DarkGreen, Color.Green, (f - 0.4f) / 0.3f);
            }
            else if (f < 0.9f)
            {
                return Color.Lerp(Color.Green, Color.LightBlue, (f - 0.7f) / 0.2f);
            }
            else
                return Color.LightBlue;
        }

        /// <summary>
        /// Used to get the plane in a certain XZ coordinate on the terrain.
        /// </summary>
        /// <param name="pos">The XZ coordinate.</param>
        /// <returns>The plane.</returns>
        private Plane get_plane(Vector2 pos)
        {
            bool b;
            return get_plane(pos, out b);
        }

        /// <summary>
        /// Used to get the plane in a certain XZ coordinate on the terrain.
        /// </summary>
        /// <param name="pos">The XZ coordinate.</param>
        /// <param name="whichTriangle">Which triangle in the rectangle was used.</param>
        /// <returns>The plane.</returns>
        private Plane get_plane(Vector2 pos, out bool whichTriangle)
        {
            //here vector2 represents X Z coordinates (not X Y)
            int ix = (int)pos.X;
            int iz = (int)pos.Y;

            if (ix + 1 + (iz + 1) * Width > Width * Height)
            // if out of bounds, returns default plane.
            {
                whichTriangle = true;
                return new Plane(Vector3.UnitY, 0);
            }

            VPCN topLeft = vertices[ix + iz * Width];
            VPCN bottomRight = vertices[ix + 1 + (iz + 1) * Width];

            Vector2 tlxz = new Vector2(topLeft.Position.X,
                 topLeft.Position.Z);
            Vector2 brxz = new Vector2(bottomRight.Position.X,
                 bottomRight.Position.Z);

            Plane p;

            if ((pos - tlxz).Length() < (pos - brxz).Length())
            {
                p = new Plane(
                    vertices[ix + iz * Width].Position,
                    vertices[ix + 1 + iz * Width].Position,
                    vertices[ix + (iz + 1) * Width].Position);

                whichTriangle = true;
            }
            else
            {
                p = new Plane(
               vertices[ix + (iz + 1) * Width].Position,
               vertices[ix + 1 + iz * Width].Position,
               vertices[ix + 1 + (iz + 1) * Width].Position);

                whichTriangle = false;
            }
            return p;
        }

        /// <summary>
        /// Returns XZ vector from index of vertex.
        /// </summary>
        /// <param name="i">Vertex index.</param>
        /// <returns>Vertex XZ location.</returns>
        private Vector3 get_xz_vector_from_index(int i)
        {
            return new Vector3(vertices[indices[i]].Position.X, 0,
                vertices[indices[i]].Position.Z);
        }

        /// <summary>
        /// Init index buffer and vertex buffer.
        /// </summary>
        private void init_buffers()
        {
            vBuffer = new VertexBuffer(S.gd, VPCN.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vBuffer.SetData(vertices);

            iBuffer = new IndexBuffer(S.gd, typeof(int), indices.Length, BufferUsage.WriteOnly);
            iBuffer.SetData(indices);
        }

        /// <summary>
        /// Update the Vertex Buffer with current vertices.
        /// </summary>
        public void update_vbuffer()
        {
            vBuffer.SetData(vertices);
        }

        /// <summary>
        /// Initializes the vertices array from the height map.
        /// </summary>
        private void init_vertices()
        {
            vertices = new VPCN[Width * Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    vertices[i + j * Width].Position.X = i;
                    vertices[i + j * Width].Position.Z = j;
                }
            }
        }

        /// <summary>
        /// Initialize indices
        /// </summary>
        private void init_indices()
        {
            indices = new int[(Width - 1) * (Height - 1) * 6];

            int index = 0;

            for (int x = 0; x < Width - 1; x++)
            {
                for (int z = 0; z < Height - 1; z++)
                {
                    int topLeft = x + z * Width;
                    int topRight = x + 1 + z * Width;
                    int bottomLeft = x + (z + 1) * Width;
                    int bottomRight = x + 1 + (z + 1) * Width;

                    index = set_triangle(topLeft, topRight, bottomLeft, index);

                    index = set_triangle(bottomLeft, topRight, bottomRight, index);
                }
            }
        }

        /// <summary>
        /// set traingle and calculate normal
        /// </summary>
        /// <param name="a">first vertex index</param>
        /// <param name="b">second vertex index</param>
        /// <param name="c">third vertex index</param>
        /// <param name="nRegister">array for counting normal calculations</param>
        /// <param name="index">the current index in the indices array</param>
        /// <returns>The new index from which to continue allocating.</returns>
        private int set_triangle(int a, int b, int c, int index)
        {
            calcualate_normals(a, b, c);

            indices[index++] = a;
            indices[index++] = b;
            indices[index++] = c;

            return index;
        }

        /// <summary>
        /// Calculate normal of a triangle and add it to the vertices.
        /// </summary>
        /// <param name="a">First point vertex index.</param>
        /// <param name="b">Second point vertex index.</param>
        /// <param name="c">Third point vertex index.</param>
        private void calcualate_normals(int a, int b, int c)
        {
            Vector3 normal = Vector3.Cross(vertices[b].Position - vertices[a].Position,
                                                                vertices[c].Position - vertices[a].Position);
            normal.Normalize();

            vertices[a].Normal += normal;
            vertices[a].Normal.Normalize();

            vertices[b].Normal += normal;
            vertices[b].Normal.Normalize();

            vertices[c].Normal += normal;
            vertices[c].Normal.Normalize();
        }
        #endregion
    }
}
