namespace TypeManShip
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Data.DataColumn Password;
            System.Data.DataColumn ExpValue;
            System.Data.DataColumn Dispersion;
            this.pass_enter_textbox = new System.Windows.Forms.TextBox();
            this.pass_set_label = new System.Windows.Forms.Label();
            this.login_box = new System.Windows.Forms.TextBox();
            this.pass_enter_label = new System.Windows.Forms.Label();
            this.pass_set_button = new System.Windows.Forms.Button();
            this.pass_entry_button = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.UserID = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.EntryID = new System.Data.DataColumn();
            this.Speed = new System.Data.DataColumn();
            this.EntryDate = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
            this.PressID = new System.Data.DataColumn();
            this.FKEntryID = new System.Data.DataColumn();
            this.KeyDownTime = new System.Data.DataColumn();
            this.KeyUpTime = new System.Data.DataColumn();
            this.register_button = new System.Windows.Forms.Button();
            this.BD_button = new System.Windows.Forms.Button();
            Password = new System.Data.DataColumn();
            ExpValue = new System.Data.DataColumn();
            Dispersion = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            this.SuspendLayout();
            // 
            // Password
            // 
            Password.ColumnName = "Password";
            // 
            // ExpValue
            // 
            ExpValue.ColumnName = "ExpValue";
            ExpValue.DataType = typeof(double);
            // 
            // Dispersion
            // 
            Dispersion.ColumnName = "Dispersion";
            Dispersion.DataType = typeof(double);
            // 
            // pass_enter_textbox
            // 
            this.pass_enter_textbox.Location = new System.Drawing.Point(24, 129);
            this.pass_enter_textbox.Name = "pass_enter_textbox";
            this.pass_enter_textbox.Size = new System.Drawing.Size(122, 20);
            this.pass_enter_textbox.TabIndex = 0;
            this.pass_enter_textbox.TextChanged += new System.EventHandler(this.pass_enter_textbox_TextChanged);
            this.pass_enter_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pass_enter_textbox_KeyDown);
            this.pass_enter_textbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pass_enter_textbox_KeyUp);
            // 
            // pass_set_label
            // 
            this.pass_set_label.AutoSize = true;
            this.pass_set_label.Location = new System.Drawing.Point(21, 74);
            this.pass_set_label.Name = "pass_set_label";
            this.pass_set_label.Size = new System.Drawing.Size(38, 13);
            this.pass_set_label.TabIndex = 2;
            this.pass_set_label.Text = "Логин";
            // 
            // login_box
            // 
            this.login_box.Location = new System.Drawing.Point(24, 90);
            this.login_box.Name = "login_box";
            this.login_box.Size = new System.Drawing.Size(122, 20);
            this.login_box.TabIndex = 3;
            this.login_box.TextChanged += new System.EventHandler(this.pass_set_textbox_TextChanged);
            this.login_box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pass_set_textbox_KeyDown);
            this.login_box.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pass_set_textbox_KeyUp);
            // 
            // pass_enter_label
            // 
            this.pass_enter_label.AutoSize = true;
            this.pass_enter_label.Location = new System.Drawing.Point(21, 113);
            this.pass_enter_label.Name = "pass_enter_label";
            this.pass_enter_label.Size = new System.Drawing.Size(45, 13);
            this.pass_enter_label.TabIndex = 4;
            this.pass_enter_label.Text = "Пароль";
            // 
            // pass_set_button
            // 
            this.pass_set_button.Location = new System.Drawing.Point(228, 87);
            this.pass_set_button.Name = "pass_set_button";
            this.pass_set_button.Size = new System.Drawing.Size(102, 23);
            this.pass_set_button.TabIndex = 5;
            this.pass_set_button.Text = "Идентификация";
            this.pass_set_button.UseVisualStyleBackColor = true;
            this.pass_set_button.Click += new System.EventHandler(this.pass_set_button_Click);
            // 
            // pass_entry_button
            // 
            this.pass_entry_button.Location = new System.Drawing.Point(228, 126);
            this.pass_entry_button.Name = "pass_entry_button";
            this.pass_entry_button.Size = new System.Drawing.Size(102, 23);
            this.pass_entry_button.TabIndex = 6;
            this.pass_entry_button.Text = "Верификация";
            this.pass_entry_button.UseVisualStyleBackColor = true;
            this.pass_entry_button.Click += new System.EventHandler(this.pass_entry_button_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "BaseData";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.UserID,
            Password,
            ExpValue,
            Dispersion});
            this.dataTable1.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "UserID"}, true)});
            this.dataTable1.PrimaryKey = new System.Data.DataColumn[] {
        this.UserID};
            this.dataTable1.TableName = "Users";
            // 
            // UserID
            // 
            this.UserID.AllowDBNull = false;
            this.UserID.AutoIncrement = true;
            this.UserID.ColumnName = "UserID";
            this.UserID.DataType = typeof(long);
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.EntryID,
            this.Speed,
            this.EntryDate});
            this.dataTable2.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "Entry_id"}, true)});
            this.dataTable2.PrimaryKey = new System.Data.DataColumn[] {
        this.EntryID};
            this.dataTable2.TableName = "PhraseEntries";
            // 
            // EntryID
            // 
            this.EntryID.AllowDBNull = false;
            this.EntryID.ColumnName = "Entry_id";
            this.EntryID.DataType = typeof(long);
            // 
            // Speed
            // 
            this.Speed.ColumnName = "Speed";
            this.Speed.DataType = typeof(double);
            // 
            // EntryDate
            // 
            this.EntryDate.Caption = "EntryDate";
            this.EntryDate.ColumnName = "EntryDate";
            this.EntryDate.DataType = typeof(System.DateTime);
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.PressID,
            this.FKEntryID,
            this.KeyDownTime,
            this.KeyUpTime});
            this.dataTable3.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "PressID"}, true)});
            this.dataTable3.PrimaryKey = new System.Data.DataColumn[] {
        this.PressID};
            this.dataTable3.TableName = "KeyPresses";
            // 
            // PressID
            // 
            this.PressID.AllowDBNull = false;
            this.PressID.ColumnName = "PressID";
            this.PressID.DataType = typeof(long);
            // 
            // FKEntryID
            // 
            this.FKEntryID.ColumnName = "EntryID";
            // 
            // KeyDownTime
            // 
            this.KeyDownTime.ColumnName = "KeyDownTime";
            this.KeyDownTime.DataType = typeof(int);
            // 
            // KeyUpTime
            // 
            this.KeyUpTime.ColumnName = "KeyUpTime";
            this.KeyUpTime.DataType = typeof(int);
            // 
            // register_button
            // 
            this.register_button.Location = new System.Drawing.Point(12, 240);
            this.register_button.Name = "register_button";
            this.register_button.Size = new System.Drawing.Size(88, 23);
            this.register_button.TabIndex = 20;
            this.register_button.Text = "Регистрация";
            this.register_button.UseVisualStyleBackColor = true;
            this.register_button.Click += new System.EventHandler(this.register_button_Click);
            // 
            // BD_button
            // 
            this.BD_button.Location = new System.Drawing.Point(242, 240);
            this.BD_button.Name = "BD_button";
            this.BD_button.Size = new System.Drawing.Size(88, 23);
            this.BD_button.TabIndex = 21;
            this.BD_button.Text = "Показать БД";
            this.BD_button.UseVisualStyleBackColor = true;
            this.BD_button.Click += new System.EventHandler(this.BD_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 275);
            this.Controls.Add(this.BD_button);
            this.Controls.Add(this.register_button);
            this.Controls.Add(this.pass_entry_button);
            this.Controls.Add(this.pass_set_button);
            this.Controls.Add(this.pass_enter_label);
            this.Controls.Add(this.login_box);
            this.Controls.Add(this.pass_set_label);
            this.Controls.Add(this.pass_enter_textbox);
            this.Name = "Form1";
            this.Text = "Меню";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pass_enter_textbox;
        private System.Windows.Forms.Label pass_set_label;
        private System.Windows.Forms.TextBox login_box;
        private System.Windows.Forms.Label pass_enter_label;
        private System.Windows.Forms.Button pass_set_button;
        private System.Windows.Forms.Button pass_entry_button;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn UserID;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn EntryID;
        private System.Data.DataColumn Speed;
        private System.Data.DataColumn EntryDate;
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn PressID;
        private System.Data.DataColumn FKEntryID;
        private System.Data.DataColumn KeyDownTime;
        private System.Data.DataColumn KeyUpTime;
        private System.Windows.Forms.Button register_button;
        private System.Windows.Forms.Button BD_button;
    }
}

