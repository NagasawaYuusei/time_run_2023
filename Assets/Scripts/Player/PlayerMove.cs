using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("")] float _playerMoveSpeed = 5f;
    [SerializeField, Tooltip("")] float _playerMultipleSpeed = 1f;
    [SerializeField] PlayerInput _playerInput;
    Rigidbody _rb;
    Transform _transform;
    Vector2 _moveDir;

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
        if(!TryGetComponent(out _rb))
        {
            Debug.LogWarning("RbÇ»Ç¢ÇÊÅ`");
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

    private void OnEnable()
    {
        _playerInput.onActionTriggered += OnMove;
    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move")
            return;
        Debug.Log("Move");
        _moveDir = context.ReadValue<Vector2>();
    }
}
