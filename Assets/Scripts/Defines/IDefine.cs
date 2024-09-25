namespace Define
{
    public interface IDefine
    {
    }

    public interface IIconItemDefine:IDefine //给可以在UI中显示出来Icon的物品用的
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
