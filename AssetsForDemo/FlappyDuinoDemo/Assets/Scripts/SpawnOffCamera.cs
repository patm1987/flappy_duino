using System.Collections;
using UnityEngine;

public class SpawnOffCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    private void Reset()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
            var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(_camera);
            var rightPlane = frustumPlanes[1];
            var spawnHeight = Random.Range(-_camera.orthographicSize, _camera.orthographicSize);
            var cameraPosition = _camera.transform.position;
            var spawnPosition = rightPlane.ClosestPointOnPlane(
                new Vector3(cameraPosition.x, cameraPosition.y + spawnHeight, cameraPosition.z));
            var spawnedObject = Instantiate(
                _objectToSpawn, 
                spawnPosition, 
                _objectToSpawn.transform.rotation, 
                transform);
        }
    }
}
