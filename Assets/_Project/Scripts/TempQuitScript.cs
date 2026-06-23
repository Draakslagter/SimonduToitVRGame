using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TempQuitScript : MonoBehaviour
{
  [SerializeField] private Button quitButton;

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
  private void Start()
  {
    quitButton.onClick.AddListener(QuitGame);
  }

  private void OnDisable()
  {
      quitButton.onClick.RemoveAllListeners();
  }

  private void QuitGame()
  {
      Debug.Log("Quitting game...");
      Application.Quit();
  }

    
}
