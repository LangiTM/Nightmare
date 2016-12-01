using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;	// uGUIの機能を使うお約束

public class TextController : MonoBehaviour
{

    public string[] texts; // シナリオを格納する
    public Text uiText; // uiTextへの参照を保つ
    public Text condText;
    public  Boolean isShow;

    int currentLine = 0; // 現在の行番号

    void Start()
    {
       
    }
   
    public void textClear()
    {
        uiText.text = "";
        condText.text = "";
        
    }
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
            keys[1] = "\n        -RecRoom Key";
        }
        String text="You have following key"+keys[0]+keys[1]+keys[2] + keys[3] + keys[4] + keys[5];
        condText.text = text;
    }
    public void textUpdate(string text)
    {
         isShow = true;

        uiText.text = text;
  
    }

}