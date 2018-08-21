using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Character : MonoBehaviour
{
    public HeroData status;
}

[Serializable]
public struct HeroData
{
    public string name;
    public float attack;
    public float defence;
    public float speed;
}