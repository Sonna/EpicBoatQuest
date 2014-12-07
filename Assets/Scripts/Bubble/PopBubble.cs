using UnityEngine;
using System.Collections;

public class PopBubble : MonoBehaviour
{
    public float secondsToLive = 5.0f;

    public void Start()
    {
        StartCoroutine(BubblePop());
    }

    IEnumerator BubblePop()
    {
        yield return new WaitForSeconds(secondsToLive);
        Destroy(this.gameObject);
    }
}
