using UnityEngine;
using UnityEngine.InputSystem;

public class TempQuitScript : MonoBehaviour
{
    private VRKeyboardInput _inputAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _inputAction = new VRKeyboardInput();
        _inputAction.Enable();
        _inputAction.VRKeyboardMap.QuitGame.performed += QuitGame;
    }

    private void QuitGame(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
