using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("�X�s�[�h�̐ݒ�")]
    [SerializeField, Tooltip("�v���C���[�̓����X�s�[�h")]
    float _playerMoveSpeed = 12f;

    [Tooltip("�v���C���[�X�s�[�h�̏搔")]
    float _playerMultipleSpeed = 10f;

    [Tooltip("�C���v�b�g�V�X�e���̃R���|�[�l���g"), SerializeField]
    PlayerInput _playerInput;

    Rigidbody _rb;

    Transform _transform;

    Vector2 _moveDir;

    #region �C�x���g�o�^
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
        //RequireComponent�m���Ă邯�ǃf�o�b�O���o���������炱������
        if(!TryGetComponent(out _rb))
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

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move")
            return;
        _moveDir = context.ReadValue<Vector2>();
    }
}
