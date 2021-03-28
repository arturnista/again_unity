using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicProjectile : MonoBehaviour
{
    
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _speed = 5f;

    private Rigidbody _rigidbody;
    private Transform _creator;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 direction, Transform creator)
    {
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        _rigidbody.velocity = direction * _speed;
        _creator = creator;
    }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerHealth damageable = collider.gameObject.GetComponent<PlayerHealth>();
        if (damageable != null)
        {
            damageable.DealDamage(_damage);
        }

        Destroy(gameObject);
    }

}
