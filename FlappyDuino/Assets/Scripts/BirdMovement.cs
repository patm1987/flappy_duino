using UnityEngine;
using UnityEngine.Events;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _flyForce;
    [SerializeField] private bool _flying;
    public FlyingEvent OnFlying = new FlyingEvent();

    public bool Flying
    {
        get { return _flying; }
        set
        {
            if (_flying != value)
            {
                _flying = value;
                OnFlying.Invoke(_flying);
            }
        }
    }

    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        OnFlying.Invoke(_flying);
    }

    private void FixedUpdate()
    {
        if (Flying)
        {
            _rigidbody.AddForce(new Vector2(0, _flyForce * _rigidbody.mass));
        }
    }

    [System.Serializable]
    public class FlyingEvent : UnityEvent<bool> { }
}
