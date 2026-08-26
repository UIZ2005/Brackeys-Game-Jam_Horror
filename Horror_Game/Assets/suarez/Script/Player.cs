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
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = movement.normalized;

        rb.linearVelocity = direction * speed;

        UpdateAnimation(direction);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        bool isWalking = direction != Vector2.zero;

        // Activar/desactivar caminar
        animator.SetBool("IsWalking", isWalking);

        if (!isWalking)
            return;

        float x = direction.x;
        float y = direction.y;

        // Resetear direcciones
        animator.SetBool("WalkUp", false);
        animator.SetBool("WalkDown", false);
        animator.SetBool("WalkRight", false);
        animator.SetBool("WalkUpRight", false);

        // -------------------------
        // ARRIBA
        // -------------------------
        if (y > 0.5f && Mathf.Abs(x) < 0.5f)
        {
            animator.SetBool("WalkUp", true);
            spriteRenderer.flipX = false;
        }

        // -------------------------
        // ABAJO
        // -------------------------
        else if (y < -0.5f && Mathf.Abs(x) < 0.5f)
        {
            animator.SetBool("WalkDown", true);
            spriteRenderer.flipX = false;
        }

        // -------------------------
        // DIAGONAL ARRIBA DERECHA
        // -------------------------
        else if (x > 0.5f && y > 0.5f)
        {
            animator.SetBool("WalkUpRight", true);
            spriteRenderer.flipX = false;
        }

        // -------------------------
        // DIAGONAL ARRIBA IZQUIERDA
        // -------------------------
        else if (x < -0.5f && y > 0.5f)
        {
            animator.SetBool("WalkUpRight", true);
            spriteRenderer.flipX = true;
        }

        // -------------------------
        // DERECHA
        // -------------------------
        else if (x > 0.5f)
        {
            animator.SetBool("WalkRight", true);
            spriteRenderer.flipX = false;
        }

        // -------------------------
        // IZQUIERDA
        // -------------------------
        else if (x < -0.5f)
        {
            animator.SetBool("WalkRight", true);
            spriteRenderer.flipX = true;
        }
    }
}
