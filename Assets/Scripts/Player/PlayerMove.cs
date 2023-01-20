using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("スピードの設定")]
    [SerializeField, Tooltip("プレイヤーの動くスピード")]
    float _playerMoveSpeed = 12f;

    [Tooltip("プレイヤースピードの乗数")]
    float _playerMultipleSpeed = 10f;

    [SerializeField]
    float _playerJumpPower = 10f;

    [Tooltip("インプットシステムのコンポーネント"), SerializeField]
    PlayerInput _playerInput;

    Rigidbody _rb;

    Vector2 _moveDir;

    bool _isJump;

    [SerializeField] PlayerState _state;

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

    void Start()
    {
        SetUp();
    }

    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void SetUp()
    {
        //RequireComponent知ってるけどデバッグを出したいからこうする
        if (!TryGetComponent(out _rb))
        {
            Debug.LogWarning("Rbないよ～");
            _rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    void Move()
    {
        Vector3 dir = Camera.main.transform.TransformDirection(_moveDir);
        dir.y = 0;
        _rb.AddForce((dir.normalized * _playerMoveSpeed * _playerMultipleSpeed) + _rb.velocity.y * Vector3.up, ForceMode.Acceleration);
    }

    void Jump()
    {
        if(_isJump && _state.IsGround())
        {
            _rb.AddForce(Vector3.up * _playerJumpPower);
            _isJump = false;
        }
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move")
            return;

        _moveDir = context.ReadValue<Vector2>();
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
