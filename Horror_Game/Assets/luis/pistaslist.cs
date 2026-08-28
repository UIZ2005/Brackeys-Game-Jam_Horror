using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class pistaslist : MonoBehaviour
{
    public TextMeshProUGUI lista;
    private Animator animator;
    private float typingtext = 0.06f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void agregarPista(string pista)
    {
        lista.text += "\n -";
        StartCoroutine(escribirpista(pista));
    }

    IEnumerator escribirpista(string pista)
    {
        animator.SetBool("open", true);

        foreach (char ch in pista)
        {
            lista.text += ch;
            yield return new WaitForSecondsRealtime(typingtext);
        }

        yield return new WaitForSecondsRealtime(10f);
        animator.SetBool("open", false);

        yield return null;
    }
    public void limpiarlista()
    {
        lista.text = " -";
    }

    public void abir()
    {
        if (animator.GetBool("open"))
        {
            animator.SetBool("open", false);
        }
        else
        {
            animator.SetBool("open", true);
        }
        
    }

}
