using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;
    
    [SerializeField] private float _maxHealth = 100;

    [Range(0f, 1f)] [SerializeField] private float _stunPercentage = .5f;
    [SerializeField] private GameObject _hitPrefab = default;
    public GameObject HitEffectPrefab { get => _hitPrefab; }
    
    [SerializeField] private GameObject _soundPrefab = default;
    [SerializeField] private GameObject _dropPrefab = default;
    
    private float _currentHealth = 100;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void DealDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            if (_dropPrefab != null) Instantiate(_dropPrefab, transform.position, Quaternion.identity);
            if (_soundPrefab != null) Instantiate(_soundPrefab, transform.position, Quaternion.identity);
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            float stunRandom = Random.value;
            if (stunRandom < _stunPercentage)
            {
                GetComponent<EnemyMovement>().Stun();
            }
            StopAllCoroutines();
            StartCoroutine(HitReactionCoroutine());
        }
    }

    private IEnumerator HitReactionCoroutine()
    {
        var animator = GetComponentInChildren<Animator>();
        var sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.black;
        animator.speed = 0f;
        yield return new WaitForSeconds(.05f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(.05f);
        animator.speed = 1f;
    }

}
