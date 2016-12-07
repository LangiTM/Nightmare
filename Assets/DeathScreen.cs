using UnityEngine;
using System.Collections;

public class DeathScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().enabled = true;
        GameObject.Find("FPSController").GetComponentInChildren<Camera>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
