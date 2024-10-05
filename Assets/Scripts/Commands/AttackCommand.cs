using QFramework;
using UnityEngine;
class AttackCommand : AbstractCommand
{
    Transform target;
    Transform self;
    int value;
    public AttackCommand(Transform self,Transform target, int value)
    {
        this.self = self;
        this.target = target;
        this.value = value;
    }
    protected override void OnExecute()
    {
        IHurt target = this.target.GetComponent<IHurt>();
        Rigidbody2D rb = this.target.GetComponentInChildren<Rigidbody2D>();
        if (target != null)
        {
            target.Hurt(value);
        }
        if (rb != null)
        {
            Vector3 dir = (this.target.position - self.position).normalized;
            rb.AddForce(dir * 10, ForceMode2D.Impulse);
        }
    }
}
