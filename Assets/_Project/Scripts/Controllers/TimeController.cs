using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{

    [SerializeField] private Image _image = default;

    private static float s_TotalTime;
    public static float TotalTime => s_TotalTime;

    private static float s_TimeRemaining;
    public static float TimeRemaining => s_TimeRemaining;

    private CharacterController _player;
    private Vector3 _spawnPosition;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        _spawnPosition = _player.transform.position;
        StartCoroutine(TimeCoroutine());

        _image.gameObject.SetActive(false);

        s_TotalTime = 0;
    }

    private void Update()
    {
        s_TotalTime += Time.deltaTime;
    }

    private IEnumerator TimeCoroutine()
    {
        while (true)
        {
            s_TimeRemaining = 60f;
            while (s_TimeRemaining > 0)
            {
                s_TimeRemaining -= Time.deltaTime;
                yield return null;
            }

            _player.enabled = false;
            _image.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            _player.transform.position = _spawnPosition;
            Time.timeScale = 0;

            UIMessage.Instance.ShowMessage("Your time is over\n\nGo again");

            yield return new WaitForSecondsRealtime(3f);

            GameObject.FindObjectOfType<PlayerHealth>().Restart();
            UIMessage.Instance.Clear();
            Time.timeScale = 1;
            _image.gameObject.SetActive(false);
            _player.enabled = true;
        }
    }

}
