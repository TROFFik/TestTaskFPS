using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private float _damage = 0;

    public float Damage
    {
        set
        {
            _damage = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Timer();
    }

    protected async void Timer()
    {
        await Task.Delay(10);
        Destroy(gameObject);
    }
}
