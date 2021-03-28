using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    
    private static int _enemiesKilled;
    public static int EnemiesKilled => _enemiesKilled;
    private static int _enemiesTotal;
    public static int EnemiesTotal => _enemiesTotal;

    private EnemyHealth[] _enemiesHealth;

    private void Start()
    {
        _enemiesKilled = 0;
        
        _enemiesHealth = GameObject.FindObjectsOfType<EnemyHealth>();
        _enemiesTotal = _enemiesHealth.Length;

        foreach (var item in _enemiesHealth)
        {
            item.OnDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath()
    {
        _enemiesKilled += 1;
    }

}
