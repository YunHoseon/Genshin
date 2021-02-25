using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    private int slotCnt;

    void Start()
    {
        onSlotCountChange.Invoke(slotCnt);
    }
    
    void Update()
    {
        
    }
}
