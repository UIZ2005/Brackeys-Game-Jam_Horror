using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private Button startButton;
    private Button creditsButton;
    private Button closeButton;

    private VisualElement canvasCreditos;

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        VisualElement root = uiDocument.rootVisualElement;

        // Buscar los botones
        startButton = root.Q<Button>("StartButton");
        creditsButton = root.Q<Button>("CreditsButton");
        closeButton = root.Q<Button>("CloseButton");

        // Buscar el panel de creditos
        canvasCreditos = root.Q<VisualElement>("CanvasCreditos");

        // Conectar los botones con sus funciones
        startButton.clicked += StartGame;
        creditsButton.clicked += OpenCredits;
        closeButton.clicked += CloseCredits;

        // Ocultar los creditos al iniciar
        canvasCreditos.style.display = DisplayStyle.None;
    }

    private void StartGame()
    {
        Debug.Log("Iniciar juego");
        /*SceneManager.LoadScene("NombreEscena que hay que cargar para iniciar el juego aqui")*/
    }

    private void OpenCredits()
    {
        canvasCreditos.style.display = DisplayStyle.Flex;
    }

    private void CloseCredits()
    {
        canvasCreditos.style.display = DisplayStyle.None;
    }

    private void OnDisable()
    {
        startButton.clicked -= StartGame;
        creditsButton.clicked -= OpenCredits;
        closeButton.clicked -= CloseCredits;
    }
}