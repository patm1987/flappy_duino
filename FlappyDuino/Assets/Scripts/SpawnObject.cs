using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;

    public void Run()
    {
        Instantiate(_objectToSpawn, transform);
    }
}
