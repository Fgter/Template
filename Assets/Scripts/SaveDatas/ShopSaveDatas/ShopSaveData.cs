using System.Collections.Generic;

namespace SaveData
{
    class ShopSaveData
    {
        public Dictionary<int, Dictionary<int, ShopItemSaveData>> shopItemDict;
        public List<ShopItemSaveData> shopItemList;
    }

    struct ShopItemSaveData
    {
        public int count;
        public bool status;
    }
}
