using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VortexSuck : MonoBehaviour 
{
    private List<GameObject> collidersInside;
    public float pullForce = 1000f;
    public float pullTime = 1f;
    private bool pullEnabled = false;

    // Use this for initialization
    void Start () 
    {
        collidersInside = new List<GameObject>();
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {
        foreach(GameObject suck in collidersInside)
        {
            // ensure the objects have rigid bodies
            if(suck.rigidbody == null)
            {
                break;
            }

            float force = pullForce;
            Vector3 forceDirection;

            if(pullEnabled)
            {
                // calculate direction from target to me
                forceDirection = transform.position - suck.transform.position;
            }
            else
            {
                force *= 5;
                // calculate direction from target to me
                forceDirection = transform.position + suck.transform.position;
            }

            // apply force on target towards me
            suck.rigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
        }
    }

    private IEnumerator TriggerSucking()
    {
            pullEnabled = true;
            yield return new WaitForSeconds(pullTime);
            pullEnabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        collidersInside.Add(other.gameObject);
        StartCoroutine(TriggerSucking());
    }

    private void OnTriggerExit(Collider other)
    {
        if(collidersInside.Contains(other.gameObject))
        {
            collidersInside.Remove(other.gameObject);
        }
    }
}
