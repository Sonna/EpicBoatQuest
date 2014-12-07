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
        if (!starTexture) 
        {
            return;
        }

        for(int i = 0; i < totalStars; i++)
        {
            GUI.DrawTexture(new Rect(i * iconWidth, 0, iconWidth, iconHeight), (i < starsCollected ? starTexture : emptyStarTexture));
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
