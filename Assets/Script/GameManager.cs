using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    bool sinarioLoadChk = true;
    public int ScenarioNum;

    // Update is called once per frame
    void Update () {
	    if(sinarioLoadChk)
        {
            sinarioLoadChk = false;
            Transform dialogparents = GameObject.Find("Canvas").GetComponent<Transform>();
            GameObject dialogboxprefeb = Resources.Load<GameObject>("prefebs/dialogbox");
            GameObject dialogUI = Instantiate(dialogboxprefeb, dialogboxprefeb.transform.position, Quaternion.identity) as GameObject;
            dialogUI.name = "dialogUI";
            dialogUI.transform.parent = dialogparents;
            //dialogUI.
        }
	}
}
