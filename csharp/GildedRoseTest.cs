using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void InDateItemQualityDoesNotFallBellowZero()
        {
            var days = 0;
            var quality = 0;
            IList<Item> Items = new List<Item> { new Item { Name = "Halloumi", SellIn = days, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality, Items[0].Quality);
        }

        [Test]
        public void OutOfDateItemQualityDoesNotFallBellowZero()
        {
            var days = -1;
            var quality = 1;
            IList<Item> Items = new List<Item> { new Item { Name = "Leek", SellIn = days, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality - 1, Items[0].Quality);
        }

        [TestCase(1)]
        [TestCase(5)]
        public void InDateItemQualityDegradesByOne(int quality)
        {
            var days = 4;
            var Items = new List<Item> {new Item {Name = "Whiskey", SellIn = days, Quality = quality}};
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality - 1, Items[0].Quality);
        }

        [TestCase(2)]
        [TestCase(100)]
        public void OutOfDateItemQualityDegradesTwiceAsFast(int quality)
        {
            var days = 0;
            var Items = new List<Item> { new Item { Name = "Gin", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality - 2, Items[0].Quality);
        }

        [TestCase(2, 34)]
        [TestCase(1, 44)]
        public void InDateAgedBrieIncreasesInQuality(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality + 1, Items[0].Quality);
        }

        [TestCase(0, 0)]
        [TestCase(-1, 32)]
        public void OutOfDateAgedBrieDoublyIncreasesInQuality(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality + 2, Items[0].Quality);
        }

        [TestCase(0, 49)]
        [TestCase(4, 50)]
        [TestCase(-1, 49)]
        [TestCase(-1, 50)]
        public void ItemQualityDoesNotIncreaseAboveFifty(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
        }

        [TestCase(231)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        public void ItemSellByDateDecreasesByOne(int days)
        {
            var quality = 22;
            var Items = new List<Item> {new Item() {Name = "Chorizo", SellIn = days, Quality = quality}};
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(days - 1, Items[0].SellIn);
        }

        [TestCase(231)]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(-1)]
        public void SulfurasSellInDateDoesNotDecrease(int days)
        {
            var quality = 80;
            var Items = new List<Item> { new Item() { Name = "Sulfuras, Hand of Ragnaros", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(days, Items[0].SellIn);
        }

        [Test]
        public void SulfurasQualityDoesNotDecrease()
        {
            var days = 7;
            var quality = 80;
            var Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(quality, Items[0].Quality);
        }

        [TestCase(99, 24)]
        [TestCase(11, 50)]
        public void OverTenDaysAwayBackstagePassesIncreaseInQuality(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(Math.Min(50, quality + 1), Items[0].Quality);
        }

        [TestCase(10, 20)]
        [TestCase(9, 49)]
        [TestCase(8, 50)]
        [TestCase(7, 2)]
        [TestCase(6, 19)]
        public void SixToTenDaysAwayBackstagePassesDoublyIncreaseInQuality(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(Math.Min(50, quality + 2), Items[0].Quality);
        }

        [TestCase(5, 2)]
        [TestCase(4, 47)]
        [TestCase(3, 50)]
        [TestCase(2, 22)]
        [TestCase(1, -1)]
        public void OneToFiveDaysAwayBackstagePassesTriplyIncreaseInQuality(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(Math.Min(50, quality + 3), Items[0].Quality);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void OutOfDateBackstagePassesQualityDropsToZero(int days)
        {
            var quality = 48;
            var Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }

        [TestCase(4, 2)]
        [TestCase(1, 0)]
        [TestCase(0, 4)]
        public void InDateConjuredItemQualityDegradesByTwo(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Conjured chorizo", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(Math.Max(0, quality - 2), Items[0].Quality);
        }

        [TestCase(-1, 5)]
        [TestCase(-2, 4)]
        [TestCase(-3, 3)]
        [TestCase(-4, 0)]
        [TestCase(-5, -1)]
        public void OutOfDateConjuredItemQualityDegradesByFour(int days, int quality)
        {
            var Items = new List<Item> { new Item { Name = "Conjured swiss cheese", SellIn = days, Quality = quality } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(Math.Max(0, quality - 2), Items[0].Quality);
        }
    }
}
