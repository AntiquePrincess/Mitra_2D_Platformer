using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public hit gunhit;
    public bool hit = true;//Есть в руке камень или нет
    public bool dablhitnot = true; 

    public GameObject BulletPrefab;
    public float Power = 100;

    public float offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        float enter;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        new Plane(-Vector3.forward, transform.position).Raycast(ray, out enter);
        Vector3 mouseInWorld = ray.GetPoint(enter);

        Vector3 speed = (mouseInWorld - transform.position) * Power;
        transform.rotation = Quaternion.LookRotation(speed); 


        if (Input.GetMouseButtonDown(0) && hit)
        {
            hit = false;
            Rigidbody2D bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bullet.AddForce(speed, ForceMode2D.Impulse);
            //Destroy(gameObject);

        }
        if (hit)
        {
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    /* Идея такова будет два шарика , шарик пушка и шарик пуля.
     * По умолчанию на карте всегда будет лежать предмет камень пуля ,но  подбирая его мы будем его как бы уничтожать
     * и спавнить у себя в руке камень пушку. 
     * Совершая выстрел мы будем удалять камень пушку и спавнить камень пулю.
     * 
     * Надо будет придумать как пофиксить уничтожение двух камней с взятием одного,
     * урон по монстрам при отсутствии двежения.
     * 
     * Надо зделать выстрел возможным только при повороте в ту же сторону куда смотрит персонаж.
     */
}
