using UnityEngine;
using System.Collections;

public class EventoRayo : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player player;

    [Header("Luces Globales")]
    [SerializeField] private GameObject luzGlobal1;
    [SerializeField] private GameObject luzGlobal2;

    [Header("Spot Lights")]
    [SerializeField] private GameObject[] lucesSpot;

    [Header("Panel")]
    [SerializeField] private GameObject panel;
    [SerializeField] private FadePanel fadePanel;

    [Header("Rayo")]
    [SerializeField] private float rayoEncendido = 0.08f;
    [SerializeField] private float rayoApagado = 0.12f;
    [SerializeField] private float pausaEntreRayos = 0.15f;

    [Header("Sonido del Rayo")]
    [SerializeField] private AudioSource sonidoRayo;

    [Header("Parpadeo de Spot Lights")]
    [SerializeField] private float tiempoParpadeo = 0.08f;
    [SerializeField] private int cantidadParpadeos = 5;

    private bool eventoActivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (eventoActivado)
            return;

        if (!other.CompareTag("Player"))
            return;

        eventoActivado = true;

        StartCoroutine(SecuenciaRayo());
    }

    private IEnumerator SecuenciaRayo()
    {
        // 1. El jugador se queda quieto
        player.ActivarQuieto();

        // 2. Apagamos la luz global principal
        luzGlobal1.SetActive(false);

        // Nos aseguramos de que la luz del rayo empiece apagada
        luzGlobal2.SetActive(false);

        // 3. Simulamos el rayo
        yield return StartCoroutine(EfectoRayo());

        // 4. Parpadean las Spot Lights y finalmente se apagan
        yield return StartCoroutine(ParpadeoSpots());

        // 5. Volvemos a encender la luz global principal
        luzGlobal1.SetActive(true);

        // La luz del rayo queda apagada
        luzGlobal2.SetActive(false);

        // 6. Activamos el panel
        panel.SetActive(true);

        // 7. Ejecutamos el FadeIn
        fadePanel.FadeIn();
    }

    private IEnumerator EfectoRayo()
    {
        // PRIMER DESTELLO
        luzGlobal2.SetActive(true);

        // SONIDO DEL RAYO
        if (sonidoRayo != null)
            sonidoRayo.Play();

        yield return new WaitForSeconds(rayoEncendido);

        luzGlobal2.SetActive(false);
        yield return new WaitForSeconds(rayoApagado);

        // SEGUNDO DESTELLO
        luzGlobal2.SetActive(true);
        yield return new WaitForSeconds(rayoEncendido);

        luzGlobal2.SetActive(false);
        yield return new WaitForSeconds(pausaEntreRayos);
    }

    private IEnumerator ParpadeoSpots()
    {
        // Parpadean todas las luces
        for (int i = 0; i < cantidadParpadeos; i++)
        {
            bool encendidas = i % 2 == 0;

            foreach (GameObject luz in lucesSpot)
            {
                if (luz != null)
                    luz.SetActive(encendidas);
            }

            yield return new WaitForSeconds(tiempoParpadeo);
        }

        // Finalmente todas quedan apagadas
        foreach (GameObject luz in lucesSpot)
        {
            if (luz != null)
                luz.SetActive(false);
        }
    }
}