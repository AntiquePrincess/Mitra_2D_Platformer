using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Fli : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private UnityEngine.Object explosion;

    public Transform point2;
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
        if (angry == false)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, Playre.position) < stoppingDistance) // Можно еще подумать
        {

            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, Playre.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }
        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
    }
    void Chill()
    {
        if (transform.position.x == point.position.x)
        {
            moveingRight = true;
        }
        else if (transform.position.x == point2.position.x)
        {
            moveingRight = false;
        }

        if (moveingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, point2.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Weapon"))
        {
            health--;
            spriteRend.material = matBlink;

            if (health <= 0)
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
}
