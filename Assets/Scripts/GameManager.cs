using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public Vector3 shipStartPosition;
    public Texture emptyStarTexture;
    public Texture starTexture;
    public int iconWidth;
    public int iconHeight;
    
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
}
