using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;
    public float Health => _currentHealth;

    [Header("SFX")]
    [SerializeField] private AudioClip _damageSfx = default;
    [SerializeField] private AudioClip _deathSfx = default;

    private AudioSource _audioSource;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _audioSource = GetComponent<AudioSource>();
    }

    public void DealDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            _audioSource.PlayOneShot(_deathSfx);
            UIMessage.Instance.ShowMessage("YOU DIED!");
            EventManager.Instance.DispatchEvent(EventManager.EventType.PlayerDeath, gameObject);
        }
        else
        {
            _audioSource.PlayOneShot(_damageSfx);
            EventManager.Instance.DispatchEvent(EventManager.EventType.PlayerTakeDamage, gameObject);
        }
    }

    public void Restart()
    {
        _currentHealth = _maxHealth;
    }

}
