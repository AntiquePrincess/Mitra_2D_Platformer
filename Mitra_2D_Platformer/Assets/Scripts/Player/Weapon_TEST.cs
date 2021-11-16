using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_TEST : MonoBehaviour
{

    public bool hold;
    public Transform holdPoint;

    void Start()
    {
        
    }

    void Update()
    {
        Cam();
    }

    private void Cam()
    {
        if (hold)
        {
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!hold)
                {
                    hold = true;
                }
                else
                {
                    hold = false;
                }
            }
        }
    }
}
