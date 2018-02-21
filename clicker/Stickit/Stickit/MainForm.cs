using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Threading;
using XmlLib;
using System.IO;
using Point = System.Drawing.Point;
using System.Collections;
using Processors;
using System.Diagnostics;

namespace Stickit
{
    public partial class MainForm : Form
    {
        Game1 game;
        public static List<RootMovement> saveRootMovement = new List<RootMovement>();
        public MainForm()
        {
            InitializeComponent();
            type.SelectedIndex = 0;
        }

        #region FORM EVENTS
        //[STAThreadAttribute]
        private void MainForm_Load(object sender, EventArgs e)
        {
            new ChooseRootMovement().ShowDialog(); ;

            OpenFileDialog of = new OpenFileDialog();
            of.ShowDialog();
            //game.Activated += new EventHandler<EventArgs>(game_Activated);
            Game1.filename = of.FileName;
            Game1.ParentHandle = panel1.Handle;
            Thread t = new Thread(new ThreadStart(runit));
            t.Start();

            Game1.UpdateEvent += new UpdateDelegate(Game1_UpdateEvent);
            update_static_win_loc();
            //pictureBox1.Location = new Point(
            //    frameTb.Location.X, pictureBox1.Location.Y);
            actionGraphPb.Width = frameTb.Width;
        }

        void Game1_UpdateEvent(GameTime gameTime)
        {
            int i = S.player.Frame;
            if (frameTb.InvokeRequired)
            {
                frameTb.Invoke(
                     (MethodInvoker)delegate
                {
                    frameTb.Value = i;
                    frameNum.Value = i;
                });
            }

        }

        //[STAThreadAttribute]
        public void runit()
        {
            game = new Game1(this.panel1.Width, this.panel1.Height);
            game.Activated += new EventHandler<EventArgs>(game_Activated);
            game.Run();
        }

        void game_Activated(object sender, EventArgs e)
        {
            int i = S.player.Bvh.MXI.FrameCount - 1;
            if (frameTb.InvokeRequired)
            {
                frameTb.Invoke(
                     (MethodInvoker)delegate
                {
                    frameTb.Minimum = 0;
                    frameTb.Maximum = i;
                    frameNum.Maximum = i;
                    skillFromFrame.Maximum = i;
                    skillToFrame.Maximum = i;
                });
            }
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            update_static_win_loc();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.Activated -= game_Activated;
            Game1.UpdateEvent -= Game1_UpdateEvent;
            game.Exit();
        }
        #endregion

        #region SERVICE FUNCTIONS
        private void update_static_win_loc()
        {
            S.Winform.X = Location.X + panel1.Location.X;
            S.Winform.Y = Location.Y + panel1.Location.Y;
        }

        private void update_label_text()
        {
            if (actions.SelectedItem != null)
            {
                update_label_text(((BVHAction)actions.SelectedItem).ToXml());
            }
        }

        private void update_label_text(string text)
        {
            xmlOutput.Text = text;
        }

        private void update_trackbar_rects()
        {
            actionGraphPb.Invalidate();
        }

        private BVHAction[] get_actions_by_type(string type, BVHAction[] all)
        {
            return all.Where(new Func<BVHAction, bool>((BVHAction b) =>
             {
                 return b.Type.ToLower() == type;
             })).ToArray();
        }
        #endregion

        #region ATTACK SETTINGS
        private void setAttackDir_Click(object sender, EventArgs e)
        {
            BVHAction action =
                    (BVHAction)actions.SelectedItem;

            if (S.player.Frame >= action.StartFrame &&
                S.player.Frame <= action.EndFrame)
            {
                Vector3 dir =
                    S.camera.usedCamPos -
                    S.camera.focus.Position;
                dir.Normalize();

                BVHAttackAction attackAction;

                if (action is BVHAttackAction)
                {
                    attackAction = (BVHAttackAction)action;
                }
                else
                {
                    attackAction = new BVHAttackAction(action);
                }

                if (attackAction.attackDrcs.ContainsKey(
                    S.player.Frame))
                {
                    attackAction.attackDrcs.Remove(S.player.Frame);

                    S.attackDirMatrix = Matrix.CreateTranslation(Vector3.One * float.PositiveInfinity);
                }
                else
                {
                    attackAction.attackDrcs.Add(
                        S.player.Frame, dir);

                    S.attackDirMatrix = S.point_to_axis_angle(
                        attackAction.attackDrcs[S.player.Frame]);
                }
                if (!(action is BVHAttackAction))
                {
                    actions.Items.Remove(action);
                    actions.Items.Add(attackAction);
                }

                update_label_text(attackAction.ToXml());

            }
            else
            {
                MessageBox.Show("You are not in range of action.");
            }

            update_trackbar_rects();
        }

