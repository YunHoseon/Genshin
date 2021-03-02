using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image imgBackground;
    public Image imgItem;
    public Image imgRarityStar;
    public int itemCount = 0;
    public ItemInfoBoard itemInfoBoard;

    public Text txtCount;

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
        //Debug.Log("_count : " + _count);
        item = _item;
        itemCount += _count;
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
        if (item != null)
        {
            imgItem.sprite = item.ItemImage;
            imgRarityStar.sprite = item.ItemStarImage;
            imgRarityStar.GetComponent<Image>().SetNativeSize();

            ChangeImageColorForRarity(imgBackground);
            imgItem.gameObject.SetActive(true);
            //this.gameObject.SetActive(true);
        }
    }

    private void ClearSlot()
    {
        item = null;
        imgItem = null;
        imgBackground = null;
        itemCount = 0;

        imgItem.gameObject.SetActive(false);
    }

    void ChangeImageColorForRarity(Image img)
    {
        switch (item.Rarity)
        {
            case 1:
                img.GetComponent<Image>().color = new Color32(99, 105, 107, 255);
                break;
            case 2:
                img.GetComponent<Image>().color = new Color32(83, 135, 112, 255);
                break;
            case 3:
                img.GetComponent<Image>().color = new Color32(81, 126, 154, 255);
                break;
            case 4:
                img.GetComponent<Image>().color = new Color32(136, 109, 164, 255);
                break;
            case 5:
                img.GetComponent<Image>().color = new Color32(182, 117, 61, 255);
                break;
        }
    }

    public void ClickSlot()
    {
        itemInfoBoard.UpdateInfo(item);
    }
}
