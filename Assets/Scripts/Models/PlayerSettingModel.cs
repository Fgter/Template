using QFramework;

public class PlayerSettingModel : AbstractModel
{
    public float musicVolume { get; set; } = 1;
    public float soundVolume { get; set; } = 1;
    public bool musicMute { get; set; } = false;
    public bool soundMute { get; set; } = false;
    protected override void OnInit()
    {
        Load();
        CommonMono.AddQuitAction(Save);
    }

    void Load()
    {
        PlayerSettingSaveData data = new PlayerSettingSaveData();
        data = this.GetUtility<Storage>().Load<PlayerSettingSaveData>();
        if (data == default)
            return;
        musicVolume = data.musicVolume;
        soundVolume = data.soundVolume;
        musicMute = data.musicMute;
        soundMute = data.soundMute;
    }

    void Save()
    {
        PlayerSettingSaveData data = new PlayerSettingSaveData();
        data.musicVolume = musicVolume;
        data.soundVolume = soundVolume;
        data.musicMute = musicMute;
        data.soundMute = soundMute;
        this.GetUtility<Storage>().Save(data);
     }
}