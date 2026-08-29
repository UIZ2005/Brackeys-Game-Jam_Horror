using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoUnico : MonoBehaviour
{
    [Header("Sistema de dialogos")]
    public GameObject dialogopanel;

    [SerializeField] private GameObject caradialogo;
    public Sprite Cara;
    public TextMeshProUGUI texto;

    [SerializeField, TextArea(4, 6)]
    private string[] lineasDialogo;

    [SerializeField]
    private float typingtext = 0.05f;

    [Header("Dialogo normal")]
    [SerializeField] private Diaologo dialogoNormal;

    [Header("Panel después del primer dialogo")]
    [SerializeField] private GameObject panelDespuesDialogo;
    [SerializeField] private FadePanel fadePanel;

    [Header("Sonido")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonido;

    [SerializeField]
    private float duracionSonido = 1f;

    [SerializeField]
    private float duracionFadeSonido = 0.3f;

    [Header("Dialogo después del sonido")]
    [SerializeField, TextArea(4, 6)]
    private string[] lineasDialogoDespuesSonido;

    [SerializeField]
    private float tiempoAntesDialogo = 1f;

    private int LineIndex;
    private Player player;

    private bool dialogoIniciado = false;
    private bool primerDialogoTerminado = false;
    private bool segundoDialogoIniciado = false;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (dialogoIniciado || primerDialogoTerminado)
                return;

            StartDialogo();
        }
    }

    private void StartDialogo()
    {
        if (primerDialogoTerminado)
            return;

        dialogoIniciado = true;

        // Mantener al jugador completamente quieto
        player.ActivarQuieto();

        // Mostrar panel de diálogo
        dialogopanel.SetActive(true);

        // Mostrar cara del NPC
        if (caradialogo != null && Cara != null)
        {
            caradialogo.GetComponent<Image>().sprite = Cara;
        }

        LineIndex = 0;

        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        texto.text = string.Empty;

        foreach (char ch in ObtenerLineaActual())
        {
            texto.text += ch;
            yield return new WaitForSecondsRealtime(typingtext);
        }

        // Esperar antes de pasar automáticamente
        yield return new WaitForSecondsRealtime(1.5f);

        NextDialogoLine();
    }

    private string ObtenerLineaActual()
    {
        if (!segundoDialogoIniciado)
        {
            return lineasDialogo[LineIndex];
        }
        else
        {
            return lineasDialogoDespuesSonido[LineIndex];
        }
    }

    private void NextDialogoLine()
    {
        LineIndex++;

        string[] dialogoActual;

        if (!segundoDialogoIniciado)
        {
            dialogoActual = lineasDialogo;
        }
        else
        {
            dialogoActual = lineasDialogoDespuesSonido;
        }

        if (LineIndex < dialogoActual.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            if (!segundoDialogoIniciado)
            {
                TerminarPrimerDialogo();
            }
            else
            {
                TerminarSegundoDialogo();
            }
        }
    }

    private void TerminarPrimerDialogo()
    {
        primerDialogoTerminado = true;

        StopAllCoroutines();

        // Ocultar panel de diálogo
        dialogopanel.SetActive(false);

        // IMPORTANTE:
        // NO liberamos al jugador aquí.
        // El jugador seguirá quieto durante el Canvas,
        // el sonido y el segundo diálogo.

        // Activar diálogo normal
        if (dialogoNormal != null)
        {
            dialogoNormal.enabled = true;
        }

        // Activar el panel
        if (panelDespuesDialogo != null)
        {
            panelDespuesDialogo.SetActive(true);
        }

        // Reproducir FadeIn
        if (fadePanel != null)
        {
            fadePanel.FadeIn();
        }
    }

    // Esta función la llama el botón de cerrar el panel
    public void CerrarPanel()
    {
        if (segundoDialogoIniciado)
            return;

        // Desactivar panel
        if (panelDespuesDialogo != null)
        {
            panelDespuesDialogo.SetActive(false);
        }

        // Reproducir sonido durante 1 segundo con fade
        StartCoroutine(ReproducirSonido());

        // Esperar 1 segundo y comenzar segundo diálogo
        StartCoroutine(IniciarDialogoDespuesDelSonido());
    }

    private IEnumerator ReproducirSonido()
    {
        if (audioSource == null || sonido == null)
            yield break;

        // Guardamos el volumen original
        float volumenOriginal = audioSource.volume;

        // Colocamos el sonido
        audioSource.clip = sonido;
        audioSource.volume = volumenOriginal;

        // Reproducimos
        audioSource.Play();

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionSonido)
        {
            tiempoTranscurrido += Time.unscaledDeltaTime;

            // Cuando llegamos al momento del fade
            float inicioFade = duracionSonido - duracionFadeSonido;

            if (tiempoTranscurrido >= inicioFade)
            {
                float progresoFade =
                    (tiempoTranscurrido - inicioFade) / duracionFadeSonido;

                audioSource.volume = Mathf.Lerp(
                    volumenOriginal,
                    0f,
                    progresoFade
                );
            }

            yield return null;
        }

        // Detener completamente el sonido
        audioSource.Stop();

        // Restaurar volumen original
        audioSource.volume = volumenOriginal;
    }

    private IEnumerator IniciarDialogoDespuesDelSonido()
    {
        yield return new WaitForSecondsRealtime(tiempoAntesDialogo);

        segundoDialogoIniciado = true;

        // El jugador sigue quieto
        player.ActivarQuieto();

        // Mostrar panel de diálogo
        dialogopanel.SetActive(true);

        // Mostrar cara del NPC
        if (caradialogo != null && Cara != null)
        {
            caradialogo.GetComponent<Image>().sprite = Cara;
        }

        // Empezar desde la primera línea
        LineIndex = 0;

        StartCoroutine(ShowLine());
    }

    private void TerminarSegundoDialogo()
    {
        StopAllCoroutines();

        // Ocultar panel
        dialogopanel.SetActive(false);

        // AHORA sí permitir movimiento
        player.DesactivarQuieto();

        // Ya no necesitamos este componente
        enabled = false;
    }
}