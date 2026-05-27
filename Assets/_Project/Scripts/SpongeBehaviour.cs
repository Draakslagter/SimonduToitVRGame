using System;
using UnityEngine;

public class SpongeBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        other.TryGetComponent(out IWashable washable);
        washable?.Wash();

    }
}
