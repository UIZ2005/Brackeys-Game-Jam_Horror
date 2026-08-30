using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;

public class VoteSistem : MonoBehaviour
{
    public string culpableSeleccionado;
    private pistaslist pistas;
    [SerializeField] private string votoPredeterminado = "bruno";
    private Diaologo[] npcs;
    public GameObject votacion;
    public GameObject resultados;
    public TextMeshProUGUI textovoto;
    public GameObject[] caras;
    public TextMeshProUGUI Confidence;
    private Player player;
    public GameObject salida;
    private GameManager game;
    private GameObject caratemp;
    public GameObject win;
    public GameObject titulo;
    public TextMeshProUGUI tituloni;

    public GameObject[] botones;
    private int N=1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = FindAnyObjectByType<GameManager>();
        player = FindAnyObjectByType<Player>();
        npcs = FindObjectsByType<Diaologo>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        pistas = FindAnyObjectByType<pistaslist>();
        StartCoroutine(Titulos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void comenzarVotacion()
    {
        player.ActivarQuieto();
        votacion.SetActive(true);
        pistas.abir();
    }
    public void seleccionado(string seleccionado)
    {
        culpableSeleccionado = seleccionado;
    }
    public void ConfirmarVoto()
    {
        
        string votoPueblo = ObtenerVotoDelPueblo();

        if (culpableSeleccionado == "")
            return;


        string culpableReal = "ines";

        string next = "samuel";

        pistas.abir();
        pistas.lista.text = "";
        pistas.numpistas = 0;

        if (culpableSeleccionado == next)
        {
            votoPueblo = votoPredeterminado;
        }

        if (votoPueblo == culpableReal)
        {
            StartCoroutine(canvasGamewin());
        }
        game.NextNoche();
        votacion.SetActive(false);
        resultados.SetActive(true);

        Confidence.text = "Confidence level: " + Confidence.text;
        textovoto.text = "The people chose\n" + votoPueblo;

        Debug.Log("el pueblo ha votado por " + votoPueblo);

        foreach (GameObject cara in caras)
        {
            if (cara.name.Equals(votoPueblo))
            {
                caratemp = cara;
                cara.SetActive(true);
            }
        }

        foreach (Diaologo npc in npcs)
        {
            if (npc.nombreNpc == votoPueblo)
            {
                npc.gameObject.SetActive(false);
            }
        }
        foreach (GameObject obj in botones)
        {
            if (obj.name == votoPueblo)
            {
                obj.gameObject.SetActive(false);
            }
            if (obj.name == "samuel")
            {
                obj.gameObject.SetActive(false);
            }
        }
        culpableSeleccionado = "";
         player.DesactivarQuieto();
       
       votoPredeterminado = "tomas";

        N++;
        

    }
    public void starttitulo()
    {
        tituloni.text = "Night " + N.ToString();

        if (N == 3)
        {
            tituloni.text = "Night " + N.ToString() + "\n They found Samuel dead";
        }
        StartCoroutine(Titulos());
    }
    IEnumerator Titulos()
    {
        titulo.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        titulo.SetActive(false);
    }
    IEnumerator canvasGamewin()
    {
        win.SetActive(true);


        yield return new WaitForSecondsRealtime(3f);
        //devolver a escnea principal

        yield return null;
    }
    public void desactivarcara()
    {
        caratemp.SetActive(false);
    }
    private int ObtenerNivelConfianza()
    {
        int cantidad = pistas.numpistas;

        if (cantidad == 0)
        {
            Confidence.text = " Low";
            return 0;
        }

        if (cantidad <= 2)
        {
            Confidence.text = " Medium";
            return 1;
        }

        if (cantidad <= 4)
        {
            Confidence.text = " High";
            return 2;
        }

        Confidence.text = " High";
        return 3;
    }
    private string ObtenerVotoDelPueblo()
    {
        int confianza = ObtenerNivelConfianza();

        switch (confianza)
        {
            case 0:
                return votoPredeterminado;

            case 1:
                // El pueblo todavía confía poco en el jugador
                int prob = Random.Range(0, 100);
                if (prob > 50)
                {
                    return culpableSeleccionado;
                }
                else
                {
                    return votoPredeterminado;
                }
               

            case 2:

                int prob2 = Random.Range(0, 100);
                if (prob2<85)
                {
                    return culpableSeleccionado;
                }
                else
                {
                    return votoPredeterminado;
                }

            case 3:
                // El pueblo confía bastante
                return culpableSeleccionado;
        }

        return votoPredeterminado;
    }

}
