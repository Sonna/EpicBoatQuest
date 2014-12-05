using UnityEngine;
using System.Collections;

public class ShipBuoyancy : MonoBehaviour 
{
    private bool inWater = false;

    // Use this for initialization
    void Start () 
    {
        //
    }
    
    // Update is called once per frame
    void FixedUpdate () 
    {
        if(inWater)
        {

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
