using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISlider : MonoBehaviour
{

    public delegate void ChangeValueHandler(float value);
    public event ChangeValueHandler OnChangeValue;
    
    [SerializeField] private Slider _slider = default;
    [SerializeField] private TextMeshProUGUI _valueText = default;

    public float Value { get => _slider.value; set => _slider.value = value; }

    private void Start()
    {
        HandleValueChange(Value);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleValueChange);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleValueChange);
    }

    private void HandleValueChange(float value)
    {
        _valueText.text = value.ToString("0.00");
        OnChangeValue(value);
    }

}
