﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogPrint : MonoBehaviour
{
    //DialogXMLData DXD = new DialogXMLData();
    Image printBack, printChar;
    Text printName, printText;
    DialogXMLData DXD;

    //-------------테스트용-------------------
    //public List<string> dialog = new List<string>();
    public string[] dialogstr_background;
    public string[] dialogstr_charnum; 
    public string[] dialogstr_charname;
    public string[] dialogstr_text;
    public bool dialogSkipChk = false;
    public bool init = true;
    public bool play = true;
    
    //-------------테스트용-------------------

    //철자 형태로 저장하는곳
    List<char> words = new List<char>();

    void Awake()
    {
        printBack = GameObject.Find("dialog_background").GetComponent<Image>();
        printChar = GameObject.Find("dialog_char").GetComponent<Image>(); 
        printName = GameObject.Find("dialog_name").GetComponent<Text>();
        printText = GameObject.Find("dialog_text").GetComponent<Text>();
        DXD = this.GetComponent<DialogXMLData>();
    }

    void Update()
    {
        if (init)
        {
            //초기화를 제어 -> GM에서 제어할 예정
            init = false;
            dialogstr_background = DXD.backgroundImgIndex.ToArray();
            dialogstr_charnum = DXD.charNumber.ToArray();
            dialogstr_charname = DXD.charName.ToArray();
            dialogstr_text = DXD.text.ToArray();
        }
        if(play)
        {
            //플레이를 제어 -> Gm에서 제어할 예정
            play = false;
            //코루틴을 이용해서 다이어로그 프린트함
            StartCoroutine(Dialog(3.0f,0.1f));
        }
    }
    // 다이얼로그를 1초마다 넘김
    IEnumerator Dialog(float time,float spliteTime)
    {
        //뒤에서 대기시간을 10번을 나눠서 불러오기 때문에 10번을 나눠주는 작업
        time = time / 10;
        for (int i = 0; i < dialogstr_text.Length; i++)
        {
            //기존에 있던 워드는 제거
            words.Clear();
            
            //배경,캐릭터 불러오기
            printBack.sprite = Resources.Load<Sprite>("Image/BackGround/"+dialogstr_background[i]);
            printChar.sprite = Resources.Load<Sprite>("Image/Char/" + dialogstr_charnum[i]);

            foreach (char word in dialogstr_text[i])
            {
                words.Add(word);
            }
            printName.text = dialogstr_charname[i];
            printText.text = null;
            
            //텍스트를 순차적으로 출력하는 기능
            for (int j = 0; j < words.Count; j++)
            {
                printText.text += words[j];
               
                //순차적 출력
                if (!dialogSkipChk)
                {
                    yield return new WaitForSeconds(spliteTime);
                } 
            }
            
            //스킵 브레이크 역할
            dialogSkipChk = false; 
           
            // 다음대화로 스킵 (10번에 나눠서 불러옴)
            for (int k = 0; k < 10; k++)
            {
                if (!dialogSkipChk)
                {
                    yield return new WaitForSeconds(time);
                }
            }
            
            //스킵 브레이크 역할
            dialogSkipChk = false; 
        }
    }
    
    // 클릭시 스킵하는 부분
    void OnMouseDown()
    {
        dialogSkipChk = true; 
    }


}

