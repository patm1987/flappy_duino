using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public UnityEvent OnGameOver = new UnityEvent();
    public bool IsGameOver { get; private set; }

    public void GameOver()
    {
        if (!IsGameOver)
        {
            IsGameOver = true;
            OnGameOver.Invoke();
        }
    }
}
