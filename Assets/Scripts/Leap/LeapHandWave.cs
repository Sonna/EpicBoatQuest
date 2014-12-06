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

    // Use this for initialization
    void Start()
    {
        m_leapController = new Controller();
        if (transform.parent == null)
        {
            Debug.LogError("LeapHandWave must have a parent object to control");
        }
        windDrag = 35;
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
//        Hand rightHand = GetRightMostHand(frame);

        foreach (Hand hand in frame.Hands) {
            Debug.Log(hand);
        }

        if (frame.Hands.Count >= 1 && leftHand.IsValid) {
            Vector3 tempCamera = Camera.main.transform.forward;
            tempCamera.y = transform.position.y;
            Vector3 wind = tempCamera / windDrag;

            rigidbody.AddRelativeForce(wind, ForceMode.Impulse);
        }
//        if (frame.Hands.Count >= 2)
//        {
            // something for two hands
//        }
    }
}
