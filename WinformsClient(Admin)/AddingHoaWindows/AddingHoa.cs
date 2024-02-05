using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.ServiceReference1;
using WinformsClient_Admin_.ServiceReference;

namespace WinformsClient_Admin_.AddingHoaWindows
{
    public partial class AddingHoa : Form
    {
        ServiceHoaAccountClient service1Client;
        DataTable dataTable;
        public AddingHoa()
        {
            InitializeComponent();
        }

        private void AddingHoa_Load(object sender, EventArgs e)
        {
            try
            {
                service1Client = new ServiceHoaAccountClient();
                service1Client.Open();
                UpdateTable();
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                const string message =
                "Сервер недоступен";
                const string caption = "Подключение к серверу";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
                this.Close();
            }
            //service1Client.State
           
        }

        private void UpdateTable()
        {
            dataTable = service1Client.GetTableOfHoa().Tables[0];
            gridControl1.DataSource = dataTable;
        }
        private void changeButton_Click(object sender, EventArgs e)
        {
            int hoaId;
            string hoaName;
            string hoaLogin;
            string hoaPassword;
            if (gridView1.GetFocusedRowCellValue("id") != null)
            {
                hoaId = (int)gridView1.GetFocusedRowCellValue("id");
                hoaName = gridView1.GetFocusedRowCellValue("name").ToString();
                hoaLogin = gridView1.GetFocusedRowCellValue("login").ToString();
                hoaPassword = gridView1.GetFocusedRowCellValue("password").ToString();
                AddingHoaMW addingHoaMW = new AddingHoaMW(hoaId, hoaName, hoaLogin, hoaPassword);
                addingHoaMW.ShowDialog();

                UpdateTable();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int hoaId;
            if (gridView1.GetFocusedRowCellValue("id") != null)
            {
                 
                hoaId = (int)gridView1.GetFocusedRowCellValue("id");
                service1Client.DeleteAccount(hoaId);
                UpdateTable();
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddingHoaMW addingHoaMW = new AddingHoaMW();
            addingHoaMW.ShowDialog();

            UpdateTable();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
