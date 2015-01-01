using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public float endLevelTimer = 12f;
    public GameObject player;
    public Vector3 shipStartPosition;
    public Texture emptyStarTexture;
    public Texture starTexture;
    public int iconWidth;
    public int iconHeight;
    public bool gameEnd = false;
    
    public int starsCollected = 0;
    private int totalStars = 0;

    void Start()
    {
        StarPickup[] totalStarsInScene = FindObjectsOfType<StarPickup>();

        totalStars = totalStarsInScene.Length;
    }

    void OnGUI()
    {
        if(!gameEnd)
        {
            if (!starTexture) 
            {
                return;
            }
            
            for(int i = 0; i < totalStars; i++)
            {
                GUI.DrawTexture(new Rect(i * iconWidth, 0, iconWidth, iconHeight), (i < starsCollected ? starTexture : emptyStarTexture));
            }
        }
        else
        {
            // Get half the screen and desired GUI item width
            float Width = iconWidth * 3;
            float Height = iconHeight * 3;

            float halfStars = totalStars / 2f;

            // Center screen then start in the middle on Y Axis 
            // and center minus half the total length of stars on X Axis
            float StartX = (float)((UnityEngine.Screen.width / 2)) - (halfStars * Width);
            float StartY = (float)((UnityEngine.Screen.height / 2)) - (Height * 0.5f);

            for(int i = 0; i < totalStars; i++)
            {
                float x = StartX + (i * Width);

                GUI.DrawTexture(new Rect(x, StartY, Width, Height), (i < starsCollected ? starTexture : emptyStarTexture));
            }

            // Show level complete above the stars
            GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontStyle = FontStyle.Bold;
            centeredStyle.normal.textColor = Color.blue;
            centeredStyle.fontSize = 100;

            // Draw it to screen
            GUI.Label(new Rect((float)((UnityEngine.Screen.width / 2) - 500f),
                                        StartY - 100, 1000, 100),
                                        "Level Complete!", centeredStyle);
        }
    }

    public void EndLevel(string nextLevel)
    {
        gameEnd = true;
        StartCoroutine(RotateCamera());
        StartCoroutine(LoadNextLevel(nextLevel));

    }

    private IEnumerator LoadNextLevel(string nextLevel)
    {
        yield return new WaitForSeconds(endLevelTimer);
        Application.LoadLevel(nextLevel);
    }

    private IEnumerator RotateCamera()
    {
        CameraController cameraCont = Camera.main.GetComponent<CameraController>();
        Camera.main.GetComponent<SmoothFollow>().enabled = false;
        while(true)
        {
            cameraCont.zoomOut();
            cameraCont.rotateRight();
            yield return null;
        }
    }
}
