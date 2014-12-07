using UnityEngine;
using System.Collections;

public class LoadLogo : MonoBehaviour {

    public Texture2D[] logos;
    private Texture2D logo;

    // Use this for initialization
    public void Start()
    {
        logo = logos[Random.Range(0, logos.Length)];

        // Get half the screen and desired GUI item width
        float ScreenX = (float)((UnityEngine.Screen.width * 0.5) - (Width * 0.5));
        float ScreenY = (float)((UnityEngine.Screen.height) - (Height * 0.5));
    }
}
