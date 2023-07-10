using System;
using UnityEngine;

public partial class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public Action<Vector3> rotateAction;
    public Action clickLeftButtonAction;
    public Action clickRightButtonAction;

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

#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            InputSwipe();
        }

#endif

#if UNITY_STANDALONE_WIN

        InputMouse();

#endif
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
