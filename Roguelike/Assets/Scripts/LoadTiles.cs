using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public enum InteractiveProperties
{
    STAIRS_UP,
    STAIRS_DOWN,
    SPAWNER
}

public class LoadTiles : MonoBehaviour {

    public TextAsset[] mapInformation; //holds the .xml file

    //public GameObject tempCube; //a temporary tile

    public List<InteractiveProperties> properties = new List<InteractiveProperties>();
    public List<int> tileIDs = new List<int>();

    GameObject mapParent;

    Sprite[] sprites;

    void Start()
    {
        mapParent = new GameObject("map");
        LoadMap(1);
    }

	// Use this for initialization
	public void LoadMap (int index)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(mapInformation[index - 1].text);

        XmlNodeList tempNodes = xmlDoc.GetElementsByTagName("tileset");
        
        //Debug.Log(tempNodes[0].Attributes.GetNamedItem("name").Value);
        sprites = Resources.LoadAll<Sprite>(tempNodes[0].Attributes.GetNamedItem("name").Value);
        //Debug.Log(sprites.Length);

        tempNodes = tempNodes[0].ChildNodes;
        foreach (XmlNode node in tempNodes)
        {
            if (node.Name == "tile")
            {
                XmlNode propertyNode = node.FirstChild.FirstChild;
                while (propertyNode != null)
                {
                    if(propertyNode.Attributes.GetNamedItem("name").Value.ToUpper() == "STAIRSUP")
                    {
                        properties.Add(InteractiveProperties.STAIRS_UP);
                    }
                    else if(propertyNode.Attributes.GetNamedItem("name").Value.ToUpper() == "STAIRSDOWN")
                    {
                        properties.Add(InteractiveProperties.STAIRS_DOWN);
                    }
                    else if(propertyNode.Attributes.GetNamedItem("name").Value.ToUpper() == "SPAWNER")
                    {
                        properties.Add(InteractiveProperties.SPAWNER);
                    }
                    tileIDs.Add(int.Parse(node.Attributes.GetNamedItem("id").Value) + 1);
                    propertyNode = propertyNode.NextSibling;
                }
            }
        }


        //Camera.main.transform.position = new Vector3(9.9f, -9.9f, 10f);
        //Camera.main.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        XmlNodeList layerNames = xmlDoc.GetElementsByTagName("layer");
        //Debug.Log(layerNames.Count);

        //foreach (XmlNode layerInfo in layerNames)
        //{
        //    Debug.Log("name: " + layerInfo.Attributes.GetNamedItem("name").InnerText);
        //    Debug.Log("width: " + layerInfo.Attributes.GetNamedItem("width").InnerText);
        //    Debug.Log("height: " + layerInfo.Attributes.GetNamedItem("height").InnerText);
        //}

        XmlNode tilesetInfo = xmlDoc.SelectSingleNode("map").SelectSingleNode("tileset");

        float tileWidth = int.Parse(tilesetInfo.Attributes.GetNamedItem("tilewidth").InnerText) / 100f;
        float tileHeight = int.Parse(tilesetInfo.Attributes.GetNamedItem("tileheight").InnerText) / 100f;

        foreach (XmlNode layerInfo in layerNames)
        {
            int layerWidth = int.Parse(layerInfo.Attributes.GetNamedItem("width").InnerText);
            int layerHeight = int.Parse(layerInfo.Attributes.GetNamedItem("height").InnerText);

            ParseLayer(layerInfo, layerWidth, layerHeight, tileWidth, tileHeight);
            
        }

    }

    void ParseLayer(XmlNode layerInfo, float layerWidth, float layerHeight, float tileWidth, float tileHeight)
    {
        XmlNode tempNode = layerInfo.SelectSingleNode("data");
        GameObject layerParent = new GameObject(layerInfo.Attributes.GetNamedItem("name").Value);
        layerParent.transform.parent = mapParent.transform;
        int mapLocVert, mapLocHoriz;
        mapLocHoriz = 0;
        mapLocVert = 0;
        
        foreach (XmlNode tile in tempNode.SelectNodes("tile"))
        {
            int spriteValue = int.Parse(tile.Attributes.GetNamedItem("gid").InnerText);

            if (spriteValue > 0)
            {
                string spriteName = string.Format("{0} {1} {2}", layerInfo.Attributes.GetNamedItem("name").InnerText, mapLocHoriz, mapLocVert);
                GameObject tempSprite = new GameObject(spriteName);
                tempSprite.transform.parent = layerParent.transform;
                SpriteRenderer renderer = tempSprite.AddComponent<SpriteRenderer>();
                float locationX = tileWidth * mapLocHoriz;
                float locationY = tileHeight * mapLocVert;
                tempSprite.transform.position = new Vector3(locationX, locationY, 0);
                renderer.sprite = sprites[spriteValue - 1];

                renderer.sortingLayerName = layerInfo.Attributes.GetNamedItem("name").InnerText;
                switch (layerInfo.Attributes.GetNamedItem("name").InnerText)
                {
                    case "Obstacles": //Set Obstacle layer tiles to have a collider and a rigidbody
                        tempSprite.AddComponent<BoxCollider2D>();
                        tempSprite.AddComponent<Rigidbody2D>();
                        Rigidbody2D tempRB = tempSprite.GetComponent<Rigidbody2D>();
                        tempRB.gravityScale = 0f;
                        tempRB.constraints = RigidbodyConstraints2D.FreezeAll;
                        break;
                    case "Interactive": //Set interactive layer tiles to have a trigger collider
                        tempSprite.AddComponent<BoxCollider2D>();
                        tempSprite.GetComponent<BoxCollider2D>().isTrigger = true;
                        //Insert enum check for properties
                        if(tileIDs.Contains(spriteValue))
                        {
                            tempSprite.AddComponent<InteractionDataScript>();
                            InteractionDataScript interactions = tempSprite.GetComponent<InteractionDataScript>();
                            List<int> IDList = tileIDs.FindAll(delegate (int i) { return i == spriteValue; });
                            int index = tileIDs.FindIndex(delegate (int i) { return i == spriteValue; });
                            foreach (int i in IDList)
                            {
                                Debug.Log("ID: " + i);
                                interactions.properties.Add(properties[index]);
                                index++;
                            }
                            string[] tempStrings = new string[interactions.properties.Count];
                            for (int i = 0; i < tempStrings.Length; i++)
                            {
                                tempStrings[i] = interactions.properties[i].ToString();
                            }
                            Debug.Log(string.Join(",", tempStrings ));
                        }
                        break;
                }
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
