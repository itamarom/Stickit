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
using FrameCollection = System.Collections.Generic.List<System.Collections.Generic.List<Microsoft.Xna.Framework.Matrix?>>;


namespace Processors
{
    public enum RootMovement
    {
        SetYOfRootAsFirstFrame,//Y of root will always stay as it was in the first frame.
        KeepOnlyY,//XZ of root will be deleted.
        Delete,//Delete XYZ of root.
        ZeroRootXZ//Set all root XZ values to zero.
    }
    /// <summary>
    /// represents BVH file
    /// </summary>
    public class BVHContent
    {
        /// <summary>
        /// The skeleton used that fits this BVH.
        /// </summary>
        public Skeleton Skeleton { get; set; }

        /// <summary>
        /// The delay between each frame.
        /// </summary>
        public float FrameTime { get; set; }



        /// <summary>
        /// A collection which enables access to bones by name.
        /// </summary>
        private Dictionary<string, BVHNode> BonesFromString = new Dictionary<string, BVHNode>();

        /// <summary>
        /// A collection which enables access to bones by ID.
        /// </summary>
        private Dictionary<int, BVHNode> BonesFromId = new Dictionary<int, BVHNode>();

        /// <summary>
        /// Returns node by given Bone Name.
        /// </summary>
        /// <param name="BoneName">Name of wanted bone.</param>
        public BVHNode this[string BoneName]
        {
            get
            {
                return BonesFromString[BoneName];
            }
        }

        /// <summary>
        /// Returns node by given Bone ID.
        /// </summary>
        /// <param name="BoneName">ID of wanted bone.</param>
        public BVHNode this[int BoneId]
        {
            get
            {
                return BonesFromId[BoneId];
            }
        }

        public MatrixIndexer MXI;
        private string filecontent;

        public BVHContent(string filecontent, Skeleton skl, RootMovement rootMovement)
            : this(new BVHContentString(filecontent, skl == null), skl, 
            new List<RootMovement>()
            {
            rootMovement})
        {

        }
        /// <summary>
        /// Initialize BVH Content.
        /// </summary>
        /// <param name="filecontent">BVH file content</param>
        public BVHContent(string filecontent, Skeleton skl,List<RootMovement> rootMovement)
            : this(new BVHContentString(filecontent, skl == null), skl, rootMovement)
        {
            this.filecontent = filecontent;
        }

        /// <summary>
        /// Initialize BVH Content.
        /// </summary>
        /// <param name="bvh">A semi-processed BVHContent</param>
        public BVHContent(BVHContentString bvh, Skeleton skeleton,List<RootMovement> rootMovement)
        {
            #region Initialize skeleton.
            if (skeleton == null)
            {
                Skeleton = new Skeleton();
                Skeleton.Root = new BVHNode(bvh.Root, this, -1);
            }
            else
            {
                this.Skeleton = skeleton;
            }
            #endregion

            #region Clean and set FrameTime
            this.FrameTime = float.Parse(StringProccesing.CleanUp(bvh.FrameTime));
            #endregion

            #region Organize bones ID by level
            organize_bones_id_by_level();
            #endregion

            #region Clean frames and insert to list of float arrays
            List<float[]> fFrames = string_to_fFloat(bvh.Frames);
            #endregion

            #region Delete root movement
            for (int i = 0; i < rootMovement.Count; i++)
            {
                RootMovement rm = rootMovement[i];

                if (rm == RootMovement.Delete)
                    delete_root_movement(fFrames, false);
                else if (rm == RootMovement.KeepOnlyY)
                    delete_root_movement(fFrames, true);
                else if (rm == RootMovement.ZeroRootXZ)
                    center_by_root_xyz(fFrames);
                else if (rm == RootMovement.SetYOfRootAsFirstFrame)
                    set_y_of_root_as_first_frame(fFrames);
            }
            #endregion

            #region Calculate matrices and their indices.
            MXI = new MatrixIndexer(fFrames, this);
            #endregion
        }

      
        private List<float[]> string_to_fFloat(string str)
        {
            List<float[]> fFrames = new List<float[]>(); //float frames

            string[] lines = str.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (StringProccesing.CleanUp(lines[i]) !=
                    string.Empty)
                    fFrames.Add(
                        StringProccesing.StringToFloats(lines[i], ' '));
            }

            return fFrames;
        }

        public void Keep_Y_Of_Root_Same_But_Still_Move_It(List<float[]> coolFrames)
        {  
          
        }
        public void set_y_of_root_as_first_frame(List<float[]> coolFrames)
        {
            for (int i = 1; i < coolFrames.Count; i++)
            {
                coolFrames[i][1] = coolFrames[0][1];
            }
        }

