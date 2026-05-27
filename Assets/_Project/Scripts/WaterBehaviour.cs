using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out IWashable washable);
        washable?.Wet();
    }
}
