using UnityEngine;

public class SetAnimatorBool : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _boolName;
    private int _boolHash;

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _boolHash = Animator.StringToHash(_boolName);
    }

    public void SetBool(bool value)
    {
        _animator.SetBool(_boolHash, value);
    }
}
