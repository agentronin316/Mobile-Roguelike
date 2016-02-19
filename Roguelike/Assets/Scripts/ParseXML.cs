using UnityEngine;
using System.Collections;
using System.Xml;

public class ParseXML : MonoBehaviour {


	// Use this for initialization
	void Start ()
    {
        TextAsset xmlFile = Resources.Load("CDCatalog") as TextAsset;
	    
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(xmlFile.text);

        //Debug.Log(xmlDoc.DocumentElement.Name);
        //Debug.Log(xmlDoc.DocumentElement.GetAttribute("title"));
        //Debug.Log(xmlDoc.DocumentElement.FirstChild.Name);
        //Debug.Log(xmlDoc.DocumentElement.FirstChild.FirstChild.InnerText);

        foreach (XmlNode child in xmlDoc.DocumentElement.ChildNodes)
        {
            int year = System.Convert.ToInt32(child.LastChild.InnerText);
            if (year > 2000)
            {
                Debug.Log("SKIPPED");
            }
            else if (year > 1990)
            {
                string toOutput = "";
                foreach (XmlNode node in child.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "TITLE":
                            toOutput += (" Title: " + node.InnerText);
                            break;
                        case "ARTIST":
                            toOutput += (" Artist: " + node.InnerText);
                            break;
                        case "COUNTRY":
                            toOutput += (" Country: " + node.InnerText);
                            break;
                        case "COMPANY":
                            toOutput += (" Company: " + node.InnerText);
                            break;

                    }
                }
                Debug.Log(toOutput);
            }
            else if (year > 1980)
            {
                string toOutput = "";
                foreach (XmlNode node in child.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "TITLE":
                            toOutput += (" Title: " + node.InnerText);
                            break;
                        case "ARTIST":
                            toOutput += (" Artist: " + node.InnerText);
                            break;
                    }
                }
                Debug.Log(toOutput);
            }
            
        }

    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
