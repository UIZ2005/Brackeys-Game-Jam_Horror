using System.Collections;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public GameObject salida;
    public GameObject anim;
    public float tiempoTransicion=1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = salida.transform.position;
        }
    }
    IEnumerator teleport(GameObject player)
    {
        anim.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        player.transform.position = salida.transform.position;

        yield return new WaitForSecondsRealtime(tiempoTransicion);
        player.GetComponent<Player>().quieto = false;


        yield return null;
    }
}
