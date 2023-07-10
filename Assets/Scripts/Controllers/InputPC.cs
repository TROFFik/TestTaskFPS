#if UNITY_STANDALONE_WIN
using UnityEngine;


public partial class InputController : MonoBehaviour
{
    private void InputMouse()
    {
        _rotationVector.x -= Input.GetAxis("Mouse Y") * Time.deltaTime;
        _rotationVector.y = Input.GetAxis("Mouse X") * Time.deltaTime;

        rotateAction?.Invoke(_rotationVector);

        if (Input.GetMouseButtonDown(0))
        {
            clickLeftButtonAction?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            clickRightButtonAction?.Invoke();
        }
    }
}
#endif