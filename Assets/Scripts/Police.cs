using System.Collections;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class Police : MonoBehaviour
{
    private Rigidbody2D body;
    public float wanderSpeed;
    public float pursueSpeed;
    public Transform ledgeDetector;
    public LayerMask groundLayer;
    public float raycastDistance;
    public float wallDistance;
    private BoxCollider2D boxCollider;
    public float sightDistance;

    private bool facingRight = true;
    private Vector2 forwards;

    public Player player;

    private bool turning = false;
    public float jumpForce;

    [SerializeField] private LayerMask raycastLayers;

    public LineRenderer lineRenderer;

    private Vector2 initialPosition;
    private State initialState;
    
    public enum State
    {
        Moving,
        Standing,
        Purusing,
    }
    public State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        initialPosition = transform.position;
        initialState = state;
    }

    // Update is called once per frame
    void Update()
    {
        Detection();

        switch (state)
        {
            case State.Moving:
                Moving();
                break;
            case State.Standing:
                break;
            case State.Purusing:
                Pursuing(); 
                break;
        }
    }

    //Method for patrolling
    private void Moving()
    {
        body.linearVelocity = new Vector2(wanderSpeed, body.linearVelocity.y);
        if (facingRight)
        {
            forwards = Vector2.right;
        }
        else
        {
            forwards = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D hitWall = Physics2D.Raycast(ledgeDetector.position, forwards, wallDistance, groundLayer);

        if (hit.collider == null || hitWall == true)
        {
            Rotate();
        }

        //Enable the sight line
        lineRenderer.enabled = true;
    }

    //Method for turning the object around
    void Rotate()
    {
        transform.Rotate(0, 180, 0);
        wanderSpeed = -wanderSpeed;
        pursueSpeed = -pursueSpeed;

        if (facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }

    //Method for detecting the player and drawing the sight line
    void Detection()
    {
        RaycastHit2D detection = Physics2D.Raycast(ledgeDetector.position, forwards, sightDistance, raycastLayers);
        lineRenderer.SetPosition(0, ledgeDetector.position);
        
        if (detection.collider != null)
        {
            //Debug.DrawRay(ledgeDetector.position, forwards * detection.distance, Color.red);
            lineRenderer.SetPosition(1, detection.point);

            if (detection.collider.CompareTag("Player"))
            {
                state = State.Purusing;
            }
        }
        else
        {
            //Debug.DrawRay(ledgeDetector.position, forwards * sightDistance, Color.red);
            lineRenderer.SetPosition(1, new Vector3(ledgeDetector.position.x + forwards.x * sightDistance, ledgeDetector.position.y, 0));
        }
    }

    //Method for chasing the player
    void Pursuing()
    {
        body.linearVelocity = new Vector2(pursueSpeed, body.linearVelocity.y);

        if (facingRight)
        {
            forwards = Vector2.right;
        }
        else
        {
            forwards = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.Raycast(ledgeDetector.position, Vector2.down, raycastDistance, groundLayer);
        RaycastHit2D hitWall = Physics2D.Raycast(ledgeDetector.position, forwards, wallDistance, groundLayer);

        //Will attempt to jump up walls to chase player
        if (hitWall == true && isGrounded())
        {
            Jump();
        }

        //Brief pause before turning around while chasing
        float distance = transform.position.x - player.transform.position.x;

        if (distance < 0.0f && !facingRight && turning == false)
        {
            StartCoroutine(waiterTurn());
            //Rotate();
            turning = true;
        }
        if (distance > 0 && facingRight && turning == false)
        {
            StartCoroutine(waiterTurn());
            //Rotate();
            turning = true;
        }

        if (distance > 11f || distance < -11f)
        {
            Debug.Log("Stop chasing");
            Respawn();
        }

        //Turn off line
        lineRenderer.enabled = false;
    }

    //Method for jumping
    void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
    }

    //Respawns the object
    void Respawn()
    {
        transform.position = initialPosition;
        state = initialState;
    }

    //Checks if the object is grounded
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //Timer used to create delay when turning
    IEnumerator waiterTurn()
    {
        yield return new WaitForSeconds(0.2f);
        Rotate();
        turning = false;
    }
}
