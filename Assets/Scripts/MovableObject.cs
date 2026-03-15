using TMPro;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public ClickableObject clickableObject;
    public Passcode passcode;
    public float speed;
    public Vector2 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clickableObject != null)
        {
            if (clickableObject.clicked == true)
            {
                MoveObject();
            }
        }

        if (passcode != null)
        {
            if (passcode.unlock == true)
            {
                MoveObject();
            }
        }

    }

    void MoveObject()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }
}
