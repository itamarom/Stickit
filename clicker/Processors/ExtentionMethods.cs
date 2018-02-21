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

namespace Processors
{
    public static class ExtentionMethods
    {
        public static Vector3 FromString(this Vector3 v,string s)
        {
            return StringProccesing.StringToVector3(s, ',');
        }
    }
}
