using UnityEngine;
using System.Collections;

public class PlayBumpOnCollision : MonoBehaviour
{
    public AudioClip bumpSoundEffect;

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
        playerAudioSource.clip = bumpSoundEffect;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            playerAudioSource.Play();
        }
    }
}
