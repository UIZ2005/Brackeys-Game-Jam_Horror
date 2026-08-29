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
    [SerializeField, TextArea(4, 6)] private string[] lineasDialogo;

    private float typingtext=0.05f;

    private bool isplayerInRange;
    private bool didDialagoStart;
    private int LineIndex;
    private Player player;


    [Header("Sistema de pistas")]
    public bool ispista=false;
    public string pistaTexto;
    private pistaslist pistamanager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pistamanager = FindAnyObjectByType<pistaslist>();
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

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
            else if (texto.text == lineasDialogo[LineIndex])
            {
                nextdialogoLine();
            }
            else
            {
                StopAllCoroutines();
                texto.text = lineasDialogo[LineIndex];
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

        foreach(char ch in lineasDialogo[LineIndex])
        {
            texto.text += ch;
            yield return new WaitForSecondsRealtime(typingtext);

        }

        yield return null;
    }
    
    public void nextdialogoLine()
    {
        LineIndex++;
        if(LineIndex < lineasDialogo.Length)
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
}
