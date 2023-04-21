using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            //Pickup() // kunne ikke se en grund til at det her var en method... - Victor

            Debug.Log("Item picked up");
            Destroy(gameObject);
            //InventoryManager.Instance.Add(Item);
        }
    }
}
