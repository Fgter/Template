using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class ThrowArea : MonoBehaviour, IController
{
    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(this.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Destroy(transform.root.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            this.SendCommand(new AttackCommand(transform, PlayerMono.instance.transform, 10));
        if (collision.tag == "Root")
            this.SendCommand(new AttackCommand(transform, collision.transform, 1));
    }
}
