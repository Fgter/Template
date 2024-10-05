using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrower : EnemyMonoBase
{
    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    GameObject throwArea;
    [SerializeField]
    GameObject thrown;

    bool canMove = true;
    Coroutine attackCor;

    protected override void TryAttack()
    {
        if (distance > data.define.AttackDistance)
            return;
        if (!canAttack)
            return;
        attackCor = StartCoroutine(AttackIEnum());
    }

    IEnumerator AttackIEnum()
    {
        canAttack = false;
        weaponAnim.SetTrigger("Attack");
        //生成投掷点
        GameObject go1 = GameObject.Instantiate(throwArea);
        float randomX = Random.Range(-0.5f, 0.5f);
        float randomY = Random.Range(-0.5f, 0.5f);
        Vector3 targetPos = target.position + new Vector3(randomX, randomY, 0);
        go1.transform.position = targetPos;
        float time = go1.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

        //生成投掷物
        GameObject go2 = GameObject.Instantiate(thrown);
        Thrown t = go2.GetComponent<Thrown>();
        t.startPos = shootPoint.position;
        t.targetPos = targetPos;
        t.timeToTarget = time;

        yield return new WaitForSeconds(time);
        canAttack = true;
    }


}
