using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class EnemyMonoBase : EntityMonoBase, IController
{
    public EnemyData data { get; set; }
    public int defineId { get => _defineId; }

    [SerializeField]
    int _defineId;

    protected Animator weaponAnim;
    protected Transform target;
    protected Weapon weapon;
    protected float distance;
    protected Timer timer;
    protected bool canAttack = true;

    protected virtual void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        weapon.owner = transform;
        weaponAnim = weapon.GetComponentInChildren<Animator>();
        timer = new Timer();
        data.RegisterEnemyDieEvent(OnDie);
        data.RegisterEnemyHurtEvent(OnHurt);
    }

    protected virtual void Update()
    {
        FindTarget();
        TryAttack();
        UpdateWeapon();

    }
    protected virtual void FixedUpdate()
    {
        Move();
    }

    public override void Hurt(float damage)
    {
        data.Hurt(damage);
    }

    protected override void OnHurt(float damage)
    {
        hpBar.fillAmount = data.hp / data.define.MaxHP;
    }

    protected override void OnDie()
    {
        this.SendCommand(new KillEnemyCommand(data));
        Destroy(gameObject, 0);
    }

    protected virtual void TryAttack() { }

    protected virtual void Move()
    {
        if (distance > data.define.StopDistance)
            transform.position += (target.position - transform.position).normalized * data.define.Speed * Time.deltaTime;
    }
    protected override void FindTarget()
    {
        switch (data.define.FindEnemyLogic)
        {
            case 1:
                if ((PlayerMono.instance.transform.position - transform.position).magnitude <= 4)
                    target = PlayerMono.instance.transform;
                else
                    target = RootMono.instance.transform;
                break;
            case 2:
                target = PlayerMono.instance.transform;
                break;
        }

        distance = (target.position - transform.position).magnitude;
    }

    protected virtual void UpdateWeapon()
    {
        weapon.target = target;
    }

    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }
}
