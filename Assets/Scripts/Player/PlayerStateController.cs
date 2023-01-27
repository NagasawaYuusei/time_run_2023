using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    #region 変数

    [Header("設置判定")]
    [SerializeField, Tooltip("地面のレイヤー")]
    LayerMask _groundLayer;

    [SerializeField, Tooltip("設置判定のサイズ")]
    Vector3 _groundDetectionSize;

    [SerializeField, Tooltip("設置判定の中心")]
    Vector3 _groundDetectionCentor;

    [Tooltip("移動時の設置判定の中心")]
    Vector3 _playerCentor;

    [SerializeField, Tooltip("設置判定を可視化するかどうか")]
    bool _isGizmo;


    [Tooltip("プレイヤーのRigidbody")]
    static Rigidbody _rb;

    [SerializeField, Tooltip("地面時の重力")]
    float _groundDrag;

    [SerializeField, Tooltip("空中時の重力")]
    float _airDrag;

    [Tooltip("プレイヤーのトランスフォーム")]
    Transform _transform;

    [Tooltip("プレイヤーの状態")]
    static PlayerStates _playerStatesEnum = PlayerStates.None;

    [Tooltip("スピードを可視化するかどうか")]
    bool _isPlayerVelocityWatch;

    #endregion

    #region プロパティ
    public static PlayerStateController Instance;
    static public PlayerStates PlayerState => _playerStatesEnum;
    static public Rigidbody PlayerRigidbody => _rb;
    #endregion

    #region イベント
    public event Action<PlayerStates> OnPlayerStateChangeDisable;
    public event Action<PlayerStates> OnPlayerStateChangeEnable;
    #endregion

    #region Unityメソッド

    void Awake()
    {
        //インスタンスの処理　シーンごとにインスタンス変えたいからおかしくなってる
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
        VelocityWatch();
        DragControl();
    }

    #endregion

    #region メソッド

    void Setup()
    {
        //RequireComponent知ってるけどデバッグを出したいからこうする
        if (!TryGetComponent(out _rb))
        {
            Debug.LogWarning("Rbないよ～");
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
