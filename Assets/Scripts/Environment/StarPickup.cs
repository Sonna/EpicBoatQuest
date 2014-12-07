using UnityEngine;
using System.Collections;

public class StarPickup : MonoBehaviour 
{
    public float SpinIncrease;
    public float timeBetweenEvents;
    private SpinOnSpot starSpin;
    private ParticleSystem particles;

    // Use this for initialization
    void Start () 
    {
        starSpin = GetComponentInChildren<SpinOnSpot>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the other object is player, then pick up
        if(other.tag.Equals("Player"))
        {
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator DestroySelf()
    {
        float counter = 0f;

        particles.Stop();

        while(counter < timeBetweenEvents)
        {
            counter += Time.deltaTime;
            starSpin.rotateSpeed += SpinIncrease;
            // Spin it off the screen
            starSpin.transform.Translate(starSpin.transform.up / 45);
            yield return null;
        }

        GameManager.Instance.starsCollected++;
        Destroy(this.gameObject);
    }


}
