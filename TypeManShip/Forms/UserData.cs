using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeManShip
{
    public partial class UserData : Form
    {
        MainController controller;
        public UserData(ref MainController m_controller, int id)
        {
            controller = m_controller;
            controller.DataControl.ConfigureUserEntriesAdapter(id);
            controller.DataControl.FillUserSet();
            InitializeComponent();
        }

        private void UserData_Load(object sender, EventArgs e)
        {

            bindingSource1.DataSource = controller.DataControl.data;
            bindingSource1.DataMember = "password_entries";
            dataGridView1.DataSource = bindingSource1;
        }

        private void save_buuton_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            controller.DataControl.UpdateUserEntries();
        }
    }
}
