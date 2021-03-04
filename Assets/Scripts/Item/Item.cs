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

    public Sprite ItemStarImage { get; set; }

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
        for(int i = 0; i < ItemDatabase.ItemDBInstance.itemDB.Count; i++)
        {
            if (_name == ItemDatabase.ItemDBInstance.itemDB[i]["ItemName"] as string)
            {
                ItemID = i + 1;
                break;
            }
        }
    }

    public void SetData(int _id)
    {
        itemKind = (ItemKind)Enum.Parse(typeof(ItemKind), ItemDatabase.ItemDBInstance.itemDB[_id - 1]["ItemKind"].ToString());
        Rarity = (int)ItemDatabase.ItemDBInstance.itemDB[_id - 1]["Rarity"];
        ImagePath = ItemDatabase.ItemDBInstance.itemDB[_id - 1]["Path"] as string;
        Info = ItemDatabase.ItemDBInstance.itemDB[_id - 1]["Info"] as string;

        ItemImage = Resources.Load<Sprite>(ImagePath);

        switch(Rarity)
        {
            case 1:
                ItemStarImage = Resources.Load<Sprite>("Texture/Icon_1_Star");
                break;
            case 2:
                ItemStarImage = Resources.Load<Sprite>("Texture/Icon_2_Stars");
                break;
            case 3:
                ItemStarImage = Resources.Load<Sprite>("Texture/Icon_3_Stars");
                break;
            case 4:
                ItemStarImage = Resources.Load<Sprite>("Texture/Icon_4_Stars");
                break;
            case 5:
                ItemStarImage = Resources.Load<Sprite>("Texture/Icon_5_Stars");
                break;
        }
    }
}
