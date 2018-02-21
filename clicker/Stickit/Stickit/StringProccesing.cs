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
    public static class StringProccesing
    {
        /// <summary>
        /// Load file content from Content directory
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File content</returns>
        public static string GetFileContents(string path)
        {
            Stream s = TitleContainer.OpenStream("Content/"+path);
            StreamReader sr = new StreamReader(s);
            string str = sr.ReadToEnd();
            sr.Close();
            s.Close();
            return str;
        }
        /// <summary>
        /// Get the index of closing bracket
        /// </summary>
        /// <param name="s">string to look at</param>
        /// <param name="startIndex">index of openning bracket</param>
        /// <returns>index of closing bracket</returns>
        public static int GetIndexOfClosing(string s, int startIndex)
        {
            return GetIndexOfClosing(s, '{', '}', startIndex);
        }

        /// <summary>
        /// Get the index of closing bracket
        /// </summary>
        /// <param name="s">string to look at</param>
        /// <param name="open">bracket open char</param>
        /// <param name="close">bracket close char</param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int GetIndexOfClosing(string s, char open, char close, int startIndex)
        {
            if (s[startIndex] != open)
                throw new Exception("Start index char is not open char.");

            int track = 1;

            for (int i = startIndex + 1; i < s.Length; i++)
            {
                if (s[i] == open)
                    track++;
                else if (s[i] == close)
                    track--;

                if (track == 0)
                    return i;
            }

            return -1;
        }
        /// <summary>
        /// analyze seqeunce of floats(offset, frames etc.)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector3 StringToVector3(string str)
        {
            float[] arr = StringToFloats(str);
            return new Vector3(arr[0], arr[1], arr[2]);
        }
        public static Vector3 StringToVector3(string str,char c)
        {
            float[] arr = StringToFloats(str,c);
            return new Vector3(arr[0], arr[1], arr[2]);
        }
        /// <summary>
        /// analyze seqeunce of floats(offset, frames etc.)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float[] StringToFloats(string str)
        {
            return StringToFloats(str, ' ');

        }
        public static float[] StringToFloats(string str, char split)
        {
            str = CleanUp(str);
            str = str.Replace("  ", " ");
            string[] arr = str.Split(split);
            List<float> nums = new List<float>();

            float f = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] != string.Empty && float.TryParse(arr[i], out f))
                    nums.Add(float.Parse(arr[i]));

            return nums.ToArray();
        }

        /// <summary>
        /// remove unwanted chars from string
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>returns string without unwanted chars</returns>
        public static string CleanUp(string str)
        {
            string n = str.Replace("\t", "").
                           Replace("\n", "").
                           Replace("\r", "").
                           Replace(BVHNodeString.NewLine, "");

            n = RemovePadding(n);

            return n;
        }

        /// <summary>
        /// remove padding in the start and the end of the string
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>returns new string without padding</returns>
        public static string RemovePadding(string str)
        {
            string n = str;

            while (n.StartsWith(" "))
                n = n.Remove(0, 1);

            while (n.EndsWith(" "))
                n = n.Remove(n.Length - 1);

            return n;
        }

        public static Matrix StringToMatrix(string str)
        {
            float[] arr = StringToFloats(str, ',');

            return new Matrix(
                arr[0], arr[1], arr[2], arr[3],
                arr[4],arr[5], arr[6], arr[7],
                arr[8], arr[9], arr[10], arr[11],
                arr[12], arr[13], arr[14],arr[15]);
               
        }
    }
}
