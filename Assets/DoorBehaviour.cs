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
        return new Vector3(exitDoor.transform.position.x, exitDoor.transform.position.y, exitDoor.transform.position.z);
    }
}
