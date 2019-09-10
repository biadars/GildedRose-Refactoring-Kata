using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var action = ChooseUpdateStrategy(Items[i].Name);
                action(Items[i]);
            }
        }

        private Action<Item> ChooseUpdateStrategy(string name)
        {
            var exp = new Regex(@"Conjured\s");
            switch (name)
            {
                case "Aged Brie":
                    return UpdateAgedBrie;
                case "Backstage passes to a TAFKAL80ETC concert":
                    return UpdateBackstagePass;
                case "Sulfuras, Hand of Ragnaros":
                    return s => { };
                default:
                    if (exp.IsMatch(name))
                        return UpdateConjuredItem;
                    else
                        return UpdateRegularItem;
            }
        }

        public void UpdateAgedBrie(Item brie)
        {
            brie.SellIn--;
            if (brie.Quality < 50)
                brie.Quality++;
            if (brie.SellIn < 0 && brie.Quality < 50)
                brie.Quality++;
        }

        public void UpdateBackstagePass(Item backstagePass)
        {
            if (backstagePass.SellIn <= 0)
                backstagePass.Quality = 0;
            else if (backstagePass.SellIn <= 5 && backstagePass.Quality < 48)
                backstagePass.Quality += 3;
            else if (backstagePass.SellIn <= 10 && backstagePass.Quality < 49)
                backstagePass.Quality += 2;
            else if (backstagePass.Quality < 50)
                backstagePass.Quality++;
            backstagePass.SellIn--;
        }

        public void UpdateConjuredItem(Item item)
        {
            item.Quality = Math.Max(0, item.Quality - 2);
            if (item.SellIn <= 0)
                item.Quality = Math.Max(0, item.Quality - 2);
            item.SellIn--;

        }

        public void UpdateRegularItem(Item item)
        {
            if (item.Quality > 0)
                item.Quality--;
            if (item.Quality > 0 && item.SellIn <= 0)
                item.Quality--;
            item.SellIn--;
        }

        private int UpdateQualityValue(int quality, int update)
        {
            return Math.Min(50, Math.Max(0, quality + update));
        }
    }
}
