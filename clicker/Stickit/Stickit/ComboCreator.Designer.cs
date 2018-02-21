namespace Stickit
{
    partial class ComboCreator
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.addUp = new System.Windows.Forms.Button();
            this.addDown = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.removeItem = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Up",
            "Down",
            "Left",
            "Right",
            "Attack",
            "Jump",
            "Defense"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(250, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // addUp
            // 
            this.addUp.Enabled = false;
            this.addUp.Location = new System.Drawing.Point(12, 39);
            this.addUp.Name = "addUp";
            this.addUp.Size = new System.Drawing.Size(117, 45);
            this.addUp.TabIndex = 1;
            this.addUp.Text = "Add up";
            this.addUp.UseVisualStyleBackColor = true;
            this.addUp.Click += new System.EventHandler(this.addUp_Click);
            // 
            // addDown
            // 
            this.addDown.Enabled = false;
            this.addDown.Location = new System.Drawing.Point(135, 39);
            this.addDown.Name = "addDown";
            this.addDown.Size = new System.Drawing.Size(127, 45);
            this.addDown.TabIndex = 1;
            this.addDown.Text = "Add down";
            this.addDown.UseVisualStyleBackColor = true;
            this.addDown.Click += new System.EventHandler(this.addDown_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 90);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(250, 134);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // removeItem
            // 
            this.removeItem.Enabled = false;
            this.removeItem.Location = new System.Drawing.Point(268, 12);
            this.removeItem.Name = "removeItem";
            this.removeItem.Size = new System.Drawing.Size(83, 72);
            this.removeItem.TabIndex = 3;
            this.removeItem.Text = "X";
            this.removeItem.UseVisualStyleBackColor = true;
            this.removeItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(268, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 134);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ComboCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 232);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.removeItem);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.addDown);
            this.Controls.Add(this.addUp);
            this.Controls.Add(this.comboBox1);
            this.Name = "ComboCreator";
            this.Text = "ComboCreator";
            this.Load += new System.EventHandler(this.ComboCreator_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button addUp;
        private System.Windows.Forms.Button addDown;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button removeItem;
        private System.Windows.Forms.Button button2;
    }
}