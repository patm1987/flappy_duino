using UnityEngine;

public class SignalGameOverWhenTargetHit : MonoBehaviour
{
    [SerializeField] private Game _game;

    private void Reset()
    {
        _game = FindObjectOfType<Game>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            _game.GameOver();
        }
    }
}
