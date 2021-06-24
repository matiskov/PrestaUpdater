using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OfficeOpenXml;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace cozabuty_data_updater
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			progressBar1.MarqueeAnimationSpeed = 0;
		}


		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Pliki excel (.xlsx)|*.xlsx|Wszystkie pliki (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;
				openFileDialog.ShowDialog();
				ConnectDB nowe = new ConnectDB();

				nowe.OpenConnection();
				MySqlCommand mySqlCommand = new MySqlCommand();
				mySqlCommand.Connection = nowe.conn;
				if (openFileDialog.FileName == "")
				{
					MessageBox.Show("Nie wybrano pliku lub podano nieprawidłową nazwę");

				}

				else
				{
					FileInfo file = new FileInfo(openFileDialog.FileName);
					ExcelPackage package = new ExcelPackage(file);
					ExcelWorksheet worksheet = package.Workbook.Worksheets["Priorytety"];
					int rows = worksheet.Dimension.Rows;
					int columns = worksheet.Dimension.Columns;

					for (int i = 4; i <= rows; i++)
					{
						progressBar1.Refresh();
						

						for (int j = 16; j <= columns; j++)
						{

							if (worksheet.Cells[i, j].Value != null && int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int wymiar1))
							{
								

								if (worksheet.Cells[1, j].Value != null && int.TryParse(worksheet.Cells[1, j].Value.ToString(), out int wymiar2))
								{

									int.TryParse(worksheet.Cells[i, j].Value.ToString(), out int priorytet);



									string update = "SELECT * FROM prestashop";

									update = "Update ps_category_product SET position = " + priorytet + " WHERE id_product = " + wymiar1 + " and id_category = " + wymiar2;
									mySqlCommand.CommandText = update;
									//MessageBox.Show(update);
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
										
									}
								}
							}
						}
					}
					nowe.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				if (comboBox1.SelectedItem == null)
				{
					MessageBox.Show("Nie wybrano żadnego sklepu, plik nie zostanie pobrany");
				}
				else
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Filter = "Pliki excela (*.xlsx)|*.xlsx";
					saveFileDialog.FilterIndex = 0;
					saveFileDialog.RestoreDirectory = true;
					saveFileDialog.CreatePrompt = true;
					saveFileDialog.Title = "Zapisz plik jako";
					saveFileDialog.ShowDialog();

					if (saveFileDialog.FileName != "")
					{
						ConnectDB prestaDB2 = new ConnectDB();

						MySqlCommand tabSizes = new MySqlCommand();
						tabSizes.Connection = prestaDB2.conn;
						tabSizes.CommandText = "SELECT MAX(parametr.id_feature), MAX(id_product), parametr.id_feature_value,name,value FROM prestashop.ps_feature_product inner join parametr on parametr.id_feature_value = ps_feature_product.id_feature_value";
						prestaDB2.OpenConnection();
						MySqlDataReader tSizes = tabSizes.ExecuteReader();

						int featSize = 0, proSize = 0;

						while (tSizes.Read())
						{
							int.TryParse(tSizes[0].ToString(), out featSize);
							int.TryParse(tSizes[1].ToString(), out proSize);
						}
						prestaDB2.CloseConnection();
						string towary = "";
						ConnectDB prestaDB = new ConnectDB();
						prestaDB.OpenConnection();
						MySqlCommand categoryDownload = new MySqlCommand();
						categoryDownload.Connection = prestaDB.conn;
						if (comboBox1.SelectedItem.ToString() == "Buty")
						{
							categoryDownload.CommandText = "SELECT ps_category_product.id_category, id_product, ps_category_product.position,name,id_parent FROM ps_category_product inner join ps_category_lang on ps_category_product.id_category = ps_category_lang.id_category inner join ps_category on ps_category_product.id_category = ps_category.id_category WHERE id_lang = 1 and id_shop = 1 and id_shop_default = 1 group by id_category order by id_category";
							towary = "SELECT * from produkty_z_id inner join stany on produkty_z_id.id_product = stany.id where stany > 0";
						}
						else if (comboBox1.SelectedItem.ToString() == "Papier")
						{
							categoryDownload.CommandText = "SELECT ps_category_product.id_category, id_product, ps_category_product.position,name,id_parent FROM ps_category_product inner join ps_category_lang on ps_category_product.id_category = ps_category_lang.id_category inner join ps_category on ps_category_product.id_category = ps_category.id_category WHERE id_lang = 1 and id_shop = 1 and id_shop_default = 4 group by id_category order by id_category";
							towary = "SELECT * from produkty_z_id inner join stany on produkty_z_id.id_product = stany.id where stany > 0 and id_shop_default = 4";

						}
						else if (comboBox1.SelectedItem.ToString() == "Folwark")
						{
							categoryDownload.CommandText = "SELECT ps_category_product.id_category, id_product, ps_category_product.position,name,id_parent FROM ps_category_product inner join ps_category_lang on ps_category_product.id_category = ps_category_lang.id_category inner join ps_category on ps_category_product.id_category = ps_category.id_category WHERE id_lang = 1 and id_shop = 1 and id_shop_default = 3 group by id_category order by id_category";
							towary = "SELECT * from produkty_z_id inner join stany on produkty_z_id.id_product = stany.id where stany > 0 and id_shop_default = 3";

						}
						else
						{
							MessageBox.Show("Wybrany sklep nie istnieje");
						}
						MySqlDataReader reader = categoryDownload.ExecuteReader();
						List<Category> kategorie = new List<Category>();

						while (reader.Read())
						{
							kategorie.Add(new Category(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[3].ToString()));

						}
						prestaDB.CloseConnection();

						ConnectDB priotyt = new ConnectDB();
						priotyt.OpenConnection();
						MySqlCommand mySqlCommand = new MySqlCommand();
						mySqlCommand.Connection = priotyt.conn;
						mySqlCommand.CommandText = "SELECT ps_category_product.id_category, id_product, ps_category_product.position,name,id_parent FROM ps_category_product inner join ps_category_lang on ps_category_product.id_category = ps_category_lang.id_category inner join ps_category on ps_category_product.id_category = ps_category.id_category WHERE id_lang = 1 and id_shop = 1 order by id_category";
						string[,] products_priories = new string[proSize + 1000, 3000];
						MySqlDataReader read = mySqlCommand.ExecuteReader();

						while (read.Read())
						{
							int.TryParse(read[0].ToString(), out int cat);
							int.TryParse(read[1].ToString(), out int pro);
							products_priories[pro, cat] = read[2].ToString();
						}
						priotyt.CloseConnection();
						//MessageBox.Show(featSize.ToString() + "     " + proSize.ToString());
						string[] feature = new string[featSize + 10];
						string[,] products = new string[proSize + 1000, featSize + 10];
						ConnectDB prestaDB3 = new ConnectDB();
						MySqlCommand featuresSel = new MySqlCommand();
						featuresSel.Connection = prestaDB3.conn;
						featuresSel.CommandText = "SELECT parametr.id_feature, id_product, parametr.id_feature_value,name,value FROM prestashop.ps_feature_product inner join parametr on parametr.id_feature_value = ps_feature_product.id_feature_value";
						prestaDB3.OpenConnection();
						MySqlDataReader downInfo = featuresSel.ExecuteReader();


						while (downInfo.Read())
						{
							int.TryParse(downInfo[0].ToString(), out int feat);
							int.TryParse(downInfo[1].ToString(), out int prod);
							feature[feat] = downInfo[3].ToString();
							try
							{
								products[prod, feat] = downInfo[4].ToString();
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message);
							}
						}
						prestaDB3.CloseConnection();

						ConnectDB prestaDB4 = new ConnectDB();
						MySqlCommand prices = new MySqlCommand();
						prices.Connection = prestaDB4.conn;
						prices.CommandText = "SELECT id_product, price, reduction FROM ps_specific_price group by id_product";
						prestaDB4.OpenConnection();
						MySqlDataReader pricesd = prices.ExecuteReader();
						float[] cenki = new float[proSize + 1000];
						float[] promki = new float[proSize + 1000];
						while (pricesd.Read())
						{
							int.TryParse(pricesd[0].ToString(), out int id_product);
							float.TryParse(pricesd[1].ToString(), out float price);
							float.TryParse(pricesd[2].ToString(), out float reduction);
							cenki[id_product] = price;
							promki[id_product] = reduction;
						}

						prestaDB4.CloseConnection();
						ConnectDB prestaDB5 = new ConnectDB();
						MySqlCommand catdef = new MySqlCommand();
						catdef.Connection = prestaDB5.conn;
						catdef.CommandText = "SELECT id_product, id_category_default FROM prestashop.ps_product";
						prestaDB5.OpenConnection();
						MySqlDataReader catdefault = catdef.ExecuteReader();
						int[] catdefaulttab = new int[proSize + 1000];


						while (catdefault.Read())
						{
							int.TryParse(catdefault[0].ToString(), out int id_product);
							int.TryParse(catdefault[1].ToString(), out int cat);

							catdefaulttab[id_product] = cat;
						}

						prestaDB5.CloseConnection();
						List<Products> Produkty = new List<Products>();
						ConnectDB productsConn = new ConnectDB();
						productsConn.OpenConnection();
						MySqlCommand productsC = new MySqlCommand();
						productsC.Connection = productsConn.conn;
						productsC.CommandText = towary;
						MySqlDataReader dataReader = productsC.ExecuteReader();

						while (dataReader.Read())
						{

							Produkty.Add(new Products(dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(), dataReader[4].ToString(), dataReader[5].ToString(), dataReader[6].ToString(), dataReader[7].ToString(), dataReader[9].ToString()));
						}
						productsConn.CloseConnection();
						using (ExcelPackage excel = new ExcelPackage())
						{
							excel.Workbook.Worksheets.Add("Kategorie");
							excel.Workbook.Worksheets.Add("Priorytety");
							var worksheet = excel.Workbook.Worksheets["Priorytety"];
							var worksheet2 = excel.Workbook.Worksheets["Kategorie"];
							int z = 16, x = 5;
							worksheet.Cells[1, 15].Value = "Id nadrzędnej";
							worksheet.Cells[2, 15].Value = "Id kategorii";
							worksheet.Cells[3, 15].Value = "Nazwa kategorii";
							worksheet2.Cells[1, 15].Value = "Id nadrzędnej";
							worksheet2.Cells[2, 15].Value = "Id kategorii";
							worksheet2.Cells[3, 15].Value = "Nazwa kategorii";
							string[] tabsa = new string[2000];
							foreach (Category b in kategorie)
							{
								int.TryParse(b.GetIdCategory(), out int index);
								tabsa[index] = b.GetNameCategory();
							}
							foreach (Category b in kategorie)
							{
								progressBar1.Refresh();
								int.TryParse(b.GetIDParent(), out int index);
								if (index < 10)
								{

								}
								else if (index < 96)
								{
									worksheet.Cells[2, z].Value = "Buty damskie";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[2, z].Value = "Buty damskie";

								}
								else if (index < 161)
								{
									worksheet.Cells[2, z].Value = "W panelu";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[2, z].Value = "W panelu";

								}
								else if (index < 196)
								{
									worksheet.Cells[2, z].Value = "Wyprzedaz";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[2, z].Value = "Wyprzedaz";
								}
								else if (index < 212)
								{
									worksheet.Cells[2, z].Value = "Buty męskie";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[2, z].Value = "Buty męskie";
								}
								else if (index < 240)
								{
									worksheet.Cells[2, z].Value = "Producenci";
									worksheet2.Cells[2, z].Value = "Producenci";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
								}
								else if (index < 266)
								{
									worksheet.Cells[2, z].Value = "Hity";
									worksheet2.Cells[2, z].Value = "Hity";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
								}
								else if (index < 275)
								{
									worksheet.Cells[2, z].Value = "Top frazy";
									worksheet2.Cells[2, z].Value = "Top frazy";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
								}
								else if (index < 290)
								{
									worksheet.Cells[2, z].Value = "Akcesoria";
									worksheet2.Cells[2, z].Value = "Akcesoria";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
								}
								else if (index < 296)
								{
									worksheet.Cells[2, z].Value = "Galanteria";
									worksheet2.Cells[2, z].Value = "Galanteria";
									worksheet.Cells[3, z].Value = tabsa[index];
									worksheet2.Cells[3, z].Value = tabsa[index];
								}
								//else if (index < 757)
								//{
								//	worksheet.Cells[2, z].Value = "Papier główna > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Papier główna  > " + tabsa[index];
								//}
								//else if (index < 758)
								//{
								//	worksheet.Cells[2, z].Value = "Papiery ozdobne> " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Papiery ozdobne> " + tabsa[index];
								//}
								//else if (index < 759)
								//{
								//	worksheet.Cells[2, z].Value = "Wstążki > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Wstążki > " + tabsa[index];
								//}
								//else if (index < 869)
								//{
								//	worksheet.Cells[2, z].Value = "Kokarda > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Kokarda > " + tabsa[index];
								//}
								//else if (index < 870)
								//{
								//	worksheet.Cells[2, z].Value = "Kolory > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Kolory > " + tabsa[index];
								//}
								//else if (index < 871)
								//{
								//	worksheet.Cells[2, z].Value = "Rozmiary > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Rozmiary > " + tabsa[index];
								//}
								//else if (index < 900)
								//{
								//	worksheet.Cells[2, z].Value = "Motywy > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Motywy > " + tabsa[index];
								//}
								//else if (index < 901)
								//{
								//	worksheet.Cells[2, z].Value = "Dodatki > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Dodatki > " + tabsa[index];
								//}
								//else if (index == 901)
								//{
								//	worksheet.Cells[2, z].Value = "Zestawy > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Zestawy > " + tabsa[index];
								//}
								//else
								//{
								//	worksheet.Cells[2, z].Value = "Zgłosić do zmatchowania  > " + tabsa[index];
								//	worksheet2.Cells[2, z].Value = "Zgłosić do zmatchowania > " + tabsa[index];
								//}
								int.TryParse(b.GetIdCategory(), out int test1);
								if (test1 > 9 && test1 < 96)
								{
									worksheet.Cells[1, z].Value = b.GetIdCategory();
									worksheet.Cells[4, z].Value = b.GetNameCategory();
									worksheet2.Cells[1, z].Value = b.GetIdCategory();
									worksheet2.Cells[4, z].Value = b.GetNameCategory();
									z++;
								}
								else if (checkBox1.Checked && test1 >= 96 && test1 < 161) {
									worksheet.Cells[1, z].Value = b.GetIdCategory();
									worksheet.Cells[4, z].Value = b.GetNameCategory();
									worksheet2.Cells[1, z].Value = b.GetIdCategory();
									worksheet2.Cells[4, z].Value = b.GetNameCategory();
									z++;
								}
								else if (test1 > 161)
								{
									worksheet.Cells[1, z].Value = b.GetIdCategory();
									worksheet.Cells[4, z].Value = b.GetNameCategory();
									worksheet2.Cells[1, z].Value = b.GetIdCategory();
									worksheet2.Cells[4, z].Value = b.GetNameCategory();
									z++;
								}
							}
							worksheet.Cells[4, 1].Value = "Id produktu";
							worksheet.Cells[4, 2].Value = "Nazwa produktu";
							worksheet.Cells[4, 3].Value = "Kategoria";
							worksheet.Cells[4, 4].Value = "Cena";
							worksheet.Cells[4, 5].Value = "Przed przeceną";
							worksheet.Cells[4, 6].Value = "Producent";
							worksheet.Cells[4, 7].Value = "Kod na karcie";
							worksheet.Cells[4, 8].Value = "Kolor";
							worksheet.Cells[4, 9].Value = "Cholewka";
							worksheet.Cells[4, 10].Value = "Rodzaj obcasa";
							worksheet.Cells[4, 11].Value = "Wysokość obcasa";
							worksheet.Cells[4, 12].Value = "Wariant";
							worksheet.Cells[4, 13].Value = "Ilość";
							worksheet.Cells[4, 14].Value = "Do zagospodarowania";
							worksheet2.Cells[4, 1].Value = "Id produktu";
							worksheet2.Cells[4, 2].Value = "Nazwa produktu";
							worksheet2.Cells[4, 3].Value = "Kategoria";
							worksheet2.Cells[4, 4].Value = "Cena";
							worksheet2.Cells[4, 5].Value = "Przed przeceną";
							worksheet2.Cells[4, 6].Value = "Producent";
							worksheet2.Cells[4, 7].Value = "Kod na karcie";
							worksheet2.Cells[4, 8].Value = "Kolor";
							worksheet2.Cells[4, 9].Value = "Cholewka";
							worksheet2.Cells[4, 10].Value = "Rodzaj obcasa";
							worksheet2.Cells[4, 11].Value = "Wysokość obcasa";
							worksheet2.Cells[4, 12].Value = "Wariant";
							worksheet2.Cells[4, 13].Value = "Ilość";
							worksheet2.Cells[4, 14].Value = "Do zagospodarowania";
							//MessageBox.Show(kategorie.Count.ToString());
							foreach (Products a in Produkty)
							{
								int y = 16;
								int.TryParse(a.GetID(), out int id_prod);
								while (worksheet.Cells[1, y].Value != null)
								{
									int.TryParse(worksheet.Cells[1, y].Value.ToString(), out int result);
									if (products_priories[id_prod, result] != null)
									{

										worksheet.Cells[x, y].Value = int.Parse(products_priories[id_prod, result]);
										worksheet2.Cells[x, y].Value = (int)1;

									}
									y++;
								}
								progressBar1.Refresh();
								worksheet.Cells[x, 1].Value = int.Parse(a.GetID());
								worksheet.Cells[x, 2].Value = a.GetName();
								if (catdefaulttab[id_prod] < 121)
								{
									worksheet.Cells[x, 3].Value = "Buty damskie | " + tabsa[catdefaulttab[id_prod]];
									worksheet2.Cells[x, 3].Value = "Buty damskie | " + tabsa[catdefaulttab[id_prod]];
								}
								else if (catdefaulttab[id_prod] < 134)
								{
									worksheet.Cells[x, 3].Value = "Buty męskie | " + tabsa[catdefaulttab[id_prod]];
									worksheet2.Cells[x, 3].Value = "Buty męskie | " + tabsa[catdefaulttab[id_prod]];

								}
								else if (catdefaulttab[id_prod] == 134)
								{
									worksheet.Cells[x, 3].Value = "Kategoria tymczasowa";
									worksheet2.Cells[x, 3].Value = "Kategoria tymczasowa";
								}
								else if (catdefaulttab[id_prod] < 150)
								{
									worksheet.Cells[x, 3].Value = "Akcesoria  | " + tabsa[catdefaulttab[id_prod]];
									worksheet2.Cells[x, 3].Value = "Akcesoria  | " + tabsa[catdefaulttab[id_prod]];

								}
								else if (catdefaulttab[id_prod] < 159)
								{
									worksheet.Cells[x, 3].Value = "Galanteria  | " + tabsa[catdefaulttab[id_prod]];
									worksheet2.Cells[x, 3].Value = "Galanteria  | " + tabsa[catdefaulttab[id_prod]];

								}
								else if (catdefaulttab[id_prod] < 161)
								{
									worksheet.Cells[x, 3].Value = "Kategoria tymczasowa";
									worksheet2.Cells[x, 3].Value = "Kategoria tymczasowa";
								}
								else
								{
									worksheet.Cells[x, 3].Value = tabsa[catdefaulttab[id_prod]];
									worksheet2.Cells[x, 3].Value = tabsa[catdefaulttab[id_prod]];
								}
								if (cenki[id_prod] != 0)
								{
									worksheet.Cells[x, 4].Value = Math.Round(1.23 * cenki[id_prod] * (1 - promki[id_prod]), 2);
									worksheet.Cells[x, 5].Value = Math.Round(1.23 * cenki[id_prod], 2);
								}
								else
								{
									float.TryParse(a.GetPrice(), out float pricee);
									worksheet.Cells[x, 4].Value = Math.Round(1.23 * pricee, 2);
									worksheet.Cells[x, 5].Value = Math.Round(1.23 * pricee, 2);
								}
								worksheet.Cells[x, 6].Value = a.GetProducent();
								worksheet.Cells[x, 7].Value = a.GetReference();
								worksheet.Cells[x, 8].Value = products[id_prod, 3];
								worksheet.Cells[x, 9].Value = products[id_prod, 5];
								worksheet.Cells[x, 10].Value = products[id_prod, 10];
								worksheet.Cells[x, 11].Value = products[id_prod, 15];
								worksheet.Cells[x, 12].Value = products[id_prod, 20];
								worksheet.Cells[x, 13].Value = int.Parse(a.GetStany());
								worksheet2.Cells[x, 1].Value = int.Parse(a.GetID());
								worksheet2.Cells[x, 2].Value = a.GetName();

								if (cenki[id_prod] != 0)
								{
									worksheet2.Cells[x, 4].Value = Math.Round(1.23 * cenki[id_prod] * (1 - promki[id_prod]), 2);
									worksheet2.Cells[x, 5].Value = Math.Round(1.23 * cenki[id_prod], 2);
								}
								else
								{
									float.TryParse(a.GetPrice(), out float pricee);
									worksheet2.Cells[x, 4].Value = Math.Round(1.23 * pricee, 2);
									worksheet2.Cells[x, 5].Value = Math.Round(1.23 * pricee, 2);
								}
								worksheet2.Cells[x, 6].Value = a.GetProducent();
								worksheet2.Cells[x, 7].Value = a.GetReference();
								worksheet2.Cells[x, 8].Value = products[id_prod, 3];
								worksheet2.Cells[x, 9].Value = products[id_prod, 5];
								worksheet2.Cells[x, 10].Value = products[id_prod, 10];
								worksheet2.Cells[x, 11].Value = products[id_prod, 15];
								worksheet2.Cells[x, 12].Value = products[id_prod, 20];
								worksheet2.Cells[x, 13].Value = int.Parse(a.GetStany());
								//worksheet.Cells[x, 14].Value = "Do zagospodarowania";
								x++;
							}
							try
							{
								FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
								excel.SaveAs(excelFile);

							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message);
							}
							finally
							{
								progressBar1.MarqueeAnimationSpeed = 0;
							}
						}

					}

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				MessageBox.Show("Zakończono operację");
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}
		private void daneSerweraToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var Window = new Form2();
			Window.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Pliki excel (.xlsx)|*.xlsx|Wszystkie pliki (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;
				openFileDialog.ShowDialog();

				if (openFileDialog.FileName == "")
				{
					MessageBox.Show("Nie wybrano pliku lub podano nieprawidłową nazwę");

				}

				else
				{
					FileInfo file = new FileInfo(@openFileDialog.FileName);
					ExcelPackage package = new ExcelPackage(file);
					ExcelWorksheet worksheet = package.Workbook.Worksheets["Kategorie"];
					int rows = worksheet.Dimension.Rows;
					int columns = worksheet.Dimension.Columns;
					ConnectDB nowe2 = new ConnectDB();
					nowe2.OpenConnection();
					MySqlCommand mySqlCommand = new MySqlCommand();
					mySqlCommand.Connection = nowe2.conn;
					for (int i = 4; i <= rows; i++)
					{
						progressBar1.Refresh();
						for (int j = 16; j <= columns; j++)
						{
							if (worksheet.Cells[i, j].Value != null)
							{
								int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int wymiar1);
								if (worksheet.Cells[1, j].Value != null && int.TryParse(worksheet.Cells[1, j].Value.ToString(), out int wymiar2))
								{
									int.TryParse(worksheet.Cells[i, j].Value.ToString(), out int priorytet);
									string content = worksheet.Cells[i, j].Value.ToString();
									string update = "SELECT * FROM prestashop";

									if (priorytet == 0)
									{
										update = "DELETE FROM ps_category_product WHERE id_category=" + wymiar2 + " and id_product=" + wymiar1;
									}
									if (priorytet == 1)
									{
										update = "INSERT INTO ps_category_product(id_category, id_product, position) VALUES(" + wymiar2 + "," + wymiar1 + ",10001)";
									}
									mySqlCommand.CommandText = update;

									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{
										ex.Message.ToString();
									}

								}


							}
						}
					}
					nowe2.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "Pliki excela (*.xlsx)|*.xlsx";
				saveFileDialog.FilterIndex = 0;
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.CreatePrompt = true;
				saveFileDialog.Title = "Zapisz plik jako";
				saveFileDialog.ShowDialog();
				if (saveFileDialog.FileName == "")
				{
					throw new Exception("Nie wybrano pliku");
				}
				ConnectDB prestaDB2 = new ConnectDB();

				MySqlCommand tabSizes = new MySqlCommand();
				tabSizes.Connection = prestaDB2.conn;
				tabSizes.CommandText = "SELECT MAX(parametr.id_feature), MAX(id_product), parametr.id_feature_value,name,value FROM prestashop.ps_feature_product inner join parametr on parametr.id_feature_value = ps_feature_product.id_feature_value";
				prestaDB2.OpenConnection();
				MySqlDataReader tSizes = tabSizes.ExecuteReader();

				int featSize = 0, proSize = 0;

				while (tSizes.Read())
				{
					int.TryParse(tSizes[0].ToString(), out featSize);
					int.TryParse(tSizes[1].ToString(), out proSize);
				}
				prestaDB2.CloseConnection();
				string[] feature = new string[featSize + 100];
				string[,] products = new string[proSize + 1000, featSize + 100];
				ConnectDB prestaDB3 = new ConnectDB();
				MySqlCommand featuresSel = new MySqlCommand();
				featuresSel.Connection = prestaDB3.conn;
				featuresSel.CommandText = "SELECT parametr.id_feature, id_product, parametr.id_feature_value,name,value FROM prestashop.ps_feature_product inner join parametr on parametr.id_feature_value = ps_feature_product.id_feature_value";
				prestaDB3.OpenConnection();
				MySqlDataReader downInfo = featuresSel.ExecuteReader();


				while (downInfo.Read())
				{
					int.TryParse(downInfo[0].ToString(), out int feat);
					int.TryParse(downInfo[1].ToString(), out int prod);
					feature[feat] = downInfo[3].ToString();
					try
					{
						products[prod, feat] = downInfo[4].ToString();
					}
					catch (Exception ex2)
					{
						MessageBox.Show(ex2.Message);
					}
				}
				prestaDB3.CloseConnection();

				string[] stany = new string[proSize + 5000];
				ConnectDB connect = new ConnectDB();
				MySqlCommand mySqlCommand = new MySqlCommand();
				mySqlCommand.Connection = connect.conn;
				mySqlCommand.CommandText = "select * from stany";
				connect.OpenConnection();
				MySqlDataReader reader = mySqlCommand.ExecuteReader();
				while (reader.Read())
				{
					int.TryParse(reader[0].ToString(), out int id);
					stany[id] = reader[1].ToString();
				}
				connect.CloseConnection();

				string[] nazwy_produktow = new string[proSize + 1000];
				ConnectDB connect1 = new ConnectDB();
				MySqlCommand mySqlCommand1 = new MySqlCommand();
				mySqlCommand1.Connection = connect1.conn;
				mySqlCommand1.CommandText = "SELECT id_product, name FROM ps_product_lang where id_lang = 1 and id_shop = 1";
				connect1.OpenConnection();

				MySqlDataReader reader1 = mySqlCommand1.ExecuteReader();

				while (reader1.Read())
				{
					int.TryParse(reader1[0].ToString(), out int id);
					nazwy_produktow[id] = reader1[1].ToString();
				}
				connect1.CloseConnection();

				using (ExcelPackage excel = new ExcelPackage())
				{
					excel.Workbook.Worksheets.Add("Parametry");
					var worksheet = excel.Workbook.Worksheets[1];

					worksheet.Cells[2, 1].Value = "Id produktu";
					worksheet.Cells[2, 2].Value = "Nazwa produktu";
					worksheet.Cells[2, 3].Value = "Stan";
					int col = 4;
					for (int j = 0; j <= featSize + 1; j++)
					{

						if (feature[j] != "")
						{
							worksheet.Cells[1, col].Value = j;
							worksheet.Cells[2, col].Value = feature[j];
							col++;
						}


					}
					int row = 3;

					for (int i = 0; i <= proSize + 1; i++)
					{
						int col2 = 4;
						progressBar1.Refresh();
						if (nazwy_produktow[i] != null)
						{
							for (int j = 0; j <= featSize + 1; j++)
							{

								worksheet.Cells[row, 1].Value = i;
								worksheet.Cells[row, 2].Value = nazwy_produktow[i];
								worksheet.Cells[row, 3].Value = stany[i];
								worksheet.Cells[row, col2].Value = products[i, j];
								col2++;


							}
							progressBar1.Refresh();
							row++;
						}

					}

					FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
					excel.SaveAs(excelFile);


				}
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Pliki excel (.xlsx)|*.xlsx|Wszystkie pliki (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;
				openFileDialog.ShowDialog();
				if (comboBox2.SelectedItem == null) { 
					throw new Exception("Nie wybrano akcji.");
				}
				if (openFileDialog.FileName == "")
				{
					MessageBox.Show("Nie wybrano pliku lub podano nieprawidłową nazwę");

				}

				else
				{
					ConnectDB connect1 = new ConnectDB();
					MySqlCommand test123 = new MySqlCommand();
					connect1.OpenConnection();
					test123.Connection = connect1.conn;
					test123.CommandText = "Select * from ps_feature_value";
					MySqlDataReader test1234 = test123.ExecuteReader();
					int[] test12345 = new int[100000];
					while (test1234.Read()) {
						int.TryParse(test1234[0].ToString(), out int id_value);
						int.TryParse(test1234[1].ToString(), out int id_feature);
						test12345[id_value] = id_feature;
					}
					connect1.CloseConnection();
					ConnectDB connectDB = new ConnectDB();
					MySqlCommand sqlCommand = new MySqlCommand();
					connectDB.OpenConnection();
					sqlCommand.Connection = connectDB.conn;
					sqlCommand.CommandText = "SELECT id_feature_value, value FROM ps_feature_value_lang WHERE id_lang=1";
					MySqlDataReader reader = sqlCommand.ExecuteReader();
					string[] id_param = new string[50000];
					while (reader.Read())
					{
						int.TryParse(reader[0].ToString(), out int ind);
						id_param[ind] = reader[1].ToString();

					}


					FileInfo file = new FileInfo(@openFileDialog.FileName);
					ExcelPackage package = new ExcelPackage(file);
					ExcelWorksheet worksheet = package.Workbook.Worksheets["Parametry"];
					int rows = worksheet.Dimension.Rows;
					int columns = worksheet.Dimension.Columns;
					ConnectDB nowe2 = new ConnectDB();
					nowe2.OpenConnection();
					MySqlCommand mySqlCommand = new MySqlCommand();
					mySqlCommand.Connection = nowe2.conn;
					for (int i = 3; i <= rows; i++)
					{
						progressBar1.Refresh();
						for (int j = 5; j <= columns; j++)
						{
							if (worksheet.Cells[i, j].Value != null)
							{
								int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int wymiar1);
								int.TryParse(worksheet.Cells[1, j].Value.ToString(), out int wymiar2);
								int id_para = 0;
								string content = worksheet.Cells[i, j].Value.ToString();
								
								
									for (int h = 0; h < id_param.Length; h++)
									{
										if (wymiar2 == test12345[h])
										{
											if (content == id_param[h])
											{
												id_para = h;
												break;
											}
										}
									}
								
								string update = "";
								if (comboBox2.SelectedItem.ToString() == "Dodaj parametry")
								{
									update = "INSERT INTO ps_feature_product(id_feature_value,id_feature,id_product) VALUES (" + id_para + "," + wymiar2 + "," + wymiar1 + ")";
								}
								else if (comboBox2.SelectedItem.ToString() == "Aktualizuj parametry")
								{
									update = "UPDATE ps_feature_product SET id_feature_value = " + id_para + " WHERE id_feature = " + wymiar2 + " and id_product = " + wymiar1;
								}
								else if (comboBox2.SelectedItem.ToString() == "Dodaj, usuń lub aktualizuj parametry")
								{
									bool exist = false;
									ConnectDB connect = new ConnectDB();
									connect.OpenConnection();
									MySqlCommand test = new MySqlCommand();
									test.Connection = connect.conn;
									test.CommandText = "Select * from ps_feature_product WHERE id_feature = " + wymiar2 + " and id_product = " + wymiar1;
									//MessageBox.Show(test.CommandText);
									MySqlDataReader readerlol = test.ExecuteReader();
									while (readerlol.Read()){
										exist = true;
										
									}
									
									connect.CloseConnection();
									if (content == "0" && exist == true){
										update = "DELETE FROM ps_feature_product WHERE id_feature = " + wymiar2 + " and id_product = " + wymiar1;
									}
									else if (exist == true)
									{
										update = "UPDATE ps_feature_product SET id_feature_value = " + id_para + " WHERE id_feature = " + wymiar2 + " and id_product = " + wymiar1;
									}
									else if (exist == false)
									{
										update = "INSERT INTO ps_feature_product(id_feature_value,id_feature,id_product) VALUES (" + id_para + "," + wymiar2 + "," + wymiar1 + ")";
									}
								}
								else if(comboBox2.SelectedItem.ToString() == "Usuń parametry")
								{
									update = "DELETE FROM ps_feature_product WHERE id_feature = " + wymiar2 + " and id_product = " + wymiar1;
								}
								//MessageBox.Show(update);
								mySqlCommand.CommandText = update;

								try
								{
									mySqlCommand.ExecuteNonQuery();
								}
								catch (MySqlException ex)
								{
									using (StreamWriter sw = new StreamWriter("log_params.txt"))
									{
										sw.WriteLine(ex.Message.ToString());
									}
									
								}
							}
						}
					}
					nowe2.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				if (comboBox1.SelectedItem == null)
				{
					MessageBox.Show("Nie wybrano żadnego sklepu, plik nie zostanie pobrany");
				}
				else
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Filter = "Pliki excela (*.xlsx)|*.xlsx";
					saveFileDialog.FilterIndex = 0;
					saveFileDialog.RestoreDirectory = true;
					saveFileDialog.CreatePrompt = true;
					saveFileDialog.Title = "Zapisz plik jako";
					saveFileDialog.ShowDialog();

					if (saveFileDialog.FileName != "")
					{
						ConnectDB connect = new ConnectDB();
						connect.OpenConnection();
						MySqlCommand mySql = new MySqlCommand();
						mySql.Connection = connect.conn;
						mySql.CommandText = "select * from wymiary";
						MySqlDataReader reader = mySql.ExecuteReader();
						List<wymiary> produkty = new List<wymiary>();
						while (reader.Read())
						{
							produkty.Add(new wymiary(reader[1].ToString(), reader[0].ToString(), reader[5].ToString(), reader[3].ToString(), reader[2].ToString(), reader[4].ToString()));
						}
						connect.CloseConnection();

						using (ExcelPackage excel = new ExcelPackage())
						{
							excel.Workbook.Worksheets.Add("Grupy towarów i wagi");

							var worksheet = excel.Workbook.Worksheets["Grupy towarów i wagi"];


							worksheet.Cells[1, 1].Value = "Id produktu";
							worksheet.Cells[1, 2].Value = "Nazwa produktu";
							worksheet.Cells[1, 3].Value = "Id grupy";
							worksheet.Cells[1, 4].Value = "Głębokość";
							worksheet.Cells[1, 5].Value = "Szerokość";
							worksheet.Cells[1, 6].Value = "Wysokość";
							worksheet.Cells[1, 7].Value = "Waga";

							int i = 2;
							foreach (wymiary s in produkty)
							{
								progressBar1.Refresh();
								worksheet.Cells[i, 1].Value = s.GetID();
								worksheet.Cells[i, 2].Value = s.GetName();
								//worksheet.Cells[i, 3].Value = s.GetID();
								worksheet.Cells[i, 4].Value = s.GetDepth();
								worksheet.Cells[i, 5].Value = s.GetWidth();
								worksheet.Cells[i, 6].Value = s.GetHeight();
								worksheet.Cells[i, 7].Value = s.GetWeight();
								i++;
							}
							try
							{
								FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
								excel.SaveAs(excelFile);

							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message);
							}

						}

					}

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Pliki excel (.xlsx)|*.xlsx|Wszystkie pliki (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;
				openFileDialog.ShowDialog();

				if (openFileDialog.FileName == "")
				{
					MessageBox.Show("Nie wybrano pliku lub podano nieprawidłową nazwę");

				}

				else
				{

					FileInfo file = new FileInfo(@openFileDialog.FileName);
					ExcelPackage package = new ExcelPackage(file);
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					int rows = worksheet.Dimension.Rows;
					MessageBox.Show(rows.ToString());
					int columns = worksheet.Dimension.Columns;
					ConnectDB nowe2 = new ConnectDB();
					nowe2.OpenConnection();
					MySqlCommand mySqlCommand = new MySqlCommand();
					mySqlCommand.Connection = nowe2.conn;
					for (int i = 2; i <= rows; i++)
					{
						progressBar1.Refresh();
						if (worksheet.Cells[i, 1].Value != null)
						{
							for (int j = 2; j < rows; j++)
							{

								int.TryParse(worksheet.Cells[i, 1].Value.ToString(), out int id_prod);
								int.TryParse(worksheet.Cells[j, 1].Value.ToString(), out int id_prod_2);
								string id_grupy = worksheet.Cells[i, 3].Value.ToString();
								string id_grupy_next = worksheet.Cells[j, 3].Value.ToString();

								string insert = "SELECT * FROM ps_alias";
								string select = "SELECT * FROM ps_accessory";
								if (id_grupy == id_grupy_next)
								{
									insert = "INSERT INTO ps_accessory(id_product_1, id_product_2) VALUES(" + id_prod + ", " + id_prod_2 + ")";
									select = "SELECT * from ps_accessory WHERE id_product_1 = " + id_prod + " and id_product_2 = " + id_prod_2;

									mySqlCommand.CommandText = select;
									MySqlDataReader reader = mySqlCommand.ExecuteReader();
									bool result = false;
									if (reader.Read())
									{
										result = true;
										//MessageBox.Show(reader[0].ToString());
									}
									reader.Close();
									try
									{
										if (result == false)
										{
											using (StreamWriter sw = new StreamWriter("logs.txt"))
											{
												sw.WriteLine(insert);
											}
											//MessageBox.Show("Dodaję...");
											mySqlCommand.CommandText = insert;
											progressBar1.Refresh();
											mySqlCommand.ExecuteNonQuery();
										}

									}
									catch (MySqlException ex)
									{

										MessageBox.Show(ex.Message.ToString());
									}
									using (StreamWriter sw = new StreamWriter("logs.txt"))
									{
										sw.WriteLine(id_prod + "," + id_prod_2);
									}
								}
							}
						}

					}
					nowe2.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void button9_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Pliki excel (.xlsx)|*.xlsx|Wszystkie pliki (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;
				openFileDialog.ShowDialog();

				if (openFileDialog.FileName == "")
				{
					MessageBox.Show("Nie wybrano pliku lub podano nieprawidłową nazwę");

				}

				else
				{

					FileInfo file = new FileInfo(@openFileDialog.FileName);
					ExcelPackage package = new ExcelPackage(file);
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					int rows = worksheet.Dimension.Rows;
					int columns = worksheet.Dimension.Columns;
					ConnectDB nowe2 = new ConnectDB();
					nowe2.OpenConnection();
					MySqlCommand mySqlCommand = new MySqlCommand();
					mySqlCommand.Connection = nowe2.conn;
					for (int i = 2; i <= rows; i++)
					{
						progressBar1.Refresh();
						if (worksheet.Cells[i, 1].Value != null)
						{
							for (int j = 2; j < columns; j++)
							{

								int id_conversion_group = 0;
								int id_conversion_chart = 0;
								if (j <= 23)
								{
									id_conversion_group = 10 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Długość wkładki w której mieści się stopa średniej tęgości (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								else if (j < 32)
								{
									id_conversion_group = 11 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Obwód cholewki (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								else if (j < 39)
								{
									id_conversion_group = 12 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Wysokość cholewki (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								else if (j < 58)
								{
									id_conversion_group = 13 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Zmierzona długość wkładki wewnątrz buta (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								int.TryParse(worksheet.Cells[2, j].Value.ToString(), out int id_attribute);
								string insert = "select * from ps_product";
								string insert2 = "select * from ps_product";
								if (worksheet.Cells[i, j].Value != null)
								{
									int.TryParse(worksheet.Cells[i, j].Value.ToString(), out int value);
									insert = "INSERT INTO ps_conversion_attributes (id_conversion_group,id_attribute, value) VALUES (" + id_conversion_group + ", " + id_attribute + ", " + value + ")";
									insert2 = "INSERT INTO ps_conversion_chart(id_conversion_chart,id_attribute_group, all_products, active, display_method, custom_img_url, type) VALUES (" + id_conversion_chart + "18, 0,1," + "inpage" + "," + "," + "regular" + ")";
									mySqlCommand.CommandText = insert2;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}

								}
								string insert3 = "INSERT INTO ps_conversion_chart_comments(id_chart, id_lang, chart_name) VALUES (" + id_conversion_chart + "," + "1" + worksheet.Cells[i, 1].Value.ToString() + ")";
								mySqlCommand.CommandText = insert3;
								progressBar1.Refresh();
								try
								{
									mySqlCommand.ExecuteNonQuery();
								}
								catch (MySqlException ex)
								{

									ex.Message.ToString();
								}
								//MessageBox.Show(update);
								mySqlCommand.CommandText = insert;
								progressBar1.Refresh();
								try
								{
									mySqlCommand.ExecuteNonQuery();
								}
								catch (MySqlException ex)
								{

									ex.Message.ToString();
								}
							}
						}

					}
					nowe2.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void button9_Click_1(object sender, EventArgs e)
		{
			try
			{
				progressBar1.MarqueeAnimationSpeed = 100;
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "Pliki excel (.xlsx)|*.xlsx|Wszystkie pliki (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.Multiselect = false;
				openFileDialog.ShowDialog();

				if (openFileDialog.FileName == "")
				{
					MessageBox.Show("Nie wybrano pliku lub podano nieprawidłową nazwę");

				}

				else
				{

					FileInfo file = new FileInfo(@openFileDialog.FileName);
					ExcelPackage package = new ExcelPackage(file);
					ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
					int rows = worksheet.Dimension.Rows;
					int columns = worksheet.Dimension.Columns;
					ConnectDB nowe2 = new ConnectDB();
					nowe2.OpenConnection();
					MySqlCommand mySqlCommand = new MySqlCommand();
					mySqlCommand.Connection = nowe2.conn;
					for (int i = 2; i <= rows; i++)
					{
						progressBar1.Refresh();
						if (worksheet.Cells[i, 1].Value != null)
						{

							//MessageBox.Show(update);
							for (int j = 2; j < columns; j++)
							{

								int id_conversion_group = 0;
								int id_conversion_chart = 0;
								if (j <= 23)
								{
									id_conversion_group = 10 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Długość wkładki w której mieści się stopa średniej tęgości (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								else if (j < 32)
								{
									id_conversion_group = 11 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Obwód cholewki (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								else if (j < 39)
								{
									id_conversion_group = 12 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Wysokość cholewki (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								else if (j < 58)
								{
									id_conversion_group = 13 + i;
									id_conversion_chart = 8 + i;
									string inserti = "INSERT Into ps_conversion_groups(name, id_conversion_chart) VALUES (" + "Zmierzona długość wkładki wewnątrz buta (cm)" + "," + id_conversion_chart + ")";
									mySqlCommand.CommandText = inserti;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}
								}
								string insert3 = "INSERT INTO ps_conversion_chart_comments(id_chart, id_lang, chart_name) VALUES (" + id_conversion_chart + "," + "1" + worksheet.Cells[i, 1].Value.ToString() + ")";
								mySqlCommand.CommandText = insert3;
								progressBar1.Refresh();
								try
								{
									mySqlCommand.ExecuteNonQuery();
								}
								catch (MySqlException ex)
								{

									ex.Message.ToString();
								}
								int.TryParse(worksheet.Cells[2, j].Value.ToString(), out int id_attribute);
								string insert = "select * from ps_product";
								string insert2 = "select * from ps_product";
								if (worksheet.Cells[i, j].Value != null)
								{
									int.TryParse(worksheet.Cells[i, j].Value.ToString(), out int value);
									insert = "INSERT INTO ps_conversion_attributes (id_conversion_group,id_attribute, value) VALUES (" + id_conversion_group + ", " + id_attribute + ", " + value + ")";
									insert2 = "INSERT INTO ps_conversion_chart(id_conversion_chart,id_attribute_group, all_products, active, display_method, custom_img_url, type) VALUES (" + id_conversion_chart + "18, 0,1," + "inpage" + "," + "," + "regular" + ")";
									mySqlCommand.CommandText = insert2;
									progressBar1.Refresh();
									try
									{
										mySqlCommand.ExecuteNonQuery();
									}
									catch (MySqlException ex)
									{

										ex.Message.ToString();
									}

								}


							}
						}

					}
					nowe2.CloseConnection();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				progressBar1.MarqueeAnimationSpeed = 0;
				MessageBox.Show("Zakończono operację");
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			try
			{

				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "Pliki excela (*.xlsx)|*.xlsx";
				saveFileDialog.FilterIndex = 0;
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.CreatePrompt = true;
				saveFileDialog.Title = "Zapisz plik jako";
				saveFileDialog.ShowDialog();
			
				if (saveFileDialog.FileName != "")
				{
					ConnectDB prestaDB2 = new ConnectDB();

					MySqlCommand tabSizes = new MySqlCommand();
					tabSizes.Connection = prestaDB2.conn;
					tabSizes.CommandText = "SELECT * FROM ps_tag";
					prestaDB2.OpenConnection();
					MySqlDataReader tSizes = tabSizes.ExecuteReader();

					string[] Tags = new string[1000];

					while (tSizes.Read())
					{
						int.TryParse(tSizes[0].ToString(), out int id_tagu);
						string name = tSizes[2].ToString();
						Tags[id_tagu] = name;
					}
					prestaDB2.CloseConnection();

					ConnectDB prestaDB3 = new ConnectDB();

					MySqlCommand produtyTagi = new MySqlCommand();
					produtyTagi.Connection = prestaDB3.conn;
					produtyTagi.CommandText = "SELECT * FROM ps_tag";
					prestaDB3.OpenConnection();
					MySqlDataReader produtyTagis = produtyTagi.ExecuteReader();

					int[] TagsProducts = new int[100000];

					while (produtyTagis.Read())
					{
						int.TryParse(produtyTagis[0].ToString(), out int id_produktu);
						int.TryParse(produtyTagis[1].ToString(), out int id_tagu);
						TagsProducts[id_produktu] = id_tagu;
					}
					prestaDB3.CloseConnection();

					ConnectDB prestaDB4 = new ConnectDB();

					MySqlCommand nazwy = new MySqlCommand();
					nazwy.Connection = prestaDB4.conn;
					nazwy.CommandText = "SELECT id_product, name FROM ps_product_lang Where id_shop = 1 and id_lang = 1";
					prestaDB4.OpenConnection();
					MySqlDataReader produtyNazwy = nazwy.ExecuteReader();

					string[] produtyNazw = new string[100000];

					while (produtyNazwy.Read())
					{
						int.TryParse(produtyNazwy[0].ToString(), out int id_produktu);
						string name = produtyNazwy[1].ToString();
						produtyNazw[id_produktu] = name;
					}
					prestaDB4.CloseConnection();

					using (ExcelPackage excel = new ExcelPackage())
					{
						excel.Workbook.Worksheets.Add("Tagi");

						var worksheet = excel.Workbook.Worksheets["Tagi"];
						worksheet.Cells[2, 1].Value = "Id produktu";
						worksheet.Cells[2, 2].Value = "Nazwa produktu";
						for (int y = 3; y < 1000; y++)
						{
							if (Tags[y] != null)
							{
								worksheet.Cells[1, y].Value = y;
								worksheet.Cells[2, y].Value = Tags[y];
							}
						}


						for (int i = 3; i < 20000; i++)
						{


								if (TagsProducts[i] > 0)
								{
									worksheet.Cells[i, 1].Value = i;
									worksheet.Cells[i, 2].Value = produtyNazw[i];
									
								}
							
						}

						try
						{
							FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
							excel.SaveAs(excelFile);

						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				MessageBox.Show("Zakończono operację");
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
