using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("�X�s�[�h�̐ݒ�")]
    [SerializeField, Tooltip("�v���C���[�̓����X�s�[�h")]
    float _playerMoveSpeed = 12f;

    [Tooltip("�v���C���[�X�s�[�h�̏搔")]
    float _playerMultipleSpeed = 10f;

    [SerializeField]
    float _playerJumpPower = 10f;

    [Tooltip("�C���v�b�g�V�X�e���̃R���|�[�l���g"), SerializeField]
    PlayerInput _playerInput;

    Rigidbody _rb;

    Transform _transform;

    Vector2 _moveDir;

    bool _isJump;

    [SerializeField]
    LayerMask _groundLayer;

    [SerializeField]
    Vector3 _groundDetectionSize;

    [SerializeField]
    Vector3 _groundDetectionCentor;

    Vector3 _playerCentor;

    [SerializeField]
    bool _isGizmo;

    #region �C�x���g�o�^
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
        State();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void SetUp()
    {
        //RequireComponent�m���Ă邯�ǃf�o�b�O���o���������炱������
        if (!TryGetComponent(out _rb))
        {
            Debug.LogWarning("Rb�Ȃ���`");
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

    //���x�v���C���[�X�e�[�g�I�ȃN���X����邩������z���܂�
    void State()
    {
        _playerCentor = _transform.position + _groundDetectionCentor;
    }

    void Jump()
    {
        if(_isJump && IsGround())
        {
            _rb.AddForce(Vector3.up * _playerJumpPower);
            _isJump = false;
        }
    }

    //���x�v���C���[�X�e�[�g�I�ȃN���X����邩������z���܂�
    public bool IsGround()
    {
        Collider[] collision = Physics.OverlapBox(_playerCentor, _groundDetectionSize, Quaternion.identity, _groundLayer);
        if (collision.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGizmo)
        {
            Gizmos.DrawCube(_playerCentor, _groundDetectionSize);
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
