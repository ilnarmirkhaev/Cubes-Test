using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const int DefaultTimeBetweenSpawn = 1;
    private const int DefaultCubeSpeed = 3;
    private const int DefaultCubeDistance = 10;

    public Cube cubePrefab;
    public Transform spawnPoint;

    public int TimeBetweenSpawn
    {
        get => _timeBetweenSpawn;
        set
        {
            _timeBetweenSpawn = value;
            OnCubeParametersChanged();
        }
    }

    public int CubeSpeed
    {
        get => _cubeSpeed;
        set
        {
            _cubeSpeed = value;
            OnCubeParametersChanged();
        }
    }

    public int CubeDistance
    {
        get => _cubeDistance;
        set
        {
            _cubeDistance = value;
            OnCubeParametersChanged();
        }
    }

    public event Action CubeParametersChanged;

    private Coroutine _spawnCoroutine;
    private int _timeBetweenSpawn;
    private int _cubeSpeed;
    private int _cubeDistance;

    private void Awake()
    {
        TimeBetweenSpawn = DefaultTimeBetweenSpawn;
        CubeSpeed = DefaultCubeSpeed;
        CubeDistance = DefaultCubeDistance;
    }

    private void Update() =>
        _spawnCoroutine ??= StartCoroutine(WaitForSpawnCoroutine());

    private IEnumerator WaitForSpawnCoroutine()
    {
        Spawn();
        yield return new WaitForSeconds(TimeBetweenSpawn);
        _spawnCoroutine = null;
    }

    private void Spawn()
    {
        var cube = Instantiate(cubePrefab, spawnPoint, false);
        cube.Initialize(CubeSpeed, CubeDistance, this);
    }

    private void OnCubeParametersChanged()
    {
        StopSpawnCoroutine();

        CubeParametersChanged?.Invoke();
    }

    private void StopSpawnCoroutine()
    {
        if (_spawnCoroutine is null) return;
        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }
}