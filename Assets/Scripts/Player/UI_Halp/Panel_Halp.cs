using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Halp : MonoBehaviour
{
    private int _name_ = 0;
    public GameObject panelHalpe;
    public Text text;

    private bool Gun = false;
    private bool scenee = true;

    public string[] message;
    void Start()
    {
        message[0] = "a";
        message[1] = "b";
        message[2] = "c";
        message[3] = "d";
        panelHalpe.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && scenee)
        {
            Gun = true;
            panelHalpe.SetActive(true);
 
            text.text = message[_name_];

            if(_name_ == 3)
            {
            scenee = false;
            }



        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Gun = false;
        panelHalpe.SetActive(false);
    }
    void Update()
    {
        if (Gun)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(_name_ < 3)
                {
                _name_ += 1;

                }
                text.text = message[_name_];
            }
        }



    }
}
