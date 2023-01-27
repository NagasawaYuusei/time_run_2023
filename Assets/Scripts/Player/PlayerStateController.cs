using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    #region �ϐ�

    [Header("�ݒu����")]
    [SerializeField, Tooltip("�n�ʂ̃��C���[")]
    LayerMask _groundLayer;

    [SerializeField, Tooltip("�ݒu����̃T�C�Y")]
    Vector3 _groundDetectionSize;

    [SerializeField, Tooltip("�ݒu����̒��S")]
    Vector3 _groundDetectionCentor;

    [Tooltip("�ړ����̐ݒu����̒��S")]
    Vector3 _playerCentor;

    [SerializeField, Tooltip("�ݒu������������邩�ǂ���")]
    bool _isGizmo;


    [Tooltip("�v���C���[��Rigidbody")]
    static Rigidbody _rb;

    [SerializeField, Tooltip("�n�ʎ��̏d��")]
    float _groundDrag;

    [SerializeField, Tooltip("�󒆎��̏d��")]
    float _airDrag;

    [Tooltip("�v���C���[�̃g�����X�t�H�[��")]
    Transform _transform;

    [Tooltip("�v���C���[�̏��")]
    static PlayerStates _playerStatesEnum = PlayerStates.None;

    [Tooltip("�X�s�[�h���������邩�ǂ���")]
    bool _isPlayerVelocityWatch;

    #endregion

    #region �v���p�e�B
    public static PlayerStateController Instance;
    static public PlayerStates PlayerState => _playerStatesEnum;
    static public Rigidbody PlayerRigidbody => _rb;
    #endregion

    #region �C�x���g
    public event Action<PlayerStates> OnPlayerStateChangeDisable;
    public event Action<PlayerStates> OnPlayerStateChangeEnable;
    #endregion

    #region Unity���\�b�h

    void Awake()
    {
        //�C���X�^���X�̏����@�V�[�����ƂɃC���X�^���X�ς��������炨�������Ȃ��Ă�
        if(Instance && Instance.gameObject)
        {
            Debug.LogWarning("Instance���������[");
        }
        Instance = this;
    }

    void Start()
    {
        Setup();
    }

    void Update()
    {
        State();
        VelocityWatch();
        DragControl();
    }

    #endregion

    #region ���\�b�h

    void Setup()
    {
        //RequireComponent�m���Ă邯�ǃf�o�b�O���o���������炱������
        if (!TryGetComponent(out _rb))
        {
            Debug.LogWarning("Rb�Ȃ���`");
            _rb = gameObject.AddComponent<Rigidbody>();
        }
        _transform = GetComponent<Transform>();
    }

    void State()
    {
        _playerCentor = _transform.position + _groundDetectionCentor;
    }

    void VelocityWatch()
    {
        if(_isPlayerVelocityWatch)
        {
            Debug.Log(_rb.velocity);
        }
    }

    void DragControl()
    {
        if (IsGround())
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = _airDrag;
        }
    }

    public void ChangePlayerState(PlayerStates state)
    {
        //switch(state)
        //{
        //    case PlayerStates.None:
        //        break;
        //    case PlayerStates.Idle:
        //        break;
        //    case PlayerStates.Fly:
        //        break;
        //    case PlayerStates.WallRun:
        //        break;
        //    case PlayerStates.Pause:
        //        break;
        //    default:
        //        break;
        //}

        if (state == _playerStatesEnum)
        {
            Debug.LogWarning("State�ꏏ����[");
            return;
        }

        OnPlayerStateChangeDisable?.Invoke(_playerStatesEnum);
        _playerStatesEnum = state;
        OnPlayerStateChangeEnable?.Invoke(_playerStatesEnum);
    }

    public bool IsGround()
    {
        Collider[] collision = Physics.OverlapBox(_playerCentor, _groundDetectionSize, Quaternion.identity, _groundLayer);
        if (collision.Length != 0)
        {
            if(_playerStatesEnum != PlayerStates.WallRun)
            {
                ChangePlayerState(PlayerStates.Fly);
            }
            return true;
        }
        else
        {
            ChangePlayerState(PlayerStates.Idle);
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

    public enum PlayerStates
    {
        None,
        Idle,
        WallRun,
        Fly,
        Pause
    }

    #endregion
}
