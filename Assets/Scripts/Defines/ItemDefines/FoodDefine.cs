using System.Collections.Generic;

namespace Define
{
    public class FoodDefine : IIconItemDefine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<int> Supplies { get; set; } = new ();//消耗物的Id
        public List<int> Sum { get; set; } = new();//消耗食物的数量
        public int Price { get; set; }//出售的基础价格
        public string Description { get; set; }//描述
        public List<string> Acclaim { get; set; } = new();//好评列表
        
    }
}

