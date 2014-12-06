using UnityEngine;
using System.Collections;

public class SpinOnSpot : MonoBehaviour 
{
    public float rotateSpeed = 5f;
    
    // Update is called once per frame
    void Update ()
    {
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
