using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private PoolObject _poolObject = null;
    [SerializeField] private int _minObjectCount = 0;
    [SerializeField] private int _maxObjectCount = 0;

    private int _currentObjectCount = 0;
    private List<PoolObject> _listObjects = new List<PoolObject>();

    private void Start()
    {
        _currentObjectCount = _minObjectCount;
        CreatePool();
    }
    private void CreatePool()
    {
        _listObjects = new List<PoolObject>(_minObjectCount);

        for (int i = 0; i < _minObjectCount; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        GameObject TempObject;
        TempObject = Instantiate(_poolObject.gameObject, transform);
        _listObjects.Add(TempObject.GetComponent<PoolObject>());
        TempObject.SetActive(false);
        return TempObject;
    }
    public GameObject GetPoolObject()
    {
        foreach (var item in _listObjects)
        {
            if (item.Ready)
            {
                item.Ready = false;
                return item.gameObject;
            }
        }
        if (_currentObjectCount < _maxObjectCount)
        {
            _currentObjectCount++;
            return CreateObject();
        }

        return null;
    }
}
