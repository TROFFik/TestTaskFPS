using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }

    [SerializeField] private Projectile _poolObject = null;
    [SerializeField] private int _minObjectCount = 0;
    [SerializeField] private int _maxObjectCount = 0;

    private int _currentObjectCount = 0;
    private List<Projectile> _listObjects = new List<Projectile>();

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        _currentObjectCount = _minObjectCount;
        CreatePool();
    }
    private void CreatePool()
    {
        _listObjects = new List<Projectile>(_minObjectCount);

        for (int i = 0; i < _minObjectCount; i++)
        {
            CreateObject();
        }
    }

    private Projectile CreateObject()
    {
        Projectile TempObject;
        TempObject = Instantiate(_poolObject.gameObject, transform).GetComponent<Projectile>();
        _listObjects.Add(TempObject.GetComponent<Projectile>());
        TempObject.gameObject.SetActive(false);
        TempObject.collisionAction += ReturnPoolObject;
        return TempObject;
    }
    public Projectile GetPoolObject()
    {
        foreach (var item in _listObjects)
        {
            if (item.Ready)
            {
                item.Ready = false;
                return item;
            }
        }
        if (_currentObjectCount < _maxObjectCount)
        {
            _currentObjectCount++;
            return CreateObject();
        }

        return null;
    }

    private void ReturnPoolObject(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.gameObject.transform.position = transform.position;
        projectile.Ready = true;
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

    private void OnDestroy()
    {
        foreach (var projectile in _listObjects)
        {
            projectile.collisionAction -= ReturnPoolObject;
        }
    }
}
