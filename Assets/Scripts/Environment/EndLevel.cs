using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour 
{
    public string levelName;

    private void OnTriggerEnter(Collider other)
    {
        // If the other object is player, move them back to the start
        if(other.tag.Equals("Player"))
        {
            GameManager.Instance.EndLevel(levelName);
        }
    }
}
