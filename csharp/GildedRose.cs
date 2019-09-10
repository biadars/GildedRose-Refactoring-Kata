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
            var exp = new Regex(@"Conjured\s");
            for (var i = 0; i < Items.Count; i++)
            {
                switch (Items[i].Name)
                {
                    case "Aged Brie":
                        UpdateAgedBrie(Items[i]);
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        UpdateBackstagePass(Items[i]);
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    default:
                        if (exp.IsMatch(Items[i].Name))
                            UpdateConjuredItem(Items[i]);
                        else
                            UpdateRegularItem(Items[i]);
                        break;
                }
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
    }
}
