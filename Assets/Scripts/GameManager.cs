using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public Vector3 shipStartPosition;
    public Texture starTexture;
    public int iconWidth;
    public int iconHeight;
    
    public int starsCollected = 0;


    void OnGUI()
    {
        if (!starTexture) 
        {
            return;
        }

        for(int i = 0; i < starsCollected; i++)
        {
            GUI.DrawTexture(new Rect(i * iconWidth, 0, iconWidth, iconHeight), starTexture);
        }
    }
}
