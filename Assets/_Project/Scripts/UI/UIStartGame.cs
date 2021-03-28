using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStartGame : MonoBehaviour
{

    [SerializeField] private string _sceneName = "Game";

    private Button _uiButton;

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
        SceneManager.LoadScene(_sceneName);
    }

}
