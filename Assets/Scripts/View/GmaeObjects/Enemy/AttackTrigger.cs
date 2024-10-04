using UnityEngine;
using QFramework;

class AttackTrigger : MonoBehaviour, IController
{
    enum TriggerType
    {
        Player,
        Enemy
    }
    [SerializeField]
    TriggerType triggerType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (triggerType)
        {
            case TriggerType.Player:
                if (collision.tag == "Enemy")
                    this.SendCommand(new AttackCommand(PlayerMono.instance.transform, collision.transform, 1));
                break;
            case TriggerType.Enemy:
                if (collision.tag == "Player")
                    this.SendCommand(new AttackCommand(transform, PlayerMono.instance.transform, 1));
                else if(collision.tag == "Root")
                    this.SendCommand(new AttackCommand(transform, collision.transform, 1));
                break;
        }
    }

    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }
}
