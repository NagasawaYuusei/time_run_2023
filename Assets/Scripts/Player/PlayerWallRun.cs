using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _wallRunRaySize;
    RaycastHit _hitWall;
    [SerializeField] LayerMask _wallLayer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void WallRunReady()
    {
        if(Physics.Raycast(transform.position, _cameraTransform.right, out _hitWall, _wallRunRaySize, _wallLayer))
        {

        } 
        else if(Physics.Raycast(transform.position, -_cameraTransform.right, out _hitWall, _wallRunRaySize, _wallLayer))
        {

        }
    }
}
