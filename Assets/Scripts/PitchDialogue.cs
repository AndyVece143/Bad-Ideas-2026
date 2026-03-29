using UnityEngine;
using TMPro;
using System.Collections;

public class PitchDialogue : MonoBehaviour
{
    public TextMeshPro textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        timer = 1f;

        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (textComponent.text == lines[index])
        //    {
        //        NextLine();
        //    }
        //    else
        //    {
        //        StopAllCoroutines();
        //        textComponent.text = lines[index];
        //    }
        //}
        if (textComponent.text == lines[index])
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                NextLine();
                timer = 1f;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
