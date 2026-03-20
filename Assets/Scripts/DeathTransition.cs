using UnityEngine;

public class DeathTransition : MonoBehaviour
{
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoTransition()
    {
        Debug.Log("Deat");
        anim.Play("trans");
    }
}
