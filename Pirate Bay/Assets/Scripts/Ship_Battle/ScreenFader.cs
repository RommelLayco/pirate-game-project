using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//https://unity3d.com/learn/tutorials/projects/stealth/screen-fader
public class ScreenFader : MonoBehaviour
{
    public ScreenFader(Canvas c)
    {
        canvas = c;
    }
    public Canvas canvas;
    public GUITexture texture;
    void Awake() {
       texture = canvas.GetComponent<GUITexture>();
       texture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }
    void Update()
    {
        if (texture.color.a >= 0.95f) { 
            Application.LoadLevel("Main");
        }
        // Lerp the colour of the texture between itself and black.
        texture.color = Color.Lerp(texture.color, Color.black, 1.5f * Time.deltaTime);
    }
}