        private void setAttackBtn_Click(object sender, EventArgs e)
        {
            BVHAttackAction attack = new BVHAttackAction(
                (BVHAction)actions.SelectedItem);
            actions.Items.Insert(
                actions.SelectedIndex,
                attack);
            actions.Items.Remove(actions.SelectedItem);

            actions.SelectedItem = attack;

            actions_SelectedIndexChanged(sender, e);
            update_trackbar_rects();
        }

        private void markingBones_Click(object sender, EventArgs e)
        {
            if (markingBones.Text.StartsWith("Start"))
                markingBones.Text = "Stop marking bones (and update xml label)";
            else
            {
                markingBones.Text = "Start marking bones";
                update_label_text();
            }
            S.isMarkingBones = markingBones.Text.StartsWith("Stop");

        }
        #endregion

        #region PLAYER CONTROL
        private void playBtn_Click(object sender, EventArgs e)
        {
            S.player.animate = !S.player.animate;
            if (S.player.animate)
                playBtn.Text = "Stop";
            else
                playBtn.Text = "Play";
        }

        private void saveToClipboard_Click(object sender, EventArgs e)
        {

        }

        private void frameTb_ValueChanged(object sender, EventArgs e)
        {

            S.player.Frame = (int)frameTb.Value;
            S.player.run_frame();
        }

        private void actions_SelectedIndexChanged(object sender, EventArgs e)
        {
            S.action = (BVHAction)actions.SelectedItem;
            update_label_text();

            Game1.chosenBones = new List<int>();

            targetGb.Enabled = false;

            fplTxt.Enabled =
            skillsGb.Enabled =
            tabPage4.Enabled =
            setAttackBtn.Enabled =
            setAttackDir.Enabled =
            markingBones.Enabled =
            remove.Enabled =
            playAction.Enabled =
            setEndFrame.Enabled =
            setStartFrame.Enabled =
            setNewWorldMX.Enabled = actions.SelectedIndex != -1;

            skillsList.SelectedIndex = -1;


            if (actions.SelectedIndex != -1)
            {
                BVHAction action
                    = (actions.SelectedItem as BVHAction);

                fplTxt.Text = action.FramesPerLoop.ToString();

                cyclicCb.Checked = action.IsCyclic;
                type.Text = action.Type;

                if (actions.SelectedItem is BVHAttackAction)
                {
                    setAttackBtn.Enabled = false;
                    Game1.chosenBones = (actions.SelectedItem
                        as BVHAttackAction).collisionBones;
                }
                else
                {
                    if (action.Type.ToLower() == "attack")
                    {
                        setAttackBtn.Enabled = true;
                        setAttackDir.Enabled =
                        markingBones.Enabled = false;
                    }
                    else
                    {
                        setAttackBtn.Enabled =
                        setAttackDir.Enabled =
                        markingBones.Enabled = false;

                    }
                }

                skillsList.Items.Clear();

                foreach (Skill skill in ((BVHAction)actions.SelectedItem).Skills)
                {
                    skillsList.Items.Add(skill);
                }

                skillTargets.Items.Clear();
            }

            if (actions.SelectedItem is BVHAttackAction)
            {
                BVHAttackAction action =
                    ((BVHAttackAction)actions.
                        SelectedItem);

                if (action != null &&
                  action.attackDrcs.ContainsKey(S.player.Frame))
                {
                    S.attackDirMatrix = S.point_to_axis_angle(
                        action.attackDrcs[S.player.Frame]);
                }
            }

        }

        private void add_Click(object sender, EventArgs e)
        {
            #region add new bvhaction
            string name = Microsoft.VisualBasic.
                                 Interaction.InputBox("Title", "Prompt", "Default", 0, 0);
            if (name != string.Empty)
            {
                string fln = Game1.filename;
                fln = Game1.filename.Substring(fln.LastIndexOf('\\') + 1);
                fln = fln.Substring(0, fln.IndexOf('.'));
                BVHAction act = new BVHAction(fln, (string)type.SelectedItem, Matrix.Identity,
                    name, S.player.Frame, S.player.Frame + 100, cyclicCb.Checked, 1);
                if (act.EndFrame >= S.player.Bvh.MXI.FrameCount)
                    act.EndFrame =
                        S.player.Bvh.MXI.FrameCount - 1;
                actions.Items.Add(act);
                actions.SetSelected(actions.Items.Count - 1, true);
            }
            #endregion

            update_trackbar_rects();
        }

        private void plusFrame_Click(object sender, EventArgs e)
        {
            S.player.Frame++;
            if (S.player.Frame >= S.player.Bvh.MXI.FrameCount)
                S.player.Frame = S.player.Bvh.MXI.FrameCount - 1;
        }

