using UnityEngine;
using System.Collections;
using Leap;

/// <summary>
///
/// </summary>
public class LeapHandWave : MonoBehaviour
{

    Controller m_leapController;


    // Use this for initialization
    void Start()
    {
        m_leapController = new Controller();
        if (transform.parent == null)
        {
            Debug.LogError("LeapHandWave must have a parent object to control");
        }

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

        foreach (Hand hand in frame.Hands) {
            Debug.Log(hand);
        }

        if (frame.Hands.Count >= 2)
        {
            // takes the average vector of the forward vector of the hands, used for the
            // pitch of the plane.
            Vector3 avgPalmForward = (frame.Hands[0].Direction.ToUnity() + frame.Hands[1].Direction.ToUnity()) * 0.5f;

            Vector3 handDiff = leftHand.PalmPosition.ToUnityScaled() - rightHand.PalmPosition.ToUnityScaled();

            Vector3 newRot = transform.localRotation.eulerAngles;
            newRot.z = -handDiff.y * 20.0f;

            // adding the rot.z as a way to use banking (rolling) to turn.
            newRot.y += handDiff.z * 3.0f - newRot.z * 0.03f * transform.rigidbody.velocity.magnitude;
            newRot.x = -(avgPalmForward.y - 0.1f) * 100.0f;

            float forceMult = 20.0f;

            // if closed fist, then stop the plane and slowly go backwards.
            if (frame.Fingers.Count < 3)
            {
                forceMult = -3.0f;
            }

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(newRot), 0.1f);
            transform.rigidbody.velocity = transform.forward * forceMult;
        }

    }

}
