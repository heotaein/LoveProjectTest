using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogPrint : MonoBehaviour
{
    //DialogXMLData DXD = new DialogXMLData();
    GameManager GM = new GameManager();
    Text tempText;
    DialogXMLData DXD;

    //-------------테스트용-------------------
    //public List<string> dialog = new List<string>();
    public string[] dialog;
    
    public bool init = true;
    bool play = true;
    
    //-------------테스트용-------------------
    


    void Awake()
    {
        tempText = this.GetComponent<Text>();
        DXD = this.GetComponentInParent<DialogXMLData>();
    }

    void Update()
    {
        if (init)
        {
            Debug.Log(DXD.text.Count);
            //초기화를 제어 -> GM에서 제어할 예정
            init = false;
            dialog = DXD.textArr;
            

        }
        if(play)
        {
            //플레이를 제어 -> Gm에서 제어할 예정
            play = false;
            //코루틴을 이용해서 다이어로그 프린트함
            StartCoroutine(Dialog(1.0f));
        }
    }
    // 다이얼로그를 1초마다 넘김
    IEnumerator Dialog(float time)
    {
        for (int i = 0; i < dialog.Length; i++)
        {
            tempText.text = dialog[i];
            yield return new WaitForSeconds(time);
        }
    }


}

