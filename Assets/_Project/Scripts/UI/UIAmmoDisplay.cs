using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAmmoDisplay : MonoBehaviour
{
    
    private TextMeshProUGUI _textMesh;
    private PlayerAttack _player;

    private int _lastAmmo;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (_player.IsReloading) _textMesh.text = "<size=50>Reloading...</size>";
        if (_player.CurrentAmmo != _lastAmmo)
        {
            _textMesh.text = string.Format("{0}/<size=75>{1}</size>", _player.CurrentAmmo, _player.AmmoAmount);
        }
        _lastAmmo = _player.CurrentAmmo;
    }

}