        public void center_by_root_xyz(List<float[]> coolFrames)
        {
            Vector3 d = new Vector3(coolFrames[0][0], 0, coolFrames[0][2]);
            d *= -1;

            for (int i = 0; i < coolFrames.Count; i++)
            {
                coolFrames[i][0] += d.X;
                coolFrames[i][2] += d.Z;
            }
        }

        public void delete_root_movement(List<float[]> coolFrames, bool keepY)
        {
            for (int i = 0; i < coolFrames.Count; i++)
            {
                coolFrames[i][0] = 0;
                if (keepY == false)
                    coolFrames[i][1] = 0;
                coolFrames[i][2] = 0;
            }
        }

        private void organize_bones_id_by_level()
        {
            List<List<BVHNode>> levels =
                new List<List<BVHNode>>();

            feed_level_list(Skeleton.Root, levels);

            int id = 0;

            #region Go over all levels.
            for (int l = 0; l < levels.Count; l++) //each level
            {
                #region Go over all bones in level.
                for (int n = 0; n < levels[l].Count; n++) //each node in level
                {
                    BVHNode node = levels[l][n];

                    #region Set bone ID.
                    node.Id = id;
                    #endregion

                    #region Add bone to BonesFromString collection.
                    BonesFromString.Add(node.Name, node);
                    #endregion

                    #region Add bone to BonesFromId collection.
                    BonesFromId.Add(node.Id, node);
                    #endregion

                    #region Raise Id count by one.
                    id++;
                    #endregion
                }
                #endregion
            }
            #endregion
        }

        private void feed_level_list(BVHNode start, List<List<BVHNode>> levels)
        {
            #region If reached a new level, create it.
            if (start.Level >= levels.Count)
                levels.Add(new List<BVHNode>());
            #endregion

            #region Add start bone to a collection according to its level.
            levels[start.Level].Add(start);
            #endregion

            #region Go over bone children, set their Parent value and call the function recursively.
            for (int i = 0; i < start.Children.Count; i++)
            {
                start.Children[i].Parent = start;

                feed_level_list(start.Children[i],
                    levels);
            }
            #endregion
        }

