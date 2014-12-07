using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        GameManager.Instance.player = this.gameObject;
        GameManager.Instance.shipStartPosition = transform.position;
    }
}
