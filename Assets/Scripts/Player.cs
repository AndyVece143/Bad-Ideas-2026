using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float jumpForceTwo;
    public float speed;
    private Rigidbody2D body;
    public BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;

    public bool canMove = true;
    public bool noClip = false;

    public bool doubleJump;
    private bool canDoubleJump = true;

    public bool airDash;
    private bool canAirDash = true;
    public float airDashSpeed;
    private bool isAirDashing = false;
    public float airDashTime;

    public float jumpTime;
    public float jumpTimeCounter;

    private bool isJumping;

    public GameManager manager;
    public Police police;
    public enum State
    {
        Standard,
        Grab,
    }
    public State state;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Standard;
    }

    // Update is called once per frame
    void Update()
    {
        //if (canMove)
        //{
        //    if (!noClip)
        //    {
        //        Movement();
        //    }
        //    if (noClip)
        //    {
        //        NoClipMovement();
        //    }
        //}

        switch (state)
        {
            case State.Standard:
                Movement();
                break;
            case State.Grab:
                Grabbed();
                break;
        }
    }

    private void Movement()
    {
        boxCollider.enabled = true;
        float horizontalInput = Input.GetAxis("Horizontal");

        if (canMove)
        {
            if (!isAirDashing)
            {
                body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                {
                    isJumping = true;
                    jumpTimeCounter = jumpTime;
                    Jump();
                }

                if (Input.GetKey(KeyCode.Space) && isJumping == true)
                {
                    if (jumpTimeCounter > 0)
                    {
                        Jump();
                        jumpTimeCounter -= Time.deltaTime;
                    }

                    else
                    {
                        isJumping = false;
                    }
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    isJumping = false;
                }

                if (Input.GetKeyDown(KeyCode.Space) && !isGrounded() && doubleJump && canDoubleJump)
                {
                    DoubleJump();
                    canDoubleJump = false;
                }

                if (isGrounded())
                {
                    canDoubleJump = true;
                    canAirDash = true;
                    body.gravityScale = 1.5f;
                }

                if (!isGrounded() && body.linearVelocity.y <= 0 && isAirDashing == false)
                {
                    body.gravityScale = 2;
                }
            }

            if (!isGrounded() && canAirDash && Input.GetKeyDown(KeyCode.O))
            {
                StartCoroutine(Dash());
            }
        }


        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void NoClipMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Physics2D.IgnoreLayerCollision(6, 7);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
    }

    private void DoubleJump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForceTwo);
    }

    public void StopMoving()
    {
        body.linearVelocity = Vector2.zero;
        canMove = false;
    }

    void Grabbed()
    {
        body.linearVelocity = new Vector2(0, 0);
        if (police != null)
        {
            body.transform.position = police.grabSpot.position;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Police")
        {
            state = State.Grab;
            police = collision.gameObject.GetComponent<Police>();
            police.state = Police.State.Grab;
            boxCollider.enabled = false;
            StartCoroutine(manager.waiter(police));
            //manager.RespawnPlayer(collision.gameObject.GetComponent<Police>());
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private IEnumerator Dash()
    {
        canAirDash = false;
        isAirDashing = true;

        body.gravityScale = 0;
        float airDashDirection = transform.localScale.x;
        body.linearVelocity = new Vector2(airDashSpeed * airDashDirection, 0);

        yield return new WaitForSeconds(airDashTime);

        isAirDashing = false;
    }
}
