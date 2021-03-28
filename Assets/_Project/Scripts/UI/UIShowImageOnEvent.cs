using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowImageOnEvent : MonoBehaviour
{
    [SerializeField] private EventManager.EventType _event = EventManager.EventType.None;
    
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEventListener(_event, HandleEvent);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener(_event, HandleEvent);
    }

    private void HandleEvent(EventData data)
    {
        _image.enabled = true;
    }
}
