using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOnTrigger : MonoBehaviour
{
    
    [SerializeField] private Transform _mesh = default;
    [SerializeField] private BoxCollider _collider;
    [Header("Locked")]
    [SerializeField] private bool _isLocked = false;
    [SerializeField] private string _lockRequirement = "";
    [SerializeField] private EventManager.EventType _unlockOnEvent = EventManager.EventType.None;

    private bool _isOpen = false;

    private Vector3 _initialPosition;
    private Vector3 _finalPosition;

    private int _entitiesInside;

    private void Awake()
    {
        _initialPosition = _mesh.localPosition;
        _finalPosition = -Vector3.forward * 1.1f;
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEventListener(_unlockOnEvent, HandleUnlockEvent);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener(_unlockOnEvent, HandleUnlockEvent);
    }

    private void HandleUnlockEvent(EventData data)
    {
        _isLocked = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_isLocked)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                UIMessage.Instance.ShowMessage(string.Format("{0} required", _lockRequirement));
            }

            return;
        }

        if (_entitiesInside == 0)
        {
            Open();
        }
        _entitiesInside += 1;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (_isLocked) return;
        _entitiesInside -= 1;
        if (_entitiesInside == 0)
        {
            Close();
        }
    }

    protected virtual void Open()
    {
        StopAllCoroutines();
        StartCoroutine(OpenCoroutine());
    }

    protected virtual void Close()
    {
        StopAllCoroutines();
        StartCoroutine(CloseCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        float timePerc = 0f;
        while (timePerc < 1f)
        {   
            timePerc += 3f * Time.deltaTime;
            _mesh.localPosition = Vector3.Lerp(_initialPosition, _finalPosition, timePerc);
            yield return null;
        }

        _collider.enabled = false;
    }

    private IEnumerator CloseCoroutine()
    {
        float timePerc = 0f;
        while (timePerc < 1f)
        {   
            timePerc += 3f * Time.deltaTime;
            _mesh.localPosition = Vector3.Lerp(_finalPosition, _initialPosition, timePerc);
            yield return null;
        }

        _collider.enabled = true;
    }

}
