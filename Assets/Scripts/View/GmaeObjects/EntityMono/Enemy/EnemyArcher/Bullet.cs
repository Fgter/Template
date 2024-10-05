using UnityEngine;
using QFramework;

class Bullet : MonoBehaviour, IController
{
    public Vector3 targetPos;
    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    private void Update()
    {
        transform.position += (targetPos - transform.position).normalized * 5 * Time.deltaTime;
        if(Vector3.Distance(targetPos,transform.position)<=0.01f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.SendCommand(new AttackCommand(transform, PlayerMono.instance.transform, 10));
            Destroy(gameObject);

        }
        if (collision.tag == "Root")
        {
            this.SendCommand(new AttackCommand(transform, collision.transform, 1));
            Destroy(gameObject);
        }
            

    }
}
