using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float horizontalVelocity;
    [SerializeField]
    private float jumpIntensity;
    private bool rightDirection;
    private bool leftDirection;
    private bool isCollidingWithGround;
    private Rigidbody2D rigidbodyComponent;
    public Animator animator;

    private bool hasDoubleJump;
    private bool hasBossKey;
    private bool hasFireProjectile = true;

    private int jumpCount = 1;
    private int maxJumps = 1;

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        leftDirection = false;
        rightDirection = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpCount > 0)
            if ((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow))))
                Jump();

        PlayerMovement();
    }

    //
    // Movements
    //

    void PlayerMovement()
    {
        if (!isMoving())
        {
            RunningAnimation(false);
        }

        else
        {
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (!leftDirection)
                {
                    Flip(false);
                }
                transform.position += Vector3.left * horizontalVelocity * Time.deltaTime;
                RunningAnimation(true);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (!rightDirection)
                {
                    Flip(true);
                }
                transform.position += Vector3.right * horizontalVelocity * Time.deltaTime;
                RunningAnimation(true);
            }
        }

    }

    private bool isMoving()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            return true;
        else
            return false;
    }

    private void RunningAnimation(bool moving)
    {
        if(moving)
            animator.SetBool("Moving", true);
        else
            animator.SetBool("Moving", false);

    }

    void Jump()
    {
        rigidbodyComponent.AddForce(Vector2.up * jumpIntensity, ForceMode2D.Impulse);
        jumpCount--;
    }

    void Flip(bool direction)
    {
        //True flips to the right. False flips to the left
        Vector3 scale = transform.localScale;

        if (direction)
        {
            scale.x *= -1;
            transform.localScale = scale;
            leftDirection = false;
            rightDirection = true;
        }
        else
        {
            scale.x *= -1;
            transform.localScale = scale;
            leftDirection = true;
            rightDirection = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            jumpCount = maxJumps;
            isCollidingWithGround = true;
            animator.SetBool("Jumping", false);
        }

        if (collision.collider.CompareTag("JumpBootItem"))
        {
            maxJumps++;
            Destroy(collision.gameObject);
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isCollidingWithGround = false;
            animator.SetBool("Jumping", true);
        }
    }

    //
    // Getters and Setters
    //

    public bool getDirection()
    {
        if (rightDirection)
            return true;
        else
            return false;
    }
}
