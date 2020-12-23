namespace TypeManShip
{
    partial class RegistrationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.login_box = new System.Windows.Forms.TextBox();
            this.passw_box = new System.Windows.Forms.TextBox();
            this.register_button = new System.Windows.Forms.Button();
            this.Add_data_button = new System.Windows.Forms.Button();
            this.login_label = new System.Windows.Forms.Label();
            this.passw_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stats_button = new System.Windows.Forms.Button();
            this.day_stats_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // login_box
            // 
            this.login_box.Location = new System.Drawing.Point(81, 74);
            this.login_box.Name = "login_box";
            this.login_box.Size = new System.Drawing.Size(132, 20);
            this.login_box.TabIndex = 0;
            // 
            // passw_box
            // 
            this.passw_box.Location = new System.Drawing.Point(81, 100);
            this.passw_box.Name = "passw_box";
            this.passw_box.Size = new System.Drawing.Size(132, 20);
            this.passw_box.TabIndex = 1;
            this.passw_box.TextChanged += new System.EventHandler(this.passw_box_TextChanged);
            this.passw_box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passw_box_KeyDown);
            this.passw_box.KeyUp += new System.Windows.Forms.KeyEventHandler(this.passw_box_KeyUp);
            // 
            // register_button
            // 
            this.register_button.Location = new System.Drawing.Point(81, 126);
            this.register_button.Name = "register_button";
            this.register_button.Size = new System.Drawing.Size(132, 23);
            this.register_button.TabIndex = 2;
            this.register_button.Text = "Регистрация";
            this.register_button.UseVisualStyleBackColor = true;
            this.register_button.Click += new System.EventHandler(this.register_button_Click);
            // 
            // Add_data_button
            // 
            this.Add_data_button.Location = new System.Drawing.Point(81, 155);
            this.Add_data_button.Name = "Add_data_button";
            this.Add_data_button.Size = new System.Drawing.Size(132, 23);
            this.Add_data_button.TabIndex = 3;
            this.Add_data_button.Text = "Добавить запись";
            this.Add_data_button.UseVisualStyleBackColor = true;
            this.Add_data_button.Click += new System.EventHandler(this.Add_data_button_Click);
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.Location = new System.Drawing.Point(40, 77);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(38, 13);
            this.login_label.TabIndex = 4;
            this.login_label.Text = "Логин";
            // 
            // passw_label
            // 
            this.passw_label.AutoSize = true;
            this.passw_label.Location = new System.Drawing.Point(33, 103);
            this.passw_label.Name = "passw_label";
            this.passw_label.Size = new System.Drawing.Size(45, 13);
            this.passw_label.TabIndex = 5;
            this.passw_label.Text = "Пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // stats_button
            // 
            this.stats_button.Location = new System.Drawing.Point(81, 183);
            this.stats_button.Name = "stats_button";
            this.stats_button.Size = new System.Drawing.Size(132, 23);
            this.stats_button.TabIndex = 7;
            this.stats_button.Text = "Общая статистика";
            this.stats_button.UseVisualStyleBackColor = true;
            this.stats_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // day_stats_button
            // 
            this.day_stats_button.Location = new System.Drawing.Point(81, 212);
            this.day_stats_button.Name = "day_stats_button";
            this.day_stats_button.Size = new System.Drawing.Size(132, 23);
            this.day_stats_button.TabIndex = 8;
            this.day_stats_button.Text = "Временая статистика";
            this.day_stats_button.UseVisualStyleBackColor = true;
            this.day_stats_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 247);
            this.Controls.Add(this.day_stats_button);
            this.Controls.Add(this.stats_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passw_label);
            this.Controls.Add(this.login_label);
            this.Controls.Add(this.Add_data_button);
            this.Controls.Add(this.register_button);
            this.Controls.Add(this.passw_box);
            this.Controls.Add(this.login_box);
            this.Name = "RegistrationForm";
            this.Text = "RegistrationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox login_box;
        private System.Windows.Forms.TextBox passw_box;
        private System.Windows.Forms.Button register_button;
        private System.Windows.Forms.Button Add_data_button;
        private System.Windows.Forms.Label login_label;
        private System.Windows.Forms.Label passw_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button stats_button;
        private System.Windows.Forms.Button day_stats_button;
    }
}