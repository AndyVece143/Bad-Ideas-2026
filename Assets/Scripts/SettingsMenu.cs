using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    public GameObject brightness;
    public GameObject circleObject;

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void ChangeBrightness(float bright)
    {
        //UnityEngine.UI.Image image = brightness.GetComponent<UnityEngine.UI.Image>();
        //image.color = new Color(image.color.r, image.color.g, image.color.b, -bright);
        SpriteRenderer brightRender = brightness.GetComponent<SpriteRenderer>();
        brightRender.color = new Color(brightRender.color.r, brightRender.color.g, brightRender.color.b, -bright);

        //renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, (-bright));
        //Debug.Log("" + bright + "" + image.color);
        SpriteRenderer renderer = circleObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, -bright);

    }
}
