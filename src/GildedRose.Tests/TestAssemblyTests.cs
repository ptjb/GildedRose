using Xunit;
using GildedRose.Console;
using System.Collections.Generic;
//using System;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        //Assert.True(true);
        //Assert.Equal(4, (2+2));
        //Assert.False(false);
        
        [Fact]
        public void ItemEntry()
        {
            Program test = new Program()
            {
                Items = new List<Item>
                {
					new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                }
            };

			Assert.Equal("+5 Dexterity Vest", test.Items[0].Name);
			Assert.Equal(10, test.Items[0].SellIn);
			Assert.Equal(20, test.Items[0].Quality);
		}

		[Fact]
		public void RegularItemAgingInDate()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 3, Quality = 9},
						new Item {Name = "Elixir of the Mongoose", SellIn = 3, Quality = 9}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;
			int DexQualityYesterday;

			int EliSellInYesterday;
			int EliQualityYesterday;

			//Tests behaviour while in date
			while ((test.Items[0].SellIn != 0) && (test.Items[1].SellIn != 0))
			{
				DexSellInYesterday = test.Items[0].SellIn;
				DexQualityYesterday = test.Items[0].Quality;

				EliSellInYesterday = test.Items[1].SellIn;
				EliQualityYesterday = test.Items[1].Quality;

				test.UpdateQuality();

				//+5 Dexterity Vest, SellIn -= 1, Quality -= 1
				Assert.Equal(DexSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(DexQualityYesterday - 1, test.Items[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality -= 1
				Assert.Equal(EliSellInYesterday - 1, test.Items[1].SellIn);
				Assert.Equal(EliQualityYesterday - 1, test.Items[1].Quality);
			}
		}

		[Fact]
		public void RegularItemAgingOutDate()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 9},
						new Item {Name = "Elixir of the Mongoose", SellIn = 0, Quality = 9}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;
			int DexQualityYesterday;

			int EliSellInYesterday;
			int EliQualityYesterday;

			//Tests behaviour while out of date, should be twice as fast to decline
			while ((test.Items[0].SellIn != -3) && (test.Items[1].SellIn != -3))
			{
				DexSellInYesterday = test.Items[0].SellIn;
				DexQualityYesterday = test.Items[0].Quality;

				EliSellInYesterday = test.Items[1].SellIn;
				EliQualityYesterday = test.Items[1].Quality;

				test.UpdateQuality();

				//+5 Dexterity Vest, SellIn -= 1, Quality -= 2
				Assert.Equal(DexSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(DexQualityYesterday - 2, test.Items[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality -= 2
				Assert.Equal(EliSellInYesterday - 1, test.Items[1].SellIn);
				Assert.Equal(EliQualityYesterday - 2, test.Items[1].Quality);
			}
		}

		[Fact]
		public void RegularItemAgingMinQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 3, Quality = 0},
						new Item {Name = "Elixir of the Mongoose", SellIn = 3, Quality = 0}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;
			
			int EliSellInYesterday;
			
			//Tests that quality never drops below 0
			while ((test.Items[0].SellIn != -3) && (test.Items[1].SellIn != -3))
			{
				DexSellInYesterday = test.Items[0].SellIn;

				EliSellInYesterday = test.Items[1].SellIn;

				test.UpdateQuality();

				//+5 Dexterity Vest, SellIn -= 1, Quality == 0
				Assert.Equal(DexSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(0, test.Items[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality == 0
				Assert.Equal(EliSellInYesterday - 1, test.Items[1].SellIn);
				Assert.Equal(0, test.Items[1].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingInDate()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 3, Quality = 0}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;
			
			//Tests behaviour while in date
			while (test.Items[0].SellIn != 0)
			{
				BrieSellInYesterday = test.Items[0].SellIn;
				BrieQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Aged Brie, SellIn -= 1, Quality += 1
				Assert.Equal(BrieSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(BrieQualityYesterday + 1, test.Items[0].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingOutDate()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 3}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests behaviour while out of date, should be twice as fast to increase
			while (test.Items[0].SellIn != -3)
			{
				BrieSellInYesterday = test.Items[0].SellIn;
				BrieQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Aged Brie, SellIn -= 1, Quality += 2
				Assert.Equal(BrieSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(BrieQualityYesterday + 2, test.Items[0].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 3, Quality = 50}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests Quality Cap == 50
			while (test.Items[0].SellIn != -3)
			{
				BrieSellInYesterday = test.Items[0].SellIn;
				BrieQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Aged Brie, SellIn -= 1, Quality == 50
				Assert.Equal(BrieSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);
			}
		}

		[Fact]
		public void SulfurasAging()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80}
					}
			};

			//Record previous day's values here
			int SulfSellInYesterday;
			int SulfQualityYesterday;

			//Tests Sulfuras, Hand of Ragnaros is unaltered after many (10) UpdateQuality()
			for (int i = 0; i < 10; i++)
			{
				SulfSellInYesterday = test.Items[0].SellIn;
				SulfQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Sulfuras, Hand of Ragnaros, SellIn == 0, Quality == 80
				Assert.Equal(0, test.Items[0].SellIn);
				Assert.Equal(80, test.Items[0].Quality);
			}
		}

		[Fact]
		public void PassAgingOverTen()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 15,
							Quality = 20
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while SellIn > 10
			while (test.Items[0].SellIn != 10)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 2, until SellIn <= 10
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 1, test.Items[0].Quality);
			}
		}

		[Fact]
		public void PassAgingOverFive()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 10,
							Quality = 20
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while 5 <= SellIn < 10
			while (test.Items[0].SellIn != 5)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 2, until SellIn <= 5
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 2, test.Items[0].Quality);
			}
		}

		[Fact]
		public void PassAgingOverZero()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 5,
							Quality = 20
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while 0 <= SellIn < 5
			while (test.Items[0].SellIn != 0)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 3, until SellIn = 0
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 3, test.Items[0].Quality);
			}
		}

		[Fact]
		public void PassAgingOutDate()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 0,
							Quality = 20
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while -10 < SellIn <= 0
			while (test.Items[0].SellIn != -10)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 0
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(0, test.Items[0].Quality);
			}
		}
		/*
		[Fact]
		public void NonPassInDateAging()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
						new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
						new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
					}
			};

			test.UpdateQuality();

			//+5 Dexterity Vest, SellIn -= 1, Quality -= 1
			Assert.Equal(9, test.Items[0].SellIn);
			Assert.Equal(19, test.Items[0].Quality);

			//Aged Brie, SellIn -= 1, Quality += 1
			Assert.Equal(1, test.Items[1].SellIn);
			Assert.Equal(1, test.Items[1].Quality);

			//Sulfuras, Hand of Ragnaros, SellIn == 0, Quality == 80
			Assert.Equal(0, test.Items[2].SellIn);
			Assert.Equal(80, test.Items[2].Quality);
		}
		*//*
		[Fact]
		public void NonPassOutDateAging()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = -1, Quality = 20},
						new Item {Name = "Aged Brie", SellIn = -1, Quality = 0},
						new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
					}
			};

			test.UpdateQuality();

			//+5 Dexterity Vest, SellIn -= 1, Quality -= 1
			Assert.Equal(9, test.Items[0].SellIn);
			Assert.Equal(19, test.Items[0].Quality);

			//Aged Brie, SellIn -= 1, Quality += 1
			Assert.Equal(1, test.Items[1].SellIn);
			Assert.Equal(1, test.Items[1].Quality);

			//Sulfuras, Hand of Ragnaros, SellIn == 0, Quality == 80
			Assert.Equal(0, test.Items[2].SellIn);
			Assert.Equal(80, test.Items[2].Quality);
		}
		*/
	}
}

/*
Items = new List<Item>
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
*/