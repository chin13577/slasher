using System;

[Serializable]
public class Status
{
    public int hp;
    public int maxHp;
    public float attack;
    public float defense;
    public float speed;

    public Status()
    {
        hp = maxHp;
    }
}