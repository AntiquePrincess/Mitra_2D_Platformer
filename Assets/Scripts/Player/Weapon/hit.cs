using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public Gun hitgun;
    public bool hit_m;

    void Start()
    {
    }

    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Arm"))
        {
            if (Input.GetKey(KeyCode.E) && hitgun.hit == false)
            {

                hitgun.hit = true;
                Destroy(gameObject);
            }               
        }
    }
}
