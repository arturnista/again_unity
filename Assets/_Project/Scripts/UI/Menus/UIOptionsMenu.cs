using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionsMenu : MonoBehaviour
{
    
    [SerializeField] private UISlider _sensibilitySlider = default;

    private void OnEnable()
    {
        _sensibilitySlider.Value = ConfigurationManager.Instance.Sensibility;
        _sensibilitySlider.OnChangeValue += value => ConfigurationManager.Instance.Sensibility = value;
    }

}
