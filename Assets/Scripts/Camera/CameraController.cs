using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    public enum MOUSEBUTTON
    {
        LEFT_CLICK,
        RIGHT_CLICK,
        MIDDLE_CLICK
    }
    
    public GameObject boat;
    public float rotateSpeed = 25f;

    // Use this for initialization
    void Start ()
    {
        if(boat == null)
        {
            boat = GameObject.FindGameObjectWithTag("Player");
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
        // Make hand gestures here
        
        if(Input.GetMouseButton((int)MOUSEBUTTON.LEFT_CLICK))
        {
            this.transform.RotateAround(boat.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
        
        if(Input.GetMouseButton((int)MOUSEBUTTON.RIGHT_CLICK))
        {
            this.transform.RotateAround(boat.transform.position, Vector3.up, -rotateSpeed * Time.deltaTime);
        }
    }
}
