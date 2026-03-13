using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public bool clicked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Chris walters");
        clicked = true;
    }
}
