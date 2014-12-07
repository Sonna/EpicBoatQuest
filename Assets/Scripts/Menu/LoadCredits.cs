using UnityEngine;
using System.Collections;

public class LoadCredits : MonoBehaviour {

    public Texture2D credits;
    private float Width;
    private float Height;

    // Use this for initialization
    public void Start()
    {
        Width = credits.width;
        Height = credits.height;
    }

    void OnGUI()
    {
        // Get half the screen and desired GUI item width
        float ScreenX = (float)((UnityEngine.Screen.width * 0.5) - (Width * 0.5));
        float ScreenY = (float)((UnityEngine.Screen.height* 0.05));

        Width *= ((float)(UnityEngine.Screen.width * 0.5)/ Width);
        Height *= ((float)(UnityEngine.Screen.height * 0.65)/ Height);

        GUI.DrawTexture(new Rect(ScreenX, ScreenY, Width, Height), credits);
    }
}
