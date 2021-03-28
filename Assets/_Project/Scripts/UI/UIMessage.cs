using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMessage : MonoBehaviour
{

    private static UIMessage s_Instance;
    public static UIMessage Instance => s_Instance;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        s_Instance = this;

        _text = GetComponent<TextMeshProUGUI>();
        _text.enabled = false;
        _text.text = "";
    }
    
    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(MessageCoroutine(message));
    }
    
    public void Clear()
    {
        StopAllCoroutines();

        _text.enabled = false;
        _text.text = "";
    }

    private IEnumerator MessageCoroutine(string message)
    {
        _text.text = message;
        _text.enabled = true;

        yield return new WaitForSeconds(3f);

        Clear();
    }

}
