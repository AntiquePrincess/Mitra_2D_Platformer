using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    public Movement mov;
    void Start()
    {
        //mov = GetCompoent<Movement>();
    }
    void Update()
    {
        mov.Jump();
        mov.Wolk();
    }
}
