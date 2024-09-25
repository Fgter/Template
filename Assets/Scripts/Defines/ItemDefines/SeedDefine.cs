namespace Define
{
    public class SeedDefine : IIconItemDefine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int TargetPlant { get; set; }
        public int UnlockLevel { get; set; }
        public int TradingPrice { get; set; }
    }
}
