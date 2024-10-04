using System;
using UnityEngine;
public class EnemyData
{
    Action<int> EnemyHurtEvent;
    Action EnemyDieEvent;
    public int hp { get; private set; }
    public bool dead { get; private set; }
    public Transform transform { get; set; }

    public void Init()
    {
        hp = 3;
    }

    public void Hurt(int damage)
    {
        hp = hp - damage > 0 ? hp - damage : 0;
        if (hp <= 0)
        {
            Die();
            EnemyDieEvent?.Invoke();
            return;
        }
        EnemyHurtEvent?.Invoke(hp);
    }

    void Die()
    {
        dead = true;
    }
    public void RegisterEnemyDieEvent(Action func) => EnemyDieEvent += func;
    public void UnRegisterEnemyDieEvent(Action func) => EnemyDieEvent -= func;
    public void RegisterEnemyHurtEvent(Action<int> func) => EnemyHurtEvent += func;
    public void UnRegisterEnemyHurtEvent(Action<int> func) => EnemyHurtEvent -= func;
}
