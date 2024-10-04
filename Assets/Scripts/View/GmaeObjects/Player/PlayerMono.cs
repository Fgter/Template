using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class PlayerMono : MonoSingleton<PlayerMono>, IController, IHurt
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float attackDistance;
    [SerializeField]
    float attackCD;

    private Rigidbody2D rb;
    Animator weaponAnim;
    Vector3 movement;
    EnemyModel model;
    Transform target;
    Weapon weapon;
    bool canAttack = true;
    Timer timer;

    private void Start()
    {
        model = this.GetModel<EnemyModel>();
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        weapon.owner = transform;
        weaponAnim = weapon.GetComponentInChildren<Animator>();
        timer = new Timer();
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

    public void Hurt(int damage)
    {
        this.SendCommand<PlayerHurtCommand>();
        Debug.Log(this.GetModel<PlayerModel>().hp.Value);
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
            timer.Time(attackCD, () => canAttack = true);
        }
    }

    void GetTarget()
    {
        target = model.GetEnemyByDistance(transform);
        weapon.target = target;
    }
    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

}
