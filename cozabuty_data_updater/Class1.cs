using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozabuty_data_updater
{
	class Category
	{
		private string id_category, id_product, position, id_parent;
		private string name;

		public Category(string kat_id, string pro_id, string prior, string par_id, string name) {
			this.id_category = kat_id;
			this.id_product = pro_id;
			this.position = prior;
			this.id_parent = par_id;
			this.name = name;
			}

		public string GetAllInfo() {
			return id_category + "," + id_product + "," + position + "," + id_parent + "," + name;
		}
		public string GetIdCategory() {
			return id_category;
		}
		public string GetNameCategory()
		{
			return name;
		}
		public void GetTabNames(string[] tab)
		{
			int index = 0;
			int.TryParse(id_category, out index);
			tab[index] = name;
		}
		public string GetIDParent()
		{
			return id_parent;
		}
	}
}
