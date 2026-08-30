using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Diaologo : MonoBehaviour
{
    [Header("Sistema de dialogos")]
    public string nombreNpc;
    public GameObject dialogoMark;
    public GameObject dialogopanel;

    [SerializeField] private GameObject caradialogo;
    public Sprite Cara;
    public TextMeshProUGUI texto;

    [SerializeField, TextArea(4, 6)]
    private string[] lineasDialogo;

    [SerializeField, TextArea(4, 6)]
    private string[] lineasDialogoDespuesPista;

    private float typingtext = 0.05f;

    private bool isplayerInRange;
    private bool didDialagoStart;
    private int LineIndex;
    private Player player;

    private bool pistaDescubierta = false;

    [Header("Sistema de pistas")]
    public bool ispista = false;
    public string pistaTexto;
    private pistaslist pistamanager;

    private void Start()
    {
        pistamanager = FindAnyObjectByType<pistaslist>();
        player = FindAnyObjectByType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isplayerInRange = true;
            dialogoMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isplayerInRange = false;
            dialogoMark.SetActive(false);
        }
    }

    public void interact()
    {
        if (isplayerInRange)
        {
            if (!didDialagoStart)
            {
                startdialogo();
            }
            else if (texto.text == ObtenerLineaActual())
            {
                nextdialogoLine();
            }
            else
            {
                StopAllCoroutines();
                texto.text = ObtenerLineaActual();
            }
        }
    }

    public void startdialogo()
    {
        player.quieto = true;
        didDialagoStart = true;

        dialogopanel.SetActive(true);
        dialogoMark.SetActive(false);

        if (caradialogo != null && Cara != null)
        {
            caradialogo.GetComponent<Image>().sprite = Cara;
        }

        LineIndex = 0;

        StartCoroutine(ShowLine());
    }

    IEnumerator ShowLine()
    {
        texto.text = string.Empty;

        foreach (char ch in ObtenerLineaActual())
        {
            texto.text += ch;
            yield return new WaitForSecondsRealtime(typingtext);
        }

        yield return null;
    }

    private string ObtenerLineaActual()
    {
        if (pistaDescubierta && lineasDialogoDespuesPista.Length > 0)
        {
            return lineasDialogoDespuesPista[LineIndex];
        }

        return lineasDialogo[LineIndex];
    }

    private string[] ObtenerDialogoActual()
    {
        if (pistaDescubierta && lineasDialogoDespuesPista.Length > 0)
        {
            return lineasDialogoDespuesPista;
        }

        return lineasDialogo;
    }

    public void nextdialogoLine()
    {
        LineIndex++;

        string[] dialogoActual = ObtenerDialogoActual();

        if (LineIndex < dialogoActual.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialagoStart = false;

            dialogopanel.SetActive(false);
            dialogoMark.SetActive(true);

            player.quieto = false;
            texto.text = "";

            if (ispista)
            {
                pistamanager.agregarPista(pistaTexto);
                ispista = false;
            }
        }
    }

    // Esta función se llama cuando el jugador descubre la pista
    public void CambiarDialogo()
    {
        pistaDescubierta = true;
    }
}
