using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action PickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            PickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
