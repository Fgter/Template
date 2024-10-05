using System;
using UnityEngine;
public class EnemyData
{
    Action<float> EnemyHurtEvent;
    Action EnemyDieEvent;
    public float hp { get; private set; }
    public bool dead { get; private set; }
    public Transform transform { get; set; }
    public EnemyDefine define { get; private set; }

    public void Init(EnemyDefine define)
    {
        this.define = define;
        hp = define.MaxHP;
        
    }

    public void Hurt(float damage)
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
    public void RegisterEnemyHurtEvent(Action<float> func) => EnemyHurtEvent += func;
    public void UnRegisterEnemyHurtEvent(Action<float> func) => EnemyHurtEvent -= func;
}
