using System;

[Serializable]
public class Status
{
    public int hp;
    public int maxHp;
    public int attack;
    public int defense;
    public float speed;

    public Status()
    {
        hp = maxHp;
    }
}