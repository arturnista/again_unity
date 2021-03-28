using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExitButton : MonoBehaviour
{
    private Button _uiButton;

    private void Awake()
    {
#if UNITY_WEBGL
        Destroy(gameObject);
#endif
    }

    private void OnEnable()
    {
        _uiButton = GetComponent<Button>();
        _uiButton.onClick.AddListener(HandleStartGame);
    }

    private void OnDisable()
    {
        _uiButton.onClick.RemoveListener(HandleStartGame);        
    }

    private void HandleStartGame()
    {
        Application.Quit();
    }
}
