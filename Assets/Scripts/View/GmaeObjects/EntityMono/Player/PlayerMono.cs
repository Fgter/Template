using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;

public class PlayerMono : MonoSingleton<PlayerMono>, IController, IHurt
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float attackDistance;
    [SerializeField]
    float attackCD;
    [SerializeField]
    Image hpBar;

    private Rigidbody2D rb;
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

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, vertical, 0);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    public void Hurt(float damage)
    {
        this.SendCommand(new PlayerHurtCommand(damage));
    }

    void OnHurt(float currentHp)
    {
        hpBar.fillAmount = currentHp / playerModel.maxHp;
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
