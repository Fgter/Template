using QFramework;
namespace Models
{
    public class PlayerModel : AbstractModel
    {
        public bool isFirstEnter;
        public BindableProperty<int> Gold { get; set; } = new();
        protected override void OnInit()
        {
            isFirstEnter = true;
            Load();
            CommonMono.AddQuitAction(Save);
        }

        void Load()
        {
            PlayerSaveData data = this.GetUtility<Storage>().Load<PlayerSaveData>();
            if (data == default)
                return;
            Gold.Value = data.gold;
            isFirstEnter = data.isFirstEnter;
        }

        void Save()
        {
            PlayerSaveData data = new PlayerSaveData();
            data.gold = Gold.Value;
            data.isFirstEnter = isFirstEnter;
            this.GetUtility<Storage>().Save(data);
        }
    }

}
