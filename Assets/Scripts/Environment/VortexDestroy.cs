using UnityEngine;
using System.Collections;

public class VortexDestroy : MonoBehaviour 
{

    private void OnTriggerEnter(Collider other)
    {
        // If the other object is player, move them back to the start
        if(other.tag.Equals("Player"))
        {
            other.rigidbody.velocity = Vector3.zero;
            other.transform.position = GameManager.Instance.shipStartPosition;
        }
    }
}
