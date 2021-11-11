using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePoint : MonoBehaviour
{
    public Transform holdPoint;
    bool dafolt;
    bool arm;
    bool flay;
    Rigidbody2D myRig;

    void Start()
    {
        myRig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }
    public void PickUp()
    {
        transform.SetParent(holdPoint);
        transform.position = holdPoint.position;
        transform.rotation = holdPoint.rotation;
        //myRig.
    }
}
