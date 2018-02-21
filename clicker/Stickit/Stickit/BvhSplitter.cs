using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Stickit
{
    public partial class BvhSplitter : Form
    {
        struct BvhPart
        {
            public int Start;
            public int End;

            public BvhPart(int start, int end)
            {
                this.Start = start;
               this.End=end;
            }
        }

        List<BvhPart> parts = new List<BvhPart>();

        public BvhSplitter()
        {
            InitializeComponent();
            numericUpDown1.Maximum = int.MaxValue;
            numericUpDown2.Maximum = int.MaxValue;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            parts.Add(new BvhPart(
                (int)numericUpDown1.Value,
                (int)numericUpDown2.Value));
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                string file = S.player.Bvh.split_bvh(
                    parts[i].Start,
                    parts[i].End);

                File.WriteAllText("C:/" + i.ToString() + ".txt",
                    file);
            }
            Process.Start("C:/");
        }

        private void BvhSplitter_Load(object sender, EventArgs e)
        {

        }
    }
}
