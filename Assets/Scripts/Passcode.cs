using UnityEngine;

public class Passcode : MonoBehaviour
{
    public string password;
    public bool unlock = false;
    //public string inputedPassword;
    public BoxCollider2D boxCollider;
    public Player player;
    public PasscodeUI passcodeUI;
    public bool interactable;

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
                PasscodeUI newPasscodeUI = Instantiate(passcodeUI);
                newPasscodeUI.passcode = this;
            }
        }
    }

    public void GetPasscode(string inputedCode)
    {
        if (inputedCode == password)
        {
            Debug.Log("liberal");
            unlock = true;
        }
    }
}
