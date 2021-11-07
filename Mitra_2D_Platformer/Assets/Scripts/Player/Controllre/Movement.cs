using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed = 2.0f;
    public GameObject character;
    public bool bloc;
    public bool blocJump;
    public bool blocWolk;
    private Rigidbody2D rb;
    private Animator anim;

    #region
    public float jumpForse = 20;
    private bool jumpControl;
    private int jumpIteration = 0;
    public int jumpValueration = 45;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundcheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    void Update()
    {
        Jump();
        Wolk();
    }
    public void Jump()
    {
        if (bloc == false && blocJump == false) 
        {

            if (Input.GetKey(KeyCode.W))
            {
                if (onGround == true) { jumpControl = true; }
            }
            else
            {
                jumpControl = false;
            }
            if (jumpControl)
            {
                if (jumpIteration++ < jumpValueration)
                {
                    rb.AddForce(Vector2.up * jumpForse / jumpIteration);
                }
            }
            else
            {
                jumpIteration = 0;
            }
        }

    }
    public void Wolk()
    {
        if (bloc == false && blocWolk == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //transform.position += Vector3.right * speed * Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x + speed, rb.velocity.y);
            }
            if (Input.GetKey(KeyCode.A))
            {
                //transform.position += Vector3.left * speed * Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x - speed, rb.velocity.y);
            }
        }
    }

    #region Draw cube to checking touch with Ground
    [SerializeField] LayerMask Ground;
    [SerializeField] float GroundcheckRadius = 0.3f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] private bool onGround;
    #endregion End draw
    public void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundcheckRadius, Ground);
        //anim.SetBool("onGround", onGround);
    }
}