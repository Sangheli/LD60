using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectLoot>(out var x))
        {
            x.DoCollect();
        }
    }
}