        public string split_bvh(int startFrame, int endFrame)
        {
            string her = filecontent.Substring(0,
                filecontent.LastIndexOf("MOTION") + "MOTION".Length);

            her += Environment.NewLine + "Frames: " + (endFrame - startFrame).ToString();
            her += Environment.NewLine;
            her += "Frame Time: .0083333";
            her += Environment.NewLine;

            int firstLine = filecontent.IndexOf('\n',
                filecontent.IndexOf("Frame Time"));

            int startChar = firstLine;
            int count = 0;

            while (count < startFrame)
            {
                startChar = filecontent.IndexOf('\n',
                    startChar + 1);

                count++;
            }

            int endChar = startChar;
            //count = 0;

            while (count <= endFrame)
            {
                endChar = filecontent.IndexOf('\n',
                    endChar + 1);

                count++;
            }

            her += filecontent.Substring(startChar, endChar - startChar);

            return her;
        }


    }

    /// <summary>
    /// represents a bone
    /// </summary>
    public class BVHNode
    {
        const string NewLine = "\n";

        public int Id { get; set; }
        public string Name { get; private set; }
        public Vector3 Offset { get; private set; }
        public BoneChannel[] Channels { get; set; }
        public BVHNode Parent { get; set; }

        public Vector3? EndSiteOffset { get; set; }
        public List<BVHNode> Children { get; set; }
        public int Level { get; private set; }

        /// <summary>
        /// init data
        /// </summary>
        /// <param name="bvh">bone</param>
        /// <param name="content">content</param>
        public BVHNode(BVHNodeString bvh, BVHContent content, int ParentLevel)
        {
            #region Set fields value.
            this.Level = ++ParentLevel;
            this.Name = StringProccesing.CleanUp(bvh.Name);
            this.Id = content.Skeleton.BoneCount;
            #endregion

            content.Skeleton.BoneCount++;

            #region Calculate offset and channels.
            this.Offset = StringProccesing.StringToVector3(bvh.Offset.Replace("OFFSET", ""));

            this.Channels = StringToChannels(
                bvh.Channels.Remove(bvh.Channels.IndexOf("CHANNELS"),
                "CHANNELS".Length + 2));
            #endregion

            #region Add this bones channel to content channel collection.
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i].pifl = content.Skeleton.BoneChannels.Count;
                content.Skeleton.BoneChannels.Add(Channels[i]);
            }
            #endregion

            #region Set EndsiteOffset if needed.
            if (bvh.EndSiteOffset != null)
                EndSiteOffset = StringProccesing.StringToVector3(bvh.EndSiteOffset.Replace("OFFSET", ""));
            #endregion

            #region Initialize children and add them from BVHNodeString.
            Children = new List<BVHNode>();

            for (int i = 0; i < bvh.Children.Count; i++)
            {
                Children.Add(new BVHNode(bvh.Children[i], content, Level));
            }
            #endregion

            #region If bone has children, set EndSiteOffset as Offset of first one.
            if (EndSiteOffset == null)
                EndSiteOffset = Children[0].Offset;
            #endregion
        }

        /// <summary>
        /// analyze channels from string
        /// </summary>
        /// <param name="str">channels as string</param>
        /// <returns>array of BoneChannel</returns>
        private BoneChannel[] StringToChannels(string str)
        {
            str = StringProccesing.CleanUp(str);
            str = str.Replace("  ", " ");
            string[] arr = str.Split(' ');
            BoneChannel[] chans = new BoneChannel[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                chans[i] = new BoneChannel(this, arr[i]);

            return chans;
        }

        public Matrix calc_abs_mx_from_frame(MatrixIndexer frames, int frame, Matrix? parent)
        {
            Matrix m = frames.get_matrix(frame, Id);

            if (parent.HasValue)
                m *= parent.Value;

            return m;
        }

        /// <summary>
        /// Calculates a FrameCalc from a given frame.
        /// </summary>
        /// <param name="arr">Frame values.</param>
        /// <returns>FrameCalc of this node.</returns>
        public FrameCalc calc_fc_from_frame(float[] arr)
        {
            FrameCalc frame = new FrameCalc(this.Name, this.Offset);

            for (int i = 0; i < Channels.Length; i++)
            {
                BoneChannel bc = Channels[i];
                frame.update_channel_values(bc.channel, arr[bc.pifl]);
            }

            return frame;
        }

    }

    public class Skeleton
    {
        /// <summary>
        /// First bone in skeleton.
        /// </summary>
        public BVHNode Root;

        /// <summary>
        /// Bone channels in each frame.
        /// </summary>
        public List<BoneChannel> BoneChannels;

        /// <summary>
        /// Number of bones in skeleton.
        /// </summary>
        public int BoneCount
        {
            get;
            set;
        }

        public Matrix[] CylinderScaleOffset
        {
            get;
            set;
        }

        /// <summary>
        /// Initialize skeleton with empty values.
        /// </summary>
        public Skeleton()
        {
            this.BoneChannels = new List<BoneChannel>();
        }

        /// <summary>
        ///  Initialize skeleton with given values.
        /// </summary>
        /// <param name="Root">First bone in skeleton.</param>
        /// <param name="BoneChannels">Bone channels in each frame</param>
        public Skeleton(BVHNode Root, List<BoneChannel> BoneChannels)
        {
            this.Root = Root;
            this.BoneChannels = BoneChannels;
        }

        public void calc_cylinder_matrices(BVHContent bvh)
        {
            CylinderScaleOffset = new Matrix[bvh.Skeleton.BoneCount];

            for (int i = 0; i < BoneCount; i++)
            {
                #region point to axis angle
                Vector3 offset = bvh[i].EndSiteOffset.Value;
                Vector3 normal = Vector3.Normalize(offset);
                float length = offset.Length();

                float angle =
               (float)Math.Atan2(
               (double)new Vector2(normal.X, normal.Z).Length(),
               normal.Y);

                Vector3 axis = new Vector3(normal.Z, 0,
                    -normal.X);

                axis.Normalize();

                if (float.IsNaN(axis.X) || float.IsNaN(axis.Y)
                    || float.IsNaN(axis.Z))
                {
                    axis = Vector3.UnitX;
                    angle = MathHelper.Pi;
                }
                #endregion


                if (bvh[i].Name == "Head")
                {
                    CylinderScaleOffset[i] =
                         Matrix.CreateScale(length) *
                         Matrix.CreateScale(new Vector3(3, 0.4f, 3)) *
                         Matrix.CreateFromAxisAngle(axis, angle);
                }
                else
                {
                    CylinderScaleOffset[i] =
                        Matrix.CreateScale(length) *
                        Matrix.CreateFromAxisAngle(axis, angle);
                }
            }
        }
    }

    /// <summary>
    /// represents a bone channel
    /// </summary>
    public struct BoneChannel
    {
        public string channel;
        public BVHNode node;
        /// <summary>
        /// Place in frame line.
        /// </summary>
        public int pifl;

        /// <summary>
        /// Initialize fields.
        /// </summary>
        /// <param name="node">Bone.</param>
        /// <param name="channel">Channel to update (xPosition, yRotation etc)</param>
        /// <param name="pifl">Place in frame line.</param>
        public BoneChannel(BVHNode node, string channel)
        {
            this.node = node;
            this.channel = channel;
            pifl = -1;
        }
    }
}
