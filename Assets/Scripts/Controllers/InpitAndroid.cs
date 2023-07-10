#if UNITY_ANDROID
using UnityEngine;

public partial class InputController : MonoBehaviour
{
    private void InputSwipe()
    {
        Touch touch = Input.GetTouch(0);

        _rotationVector.x -= touch.deltaPosition.y * Time.deltaTime;
        _rotationVector.y = touch.deltaPosition.x * Time.deltaTime;

        rotateAction?.Invoke(_rotationVector);

        if (touch.phase == TouchPhase.Ended)
        {
            if (touch.position.x < Screen.width / 2)
            {
                clickLeftButtonAction?.Invoke();
            }
            else
            {
                clickRightButtonAction?.Invoke();
            }
        }
    }
}
#endif