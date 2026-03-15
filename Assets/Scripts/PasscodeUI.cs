using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasscodeUI : MonoBehaviour
{
    public Passcode passcode;
    public Player player;
    public TMP_InputField resultText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmButton()
    {
        passcode.GetPasscode(resultText.text);
        player.canMove = true;
        Destroy(gameObject);
    }

    public void ExitButton()
    {
        Destroy(gameObject);
        player.canMove = true;
        passcode.interactable = true;
    }
}
