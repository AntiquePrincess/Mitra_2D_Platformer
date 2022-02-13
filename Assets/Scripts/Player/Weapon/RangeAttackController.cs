using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackController : MonoBehaviour
{
    public GameObject StonePrefab;
    public Transform RangeWeaponPosition;
    public float timeBtwShot;
    public Transform TargetPoint;

    private Stone _currentStone;
    private Vector3 _direction;
    private float _timer;
    private float _rotationZ;
    private bool _hasStone = false;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateStone();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        CreateStone();
        Shot();
    }

    public void Shot()
    {
        if (Input.GetButton("Fire1") && _hasStone)
        {
            _currentStone.Shot();
            _currentStone.transform.parent = null;
            _hasStone = false;
        }
    }
    private void Aim()
    {
        if (Input.GetButton("Fire2"))
        {
            _direction = TargetPoint.position - RangeWeaponPosition.position;
            _rotationZ = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            RangeWeaponPosition.rotation = Quaternion.Euler(0, 0,_rotationZ);
        }
    }

    private void CreateStone()
    {
        if (!_hasStone &&  _timer <= 0)
        {
            _currentStone = Instantiate(StonePrefab, RangeWeaponPosition.GetChild(0)).GetComponent<Stone>();
            _hasStone = true;
            _timer = timeBtwShot;
        }
        else if (!_hasStone)
        {
            _timer -= Time.deltaTime;
        }
    }
}
