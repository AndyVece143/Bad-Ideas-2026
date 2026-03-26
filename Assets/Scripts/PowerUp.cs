using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string[] dialogueLines;
    public Player player;
    public bool isDoubleJump;
    public bool isAirDash;
    public BoxCollider2D boxCollider;
    public Dialogue dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = Player.FindAnyObjectByType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dialogue newDialogue = Instantiate(dialogue);
            newDialogue.lines = dialogueLines;

            if (isDoubleJump)
            {
                player.doubleJump = true;
            }

            if (isAirDash)
            {
                player.airDash = true;
            }

            Destroy(gameObject);
        }
    }
}
