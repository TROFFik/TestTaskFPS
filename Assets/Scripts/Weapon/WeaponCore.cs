using System.Threading.Tasks;
using UnityEngine;

public class WeaponCore : MonoBehaviour
{
    [SerializeField] protected float _damage = 0;

    [Tooltip("Reload time in milliseconds")]
    [SerializeField] protected int _cooldown = 0;

    protected bool _canShoot = true;

    protected virtual void Shoot()
    {

    }

    protected async void WaitCooldown()
    {
        await Task.Delay(_cooldown);
        _canShoot = true;
    }
}
