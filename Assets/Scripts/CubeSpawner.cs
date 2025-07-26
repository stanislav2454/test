using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public Cube[] CreateCubes(Vector3 position, float splitChance, Vector3 scale)
    {
        if (_cubePrefab == null)
            return new Cube[0];

        int count = Random.Range(2, 9);
        Cube[] cubes = new Cube[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 offset = Random.insideUnitSphere * 1.5f;
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