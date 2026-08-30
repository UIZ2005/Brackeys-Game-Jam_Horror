using UnityEngine;

public class ActivarUI : MonoBehaviour
{
    [Header("Objeto que se activará")]
    [SerializeField] private GameObject objetoAActivar;


    [Header("Script FadePanel")]
    [SerializeField] private FadePanel fadePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Activar el GameObject
            if (objetoAActivar != null)
            {
                objetoAActivar.SetActive(true);
            }

            // Ejecutar FadeIn
            if (fadePanel != null)
            {
                fadePanel.FadeIn();
                gameObject.SetActive(false);
            }
        }
    }
}