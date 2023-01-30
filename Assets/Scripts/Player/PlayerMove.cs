using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    #region 変数
    [Header("スピードの設定")]
    [SerializeField, Tooltip("プレイヤーの動くスピード")]
    float _playerMoveSpeed = 12f;

    [Tooltip("プレイヤースピードの乗数")]
    float _playerMultipleSpeed = 10f;

    [Tooltip("プレイヤーの最大スピード"), SerializeField]
    float _playerMaximizeSpeed = 100f;
    

    [SerializeField, Tooltip("プレイヤーのジャンプスピード")]
    float _playerJumpPower = 10f;

    [Tooltip("ジャンプスピードの乗数")]
    float _playerMultipleJumpPower = 100f;

    [SerializeField, Tooltip("インプットシステムのコンポーネント")]
    PlayerInput _playerInput;

    [Tooltip("動きの方向")]
    Vector3 _moveDir;

    [Tooltip("ジャンプしたかどうか")]
    bool _isJump;

    [SerializeField, Tooltip("プレイヤーのステータス")]
    PlayerStateController _state;

    [SerializeField]
    PlayerSpeedController _speedController;

    #endregion

    #region イベント登録
    private void OnEnable()
    {
        _playerInput.onActionTriggered += OnMove;
        _playerInput.onActionTriggered += OnJump;
    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnMove;
        _playerInput.onActionTriggered -= OnJump;
    }
    #endregion

    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 dir = Camera.main.transform.TransformDirection(_moveDir);
        dir.y = 0;
        PlayerStateController.PlayerRigidbody.AddForce((dir.normalized * _speedController.PlayerSpeed * _playerMultipleSpeed)
            + PlayerStateController.PlayerRigidbody.velocity.y * Vector3.up, ForceMode.Acceleration);
    }

    void Jump()
    {
        if(_isJump && _state.IsGround())
        {
            PlayerStateController.PlayerRigidbody.AddForce(Vector3.up * _playerJumpPower * _playerMultipleJumpPower);
            _isJump = false;
        }
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move" && PlayerStateController.PlayerState != PlayerStateController.PlayerStates.WallRun)
            return;

        Vector2 dir = context.ReadValue<Vector2>();
        _moveDir = new Vector3(dir.x, 0, dir.y);
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.action.name != "Jump") 
            return;

        if (context.started)
        { 
            _isJump = true;
        }

        if (context.canceled)
        {
            _isJump = false;
        }
    }
}
