using UnityEngine;
using System.Collections;
using Leap;

/// <summary>
///
/// </summary>
public class LeapHandWave : MonoBehaviour
{

    Controller m_leapController;

    private int windDrag;
    private int windMouseDrag;
    private Vector2 mousePosLastframe;
    private CameraController cameraController;

    // Use this for initialization
    void Start()
    {
        m_leapController = new Controller();
        windDrag = 10000;
        windMouseDrag = 1000;
        mousePosLastframe = Input.mousePosition;
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    Hand GetLeftMostHand(Frame f)
    {
        float smallestVal = float.MaxValue;
        Hand h = null;
        for (int i = 0; i < f.Hands.Count; ++i)
        {
            if (f.Hands[i].PalmPosition.ToUnity().x < smallestVal)
            {
                smallestVal = f.Hands[i].PalmPosition.ToUnity().x;
                h = f.Hands[i];
            }
        }
        return h;
    }

    Hand GetRightMostHand(Frame f)
    {
        float largestVal = -float.MaxValue;
        Hand h = null;
        for (int i = 0; i < f.Hands.Count; ++i)
        {
            if (f.Hands[i].PalmPosition.ToUnity().x > largestVal)
            {
                largestVal = f.Hands[i].PalmPosition.ToUnity().x;
                h = f.Hands[i];
            }
        }
        return h;
    }


    // This script should capture the difference between large set number of frames;
    // e.g every fifth frame it gets the Player's hand position and rotations and
    // calculates the positive difference.
    //
    // The calculated maginitude is then applied as force to the boat object in the
    // scene either directly or indriectly (possible an invisible collider object to
    // simulate wind)
    //
    void FixedUpdate()
    {
        Frame frame = m_leapController.Frame();

        Hand leftHand = GetLeftMostHand(frame);
        Hand rightHand = GetRightMostHand(frame);
/*
        foreach (Hand hand in frame.Hands) {
            Debug.Log(hand);
        }
*/

        if (frame.Hands.Count >= 1 && leftHand.IsValid) {
            float speed = leftHand.PalmVelocity.Magnitude;

            Vector3 tempCamera = Camera.main.transform.forward;
            tempCamera.y = transform.position.y;
            Vector3 wind = tempCamera * speed / windDrag;

            if(speed >= 5.0f)
            {
                //Vector3 targetDir = tempCamera;
                Vector3 targetDir = tempCamera * speed;
                float step = 0.125f * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);
            }

            // something for two hands
            if (frame.Hands.Count >= 2)
            {
                wind = tempCamera * rightHand.PalmVelocity.Magnitude / windDrag;

                float roll = leftHand.PalmPosition.Roll;
                Debug.Log("Roll Ya'll " + roll);
                if (roll < -2.7f)
                {
                    cameraController.rotateLeft();
                }
                else if (roll > -2.5f)
                {
                    cameraController.rotateRight();
                }
            }

            rigidbody.AddRelativeForce(wind, ForceMode.Impulse);
        }
        else
        {
//            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = Input.mousePosition;
            Vector2 mousePosDiff = mousePos - mousePosLastframe;

            Vector3 tempCamera = Camera.main.transform.forward;
            tempCamera.y = transform.position.y;
            Vector3 wind = tempCamera * mousePosDiff.magnitude / windMouseDrag;

            rigidbody.AddRelativeForce(wind, ForceMode.Impulse);

            mousePosLastframe = mousePos;
        }
    }
}
