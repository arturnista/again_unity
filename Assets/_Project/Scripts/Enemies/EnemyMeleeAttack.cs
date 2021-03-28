using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{

    [SerializeField] protected float _damage = 15f;
    [SerializeField] protected float _anticipationTime = .2f;
    [SerializeField] protected float _recoverTime = 1f;

    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
    }

    protected override void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        var enemyMovement = GetComponent<EnemyMovement>();

        _isAttacking = true;
        enemyMovement.StopMovement();
        yield return new WaitForSeconds(_anticipationTime);

        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(.3f);

        Damage();

        yield return new WaitForSeconds(_recoverTime);
        enemyMovement.StartMovement();
        _isAttacking = false;
    }

    protected virtual void Damage()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= _damageDistance)
        {
            _player.DealDamage(_damage);
        }
    }
    
}
