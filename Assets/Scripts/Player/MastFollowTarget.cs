using UnityEngine;
using System.Collections;

public class MastFollowTarget : MonoBehaviour 
{
    public GameObject targetObject;
    public float rotationSpeed;
    
    // Use this for initialization
    void Start () 
    {
        if(targetObject == null)
        {
            targetObject = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
        Vector3 dir = targetObject.transform.position - transform.position;

        // Why is this working!?... BLACK MAGIC!?!?!?!?!?!?
        dir.y = 100000;

        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);

    }
}
