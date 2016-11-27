using UnityEngine;
using System.Collections;

public class NoteObject : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void readNote()
    {
        GameObject refg = GameObject.Find("TextController");
        TextController t = refg.GetComponent<TextController>();

        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.key_1 = true;


        t.textUpdate("What you seek is hidden in an object.\nLook closely at an object hard and sturdy. \n press enter");


    }
}
