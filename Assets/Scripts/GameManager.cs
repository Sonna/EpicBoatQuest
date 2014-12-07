using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public Vector3 shipStartPosition;
    
    public int starsCollected = 0;
}
