using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private InventoryM inventory;
    public GameObject slotButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Playre").GetComponent<InventoryM>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Playre"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                }
            }
        }
    }
}
