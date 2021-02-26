using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    private static ItemDatabase instance =  null;

    public static ItemDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("_ItemDatabase");
                instance = newGameObject.AddComponent<ItemDatabase>();
            }
            return instance;
        }
    }

    public List<Dictionary<string, object>> itemDB;
    private void Awake()
    {
        itemDB = CSVReader.Read("Datas/ItemData");
        /*for (var i = 0; i < itemDB.Count; i++)
        {
            print("ItemID " + itemDB[i]["ItemID"] + " " +
                   "ItemName " + itemDB[i]["ItemName"] + " " +
                   "ItemKind " + itemDB[i]["ItemKind"] + " " +
                   "Rarity " + itemDB[i]["Rarity"] + " " +
                   "Path " + itemDB[i]["Path"] + " " +
                   "Info " + itemDB[i]["Info"]);
        }*/
    }
}
