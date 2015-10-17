using UnityEngine;
using System.Collections;

//Taken from tutorial https://unity3d.com/learn/tutorials/modules/intermediate/graphics/fading-between-scenes
//with some modifications

public class FadeScript : MonoBehaviour {

    public Texture2D fadeTexture;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -10000;
    private float alpha = 0.0f;
    private int fadeDir = 0;
    private bool fadeIn = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }

    private void BeginFade(int direction)
    {
        fadeDir = direction;
    }

    void OnLevelWasLoaded()
    {
        if (fadeIn)
        {
            BeginFade(-1);
            fadeIn = false;
        }
    }

    private IEnumerator FadeOut(string level)
    {
        Debug.Log("Step 1");
        fadeIn = true;
        BeginFade(1);
        yield return new WaitForSeconds(1);
        Debug.Log("Step 2");
        Application.LoadLevel(level);
    }

    public void FadeToLevel(string level)
    {
        StartCoroutine(FadeOut(level));
    }

}
