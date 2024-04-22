using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 input;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.isPlayerDead == false)
        {
            ProcessInputs();
            Flip();
            animator.SetFloat("Speed", input.magnitude);
        }

    }

    private void ProcessInputs()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        // Normalizacion del vector para que se mueva a la misma velocidad en diagonal.
        input = new Vector2(xInput, yInput).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = input * speed;
    }

    private void Flip()
    {
        // Si mirando hacia un lado damos al contrado

        if ((isFacingRight && input.x < 0f) || !isFacingRight && input.x > 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
    }
}
