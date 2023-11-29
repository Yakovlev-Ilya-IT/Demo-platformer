using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private CoinSpawnPoint[] _spawnPoints;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField, Range(0, 10)] private float _delayBeforeSpawnNewCoin;

    private void Awake()
    {
        if (_spawnPoints.Length == 0)
            throw new InvalidOperationException(nameof(_spawnPoints));

        foreach (CoinSpawnPoint point in _spawnPoints)
            SpawnCoin(point);
    }

    private void OnEnable()
    {
        foreach (CoinSpawnPoint point in _spawnPoints)
            point.Released += OnReleasedCoinSpawnPoint;
    }

    private void OnDisable()
    {
        foreach (CoinSpawnPoint point in _spawnPoints)
            point.Released -= OnReleasedCoinSpawnPoint;
    }

    private void OnReleasedCoinSpawnPoint(CoinSpawnPoint point) => StartCoroutine(SpawnCoinWithDelay(point));

    private IEnumerator SpawnCoinWithDelay(CoinSpawnPoint point)
    {
        yield return new WaitForSeconds(_delayBeforeSpawnNewCoin);
        SpawnCoin(point);
    }

    private void SpawnCoin(CoinSpawnPoint point)
    {
        Coin coin = Instantiate(_coinPrefab, point.transform.position, Quaternion.identity);
        point.Set(coin);
    }
}
