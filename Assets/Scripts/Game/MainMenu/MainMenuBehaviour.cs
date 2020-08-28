using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => GameManager.Instance?.LoadGame());
    }
}
