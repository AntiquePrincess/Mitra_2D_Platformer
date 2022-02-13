using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _doorAnim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _doorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        _doorAnim.SetBool("Open", true);
    }

    public void Close()
    {
        _doorAnim.SetBool("Open", false);
    }
}