        private void minFrame_Click(object sender, EventArgs e)
        {
            S.player.Frame--;
            if (S.player.Frame < 0)
                S.player.Frame = 0;
        }

        private void remove_Click(object sender, EventArgs e)
        {
            actions.Items.Remove(actions.SelectedItem);
            Invalidate();
        }

        private void playAction_Click(object sender, EventArgs e)
        {
            S.player.animate = true;
            S.player.Frame = ((BVHAction)actions.SelectedItem).StartFrame;
            S.stopFrame = ((BVHAction)actions.SelectedItem).EndFrame;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            zoomTb.Maximum
                = (int)maxZoom.Value;
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            S.player.Frame = (int)frameNum.Value;
        }

        private void zoomTb_Scroll(object sender, EventArgs e)
        {
            S.camera.zoom = (float)
                (zoomTb.Maximum - zoomTb.Value + 1) / 4f;
        }
        #endregion

        #region ACTION SETTINGS
        private void setEndFrame_Click(object sender, EventArgs e)
        {
            BVHAction bvh = (BVHAction)actions.SelectedItem;
            bvh.EndFrame = S.player.Frame;
            update_label_text();
            update_trackbar_rects();
        }

        private void setNewWorldMX_Click(object sender, EventArgs e)
        {
            BVHAction bvh = (BVHAction)actions.SelectedItem;
            bvh.WorldMX = Matrix.CreateRotationY(-S.camera.AngleA);
            update_label_text();
        }

        private void setStartFrame_Click(object sender, EventArgs e)
        {
            BVHAction bvh = (BVHAction)actions.SelectedItem;
            bvh.StartFrame = S.player.Frame;
            update_label_text();
            update_trackbar_rects();
        }
        #endregion

        #region XML FUNCTIONS
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str = get_xml();

