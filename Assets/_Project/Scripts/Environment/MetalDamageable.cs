using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDamageable : MonoBehaviour, IDamageable
{

    [SerializeField] private GameObject _hitPrefab = default;
    public GameObject HitEffectPrefab { get => _hitPrefab; }

    public void DealDamage(float damage)
    {
        
    }

}
