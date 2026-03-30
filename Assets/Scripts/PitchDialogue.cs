using UnityEngine;
using TMPro;
using System.Collections;

public class PitchDialogue : MonoBehaviour
{
    public TextMeshPro textComponent;
    public string[] lines;
    public float textSpeed;
    public int index;
    private float timer;
    public PitchMe pitchMe;

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
        switch (index)
        {
            case 0:
                pitchMe.animInt = 8;
                break;
            case 1:
                pitchMe.animInt = 0;
                break;
            case 2:
                pitchMe.animInt = 7;
                break;
            case 3:
                pitchMe.animInt = 1;
                break;
            case 4:
                pitchMe.animInt = 9;
                break;
        }

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
