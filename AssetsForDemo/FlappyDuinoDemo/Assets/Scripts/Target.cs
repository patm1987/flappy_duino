using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        var position = transform.localPosition;
        position.x -= _speed * Time.deltaTime;
        transform.localPosition = position;
    }
}
