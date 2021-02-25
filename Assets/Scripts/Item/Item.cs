using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind
{
    Weapon = 0,
    Artifact,
    Ingredient,
    Nurture
}

public class Item : MonoBehaviour
{
    public ItemKind itemKind { get; set; }
    public int ItemID { get; set; } = 0;
    public string ItemName { get; set; } = null;
    public Sprite ItemImage { get; set; }
    public int Rarity { get; set; } = 0;

    public bool Use()
    {
        return false;
    }
}
