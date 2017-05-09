using Xunit;
using GildedRose.Console;
using System.Collections.Generic;
//using System;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
        }
        /*
        [Fact]
        public void TestTheEqual()
        {
            Assert.Equal(4, (2+2));
        }

        [Fact]
        public void TestTheFalse()
        {
            Assert.False(false);
        }*/

        //static public void Main(string[] args)

        [Fact]
        public void TestInitialReduction()
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
                        new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                    }
            };

            //string[] a = new string[0];
            //Program.Main(a);
            //Program.Main(a);
            //test.UpdateQuality();
            //System.Console.WriteLine("Item 0: {0}\t{1}\t{2}", test.Items[0].Name, test.Items[0].SellIn, test.Items[0].Quality);
            Assert.True(test.Items.Count != 0);
            //Assert.Equal(9, test.Items[0].SellIn);
            //System.Console.ReadKey();
        }


    }
}