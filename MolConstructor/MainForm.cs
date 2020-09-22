using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MolConstructor
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            var infForm = new InfoForm();
            infForm.Show();
        }

        private void btnShowConstruct_Click(object sender, EventArgs e)
        {
            cnstrControl.Visible = true;
            editControl.Visible = false;
            anlsControl.Visible = false;
            visControl.Visible = false;
        }

        private void btnShowEdits_Click(object sender, EventArgs e)
        {
            cnstrControl.Visible = false;
            editControl.Visible = true;
            anlsControl.Visible = false;
            visControl.Visible = false;
        }

        private void btnShowAnalysis_Click(object sender, EventArgs e)
        {
            cnstrControl.Visible = false;
            editControl.Visible = false;
            anlsControl.Visible = true;
            visControl.Visible = false;
        }

        private void btnShowVisualisation_Click(object sender, EventArgs e)
        {
            cnstrControl.Visible = false;
            editControl.Visible = false;
            anlsControl.Visible = false;
            visControl.Visible = true;
        }

        private void btnShowSettings_Click(object sender, EventArgs e)
        {

        }
    }
}
