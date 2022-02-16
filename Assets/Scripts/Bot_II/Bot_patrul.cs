using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_patrul : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private UnityEngine.Object explosion;

    public int positionOfPatrol;
    public Transform point;
    bool moveingRight;

    Transform Playre;
    public float stoppingDistance;
    private float stoppingDistanceSlep;

    bool chill = false;
    bool angry = false;
    bool goBack = false;
    bool Dist = false;

    //Жизни
    private int health = 1;
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    
    //jump
    public float distance = 1f;
    RaycastHit2D hit;
    public int jump = 5;

    void Start()
    {

        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;

        explosion = Resources.Load("Explosion");

        Physics2D.queriesStartInColliders = false;
        rb = GetComponent<Rigidbody2D>();
        Playre = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    void Update()
    {
        if (transform.rotation.z > 90 || transform.rotation.z < -90)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Jump();

        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false && !Dist)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, Playre.position) < stoppingDistance && Dist == false) // Можно еще подумать
        {

            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, Playre.position) > stoppingDistance && !Dist)
        {
            goBack = true;
            angry = false;
        }
        if(chill == true)
        {
            Chill();
        }
        else if(angry == true)
        {
            Angry();
        }
        else if(goBack == true)
        {
            GoBack();
        }
        else
        {
            DistF();
        }
    }

    void Jump()
    {
        if (moveingRight == true)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
            if (hit.collider != null && hit.collider.tag == "Ground")
            {
                rb.velocity = Vector2.up * jump;
            }
        }
        else if (moveingRight == false)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance);
            if (hit.collider != null && hit.collider.tag == "Ground") //&& hit.collider.tag == "Ground"
            {
                rb.velocity = Vector2.up * jump;
            }
        }
    }

    void Chill()
    {
         if(transform.position.x > point.position.x + positionOfPatrol)
        {
            moveingRight = false;
        }
         else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            moveingRight = true;
        }

        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, Playre.position, speed * Time.deltaTime);
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }

    void DistF()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y);// хренова работает
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Weapon"))
        {
            health--;
            spriteRend.material= matBlink;

            if( health <= 0)
            {
                Dist = true;
                Invoke("KillEnemy", 1f);
            }
            else
            {
                Invoke("ResetMaterial", 0.2f);
            }
        }

    }
    void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }
    void KillEnemy()
    {
        GameObject explosionRef = (GameObject)Instantiate(explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }

    private void OnTriggerEnter2D(Collider collision)
    {
        if (collision.tag == "Weapon")
        {
            Destroy(gameObject);
        }

        ;
    }

    /*private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if(transform.position.x >= Random.Range(3, 6))
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            moveingRight = false;
        }
        else if (transform.position.x <= Random.Range(-3, -6))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveingRight = true;
        }
    }*/
}
