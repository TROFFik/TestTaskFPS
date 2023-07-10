using UnityEngine;

public class WeaponPhysics : WeaponCore
{
    [SerializeField] private Pool _pool = null;

    [SerializeField] private Transform _shootPoint = null;
    [SerializeField] private Projectile _projectile = null;
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
            GameObject tempProjectile = _pool.GetPoolObject();

            Debug.Log(1);
            if (tempProjectile != null)
            {
                Debug.Log(2);
                _canShoot = false;

                tempProjectile.SetActive(true);
                tempProjectile.transform.position = _shootPoint.position;
                tempProjectile.gameObject.GetComponent<Projectile>().Damage = _damage;

                Vector3 direction = DirectionCalculation();
                tempProjectile.gameObject.GetComponent<Rigidbody>().AddForce(direction * _force);

                Timer();
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
}
