using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.name.Equals("BackToMain"))
        {
            if (Input.GetMouseButtonDown(0))
                Application.LoadLevel("MainMenu");
        }
	}

    void OnMouseDown()
    {
        Application.LoadLevel("Outside");
    }
}
