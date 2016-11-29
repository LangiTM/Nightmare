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
        if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.key_1)
        {
            keys[0] = "\n        -Key1";
        }
        if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.key_2)
        {
            keys[1] = "\n        -Key2";
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