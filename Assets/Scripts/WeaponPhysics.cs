using UnityEngine;

public class WeaponPhysics : WeaponCore
{
    [SerializeField] private Transform _shootPoint = null;
    [SerializeField] private GameObject _projectile = null;

    protected override void Shoot()
    {
        if (_canShoot)
        {
            _canShoot = false;

            Instantiate(_projectile, _shootPoint.position, _shootPoint.rotation);

            Timer();
        }
    }
}
