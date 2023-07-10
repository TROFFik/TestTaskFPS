using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public Action<Vector3> rotateAction;
    public Action clickLeftButtonAction;
    public Action clickRightButtonAction;
    public Action clickEAction;

    private Vector2 _rotationVector = new Vector2();

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        InputRotation();

        if (Input.GetMouseButtonDown(0))
        {
            clickLeftButtonAction?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            clickRightButtonAction?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            clickEAction?.Invoke();
        }
    }

    private void InputRotation()
    {
        _rotationVector.x -= Input.GetAxis("Mouse Y") * Time.deltaTime;
        _rotationVector.y = Input.GetAxis("Mouse X") * Time.deltaTime;

        rotateAction?.Invoke(_rotationVector);
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
