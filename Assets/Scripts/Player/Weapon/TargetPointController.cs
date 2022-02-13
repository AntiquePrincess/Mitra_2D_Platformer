using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // по задумке, при зажатой правой кнопки мыши происходит прицеливание, но что-то пошло не так
    private void Move()
    {
        if (Input.GetButton("Fire2"))
        {
            var newPosition = new Vector3(transform.position.x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
            transform.position = newPosition;
        }
    }
    
}
