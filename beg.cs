using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript2D : MonoBehaviour
{
    private Rigidbody2D rb;
    private float HorizontalMove = 0f;
    private bool FacingRight = true;

    [Header("Player Movement Settings")]
    [Range(0, 10f)] public float walkSpeed = 1f; // скорость при Shift
    [Range(0, 10f)] public float runSpeed = 0.5f; // базовая скорость (ходьба)
    [Range(0, 15f)] public float jumpForce = 8f;

    [Header("Player Animation Settings")]
    public Animator animator;

    [Header("Ground Checker Settings")]
    public bool isGrounded = false;
    [Range(-5f, 5f)] public float checkGroundOffsetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   void Update()
{
    if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Attack"); // Запуск анимации удара
}
    if (Input.GetKeyDown(KeyCode.Q))
{
            animator.SetTrigger("Attack2"); // Запуск анимации удара2
        }
    float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed : runSpeed;

   {
        // Проверка на землю с использованием Raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
{
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    animator.SetTrigger("Jumping");
}

   }
    HorizontalMove = Input.GetAxisRaw("Horizontal") * currentSpeed;

    animator.SetFloat("HorizontalMove", Mathf.Abs(HorizontalMove));
    
    // Проверка анимации ходьбы/бега
    if (currentSpeed == walkSpeed)
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsRunning", false);
    }
    else
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunning", true);
    }

    animator.SetBool("Jumping", !isGrounded);

    if (HorizontalMove < 0 && FacingRight)
    {
        Flip();
    }
    else if (HorizontalMove > 0 && !FacingRight)
    {
        Flip();
    }
}


    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(HorizontalMove * 10f, rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;

        CheckGround();
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

private void CheckGround()
{
    // Использование OverlapCircleAll для проверки земли
    Collider2D[] colliders = Physics2D.OverlapCircleAll(
        new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);

    isGrounded = false; // По умолчанию не на земле

    foreach (var collider in colliders)
    {
        // Проверяем, есть ли тег "Ground" у объекта
        if (collider.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Персонаж на земле!");
            break; // Прекращаем проверку, как только нашли землю
        }
    }
}

}