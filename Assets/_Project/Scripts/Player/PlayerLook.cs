using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private float _mouseSensitivity = 1f;
    
    private Transform _head;
    private float xRotation = 0f;

    private void Awake()
    {
        _head = GetComponentInChildren<Camera>().transform.parent;
        _mouseSensitivity = ConfigurationManager.Instance.Sensibility;
    }
    
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EventManager.Instance.AddEventListener(EventManager.EventType.GamePause, HandlePause);
        EventManager.Instance.AddEventListener(EventManager.EventType.GameResume, HandleResume);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        EventManager.Instance.RemoveEventListener(EventManager.EventType.GamePause, HandlePause);
        EventManager.Instance.RemoveEventListener(EventManager.EventType.GameResume, HandleResume);
    }

    private void HandlePause(EventData data)
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void HandleResume(EventData data)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (PauseController.IsPaused) return;
        
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        // _head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}