using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    public static PlayerStateController Instance;

    [SerializeField]
    LayerMask _groundLayer;

    [SerializeField]
    Vector3 _groundDetectionSize;

    [SerializeField]
    Vector3 _groundDetectionCentor;

    Vector3 _playerCentor;

    [SerializeField]
    bool _isGizmo;

    Rigidbody _rb;

    [SerializeField] float _groundDrag;
    [SerializeField] float _airDrag;

    Transform _transform;

    static PlayerStates _playerStatesEnum = PlayerStates.None;

    #region プロパティ
    static public PlayerStates PlayerState => _playerStatesEnum;
    #endregion

    #region イベント
    public event Action<PlayerStates> OnPlayerStateChangeDisable;
    public event Action<PlayerStates> OnPlayerStateChangeEnable;
    #endregion


    void Awake()
    {
        if(Instance && Instance.gameObject)
        {
            Debug.LogWarning("Instance複数あるよー");
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
        DragControl();
    }

    void Setup()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void State()
    {
        _playerCentor = _transform.position + _groundDetectionCentor;
        //Debug.Log(_rb.velocity);
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
            Debug.LogWarning("State一緒だよー");
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

    public enum PlayerStates
    {
        None,
        Idle,
        WallRun,
        Fly,
        Pause
    }
}
