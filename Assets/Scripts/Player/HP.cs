using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{

    public int health;
    public int numberOfLives;

    public Image[] lives;

    public Sprite fullLivse;
    public Sprite emptyLivse;

    void Start()
    {
        
    }

    void Update()
    {
        if(health > numberOfLives)
        {
            health = numberOfLives;
        }

        for (int i = 0; i < lives.Length; i++)
        {
            if(i < health)
            {
                lives[i].sprite = fullLivse;
            }
            else
            {
                lives[i].sprite = emptyLivse;
            }

            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

}
