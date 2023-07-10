using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PoolObject))]
public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody = null;
    private PoolObject _poolObject = null;

    private float _damage = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _poolObject = GetComponent<PoolObject>();
    }

    public float Damage
    {
        set
        {
            _damage = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _poolObject.ReturnToPool();
        _rigidbody.Sleep();
    }
}
