using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace cozabuty_data_updater
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
			try
			{
				using (StreamReader sr = new StreamReader("conf"))
				{
					string line;
					int i = 0;
					while ((line = sr.ReadLine()) != null)
					{
						if ( i == 0)
						{
							server.Text = line;						
						}
						if (i == 1)
						{
							user.Text = line;
						}
						if (i == 2)
						{
							dbName.Text = line;
						}
						if (i == 3)
						{
							port.Text = line;
						}
						if (i == 4)
						{
							pass.Text = line;
						}
						i++;
					}
				}
			}
			catch (Exception ex)
			{
				string news = ex.ToString();
			}
		}

		private void textBox5_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{


			using (StreamWriter sw = new StreamWriter("conf"))
			{
				sw.WriteLine(server.Text);
				sw.WriteLine(user.Text);
				sw.WriteLine(dbName.Text);
				sw.WriteLine(port.Text);
				sw.WriteLine(pass.Text);
			}
			Close();
		}
	}
}
