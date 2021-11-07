using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHold : MonoBehaviour
{

    // ПЛОХО РАБОЧЕЕ ГАВНО НАДО РАБОТАТЬ НАД Weapon_TEST
    public bool hold;
    public float distance = 2f;
    RaycastHit2D hit;
    public Transform holdPoint;

    //public Transform Cam;
    public GameObject Cam;
    public Transform shotDir;

    public float offset;

    public float thrwObj = 6;
    public float thrwObj_x = 3;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);//  костыль
                //hit = Physics2D.OverlapCircle(shotDir.position, distance, ;

                if (hit.collider != null && hit.collider.tag == "Weapon")
                {
                    hold = true;
                }
            }
            else
            {
                hold = false;
                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * thrwObj_x, 1) * thrwObj;// может иметь другое применение

                    if (Input.GetMouseButtonDown(1))
                    {
                        Instantiate(Cam, shotDir.position, transform.rotation);
                    }
                }
            }
        }

        if (hold)
        {
            hit.collider.gameObject.transform.position = holdPoint.position;

            if(holdPoint.position.x > transform.position.x && hold == true)
            {
                hit.collider.gameObject.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            }
            else if (holdPoint.position.x < transform.position.x && hold == true)
            {
                hit.collider.gameObject.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            }
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // костыль
            Cam.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);//    костыль
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
