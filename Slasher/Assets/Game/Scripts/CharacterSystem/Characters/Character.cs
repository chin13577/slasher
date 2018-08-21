using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Character : MonoBehaviour
{
    public HeroData status;


    protected void RotateTo(Vector2 position)
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward,
                         new Vector3(position.x, position.y, transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, status.speed * Time.deltaTime);
    }

    protected void MoveTo(Vector2 position)
    {
        float step = status.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, position, step);
    }

}

[Serializable]
public struct HeroData
{
    public string name;
    public float attack;
    public float defence;
    public float speed;
}