using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMono : MonoBehaviour
{
    [SerializeField]
    Light2D viewlight;
    [SerializeField]
    float _speed;

    private void Update()
    {
        Move();
        RotateViewLight();
    }
    void Move()
    {
        transform.Translate(InputController.movement * _speed * Time.deltaTime, Space.World);
    }

    Vector3 targetPos;
    Vector3 dirction;
    void RotateViewLight()
    {
        targetPos = Camera.main.ScreenToWorldPoint(new Vector3(InputController.mousePosition.x, InputController.mousePosition.y, 10));
        dirction = (targetPos - transform.position).normalized;
        viewlight.transform.localRotation = Quaternion.FromToRotation(transform.up, dirction);
    }
}
