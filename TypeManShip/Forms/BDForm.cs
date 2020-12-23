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
    public partial class BDForm : Form
    {
        MainController controller;
        public BDForm(ref MainController m_controller)
        {
            controller = m_controller;
            InitializeComponent();
        }

        private void BDForm_Load(object sender, EventArgs e)
        {
            controller.DataControl.FillSet();
            bindingSource2.DataSource = controller.DataControl.data;
            bindingSource2.DataMember = "password_entries";
            dataGridView2.DataSource = bindingSource2;
            bindingSource1.DataSource = controller.DataControl.data;
            bindingSource1.DataMember = "users";
            dataGridView1.DataSource = bindingSource1;
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            dataGridView2.EndEdit();
            dataGridView1.EndEdit();
            controller.DataControl.UpdateGroupsUsers();
        }
    }
}
