using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DialogXMLData : MonoBehaviour
{

    public string _FileName = "DialogDataXML";
    //public TextAsset _ExpXmlFile;
    public List<string> charName = new List<string>();
    public List<string> text = new List<string>();

    public string[] textArr;

    void Awake()
    {
        /*
        GM에서 불러와야 하는 데이터만 가져와서 프린트 해야함으로
        GM에 신 넘버가 필요함   
        */
        
        Xml_Load(_FileName);
    }

    void Xml_Load(string Filename)
    {
        //xml생성
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + Filename);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        // 전체 가져오기
        XmlNodeList nodes = xmldoc.SelectNodes("DialogSet/Line");

        // 데이터 가져오기! 함수
        foreach (XmlNode node in nodes)
        {
            //Debug.Log(Convert.ToInt16(node.Attributes.SetNamedItem("Levelnum").Value));
            charName.Add(node.SelectSingleNode("CharName").InnerText);
            text.Add(node.SelectSingleNode("Text").InnerText);
            //.InnerText
        }
        textArr = text.ToArray();
    }
}
