using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public struct SpawnRuleDefine : IDefine
{
    public int Round { get; set; }
    public int Enemy1Count { get; set; }
    public int Enemy2Count { get; set; }
    public int Enemy3Count { get; set; }
    public int Enemy4Count { get; set; }
    public float SpawnInterval { get; set; }
    public int EachSpawnCount { get; set; }
}
