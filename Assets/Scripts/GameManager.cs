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

            float ScreenX = (float)((UnityEngine.Screen.height * 0.5) - (Width * 0.5));
            float ScreenY = (float)((UnityEngine.Screen.height / 2 ) - (Height * 0.5));

            for(int i = 0; i < totalStars; i++)
            {
                ScreenY = (float)((UnityEngine.Screen.width * 0.5)) - ((Width * ((totalStars / 2) - i)));
                GUI.DrawTexture(new Rect(ScreenY, ScreenX, iconWidth * 3, iconHeight * 3), (i < starsCollected ? starTexture : emptyStarTexture));
            }
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
