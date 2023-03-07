using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item 
{
    public enum ItemType
    {
        Item1,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Item1: return ItemAssets.Instance.Item1;
        }
    }
}
