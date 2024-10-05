using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrown : MonoBehaviour
{
    public Vector3 targetPos { get; set; } 
    public float timeToTarget { get; set; }
    public Vector3 startPos { get; set; }
    float elapsedTime;
    void Update()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        transform.Rotate(-Vector3.forward * Time.deltaTime * 40);
    }

    void Move()
    {
        if (elapsedTime < timeToTarget)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / timeToTarget;

            float x = Mathf.Lerp(startPos.x, targetPos.x, t);
            float y = Mathf.Lerp(startPos.y, targetPos.y, t) + Mathf.Sin(t * Mathf.PI) * 2.0f;

            transform.position = new Vector3(x, y, startPos.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
