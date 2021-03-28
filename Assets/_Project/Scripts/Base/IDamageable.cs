using UnityEngine;

public interface IDamageable
{

    GameObject HitEffectPrefab { get; }
    void DealDamage(float damage);

}