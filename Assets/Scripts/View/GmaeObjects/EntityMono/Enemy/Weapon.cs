using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform target { get; set; }
    public Transform owner { get; set; }
    [SerializeField]
    float weaponOffset;

    private void Update()
    {
        if (target == null)
            return;
        Vector3 dir = (target.position - owner.transform.position).normalized;
        transform.position = owner.transform.position + dir * weaponOffset;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, dir);
        transform.rotation = rotation;
    }
}
