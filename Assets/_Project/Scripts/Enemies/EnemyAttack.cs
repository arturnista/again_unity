using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{

    [SerializeField] protected float _damageDistance = 1.5f;

    protected PlayerHealth _player;
    protected bool _isAttacking;

    protected virtual void Awake()
    {
        _isAttacking = false;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    protected void Update()
    {
        if (_isAttacking) return;
        if (Vector3.Distance(_player.transform.position, transform.position) <= _damageDistance)
        {
            Attack();
        }
    }

    protected abstract void Attack();
    
}