                Clipboard.SetDataObject(str, true,
                     3, 100);

            }
            catch (Exception)
            {
                MessageBox.Show("Error while copying info to clipboard.");
            }
        }

        private string get_xml()
        {
            string str = string.Empty;
            BVHAction[] all = new BVHAction[actions.Items.Count];
            actions.Items.CopyTo(all, 0);
            BVHAction[] other = get_actions_by_type("other", all);
            BVHAction[] defense = get_actions_by_type("defense", all);
            BVHAction[] damaged = get_actions_by_type("damaged", all);
            BVHAction[] attack = get_actions_by_type("attack", all);

            str += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            str += "<XnaContent>";
            str += "<Asset Type=\"XmlLib.ActionCollection\">";
            str += "<rootMovements>";
            foreach (RootMovement rm in saveRootMovement)
            {
                str += "<Item>"+rm.ToString()+"</Item>";
            }
            str += "</rootMovements>";
            str += "<other>";
            for (int i = 0; i < other.Length; i++)
            {
                str += other[i].ToXml() + "\n";
            }
            str += "</other>";
            str += "<defense>";
            for (int i = 0; i < defense.Length; i++)
            {
                str += defense[i].ToXml() + "\n";
            }
            str += "</defense>";
            str += "<damaged>";
            for (int i = 0; i < damaged.Length; i++)
            {
                str += damaged[i].ToXml() + "\n";
            }
            str += "</damaged>";
            str += "<attack>";
            for (int i = 0; i < attack.Length; i++)
            {
                str += attack[i].ToXml() + "\n";
            }
            str += "</attack>";

            str += "</Asset></XnaContent>";

            return str;

        }
        #endregion

        #region ACTION GRAPH EVENTS
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Font nameFont = new Font("Arial",
                12, FontStyle.Bold);


            foreach (BVHAction act in actions.Items)
            {
                if (act.EndFrame > act.StartFrame)
                {

                    Brush bgColor;
                    Brush foreColor;

                    if (act is BVHAttackAction)
                    {
                        bgColor = Brushes.DarkRed;
                        foreColor = Brushes.White;
                    }
                    else
                    {
                        bgColor = Brushes.Blue;
                        foreColor = Brushes.White;
                    }

                    int tbWidth = frameTb.Width - 20;

                    int startX = act.StartFrame * tbWidth /
                        S.player.Bvh.MXI.FrameCount;
                    int endX = act.EndFrame * tbWidth /
                        S.player.Bvh.MXI.FrameCount;

                    SizeF nameSize = e.Graphics.MeasureString(
                        act.Name, nameFont);

                    int textX = (startX + endX) / 2
                        - (int)nameSize.Width / 2;
                    int textY = (int)actionGraphPb.Height / 2 -
                        (int)nameSize.Height / 2;

                    e.Graphics.FillRectangle(
                    bgColor,
                    new System.Drawing.Rectangle(
                        startX, 0, endX - startX, actionGraphPb.Height));

                    if (act is BVHAttackAction)
                    {
                        Dictionary<int, Vector3> attackDrcs
                                = ((BVHAttackAction)act).attackDrcs;

                        foreach (KeyValuePair<int, Vector3> kvp in attackDrcs)
                        {
                            int x
                                = kvp.Key * tbWidth /
                        S.player.Bvh.MXI.FrameCount;

                            e.Graphics.FillRectangle(
                                          Brushes.Yellow,
                                          new System.Drawing.Rectangle(
                                             x, 0, 1, actionGraphPb.Height));

                        }
                    }


                    e.Graphics.DrawString(
                        act.Name, nameFont, foreColor,
                        new PointF(textX, textY));
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region SKILLS SETTINGS
        private void chooseCombo_Click(object sender, EventArgs e)
        {
            List<KeyAction> combo = ComboCreator.GetCombo();
            if (combo != null)
            {
                Skill s = new Skill(
                        (int)skillFromFrame.Value,
                        (int)skillToFrame.Value,
                        combo);

                skillsList.Items.Add(s);
                ((BVHAction)actions.SelectedItem).Skills.Add(s);
            }
        }

        private void skillsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            targetGb.Enabled = skillsList.SelectedIndex != -1;
            skillTargets.Items.Clear();
            if (targetGb.Enabled)
            {
                foreach (BVHTargetAction target in ((Skill)skillsList.SelectedItem).targets)
                {
                    skillTargets.Items.Add(target);
                }
            }
        }

        private void addSkillTarget_Click(object sender, EventArgs e)
        {
            string bvh = targetBvhTxt.Text;
            int frame;
            if (int.TryParse(targetFrmTxt.Text, out frame))
            {
                targetFrmTxt.Text = targetBvhTxt.Text = string.Empty;

                BVHTargetAction target = new BVHTargetAction(bvh, frame);

                skillTargets.Items.Add(target);
                ((Skill)(skillsList.SelectedItem)).targets.Add(target);
            }
            else
            {
                MessageBox.Show("Target frame must be a valid integer.");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteSkills_Click(object sender, EventArgs e)
        {
            skillsList.Items.RemoveAt(skillsList.SelectedIndex);
        }

        private void deleteTarget_Click(object sender, EventArgs e)
        {
            skillTargets.Items.RemoveAt(skillTargets.SelectedIndex);
        }
        #endregion

        private void cyclicCb_CheckedChanged(object sender, EventArgs e)
        {
            if (actions.SelectedItem != null)
            {
                ((BVHAction)actions.SelectedItem).IsCyclic = cyclicCb.Checked;
            }
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actions.SelectedItem != null)
            {
                ((BVHAction)actions.SelectedItem).Type =
                    type.Text;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog s = new SaveFileDialog();
                s.ShowDialog();
                if (s.FileName != string.Empty)
                {
                    File.WriteAllText(s.FileName, get_xml());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while saving xml file.");
            }
        }

        private void fplTxt_TextChanged(object sender, EventArgs e)
        {
            if (actions.SelectedIndex != -1)
            {
                BVHAction action = (BVHAction)actions.SelectedItem;

                float fpl;
                if (float.TryParse(fplTxt.Text, out fpl))
                {
                    action.FramesPerLoop = fpl;
                }
                else
                {
                    fplTxt.Text = fpl.ToString();
                }
            }
        }

        private void alignRight_Click(object sender, EventArgs e)
        {
            S.camera.AngleA %= MathHelper.TwoPi;
            if (S.camera.AngleA < 0)
                S.camera.AngleA += MathHelper.TwoPi;

            float max = S.camera.AngleA + (MathHelper.PiOver2 - S.camera.AngleA % MathHelper.PiOver2);
            S.camera.AngleA = max;
        }

        private void alignLeft_Click(object sender, EventArgs e)
        {
            S.camera.AngleA %= MathHelper.TwoPi;
            if (S.camera.AngleA < 0)
                S.camera.AngleA += MathHelper.TwoPi;
            float min = S.camera.AngleA - (S.camera.AngleA % MathHelper.PiOver2);
            S.camera.AngleA = min;
        }

        private void splitBVHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void splitBVHByActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < actions.Items.Count; i++)
            {
                BVHAction act = (BVHAction)(actions.Items[i]);
                string file = S.player.Bvh.split_bvh(
                    act.StartFrame,
                    act.EndFrame);

                File.WriteAllText("C:/" + i.ToString() + ".txt",
                    file);
            }
            Process.Start("C:/");
        }

        private void splitBVHByCustomPickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BvhSplitter bvh = new BvhSplitter();
            bvh.ShowDialog();
        }

    }
}
