using UnityEngine;

public interface IDamageable
{
    Vector3 direction { get; }
    Vector3 position { get; }
    bool CanAttacked { get; }

    Transform GetTransform();
    void TakeDamage(float damage);
    void TakeDamage(GameObject attacker);
}
