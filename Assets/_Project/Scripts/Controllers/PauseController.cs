using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    
    private static bool s_IsPaused;
    public static bool IsPaused => s_IsPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (s_IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void OnDisable()
    {
        Resume();
    }

    private void Pause()
    {
        s_IsPaused = true;
        Time.timeScale = 0f;
        EventManager.Instance.DispatchEvent(EventManager.EventType.GamePause, gameObject);
    }

    private void Resume()
    {
        s_IsPaused = false;
        Time.timeScale = 1f;
        EventManager.Instance.DispatchEvent(EventManager.EventType.GameResume, gameObject);
    }

}
