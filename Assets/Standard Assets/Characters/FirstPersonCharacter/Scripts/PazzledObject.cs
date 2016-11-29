using UnityEngine;
using System.Collections;

public class PazzledObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void findKey()
    {
        GameObject refg = GameObject.Find("TextController");
        TextController t = refg.GetComponent<TextController>();

        if(transform.name=="key_1")
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.key_1 = true;
        else if(transform.name=="key_2")
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.key_2 = true;


        t.textUpdate("You've found a key!! Go open the door. \n Click to exit");

        
    }
}
    

