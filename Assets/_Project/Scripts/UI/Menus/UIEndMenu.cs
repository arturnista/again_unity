using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEndMenu : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _enemiesText = default;
    [SerializeField] private TextMeshProUGUI _timeText = default;

    private void Awake()
    {
        _enemiesText.text = string.Format("Enemies\n{0} / {1}", EnemiesController.EnemiesKilled, EnemiesController.EnemiesTotal);
        _timeText.text = string.Format("Time\n{0}", Format.Time(TimeController.TotalTime));
    }

}
