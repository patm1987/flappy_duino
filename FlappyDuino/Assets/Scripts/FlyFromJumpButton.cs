using UnityEngine;

public class FlyFromJumpButton : MonoBehaviour
{
    [SerializeField] private BirdMovement _birdMovement;

    private void Reset()
    {
        _birdMovement = GetComponent<BirdMovement>();
    }

    private void Update()
    {
        _birdMovement.Flying = Input.GetButton("Jump");
    }
}
