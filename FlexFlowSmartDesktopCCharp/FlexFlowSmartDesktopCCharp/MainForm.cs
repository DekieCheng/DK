using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlexFlowSmartDesktopCCharp
{
    public partial class MainForm : Form
    {
        exetowinform fr = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Oppf = new OpenFileDialog();
            Oppf.Filter = "*.exe|*.exe";
            Oppf.ShowDialog();
            if (Oppf.FileName != "")
            {
                panel1.Controls.Clear();
                fr = new exetowinform(panel1, "");
                //fr = new exetowinform(this, "");
                fr.Start(Oppf.FileName);
            }
        }
    }
}
