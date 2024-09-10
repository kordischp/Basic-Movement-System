using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;  // Needed for UI elements

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;
    public float sprintMultiplier = 1.5f;
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;

    Vector2 motionVector;
    public Vector2 lastMotionVector;
    Animator animator;
    public bool moving;

    public TMP_Text actionText;
    private bool isDashing;
    private bool isSprinting;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDashing = false;
        isSprinting = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Handle sprint when Shift is held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            actionText.text = "Sprint";
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        // Set movement speed when sprinting
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;

        motionVector = new Vector2(horizontal, vertical);
        moving = horizontal != 0 || vertical != 0;

        if (moving && !isDashing)
        {
            lastMotionVector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("horizontal", horizontal);
            animator.SetFloat("vertical", vertical);

            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);

            animator.speed = 1f;
        }
        else if (!isDashing)
        {
            animator.speed = 0f;
        }

        animator.SetBool("moving", moving);

        // Dash mechanic
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isDashing)
        {
            float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
            rigidbody2d.velocity = motionVector * currentSpeed;
        }
    }

    private IEnumerator Dash()
    {
        actionText.text = "Dash";
        isDashing = true;

        // Calculate dash direction and distance
        Vector2 dashPosition = rigidbody2d.position + lastMotionVector * dashDistance;

        // Move the character to dash position over dashDuration
        float elapsedTime = 0;
        Vector2 initialPosition = rigidbody2d.position;

        while (elapsedTime < dashDuration)
        {
            rigidbody2d.position = Vector2.Lerp(initialPosition, dashPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rigidbody2d.position = dashPosition;
        isDashing = false;
        actionText.text = "";  // Clear the action text after dashing
    }
}
