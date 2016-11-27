using UnityEngine;
using System.Collections;

public class PazzledObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider collide)
    {
        GameObject refg = GameObject.Find("TextController");
        TextController t = refg.GetComponent<TextController>();

        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.key_1 = true;

       
         t.textUpdate("You've found a key!! Go open the door. \n press enter");

        
    }
}
    

