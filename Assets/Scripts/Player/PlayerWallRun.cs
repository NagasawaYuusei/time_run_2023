using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _wallRunRaySize;
    RaycastHit _hitWall;
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] PlayerStateController _playerState;
    [SerializeField] float _wallRunSpeed = 1;

    void Update()
    {
        WallRunReady();
    }

    void WallRunReady()
    {
        if (Physics.Raycast(transform.position, _cameraTransform.right, out _hitWall, _wallRunRaySize, _wallLayer) && !_playerState.IsGround())
        {
            PlayerStateController.Instance.ChangePlayerState(PlayerStateController.PlayerStates.WallRun);
            Vector3 v0 = _hitWall.normal;
            Vector3 v1 = new Vector3(0, 90, 0);
            Vector3 v2 = new Vector3(v0.x, 0, v0.z).normalized;
            Vector3 v3 = Quaternion.Euler(v1) * v2;
            Debug.DrawRay(transform.position, v3 * 100, Color.red);
            PlayerStateController.PlayerRigidbody.AddForce(v3 * 10 * _wallRunSpeed, ForceMode.Acceleration);
            PlayerStateController.PlayerRigidbody.useGravity = false;
        }
        else if (Physics.Raycast(transform.position, -_cameraTransform.right, out _hitWall, _wallRunRaySize, _wallLayer) && !_playerState.IsGround())
        {
            PlayerStateController.Instance.ChangePlayerState(PlayerStateController.PlayerStates.WallRun);
            Vector3 v0 = _hitWall.normal;
            Vector3 v1 = new Vector3(0, 90, 0);
            Vector3 v2 = new Vector3(v0.x, 0, v0.z).normalized;
            Vector3 v3 = Quaternion.Euler(-v1) * v2;
            Debug.DrawRay(transform.position, v3 * 100, Color.red);
            PlayerStateController.PlayerRigidbody.AddForce(v3 * 10 * _wallRunSpeed, ForceMode.Acceleration);
            PlayerStateController.PlayerRigidbody.useGravity = false;
        }
        else
        {
            PlayerStateController.Instance.ChangePlayerState(PlayerStateController.PlayerStates.Idle);
            PlayerStateController.PlayerRigidbody.useGravity = true;
        }
    }
}
