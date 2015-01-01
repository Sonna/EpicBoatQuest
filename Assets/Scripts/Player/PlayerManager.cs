using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
    private int counter = 0;

    // Use this for initialization
    void Start ()
    {
        GameManager.Instance.player = this.gameObject;
        GameManager.Instance.shipStartPosition = transform.position;
    }

    void Update()
    {
        // Reset player location on R press
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(counter >= 3)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                GameManager.Instance.player.transform.position = GameManager.Instance.shipStartPosition;
            }

            counter++;
        }

        // Reset player location on R press
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
