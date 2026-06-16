using DG.Tweening;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakable
{
    [Header("Object Variables")]
    [SerializeField] protected GameObject wholeObject;
    [SerializeField] protected GameObject brokenObject;
    protected MeshRenderer wholeObjectMeshRenderer;
    
    [Header("Explosion Variables")]
    [SerializeField] protected float explosionForceMin = 5;
    [SerializeField] protected float explosionForceMax= 100;
    [SerializeField] protected float explosionForceRadius = 10;
    [SerializeField] protected float fragmentScaleFactor = 1;
    [SerializeField] protected float shrinkSpeed = 5;
    
    [Header("Speed Variables")] 
    private Rigidbody _wholeObjectRigidbody;
    [SerializeField] protected float breakSpeed = 5;

    protected virtual void Awake()
    {
        if (_wholeObjectRigidbody == null)
        {
            _wholeObjectRigidbody = GetComponent<Rigidbody>();
        }

        if (wholeObjectMeshRenderer == null)
        {
            wholeObjectMeshRenderer = GetComponent<MeshRenderer>();
        }
    }
    
    public float CheckSpeed()
    {
       return _wholeObjectRigidbody.linearVelocity.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger: Checking Break");
        Debug.Log($"Speed is {CheckSpeed()}");
        Debug.Log(other.name);
        
        other.TryGetComponent(out IBreakable breakable);
        var otherSpeed =  breakable?.CheckSpeed();
        if (CheckSpeed() >= breakSpeed || otherSpeed >= breakSpeed)
        {
            Debug.Log("Breaking");
            Shatter();
        }
    }
    public void Shatter()
    {
        Debug.Log("Broken");
        if (wholeObject == null) return;
        wholeObjectMeshRenderer.enabled = false;

        if (brokenObject == null) return;
        brokenObject.SetActive(true);
        foreach (Transform fragment in brokenObject.transform)
        {
            var rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(explosionForceMin, explosionForceMax), wholeObject.transform.position, explosionForceRadius);
            }
        }
    }

    protected void Shrink(Transform fragment)
    {
        fragment.DOScale(fragmentScaleFactor, shrinkSpeed).SetEase(Ease.Linear);
    }
}
