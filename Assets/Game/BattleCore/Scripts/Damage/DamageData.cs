using UnityEngine;

public struct DamageData
{
    public float damage;
    public Vector2 damageDirection;
    public InterruptedType interruptedType;
    /// <summary>
    /// Stun or Freeze duration.
    /// </summary>
    public float interruptedDuration;
}
