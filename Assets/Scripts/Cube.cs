using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private Vector3 _initialScale;
    private ColorChanger _colorChanger;

    public float SplitChance { get; private set; } = 1f;
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        CacheComponents();
        Initialize(SplitChance);
    }

    private void CacheComponents()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
        _initialScale = transform.localScale;
    }

    public void Initialize(float splitChance)
    {
        SplitChance = Mathf.Clamp01(splitChance);
        float newScale = Mathf.Max(_initialScale.x * splitChance, 0.1f);
        transform.localScale = _initialScale * splitChance;

        if (_colorChanger != null)        
            _colorChanger.SetRandomColor();        
    }
}