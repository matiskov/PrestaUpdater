using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;
namespace cozabuty_data_updater
{
	class ConnectDB
	{
		public MySqlConnection conn = new MySqlConnection();
		private string connString;



		public void OpenConnection()
		{
			try
			{
				using (StreamReader sr = new StreamReader("conf"))
				{
					string line;
					int i = 0;
					while ((line = sr.ReadLine()) != null)
					{

						if (i == 0)
						{
							connString += "server=" + line + ";";
						}
						if (i == 1)
						{
							connString += "user=" + line + ";";
						}
						if (i == 2)
						{
							connString += "database=" + line + ";";
						}
						if (i == 3)
						{
							connString += "port=" + line + ";";
						}
						if (i == 4)
						{
							connString += "password=" + StringUtil.Decrypt(line) + ";";
						}
						i++;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Nie uzupełniono danych serwera. Sprawdź Konfiguracja -> Dane serwera");
			}
			try
			{
				conn.ConnectionString = connString;
				conn.Open();
				
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
				throw new Exception("Nie uzupełniono danych serwera. Sprawdź Konfiguracja -> Dane serwera");
				
			}

		}

		public void CloseConnection()
		{
			

			conn.Close();
		}
	}
}
