using UnityEngine;
using System.Collections;

public class ShipBuoyancy : MonoBehaviour 
{
    public float bounceDampening;
    public float forceFactor;
    private float triggerHeight;
    private bool inWater = false;
    private Vector3 uplift;
    private Vector3 forceDirection;
    private float boatBottom;

    // Use this for initialization
    void Start () 
    {
        uplift = new Vector3();
        //
    }
    
    // Update is called once per frame
    void FixedUpdate () 
    {
        if(inWater)
        {
            forceDirection = transform.position + transform.TransformDirection(Vector3.down);
            forceFactor = (1f - transform.position.y);

            uplift = -Physics.gravity * (forceFactor - rigidbody.velocity.y * bounceDampening);
            rigidbody.AddForceAtPosition(uplift, forceDirection);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            inWater = true;
        }
    }
        
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Water")
        {
            inWater = false;
        }
    }
}
