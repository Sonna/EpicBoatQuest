using UnityEngine;
using System.Collections;

public class LoadLogo : MonoBehaviour {

    public Texture2D[] logos;
    private Texture2D logo;
    private float Width;
    private float Height;

    // Use this for initialization
    public void Start()
    {
        logo = logos[Random.Range(0, logos.Length)];
        Width = logo.width;
        Height = logo.height;
    }

    void OnGUI()
    {
        // Get half the screen and desired GUI item width
        float ScreenX = (float)((UnityEngine.Screen.width * 0.5) - (Width * 0.5));
        float ScreenY = (float)((UnityEngine.Screen.height* 0.05));

        Width *= ((float)(UnityEngine.Screen.width * 0.5)/ Width);
        Height *= ((float)(UnityEngine.Screen.height * 0.65)/ Height);

        GUI.DrawTexture(new Rect(ScreenX, ScreenY, Width, Height), logo);
    }
}
