using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open: MonoBehaviour
{
    int fullElement;
    public static int myElement;
    public GameObject myPuzzl; 
    public GameObject myPanel;
    public GameObject winText;
    public GameObject winButton;

    void Start()
    {
        fullElement = myPuzzl.transform.childCount;
    }

    void Update()
    {
        if(fullElement == myElement){
            myPanel.SetActive(false);
            winText.SetActive(true);
            winButton.SetActive(true);
        }
    }
    
    public static void AddElement(){
        myElement ++;
    }
}
