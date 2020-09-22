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
    public partial class TypesForm: Form
    {
        public TypesForm()
        {
            InitializeComponent();

            for (int i = 0; i <= 9; i++)
            {
                dataGridView1.Rows.Add();
            }

            dataGridView1[0, 0].Value = "C";
            dataGridView1[0, 1].Value = "O";
            dataGridView1[0, 2].Value = "W";
            dataGridView1[0, 3].Value = "S";
            dataGridView1[0, 4].Value = "N";
            dataGridView1[0, 5].Value = "P";
            dataGridView1[0, 6].Value = "H";
            dataGridView1[0, 7].Value = "CL";
            dataGridView1[0, 8].Value = "Стенки ниж.";
            dataGridView1[0, 9].Value = "Стенки верх.";
            dataGridView1[1, 0].Value = "1";
            dataGridView1[1, 1].Value = "2";
            dataGridView1[1, 2].Value = "3";
            dataGridView1[1, 3].Value = "4";
            dataGridView1[1, 4].Value = "5";
            dataGridView1[1, 5].Value = "6";
            dataGridView1[1, 6].Value = "7";
            dataGridView1[1, 7].Value = "8";
            dataGridView1[1, 8].Value = "9";
            dataGridView1[1, 9].Value = "10";
            dataGridView1[2, 0].Value = "1.00";
            dataGridView1[2, 1].Value = "1.01";
            dataGridView1[2, 2].Value = "1.03";
            dataGridView1[2, 3].Value = "1.02";
            dataGridView1[2, 4].Value = "1.04";
            dataGridView1[2, 5].Value = "1.05";
            dataGridView1[2, 6].Value = "1.06";
            dataGridView1[2, 7].Value = "1.07";
            dataGridView1[2, 8].Value = "1.08";
            dataGridView1[2, 9].Value = "1.09";
        }
    }
}
