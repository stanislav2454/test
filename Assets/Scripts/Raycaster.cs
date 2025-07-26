using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 40f;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private DestructionHandler _destructionHandler;

    private Camera _mainCamera;

    private void Awake() =>
        _mainCamera = Camera.main;

    private void OnEnable() =>
        _inputReader.MouseButtonDown += PerformRaycast;

    private void OnDisable() =>
        _inputReader.MouseButtonDown -= PerformRaycast;

    private void PerformRaycast()
    {
        if (_mainCamera == null || _destructionHandler == null)
            return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
            if (hit.transform.TryGetComponent(out Cube cube))
                _destructionHandler.HandleDestruction(cube);
    }
}