using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChackGround : MonoBehaviour
{
    public bool checkGround;

    private void OnCollisionEnter2D(Collision2D collision)// косание чего либа
    {
        if (collision.gameObject.tag == "Groud") // при создании тэга в сцене пропустил букву (n) и потаму тут тоже ошибка.
        {
            checkGround = true;
        }
        else
        {
            checkGround = false;
        }
    }
}
