using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

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
                switch (Items[i].Name)
                {
                    case "Aged Brie":
                        Items[i] = UpdateAgedBrie(Items[i]);
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        Items[i] = UpdateBackstagePass(Items[i]);
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    default:
                        Items[i] = UpdateRegularItem(Items[i]);
                        break;
                }
            }
        }

        public Item UpdateAgedBrie(Item brie)
        {
            brie.SellIn--;
            if (brie.Quality < 50)
                brie.Quality++;
            if (brie.SellIn < 0 && brie.Quality < 50)
                brie.Quality++;
            return brie;
        }

        public Item UpdateBackstagePass(Item backstagePass)
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
            return backstagePass;
        }

        public Item UpdateRegularItem(Item item)
        {
            if (item.Quality > 0)
                item.Quality--;
            if (item.Quality > 0 && item.SellIn <= 0)
                item.Quality--;
            item.SellIn--;
            return item;
        }
    }
}
