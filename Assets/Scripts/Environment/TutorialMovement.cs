using UnityEngine;
using System.Collections;

public class TutorialMovement : MonoBehaviour {

    public float upSpeed;
    public float fadeTime;
    public AudioClip contactSoundEffect;
    public GameObject textObject;
    private AudioSource objectAudioSource;
    public float fadeSpeed = 10f;
    
    // Use this for initialization
    void Start () 
    {
        objectAudioSource = gameObject.GetComponent<AudioSource>();
        if (objectAudioSource == null)
        {
            gameObject.AddComponent<AudioSource>();
            objectAudioSource = gameObject.GetComponent<AudioSource>();
        }
        objectAudioSource.clip = contactSoundEffect;

        Color TextColor = textObject.renderer.material.color;
        
        TextColor.a = 0f; 
        textObject.renderer.material.color = TextColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the other object is player, then pick up
        if(other.tag.Equals("Player"))
        {
            StartCoroutine(FadeIn());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // If the other object is player, then destroy
        if(other.tag.Equals("Player"))
        {
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator FadeIn()
    {
        float counter = 0f;
        
        objectAudioSource.Play();
        
        while(counter < fadeTime)
        {
            counter += Time.deltaTime;
            ChangeTextColour(textObject, true);
            yield return null;
        }
    }
    
    private IEnumerator DestroySelf()
    {
        float counter = 0f;
        
        objectAudioSource.Play();
        
        while(counter < fadeTime)
        {
            counter += Time.deltaTime;
            // Spin it off the screen
            this.transform.Translate((this.transform.up / 45f) * upSpeed);
            ChangeTextColour(textObject, false);

            yield return null;
        }
        
        Destroy(this.gameObject);
    }

    // Update is called once per frame 
    private void ChangeTextColour(GameObject text, bool positive = false) 
    { 
        Color TextColor = text.renderer.material.color;

        TextColor.a = TextColor.a - ((0.01f * fadeSpeed) * (positive ? -1f : 1f)); 

        if(TextColor.a > 1f)
        {
            TextColor.a = 1f;
        }
        else if(TextColor.a < 0f)
        {
            TextColor.a = 0f;
        }

        text.renderer.material.color = TextColor;
        
    }
}
