using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] Transform _apoint;
    [SerializeField] Transform _bpoint;
    [SerializeField] bool _isLate;
    [SerializeField] LineRenderer _lr;

    // Update is called once per frame
    void Update()
    {
        if(!_isLate)
        {
            DrawRoape();
        }
    }

    void LateUpdate()
    {
        if(_isLate)
        {
            DrawRoape();
        }
    }

    void DrawRoape()
    {
        _lr.SetPosition(0, _apoint.position);
        _lr.SetPosition(1, _bpoint.position);
    }
}
