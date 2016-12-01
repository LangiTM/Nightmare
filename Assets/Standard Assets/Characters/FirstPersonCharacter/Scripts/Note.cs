using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
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
        string text="";
        if (transform.name == "Riddle1")
        {
            text = "What you seek is hidden in an object.\nLook closely at an object hard and sturdy. \n click to exit";
        }
        else if (transform.name == "Riddle2")
        {
            text = "Beautiful music can be heard from inside.\nYou'll definetly need to look for a good book,\n and maybe a nice desk to read it at";
        }
        //Make it so the door is locked from the inside
        else if(transform.name == "Riddle3")
        {
            text = "This room is already unlocked for you,\n but a mystery may keep you from ever leaving";
        }
        //This key isnt being picked up :(
        else if (transform.name == "Riddle4")
        {
            text = "Haha you fool. You're trapped in here for good.\nMight as well go enjoy a nice drink at the bar.";
        }
        t.textUpdate(text);


    }
}
