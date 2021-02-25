using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Dictionary<string, object>> itemDB;

    void Start()
    {
        itemDB = CSVReader.Read("Datas/ItemData");
        Debug.Log("Count : " + itemDB.Count);
        for (var i = 0; i < itemDB.Count; i++)
        {
            print("ItemID " + itemDB[i]["ItemID"] + " " +
                   "ItemName " + itemDB[i]["ItemName"] + " " +
                   "ItemKind " + itemDB[i]["ItemKind"] + " " +
                   "Rarity " + itemDB[i]["Rarity"]);
        }
    }
}
