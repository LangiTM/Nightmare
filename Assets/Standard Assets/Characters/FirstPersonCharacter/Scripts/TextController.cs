using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;	// uGUIの機能を使うお約束

public class TextController : MonoBehaviour
{

    public string[] texts; // シナリオを格納する
    public Text uiText; // uiTextへの参照を保つ
    public  Boolean isShow;

    int currentLine = 0; // 現在の行番号

    void Start()
    {
       
    }
   
    public void textClear()
    {
        uiText.text = "";
        
    }
    public void textUpdate(string text)
    {
         isShow = true;

        uiText.text = text;
  
    }

}