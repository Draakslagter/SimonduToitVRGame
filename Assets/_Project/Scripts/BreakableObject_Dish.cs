using System;
using UnityEngine;

public class BreakableObject_Dish : BreakableObject, IWashable
{
    [Header("Material Manipulation Variables")]
    [SerializeField] private Material dirtyMaterial;
    [SerializeField] private Material cleanMaterial;
    
    [Header("Action Check Variables")]
    private bool _wet;
    private bool _washed;

    protected override void Awake()
    {
        base.Awake();
        if (wholeObjectMeshRenderer == null)
        {
            wholeObjectMeshRenderer = wholeObject.GetComponent<MeshRenderer>();
        }
    }

    public void Wet()
    {
        _wet = true;
        Debug.Log("Wet");
    }

    public void Wash()
    {
        if (_wet && !_washed)
        {
            _washed = true;
            wholeObjectMeshRenderer.material = cleanMaterial;
            Debug.Log("Washed");
        }
        else
        {
            Debug.Log("Not Wet");
        }
    }
}
