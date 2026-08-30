using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] noches;
    private int actual=0;
    public GameObject gameover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextNoche()
    {
        if (noches[actual] != null)
        {
            noches[actual].SetActive(false);
        }
        
        actual++;
        if (noches[actual] != null)
        {
            noches[actual].SetActive(true);
        }
        else
        {
            StartCoroutine(canvasGameover());
        }
    }

    IEnumerator canvasGameover()
    {
        gameover.SetActive(true);

        yield return new WaitForSecondsRealtime(3f);

        //devolver a escnea principal


        LoginEscena change = GetComponent<LoginEscena>();
        change.changeEscena("Canvas");

        yield return null;
    }
}
