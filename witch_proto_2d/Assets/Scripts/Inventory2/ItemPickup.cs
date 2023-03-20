using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        Debug.Log("Item picked up");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Pickup();
        }
    }
}
