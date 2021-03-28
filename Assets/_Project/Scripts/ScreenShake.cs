using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    private static ScreenShake s_Instance;

    public static void Shake(float strength, float time)
    {
        s_Instance.StartCoroutine(s_Instance.ScreenShakeCoroutine(strength, time));
    }

    private void Awake()
    {
        s_Instance = this;
    }
    
    private IEnumerator ScreenShakeCoroutine(float strength, float time)
    {
        float currentTime = 0f;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            transform.localPosition = Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = Vector3.zero;
    }

}
