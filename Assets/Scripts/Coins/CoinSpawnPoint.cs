using System;
using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    public event Action<CoinSpawnPoint> Released;

    private Coin _coin;

    public void Set(Coin coin)
    {
        if(_coin != null)
            Clear();

        _coin = coin;
        _coin.PickedUp += OnPickedUpCoin;
    }

    private void OnPickedUpCoin()
    {
        Clear();
        Released?.Invoke(this);
    }

    private void Clear()
    {
        if (_coin == null)
            return;

        _coin.PickedUp -= OnPickedUpCoin;
        _coin = null;
    }
}
