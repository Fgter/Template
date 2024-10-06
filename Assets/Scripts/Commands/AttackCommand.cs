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
        bool hDir = this.target.position.x - self.position.x > 0 ? true : false;
        if (target != null)
        {
            target.Hurt(value,hDir);
        }
        if (rb != null)
        {
            Vector3 dir = (this.target.position - self.position).normalized;
            rb.AddForce(dir * 10, ForceMode2D.Impulse);
        }
    }
}
