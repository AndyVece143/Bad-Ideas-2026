using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SignUI : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public UnityEngine.UI.Button exitButton;
    public Sign sign;
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitButton()
    {
        Destroy(gameObject);
        player.canMove = true;
        sign.interactable = true;
    }
}
