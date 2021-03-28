using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    private void OnEnable()
    {
        EventManager.Instance.AddEventListener(EventManager.EventType.GameWin, HandleGameWin);
        EventManager.Instance.AddEventListener(EventManager.EventType.PlayerDeath, HandlePlayerDeath);
    }
    
    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener(EventManager.EventType.GameWin, HandleGameWin);
        EventManager.Instance.RemoveEventListener(EventManager.EventType.PlayerDeath, HandlePlayerDeath);
    }

    private void HandlePlayerDeath(EventData data)
    {
        StartCoroutine(DeathCoroutine());
    }

    private void HandleGameWin(EventData data)
    {
        StartCoroutine(WinCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        Time.timeScale = 0f;

        Transform camera = Camera.main.transform;
        camera.SetParent(null);

        var originalAngle = camera.eulerAngles;

        float time = 0f;
        while (time < 1f)
        {
            time += Time.unscaledDeltaTime;
            
            camera.eulerAngles = Vector3.Lerp(originalAngle, originalAngle + new Vector3(-45f, 0f, 0f), time);
            yield return null;
        }
        
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator WinCoroutine()
    {
        KillAllEnemies();
        UIMessage.Instance.ShowMessage("Completed");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("WinGame");
    }

    private void KillAllEnemies()
    {
        EnemyHealth[] enemies = GameObject.FindObjectsOfType<EnemyHealth>();
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }
    }

}
