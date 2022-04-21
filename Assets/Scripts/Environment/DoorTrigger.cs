using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class DoorTrigger : MonoBehaviour
{
    public Door door;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            door.Open();
        }
        else if (collision.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            door.Close();
        }
    }
    
}
