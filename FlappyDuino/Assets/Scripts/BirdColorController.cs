using UnityEngine;

public class BirdColorController : MonoBehaviour
{
    [SerializeField] private BirdMovement _birdMovement;
    [SerializeField] private WebsocketController _websocketController;
    [SerializeField] private Color _flyingColor = Color.blue;
    [SerializeField] private Color _fallingColor = Color.green;

    void Start()
    {
        _birdMovement.OnFlying.AddListener(HandleFlying);
    }

    void OnDestroy()
    {
        _birdMovement.OnFlying.RemoveListener(HandleFlying);
    }

    private void HandleFlying(bool flying)
    {
        _websocketController.SetColor(flying ? _flyingColor : _fallingColor);
    }
}