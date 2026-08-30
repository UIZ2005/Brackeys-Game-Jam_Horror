using UnityEngine;

public class ObjetoPista : MonoBehaviour
{
    [Header("Interacción")]
    [SerializeField] private GameObject interactuarMark;
    [SerializeField] private GameObject panelDetalles;

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

            if (interactuarMark != null)
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

            if (interactuarMark != null)
            {
                interactuarMark.SetActive(false);
            }
        }
    }

    public void Interactuar()
    {
        if (!isPlayerInRange)
            return;

        if (panelAbierto)
            return;

        AbrirDetalles();
    }

    private void AbrirDetalles()
    {
        panelAbierto = true;

        // Ocultar el indicador de interacción
        if (interactuarMark != null)
        {
            interactuarMark.SetActive(false);
        }

        // Detener al jugador
        player.ActivarQuieto();

        // Mostrar Canvas
        if (panelDetalles != null)
        {
            panelDetalles.SetActive(true);
        }

        // Agregar la pista solamente la primera vez
        if (esPista && !pistaDescubierta)
        {
            pistamanager.agregarPista(pistaTexto);

            pistaDescubierta = true;

            if (npcACambiar != null)
            {
                npcACambiar.CambiarDialogo();
            }
        }
    }

    public void CerrarDetalles()
    {
        panelAbierto = false;

        // Cerrar Canvas
        if (panelDetalles != null)
        {
            panelDetalles.SetActive(false);
        }

        // Permitir movimiento
        player.DesactivarQuieto();

        // Si sigue cerca del objeto, mostrar nuevamente el indicador
        if (isPlayerInRange && interactuarMark != null)
        {
            interactuarMark.SetActive(true);
        }
    }
}