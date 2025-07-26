using UnityEngine;
using System.Collections.Generic;

public class DestructionHandler : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 11f;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _minExplosionRadius = 0.5f;

    private const float ChanceReductionFactor = 0.5f;

    public void HandleDestruction(Cube hitCube)
    {
        if (hitCube == null) 
            return;

        if (ShouldSplit(hitCube))
            SplitCube(hitCube);
        else
            ExplodeNearbyObjects(hitCube.transform.position);

        Destroy(hitCube.gameObject);
    }

    private bool ShouldSplit(Cube cube) =>
         Random.value <= cube.SplitChance && _cubeSpawner != null;

    private void SplitCube(Cube hitCube) =>
        _cubeSpawner.CreateCubes(
            hitCube.transform.position,
            hitCube.SplitChance * ChanceReductionFactor,
            hitCube.transform.localScale);

    private void ExplodeNearbyObjects(Vector3 position)
    {
        if (_exploder == null) 
            return;

        Collider[] colliders = Physics.OverlapSphere(position, Mathf.Max(_explosionRadius, _minExplosionRadius));

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (var collider in colliders)        
            if (collider != null && collider.attachedRigidbody != null)
                rigidbodies.Add(collider.attachedRigidbody);        

        if (rigidbodies.Count > 0)
            _exploder.Explode(rigidbodies, position);
    }
}