using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSV
{
	class CardInfo
	{
		public string ArtLocation { get; set; }
		public string Title { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public string FlavorText { get; set; }
		public int Heat { get; set; }
		public int Cost { get; set; }

		public CardInfo(string location)
		{
			this.ArtLocation = location;
			this.Title = "";
			this.Type = "";
			this.Description = "";
			this.FlavorText = "";
			this.Heat = 0;
			this.Cost = 0;
		}

		public CardInfo withTitle(string title)
		{
			this.Title = title;
			return this;
		}
		public CardInfo withType(string type)
		{
			this.Type = type;
			return this;
		}
		public CardInfo withDescription(string description)
		{
			this.Description = description;
			return this;
		}
		public CardInfo withFlavorText(string flavorText)
		{
			this.FlavorText = flavorText;
			return this;
		}
		public CardInfo withHeat(int heat)
		{
			this.Heat = heat;
			return this;
		}
		public CardInfo withCost(int cost)
		{
			this.Cost = cost;
			return this;
		}
	}
}
