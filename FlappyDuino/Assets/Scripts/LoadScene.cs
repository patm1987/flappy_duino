using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;

    public void Run()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
