using UnityEngine;

public class DishBehaviour : MonoBehaviour, IWashable
{
    private bool wet;
    private bool washed;
   
    
    public void Wet()
    {
        wet = true;
        Debug.Log("Wet");
    }

    public void Wash()
    {
        if (wet)
        {
            washed = true;
            Debug.Log("Washed");
        }
        else
        {
            Debug.Log("Not Wet");
        }
    }
}
