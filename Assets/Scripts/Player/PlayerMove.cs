using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    #region �ϐ�
    [Header("�X�s�[�h�̐ݒ�")]
    [SerializeField, Tooltip("�v���C���[�̓����X�s�[�h")]
    float _playerMoveSpeed = 12f;

    [Tooltip("�v���C���[�X�s�[�h�̏搔")]
    float _playerMultipleSpeed = 10f;

    [Tooltip("�v���C���[�̍ő�X�s�[�h"), SerializeField]
    float _playerMaximizeSpeed = 100f;
    

    [SerializeField, Tooltip("�v���C���[�̃W�����v�X�s�[�h")]
    float _playerJumpPower = 10f;

    [Tooltip("�W�����v�X�s�[�h�̏搔")]
    float _playerMultipleJumpPower = 100f;

    [SerializeField, Tooltip("�C���v�b�g�V�X�e���̃R���|�[�l���g")]
    PlayerInput _playerInput;

    [Tooltip("Rigidbody")] 
    Rigidbody _rb;

    [Tooltip("�����̕���")]
    Vector3 _moveDir;

    [Tooltip("�W�����v�������ǂ���")]
    bool _isJump;

    [SerializeField, Tooltip("�v���C���[�̃X�e�[�^�X")]
    PlayerStateController _state;
    #endregion

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
    }

    void Move()
    {
        Vector3 dir = Camera.main.transform.TransformDirection(_moveDir);
        dir.y = 0;
        Debug.DrawRay(transform.position, dir.normalized * 10, Color.red);
        _rb.AddForce((dir.normalized * _playerMoveSpeed * _playerMultipleSpeed) + _rb.velocity.y * Vector3.up, ForceMode.Acceleration);
    }

    void Jump()
    {
        if(_isJump && _state.IsGround())
        {
            _rb.AddForce(Vector3.up * _playerJumpPower * _playerMultipleJumpPower);
            _isJump = false;
        }
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move")
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
