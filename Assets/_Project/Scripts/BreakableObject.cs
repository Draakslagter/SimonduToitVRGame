using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using Random = UnityEngine.Random;

public class BreakableObject : MonoBehaviour, IBreakable, IAudible
{
    [Header("Object Variables")]
    [SerializeField] protected GameObject brokenObject;
    
    private Transform _wholeObjectTransform;
    protected MeshRenderer wholeObjectMeshRenderer;
    private XRGrabInteractable _wholeObjectGrabInteractable;
    private XRBaseGrabTransformer _wholeObjectGrabTransformer;
    
    [Header("Explosion Variables")]
    [SerializeField] protected float explosionForceMin = 5;
    [SerializeField] protected float explosionForceMax= 100;
    [SerializeField] protected float explosionForceRadius = 10;
    [SerializeField] protected float fragmentScaleFactor = 1;
    [SerializeField] protected float shrinkSpeed = 5;
    
    [Header("Speed & Break Variables")] 
    private Rigidbody _wholeObjectRigidbody;
    [SerializeField] protected float breakSpeed = 5;
    protected bool objectBroken;
    
    [Header("Score Variables")] 
    [SerializeField] protected int cleanPoints = 50;
    [SerializeField] protected int dirtyBreakPoints = 100;
    [SerializeField] protected int cleanBreakPoints = 200;
    public static Action<int, bool, bool> OnScorePoints;

    [Header("Audio")] 
    public UnityEvent<bool> onHitAudio;

    protected void Awake()
    {
        if (_wholeObjectTransform == null)
        {
            _wholeObjectTransform = GetComponent<Transform>();
        }
        if (_wholeObjectRigidbody == null)
        {
            _wholeObjectRigidbody = GetComponent<Rigidbody>();
        }
        if (wholeObjectMeshRenderer == null)
        {
            wholeObjectMeshRenderer = GetComponent<MeshRenderer>();
        }

        if (_wholeObjectGrabInteractable == null)
        {
            _wholeObjectGrabInteractable = GetComponent<XRGrabInteractable>();
        }

        if (_wholeObjectGrabTransformer == null)
        {
            _wholeObjectGrabTransformer = GetComponent<XRBaseGrabTransformer>();
        }
    }
    
    public float CheckSpeed()
    {
       return _wholeObjectRigidbody.linearVelocity.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out IBreakable breakable);
        var otherSpeed = breakable?.CheckSpeed();
        if (CheckSpeed() >= breakSpeed || otherSpeed >= breakSpeed)
        {
            Debug.Log($"{this.name} Speed:{CheckSpeed()}");
            Shatter();
        }
        onHitAudio?.Invoke(objectBroken);
    }
    public void Shatter()
    {
        Debug.Log($"{this.name} is breaking");
        if (objectBroken) return;
        wholeObjectMeshRenderer.enabled = false;
        _wholeObjectRigidbody.isKinematic = true;
        
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

        if (_wholeObjectGrabInteractable != null)
        {
            _wholeObjectGrabInteractable.enabled = false;
            _wholeObjectGrabTransformer.enabled = false;
        }
        
        if (brokenObject == null) return;
        Debug.Log($"{this.name} is broken");
        onHitAudio?.Invoke(objectBroken);
        objectBroken = true;
        brokenObject.SetActive(true);
        
        foreach (Transform fragment in brokenObject.transform)
        {
            var rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(explosionForceMin, explosionForceMax),
                    _wholeObjectTransform.position, explosionForceRadius);
            }
        }
        ScorePoints();
    }
    protected virtual void ScorePoints()
    {
        OnScorePoints.Invoke(dirtyBreakPoints, false, true);
    }
    protected void Shrink(Transform fragment)
    {
        fragment.DOScale(fragmentScaleFactor, shrinkSpeed).SetEase(Ease.Linear);
    }

    public void SetAudioBehaviour(AudioBehaviour audioBehaviour)
    {
        onHitAudio.AddListener(audioBehaviour.PlayAudio);
    }

    public void RemoveAudioBehaviour(AudioBehaviour audioBehaviour)
    {
        onHitAudio.RemoveListener(audioBehaviour.PlayAudio);
    }
}
