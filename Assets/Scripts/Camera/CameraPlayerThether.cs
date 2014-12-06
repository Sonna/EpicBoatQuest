using UnityEngine;
using System.Collections;

public class CameraPlayerThether : MonoBehaviour {

    private Transform playerTransform;

    // Temporary pool variables
    private Vector3 tempVector3;
    private float lift;
    private float distance;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lift = 5.0f;
        distance = 5.0f;
    }

    // Update is called once per frame
    void Update () {
        tempVector3.Set(0, lift, distance);

        transform.position = playerTransform.position + tempVector3;
        // transform.position = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z);
    }

    void LateUpdate ()
    {
        transform.LookAt(playerTransform);
    }
}
