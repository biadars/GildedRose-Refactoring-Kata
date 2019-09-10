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
            UpdateQualityValue(brie, 1);
            if (brie.SellIn < 0)
                UpdateQualityValue(brie, 1);
        }

        public void UpdateBackstagePass(Item backstagePass)
        {
            if (backstagePass.SellIn <= 0)
                backstagePass.Quality = 0;
            else if (backstagePass.SellIn <= 5)
                UpdateQualityValue(backstagePass, 3);
            else if (backstagePass.SellIn <= 10)
                UpdateQualityValue(backstagePass, 2);
            else
                UpdateQualityValue(backstagePass, 1);
            backstagePass.SellIn--;
        }

        public void UpdateConjuredItem(Item item)
        {
            UpdateQualityValue(item, -2);
            if (item.SellIn <= 0)
                UpdateQualityValue(item, -2);
            item.SellIn--;

        }

        public void UpdateRegularItem(Item item)
        {
            UpdateQualityValue(item, -1);
            if (item.SellIn <= 0)
                UpdateQualityValue(item, -1);
            item.SellIn--;
        }

        private void UpdateQualityValue(Item item, int update)
        {
            item.Quality = Math.Min(50, Math.Max(0, item.Quality + update));
        }
    }
}
