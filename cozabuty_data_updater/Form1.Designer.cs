namespace cozabuty_data_updater
{
	partial class Form1
	{
		/// <summary>
		/// Wymagana zmienna projektanta.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Wyczyść wszystkie używane zasoby.
		/// </summary>
		/// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Kod generowany przez Projektanta formularzy systemu Windows

		/// <summary>
		/// Metoda wymagana do obsługi projektanta — nie należy modyfikować
		/// jej zawartości w edytorze kodu.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.konfiguracjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.daneSerweraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button8 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 115);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(214, 55);
			this.button1.TabIndex = 0;
			this.button1.Text = "Wgraj priorytety";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 176);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(214, 55);
			this.button2.TabIndex = 1;
			this.button2.Text = "Wgraj kategorie";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(12, 237);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(214, 55);
			this.button3.TabIndex = 2;
			this.button3.Text = "Pobierz plik z kategoriami i priorytetami";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(12, 298);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(214, 55);
			this.button4.TabIndex = 3;
			this.button4.Text = "Wgraj parametry";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(12, 359);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(214, 55);
			this.button5.TabIndex = 4;
			this.button5.Text = "Pobierz plik z parametrami";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
            "Buty",
            "Papier",
            "Folwark"});
			this.comboBox1.Location = new System.Drawing.Point(12, 88);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(213, 21);
			this.comboBox1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(213, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Wybierz grupę towarów(do pobierania pliku)";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.konfiguracjaToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(238, 24);
			this.menuStrip1.TabIndex = 8;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// konfiguracjaToolStripMenuItem
			// 
			this.konfiguracjaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.daneSerweraToolStripMenuItem,
            this.autorToolStripMenuItem});
			this.konfiguracjaToolStripMenuItem.Name = "konfiguracjaToolStripMenuItem";
			this.konfiguracjaToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
			this.konfiguracjaToolStripMenuItem.Text = "Konfiguracja";
			// 
			// daneSerweraToolStripMenuItem
			// 
			this.daneSerweraToolStripMenuItem.Name = "daneSerweraToolStripMenuItem";
			this.daneSerweraToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.daneSerweraToolStripMenuItem.Text = "Dane serwera";
			this.daneSerweraToolStripMenuItem.Click += new System.EventHandler(this.daneSerweraToolStripMenuItem_Click);
			// 
			// autorToolStripMenuItem
			// 
			this.autorToolStripMenuItem.Name = "autorToolStripMenuItem";
			this.autorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.autorToolStripMenuItem.Text = "Autor";
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(12, 420);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(214, 55);
			this.button6.TabIndex = 9;
			this.button6.Text = "Aktualizuj grupy towarów";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(12, 542);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(214, 55);
			this.button7.TabIndex = 10;
			this.button7.Text = "Pobierz plik z wagami i grupami towarów";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Enabled = false;
			this.progressBar1.Location = new System.Drawing.Point(12, 37);
			this.progressBar1.Maximum = 100000;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(214, 23);
			this.progressBar1.Step = 1;
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(-7, 294);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(258, 2);
			this.label2.TabIndex = 12;
			this.label2.Text = "label2";
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(-10, 417);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(258, 2);
			this.label3.TabIndex = 13;
			this.label3.Text = "label3";
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(11, 481);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(214, 55);
			this.button8.TabIndex = 14;
			this.button8.Text = "Aktualizuj wagi i wymiary";
			this.button8.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(238, 601);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CoZaButy Data Updater";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem konfiguracjaToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem daneSerweraToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autorToolStripMenuItem;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button8;
	}
}

