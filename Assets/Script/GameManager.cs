using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    bool sinarioLoadChk = true;
    public int ScenarioNum;

    // Update is called once per frame
    void Update () {

        //시나리오를 불렀을 경우!
	    if(sinarioLoadChk)
        {
            sinarioLoadChk = false;
            Transform dialogparents = GameObject.Find("Canvas").GetComponent<Transform>();
            GameObject dialogboxprefeb = Resources.Load<GameObject>("prefebs/dialogbox");
            GameObject dialogUI = Instantiate(dialogboxprefeb) as GameObject;
            //GameObject dialogUI = (GameObject)GameObject.Instantiate(dialogboxprefeb);
            dialogUI.name = "dialogbox";
            
            //할당된 부모의 정보를 가져가지 않도록 하는 구문
            dialogUI.transform.SetParent(dialogparents,false);
        }
	}
}
