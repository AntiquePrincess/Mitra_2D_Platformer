using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TargetPointController : MonoBehaviour
{
    private float _rotateZ;

    public float offset;
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

        /*
         Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        */

      
         
        var newPosition = new Vector3(transform.position.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
        transform.position = newPosition;
       
    }
    
}
