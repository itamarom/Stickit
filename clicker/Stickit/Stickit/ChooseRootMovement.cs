using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Processors;

namespace Stickit
{
    public partial class ChooseRootMovement : Form
    {
        public ChooseRootMovement()
        {
            InitializeComponent();
        }

        private void ChooseRootMovement_Load(object sender, EventArgs e)
        {
            string[] options = Enum.GetNames(typeof(RootMovement));
            foreach (string str in options)
                comboBox1.Items.Add(str);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            RootMovement rm;
            Enum.TryParse<RootMovement>(comboBox1.Text, out rm);

            MainForm.saveRootMovement.Add(rm);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
