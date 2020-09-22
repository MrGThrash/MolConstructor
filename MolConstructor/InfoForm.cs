
using System.Diagnostics;
using System.Windows.Forms;


namespace MolConstructor
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://polly.phys.msu.ru/ru/labs/Potemkin/Home_EN.html");
        }
    }
}
