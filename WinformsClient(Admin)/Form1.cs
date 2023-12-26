using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsClient_Admin_.AddingHoaWindows;
using WinformsClient_Admin_.ObjectsWindow;

namespace WinformsClient_Admin_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void accountButton_Click(object sender, EventArgs e)
        {
            AddingHoa addingHoa = new AddingHoa();
            addingHoa.ShowDialog();
        }

        private void objectsButton_Click(object sender, EventArgs e)
        {

            ObjectsWindow objectsWindow = new ObjectsWindow();
            objectsWindow.ShowDialog(); 
        }
    }
}
