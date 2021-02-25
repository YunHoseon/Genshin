using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Slot[] slots;
    public Transform slotHolder;

    void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();

    }
    
    void Update()
    {
        
    }
}
