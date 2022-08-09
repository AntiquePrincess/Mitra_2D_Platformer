using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryM : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventotry;
    private bool inventotryOn;

    private void Start()
    {
        inventotryOn = false;
    }

    public void Chest()
    {
        if (inventotryOn == false)
        {
            inventotryOn = true;
            inventotry.SetActive(true);
        }
        else if(inventotryOn == true)
        {
            inventotryOn = false;
            inventotry.SetActive(false);
        }

    }
}
