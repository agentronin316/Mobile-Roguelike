using UnityEngine;
using System.Collections;
using System.Xml;

public class LoadTiles : MonoBehaviour {

    public TextAsset[] mapInformation; //holds the .xml file

    public GameObject tempCube; //a temporary tile

    GameObject tileParent;

    Sprite[] sprites;

    void Start()
    {
        tileParent = new GameObject();
        LoadMap(1);
    }

	// Use this for initialization
	public void LoadMap (int index)
    {
        sprites = Resources.LoadAll<Sprite>("roguelikeSheet_transparent");
        Debug.Log(sprites.Length);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(mapInformation[index - 1].text);
        

        Camera.main.transform.position = new Vector3(9.9f, -9.9f, 10f);
        Camera.main.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        XmlNodeList layerNames = xmlDoc.GetElementsByTagName("layer");
        Debug.Log(layerNames.Count);

        foreach (XmlNode layerInfo in layerNames)
        {
            Debug.Log("name: " + layerInfo.Attributes.GetNamedItem("name").InnerText);
            Debug.Log("width: " + layerInfo.Attributes.GetNamedItem("width").InnerText);
            Debug.Log("height: " + layerInfo.Attributes.GetNamedItem("height").InnerText);
        }

        XmlNode tilesetInfo = xmlDoc.SelectSingleNode("map").SelectSingleNode("tileset");

        float tileWidth = int.Parse(tilesetInfo.Attributes.GetNamedItem("tilewidth").InnerText) / 100f;
        float tileHeight = int.Parse(tilesetInfo.Attributes.GetNamedItem("tileheight").InnerText) / 100f;

        foreach (XmlNode layerInfo in layerNames)
        {
            int layerWidth = int.Parse(layerInfo.Attributes.GetNamedItem("width").InnerText);
            int layerHeight = int.Parse(layerInfo.Attributes.GetNamedItem("height").InnerText);

            switch(layerInfo.Attributes.GetNamedItem("name").InnerText)
            {
                case "Obstacles":

                    goto default;
                case "Interactive":

                    goto default;
                default:
                    ParseLayer(layerInfo, layerWidth, layerHeight, tileWidth, tileHeight);
                    break;
            }
        }

    }

    void ParseLayer(XmlNode layerInfo, float layerWidth, float layerHeight, float tileWidth, float tileHeight)
    {
        XmlNode tempNode = layerInfo.SelectSingleNode("data");

        int mapLocVert, mapLocHoriz;
        mapLocHoriz = 0;
        mapLocVert = 0;
        
        foreach (XmlNode tile in tempNode.SelectNodes("tile"))
        {
            int spriteValue = int.Parse(tile.Attributes.GetNamedItem("gid").InnerText);

            if (spriteValue > 0)
            {
                GameObject tempSprite = new GameObject("test");
                SpriteRenderer renderer = tempSprite.AddComponent<SpriteRenderer>();
                float locationX = tileWidth * mapLocHoriz;
                float locationY = tileHeight * mapLocVert;
                tempSprite.transform.position = new Vector3(locationX, locationY, 0);
                renderer.sprite = sprites[spriteValue - 1];

                renderer.sortingLayerName = layerInfo.Attributes.GetNamedItem("name").InnerText;
            }
            mapLocHoriz++;

            //if Typewriter goes "Ding!",
            if (mapLocHoriz >= layerWidth)
            {
                //Time to go "Ka-chunk!"
                mapLocHoriz = 0;
                mapLocVert--;
            }

        }
    }
}
