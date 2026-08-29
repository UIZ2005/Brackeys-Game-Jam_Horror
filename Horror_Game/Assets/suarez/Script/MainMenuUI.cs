using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private Button startButton;
    private Button creditsButton;

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        VisualElement root = uiDocument.rootVisualElement;

        startButton = root.Q<Button>("StartButton");
        creditsButton = root.Q<Button>("CreditsButton");

        startButton.clicked += StartGame;
        creditsButton.clicked += ShowCredits;
    }

    private void StartGame()
    {
        Debug.Log("Nombre escena que inicia el juego");
        /*SceneManager.LoadScene("NombreEscena que hay que cargar para iniciar el juego aqui")*/
    }

    private void ShowCredits()
    {
        Debug.Log("CanvasCreditos");
    }

    private void OnDisable()
    {
        startButton.clicked -= StartGame;
        creditsButton.clicked -= ShowCredits;
    }
}