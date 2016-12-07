using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Retry : MonoBehaviour {
    public string level;

	// Use this for initialization
	void Start () {
        level = FirstPersonController.currLevel;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseDown()
    {
        //level = GameObject.Find("FPSController").GetComponent<FirstPersonController>().currLevel;
        Debug.Log(level);
        Application.LoadLevel(level);
    }
}
