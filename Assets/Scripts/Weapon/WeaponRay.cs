using UnityEngine;

public class WeaponRay : WeaponCore 
{
    [SerializeField] private float _maxDistance = 0;

    private void Start()
    {
        InputController.Instance.clickRightButtonAction += Shoot;
    }

    protected override void Shoot()
    {
        if (_canShoot)
        {
            _canShoot = false;

            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, _maxDistance);

            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("We hit " + hit.collider.name);

                DrawPixel(hit);
            }

            Timer();
        }
    }

    private void DrawPixel(RaycastHit hit) //For demonstrate hits, the pixel that we hit will be colored.
    {
        TextureObject drawObject = hit.collider.gameObject.GetComponent<TextureObject>();

        if (drawObject != null)
        {
            int rayX = (int)(hit.textureCoord.x * drawObject.TextureSize);
            int rayY = (int)(hit.textureCoord.y * drawObject.TextureSize);

            drawObject.Texture.SetPixel(rayX, rayY, Color.red);
            drawObject.Texture.Apply();
        }
    }
}
