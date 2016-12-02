using UnityEngine;
using System.Collections;

public class ShowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        if (Cursor.visible == false)
            Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.None;
        if (Cursor.visible == false)
            Cursor.visible = true;
	}
}
