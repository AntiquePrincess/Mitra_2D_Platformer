using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Platform_1 : MonoBehaviour
{
    public bool MovingUp;
    public float Speed;
    public Transform Point;
    public int PositionOfPatrol;
    void Update()
    {
        if (transform.position.y > Point.position.y + PositionOfPatrol)
        {
            MovingUp = false;
        }
        else if (transform.position.y < Point.position.y - PositionOfPatrol)
        {
            MovingUp = true;
        }
        if (MovingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + Speed * Time.deltaTime );
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - Speed * Time.deltaTime);
        }
    }
 
}
