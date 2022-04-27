using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveing: MonoBehaviour
{
    private bool _move;
    private Vector2 _mousePos;
    private float _startPosX;
    private float _startPosY;
    private bool _finish;
    
    public GameObject correctDomino;

    void OnMouseDown(){
        if(Input.GetMouseButtonDown(0)){
            _move = true;
            _mousePos = Input.mousePosition;

            _startPosX = _mousePos.x - this.transform.localPosition.x;
            _startPosY = _mousePos.y - this.transform.localPosition.y;
        }
    }

    // Нуждается в переработке, необходимо уйти от счетчика в сторону, вероятнее всего, true/false
    void OnMouseUp(){
        _move = false;

        
        if(Mathf.Abs(this.transform.localPosition.x - correctDomino.transform.localPosition.x)<=5f&&
           Mathf.Abs(this.transform.localPosition.y - correctDomino.transform.localPosition.y)<=5f){
               this.transform.position = new Vector2(correctDomino.transform.position.x, correctDomino.transform.position.y);
               _finish = true;
               Destroy(gameObject);
               correctDomino.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255,255);
               Open.AddElement();
        }
    }

   
    void Update()
    {
        if(_move == true && _finish == false){
            _mousePos = Input.mousePosition;
            this.gameObject.transform.localPosition = new Vector2(_mousePos.x - _startPosX, _mousePos.y-_startPosY);
        }
    }
}
