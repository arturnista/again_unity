using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHealthDisplay : MonoBehaviour
{
    
    private TextMeshProUGUI _textMesh;
    private PlayerHealth _player;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (_player.Health <= 0f)
        {
            _textMesh.gameObject.SetActive(false);
        }
        else
        {
            _textMesh.text = string.Format("+ {0}", _player.Health);
        }
    }

}
