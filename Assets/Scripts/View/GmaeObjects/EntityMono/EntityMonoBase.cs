using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityMonoBase : MonoBehaviour, IHurt
{
    [SerializeField]
    protected Image hpBar;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FindTarget()
    {

    }

    public virtual void Hurt(float damage, bool dir)
    {
       
    }
    protected virtual void OnHurt(float damage)
    {

    }

    protected virtual void OnDie()
    {

    }
}
