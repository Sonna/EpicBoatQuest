using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour 
{
    public string levelName;

    public AudioClip endSoundEffect;
    private AudioSource playerAudioSource;
    
    // Use this for initialization
    void Start()
    {
        playerAudioSource = gameObject.GetComponent<AudioSource>();
        if (playerAudioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            playerAudioSource = gameObject.GetComponent<AudioSource>();
        }
        playerAudioSource.clip = endSoundEffect;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the other object is player, move them back to the start
        if(other.tag.Equals("Player"))
        {
            playerAudioSource.Play();
            GameManager.Instance.EndLevel(levelName);
        }
    }
}
