using UnityEngine;
using System.Collections;

public class AddClothRenderer : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        ClothRenderer cr = gameObject.AddComponent<ClothRenderer>();
        cr.material = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
