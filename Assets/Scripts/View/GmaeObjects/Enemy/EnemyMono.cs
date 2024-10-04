using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class EnemyMono : MonoBehaviour, IHurt,IController
{
    public EnemyData data { get; set; }

    [SerializeField]
    float speed;
    [SerializeField]
    float attackDistance;
    [SerializeField]
    float stopDistance;
    [SerializeField]
    float attackCD;


    Rigidbody2D rb;
    Animator anim;
    Animator weaponAnim;
    Transform target;
    Weapon weapon;
    float distance;
    Timer timer;
    bool canAttack = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        weapon.owner = transform;
        weaponAnim = weapon.GetComponentInChildren<Animator>();
        timer = new Timer();
        data.RegisterEnemyDieEvent(OnDie);
        data.RegisterEnemyHurtEvent(OnHurt);
    }

    private void Update()
    {
        FindTarget();
        TryAttack();
        UpdateWeapon();

    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Hurt(int damage)
    {
        data.Hurt(1);
    }

    public void OnHurt(int damage)
    {
        Debug.Log(data.hp);
    }

    private void OnDie()
    {
        this.SendCommand(new KillEnemyCommand(data));
        Destroy(gameObject, 0);
    }

    void TryAttack()
    {
        if (distance > attackDistance)
            return;
        if (canAttack)
        {
            weaponAnim.SetTrigger("Attack");
            canAttack = false;
            timer.Time(attackCD, () => canAttack = true);
        }
    }

    void Move()
    {
        if (distance > stopDistance)
            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
    }
    void FindTarget()
    {
        if ((PlayerMono.instance.transform.position - transform.position).magnitude <= 3)
            target = PlayerMono.instance.transform;
        else
            target = RootMono.instance.transform;
        distance = (target.position - transform.position).magnitude;
    }

    void UpdateWeapon()
    {
        weapon.target = target;
    }

    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }
}
