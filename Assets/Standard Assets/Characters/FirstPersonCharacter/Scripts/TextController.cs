/**
 * Script for managing pop up text system. 
 * When this script gets called, it will update the text UI 
 * to show desired text to player.
 * 
 * Author: Team Nightmare
 * */



using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{

    public Text uiText;
    public Text condText;

    int currentLine = 0; // 現在の行番号

    void Start()
    {
       
    }

   //for clearing the text on the screen
    public void textClear()
    {
        uiText.text = "";
        condText.text = "";
        
    }
    //for showing players current condition 
    public void condUpdate()
    {
        String []keys=new String[6];

        //check the players status
        if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.WestHall_Key)
        {
            keys[0] = "\n        -West Hall Key";
        }
        if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.MusicRoom_Key)
        {
            keys[1] = "\n        -Music Room Key";
        }
        if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.RecRoom_Key)
        {
            keys[3] = "\n        -RecRoom Key";
        }
        String text="You have following key"+keys[0]+keys[1]+keys[2] + keys[3] + keys[4] + keys[5];
        condText.text = text;
    }
    public void textUpdate(string text)
    {

        uiText.text = text;
  
    }

}