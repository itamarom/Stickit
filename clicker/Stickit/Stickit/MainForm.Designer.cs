namespace Stickit
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.frameTb = new System.Windows.Forms.TrackBar();
            this.playAction = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.markingBones = new System.Windows.Forms.Button();
            this.minFrame = new System.Windows.Forms.Button();
            this.plusFrame = new System.Windows.Forms.Button();
            this.setAttackDir = new System.Windows.Forms.Button();
            this.setEndFrame = new System.Windows.Forms.Button();
            this.setStartFrame = new System.Windows.Forms.Button();
            this.setNewWorldMX = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.remove = new System.Windows.Forms.Button();
            this.actions = new System.Windows.Forms.ListBox();
            this.xmlOutput = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.fplTxt = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.setAttackBtn = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.targetGb = new System.Windows.Forms.GroupBox();
            this.deleteTarget = new System.Windows.Forms.Button();
            this.addSkillTarget = new System.Windows.Forms.Button();
            this.targetFrmTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.targetBvhTxt = new System.Windows.Forms.TextBox();
            this.skillTargets = new System.Windows.Forms.ListBox();
            this.skillsGb = new System.Windows.Forms.GroupBox();
            this.deleteSkills = new System.Windows.Forms.Button();
            this.chooseCombo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.skillToFrame = new System.Windows.Forms.NumericUpDown();
            this.skillFromFrame = new System.Windows.Forms.NumericUpDown();
            this.skillsList = new System.Windows.Forms.ListBox();
            this.zoomTb = new System.Windows.Forms.TrackBar();
            this.maxZoom = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.actionGraphPb = new System.Windows.Forms.PictureBox();
            this.frameNum = new System.Windows.Forms.NumericUpDown();
            this.type = new System.Windows.Forms.ComboBox();
            this.cyclicCb = new System.Windows.Forms.CheckBox();
            this.alignLeft = new System.Windows.Forms.Button();
            this.alignRight = new System.Windows.Forms.Button();
            this.splitBVHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitBVHByActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitBVHByCustomPickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.frameTb)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.targetGb.SuspendLayout();
            this.skillsGb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillToFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skillFromFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionGraphPb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameNum)).BeginInit();
            this.SuspendLayout();
            // 
            // frameTb
            // 
            this.frameTb.Location = new System.Drawing.Point(0, 32);
            this.frameTb.Name = "frameTb";
            this.frameTb.Size = new System.Drawing.Size(915, 45);
            this.frameTb.TabIndex = 1;
            this.frameTb.ValueChanged += new System.EventHandler(this.frameTb_ValueChanged);
            // 
            // playAction
            // 
            this.playAction.Enabled = false;
            this.playAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.playAction.Location = new System.Drawing.Point(659, 110);
            this.playAction.Name = "playAction";
            this.playAction.Size = new System.Drawing.Size(183, 95);
            this.playAction.TabIndex = 1;
            this.playAction.Text = "Play action";
            this.playAction.Click += new System.EventHandler(this.playAction_Click);
            // 
            // playBtn
            // 
            this.playBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.playBtn.Location = new System.Drawing.Point(845, 110);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(141, 95);
            this.playBtn.TabIndex = 1;
            this.playBtn.Text = "Play";
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // markingBones
            // 
            this.markingBones.Enabled = false;
            this.markingBones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.markingBones.Location = new System.Drawing.Point(3, 128);
            this.markingBones.Name = "markingBones";
            this.markingBones.Size = new System.Drawing.Size(133, 89);
            this.markingBones.TabIndex = 1;
            this.markingBones.Text = "Start marking bones";
            this.markingBones.Click += new System.EventHandler(this.markingBones_Click);
            // 
            // minFrame
            // 
            this.minFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.minFrame.Location = new System.Drawing.Point(921, 36);
            this.minFrame.Name = "minFrame";
            this.minFrame.Size = new System.Drawing.Size(33, 48);
            this.minFrame.TabIndex = 1;
            this.minFrame.Text = "<";
            this.minFrame.Click += new System.EventHandler(this.minFrame_Click);
            // 
            // plusFrame
            // 
            this.plusFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.plusFrame.Location = new System.Drawing.Point(958, 36);
            this.plusFrame.Name = "plusFrame";
            this.plusFrame.Size = new System.Drawing.Size(33, 48);
            this.plusFrame.TabIndex = 1;
            this.plusFrame.Text = ">";
            this.plusFrame.Click += new System.EventHandler(this.plusFrame_Click);
            // 
            // setAttackDir
            // 
            this.setAttackDir.Enabled = false;
            this.setAttackDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.setAttackDir.Location = new System.Drawing.Point(3, 59);
            this.setAttackDir.Name = "setAttackDir";
            this.setAttackDir.Size = new System.Drawing.Size(133, 63);
            this.setAttackDir.TabIndex = 1;
            this.setAttackDir.Text = "set\\remove attack dir";
            this.setAttackDir.Click += new System.EventHandler(this.setAttackDir_Click);
            // 
            // setEndFrame
            // 
            this.setEndFrame.Enabled = false;
            this.setEndFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.setEndFrame.Location = new System.Drawing.Point(3, 55);
            this.setEndFrame.Name = "setEndFrame";
            this.setEndFrame.Size = new System.Drawing.Size(133, 43);
            this.setEndFrame.TabIndex = 1;
            this.setEndFrame.Text = "set end frame";
            this.setEndFrame.Click += new System.EventHandler(this.setEndFrame_Click);
            // 
            // setStartFrame
            // 
            this.setStartFrame.Enabled = false;
            this.setStartFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.setStartFrame.Location = new System.Drawing.Point(3, 6);
            this.setStartFrame.Name = "setStartFrame";
            this.setStartFrame.Size = new System.Drawing.Size(133, 43);
            this.setStartFrame.TabIndex = 1;
            this.setStartFrame.Text = "set start frame";
            this.setStartFrame.Click += new System.EventHandler(this.setStartFrame_Click);
            // 
            // setNewWorldMX
            // 
            this.setNewWorldMX.Enabled = false;
            this.setNewWorldMX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.setNewWorldMX.Location = new System.Drawing.Point(3, 104);
            this.setNewWorldMX.Name = "setNewWorldMX";
            this.setNewWorldMX.Size = new System.Drawing.Size(133, 75);
            this.setNewWorldMX.TabIndex = 1;
            this.setNewWorldMX.Text = "camera rot to world MX";
            this.setNewWorldMX.Click += new System.EventHandler(this.setNewWorldMX_Click);
            // 
            // add
            // 
            this.add.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.add.Location = new System.Drawing.Point(632, 110);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(21, 46);
            this.add.TabIndex = 1;
            this.add.Text = "+";
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // remove
            // 
            this.remove.Enabled = false;
            this.remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.remove.Location = new System.Drawing.Point(632, 162);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(21, 43);
            this.remove.TabIndex = 1;
            this.remove.Text = "X";
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // actions
            // 
            this.actions.Location = new System.Drawing.Point(507, 110);
            this.actions.Name = "actions";
            this.actions.Size = new System.Drawing.Size(119, 69);
            this.actions.TabIndex = 1;
            this.actions.SelectedIndexChanged += new System.EventHandler(this.actions_SelectedIndexChanged);
            // 
            // xmlOutput
            // 
            this.xmlOutput.AutoSize = true;
            this.xmlOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.xmlOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.xmlOutput.Location = new System.Drawing.Point(22, 3);
            this.xmlOutput.Name = "xmlOutput";
            this.xmlOutput.Size = new System.Drawing.Size(0, 20);
            this.xmlOutput.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(9, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 526);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.splitBVHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.fileToolStripMenuItem.Text = "Copy Xml to clipboard";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabControl1.Location = new System.Drawing.Point(507, 211);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(477, 477);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.fplTxt);
            this.tabPage1.Controls.Add(this.setNewWorldMX);
            this.tabPage1.Controls.Add(this.setEndFrame);
            this.tabPage1.Controls.Add(this.setStartFrame);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(469, 440);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Action settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-1, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 24);
            this.label6.TabIndex = 3;
            this.label6.Text = "Frames per loop:";
            // 
            // fplTxt
            // 
            this.fplTxt.Enabled = false;
            this.fplTxt.Location = new System.Drawing.Point(3, 208);
            this.fplTxt.Name = "fplTxt";
            this.fplTxt.Size = new System.Drawing.Size(133, 29);
            this.fplTxt.TabIndex = 2;
            this.fplTxt.Text = "1";
            this.fplTxt.TextChanged += new System.EventHandler(this.fplTxt_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.setAttackBtn);
            this.tabPage2.Controls.Add(this.markingBones);
            this.tabPage2.Controls.Add(this.setAttackDir);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(469, 440);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Attack settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // setAttackBtn
            // 
            this.setAttackBtn.Enabled = false;
            this.setAttackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.setAttackBtn.Location = new System.Drawing.Point(6, 6);
            this.setAttackBtn.Name = "setAttackBtn";
            this.setAttackBtn.Size = new System.Drawing.Size(130, 47);
            this.setAttackBtn.TabIndex = 2;
            this.setAttackBtn.Text = "set as attack";
            this.setAttackBtn.UseVisualStyleBackColor = true;
            this.setAttackBtn.Click += new System.EventHandler(this.setAttackBtn_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.xmlOutput);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(469, 440);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Xml";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.targetGb);
            this.tabPage4.Controls.Add(this.skillsGb);
            this.tabPage4.Location = new System.Drawing.Point(4, 33);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(469, 440);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Skills";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // targetGb
            // 
            this.targetGb.Controls.Add(this.deleteTarget);
            this.targetGb.Controls.Add(this.addSkillTarget);
            this.targetGb.Controls.Add(this.targetFrmTxt);
            this.targetGb.Controls.Add(this.label3);
            this.targetGb.Controls.Add(this.label2);
            this.targetGb.Controls.Add(this.targetBvhTxt);
            this.targetGb.Controls.Add(this.skillTargets);
            this.targetGb.Enabled = false;
            this.targetGb.Location = new System.Drawing.Point(6, 218);
            this.targetGb.Name = "targetGb";
            this.targetGb.Size = new System.Drawing.Size(457, 206);
            this.targetGb.TabIndex = 3;
            this.targetGb.TabStop = false;
            this.targetGb.Text = "Targets Of Skills";
            // 
            // deleteTarget
            // 
            this.deleteTarget.Location = new System.Drawing.Point(15, 162);
            this.deleteTarget.Name = "deleteTarget";
            this.deleteTarget.Size = new System.Drawing.Size(154, 34);
            this.deleteTarget.TabIndex = 4;
            this.deleteTarget.Text = "Delete";
            this.deleteTarget.UseVisualStyleBackColor = true;
            this.deleteTarget.Click += new System.EventHandler(this.deleteTarget_Click);
            // 
            // addSkillTarget
            // 
            this.addSkillTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.addSkillTarget.Location = new System.Drawing.Point(350, 28);
            this.addSkillTarget.Name = "addSkillTarget";
            this.addSkillTarget.Size = new System.Drawing.Size(95, 128);
            this.addSkillTarget.TabIndex = 5;
            this.addSkillTarget.Text = "+";
            this.addSkillTarget.UseVisualStyleBackColor = true;
            this.addSkillTarget.Click += new System.EventHandler(this.addSkillTarget_Click);
            // 
            // targetFrmTxt
            // 
            this.targetFrmTxt.Location = new System.Drawing.Point(175, 127);
            this.targetFrmTxt.Name = "targetFrmTxt";
            this.targetFrmTxt.Size = new System.Drawing.Size(135, 29);
            this.targetFrmTxt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Target Frame:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Target BVH Name:";
            // 
            // targetBvhTxt
            // 
            this.targetBvhTxt.Location = new System.Drawing.Point(175, 62);
            this.targetBvhTxt.Name = "targetBvhTxt";
            this.targetBvhTxt.Size = new System.Drawing.Size(135, 29);
            this.targetBvhTxt.TabIndex = 2;
            // 
            // skillTargets
            // 
            this.skillTargets.FormattingEnabled = true;
            this.skillTargets.ItemHeight = 24;
            this.skillTargets.Location = new System.Drawing.Point(15, 32);
            this.skillTargets.Name = "skillTargets";
            this.skillTargets.Size = new System.Drawing.Size(154, 124);
            this.skillTargets.TabIndex = 1;
            // 
            // skillsGb
            // 
            this.skillsGb.Controls.Add(this.deleteSkills);
            this.skillsGb.Controls.Add(this.chooseCombo);
            this.skillsGb.Controls.Add(this.label5);
            this.skillsGb.Controls.Add(this.label4);
            this.skillsGb.Controls.Add(this.skillToFrame);
            this.skillsGb.Controls.Add(this.skillFromFrame);
            this.skillsGb.Controls.Add(this.skillsList);
            this.skillsGb.Enabled = false;
            this.skillsGb.Location = new System.Drawing.Point(6, 6);
            this.skillsGb.Name = "skillsGb";
            this.skillsGb.Size = new System.Drawing.Size(457, 206);
            this.skillsGb.TabIndex = 2;
            this.skillsGb.TabStop = false;
            this.skillsGb.Text = "Skills";
            // 
            // deleteSkills
            // 
            this.deleteSkills.Location = new System.Drawing.Point(15, 166);
            this.deleteSkills.Name = "deleteSkills";
            this.deleteSkills.Size = new System.Drawing.Size(154, 34);
            this.deleteSkills.TabIndex = 4;
            this.deleteSkills.Text = "Delete";
            this.deleteSkills.UseVisualStyleBackColor = true;
            this.deleteSkills.Click += new System.EventHandler(this.deleteSkills_Click);
            // 
            // chooseCombo
            // 
            this.chooseCombo.Location = new System.Drawing.Point(175, 112);
            this.chooseCombo.Name = "chooseCombo";
            this.chooseCombo.Size = new System.Drawing.Size(276, 88);
            this.chooseCombo.TabIndex = 3;
            this.chooseCombo.Text = "Choose combo and add Skill";
            this.chooseCombo.UseVisualStyleBackColor = true;
            this.chooseCombo.Click += new System.EventHandler(this.chooseCombo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "to";
            // 
            // skillToFrame
            // 
            this.skillToFrame.Location = new System.Drawing.Point(360, 54);
            this.skillToFrame.Name = "skillToFrame";
            this.skillToFrame.Size = new System.Drawing.Size(57, 29);
            this.skillToFrame.TabIndex = 1;
            // 
            // skillFromFrame
            // 
            this.skillFromFrame.Location = new System.Drawing.Point(272, 54);
            this.skillFromFrame.Name = "skillFromFrame";
            this.skillFromFrame.Size = new System.Drawing.Size(57, 29);
            this.skillFromFrame.TabIndex = 1;
            // 
            // skillsList
            // 
            this.skillsList.FormattingEnabled = true;
            this.skillsList.ItemHeight = 24;
            this.skillsList.Location = new System.Drawing.Point(15, 37);
            this.skillsList.Name = "skillsList";
            this.skillsList.Size = new System.Drawing.Size(154, 124);
            this.skillsList.TabIndex = 0;
            this.skillsList.SelectedIndexChanged += new System.EventHandler(this.skillsList_SelectedIndexChanged);
            // 
            // zoomTb
            // 
            this.zoomTb.Location = new System.Drawing.Point(65, 644);
            this.zoomTb.Maximum = 100;
            this.zoomTb.Name = "zoomTb";
            this.zoomTb.Size = new System.Drawing.Size(298, 45);
            this.zoomTb.TabIndex = 4;
            this.zoomTb.Value = 1;
            this.zoomTb.Scroll += new System.EventHandler(this.zoomTb_Scroll);
            // 
            // maxZoom
            // 
            this.maxZoom.Location = new System.Drawing.Point(12, 660);
            this.maxZoom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxZoom.Name = "maxZoom";
            this.maxZoom.Size = new System.Drawing.Size(47, 20);
            this.maxZoom.TabIndex = 0;
            this.maxZoom.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maxZoom.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 644);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max zoom:";
            // 
            // actionGraphPb
            // 
            this.actionGraphPb.BackColor = System.Drawing.Color.Transparent;
            this.actionGraphPb.Location = new System.Drawing.Point(12, 63);
            this.actionGraphPb.Name = "actionGraphPb";
            this.actionGraphPb.Size = new System.Drawing.Size(915, 38);
            this.actionGraphPb.TabIndex = 5;
            this.actionGraphPb.TabStop = false;
            this.actionGraphPb.Click += new System.EventHandler(this.pictureBox1_Click);
            this.actionGraphPb.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // frameNum
            // 
            this.frameNum.Location = new System.Drawing.Point(921, 86);
            this.frameNum.Name = "frameNum";
            this.frameNum.Size = new System.Drawing.Size(70, 20);
            this.frameNum.TabIndex = 6;
            this.frameNum.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged_1);
            // 
            // type
            // 
            this.type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            "Other",
            "Defense",
            "Damaged",
            "Attack"});
            this.type.Location = new System.Drawing.Point(507, 184);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(59, 21);
            this.type.TabIndex = 4;
            this.type.SelectedIndexChanged += new System.EventHandler(this.type_SelectedIndexChanged);
            // 
            // cyclicCb
            // 
            this.cyclicCb.AutoSize = true;
            this.cyclicCb.Location = new System.Drawing.Point(572, 187);
            this.cyclicCb.Name = "cyclicCb";
            this.cyclicCb.Size = new System.Drawing.Size(54, 17);
            this.cyclicCb.TabIndex = 7;
            this.cyclicCb.Text = "Cyclic";
            this.cyclicCb.UseVisualStyleBackColor = true;
            this.cyclicCb.CheckedChanged += new System.EventHandler(this.cyclicCb_CheckedChanged);
            // 
            // alignLeft
            // 
            this.alignLeft.Location = new System.Drawing.Point(369, 648);
            this.alignLeft.Name = "alignLeft";
            this.alignLeft.Size = new System.Drawing.Size(62, 41);
            this.alignLeft.TabIndex = 8;
            this.alignLeft.Text = "Align Left";
            this.alignLeft.UseVisualStyleBackColor = true;
            this.alignLeft.Click += new System.EventHandler(this.alignLeft_Click);
            // 
            // alignRight
            // 
            this.alignRight.Location = new System.Drawing.Point(437, 648);
            this.alignRight.Name = "alignRight";
            this.alignRight.Size = new System.Drawing.Size(62, 41);
            this.alignRight.TabIndex = 8;
            this.alignRight.Text = "Align Right";
            this.alignRight.UseVisualStyleBackColor = true;
            this.alignRight.Click += new System.EventHandler(this.alignRight_Click);
            // 
            // splitBVHToolStripMenuItem
            // 
            this.splitBVHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.splitBVHByActionsToolStripMenuItem,
            this.splitBVHByCustomPickToolStripMenuItem});
            this.splitBVHToolStripMenuItem.Name = "splitBVHToolStripMenuItem";
            this.splitBVHToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.splitBVHToolStripMenuItem.Text = "Split BVH";
            this.splitBVHToolStripMenuItem.Click += new System.EventHandler(this.splitBVHToolStripMenuItem_Click);
            // 
            // splitBVHByActionsToolStripMenuItem
            // 
            this.splitBVHByActionsToolStripMenuItem.Name = "splitBVHByActionsToolStripMenuItem";
            this.splitBVHByActionsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.splitBVHByActionsToolStripMenuItem.Text = "Split BVH by actions";
            this.splitBVHByActionsToolStripMenuItem.Click += new System.EventHandler(this.splitBVHByActionsToolStripMenuItem_Click);
            // 
            // splitBVHByCustomPickToolStripMenuItem
            // 
            this.splitBVHByCustomPickToolStripMenuItem.Name = "splitBVHByCustomPickToolStripMenuItem";
            this.splitBVHByCustomPickToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.splitBVHByCustomPickToolStripMenuItem.Text = "Split BVH by custom pick";
            this.splitBVHByCustomPickToolStripMenuItem.Click += new System.EventHandler(this.splitBVHByCustomPickToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.alignRight);
            this.Controls.Add(this.alignLeft);
            this.Controls.Add(this.cyclicCb);
            this.Controls.Add(this.type);
            this.Controls.Add(this.frameNum);
            this.Controls.Add(this.actionGraphPb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maxZoom);
            this.Controls.Add(this.zoomTb);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.frameTb);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.actions);
            this.Controls.Add(this.playAction);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.plusFrame);
            this.Controls.Add(this.add);
            this.Controls.Add(this.minFrame);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Move += new System.EventHandler(this.MainForm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.frameTb)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.targetGb.ResumeLayout(false);
            this.targetGb.PerformLayout();
            this.skillsGb.ResumeLayout(false);
            this.skillsGb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skillToFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skillFromFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionGraphPb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;

        #region FORM
        private System.Windows.Forms.Button playBtn, setAttackDir, playAction;
        private System.Windows.Forms.Button minFrame, plusFrame;
        private System.Windows.Forms.Button markingBones;
        private System.Windows.Forms.Button setStartFrame, setEndFrame, setNewWorldMX;
        private System.Windows.Forms.Button add, remove;
        private System.Windows.Forms.Label xmlOutput;
        private System.Windows.Forms.ListBox actions;
        private System.Windows.Forms.TrackBar frameTb;
        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button setAttackBtn;
        private System.Windows.Forms.TrackBar zoomTb;
        private System.Windows.Forms.NumericUpDown maxZoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox actionGraphPb;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox skillsList;
        private System.Windows.Forms.GroupBox targetGb;
        private System.Windows.Forms.Button addSkillTarget;
        private System.Windows.Forms.TextBox targetFrmTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox targetBvhTxt;
        private System.Windows.Forms.ListBox skillTargets;
        private System.Windows.Forms.GroupBox skillsGb;
        private System.Windows.Forms.NumericUpDown frameNum;
        private System.Windows.Forms.Button chooseCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown skillToFrame;
        private System.Windows.Forms.NumericUpDown skillFromFrame;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button deleteTarget;
        private System.Windows.Forms.Button deleteSkills;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.CheckBox cyclicCb;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.TextBox fplTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button alignLeft;
        private System.Windows.Forms.Button alignRight;
        private System.Windows.Forms.ToolStripMenuItem splitBVHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitBVHByActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitBVHByCustomPickToolStripMenuItem;


    }
}