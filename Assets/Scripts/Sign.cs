using UnityEngine;

public class Sign : MonoBehaviour
{
    public SignUI signUI;
    public Player player;
    public BoxCollider2D boxCollider;
    public bool interactable;
    public string signText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider))
        {
            if (player.isGrounded() && Input.GetKeyDown(KeyCode.Space) && interactable == true)
            {
                interactable = false;
                player.StopMoving();
                SignUI newSignUI = Instantiate(signUI);
                newSignUI.textComponent.text = signText;
                newSignUI.sign = this;
            }
        }
    }
}
