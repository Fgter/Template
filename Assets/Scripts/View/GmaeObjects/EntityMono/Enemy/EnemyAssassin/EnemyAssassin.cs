using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class EnemyAssassin : EnemyMonoBase
{
    protected override void UpdateWeapon()
    {
    }

    protected override void TryAttack()
    {
        if (distance > data.define.AttackDistance)
            return;
        if (canAttack)
        {
            this.SendCommand(new AttackCommand(transform, PlayerMono.instance.transform, 10));
            canAttack = false;
            timer.Delay(data.define.AttackCD, () => canAttack = true);
        }
        
    }
}
