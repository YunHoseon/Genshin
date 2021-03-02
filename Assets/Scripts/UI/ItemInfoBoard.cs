using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoBoard : MonoBehaviour
{
    public Item item;
    public Image imgItemNameBoard;
    public Text txtItemName;
    public Image imgBackBoard;
    public Image imgItem;
    public Text txtItemInfo;

    void Start()
    {
        InitBoard();
    }

    void InitBoard()
    {
        if(item == null)
        {
            txtItemName.gameObject.SetActive(false);
            imgItem.gameObject.SetActive(false);
            txtItemInfo.gameObject.SetActive(false);
        }
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

    public void UpdateInfo(Item _item)
    {
        item = _item;
        txtItemName.text = _item.ItemName;
        ChangeImageColorForRarity(imgBackBoard);
        imgItem.sprite = _item.ItemImage;
        txtItemInfo.text = _item.Info;

        txtItemName.gameObject.SetActive(true);
        imgItem.gameObject.SetActive(true);
        txtItemInfo.gameObject.SetActive(true);
    }
}
