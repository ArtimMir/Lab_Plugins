using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

namespace Lab_Plugins
{
    public partial class Form2 : Form
    {
        public Form2(List<IPlugin> info)
        {
            InitializeComponent();

            dataGridView1.DataSource = info;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.RowHeadersVisible = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
