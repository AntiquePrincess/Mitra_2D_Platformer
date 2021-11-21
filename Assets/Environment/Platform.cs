using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Plyre_1"))
        {
            Invoke("FallPlatform", 1f);
            Destroy(gameObject, 2f);
        }
    }

    void FallPlatform()
    {
        rb.isKinematic = false;
    }
}
