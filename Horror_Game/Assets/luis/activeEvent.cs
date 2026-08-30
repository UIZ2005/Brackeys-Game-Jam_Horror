using UnityEngine;

public class activeEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject desactivar;
    public GameObject activar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activarevento()
    {
        desactivar.SetActive(false);
        activar.SetActive(true);
    }

}
