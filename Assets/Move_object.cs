using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
	void Update() {
		transform.Translate(Vector3.up * Time.deltaTime, Space.World);
	}
}