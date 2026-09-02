using UnityEngine;

public class ObjetoPista : MonoBehaviour
{
    [Header("Interacción")]
    [SerializeField] private GameObject interactuarMark;
    [SerializeField] private GameObject panelDetalles;
    [SerializeField] private FadePanel fadePanel;

    [Header("Pista")]
    [SerializeField] private bool esPista = true;
    [SerializeField] private string pistaTexto;

    [Header("NPC afectado por la pista")]
    [SerializeField] private Diaologo npcACambiar;

    private pistaslist pistamanager;
    private Player player;

    private bool isPlayerInRange = false;
    private bool pistaDescubierta = false;
    private bool panelAbierto = false;

    private void Start()
    {
        pistamanager = FindAnyObjectByType<pistaslist>();
        player = FindAnyObjectByType<Player>();

        // El panel comienza apagado
        if (panelDetalles != null)
        {
            panelDetalles.SetActive(false);
        }

        // El indicador comienza apagado
        if (interactuarMark != null)
        {
            interactuarMark.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;

            // Mostrar indicador de interacción
            if (interactuarMark != null && !panelAbierto)
            {
                interactuarMark.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;

            // Ocultar indicador
            if (interactuarMark != null)
            {
                interactuarMark.SetActive(false);
            }
        }
    }

    public void Interactuar()
    {
        // Si el jugador no está cerca, no hacer nada
        if (!isPlayerInRange)
            return;

        // Si el panel ya está abierto, no volver a abrirlo
        if (panelAbierto)
            return;

        AbrirDetalles();
    }

    private void AbrirDetalles()
    {
        panelAbierto = true;

        // Ocultar indicador de interacción
        if (interactuarMark != null)
        {
            interactuarMark.SetActive(false);
        }

        // Detener al jugador
        if (player != null)
        {
            player.ActivarQuieto();
        }

        // Mostrar el panel
        if (panelDetalles != null)
        {
            panelDetalles.SetActive(true);
        }

        // Registrar la pista SOLO LA PRIMERA VEZ
        if (esPista && !pistaDescubierta)
        {
            if (pistamanager != null)
            {
                pistamanager.agregarPista(pistaTexto);
            }

            pistaDescubierta = true;

            // Cambiar diálogo del NPC solamente la primera vez
            if (npcACambiar != null)
            {
                npcACambiar.CambiarDialogo();
            }
        }

        // Hacer FadeIn al abrir
        if (fadePanel != null)
        {
            fadePanel.FadeIn();
        }
    }

    public void CerrarDetalles()
    {
        // Marcar que el panel ya está cerrado
        panelAbierto = false;

        // Hacer FadeOut
        if (fadePanel != null)
        {
            fadePanel.FadeOut();
        }
        else
        {
            // Si no hay FadePanel, simplemente apagar el panel
            if (panelDetalles != null)
            {
                panelDetalles.SetActive(false);
            }
        }

        // Permitir nuevamente el movimiento del jugador
        if (player != null)
        {
            player.DesactivarQuieto();
        }

        // Si el jugador sigue cerca del objeto,
        // volver a mostrar el indicador de interacción
        if (isPlayerInRange && interactuarMark != null)
        {
            interactuarMark.SetActive(true);
        }
    }
}