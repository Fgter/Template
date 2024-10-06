using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class Gold : MonoBehaviour,IController
{
    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            this.SendCommand(new IncreaseGoldCommand(1));
            Destroy(gameObject);
        }
    }
}
