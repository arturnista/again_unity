using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class UIFeedback : MonoBehaviour
{
    
    [SerializeField] private float _time = .3f;
    [SerializeField] private Color _color = default;
    [SerializeField] private EventManager.EventType _eventType;

    private CanvasGroup _canvas;
    private Image _feedbackImage;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        _canvas.alpha = 0f;
        _feedbackImage = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        EventManager.Instance.AddEventListener(_eventType, HandlePlayerTakeDamage);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveEventListener(_eventType, HandlePlayerTakeDamage);
    }

    private void HandlePlayerTakeDamage(EventData data)
    {
        StartCoroutine(FeedbackCoroutine());
    }

    protected IEnumerator FeedbackCoroutine()
    {
        _feedbackImage.color = _color;

        float ratio = 1f / _time;
        float alpha = 1f;
        _canvas.alpha = alpha;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * ratio;
            _canvas.alpha = alpha;
            yield return null;
        }
    }

}
