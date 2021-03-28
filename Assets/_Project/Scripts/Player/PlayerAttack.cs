using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [Header("Speed")]
    [SerializeField] protected float _fireDelay = 0.5f;
    [Header("Attack")]
    [SerializeField] protected float _damage = 30f;
    [SerializeField] protected float _bonusDamage = 5f;
    [SerializeField] protected LayerMask _hitMask = default;
    [SerializeField] protected AudioClip _fireClip = default;
    [SerializeField] protected AudioClip _reloadClip = default;
    [Header("Effects")]
    [SerializeField] protected ParticleSystem _muzzleFlashEffect = default;
    [SerializeField] protected Animator _gunAnimator = default;
    [Header("Reload")]
    [SerializeField] protected int _ammoAmount = 30;
    [SerializeField] protected float _reloadTime = 1f;

    private Transform _head;
    private bool _isFiring;
    private float _fireTime;
    private AudioSource _audioSource;

    private bool _isReloading;
    public bool IsReloading => _isReloading;
    private int _currentAmmo;
    public int CurrentAmmo => _currentAmmo;
    public int AmmoAmount => _ammoAmount;

    private bool _hasSecondGun;
    private int _attackIndex;
    protected ParticleSystem _secondMuzzleFlashEffect = default;
    protected Animator _secondGunAnimator = default;

    private void Awake()
    {
        _currentAmmo = _ammoAmount;
        _isReloading = false;
        _audioSource = GetComponent<AudioSource>();
        _head = GetComponentInChildren<Camera>().transform.parent;
    }
    
    private void Update()
    {
        if (PauseController.IsPaused) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isFiring = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isFiring = false;
        }

        // if (!_isReloading && _currentAmmo < _ammoAmount && Input.GetKeyDown(KeyCode.R))
        // {
        //     StartCoroutine(ReloadCoroutine());
        // }

        if (_isReloading) return;

        _fireTime += Time.deltaTime;
        if (_isFiring && _fireTime > _fireDelay)
        {
            _fireTime = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        // if (_currentAmmo <= 0)
        // {
        //     StartCoroutine(ReloadCoroutine());
        //     return;
        // }

        // _currentAmmo -= 1;
        _attackIndex += 1;

        if (_hasSecondGun)
        {
            if (_attackIndex % 2 == 0)
            {
                _secondMuzzleFlashEffect.Play();
                _secondGunAnimator.SetTrigger("Fire");
            }
            else
            {
                _muzzleFlashEffect.Play();
                _gunAnimator.SetTrigger("Fire");
            }
        }
        else
        {
            _muzzleFlashEffect.Play();
            _gunAnimator.SetTrigger("Fire");
        }
        _audioSource.PlayOneShot(_fireClip);

        RaycastHit hit;
        Vector3 direction = _head.forward;

        if (Physics.Raycast(_head.position, direction, out hit, Mathf.Infinity, _hitMask))
        {
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DealDamage(_damage);
                
                GameObject created = Instantiate(damageable.HitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));
                created.transform.parent = hit.collider.transform;
            }

        }
    }

    private IEnumerator ReloadCoroutine()
    {
        _currentAmmo = 0;
        _isReloading = true;
        _audioSource.PlayOneShot(_reloadClip);

        yield return new WaitForSeconds(_reloadTime);
        
        _currentAmmo = _ammoAmount;
        _isReloading = false;
    }

    public void BonusDamage()
    {
        _fireDelay = _fireDelay / 2f;
        // _damage += _bonusDamage;
        if (_hasSecondGun) return;
        
        _hasSecondGun = true;
        GameObject gunObject = _gunAnimator.transform.parent.gameObject;
        var created = Instantiate(gunObject, gunObject.transform.parent);
        created.transform.localPosition = gunObject.transform.localPosition;
        var createdPosition = created.transform.localPosition;
        createdPosition.x = -0.2f;
        created.transform.localPosition = createdPosition;

        var gunObjectPosition = gunObject.transform.localPosition;
        gunObjectPosition.x = 0.2f;
        gunObject.transform.localPosition = gunObjectPosition;

        _secondMuzzleFlashEffect = created.GetComponentInChildren<ParticleSystem>();
        _secondGunAnimator = created.GetComponentInChildren<Animator>();
    }

}
