using UnityEngine;

public class WeaponRay : WeaponCore
{
    [SerializeField] private float _maxDistance = 0;

    protected override void Shoot()
    {
        if (_canShoot)
        {
            _canShoot = false;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("We hit " + hit.collider.name);
            }

            Timer();
        }
    }
}
