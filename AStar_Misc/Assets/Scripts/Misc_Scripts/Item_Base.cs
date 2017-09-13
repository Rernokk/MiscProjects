using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Item_Base : SerializedScriptableObject {
    [SerializeField]
    string m_Name;
    [SerializeField]
    string m_Slot;

    public Item_Base (string name, string slot)
    {
        m_Name = name;
        m_Slot = slot;
    }

    public string GetName()
    {
        return m_Name;
    }
    
    public string GetSlot()
    {
        return m_Slot;
    }
}
