using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _stopDistance = 2f;

    private Sensor _sensor;

    private CharacterController _characterController;
    private Transform _player;

    private bool _hasTarget;
    private bool _isStunned;
    private bool _isStopped;

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<PlayerAttack>().transform;
        _characterController = GetComponent<CharacterController>();
        _sensor = GetComponentInChildren<Sensor>();

        _sensor.OnDetected.AddListener(HandleDetect);
        _hasTarget = false;
    }

    private void HandleDetect(GameObject gameObject, Sensor sensor)
    {
        _hasTarget = true;
        _sensor.OnDetected.RemoveAllListeners();
        Destroy(_sensor.gameObject);
    }

    private void Update()
    {
        if (!_hasTarget) return;
        if (_isStopped) return;
        if (_isStunned) return;
        
        if (Vector3.Distance(_player.position, transform.position) > _stopDistance)
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            Vector3 moveVelocity = direction * _moveSpeed;

            _characterController.Move(moveVelocity * Time.deltaTime);
        }
    }

    public void StopMovement()
    {
        _isStopped = true;
    }

    public void StartMovement()
    {
        _isStopped = false;
    }

    public void Stun()
    {
        StartCoroutine(StunCoroutine());
    }

    private IEnumerator StunCoroutine()
    {
        _isStunned = true;
        yield return new WaitForSeconds(Random.Range(0f, .3f));
        _isStunned = false;
    }

}
