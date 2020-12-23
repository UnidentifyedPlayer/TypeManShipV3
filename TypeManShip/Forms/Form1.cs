using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



namespace TypeManShip
{
    public partial class Form1 : Form
    {
        MainController controller;
        public Form1()
        {
            InitializeComponent();
            controller = new MainController();
        }

        private void pass_set_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void pass_set_textbox_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void pass_enter_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                    pass_enter_textbox.Text = "";
                    controller.Reset();
                    break;
                default: controller.Key_Down(e.KeyCode, pass_enter_textbox.Text);
                    break;
            }
            e.Handled = true;
        }

        private void pass_enter_textbox_TextChanged(object sender, EventArgs e)
        {
            controller.Text_changed();
        }

        private void pass_enter_textbox_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Back)
            {
                controller.Key_Up(e.KeyCode);
            }
        }

        private void pass_set_button_Click(object sender, EventArgs e)
        {
            string message = "";
            int userid = controller.GetUser(pass_enter_textbox.Text);
            if(userid == -1)
            {
                message = controller.error;
            }
            else
            {
                message = string.Format("Был найден пользователь с подходящими данными.\n ID - {0}", userid);
            }
            MessageBox.Show(message);
            pass_enter_textbox.Text = "";
            controller.Reset();
            pass_enter_textbox.Focus();
        }

        private void pass_entry_button_Click(object sender, EventArgs e)
        {
            string message = "";
            if (controller.VerifyUser(login_box.Text, pass_enter_textbox.Text))
                message = "Пользователь прошёл проверку";
            else
                message = controller.error;
            MessageBox.Show(message);
            pass_enter_textbox.Text = "";
            controller.Reset();
            pass_enter_textbox.Focus();
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            controller.Reset();
            pass_enter_textbox.Text = "";
            login_box.Text = "";
        }

        List<int> array1 = new List<int> { 5, 6, 8, 4, 5, 3, 6, 7 };
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
  
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pass_set_textbox_TextChanged(object sender, EventArgs e)
        {
            Password_Q q = controller.RatePassword(login_box.Text);
            string q_s = "";
            switch(q)
            {
                case Password_Q.Bad: q_s = "Плохой"; break;
                case Password_Q.Normal: q_s = "Нормальный"; break;
                case Password_Q.Good: q_s = "Хороший"; break;
                case Password_Q.Great: q_s = "Отличный"; break;
                case Password_Q.Safe: q_s = "Надёжный"; break;
            }
            //label4.Text = "Сложность: " + q_s;
        }

        private void Stats_Click(object sender, EventArgs e)
        {
            ShowStats stats_form = new ShowStats(ref controller);
            stats_form.Show();
        }

        private void day_stats_win_button_Click(object sender, EventArgs e)
        {
            DayFluctuation day_form = new DayFluctuation(ref controller);
            day_form.Show();
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            RegistrationForm register_form = new RegistrationForm(controller);
            register_form.Show();
        }

        private void BD_button_Click(object sender, EventArgs e)
        {
            BDForm BD_form = new BDForm(ref controller);
            BD_form.Show();
        }
    }
}
