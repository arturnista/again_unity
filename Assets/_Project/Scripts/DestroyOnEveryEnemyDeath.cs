using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEveryEnemyDeath : MonoBehaviour
{
    
    [SerializeField] private List<EnemyHealth> _enemies = default;

    private int _enemiesDead;

    private void Start()
    {
        foreach (var item in _enemies)
        {
            item.OnDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath()
    {
        _enemiesDead += 1;
        if (_enemiesDead == _enemies.Count)
        {
            Destroy(gameObject);
        }
    }

}
