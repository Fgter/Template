using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

public class PlayerMono : MonoSingleton<PlayerMono>, IController, IHurt
{
    [SerializeField]
    float attackDistance;
    [SerializeField]
    float attackCD;
    [SerializeField]
    Image hpBar;
    [SerializeField]
    Transform sprite;

    private Rigidbody2D rb;
    Animator anim;
    Animator weaponAnim;
    Vector3 movement;
    EnemyModel enemyModel;
    PlayerModel playerModel;
    Transform target;
    Weapon weapon;
    bool canAttack = true;
    Timer timer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyModel = this.GetModel<EnemyModel>();
        playerModel = this.GetModel<PlayerModel>();
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        weapon.owner = transform;
        weaponAnim = weapon.GetComponentInChildren<Animator>();
        timer = new Timer();

        playerModel.hp.Register(OnHurt).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void Update()
    {
        Move();
        GetTarget();
        TryAttack();
    }

    Vector3 left = new Vector3(-1, 1, 1);
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, vertical, 0);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        transform.position += movement * playerModel.speed * Time.deltaTime;

        //animation
        if (movement.sqrMagnitude > 0.1f)
        {
            if (!anim.GetBool("Move"))
                anim.SetBool("Move", true);
        }
        else
        {
            if (anim.GetBool("Move"))
                anim.SetBool("Move", false);
        }
        //×ªÏò
        if(movement.x>0)
        {
            if (sprite.localScale.x < 0)
                sprite.localScale = Vector3.one;
        }
        else if(movement.x < 0)
        {
            if (sprite.localScale.x > 0)
                sprite.localScale = left;
        }
    }

    public void Hurt(float damage, bool dir)
    {
        anim.SetTrigger("Hurt");
        this.SendCommand(new PlayerHurtCommand(damage));
    }

    void OnHurt(float currentHp)
    {
        hpBar.fillAmount = currentHp / playerModel.maxHp.Value;
    }

    void TryAttack()
    {
        if (target == null)
            return;
        if ((target.position - transform.position).magnitude > attackDistance)
            return;
        if (canAttack)
        {
            weaponAnim.SetTrigger("Attack");
            canAttack = false;
            timer.Delay(attackCD, () => canAttack = true);
        }
    }

    void GetTarget()
    {
        target = enemyModel.GetEnemyByDistance(transform);
        weapon.target = target;
    }
    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

}
