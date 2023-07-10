using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class MeshDemo : MonoBehaviour
{
    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;

    [SerializeField] private float _deformationRadius = 0.5f;
    [SerializeField] private float _deformationScale = 0.05f;

    private MeshFilter _meshFilter = null;
    private MeshCollider _meshCollider = null;
    private Mesh _mesh = null;

    private Vector3[] _vertices;
    private int[] _triangles;
    private int _textureSize = 0;


    private void Start()
    {
        CreateStartMesh();
        CreateShape();
        UpdateMesh();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckDeformation(collision))
        {
            UpdateMesh();
        }
    }

    private void CreateStartMesh()
    {
        _mesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;

        _meshCollider = GetComponent<MeshCollider>();

        TextureObject tempTexture = GetComponent<TextureObject>();

        if (tempTexture != null)
        {
            _textureSize = tempTexture.TextureSize;
        }
    }

    private bool CheckDeformation(Collision collision)
    {
        bool isDeformate = false;

        _vertices = _mesh.vertices;

        for (int i = 0; i < _mesh.vertexCount; i++)
        {
            for (int j = 0; j < collision.contacts.Length; j++)
            {
                Vector3 Point = transform.InverseTransformPoint(collision.contacts[j].point);
                Vector3 Force = transform.InverseTransformVector(collision.relativeVelocity);
                float Distance = Vector3.Distance(Point, _vertices[i]);
                if (Distance < _deformationRadius)
                {
                    Vector3 Deformate = Force * (_deformationRadius - Distance) * _deformationScale;
                    _vertices[i] += Deformate;
                    isDeformate = true;
                }
            }
        }

        return isDeformate;
    }

    private void CreateShape()
    {
        _vertices = new Vector3[(_xSize + 1) * (_ySize + 1)];
        _triangles = new int[_xSize * _ySize * 6];

        for (int i = 0, x = _xSize; x >= 0; x--)
        {
            for (int y = 0; y <= _ySize; y++)
            {
                _vertices[i] = new Vector3(x / 10, y / 10, 0);
                i++;
            }
        }

        for (int vert = 0, tris = 0, y = 0; y < _ySize; y++)
        {
            for (int z = 0; z < _xSize; z++)
            {
                _triangles[tris] = vert;
                _triangles[tris + 1] = _triangles[tris + 4] = vert + _xSize + 1;
                _triangles[tris + 2] = _triangles[tris + 3] = vert + 1;
                _triangles[tris + 5] = vert + _xSize + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }
    }

    private void UpdateMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _meshCollider.sharedMesh = _mesh;

        UpdateUV();
    }

    private void UpdateUV()
    {
        Vector2[] uvs = new Vector2[_vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(_vertices[i].x, _vertices[i].y) / _textureSize;
        }

        _mesh.uv = uvs;
    }
}