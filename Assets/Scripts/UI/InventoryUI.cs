using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Slot[] ingredientSlots;
    public Transform ingredientSlotHolder;

    void Start()
    {
        ingredientSlots = ingredientSlotHolder.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem(Item _item, int _count)
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item != null)
            {
                if (ingredientSlots[i].item.ItemName == _item.ItemName)
                {
                    ingredientSlots[i].SetSlot(_count);
                    return;
                }
            }
        }

        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].item == null)
            {
                ingredientSlots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
