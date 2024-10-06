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
    [SerializeField]
    Transform sprite;

    protected Animator weaponAnim;
    protected Transform target;
    protected Weapon weapon;
    protected float distance;
    protected Timer timer;
    protected bool canAttack = true;
    protected RootSystem rootSystem;

    protected override void Start()
    {
        base.Start();
        rootSystem = this.GetSystem<RootSystem>();
        weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.owner = transform;
            weaponAnim = weapon.GetComponentInChildren<Animator>();
        }
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

    public override void Hurt(float damage, bool dir)
    {
        data.Hurt(damage);
    }

    protected override void OnHurt(float damage)
    {
        hpBar.fillAmount = data.hp / data.define.MaxHP;
        anim.SetTrigger("Hurt");
    }

    protected override void OnDie()
    {
        this.SendCommand(new KillEnemyCommand(data));
        Destroy(gameObject, 0);
    }

    protected virtual void TryAttack() { }

    protected virtual void Move()
    {
        if (target == null)
            return;
        if (distance > data.define.StopDistance)
        {
            transform.position += (target.position - transform.position).normalized * data.define.Speed * Time.deltaTime;
            //anim
            if (!anim.GetBool("Move"))
                anim.SetBool("Move", true);
        }
        else
        {
            //anim
            if (anim.GetBool("Move"))
                anim.SetBool("Move", false);
        }
        //×ªÏò
        float dir = target.position.x - transform.position.x;
        sprite.localScale = dir > 0 ? Vector3.one : new Vector3(-1, 1, 1);
    }
    protected override void FindTarget()
    {
        switch (data.define.FindEnemyLogic)
        {
            case 1:
                if ((PlayerMono.instance.transform.position - transform.position).magnitude <= 4)
                    target = PlayerMono.instance.transform;
                else
                    target = rootSystem.GetNearestRoot(transform);
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
