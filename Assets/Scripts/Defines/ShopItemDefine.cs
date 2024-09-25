namespace Define
{
    public class ShopItemDefine : IDefine
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int sellCount { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}