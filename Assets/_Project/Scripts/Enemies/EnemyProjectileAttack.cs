using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAttack : EnemyAttack
{

    [SerializeField] private GameObject _projectilePrefab = default;
    [SerializeField] private float _afterAttackDelay = 1f;

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
        yield return new WaitForSeconds(.2f);

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + (Vector3.up * 0.5f);
        var goCreated = Instantiate(_projectilePrefab, spawnPosition, Quaternion.LookRotation(direction, Vector3.up));
        goCreated.GetComponent<BasicProjectile>().Fire(direction, transform);

        yield return new WaitForSeconds(_afterAttackDelay);
        enemyMovement.StartMovement();
        _isAttacking = false;
    }
    
}
