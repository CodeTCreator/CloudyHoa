using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.ServiceReference1;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataTableBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //PgSqlDataTable 
            Service1Client service1Client = new Service1Client();
            if (service1Client.CheckConnection() != null)
            {
                labelControl1.Text = "ДА";
                labelControl2.Text = service1Client.GetNumRow().ToString();
            }
            DataTable dataTable = service1Client.GetData("types_objects");
            dataGridView1.DataSource = dataTable;
            gridControl1.DataSource = dataTable;
        }
    }
}
