namespace cozabuty_data_updater
{
	partial class Form2
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.server = new System.Windows.Forms.TextBox();
			this.user = new System.Windows.Forms.TextBox();
			this.pass = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.dbName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.port = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Uzupełnij konfigurację serwera bazy danych";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Adres serwera";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(102, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Nazwa użytkownika";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(36, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Hasło";
			// 
			// server
			// 
			this.server.Location = new System.Drawing.Point(183, 35);
			this.server.Name = "server";
			this.server.Size = new System.Drawing.Size(100, 20);
			this.server.TabIndex = 4;
			// 
			// user
			// 
			this.user.Location = new System.Drawing.Point(183, 61);
			this.user.Name = "user";
			this.user.Size = new System.Drawing.Size(100, 20);
			this.user.TabIndex = 5;
			// 
			// pass
			// 
			this.pass.Location = new System.Drawing.Point(183, 88);
			this.pass.Name = "pass";
			this.pass.Size = new System.Drawing.Size(100, 20);
			this.pass.TabIndex = 6;
			this.pass.UseSystemPasswordChar = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 113);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Nazwa bazy danych";
			// 
			// dbName
			// 
			this.dbName.Location = new System.Drawing.Point(183, 113);
			this.dbName.Name = "dbName";
			this.dbName.Size = new System.Drawing.Size(100, 20);
			this.dbName.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 139);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(26, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "Port";
			// 
			// port
			// 
			this.port.Location = new System.Drawing.Point(183, 139);
			this.port.Name = "port";
			this.port.Size = new System.Drawing.Size(100, 20);
			this.port.TabIndex = 10;
			this.port.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(107, 166);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "Zapisz";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form2
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(291, 197);
			this.ControlBox = false;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.port);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.dbName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.pass);
			this.Controls.Add(this.user);
			this.Controls.Add(this.server);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(311, 240);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(311, 240);
			this.Name = "Form2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Dane serwera";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox server;
		private System.Windows.Forms.TextBox user;
		private System.Windows.Forms.TextBox pass;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox dbName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox port;
		private System.Windows.Forms.Button button1;
	}
}