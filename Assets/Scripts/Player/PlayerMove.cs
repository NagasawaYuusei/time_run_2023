using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("スピードの設定")]
    [SerializeField, Tooltip("プレイヤーの動くスピード")]
    float _playerMoveSpeed = 12f;

    [Tooltip("プレイヤースピードの乗数")]
    float _playerMultipleSpeed = 10f;

    [Tooltip("インプットシステムのコンポーネント"), SerializeField]
    PlayerInput _playerInput;

    Rigidbody _rb;

    Transform _transform;

    Vector2 _moveDir;

    #region イベント登録
    private void OnEnable()
    {
        _playerInput.onActionTriggered += OnMove;
    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnMove;
    }
    #endregion

    void Start()
    {
        SetUp();
    }

    void FixedUpdate()
    {
        Move();
    }

    void SetUp()
    {
        //RequireComponent知ってるけどデバッグを出したいからこうする
        if(!TryGetComponent(out _rb))
        {
            Debug.LogWarning("Rbないよ～");
            _rb = gameObject.AddComponent<Rigidbody>();
        }
        
        _transform = GetComponent<Transform>();
    }

    void Move()
    {
        Vector3 dir = Camera.main.transform.TransformDirection(_moveDir);
        dir.y = 0;
        _rb.AddForce((dir.normalized * _playerMoveSpeed * _playerMultipleSpeed) + _rb.velocity.y * Vector3.up, ForceMode.Acceleration);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move")
            return;
        _moveDir = context.ReadValue<Vector2>();
    }
}
