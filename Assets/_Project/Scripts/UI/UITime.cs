using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITime : MonoBehaviour
{
    
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (TimeController.TimeRemaining > 0)
        {
            if (!_textMesh.enabled) _textMesh.enabled = true;
            _textMesh.text = string.Format("Time left\n{0}", Format.Time(TimeController.TimeRemaining));
        }
        else
        {
            if (_textMesh.enabled) _textMesh.enabled = false;
        }
    }

}
