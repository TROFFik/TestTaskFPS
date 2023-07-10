using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private bool _ready = true;

    public bool Ready
    {
        get { return _ready; }
        set { _ready = value; }
    }
    public void ReturnToPool()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
        _ready = true;
    }
}
