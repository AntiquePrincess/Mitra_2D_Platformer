using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public float lifeTime;
    public float damage;
    public float force;

    private Rigidbody2D _rigidbody;
    private bool _isShot;
    private bool _isHit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "tod")
        {
            _isHit = true;
        }
    }

    public void Shot()
    {
        _rigidbody.simulated = true;
        _rigidbody.AddForce(transform.right * force, ForceMode2D.Impulse);
        if (_isHit)
        {
            Destroy(gameObject);
        }
        else
            Destroy(gameObject, lifeTime);
        _isShot = true;
    }

    private void Rotate()
    {
        if (_isShot)
        {
            var direction = _rigidbody.velocity;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = quaternion.Euler(0, 0, angle);
        }
    }
}
