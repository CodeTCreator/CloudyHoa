using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsClient_Admin_.ServiceReference;

namespace WinformsClient_Admin_.AddingHoaWindows
{
    public partial class AddingHoaMW : Form
    {
        ServiceHoaAccountClient service1Client;
        int hoaId = -1;
        string hoaName;
        string hoaLogin;
        string hoaPassword;

        public AddingHoaMW()
        {
            InitializeComponent();
        }

        public AddingHoaMW(int Id, string Name,string Login, string Password)
        {
            InitializeComponent();
            hoaId = Id;
            hoaName = Name;
            hoaLogin = Login;
            hoaPassword = Password;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            string Name = textEditName.Text;
            string Login = textEditLogin.Text;
            string Password = textEditPassword.Text;
            service1Client.AddAccount(Name, Login, Password);
            this.Close();
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            string Name = textEditName.Text;
            string Login = textEditLogin.Text;
            string Password = textEditPassword.Text;
            service1Client.EditAccount(hoaId, Name, Login, Password); 
            this.Close();
        }

        private void AddingHoaMW_Load(object sender, EventArgs e)
        {
            service1Client = new ServiceHoaAccountClient();
            if (hoaId != -1) 
            { 
                PrepareForEditing();
            }
            else
            {
                PrepareForAdding();
            }
        }
        private void PrepareForEditing()
        {
            labelControlNameWindow.Text = "Редактирование учетной записи";
            textEditName.Text = hoaName;
            textEditLogin.Text = hoaLogin;
            textEditPassword.Text = hoaPassword;

            universalButton.Text = "Редактировать";
            universalButton.Click += editButton_Click;
        }
        private void PrepareForAdding()
        {
            universalButton.Click += addButton_Click;
        }
        private void CheckPassword()
        {
            if(textEditPassword.Text.Length < 7)
            {
                attentionLabelPass.Visible = true;
                universalButton.Enabled = false;
            }
            else
            {
                attentionLabelPass.Visible = false;
                universalButton.Enabled = true;
            }
        }
        private void CheckName()
        {
            string Name = textEditName.Text;
            if (service1Client.CheckAccout(Name) & Name != hoaName)
            {
                attentionLabelName.Visible = true;
                universalButton.Enabled = false;
            }
            else
            {
                attentionLabelName.Visible= false;
                universalButton.Enabled = true;
            }
            
        }
        private void textEditPassword_EditValueChanged(object sender, EventArgs e)
        {
            CheckPassword();
        }
        private void textEditName_EditValueChanged(object sender, EventArgs e)
        {
            CheckName();
        }
    }
}
