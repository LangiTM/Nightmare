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
            text = "Beautiful music. You definetly need a good book too";

        t.textUpdate(text);


    }
}
