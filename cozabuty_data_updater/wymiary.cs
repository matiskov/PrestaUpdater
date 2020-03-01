using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozabuty_data_updater
{
	class wymiary
	{
		string name, id_product, weight, height, width, depth;
		public wymiary(string name, string id_product, string weight, string height, string width, string depth){
			this.name = name;
			this.id_product = id_product;
			this.weight = weight;
			this.height = height;
			this.width = width;
			this.depth = depth;
		}

		public string GetName() {
			return this.name;
		}
		public string GetID()
		{
			return this.id_product;
		}
		public string GetWeight()
		{
			return this.weight;
		}
		public string GetHeight()
		{
			return this.height;
		}
		public string GetWidth()
		{
			return this.width;
		}
		public string GetDepth()
		{
			return this.depth;
		}
	}
}
