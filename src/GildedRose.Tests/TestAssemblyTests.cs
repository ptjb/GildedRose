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

        //Test that all items are recorded correctly
        [Fact]
        public void ItemEntry()
        {
            Program test = new Program()
            {
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
						//new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
				}
            };

			//Tests items are entered correctly

			Assert.Equal("+5 Dexterity Vest", test.Items[0].Name);
			Assert.Equal(10, test.Items[0].SellIn);
			Assert.Equal(20, test.Items[0].Quality);

			Assert.Equal("Aged Brie", test.Items[1].Name);
			Assert.Equal(2, test.Items[1].SellIn);
			Assert.Equal(0, test.Items[1].Quality);

			Assert.Equal("Elixir of the Mongoose", test.Items[2].Name);
			Assert.Equal(5, test.Items[2].SellIn);
			Assert.Equal(7, test.Items[2].Quality);

			Assert.Equal("Sulfuras, Hand of Ragnaros", test.Items[3].Name);
			Assert.Equal(0, test.Items[3].SellIn);
			Assert.Equal(80, test.Items[3].Quality);

			Assert.Equal("Backstage passes to a TAFKAL80ETC concert", test.Items[4].Name);
			Assert.Equal(15, test.Items[4].SellIn);
			Assert.Equal(20, test.Items[4].Quality);

			/*Assert.Equal("Conjured Mana Cake", test.Items[5].Name);
			Assert.Equal(3, test.Items[5].SellIn);
			Assert.Equal(6, test.Items[5].Quality);
			*/
		}

		//Test Regular Item Behaviour in and out of date,
		//before and then across the Quality Minimum in all cases
		[Fact]
		public void RegularItemAgingInDate()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 49, Quality = 50},
						new Item {Name = "Elixir of the Mongoose", SellIn = 49, Quality = 50}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;
			int DexQualityYesterday;

			int EliSellInYesterday;
			int EliQualityYesterday;

			//Tests behaviour while in date, just before the Quality Minimum
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
		public void RegularItemAgingOutDateEven()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 50},
						new Item {Name = "Elixir of the Mongoose", SellIn = 0, Quality = 50}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;
			int DexQualityYesterday;

			int EliSellInYesterday;
			int EliQualityYesterday;

			//Tests behaviour while out of date until just before Quality Minimum
			//It should be twice as fast to decline
			while ((test.Items[0].SellIn != -24) && (test.Items[1].SellIn != -24))
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
		public void RegularItemAgingOutDateOdd()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 49},
						new Item {Name = "Elixir of the Mongoose", SellIn = 0, Quality = 49}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;
			int DexQualityYesterday;

			int EliSellInYesterday;
			int EliQualityYesterday;

			//Tests behaviour while out of date until just before Quality Minimum
			//It should be twice as fast to decline
			while ((test.Items[0].SellIn != -24) && (test.Items[1].SellIn != -24))
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
		public void RegularItemAgingInDateMinQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 11, Quality = 1},
						new Item {Name = "Elixir of the Mongoose", SellIn = 11, Quality = 1}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;
			
			int EliSellInYesterday;

			//Tests that quality reaches minimum properly,
			//and then never drops below 0
			while ((test.Items[0].SellIn != 0) && (test.Items[1].SellIn != 0))
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
		public void RegularItemAgingOutDateEvenMinQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 2},
						new Item {Name = "Elixir of the Mongoose", SellIn = 0, Quality = 2}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;

			int EliSellInYesterday;

			//Tests that quality reaches minimum properly,
			//and then never drops below 0
			while ((test.Items[0].SellIn != -10) && (test.Items[1].SellIn != -10))
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
		public void RegularItemAgingOutDateOddMinQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 1},
						new Item {Name = "Elixir of the Mongoose", SellIn = 0, Quality = 1}
					}
			};

			//Record previous day's values here
			int DexSellInYesterday;

			int EliSellInYesterday;

			//Tests that quality reaches minimum properly, and then never drops
			//below 0. Quality -= 2 so there is a danger it could take Quality == -1
			while ((test.Items[0].SellIn != -10) && (test.Items[1].SellIn != -10))
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

		//Test Aged Brie Behaviour in and out of date,
		//before and then across the Quality Maximum in all cases
		[Fact]
		public void AgedBrieAgingInDate()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 49, Quality = 0}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;
			
			//Tests behaviour while in date until just before the Quality Maximum
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
		public void AgedBrieAgingOutDateEven()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 0}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests behaviour while out of date until just before the
			//Quality Maximum, should be twice as fast to increase
			while (test.Items[0].SellIn != -24)
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
		public void AgedBrieAgingOutDateOdd()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 1}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests behaviour while out of date until just before the
			//Quality Maximum, should be twice as fast to increase
			while (test.Items[0].SellIn != -24)
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
		public void AgedBrieAgingInDateMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 11, Quality = 49}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (test.Items[0].SellIn != 0)
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
		public void AgedBrieAgingOutDateEvenMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 48}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (test.Items[0].SellIn != -11)
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
		public void AgedBrieAgingOutDateOddMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 49}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50. Quality += 2 so there is a
			//danger it could take Quality == 51
			while (test.Items[0].SellIn != -11)
			{
				BrieSellInYesterday = test.Items[0].SellIn;
				BrieQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Aged Brie, SellIn -= 1, Quality == 50
				Assert.Equal(BrieSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);
			}
		}

		//Test Sulfuras, Hand of Ragnaros remains unchanged after update
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

		//Test Backstage passes to a TAFKAL80ETC concert behaviour
		//over SellIn > 10, 5 < SellIn <= 10, 0 < SellIn <=5, SellIn <= 0
		//before and then across the Quality Maximum in all cases
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
							SellIn = 59,
							Quality = 0
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while SellIn > 10,
			//until just before the Quality Maximum
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
		public void PassAgingOverFiveEven()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 10,
							Quality = 0
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while 5 <= SellIn < 10,
			//until just before the Quality Maximum
			while (test.Items[0].Quality != 48)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 2, until SellIn <= 5
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 2, test.Items[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 5) test.Items[0].SellIn = 10;
			}
		}

		[Fact]
		public void PassAgingOverFiveOdd()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 10,
							Quality = 1
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while 5 <= SellIn < 10,
			//until just before the Quality Maximum
			while (test.Items[0].Quality != 49)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 2, until SellIn <= 5
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 2, test.Items[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 5) test.Items[0].SellIn = 10;
			}
		}

		[Fact]
		public void PassAgingOverZeroZero()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 5,
							Quality = 0
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while 0 <= SellIn < 5
			while (test.Items[0].Quality != 48)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 3, until SellIn = 0
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 3, test.Items[0].Quality);

				//If SellIn drops below 0, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 0) test.Items[0].SellIn = 5;
			}
		}

		[Fact]
		public void PassAgingOverZeroOne()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 5,
							Quality = 1
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while 0 <= SellIn < 5
			while (test.Items[0].Quality != 49)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 3, until SellIn = 0
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 3, test.Items[0].Quality);

				//If SellIn drops below 0, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 0) test.Items[0].SellIn = 5;
			}
		}

		[Fact]
		public void PassAgingOverZeroTwo()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 5,
							Quality = 2
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests behaviour while 0 <= SellIn < 5
			while (test.Items[0].Quality != 47)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 3, until SellIn = 0
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(PassQualityYesterday + 3, test.Items[0].Quality);

				//If SellIn drops below 0, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 0) test.Items[0].SellIn = 5;
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
		
		[Fact]
		public void PassAgingOverTenMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 21,
							Quality = 49
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (test.Items[0].SellIn != 10)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);
			}
		}

		[Fact]
		public void PassAgingOverFiveEvenMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 10,
							Quality = 48
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Counter to ensure only 11 iterations are made
			int t = 11;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (t != 0)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 5) test.Items[0].SellIn = 10;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverFiveOddMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 10,
							Quality = 49
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Counter to ensure only 11 iterations are made
			int t = 11;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (t != 0)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				//Quality += 2 so there is a danger it could take Quality == 51
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 5) test.Items[0].SellIn = 10;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverZeroZeroMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 5,
							Quality = 48
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Counter to ensure only 11 iterations are made
			int t = 11;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (t != 0)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				//Quality += 3 so there is a danger it could take Quality == 51
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 0) test.Items[0].SellIn = 5;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverZeroOneMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 5,
							Quality = 49
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Counter to ensure only 11 iterations are made
			int t = 11;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (t != 0)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				//Quality += 3 so there is a danger it could take Quality == 52
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 0) test.Items[0].SellIn = 5;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverZeroTwoMaxQuality()
		{
			Program test = new Program()
			{
				Items = new List<Item>
					{
						new Item
						{
							Name = "Backstage passes to a TAFKAL80ETC concert",
							SellIn = 5,
							Quality = 47
						}
					}
			};

			//Record previous day's values here
			int PassSellInYesterday;
			int PassQualityYesterday;

			//Counter to ensure only 11 iterations are made
			int t = 11;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (t != 0)
			{
				PassSellInYesterday = test.Items[0].SellIn;
				PassQualityYesterday = test.Items[0].Quality;

				test.UpdateQuality();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				Assert.Equal(PassSellInYesterday - 1, test.Items[0].SellIn);
				Assert.Equal(50, test.Items[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Items[0].SellIn == 0) test.Items[0].SellIn = 5;

				t--;
			}
		}
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