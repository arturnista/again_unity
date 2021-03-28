using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    
    private CharacterController _characterController;

    [Header("Speed")]
    [SerializeField] private float _acceleration = 3f;
    [SerializeField] private float _forwardWalkSpeed = 3f;
    [SerializeField] private float _backwardMoveSpeed = 2f;
    [SerializeField] private float _stepSize = 2.5f;

    [Header("SFX")]
    [SerializeField] private AudioClip[] _footstepsSfx = default;
    
    private AudioSource _audioSource = default;
    private Animator _animator;

    private Vector3 _velocity = default;
    private Vector3 _lastPosition;
    private float _totalMoved;

    private Vector3 _targetVelocity;
    private Vector3 _currentSpeed;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (PauseController.IsPaused) return;
        
        // _characterController.Move(_velocity * Time.deltaTime);
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movement = Vector3.right * horizontalMovement + Vector3.forward * verticalMovement;
        movement = Vector3.Normalize(movement);

        float speed = (verticalMovement >= 0) ? _forwardWalkSpeed : _backwardMoveSpeed;
        _targetVelocity = movement * speed;
        _currentSpeed = Vector3.MoveTowards(_currentSpeed, _targetVelocity, _acceleration * Time.deltaTime);

        _animator.SetFloat("Speed", _currentSpeed.magnitude);

        Vector3 globalVelocity = transform.TransformDirection(_currentSpeed);
        CollisionFlags collisionFlags = _characterController.Move((globalVelocity + _velocity) * Time.deltaTime);
		if(collisionFlags == CollisionFlags.Above && _velocity.y > 0f) _velocity.y = 0f;

        // FootstepsSoundEffects();

    }

    private void FootstepsSoundEffects()
    {
        if (_characterController.velocity.magnitude > 0 && !_audioSource.isPlaying)
        {
            float moveFromLastPosition = (_lastPosition - _characterController.transform.position).magnitude;
            _lastPosition = _characterController.transform.position;
            _totalMoved += moveFromLastPosition;

            if (_totalMoved >= _stepSize)
            {
                _audioSource.PlayOneShot(GetRandomFootstepClip(_footstepsSfx));
                _totalMoved = 0f;
            }
        }
    }

    private AudioClip GetRandomFootstepClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
