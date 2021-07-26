using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public float speed = 3;
    public float speedDfa = 3;

    private bool speedBlock = true;
    #region
    public float jumpForse = 20;
    private bool jumpControl;
    private int jumpIteration = 0;
    public int jumpValueration = 45;
    private bool jumpUP;
    private bool JumpSap;
    private int JumpeAnim = 0;
    #endregion
    private bool blockFlip;

    private Rigidbody2D rb;
    private Animator anim;

    public Vector2 moveVector;

    private bool blockMoveXforJump;
    private bool blockMoveXYforLedge = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GroundcheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;

        WallcheckRadiusDoun = WallCheckDoun.GetComponent<CircleCollider2D>().radius;
        gravityDef = rb.gravityScale;

    }

    private void Update()
    {
        CheckingGround();
        Flip();
        Wolk();
        AnimJump();
        Jump();

        CheckingWall();
        MoveOnWall();
        LedgeGo();

    }
    private void FixedUpdate()
    {
        ChackingLedge();
    }

    void Flip()
    {
        if (!blockFlip)
        {
            if (Input.GetAxis("Horizontal") > 0) transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (Input.GetAxis("Horizontal") < 0) transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Wolk()//ходьба
    {
        if (blockMoveXforJump || blockMoveXYforLedge )
        {
            moveVector.x = 0;
        }
        else
        {
            moveVector.x = Input.GetAxis("Horizontal");
            anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
        }
            rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }
    #region Прыжок    
    void Sap()//функция  для прыжка
    {
        jumpUP = false;
        JumpeAnim_f1();
    }
    void JumpeLook()//функция  для прыжка
    {
        jumpUP = true;
    }
    void JumpeAnim_f1()//функция содержащяя переменную для 
    {
        JumpeAnim = 0;
    }
    void JumpeAnim_f()//функция содержащяя переменную для 
    {
        JumpeAnim = 1;
        jumpUP = false;
    }
    void JumpeAnim_0()
    {
        JumpeAnim = 2;
    }
    void AnimJump()//Анимация прыжка
    {
        anim.SetBool("jumpUP", jumpUP);
        anim.SetInteger("Anim", JumpeAnim);
        anim.SetBool("JumpSap", JumpSap);
        if (Input.GetKeyDown(KeyCode.W) && onGround == true && JumpeAnim != 1)// Down
        {
            JumpeAnim = 2;
        }
        if (JumpeAnim == 1 && onGround == true)//остоновка
        {
            speed = 0;
        }
        if (JumpeAnim != 1)
        {
            speed = speedDfa;
        }

        if (JumpeAnim != 0)//переход к анимации прыжка
        {
            if (JumpeAnim == 2)
            {
                JumpSap = true;
                anim.SetBool("JumpGo", true);
            }

        }
        else
        {
            JumpSap = false;
            anim.SetBool("JumpGo", false);
        }

    }
    void Jump()//прыжок
    {
        if (Input.GetKey(KeyCode.W) && jumpUP)
        {
            if (onGround == true) { jumpControl = true; } //1
        }
        else//1
        {
            jumpControl = false;//1
        }
        if (jumpControl)//1
        {
            if (jumpIteration++ < jumpValueration)//1
            {
                rb.AddForce(Vector2.up * jumpForse / jumpIteration);//прыжок//1
            }
        }
        else//1
        {
            jumpIteration = 0;//1
        }

    }
    #endregion Jump

#region Draw cube to checking touch with Ground
[SerializeField] LayerMask Ground;
    [SerializeField] float GroundcheckRadius = 0.3f; 
    [SerializeField] Transform GroundCheck;
    [SerializeField] private bool onGround;
    #endregion End draw
    public void CheckingGround() // Проверка земли
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundcheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
    
    #region Draw cube to checking touch with Wall
    [SerializeField] LayerMask Wall;
    public float WallcheckRayDistance = 1f;
    public float WallcheckRadiusDoun;
    [SerializeField] Transform WallCheckDoun;
    [SerializeField] Transform WallCheckUp;
    [SerializeField] private bool onWall;
    [SerializeField] private bool onWallUp;
    [SerializeField] private bool onWallDoun;
    public bool onLedge;
    public float ledgeRayCorrectY = 0.5f;

    #endregion End draw
    public void CheckingWall() // Проверка стены
    {
        onWallUp = Physics2D.Raycast(WallCheckUp.position, new Vector2(transform.localScale.x, 0), WallcheckRayDistance, Wall);
        onWallDoun = Physics2D.OverlapCircle(WallCheckDoun.position, WallcheckRadiusDoun, Wall);

        onWall = (onWallUp && onWallDoun);
        anim.SetBool("onWall", onWall);

    }
    void ChackingLedge()// Проверка уступа
    {
        if (onWallUp)
        {
            onLedge = !Physics2D.Raycast
            (
                new Vector2(WallCheckUp.position.x, WallCheckUp.position.y + ledgeRayCorrectY),
                new Vector2(transform.localScale.x, 0),
                WallcheckRayDistance,
                Wall
                );
        }
        else
        {
            onLedge = false;
        }
        anim.SetBool("onLedge", onLedge);
        if (onLedge && Input.GetAxisRaw("Vertical") != -1 || blockMoveXYforLedge)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            offsetCalculateAndCorrect();
        }

    }
    public float offsetY;
    public float minCorrectDistance = 0.01f;

    void offsetCalculateAndCorrect() // Денамический прыжок?
    {
        offsetY = Physics2D.Raycast
        (
            new Vector2(WallCheckUp.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUp.position.y + ledgeRayCorrectY),
            Vector2.down,
            ledgeRayCorrectY,
            Ground
        ).distance;
        if (offsetY > minCorrectDistance * 1.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - offsetY + minCorrectDistance, transform.position.z);
        }
    }
    private void OnDrawGizmos() // Визуализация(оформление)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(WallCheckUp.position, new Vector2(WallCheckUp.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUp.position.y));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(WallCheckUp.position.x, WallCheckUp.position.y + ledgeRayCorrectY), 
            new Vector2(WallCheckUp.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUp.position.y + ledgeRayCorrectY)
            );
        Gizmos.color = Color.green;
        Gizmos.DrawLine
          (
            new Vector2(WallCheckUp.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUp.position.y + ledgeRayCorrectY),
            new Vector2(WallCheckUp.position.x + WallcheckRayDistance * transform.localScale.x, WallCheckUp.position.y)
          );
    }
    void LedgeGo() // уступ идти(дословно) запуск
    {
        //blockMoveXYforLedge = true; // ОБРАТИЛ ВНИМАНИЕ
        if (onLedge && Input.GetKeyDown(KeyCode.UpArrow))
        {

            anim.Play("WallLedgeClimb");
        }
    }

    public Transform finishLedgePosition;
    void FinishLedge() // перемещение на позицыю при подеме
    {
        transform.position = new Vector3(finishLedgePosition.position.x, finishLedgePosition.position.y, finishLedgePosition.position.z);
        blockMoveXYforLedge = false;
    }
    # region карабконье
    public float UpDounSpeed = 4f;
    public float slideSpeed = 0;
    private float gravityDef;
    
    void MoveOnWall() // Корабконье
    {
        if((onWall && !onGround))
        {
            moveVector.y = Input.GetAxisRaw("Vertical");
            anim.SetFloat("UpDoun",moveVector.y);

            if (moveVector.y == 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, slideSpeed);
            }

            if (!blockMoveXYforLedge)
            {
                if(moveVector.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveVector.y * (UpDounSpeed));
                }
                else if (moveVector.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveVector.y * (UpDounSpeed));
                }
            }
        }
        else if(!onGround && !onWall)
        {
            rb.gravityScale = gravityDef;
        }
    }
    #endregion End draw

}
