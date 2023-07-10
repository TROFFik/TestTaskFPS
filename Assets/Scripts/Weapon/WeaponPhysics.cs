using UnityEngine;

public class WeaponPhysics : WeaponCore
{
    [SerializeField] private Transform _shootPoint = null;
    [SerializeField] private float _force = 10f;

    [Tooltip("Distance at crosshairs line and shooting line intersect")]
    [SerializeField] private float _convergenceDistance = 10f;
    private void Start()
    {
        InputController.Instance.clickLeftButtonAction += Shoot;
    }

    protected override void Shoot()
    {
        if (_canShoot)
        {
            Projectile tempProjectile = Pool.Instance.GetPoolObject();

            if (tempProjectile != null)
            {
                _canShoot = false;

                tempProjectile.gameObject.SetActive(true);
                tempProjectile.transform.position = _shootPoint.position;
                tempProjectile.Damage = _damage;

                Vector3 direction = DirectionCalculation();
                tempProjectile.gameObject.GetComponent<Rigidbody>().AddForce(direction * _force);

                WaitCooldown();
            }
        }
    }

    private Vector3 DirectionCalculation()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, _convergenceDistance);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenCenter);
        Vector3 direction = (worldPoint - _shootPoint.position).normalized;

        return direction;
    }

    private void OnDestroy()
    {
        InputController.Instance.clickLeftButtonAction -= Shoot;
    }
}
