using System.Collections.Generic;

namespace GildedRose.Console
{
    public class StockManager                    //will return to internal once I finish tests
    {
        public IList<Item> Stock;           //will return to private once I finish tests

        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new StockManager()
                                    {
                                        Stock = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }
                                    };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

		public void UpdateQuality()
        {
            foreach (var Good in Stock)
            {
				Good.SellIn = AdjustSellInByGood(Good.Name, Good.SellIn);
				Good.Quality = AdjustQualityByGood(Good.Name, Good.SellIn, Good.Quality);
			}
        }

		//Changes Item Quality based on their SellIn and individual decay profiles
		private int AdjustQualityByGood(string Name, int SellIn, int Quality)
		{
			switch (Name)
			{
				case "Sulfuras, Hand of Ragnaros":
					return Quality;

				case "Aged Brie":
					if (SellIn < 0) return BoundedQualityAdjust(Quality, 2);
					return BoundedQualityAdjust(Quality, 1);
				
				case "Backstage passes to a TAFKAL80ETC concert":
					if (SellIn < 0) return 0;
					if (SellIn < 5) return BoundedQualityAdjust(Quality, 3);
					if (SellIn < 10) return BoundedQualityAdjust(Quality, 2);
					return BoundedQualityAdjust(Quality, 1);
										
				default:
					if (SellIn < 0) return BoundedQualityAdjust(Quality, -2);
					return BoundedQualityAdjust(Quality, -1);
			}
		}

		//Decides how to change the Good Quality based on the requested change
		//I would ideally try to put this in the Item class but I'm not allowed
		private int BoundedQualityAdjust(int Quality, int Change)
		{
			Quality += Change;
			
			//Keeps Quality within limits
			if (Quality > 50) return 50;
			if (Quality < 0) return 0;
				
			return Quality;
		}

		private int AdjustSellInByGood(string Name, int SellIn)
		{
			if (Name == "Sulfuras, Hand of Ragnaros") return SellIn;
			return (SellIn - 1);
		}
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
