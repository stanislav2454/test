using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _maxExplosionForce = 9f;
    [SerializeField] private float _explosionRadius = 8f;
    [SerializeField] private float _minForceDistance = 0.1f;
    [SerializeField] private float _minScale = 0.1f;

    public void Explode(List<Rigidbody> rigidbodies, Vector3 center)
    {
        if (rigidbodies == null || rigidbodies.Count == 0) 
            return;

        foreach (var rigidbody in rigidbodies)
        {
            if (rigidbody == null) 
                continue;

            ApplyExplosionForce(rigidbody, center);
        }
    }

    private void ApplyExplosionForce(Rigidbody rigidbody, Vector3 center)
    {
        float scale = Mathf.Max(rigidbody.transform.localScale.x, _minScale);
        float distance = Mathf.Max(Vector3.Distance(center, rigidbody.position), _minForceDistance);
        float force = _maxExplosionForce / (scale * distance);

        if (float.IsNaN(force) || float.IsInfinity(force))
            force = _maxExplosionForce;

        float effectiveRadius = Mathf.Max(_explosionRadius / scale, 0.1f);
        rigidbody.AddExplosionForce(force, center, effectiveRadius);

    }
}