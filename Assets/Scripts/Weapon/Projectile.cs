using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Action<Projectile> collisionAction;

    private Rigidbody _rigidbody = null;

    private float _damage = 0;
    private bool _ready = true;

    public bool Ready
    {
        get { return _ready; }
        set { _ready = value; }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        collisionAction?.Invoke(this);
        _rigidbody.Sleep();
    }
}