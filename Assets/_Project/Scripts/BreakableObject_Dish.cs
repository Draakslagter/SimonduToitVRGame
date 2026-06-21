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
            ScorePoints();
            Debug.Log("Washed");
        }
        else
        {
            Debug.Log("Not Wet");
        }
    }

    protected override void ScorePoints()
    {
        switch (objectBroken)
        {
            case true when _washed:
                OnScorePoints(cleanBreakPoints, false, true);
                break;
            case true when !_washed:
                OnScorePoints(dirtyBreakPoints, false, true);
                break;
            case false when _washed:
                OnScorePoints(cleanPoints, true, false);
                break;
        }
    }
}
