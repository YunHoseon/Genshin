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
    private ItemKind itemKind;
    private int rarity = 0;
    //이미지

    public ItemKind GetItemKind() { return itemKind; }
    public void SetItemKind(ItemKind kind) { itemKind = kind; }

    public int GetRarity() { return rarity; }
    public void SetRarity(int rarity_) { rarity = rarity_; }
}
