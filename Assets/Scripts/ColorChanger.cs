using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        CacheComponents();
        SetRandomColor();
    }

    private void CacheComponents()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
            Debug.LogError("Renderer component is missing!", this);
    }

    public void SetRandomColor()
    {
        if (_renderer != null && _renderer.material != null)
            _renderer.material.color = Random.ColorHSV();
    }
}