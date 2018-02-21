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
    /// <summary>
    /// represents a semi proccessed BVH file as string
    /// </summary>
    public class BVHContentString
    {
        string file;

        public BVHNodeString Root { get; set; }
        public string FrameTime { get; set; }
        public string Frames { get; set; }
        public string FramesCount { get; set; }

        /// <summary>
        /// Load all data from bvh file.
        /// </summary>
        /// <param name="filecontent">bvh file content.</param>
        public BVHContentString(string filecontent, bool analizeRoot)
        {
            this.file = filecontent;

            if (analizeRoot)
            {
                int start = file.IndexOf("ROOT") + 5;
                string name = file.Substring(start, file.IndexOf('{') - start);
                string brackets = file.Substring(file.IndexOf('{'),
                    StringProccesing.GetIndexOfClosing(file, file.IndexOf('{')) - file.IndexOf('{'));

                Root = new BVHNodeString(name, brackets);
            }

            string motion = file.Substring(file.IndexOf("MOTION"));

            int framesIndex = motion.IndexOf("Frames");
            int frameTimeIndex = motion.IndexOf("Frame Time");

            FramesCount = motion.Substring(framesIndex + "Frames:".Length,
                frameTimeIndex - 1 - framesIndex - "Frames".Length);

            frameTimeIndex += "Frame Time: ".Length;

            int frameTimeEnd = frameTimeIndex;

            while (motion[frameTimeEnd] == '.' ||
               char.IsNumber(motion[frameTimeEnd]))
            {
                frameTimeEnd++;
            }

            frameTimeEnd--;

            FrameTime = motion.Substring(frameTimeIndex,
                frameTimeEnd - frameTimeIndex);

            Frames = motion.Substring(
                frameTimeEnd + 1, motion.Length - frameTimeEnd - 1);
        }
    }

    /// <summary>
    /// represents a semi proccess bone as string
    /// </summary>
    public class BVHNodeString
    {
        public const string NewLine = "\n";

        public string Name { get; set; }
        public string Offset { get; set; }
        public string Channels { get; set; }

        public string EndSiteOffset { get; set; }

        public List<BVHNodeString> Children { get; set; }
        #region Unused Codes

        ///// <summary>
        ///// init the data
        ///// </summary>
        //public BVHNodeString()
        //{
        //    this.Channels = this.Offset = this.Name = string.Empty;

        //    this.Children = new List<BVHNodeString>();

        //}

        ///// <summary>
        ///// init data
        ///// </summary>
        ///// <param name="Name">bone name</param>
        ///// <param name="Offset">bone offset</param>
        ///// <param name="Channels">bone channels</param>
        //public BVHNodeString(string Name, string Offset, string Channels)
        //{
        //    this.Name = Name;
        //    this.Offset = Offset;
        //    this.Channels = Channels;

        //    this.Children = new List<BVHNodeString>();
        //}
        
        #endregion

        /// <summary>
        /// init data
        /// </summary>
        /// <param name="Name">bone name</param>
        /// <param name="brackets">brackets content</param>
        public BVHNodeString(string Name, string brackets)
        {
            this.Name = Name;

            int offsetIndex = brackets.IndexOf("OFFSET");
            int channelsIndex = brackets.IndexOf("CHANNELS");

            this.Offset = brackets.Substring(offsetIndex,
                brackets.IndexOf(NewLine, offsetIndex) - offsetIndex);

            this.Channels = brackets.Substring(channelsIndex,
             brackets.IndexOf(NewLine, channelsIndex) - channelsIndex);


            Children = new List<BVHNodeString>();

            ///////////////////////////////////////////////////////////////////

            int where = brackets.IndexOf("JOINT");

            if (where != -1)
            {
                while (where != -1)
                {
                    int nameStart = brackets.IndexOf(' ', where) + 1;

                    string subname = brackets.Substring(nameStart,
                        brackets.IndexOf('{', where) - nameStart);

                    int subbrackStart = brackets.IndexOf('{', where);
                    int subbrackEnd = StringProccesing.GetIndexOfClosing(
                        brackets, subbrackStart);

                    string subbrackets = brackets.Substring(subbrackStart, subbrackEnd - subbrackStart);

                    Children.Add(new BVHNodeString(subname, subbrackets));

                    brackets = brackets.Remove(where,
                        subbrackEnd - where);

                    where = brackets.IndexOf("JOINT");
                }
            }
            else
            {
                int indexOfBrackets = brackets.IndexOf('{',
                    brackets.IndexOf("End Site"));

                int endOfEndSize = StringProccesing.GetIndexOfClosing(
                    brackets, '{', '}', indexOfBrackets);

                EndSiteOffset = brackets.Substring(indexOfBrackets, endOfEndSize - indexOfBrackets);
            }

            // if (brackets.IndexOf(

        }
    }
}
