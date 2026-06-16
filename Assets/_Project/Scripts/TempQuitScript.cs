using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class TempQuitScript : MonoBehaviour
{
    private VRKeyboardInput _inputAction;
    public UnityEvent onShatter;
    
    private void Awake()
    {
        _inputAction = new VRKeyboardInput();
        _inputAction.Enable();
        _inputAction.VRKeyboardMap.QuitGame.performed += QuitGame;
        _inputAction.VRKeyboardMap.ShatterTest.performed += ShatterTest;
    }

    private void OnDisable()
    {
        _inputAction.VRKeyboardMap.QuitGame.performed -= QuitGame;
        _inputAction.VRKeyboardMap.ShatterTest.performed -= ShatterTest;
        _inputAction.Disable();
    }
    private void ShatterTest(InputAction.CallbackContext obj)
    {
        onShatter.Invoke();
    }

    private void QuitGame(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    
}
