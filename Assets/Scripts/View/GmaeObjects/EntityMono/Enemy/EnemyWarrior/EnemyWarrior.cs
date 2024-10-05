using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : EnemyMonoBase
{
    protected override void TryAttack()
    {
        if (distance > data.define.AttackDistance)
            return;
        if (canAttack)
        {
            weaponAnim.SetTrigger("Attack");
            canAttack = false;
            timer.Delay(data.define.AttackCD, () => canAttack = true);
        }
    }
}
