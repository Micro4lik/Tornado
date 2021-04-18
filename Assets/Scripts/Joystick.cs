using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class Joystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [InputControl(layout = "Vector2")] [SerializeField]
    private new string controlPath;

    protected override string controlPathInternal
    {
        get => controlPath;
        set => controlPath = value;
    }

    [Space] [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;

    [Space] [SerializeField] private float handleRange = 1;
    [SerializeField] private float deadZone;

    private RectTransform _baseRect;
    private Vector2 _input = Vector2.zero;
    private Canvas _canvas;

    private void Awake()
    {
        _baseRect = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        background.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, eventData.position, null,
            out var localPoint);

        localPoint -= background.anchorMax * _baseRect.sizeDelta;
        background.anchoredPosition = localPoint;
        background.gameObject.SetActive(true);

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        var radius = background.sizeDelta / 2;
        _input = (eventData.position - position) / (radius * _canvas.scaleFactor);
        HandleInput(_input.magnitude, _input.normalized);
        handle.anchoredPosition = _input * radius * handleRange;

        SendValueToControl(_input);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);

        _input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        SendValueToControl(Vector2.zero);
    }

    private void HandleInput(float magnitude, Vector2 normalised)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                _input = normalised;
        }
        else
            _input = Vector2.zero;
    }
}