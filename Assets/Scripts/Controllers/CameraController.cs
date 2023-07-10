using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100.0f;

    [Tooltip("The maximum angle that the camera can be raised")]
    [SerializeField] private float _maxAngleX = 90.0f;

    [Tooltip("The minimum angle that the camera can be lowered")]
    [SerializeField] private float _minAngleX = -90.0f;

    [SerializeField] private Transform _playerBody;

    void Start()
    {
        InputController.Instance.rotateAction += Rotate;
    }

    private void Rotate(Vector3 value)
    {
        value.x = Mathf.Clamp(value.x, _minAngleX, _maxAngleX) * _mouseSensitivity; 

        transform.localRotation = Quaternion.Euler(value.x, 0.0f, 0.0f);
        _playerBody.Rotate(Vector3.up * value.y * _mouseSensitivity);
    }
}
