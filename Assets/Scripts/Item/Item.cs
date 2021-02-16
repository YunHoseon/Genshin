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
    private int itemID = 0;
    private string itemName = null;
    private ItemKind itemKind;
    private int rarity = 0;
    //이미지

    public int GetItemID() { return itemID; }
    public void SetItemID(int id) { itemID = id;}

    public string GetItemName() { return itemName; }
    public void SetItemName(string name) { itemName = name; }

    public ItemKind GetItemKind() { return itemKind; }
    public void SetItemKind(ItemKind kind) { itemKind = kind; }

    public int GetRarity() { return rarity; }
    public void SetRarity(int rarity_) { rarity = rarity_; }
}
