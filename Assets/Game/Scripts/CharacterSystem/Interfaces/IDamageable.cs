using UnityEngine;

public interface IDamageable
{
    Vector3 direction { get; }
    bool CanAttack { get; }

    Transform GetTransform();
    void TakeDamage(float damage);
    void TakeDamage(GameObject attacker);
}
