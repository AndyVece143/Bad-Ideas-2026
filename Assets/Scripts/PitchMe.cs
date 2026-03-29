using UnityEngine;

public class PitchMe : MonoBehaviour
{
    public Animator anim;
    public int animInt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animInt = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("talk", animInt);
    }
}
