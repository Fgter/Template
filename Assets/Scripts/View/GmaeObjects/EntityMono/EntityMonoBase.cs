using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityMonoBase : MonoBehaviour, IHurt
{
    [SerializeField]
    protected Image hpBar;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FindTarget()
    {

    }

    public virtual void Hurt(float damage)
    {
       
    }
    protected virtual void OnHurt(float damage)
    {

    }

    protected virtual void OnDie()
    {

    }
}
