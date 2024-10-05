using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : EnemyMonoBase
{
    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    GameObject bullet;

    bool canMove = true;
    LineRenderer lineRenderer;
    Vector3 targetPos;
    Coroutine attackCor;

    protected override void Start()
    {
        base.Start();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.gameObject.SetActive(false);
    }

    protected override void TryAttack()
    {
        if (distance > data.define.AttackDistance)
            return;
        if (!canAttack)
            return;
        attackCor = StartCoroutine(AttackIEnum());
       
    }

    protected override void OnHurt(float damage)
    {
        base.OnHurt(damage);
        if (attackCor != null)
        {
            StopCoroutine(attackCor);
            canMove = true;
            lineRenderer.gameObject.SetActive(false);
            timer.Delay(1, () => canAttack = true);
        }
    }

    IEnumerator AttackIEnum()
    {
        canAttack = false;
        canMove = false;
        SpawnLine();
        yield return new WaitForSeconds(1);
        Attack();
        attackCor = null;
        timer.Delay(data.define.AttackCD, () => canAttack = true);
    }

    void Attack()
    {

        GameObject go = GameObject.Instantiate(bullet);

        Vector3 dir = (target.position - targetPos).normalized;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, dir);

        go.transform.position = shootPoint.position;
        go.transform.rotation = rotation;
        go.GetComponent<Bullet>().targetPos = targetPos;

        lineRenderer.gameObject.SetActive(false);
        canMove = true;
    }

    protected override void Move()
    {
        if (!canMove)
            return;
        base.Move();
    }

    void SpawnLine()
    {
        targetPos = target.position;
        lineRenderer.gameObject.SetActive(true);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPos);
    }
}
