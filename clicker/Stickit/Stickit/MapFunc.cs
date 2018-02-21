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
    class MapFunc : ITerrainFunc
    {
        float[,] heightMap;
        public float LowestPoint { get; private set; }
        public float HighestPoint { get; private set; }

        public MapFunc(Texture2D tex, float minHeight, float maxHeight, bool lowestIsZero)
        {
            Color[] clr = new Color[tex.Width * tex.Height];
            heightMap = new float[tex.Width, tex.Height];

            tex.GetData(clr);

            float lowest = float.MaxValue;
            float highest = float.MinValue;

            for (int x = 0; x < tex.Width; x++)
            {
                for (int y = 0; y < tex.Height; y++)
                {
                    float brightness = clr[x + y * tex.Width].R / 255f; //255 = max value of rgb.
                    heightMap[x, y] = (brightness * (maxHeight - minHeight)) + minHeight;

                    if (lowestIsZero && heightMap[x, y] < lowest)
                        lowest = heightMap[x, y];
                    if (heightMap[x, y] > highest)
                        highest = heightMap[x, y];
                }
            }

            if (lowestIsZero)
            {
                for (int x = 0; x < tex.Width; x++)
                {
                    for (int y = 0; y < tex.Height; y++)
                    {
                        heightMap[x, y] -= lowest;
                    }
                }

                highest -= lowest;
                lowest = 0;
            }


            this.HighestPoint = highest;
            this.LowestPoint = lowest;
        }

        public void init()
        { }

        public float get(float x, float z)
        {
            x = Math.Max(0, Math.Min(heightMap.GetLength(0) - 1, x)); //make sure it doesn't go out of bounds.
            z = Math.Max(0, Math.Min(heightMap.GetLength(1) - 1, z));
            return heightMap[(int)x, (int)z];
        }
    }
}
