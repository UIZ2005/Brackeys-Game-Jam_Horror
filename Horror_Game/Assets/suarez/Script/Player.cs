using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Object")]
    [SerializeField] private GameObject linterna;
    [SerializeField] private pistaslist pistamanager;

    private Diaologo NPCActual;
    public bool quieto = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputValue value)
    {
        if (quieto) return;


        movement = value.Get<Vector2>();
        linterna.SetActive(true);

        if (movement != Vector2.zero)
        {
            if (movement.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            animator.SetFloat("XInput", movement.x);
            animator.SetFloat("YInput", movement.y);
        }

    }
    public void OnAttack(InputValue value)
    {
        if (quieto) return;
        
        if (linterna.activeSelf)
        {
            linterna.SetActive(false);
        }
        else
        {
            linterna.SetActive(true);
        }
    }
    public void OnInteract(InputValue value)
    {
        if (NPCActual != null)
        {
            NPCActual.interact();
        }
    }
    public void OnNext(InputValue value)
    {
        if (quieto) return;
        pistamanager.abir();
    }


    private void FixedUpdate()
    {
        if (quieto) return;

        Vector2 direction = movement.normalized;

        rb.linearVelocity = direction * speed;
        if (movement != Vector2.zero)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            NPCActual = collision.GetComponent<Diaologo>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            NPCActual = null;
        }
    }

    public void ActivarQuieto()
    {
        quieto = true;
    }

    public void DesactivarQuieto()
    {
        quieto = false;
    }


}
