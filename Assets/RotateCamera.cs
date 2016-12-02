using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 0.05f);
	}
}
