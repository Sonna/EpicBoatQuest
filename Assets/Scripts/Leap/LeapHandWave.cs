using UnityEngine;
using System.Collections;
using Leap;

/// <summary>
///
/// </summary>
public class LeapHandWave : MonoBehaviour
{

    Controller m_leapController;

    public AudioClip[] windNoises;

    private int particleSpawnRate = 3;
    private int windDrag;
    private int windMouseDrag;
    private Vector2 mousePosLastframe;
    private CameraController cameraController;
    private ParticleSystem particleSystem;

    private InteractiveCloth sailCloth;
    private AudioSource cameraAudioSource;

    // Use this for initialization
    void Start()
    {
        m_leapController = new Controller();
        windDrag = 5000;
        windMouseDrag = 1000;
        mousePosLastframe = Input.mousePosition;
        cameraController = Camera.main.GetComponent<CameraController>();
        particleSystem = Camera.main.GetComponentInChildren<ParticleSystem>();
        sailCloth = GetComponentInChildren<InteractiveCloth>();
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
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
    // The calculated magnitude is then applied as force to the boat object in the
    // scene either directly or indriectly (possible an invisible collider object to
    // simulate wind)
    //
    void FixedUpdate()
    {
        if(GameManager.Instance.gameEnd)
        {
            ProduceWind(0f);
            return;
        }

        float magnitude = 0f;
        Frame frame = m_leapController.Frame();

        Hand leftHand = GetLeftMostHand(frame);
        Hand rightHand = GetRightMostHand(frame);
        sailCloth.externalAcceleration = Vector3.zero;
/*
        foreach (Hand hand in frame.Hands) {
            Debug.Log(hand);
        }
*/

        if (frame.Hands.Count >= 1 && leftHand.IsValid) {
            magnitude = leftHand.PalmVelocity.Magnitude;

            Vector3 tempCamera = Camera.main.transform.forward;
            Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(tempCamera);
            localForward.y = transform.position.y;
            Vector3 wind = localForward * magnitude / windDrag;

            if(magnitude >= 5.0f)
            {
                Vector3 targetDir = localForward * magnitude;
                float step = 0.125f * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);
            }

            sailCloth.externalAcceleration = wind * 1000.0f;

            // something for two hands
            if (frame.Hands.Count >= 2)
            {
                wind = localForward * rightHand.PalmVelocity.Magnitude / windDrag;

                float roll = leftHand.PalmPosition.Roll;
                //Debug.Log("Roll Ya'll " + roll);
                if (roll < -2.7f)
                {
                    cameraController.rotateLeft();
                }
                else if (roll > -2.5f)
                {
                    cameraController.rotateRight();
                }
            }
          ProduceWind(magnitude/100);
          rigidbody.AddRelativeForce(wind, ForceMode.Impulse);
        }
        else
        {
//            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = Input.mousePosition;
            Vector2 mousePosDiff = mousePos - mousePosLastframe;

            magnitude = mousePosDiff.magnitude;
            magnitude = Mathf.Clamp(magnitude, 0f, 700f);

            Vector3 tempCamera = Camera.main.transform.forward;
            tempCamera.y = transform.position.y;
            Vector3 wind = tempCamera * magnitude / windMouseDrag;

            Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(tempCamera);
            localForward.y = transform.position.y;

            if(magnitude >= 5.0f)
            {
                Vector3 targetDir = localForward * magnitude;
                float step = 0.25f * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                transform.rotation = Quaternion.LookRotation(newDir);
            }

            rigidbody.AddRelativeForce(wind, ForceMode.Impulse);

            mousePosLastframe = mousePos;
            ProduceWind(magnitude/10);
        }


        Debug.Log("Magnitude = " + magnitude);

        // Play Wind noises based off magnitude

        if (magnitude >= 100)
        {
            cameraAudioSource.clip = windNoises[0];
        }

        if (!cameraAudioSource.isPlaying)
        {
            cameraAudioSource.Play();
        }

        if (magnitude <= 10)
        {
          cameraAudioSource.Stop();
        }
    }

    private void ProduceWind(float magnitude)
    {
        particleSystem.emissionRate = magnitude / particleSpawnRate;
    }
}
