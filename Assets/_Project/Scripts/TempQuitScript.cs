using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TempQuitScript : MonoBehaviour
{
  [SerializeField] private Button quitButton;

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
