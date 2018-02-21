using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XmlLib;
namespace Stickit
{
    public partial class ComboCreator : Form
    {
        private static List<KeyAction> returnedValue;
        public ComboCreator()
        {
            InitializeComponent();
        }

        public static List<KeyAction> GetCombo()
        {
            ComboCreator cc = new ComboCreator();
            returnedValue = null;
            cc.ShowDialog();
            return returnedValue;

        }

        private void addUp_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new KeyAction(comboBox1.Text, false));
        }

        private void addDown_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new KeyAction(comboBox1.Text, true));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.addDown.Enabled =
              this.addUp.Enabled =
              comboBox1.SelectedIndex != -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComboCreator.returnedValue = new List<KeyAction>();
            foreach (KeyAction action in listBox1.Items)
                ComboCreator.returnedValue.Add(action);

            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.removeItem.Enabled = listBox1.SelectedIndex != -1;
        }
    }
}
