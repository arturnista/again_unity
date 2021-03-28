using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultipleAttacks : MonoBehaviour
{
    
    [SerializeField] private float _changeDistance = 2f;

    private EnemyMeleeAttack _meleeAttack;
    private EnemyProjectileAttack _projectileAttack;

    protected PlayerHealth _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        _meleeAttack = GetComponent<EnemyMeleeAttack>();
        _projectileAttack = GetComponent<EnemyProjectileAttack>();

        _meleeAttack.enabled = false;
        _projectileAttack.enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) > _changeDistance)
        {
            _projectileAttack.enabled = true;
            _meleeAttack.enabled = false;
        }
        else
        {
            _projectileAttack.enabled = false;
            _meleeAttack.enabled = true;
        }
    }

}
