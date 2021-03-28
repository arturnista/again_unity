using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _canvas = default;

    private void OnEnable()
    {
        HandleGameResume(null);
        EventManager.Instance.AddEventListener(EventManager.EventType.GamePause, HandleGamePause);
        EventManager.Instance.AddEventListener(EventManager.EventType.GameResume, HandleGameResume);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener(EventManager.EventType.GamePause, HandleGamePause);
        EventManager.Instance.RemoveEventListener(EventManager.EventType.GameResume, HandleGameResume);
    }

    private void HandleGamePause(EventData data)
    {
        _canvas.SetActive(true);
    }

    private void HandleGameResume(EventData data)
    {
        _canvas.SetActive(false);
    }

}
