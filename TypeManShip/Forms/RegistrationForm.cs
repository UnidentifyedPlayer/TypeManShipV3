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
    public partial class RegistrationForm : Form
    {
        MainController controller;
        public RegistrationForm(MainController m_controller)
        {
            controller = m_controller;
            InitializeComponent();
        }

        private void passw_box_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                    passw_box.Text = "";
                    controller.Reset();
                    break;
                default:
                    controller.Key_Down(e.KeyCode, passw_box.Text);
                    break;
            }
            e.Handled = true;
        }

        private void passw_box_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back)
            {
                controller.Key_Up(e.KeyCode);
            }
        }

        private void passw_box_TextChanged(object sender, EventArgs e)
        {
            controller.Text_changed();
            label1.Text = "Сложность:\n" + PasswInfo();
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            string message = "";
            if (controller.RegisterUser(login_box.Text, passw_box.Text, PasswInfo()))
                message = "Пользователь добавлен";
            else
                message = controller.error;
            MessageBox.Show(message);
            passw_box.Text = "";
            controller.Reset();
            passw_box.Focus();
        }

        private void Add_data_button_Click(object sender, EventArgs e)
        {
            string message = "";
            if (controller.Enter_button(passw_box.Text, login_box.Text))
                message = "Запись добавлена";
            else
                message = controller.error;
            MessageBox.Show(message);
            passw_box.Text = "";
            controller.Reset();
            passw_box.Focus();
        }

        private string PasswInfo()
        {
            Password_Q q = controller.RatePassword(passw_box.Text);
            string q_s = "";
            switch (q)
            {
                case Password_Q.Bad: q_s = "Плохой"; break;
                case Password_Q.Normal: q_s = "Нормальный"; break;
                case Password_Q.Good: q_s = "Хороший"; break;
                case Password_Q.Great: q_s = "Отличный"; break;
                case Password_Q.Safe: q_s = "Надёжный"; break;
            }
            return q_s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "";
            int userid = controller.GetUser(login_box.Text, passw_box.Text);
            if (userid == -1)
            {
                message = controller.error;
                MessageBox.Show(message);
            }
            else
            {
                if (!controller.LoadUserData(userid))
                {
                    message = controller.error;
                    MessageBox.Show(message);
                }
                else
                {
                    ShowStats stats_form = new ShowStats(ref controller);
                    stats_form.Show();
                }
            }
            passw_box.Text = "";
            controller.Reset();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "";
            int userid = controller.GetUser(login_box.Text, passw_box.Text);
            if (userid == -1)
            {
                message = controller.error;
                MessageBox.Show(message);
            }
            else
            {
                if (!controller.LoadUserData(userid))
                {
                    message = controller.error;
                    MessageBox.Show(message);
                }
                else
                {
                    controller.LoadUserData(userid);
                    DayFluctuation day_form = new DayFluctuation(ref controller);
                    day_form.Show();
                }
            }
            passw_box.Text = "";
            controller.Reset();
        }
    }
}
