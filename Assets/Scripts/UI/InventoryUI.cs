using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Slot[] slots;
    public Transform slotHolder;

    void Awake()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }

    public void AcquireItem(Item _item, int _count)
    {
        for (int i = 0; i < slots.Length; i++)    //이미 있는 아이템이면 개수만 +1
        {
            if (slots[i].item != null && _item != null)
            {
                if (slots[i].item.ItemName == _item.ItemName)
                {
                    slots[i].SetSlot(_count);
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)    //빈 슬롯에 아이템 추가
        {
            if (_item != null)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item, _count);
                    slots[i].gameObject.SetActive(true);
                    return;
                }
            }
        }
    }
}
