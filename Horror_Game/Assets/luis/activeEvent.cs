using UnityEngine;

public class activeEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject desactivar;
    public GameObject activar;
    public bool iskill=false;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activarevento()
    {
        if(iskill){
            audioManager.seleccionAudio(5);
        }
        desactivar.SetActive(false);
        activar.SetActive(true);
    }

}
