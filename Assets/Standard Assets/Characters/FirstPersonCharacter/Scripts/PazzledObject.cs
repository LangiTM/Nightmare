/**
 * This script manages the object interaction. 
 * This script is mainly attached with key objects.
 * When player interact with keys, this script will 
 * access to player information.
 * 
 * Author: Team Nightmare 
 * */

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

        if (transform.name == "key_1")
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.WestHall_Key = true;
        }
        else if (transform.name == "key_2")
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.MusicRoom_Key = true;
        }
        else if (transform.name == "key_3")
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.RecRoom_Key = true;
        }
        else if (transform.name == "ChinaCabinet")
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.Dining_Key = true;
        }
        else if (transform.name == "Crate (2)")
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.MusEsc_Key = true;
        }
        else if (transform.name == "Pot (6)")
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.Pantry_Key = true;
        }
        else if (transform.name == "Barrel (19)")
        {
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController.PantryEsc_Key = true;
        }


        t.textUpdate("You've found a key! I wonder what door it opens...");

        
    }
}
    

