using UnityEngine;

public class TextureObject : MonoBehaviour
{
    [SerializeField] private Color _textureColor = Color.white;
    [SerializeField] private int _textureSize = 32;

    private Material _material;
    private Texture2D _texture;

    public Texture2D Texture
    {
        get
        {
            return _texture;
        }
    }
    public int TextureSize
    {
        get
        {
            return _textureSize;
        }
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;

        _texture = new Texture2D(_textureSize, _textureSize); _texture.Reinitialize(_textureSize, _textureSize);
        _texture.filterMode = FilterMode.Point;
        _material.mainTexture = _texture;

        for (int x = 0; x < _textureSize; x++)
        {
            for (int y = 0; y < _textureSize; y++)
            {
                {
                    _texture.SetPixel(x, y, _textureColor);
                }
            }
        }
        _texture.Apply();
    }
}
