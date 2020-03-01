using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozabuty_data_updater
{
	class Products
	{
		string id_product, upc, price, wholesale_price, id_shop_default, reference, nazwa_produktu, producent, stany;
		public Products(string id, string upc, string price, string sale_price, string default_shop, string reference, string nazwa, string producent, string stany) {
			this.id_product = id;
			this.upc = upc;
			this.price = price;
			this.wholesale_price = sale_price;
			this.id_shop_default = default_shop;
			this.reference = reference;
			this.nazwa_produktu = nazwa;
			this.producent = producent;
			this.stany = stany;
		}

		public string GetID() {
			return this.id_product;
		}
		public string GetName() {
			return this.nazwa_produktu;
		}
		public string GetUPC()
		{
			return this.upc;
		}
		public string GetPrice()
		{
			return this.price;
		}
		public string GetCenaSprzedazy()
		{
			return this.wholesale_price;
		}
		public string GetReference()
		{
			return this.reference;
		}
		public string GetProducent()
		{
			return this.producent;
		}
		public string GetStany()
		{
			return this.stany;
		}
	}
}
