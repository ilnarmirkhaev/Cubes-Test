using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private Transform _transform;

    private Vector3 _movementVector;
    private float _targetDistance;

    private Vector3 _initialPosition;
    private float _traveledDistance;

    private CubeSpawner _cubeSpawner;

    private void Awake() =>
        AssignRandomColor();

    public void Initialize(float speed, float distance, CubeSpawner cubeSpawner)
    {
        _transform = transform;

        _movementVector = Vector3.forward * speed;
        _targetDistance = distance;

        _initialPosition = _transform.position;
        _traveledDistance = 0f;

        _cubeSpawner = cubeSpawner;
        _cubeSpawner.CubeParametersChanged += Destroy;
    }

    private void Update()
    {
        Move();

        if (_traveledDistance >= _targetDistance)
            Destroy(gameObject);
    }

    private void OnDestroy() =>
        _cubeSpawner.CubeParametersChanged -= Destroy;

    private void Move()
    {
        _transform.Translate(_movementVector * Time.deltaTime);
        _traveledDistance = Vector3.Distance(_transform.position, _initialPosition);
    }

    private void AssignRandomColor() =>
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.6f, 0.6f, 1f, 1f);

    private void Destroy() =>
        Destroy(gameObject);
}