using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject platform;
    private bool _moveLeft = false;

    public float xl, xr;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (_moveLeft && platform.transform.position.x > xl)
            platform.transform.Translate(Vector2.left * Time.deltaTime);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && transform.position.y > -4.7f)
        {
            transform.Translate(Vector2.down * Time.deltaTime);
        }
        else if (collision.tag == "Player" && platform.transform.position.x < xr)
        {
            platform.transform.Translate(Vector2.right * Time.deltaTime);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        transform.position = new Vector2(transform.position.x, -4.23f);
        _moveLeft = true;

    }
}
