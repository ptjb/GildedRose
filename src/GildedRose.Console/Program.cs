using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program                    //will return to internal once I finish tests
    {
        public IList<Item> Stock;           //will return to private once I finish tests

        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
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
                if (Good.Name != "Aged Brie" && Good.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Good.Quality > 0)
                    {
                        if (Good.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Good.Quality -= 1;		
                        }
                    }
                }
                else
                {
                    if (Good.Quality < 50)
                    {
                        Good.Quality += 1;

                        if (Good.Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Good.SellIn < 11)
                            {
                                if (Good.Quality < 50)
                                {
                                    Good.Quality += 1;
                                }
                            }

                            if (Good.SellIn < 6)
                            {
                                if (Good.Quality < 50)
                                {
                                    Good.Quality += 1;
                                }
                            }
                        }
                    }
                }
				
                if (Good.Name != "Sulfuras, Hand of Ragnaros")
                {
                    Good.SellIn = Good.SellIn - 1;
                }

                if (Good.SellIn < 0)
                {
                    if (Good.Name != "Aged Brie")
                    {
                        if (Good.Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Good.Quality > 0)
                            {
                                if (Good.Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Good.Quality -= 1;
                                }
                            }
                        }
                        else
                        {
                            Good.Quality = 0;
                        }
                    }
                    else
                    {
                        if (Good.Quality < 50)
                        {
                            Good.Quality += 1;
                        }
                    }
                }
            }
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
