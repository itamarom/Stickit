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

namespace Processors
{
    public class MatrixIndexer
    {
        /// <summary>
        /// Collection of matrices before they are calculated.
        /// </summary>
        private List<FrameCalc> frmCalcs = new List<FrameCalc>();

        /// <summary>
        /// Collection of frames, where each frame is a collection indices of matrices.
        /// </summary>
        private List<List<int>> indices = new List<List<int>>();

        /// <summary>
        /// Collection of matrices.
        /// </summary>
        private Matrix[] matrices;

        /// <summary>
        /// Number of frames in animation.
        /// </summary>
        public int FrameCount
        {
            get { return this.indices.Count; }
        }

        public MatrixIndexer(List<float[]> fFrames, BVHContent bvh)
        {
            for (int f = 0; f < fFrames.Count; f++)
            {
                indices.Add(new List<int>()); //Add new frame to collection.

                for (int b = 0; b < bvh.Skeleton.BoneCount; b++)
                {
                    #region Set indices
                    BVHNode bone = bvh[b];
                    FrameCalc fc = bone.calc_fc_from_frame(fFrames[f]); //get frame calc of current matrix.
                    indices[indices.Count - 1].Add(get_index(fc, b, f)); //find index of matrix and add it to frame.
                    #endregion
                }
            }

            #region Calculate matrices from frmCalcs
            matrices = new Matrix[frmCalcs.Count];

            for (int i = 0; i < matrices.Length; i++)
            {
                matrices[i] = frmCalcs[i].calc_frame();
            }
            #endregion

            frmCalcs = null;//Allows Garbage Collector to dispose
        }

        /// <summary>
        /// Used to get a bone matrix in a certain frame.
        /// </summary>
        /// <param name="frame">A certain frame.</param>
        /// <param name="boneid">A certain bone Id.</param>
        /// <returns>The bone matrix in given frame.</returns>
        public Matrix get_matrix(int frame, int boneid)
        {
            return matrices[indices[frame][boneid]];
        }

        /// <summary>
        /// If f is not in collection, add it.
        /// </summary>
        /// <param name="f">Frame matrix information of one bone.</param>
        /// <returns>The index of the frame in collection.</returns>
        private int get_index(FrameCalc f, int boneid, int frame)
        {
            if (frame > 0)
            {
                if (frmCalcs[indices[frame - 1][boneid]] == f)
                {
                    return indices[frame - 1][boneid];
                }
            }

            frmCalcs.Add(f); //not found in collection, so add.
            return frmCalcs.Count - 1;
        }
    }
}
