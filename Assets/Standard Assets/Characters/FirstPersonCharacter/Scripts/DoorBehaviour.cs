using UnityEngine;
using System.Collections;

public class DoorBehaviour : MonoBehaviour {
    
    public GameObject exitDoor;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    

    public Vector3 getExitDoorPosition() {
        float offsetX;
        if (exitDoor.transform.localPosition.x > 0)
            offsetX = -4;
        else
            offsetX = 4;
        Vector3 exitPosition = new Vector3(exitDoor.transform.localPosition.x + offsetX, exitDoor.transform.localPosition.y, exitDoor.transform.localPosition.z);
        Debug.Log("Local: " + exitPosition);
        exitPosition = exitDoor.transform.parent.TransformPoint(exitPosition);
        Debug.Log("World: " + exitPosition);
        return exitPosition;
    }
}
