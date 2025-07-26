using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float Radius = 1.5f;
    private const int MinInclusiveQuantity = 2;
    private const int MaxExclusiveQuantity = 9;

    [SerializeField] private Cube _cubePrefab;

    public Cube[] CreateCubes(Vector3 position, float splitChance, Vector3 scale)
    {
        if (_cubePrefab == null)
            return new Cube[0];

        int count = Random.Range(MinInclusiveQuantity, MaxExclusiveQuantity);
        Cube[] cubes = new Cube[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = Random.insideUnitSphere * Radius;
            offset.y = Mathf.Abs(offset.y);

            Cube cube = Instantiate(_cubePrefab, position + offset, Quaternion.identity);

            StartCoroutine(InitializeNextFrame(cube, splitChance));

            cubes[i] = cube;
        }

        return cubes;
    }

    private System.Collections.IEnumerator InitializeNextFrame(Cube cube, float splitChance)
    {
        yield return null;
        cube.Initialize(splitChance);
    }
}