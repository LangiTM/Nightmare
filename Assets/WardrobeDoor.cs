using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class WardrobeDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Camera>().enabled = false;
            GameObject.Find("FPSController").GetComponentInChildren<Camera>().enabled = true;
        }

    }

}
