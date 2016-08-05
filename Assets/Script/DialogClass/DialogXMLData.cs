using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DialogXMLData : MonoBehaviour
{
    //GM 할당하기
    GameManager GM;
    DialogPrint DP;

    public string _FileName = "DialogDataXML";
    //public TextAsset _ExpXmlFile;
    public List<string> backgroundImgIndex = new List<string>();
    public List<string> charNumber = new List<string>();
    public List<string> charName = new List<string>();
    public List<string> text = new List<string>();

    //시나리오 넘버를 받아올 변수 생성
    string dialogScenarioNum;

    public bool awakechk = true;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        DP = GetComponentInChildren<DialogPrint>();
    }

    void Update()
    {
        if (awakechk)
        {
            awakechk = false;
            dialogScenarioNum = GM.ScenarioNum.ToString();
            Xml_Load(_FileName);
            Debug.Log(dialogScenarioNum);
        }
    }

    void Xml_Load(string Filename)
    {
        //할당하기 전 초기화
        charName.Clear();
        text.Clear();
        //xml생성
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + Filename);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        // 전체 가져오기
        XmlNodeList nodes = xmldoc.SelectNodes("DialogSet/Line");

        // 데이터 가져오기! 함수
        foreach (XmlNode node in nodes)
        {
            if (node.SelectSingleNode("ScenarioNum").InnerText == dialogScenarioNum)
            {
                backgroundImgIndex.Add(node.SelectSingleNode("BackgroundImgIndex").InnerText);
                charNumber.Add(node.SelectSingleNode("CharNum").InnerText);
                charName.Add(node.SelectSingleNode("CharName").InnerText);
                text.Add(node.SelectSingleNode("Text").InnerText);
            }
        }

        //프린트하는 창에 넘기기
        DP.init = true;
        DP.play = true;
    }
}
