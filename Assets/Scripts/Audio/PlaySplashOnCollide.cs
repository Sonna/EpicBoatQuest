using UnityEngine;
using System.Collections;

public class PlaySplashOnCollide : MonoBehaviour
{
    public AudioClip[] splashSoundEffects;

    private AudioSource objectAudioSource;


    // Use this for initialization
    void Start()
    {
        objectAudioSource = gameObject.GetComponent<AudioSource>();
        if (objectAudioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            objectAudioSource = gameObject.GetComponent<AudioSource>();
        }
        objectAudioSource.clip = splashSoundEffects[Random.Range(0, splashSoundEffects.Length)];
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            objectAudioSource.Play();
        }
    }
}
