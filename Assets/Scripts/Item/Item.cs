using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind : int
{
    Weapon = 0,
    Artifact,
    Ingredient,
    Nurture
}

public class Item : MonoBehaviour
{
    public ItemKind? itemKind { get; set; }
    public int ItemID { get; set; } = 0;
    public string ItemName { get; set; } = null;
    public Sprite ItemImage { get; set; }
    public int Rarity { get; set; } = 0;
    public string Info { get; set; }
    public string ImagePath { get; set; } = null;
    
    void Start()
    {
        ItemName = this.gameObject.name;
        SetData(ItemName);
        SetData(ItemID);
    }

    public bool Use()
    {
        return false;
    }

    void SetData(string _name)
    {
        for(int i = 0; i < ItemDatabase.Instance.itemDB.Count; i++)
        {
            if (_name == ItemDatabase.Instance.itemDB[i]["ItemName"] as string)
            {
                ItemID = i + 1;
                break;
            }
        }
    }

    public void SetData(int _id)
    {
        itemKind = (ItemKind)Enum.Parse(typeof(ItemKind), ItemDatabase.Instance.itemDB[_id - 1]["ItemKind"].ToString());
        Rarity = (int)ItemDatabase.Instance.itemDB[_id - 1]["Rarity"];
        ImagePath = ItemDatabase.Instance.itemDB[_id - 1]["Path"] as string;
        Info = ItemDatabase.Instance.itemDB[_id - 1]["Info"] as string;

        ItemImage = Resources.Load<Sprite>(ImagePath);
    }
}
