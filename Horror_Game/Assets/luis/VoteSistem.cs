using UnityEngine;

public class VoteSistem : MonoBehaviour
{
    public string culpableSeleccionado;
    private pistaslist pistas;
    [SerializeField] private string votoPredeterminado = "bruno";
    Diaologo[] npcs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcs = FindObjectsByType<Diaologo>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        pistas = FindAnyObjectByType<pistaslist>();
        pistas.abir();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if (culpableSeleccionado == next)
        {
            votoPueblo = votoPredeterminado;
        }

        if (culpableSeleccionado == culpableReal)
        {

        }


        foreach (Diaologo npc in npcs)
        {
            if (npc.nombreNpc == votoPueblo)
            {
                npc.gameObject.SetActive(false);
            }
        }

    }
    private void ocultarNpc()
    {

    }

    private int ObtenerNivelConfianza()
    {
        int cantidad = pistas.numpistas;

        if (cantidad == 0)
            return 0;

        if (cantidad <= 2)
            return 1;

        if (cantidad <= 4)
            return 2;

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
