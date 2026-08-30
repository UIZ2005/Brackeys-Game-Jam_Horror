using System.Collections;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public GameObject salida;
    public GameObject objtransicion;
    public Animator anim;
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
            objtransicion.SetActive(true);
            StartCoroutine(teleport(collision.gameObject));
        }
    }
    IEnumerator teleport(GameObject player)
    {
        anim.SetBool("enter", true);
        yield return new WaitForSecondsRealtime(0.5f);
        player.transform.position = salida.transform.position;

        yield return new WaitForSecondsRealtime(tiempoTransicion);
        anim.SetBool("enter", false);


        yield return null;
    }
}
