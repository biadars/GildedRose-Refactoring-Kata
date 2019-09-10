using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void QualityDoesNotDegradeBelowZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(-1, Items[0].SellIn);
        }

        [Test]
        public void QualityDegradesByOne()
        {
            var Items = new List<Item> {new Item {Name = "Whiskey", SellIn = 4, Quality = 5}};
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(3, Items[0].SellIn);
            Assert.AreEqual(4, Items[0].Quality);
        }

        [Test]
        public void QualityDegradesTwiceAsFastPastSellByDate()
        {
            var Items = new List<Item> { new Item { Name = "Gin", SellIn = 0, Quality = 5 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(3, Items[0].Quality);
        }

        [Test]
        public void AgedBrieIncreasesInQualityWithAge()
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 4, Quality = 5 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(3, Items[0].SellIn);
            Assert.AreEqual(6, Items[0].Quality);
        }

        [Test]
        public void QualityOfItemDoesNotGoAboveFifty()
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 4, Quality = 50 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(3, Items[0].SellIn);
            Assert.AreEqual(50, Items[0].Quality);
        }

        [Test]
        public void SulfurasNeverHasToBeSoldOrDecreaseInQuality()
        {
            var Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 7, Quality = 80 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(7, Items[0].SellIn);
            Assert.AreEqual(80, Items[0].Quality);
        }

        [Test]
        public void BackstagePassesIncreaseInQuality()
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 22 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(10, Items[0].SellIn);
            Assert.AreEqual(23, Items[0].Quality);
        }

        [Test]
        public void BackstagePassesDoublyIncreaseInQuality()
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 23 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(9, Items[0].SellIn);
            Assert.AreEqual(25, Items[0].Quality);
        }

        [Test]
        public void BackstagePassesTriplyIncreaseInQuality()
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 33 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(4, Items[0].SellIn);
            Assert.AreEqual(36, Items[0].Quality);
        }

        [Test]
        public void BackstagePassesQualityDropsAfterConcert()
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 48 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
            Assert.AreEqual(0, Items[0].Quality);
        }
    }
}
