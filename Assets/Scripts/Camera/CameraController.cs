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

        if(Input.GetMouseButton((int)MOUSEBUTTON.LEFT_CLICK)
           || Input.GetKey(KeyCode.LeftArrow)
           || Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }

        if(Input.GetMouseButton((int)MOUSEBUTTON.RIGHT_CLICK)
           || Input.GetKey(KeyCode.RightArrow)
           || Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }

        if (Input.GetKey(KeyCode.W))
        {
            boat.rigidbody.AddRelativeForce(Camera.main.transform.forward, ForceMode.Acceleration);
        }
    }

    public void rotateLeft()
    {
        this.transform.RotateAround(boat.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void rotateRight()
    {
        this.transform.RotateAround(boat.transform.position, Vector3.up, -rotateSpeed * Time.deltaTime);
    }
}
