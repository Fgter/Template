using Newtonsoft.Json;
using Define;

public class EnemyDefine:IDefine
{
    [JsonProperty("Hp")]
    public float MaxHP { get; set; }
    public float Speed { get; set; }
    public float AttackCD { get; set; }
    public float AttackDistance { get; set; }
    public float StopDistance { get; set; }

    public int FindEnemyLogic { get; set; }
}
