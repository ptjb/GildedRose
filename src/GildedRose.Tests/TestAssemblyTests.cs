using Xunit;
using GildedRose.Console;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
		//N.B: Tests marked Even (== Zero)/Odd (== One)/Two/Three denote
		//the number one reaches when acting recursively with
		//UpdateInventory() (for decrements) or from 50 (For increments).
		//That is the result of Quality % (Decrement XOR Increment)
		//These ensure that the whole range of Quality Values are tested

        //Test that all items are recorded correctly
        [Fact]
        public void ItemEntry()
        {
            InventoryManager test = new InventoryManager()
            {
                Inventory = new List<Item>
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

			//Tests items are entered correctly

			Assert.Equal("+5 Dexterity Vest", test.Inventory[0].Name);
			Assert.Equal(10, test.Inventory[0].SellIn);
			Assert.Equal(20, test.Inventory[0].Quality);

			Assert.Equal("Aged Brie", test.Inventory[1].Name);
			Assert.Equal(2, test.Inventory[1].SellIn);
			Assert.Equal(0, test.Inventory[1].Quality);

			Assert.Equal("Elixir of the Mongoose", test.Inventory[2].Name);
			Assert.Equal(5, test.Inventory[2].SellIn);
			Assert.Equal(7, test.Inventory[2].Quality);

			Assert.Equal("Sulfuras, Hand of Ragnaros", test.Inventory[3].Name);
			Assert.Equal(0, test.Inventory[3].SellIn);
			Assert.Equal(80, test.Inventory[3].Quality);

			Assert.Equal("Backstage passes to a TAFKAL80ETC concert", test.Inventory[4].Name);
			Assert.Equal(15, test.Inventory[4].SellIn);
			Assert.Equal(20, test.Inventory[4].Quality);

			Assert.Equal("Conjured Mana Cake", test.Inventory[5].Name);
			Assert.Equal(3, test.Inventory[5].SellIn);
			Assert.Equal(6, test.Inventory[5].Quality);
			
		}

		//Test Regular Item Behaviour in and out of date,
		//before and then across the Quality Minimum in all cases
		[Fact]
		public void RegularItemAgingInDate()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while ((test.Inventory[0].SellIn != 0) && (test.Inventory[1].SellIn != 0))
			{
				DexSellInYesterday = test.Inventory[0].SellIn;
				DexQualityYesterday = test.Inventory[0].Quality;

				EliSellInYesterday = test.Inventory[1].SellIn;
				EliQualityYesterday = test.Inventory[1].Quality;

				test.UpdateInventory();

				//+5 Dexterity Vest, SellIn -= 1, Quality -= 1
				Assert.Equal(DexSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(DexQualityYesterday - 1, test.Inventory[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality -= 1
				Assert.Equal(EliSellInYesterday - 1, test.Inventory[1].SellIn);
				Assert.Equal(EliQualityYesterday - 1, test.Inventory[1].Quality);
			}
		}

		[Fact]
		public void RegularItemAgingOutDateEven()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while ((test.Inventory[0].SellIn != -24) && (test.Inventory[1].SellIn != -24))
			{
				DexSellInYesterday = test.Inventory[0].SellIn;
				DexQualityYesterday = test.Inventory[0].Quality;

				EliSellInYesterday = test.Inventory[1].SellIn;
				EliQualityYesterday = test.Inventory[1].Quality;

				test.UpdateInventory();

				//+5 Dexterity Vest, SellIn -= 1, Quality -= 2
				Assert.Equal(DexSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(DexQualityYesterday - 2, test.Inventory[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality -= 2
				Assert.Equal(EliSellInYesterday - 1, test.Inventory[1].SellIn);
				Assert.Equal(EliQualityYesterday - 2, test.Inventory[1].Quality);
			}
		}

		[Fact]
		public void RegularItemAgingOutDateOdd()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while ((test.Inventory[0].SellIn != -24) && (test.Inventory[1].SellIn != -24))
			{
				DexSellInYesterday = test.Inventory[0].SellIn;
				DexQualityYesterday = test.Inventory[0].Quality;

				EliSellInYesterday = test.Inventory[1].SellIn;
				EliQualityYesterday = test.Inventory[1].Quality;

				test.UpdateInventory();

				//+5 Dexterity Vest, SellIn -= 1, Quality -= 2
				Assert.Equal(DexSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(DexQualityYesterday - 2, test.Inventory[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality -= 2
				Assert.Equal(EliSellInYesterday - 1, test.Inventory[1].SellIn);
				Assert.Equal(EliQualityYesterday - 2, test.Inventory[1].Quality);
			}
		}

		[Fact]
		public void RegularItemAgingInDateMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while ((test.Inventory[0].SellIn != 0) && (test.Inventory[1].SellIn != 0))
			{
				DexSellInYesterday = test.Inventory[0].SellIn;

				EliSellInYesterday = test.Inventory[1].SellIn;

				test.UpdateInventory();

				//+5 Dexterity Vest, SellIn -= 1, Quality == 0
				Assert.Equal(DexSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality == 0
				Assert.Equal(EliSellInYesterday - 1, test.Inventory[1].SellIn);
				Assert.Equal(0, test.Inventory[1].Quality);
			}
		}

		[Fact]
		public void RegularItemAgingOutDateEvenMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while ((test.Inventory[0].SellIn != -10) && (test.Inventory[1].SellIn != -10))
			{
				DexSellInYesterday = test.Inventory[0].SellIn;

				EliSellInYesterday = test.Inventory[1].SellIn;

				test.UpdateInventory();

				//+5 Dexterity Vest, SellIn -= 1, Quality == 0
				Assert.Equal(DexSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality == 0
				Assert.Equal(EliSellInYesterday - 1, test.Inventory[1].SellIn);
				Assert.Equal(0, test.Inventory[1].Quality);
			}
		}

		[Fact]
		public void RegularItemAgingOutDateOddMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while ((test.Inventory[0].SellIn != -10) && (test.Inventory[1].SellIn != -10))
			{
				DexSellInYesterday = test.Inventory[0].SellIn;

				EliSellInYesterday = test.Inventory[1].SellIn;

				test.UpdateInventory();

				//+5 Dexterity Vest, SellIn -= 1, Quality == 0
				Assert.Equal(DexSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);

				//Elixir of the Mongoose, SellIn -= 1, Quality == 0
				Assert.Equal(EliSellInYesterday - 1, test.Inventory[1].SellIn);
				Assert.Equal(0, test.Inventory[1].Quality);
			}
		}

		//Test Aged Brie Behaviour in and out of date,
		//before and then across the Quality Maximum in all cases
		[Fact]
		public void AgedBrieAgingInDate()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 49, Quality = 0}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;
			
			//Tests behaviour while in date until just before the Quality Maximum
			while (test.Inventory[0].SellIn != 0)
			{
				BrieSellInYesterday = test.Inventory[0].SellIn;
				BrieQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Aged Brie, SellIn -= 1, Quality += 1
				Assert.Equal(BrieSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(BrieQualityYesterday + 1, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingOutDateEven()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 0}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests behaviour while out of date until just before the
			//Quality Maximum, should be twice as fast to increase
			while (test.Inventory[0].SellIn != -24)
			{
				BrieSellInYesterday = test.Inventory[0].SellIn;
				BrieQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Aged Brie, SellIn -= 1, Quality += 2
				Assert.Equal(BrieSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(BrieQualityYesterday + 2, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingOutDateOdd()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 1}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests behaviour while out of date until just before the
			//Quality Maximum, should be twice as fast to increase
			while (test.Inventory[0].SellIn != -24)
			{
				BrieSellInYesterday = test.Inventory[0].SellIn;
				BrieQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Aged Brie, SellIn -= 1, Quality += 2
				Assert.Equal(BrieSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(BrieQualityYesterday + 2, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingInDateMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 11, Quality = 49}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (test.Inventory[0].SellIn != 0)
			{
				BrieSellInYesterday = test.Inventory[0].SellIn;
				BrieQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Aged Brie, SellIn -= 1, Quality == 50
				Assert.Equal(BrieSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingOutDateEvenMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Aged Brie", SellIn = 0, Quality = 48}
					}
			};

			//Record previous day's values here
			int BrieSellInYesterday;
			int BrieQualityYesterday;

			//Tests that quality reaches maximum properly,
			//and then never goes above 50
			while (test.Inventory[0].SellIn != -11)
			{
				BrieSellInYesterday = test.Inventory[0].SellIn;
				BrieQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Aged Brie, SellIn -= 1, Quality == 50
				Assert.Equal(BrieSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void AgedBrieAgingOutDateOddMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].SellIn != -11)
			{
				BrieSellInYesterday = test.Inventory[0].SellIn;
				BrieQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Aged Brie, SellIn -= 1, Quality == 50
				Assert.Equal(BrieSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);
			}
		}

		//Test Sulfuras, Hand of Ragnaros remains unchanged after update
		[Fact]
		public void SulfurasAging()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80}
					}
			};

			//Record previous day's values here
			int SulfSellInYesterday;
			int SulfQualityYesterday;

			//Tests Sulfuras, Hand of Ragnaros is unaltered after many (10) UpdateInventory()
			for (int i = 0; i < 10; i++)
			{
				SulfSellInYesterday = test.Inventory[0].SellIn;
				SulfQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Sulfuras, Hand of Ragnaros, SellIn == 0, Quality == 80
				Assert.Equal(0, test.Inventory[0].SellIn);
				Assert.Equal(80, test.Inventory[0].Quality);
			}
		}

		//Test Backstage passes to a TAFKAL80ETC concert behaviour
		//over SellIn > 10, 5 < SellIn <= 10, 0 < SellIn <=5, SellIn <= 0
		//before and then across the Quality Maximum in all cases
		[Fact]
		public void PassAgingOverTen()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].SellIn != 10)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 2, until SellIn <= 10
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(PassQualityYesterday + 1, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void PassAgingOverFiveEven()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].Quality != 48)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 2, until SellIn <= 5
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(PassQualityYesterday + 2, test.Inventory[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 5) test.Inventory[0].SellIn = 10;
			}
		}

		[Fact]
		public void PassAgingOverFiveOdd()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].Quality != 49)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 2, until SellIn <= 5
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(PassQualityYesterday + 2, test.Inventory[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 5) test.Inventory[0].SellIn = 10;
			}
		}

		[Fact]
		public void PassAgingOverZeroZero()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].Quality != 48)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 3, until SellIn = 0
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(PassQualityYesterday + 3, test.Inventory[0].Quality);

				//If SellIn drops below 0, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 0) test.Inventory[0].SellIn = 5;
			}
		}

		[Fact]
		public void PassAgingOverZeroOne()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].Quality != 49)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 3, until SellIn = 0
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(PassQualityYesterday + 3, test.Inventory[0].Quality);

				//If SellIn drops below 0, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 0) test.Inventory[0].SellIn = 5;
			}
		}

		[Fact]
		public void PassAgingOverZeroTwo()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].Quality != 47)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality += 3, until SellIn = 0
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(PassQualityYesterday + 3, test.Inventory[0].Quality);

				//If SellIn drops below 0, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 0) test.Inventory[0].SellIn = 5;
			}
		}

		[Fact]
		public void PassAgingOutDate()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].SellIn != -10)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 0
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);
			}
		}
		
		[Fact]
		public void PassAgingOverTenMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
			while (test.Inventory[0].SellIn != 10)
			{
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void PassAgingOverFiveEvenMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 5) test.Inventory[0].SellIn = 10;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverFiveOddMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				//Quality += 2 so there is a danger it could take Quality == 51
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 5) test.Inventory[0].SellIn = 10;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverZeroZeroMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				//Quality += 3 so there is a danger it could take Quality == 51
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 0) test.Inventory[0].SellIn = 5;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverZeroOneMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				//Quality += 3 so there is a danger it could take Quality == 52
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 0) test.Inventory[0].SellIn = 5;

				t--;
			}
		}

		[Fact]
		public void PassAgingOverZeroTwoMaxQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
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
				PassSellInYesterday = test.Inventory[0].SellIn;
				PassQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Backstage passes to a TAFKAL80ETC, SellIn -= 1, Quality == 50
				Assert.Equal(PassSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(50, test.Inventory[0].Quality);

				//If SellIn drops below 5, behaviour will change
				//This keeps it with the tested range
				if (test.Inventory[0].SellIn == 0) test.Inventory[0].SellIn = 5;

				t--;
			}
		}

		//Test Conjured Mana Cake Behaviour in and out of date,
		//before and then across the Quality Minimum in all cases
		[Fact]
		public void ConjuredCakeAgingInDateEven()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 24, Quality = 50}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;
			int CakeQualityYesterday;

			//Tests behaviour while in date, just before the Quality Minimum
			while (test.Inventory[0].SellIn != 0)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;
				CakeQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality -= 2
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(CakeQualityYesterday - 2, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingInDateOdd()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 24, Quality = 49}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;
			int CakeQualityYesterday;

			//Tests behaviour while in date, just before the Quality Minimum
			while (test.Inventory[0].SellIn != 0)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;
				CakeQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality -= 2
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(CakeQualityYesterday - 2, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateZero()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 48}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;
			int CakeQualityYesterday;

			//Tests behaviour while out of date until just before Quality Minimum
			//It should be twice as fast to decline
			while (test.Inventory[0].SellIn != -11)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;
				CakeQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality -= 4
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(CakeQualityYesterday - 4, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateOne()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 49}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;
			int CakeQualityYesterday;

			//Tests behaviour while out of date until just before Quality Minimum
			//It should be twice as fast to decline
			while (test.Inventory[0].SellIn != -12)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;
				CakeQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality -= 4
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(CakeQualityYesterday - 4, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateTwo()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 50}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;
			int CakeQualityYesterday;

			//Tests behaviour while out of date until just before Quality Minimum
			//It should be twice as fast to decline
			while (test.Inventory[0].SellIn != -12)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;
				CakeQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality -= 4
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(CakeQualityYesterday - 4, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateThree()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 47}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;
			int CakeQualityYesterday;

			//Tests behaviour while out of date until just before Quality Minimum
			//It should be twice as fast to decline
			while (test.Inventory[0].SellIn != -11)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;
				CakeQualityYesterday = test.Inventory[0].Quality;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality -= 4
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(CakeQualityYesterday - 4, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingInDateEvenMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 11, Quality = 2}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;

			//Tests that quality reaches minimum properly,
			//and then never drops below 0
			while (test.Inventory[0].SellIn != 0)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality == 0
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingInDateOddMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 11, Quality = 1}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;

			//Tests that quality reaches minimum properly, and then never
			//drops below 0. As Quality -= 2 there is a danger Quality == -1
			while (test.Inventory[0].SellIn != 0)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality == 0
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateZeroMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 4}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;

			//Tests that quality reaches minimum properly, and then never
			//drops below 0
			while (test.Inventory[0].SellIn != -10)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality == 0
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateOneMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 1}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;

			//Tests that quality reaches minimum properly, and then never
			//drops below 0. As Quality -= 4 there is a danger Quality == -3
			while (test.Inventory[0].SellIn != -10)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality == 0
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateTwoMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 2}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;

			//Tests that quality reaches minimum properly, and then never
			//drops below 0. As Quality -= 4 there is a danger Quality == -2
			while (test.Inventory[0].SellIn != -10)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality == 0
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);
			}
		}

		[Fact]
		public void ConjuredCakeAgingOutDateThreeMinQuality()
		{
			InventoryManager test = new InventoryManager()
			{
				Inventory = new List<Item>
					{
						new Item {Name = "Conjured Mana Cake", SellIn = 0, Quality = 3}
					}
			};

			//Record previous day's values here
			int CakeSellInYesterday;

			//Tests that quality reaches minimum properly, and then never
			//drops below 0. As Quality -= 4 there is a danger Quality == -1
			while (test.Inventory[0].SellIn != -10)
			{
				CakeSellInYesterday = test.Inventory[0].SellIn;

				test.UpdateInventory();

				//Conjured Mana Cake, SellIn -= 1, Quality == 0
				Assert.Equal(CakeSellInYesterday - 1, test.Inventory[0].SellIn);
				Assert.Equal(0, test.Inventory[0].Quality);
			}
		}
	}
}