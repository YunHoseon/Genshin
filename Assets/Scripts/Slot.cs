using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image imgItem;
    public int itemCount;

    [SerializeField]
    private Text txtCount;

    void Start()
    {
        UpdateSlotUI();
    }

    void Update()
    {
        if (itemCount <= 0)
            this.gameObject.SetActive(false);
        else
            UpdateSlotUI();
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        imgItem.sprite = item.ItemImage;

        imgItem.gameObject.SetActive(true);
        txtCount.text = itemCount.ToString("N0");
    }

    public void SetSlot(int _count)
    {
        itemCount += _count;
        txtCount.text = itemCount.ToString("N0");

        if (itemCount <= 0)
            ClearSlot();
    }

    public void UpdateSlotUI()
    {
        imgItem.sprite = item.ItemImage;
        imgItem.gameObject.SetActive(true);
    }

    private void ClearSlot()
    {
        item = null;
        imgItem = null;
        itemCount = 0;

        imgItem.gameObject.SetActive(false);
    }
}
