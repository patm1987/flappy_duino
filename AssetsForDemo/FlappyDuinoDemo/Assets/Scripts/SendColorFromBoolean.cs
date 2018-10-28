using UnityEngine;
using UnityEngine.Events;

public class SendColorFromBoolean : MonoBehaviour
{
    [SerializeField] private Color _trueColor = Color.green;
    [SerializeField] private Color _falseColor = Color.red;
    public ColorChangedEvent OnColorChanged = new ColorChangedEvent();

    private bool _value;
    public bool Value
    {
        get { return _value; }
        set
        {
            if (_value != value)
            {
                _value = value;
                Color = _value ? _trueColor : _falseColor;
            }
        }
    }

    private Color _color;
    public Color Color
    {
        get { return _color; }
        private set
        {
            if (_color != value)
            {
                _color = value;
                OnColorChanged.Invoke(_color);
            }
        }
    }

    [System.Serializable]
    public class ColorChangedEvent : UnityEvent<Color>
    {
    }
}